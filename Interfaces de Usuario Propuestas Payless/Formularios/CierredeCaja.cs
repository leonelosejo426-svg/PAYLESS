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
        private decimal montoArqueado = 0;
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
                cajaDAO.ObtenerCajaAbierta();

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

            DataRow fila =
                tabla.Rows[0];

            idCajaActual =
                Convert.ToInt32(
                    fila["id_caja"]);

            saldoInicial =
                Convert.ToDecimal(
                    fila["saldo_inicial"]);

            ingresos =
                cierreDAO.ObtenerIngresos(
                    idCajaActual);

            egresos =
                cierreDAO.ObtenerEgresos(
                    idCajaActual);

            montoEsperado =
                saldoInicial +
                ingresos -
                egresos;

            if (fila["monto_arqueo"] != DBNull.Value)
            {
                montoArqueado =
                    Convert.ToDecimal(
                        fila["monto_arqueo"]);
            }

            diferencia =
                montoArqueado -
                montoEsperado;

            efectivoCordobas =
                cierreDAO.ObtenerEfectivoCordobas(
                    idCajaActual);

            decimal dolares =
                cierreDAO.ObtenerEfectivoDolares(
                    idCajaActual);

            decimal tipoCambio =
                Convert.ToDecimal(
                    fila["tipo_cambio_dolar"]);

            efectivoDolares =
                dolares * tipoCambio;

            tarjetas =
                cierreDAO.ObtenerTarjetas(
                    idCajaActual);

            MostrarInformacion();
        }

        // =====================================================
        // MOSTRAR INFORMACIÓN
        // =====================================================
        private void MostrarInformacion()
        {
            lblSaldoInicial.Text =
                "C$ " +
                saldoInicial.ToString("N2");

            lblIngresos.Text =
                "C$ " +
                ingresos.ToString("N2");

            lblEgresos.Text =
                "C$ " +
                egresos.ToString("N2");

            lblMontoEsperado.Text =
                "C$ " +
                montoEsperado.ToString("N2");

            lblMontoArqueado.Text =
                "C$ " +
                montoArqueado.ToString("N2");

            lblDiferencia.Text =
                "C$ " +
                diferencia.ToString("N2");

            lblEfectivoCordobas.Text =
                "C$ " +
                efectivoCordobas.ToString("N2");

            lblEfectivoDolares.Text =
                "C$ " +
                efectivoDolares.ToString("N2");

            lblMontoTarjetas.Text =
                "C$ " +
                tarjetas.ToString("N2");

            // El saldo final corresponde
            // al dinero físico arqueado.
            lblSaldoFinal.Text =
                "C$ " +
                montoArqueado.ToString("N2");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta =
                MessageBox.Show(
                    "¿Está seguro de cerrar la caja?\n\n" +
                    "Monto esperado: C$ " +
                    montoEsperado.ToString("N2") +
                    "\nMonto arqueado: C$ " +
                    montoArqueado.ToString("N2") +
                    "\nDiferencia: C$ " +
                    diferencia.ToString("N2"),
                    "Confirmar cierre de caja",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            bool resultado =
                cierreDAO.GuardarCierre(
                    idCajaActual,
                    montoEsperado,
                    montoArqueado,
                    diferencia,
                    montoArqueado);

            if (resultado)
            {
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
