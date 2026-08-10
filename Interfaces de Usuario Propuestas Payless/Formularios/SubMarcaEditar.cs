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
    public partial class SubMarcaEditar : Form
    {
        MarcaDAO marcaDAO = new MarcaDAO();

        public SubMarcaEditar()
        {
            InitializeComponent();
        }

        private void SubMarcaEditar_Load(object sender, EventArgs e)
        {
            CargarMarcas();

            cmbMarca.Items.Clear();

            cmbMarca.Items.Add("Activo");
            cmbMarca.Items.Add("Inactivo");

            cmbMarca.SelectedIndex = 0;

            txtCodigo.ReadOnly = true;
        }

        private void CargarMarcas()
        {
            cmbMarca.DataSource =
                marcaDAO.CargarMarcas();

            cmbMarca.DisplayMember =
                "nombre_marca";

            cmbMarca.ValueMember =
                "id_marca";

            cmbMarca.SelectedIndex = -1;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbMarca.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione una marca.");

                return;
            }

            int idMarca =
                Convert.ToInt32(cmbMarca.SelectedValue);

            CargarMarca(idMarca);
        }
        private void CargarMarca(int idMarca)
        {
            ClaseMarca marca =
                marcaDAO.ObtenerMarca(idMarca);

            if (marca == null)
            {
                MessageBox.Show(
                    "No se encontró la marca.");

                return;
            }

            txtCodigo.Text =
                marca.IdMarca.ToString();

            txtNombre.Text =
                marca.NombreMarca;

            txtDescripcion.Text =
                marca.Descripcion;

            cmbMarca.SelectedItem =
                marca.Estado
                ? "Activo"
                : "Inactivo";
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show(
                    "Primero busque una marca.");

                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show(
                    "Ingrese el nombre de la marca.");

                txtNombre.Focus();
                return;
            }

            ClaseMarca marca = new ClaseMarca();

            marca.IdMarca =
                Convert.ToInt32(txtCodigo.Text);

            marca.NombreMarca =
                txtNombre.Text.Trim();

            marca.Descripcion =
                txtDescripcion.Text.Trim();

            marca.Estado =
                cmbMarca.SelectedItem.ToString()
                == "Activo";

            if (marcaDAO.EditarMarca(marca))
            {
                MessageBox.Show(
                    "Marca actualizada correctamente.",
                    "Correcto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo actualizar la marca.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
