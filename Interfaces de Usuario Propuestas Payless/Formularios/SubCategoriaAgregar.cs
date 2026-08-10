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
    public partial class SubCategoriaAgregar : Form
    {
        CategoriaDAO categoriaDAO = new CategoriaDAO();
        public SubCategoriaAgregar()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Categoria ventana = new Categoria();
            ventana.Show();
            this.Hide();
        }

        private void SubCategoriaAgregar_Load(object sender, EventArgs e)
        {
            cmbEstado.Items.Clear();

            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Inactivo");

            cmbEstado.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show(
                    "Ingrese el nombre de la categoría.",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNombre.Focus();
                return;
            }

            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione el estado de la categoría.",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbEstado.Focus();
                return;
            }

            ClaseCategoria categoria = new ClaseCategoria();

            categoria.NombreCategoria =
                txtNombre.Text.Trim();

            categoria.Descripcion =
                txtDescripcion.Text.Trim();

            categoria.Estado =
                cmbEstado.Text == "Activo";

            bool resultado =
                categoriaDAO.AgregarCategoria(categoria);

            if (resultado)
            {
                MessageBox.Show(
                    "Categoría agregada correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Categoria ventana = new Categoria();
                ventana.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo agregar la categoría.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
