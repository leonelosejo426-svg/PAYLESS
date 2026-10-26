using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class CierredeCaja : Form
    {

        private CierreCajaDAO cierreDAO =
           new CierreCajaDAO();

        private CajaDAO cajaDAO =
            new CajaDAO();

        private int idCajaActual = 0;

        private decimal saldoInicial = 0;
        private decimal ingresos = 0;
        private decimal egresos = 0;
        private decimal montoEsperado = 0;

        private decimal montoArqueo = 0;
        private decimal diferencia = 0;

        private decimal efectivoCordobas = 0;
        private decimal efectivoDolares = 0;
        private decimal tarjetas = 0;

        public CierredeCaja()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CierredeCaja_Load(object sender, EventArgs e)
        {
            lblUsuario.Text =
               ClaseSesion.UsuarioActual;

            lblFecha.Text =
                DateTime.Now.ToString(
                    "dd/MM/yyyy");

            lblHora.Text =
                DateTime.Now.ToString(
                    "HH:mm:ss");

            CargarCierre();
        }

        // CARGAR INFORMACIÓN
        // =====================================================
        private void CargarCierre()
        {
            DataTable tabla =
                cajaDAO.ObtenerCajaAbierta(ClaseSesion.IdUsuario);

            if (tabla.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No hay una caja abierta.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                Close();
                return;
            }

            DataRow fila = tabla.Rows[0];

            idCajaActual = Convert.ToInt32(fila["id_caja"]);
            saldoInicial = Convert.ToDecimal(fila["saldo_inicial"]);

            // 1. Cargar desgloses puros de la base de datos
            egresos = cierreDAO.ObtenerEgresos(idCajaActual);
            efectivoCordobas = cierreDAO.ObtenerEfectivoCordobas(idCajaActual);
            tarjetas = cierreDAO.ObtenerTarjetas(idCajaActual);

            decimal dollarsFisicos = cierreDAO.ObtenerEfectivoDolares(idCajaActual);
            decimal tipoCambio = Convert.ToDecimal(fila["tipo_cambio_dolar"]);
            efectivoDolares = dollarsFisicos * tipoCambio;

            // 2. Traemos el ingreso neto real facturado (C$ 14,163.05)
            ingresos = cierreDAO.ObtenerIngresos(idCajaActual);

            // =========================================================================
            // 🧮 CORRECCIÓN DE LUGAR: ASIGNACIÓN CORRECTA DE SALDOS
            // =========================================================================

            // MONTO ESPERADO (SISTEMA): Debe dar tus C$ 14,463.05 perfectos de la caja principal
            montoEsperado = saldoInicial + ingresos - egresos;

            // MONTO ARQUEADO (LO QUE TÚ ARQUEASTE): Leemos directo la columna de tu pantalla de Arqueo
            montoArqueo = fila["monto_arqueo"] != DBNull.Value
                ? Convert.ToDecimal(fila["monto_arqueo"])
                : 0m;

            // TU FÓRMULA ESTRICTA: Monto contado (físico) - Monto esperado (sistema)
            diferencia = montoArqueo - montoEsperado;

            // =========================================================================

            MostrarInformacion();
        }





        // =====================================================
        // MOSTRAR INFORMACIÓN
        // =====================================================
        private void MostrarInformacion()
        {
            lblSaldoInicial.Text = "C$ " + saldoInicial.ToString("N2");
            lblIngresos.Text = "C$ " + ingresos.ToString("N2");
            lblEgresos.Text = "C$ " + egresos.ToString("N2");
            lblMontoEsperado.Text = "C$ " + montoEsperado.ToString("N2");
            lblMontoArqueado.Text = "C$ " + montoArqueo.ToString("N2");
            lblDiferencia.Text = "C$ " + diferencia.ToString("N2");
            lblEfectivoCordobas.Text = "C$ " + efectivoCordobas.ToString("N2");
            lblEfectivoDolares.Text = "C$ " + efectivoDolares.ToString("N2");
            lblMontoTarjetas.Text = "C$ " + tarjetas.ToString("N2");

            // CORRECCIÓN: El saldo final de cierre debe incluir el efectivo físico arqueado + las ventas por tarjeta
            decimal saldoFinalTotal = montoArqueo + tarjetas;
            lblSaldoFinal.Text = "C$ " + saldoFinalTotal.ToString("N2");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta =
        MessageBox.Show(
            "¿Está seguro de cerrar la caja?\n\n" +
            "Monto esperado: C$ " + montoEsperado.ToString("N2") +
            "\nMonto arqueado: C$ " + montoArqueo.ToString("N2") +
            "\nDiferencia: C$ " + diferencia.ToString("N2"),
            "Confirmar cierre de caja",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            // Calculamos el saldo final total (Efectivo arqueado + Tarjetas)
            decimal saldoFinalGlobal = montoArqueo + tarjetas;

            bool resultado =
                cierreDAO.GuardarCierre(
                    idCajaActual,
                    montoEsperado,
                    montoArqueo,
                    diferencia,
                    saldoFinalGlobal);

            if (resultado)
            {
                ClaseSesion.TieneCajaActiva = false;

                MessageBox.Show(
                    "Caja cerrada correctamente.",
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Close();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo cerrar la caja.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}