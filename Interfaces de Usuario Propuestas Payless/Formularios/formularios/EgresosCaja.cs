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

namespace Interfaces_de_Usuario_Propuestas_Payless.Formularios
{
    public partial class EgresosCaja : Form
    {

        private EgresosCajaDAO egresosDAO =
           new EgresosCajaDAO();

        private CajaDAO cajaDAO =
            new CajaDAO();

        private int idCajaActual = 0;

        public EgresosCaja()
        {
            InitializeComponent();
        }

        private void EgresosCaja_Load(object sender, EventArgs e)
        {
            ObtenerCajaActual();
        }

        // OBTENER CAJA ABIERTA
        // =====================================================
        private void ObtenerCajaActual()
        {
            var tabla =
                cajaDAO.ObtenerCajaAbierta();

            if (tabla.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No hay una caja abierta.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                idCajaActual = 0;
                return;
            }

            idCajaActual =
                Convert.ToInt32(
                    tabla.Rows[0]["id_caja"]);
        }

        private void btnGuardarMovimiento_Click(object sender, EventArgs e)
        {
            string descripcion =
               txtConcepto.Text.Trim();

            if (string.IsNullOrEmpty(descripcion))
            {
                MessageBox.Show(
                    "Ingrese el concepto del egreso.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtConcepto.Focus();
                return;
            }

            decimal monto;

            if (!decimal.TryParse(
                txtMonto.Text.Trim(),
                out monto))
            {
                MessageBox.Show(
                    "Ingrese un monto válido.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtMonto.Focus();
                return;
            }

            if (monto <= 0)
            {
                MessageBox.Show(
                    "El monto debe ser mayor que cero.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtMonto.Focus();
                return;
            }

            if (idCajaActual == 0)
            {
                MessageBox.Show(
                    "No hay una caja abierta.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            bool resultado =
                egresosDAO.RegistrarEgreso(
                    descripcion,
                    monto,
                    idCajaActual);

            if (resultado)
            {
                MessageBox.Show(
                    "Egreso registrado correctamente.",
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtConcepto.Clear();
                txtMonto.Clear();

                txtConcepto.Focus();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo registrar el egreso.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();

        }
    }
}
