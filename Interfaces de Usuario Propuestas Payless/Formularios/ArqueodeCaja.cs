using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using Interfaces_de_Usuario_Propuestas_Payless.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class ArqueodeCaja : Form
    {

        private ArqueoCajaDAO arqueoDAO = new ArqueoCajaDAO();
        private int idCajaActual = 0;

        private ClaseSesion clasesesion = new ClaseSesion();
        private decimal tipoCambio = 36.50m;

        // Ventas esperadas del turno por moneda
        private decimal ventasEsperadasCordobas = 0m;
        private decimal ventasEsperadasDolares = 0m;
        private decimal ventasTotalesGlobales = 0m; 

        public ArqueodeCaja()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ArqueodeCaja_Load(object sender, EventArgs e)
        {
            // =====================================================
            // ORDEN DE TABULACIONES (TAB INDEX)
            // =====================================================
            // 1. Primero los Dólares (de mayor a menor denominación)
            txt100Dolares.TabIndex = 1;
            txt50Dolares.TabIndex = 2;
            txt20Dolares.TabIndex = 3;
            txt10Dolares.TabIndex = 4;
            txt5Dolares.TabIndex = 5;
            txt1Dolar.TabIndex = 6;

            // 2. Luego los Billetes de Córdobas (de mayor a menor)
            txt1000Cordobas.TabIndex = 7;
            txt500Cordobas.TabIndex = 8;
            txt200Cordobas.TabIndex = 9;
            txt100Cordobas.TabIndex = 10;
            txt50Cordobas.TabIndex = 11;
            txt20Cordobas.TabIndex = 12;
            txt10Cordobas.TabIndex = 13;
            txt5Cordobas.TabIndex = 14;

            // 3. Finalmente las Monedas de Córdobas
            txt5Moneda.TabIndex = 15;
            txt1Moneda.TabIndex = 16;
            txt50Centavos.TabIndex = 17;
            txt25Centavos.TabIndex = 18;

            lblUsuario.Text = ClaseSesion.UsuarioActual;

            idCajaActual = arqueoDAO.ObtenerCajaAbiertaPorUsuario(ClaseSesion.IdUsuario);

            if (idCajaActual == 0)
            {
                MessageBox.Show("No hay una caja abierta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            tipoCambio = arqueoDAO.ObtenerTipoCambio(idCajaActual);

            if (Controls.Find("lblTipoCambio", true).Length > 0)
                lblTipoCambio.Text = "C$ " + tipoCambio.ToString("N2");

            // 1. Obtener valores base y movimientos de la caja
            decimal saldoInicial = arqueoDAO.ObtenerSaldoInicialCaja(idCajaActual);
            decimal egresos = arqueoDAO.ObtenerEgresosCaja(idCajaActual);

            // Obtenemos los valores puros correctos
            decimal ventasCordobasPuras = arqueoDAO.ObtenerVentasCordobasCaja(idCajaActual);
            decimal ventasDolaresPuras = arqueoDAO.ObtenerVentasDolaresCaja(idCajaActual);
            decimal ventasTarjeta = arqueoDAO.ObtenerVentasTarjetaCaja(idCajaActual);

            decimal dolaresConvertidosVentas = ventasDolaresPuras * tipoCambio;

            // --- TOTAL GLOBAL COMERCIAL (Incluye Tarjetas) ---
            ventasTotalesGlobales = ventasCordobasPuras + dolaresConvertidosVentas + ventasTarjeta + saldoInicial - egresos;

            if (Controls.Find("lblVentasTotales", true).Length > 0)
            {
                lblVentasTotales.Text = "C$ " + ventasTotalesGlobales.ToString("N2");
            }

            if (lblTotalTarjeta != null)
            {
                lblTotalTarjeta.Text = "C$ " + ventasTarjeta.ToString("N2");
            }

            // --- VALORES ESPERADOS REALES EN GAVETA (Físico puro) ---
            // Las córdobas esperadas llevan el saldo inicial + córdobas netas de ventas - egresos
            ventasEsperadasCordobas = saldoInicial + ventasCordobasPuras - egresos;

            // Los dólares esperados son exactamente los dólares puros recibidos en efectivo en las ventas
            ventasEsperadasDolares = ventasDolaresPuras;

            ConfigurarEventos();
            CalcularArqueo();
        }


        // CONECTAR TEXTBOX CON EL CÁLCULO
        // =====================================================
        private void ConfigurarEventos()
        {
            // Dólares
            txt100Dolares.TextChanged += Cantidades_TextChanged;
            txt50Dolares.TextChanged += Cantidades_TextChanged;
            txt20Dolares.TextChanged += Cantidades_TextChanged;
            txt10Dolares.TextChanged += Cantidades_TextChanged;
            txt5Dolares.TextChanged += Cantidades_TextChanged;
            txt1Dolar.TextChanged += Cantidades_TextChanged;

            // Billetes Córdobas
            txt1000Cordobas.TextChanged += Cantidades_TextChanged;
            txt500Cordobas.TextChanged += Cantidades_TextChanged;
            txt200Cordobas.TextChanged += Cantidades_TextChanged;
            txt100Cordobas.TextChanged += Cantidades_TextChanged;
            txt50Cordobas.TextChanged += Cantidades_TextChanged;
            txt20Cordobas.TextChanged += Cantidades_TextChanged;
            txt10Cordobas.TextChanged += Cantidades_TextChanged;
            txt5Cordobas.TextChanged += Cantidades_TextChanged;

            // Monedas Córdobas
            txt5Moneda.TextChanged += Cantidades_TextChanged;
            txt1Moneda.TextChanged += Cantidades_TextChanged;
            txt50Centavos.TextChanged += Cantidades_TextChanged;
            txt25Centavos.TextChanged += Cantidades_TextChanged;
        }

        private void Cantidades_TextChanged(
            object sender,
            EventArgs e)
        {
            CalcularArqueo();
        }

        // =====================================================
        // CONVERTIR CANTIDAD
        // =====================================================
        private int Cantidad(TextBox txt)
        {
            if (txt != null && int.TryParse(txt.Text.Trim(), out int cantidad) && cantidad >= 0)
                return cantidad;
            return 0;
        }

        // =====================================================
        // CALCULAR ARQUEO
        // =====================================================
        private void CalcularArqueo()
        {
            // 1. CONTEO DE DÓLARES
            int[] cantDolares = {
            Cantidad(txt100Dolares), Cantidad(txt50Dolares), Cantidad(txt20Dolares),
            Cantidad(txt10Dolares),  Cantidad(txt5Dolares),  Cantidad(txt1Dolar)
        };
            decimal[] denomDolares = { 100m, 50m, 20m, 10m, 5m, 1m };

            lblTotal100Dolares.Text = "$ " + (cantDolares[0] * 100m).ToString("N2");
            lblTotal50Dolares.Text = "$ " + (cantDolares[1] * 50m).ToString("N2");
            lblTotal20Dolares.Text = "$ " + (cantDolares[2] * 20m).ToString("N2");
            lblTotal10Dolares.Text = "$ " + (cantDolares[3] * 10m).ToString("N2");
            lblTotal5Dolares.Text = "$ " + (cantDolares[4] * 5m).ToString("N2");
            lblTotal1Dolar.Text = "$ " + (cantDolares[5] * 1m).ToString("N2");

            decimal totalDolares = RecursividadArqueo.SumarDenominacionesRecursivo(cantDolares, denomDolares);

            // CORREGIDO: Se agregó el operador de multiplicación (*)
            decimal conversionDolares = totalDolares * tipoCambio;

            // 2. CONTEO DE BILLETES CÓRDOBAS
            int[] cantBilletesCor = {
            Cantidad(txt1000Cordobas), Cantidad(txt500Cordobas), Cantidad(txt200Cordobas), Cantidad(txt100Cordobas),
            Cantidad(txt50Cordobas),   Cantidad(txt20Cordobas),  Cantidad(txt10Cordobas),  Cantidad(txt5Cordobas)
        };
            decimal[] denomBilletesCor = { 1000m, 500m, 200m, 100m, 50m, 20m, 10m, 5m };

            lblTotal1000Cordobas.Text = "C$ " + (cantBilletesCor[0] * 1000m).ToString("N2");
            lblTotal500Cordobas.Text = "C$ " + (cantBilletesCor[1] * 500m).ToString("N2");
            lblTotal200Cordobas.Text = "C$ " + (cantBilletesCor[2] * 200m).ToString("N2");
            lblTotal100Cordobas.Text = "C$ " + (cantBilletesCor[3] * 100m).ToString("N2");
            lblTotal50Cordobas.Text = "C$ " + (cantBilletesCor[4] * 50m).ToString("N2");
            lblTotal20Cordobas.Text = "C$ " + (cantBilletesCor[5] * 20m).ToString("N2");
            lblTotal10Cordobas.Text = "C$ " + (cantBilletesCor[6] * 10m).ToString("N2");
            lblTotal5Cordobas.Text = "C$ " + (cantBilletesCor[7] * 5m).ToString("N2");

            decimal totalBilletesCordobas = RecursividadArqueo.SumarDenominacionesRecursivo(cantBilletesCor, denomBilletesCor);

            // 3. CONTEO DE MONEDAS CÓRDOBAS
            int[] cantMonedas = {
            Cantidad(txt5Moneda), Cantidad(txt1Moneda), Cantidad(txt50Centavos), Cantidad(txt25Centavos)
        };
            decimal[] denomMonedas = { 5m, 1m, 0.50m, 0.25m };

            lblTotal5Moneda.Text = "C$ " + (cantMonedas[0] * 5m).ToString("N2");
            lblTotal1Moneda.Text = "C$ " + (cantMonedas[1] * 1m).ToString("N2");
            lblTotal50Centavos.Text = "C$ " + (cantMonedas[2] * 0.50m).ToString("N2");
            lblTotal25Centavos.Text = "C$ " + (cantMonedas[3] * 0.25m).ToString("N2");

            decimal totalMonedas = RecursividadArqueo.SumarDenominacionesRecursivo(cantMonedas, denomMonedas);

            // 4. ATRIBUTO ÚNICO PARA BD: MONTO TOTAL ARQUEADO
            decimal montoTotalArqueado = totalBilletesCordobas + totalMonedas + conversionDolares;

            if (Controls.Find("lblMontoTotalArqueado", true).Length > 0)
                lblMontoTotalArqueado.Text = "C$ " + montoTotalArqueado.ToString("N2");

            // 5. RESUMEN VISUAL EN PANTALLA
            // Aquí mostramos el total global comercial real de las ventas del turno
            if (Controls.Find("lblVentasTotales", true).Length > 0)
                lblVentasTotales.Text = "C$ " + ventasTotalesGlobales.ToString("N2");

            if (Controls.Find("lblTotalEnCordobas", true).Length > 0)
                lblTotalEnCordobas.Text = "C$ " + totalBilletesCordobas.ToString("N2");

            if (Controls.Find("lblTotalEnMonedas", true).Length > 0)
                lblTotalEnMonedas.Text = "C$ " + totalMonedas.ToString("N2");

            if (Controls.Find("lblTotalEnDolares", true).Length > 0)
                lblTotalEnDolares.Text = "$ " + totalDolares.ToString("N2");

            if (Controls.Find("lblConversionDolares", true).Length > 0)
                lblConversionDolares.Text = "C$ " + conversionDolares.ToString("N2");

            // --- DIFERENCIA EXCLUSIVA EN CÓRDOBAS ---
            decimal totalCordobasContadas = totalBilletesCordobas + totalMonedas;
            var (faltanteCor, sobranteCor) = RecursividadArqueo.CalcularDiferenciaRecursivo(ventasEsperadasCordobas, totalCordobasContadas);

            if (Controls.Find("lblFaltanteCordobas", true).Length > 0)
                lblFaltanteCordobas.Text = "C$ " + faltanteCor.ToString("N2");

            if (Controls.Find("lblSobranteCordobas", true).Length > 0)
                lblSobranteCordobas.Text = "C$ " + sobranteCor.ToString("N2");

            // --- DIFERENCIA EXCLUSIVA EN DÓLARES ---
            var (faltanteDol, sobranteDol) = RecursividadArqueo.CalcularDiferenciaRecursivo(ventasEsperadasDolares, totalDolares);

            if (Controls.Find("lblFaltanteDolares", true).Length > 0)
                lblFaltanteDolares.Text = "$ " + faltanteDol.ToString("N2");

            if (Controls.Find("lblSobranteDolares", true).Length > 0)
                lblSobranteDolares.Text = "$ " + sobranteDol.ToString("N2");
        }



        private void ActualizarResumenArqueo(decimal totalBilletesCordobas, decimal totalMonedas, decimal totalDolares, decimal conversionDolares)
        {
            // Totales contados
            lblTotalEnCordobas.Text = "C $ " + totalBilletesCordobas.ToString("N2");
            lblTotalEnMonedas.Text = "C $ " + totalMonedas.ToString("N2");
            lblTotalEnDolares.Text = "$ " + totalDolares.ToString("N2");

            // Totales conversión y billetes dólares
           // lblTotalBilletesDolar.Text = "$ " + totalDolares.ToString("N2");
            lblConversionDolares.Text = "C $ " + conversionDolares.ToString("N2");

            // Diferencias Córdobas (Esperado vs Contado)
            decimal totalCordobasReal = totalBilletesCordobas + totalMonedas;
            var (faltanteCor, sobranteCor) = RecursividadArqueo.CalcularDiferenciaRecursivo(ventasEsperadasCordobas, totalCordobasReal);

            lblFaltanteCordobas.Text = "C $ " + faltanteCor.ToString("N2");
            lblSobranteCordobas.Text = "C $ " + sobranteCor.ToString("N2");

            // Diferencias Dólares (Esperado vs Contado)
            var (faltanteDol, sobranteDol) = RecursividadArqueo.CalcularDiferenciaRecursivo(ventasEsperadasDolares, totalDolares);

            //lblFaltanteDolares.Text = "$ " + faltanteDol.ToString("N2");
            //lblSobranteDolares.Text = "$ " + sobranteDol.ToString("N2");
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private void label45_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            decimal montoArqueo = ObtenerMontoArqueado();

            // 1. Obtenemos los valores base de la caja
            decimal saldoInicial = arqueoDAO.ObtenerSaldoInicialCaja(idCajaActual);
            decimal egresos = arqueoDAO.ObtenerEgresosCaja(idCajaActual);
            decimal ventasCordobasPuras = arqueoDAO.ObtenerVentasCordobasCaja(idCajaActual);
            decimal ventasDolaresPuras = arqueoDAO.ObtenerVentasDolaresCaja(idCajaActual);
            decimal ventasTarjeta = arqueoDAO.ObtenerVentasTarjetaCaja(idCajaActual);

            // 2. MONTO ESPERADO PURAMENTE EN EFECTIVO (Sin tarjetas, para la gaveta)
            decimal montoEsperadoEfectivo = saldoInicial + ventasCordobasPuras + (ventasDolaresPuras * tipoCambio) - egresos;

            // Diferencia entre lo que contaste físicamente y el efectivo real esperado
            decimal diferenciaFinal = montoArqueo - montoEsperadoEfectivo;

            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de guardar el arqueo?\n\n" +
                "Monto arqueado: C$ " + montoArqueo.ToString("N2") + "\n" +
                "Diferencia: C$ " + diferenciaFinal.ToString("N2"),
                "Confirmar arqueo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            // 3. Saldo final global consolidado de la caja (Efectivo arqueado + Tarjetas)
            decimal saldoFinalTotal = montoArqueo + ventasTarjeta;

            // 4. Guardamos en la BD
            bool resultado = arqueoDAO.GuardarArqueo(
                idCajaActual,
                montoEsperadoEfectivo, // Aquí viaja el esperado limpio de efectivo
                montoArqueo,
                diferenciaFinal,       // Aquí viaja la diferencia real de la gaveta
                saldoFinalTotal        // Aquí viaja el total global con tarjetas para el cierre
            );

            if (resultado)
            {
                MessageBox.Show("Arqueo guardado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show("No se pudo guardar el arqueo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private decimal ObtenerMontoArqueado()
        {
            // Limpiamos los símbolos de moneda, espacios y las comas de los miles
            string texto = lblMontoTotalArqueado.Text
                .Replace("C$", "")
                .Replace("C $", "")
                .Replace(",", "")
                .Trim();

            // Usamos InvariantCulture para que reconozca el punto decimal sin importar la configuración regional
            if (decimal.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal monto))
            {
                return monto;
            }

            return 0m;
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
    }
}
