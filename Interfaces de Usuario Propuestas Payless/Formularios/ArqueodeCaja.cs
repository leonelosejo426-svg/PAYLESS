using Interfaces_de_Usuario_Propuestas_Payless.Datos;
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

        private ArqueoCajaDAO arqueoDAO =
           new ArqueoCajaDAO();

        private int idCajaActual = 0;
        private decimal tipoCambio = 36.50m;


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
            idCajaActual =
                arqueoDAO.ObtenerCajaAbierta();

            if (idCajaActual == 0)
            {
                MessageBox.Show(
                    "No hay una caja abierta.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                Close();
                return;
            }

            tipoCambio =
                arqueoDAO.ObtenerTipoCambio(
                    idCajaActual);

            lblTipoCambio.Text =
                "C$ " +
                tipoCambio.ToString("N2");

            ConfigurarEventos();
            CalcularArqueo();
        }


        // CONECTAR TEXTBOX CON EL CÁLCULO
        // =====================================================
        private void ConfigurarEventos()
        {
            txt100Dolares.TextChanged +=
                Cantidades_TextChanged;

            txt50Dolares.TextChanged +=
                Cantidades_TextChanged;

            txt20Dolares.TextChanged +=
                Cantidades_TextChanged;

            txt20Dolares.TextChanged +=
                Cantidades_TextChanged;

            txt5Dolares.TextChanged +=
                Cantidades_TextChanged;

            txt1Dolar.TextChanged +=
                Cantidades_TextChanged;

            txt1000Cordobas.TextChanged +=
                Cantidades_TextChanged;

            txt500Cordobas.TextChanged +=
                Cantidades_TextChanged;

            txt200Cordobas.TextChanged +=
                Cantidades_TextChanged;

            txt100Cordobas.TextChanged +=
                Cantidades_TextChanged;

            txt50Cordobas.TextChanged +=
                Cantidades_TextChanged;

            txt20Cordobas.TextChanged +=
                Cantidades_TextChanged;

            txt10Cordobas.TextChanged +=
                Cantidades_TextChanged;

            txt5Cordobas.TextChanged +=
                Cantidades_TextChanged;

            txt5Moneda.TextChanged +=
                Cantidades_TextChanged;

            txt1Moneda.TextChanged +=
                Cantidades_TextChanged;

            txt50Centavos.TextChanged +=
                Cantidades_TextChanged;

            txt25Centavos.TextChanged +=
                Cantidades_TextChanged;
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
        private int Cantidad(
            TextBox txt)
        {
            int cantidad;

            if (int.TryParse(
                txt.Text.Trim(),
                out cantidad))
            {
                if (cantidad >= 0)
                    return cantidad;
            }

            return 0;
        }

        // =====================================================
        // CALCULAR ARQUEO
        // =====================================================
        private void CalcularArqueo()
        {
            decimal total100D =
                Cantidad(txt100Dolares) * 100m;

            decimal total50D =
                Cantidad(txt50Dolares) * 50m;

            decimal total20D =
                Cantidad(txt20Dolares) * 20m;

            decimal total10D =
                Cantidad(txt20Dolares) * 10m;

            decimal total5D =
                Cantidad(txt5Dolares) * 5m;

            decimal total1D =
                Cantidad(txt1Dolar) * 1m;

            lblTotal100Dolares.Text =
                "$ " + total100D.ToString("N2");

            lblTotal50Dolares.Text =
                "$ " + total50D.ToString("N2");

            lblTotal20Dolares.Text =
                "$ " + total20D.ToString("N2");

            lblTotal10Dolares.Text =
                "$ " + total10D.ToString("N2");

            lblTotal5Dolares.Text =
                "$ " + total5D.ToString("N2");

            lblTotal1Dolar.Text =
                "$ " + total1D.ToString("N2");

            decimal totalDolares =
                total100D +
                total50D +
                total20D +
                total10D +
                total5D +
                total1D;

            lblTotalBilletesDolares.Text =
                "$ " +
                totalDolares.ToString("N2");

            decimal conversionDolares =
                totalDolares * tipoCambio;

            lblConversionDolares.Text =
                "C$ " +
                conversionDolares.ToString("N2");

            // =================================================
            // CÓRDOBAS
            // =================================================

            decimal total1000 =
                Cantidad(txt1000Cordobas) * 1000m;

            decimal total500 =
                Cantidad(txt500Cordobas) * 500m;

            decimal total200 =
                Cantidad(txt200Cordobas) * 200m;

            decimal total100 =
                Cantidad(txt100Cordobas) * 100m;

            decimal total50 =
                Cantidad(txt50Cordobas) * 50m;

            decimal total20 =
                Cantidad(txt20Cordobas) * 20m;

            decimal total10 =
                Cantidad(txt10Cordobas) * 10m;

            decimal total5 =
                Cantidad(txt5Cordobas) * 5m;

            lblTotal1000Cordobas.Text =
                "C$ " + total1000.ToString("N2");

            lblTotal500Cordobas.Text =
                "C$ " + total500.ToString("N2");

            lblTotal200Cordobas.Text =
                "C$ " + total200.ToString("N2");

            lblTotal100Cordobas.Text =
                "C$ " + total100.ToString("N2");

            lblTotal50Cordobas.Text =
                "C$ " + total50.ToString("N2");

            lblTotal20Cordobas.Text =
                "C$ " + total20.ToString("N2");

            lblTotal10Cordobas.Text =
                "C$ " + total10.ToString("N2");

            lblTotal5Cordobas.Text =
                "C$ " + total5.ToString("N2");

            decimal totalBilletesCordobas =
                total1000 +
                total500 +
                total200 +
                total100 +
                total50 +
                total20 +
                total10 +
                total5;

            lblTotalBilletesCordobas.Text =
                "C$ " +
                totalBilletesCordobas.ToString("N2");

            // =================================================
            // MONEDAS
            // =================================================

            decimal total5Moneda =
                Cantidad(txt5Moneda) * 5m;

            decimal total1Moneda =
                Cantidad(txt1Moneda) * 1m;

            decimal total50Centavos =
                Cantidad(txt50Centavos) * 0.50m;

            decimal total25Centavos =
                Cantidad(txt25Centavos) * 0.25m;

            lblTotal5Moneda.Text =
                "C$ " +
                total5Moneda.ToString("N2");

            lblTotal1Moneda.Text =
                "C$ " +
                total1Moneda.ToString("N2");

            lblTotal50Centavos.Text =
                "C$ " +
                total50Centavos.ToString("N2");

            lblTotal25Centavos.Text =
                "C$ " +
                total25Centavos.ToString("N2");

            decimal totalMonedas =
                total5Moneda +
                total1Moneda +
                total50Centavos +
                total25Centavos;

            lblTotalMonedas.Text =
                "C$ " +
                totalMonedas.ToString("N2");

            // =================================================
            // TOTAL ARQUEADO
            // =================================================

            decimal totalCordobas =
                totalBilletesCordobas +
                totalMonedas;

            decimal montoTotal =
                totalCordobas +
                conversionDolares;

            lblMontoTotalArqueado.Text =
                "C$ " +
                montoTotal.ToString("N2");
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
            decimal montoArqueo =
                ObtenerMontoArqueado();

            DialogResult respuesta =
                MessageBox.Show(
                    "¿Está seguro de guardar el arqueo?\n\n" +
                    "Monto arqueado: C$ " +
                    montoArqueo.ToString("N2"),
                    "Confirmar arqueo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            bool resultado =
                arqueoDAO.GuardarArqueo(
                    idCajaActual,
                    montoArqueo);

            if (resultado)
            {
                MessageBox.Show(
                    "Arqueo guardado correctamente.",
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Close();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo guardar el arqueo.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private decimal ObtenerMontoArqueado()
        {
            string texto =
                lblMontoTotalArqueado.Text
                .Replace("C$", "")
                .Trim();

            decimal monto;

            if (decimal.TryParse(
                texto,
                NumberStyles.Number,
                CultureInfo.CurrentCulture,
                out monto))
            {
                return monto;
            }

            return 0;
        }

    }
}
