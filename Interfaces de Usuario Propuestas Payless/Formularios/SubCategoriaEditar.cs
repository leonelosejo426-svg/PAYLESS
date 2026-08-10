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
    public partial class SubCategoriaEditar : Form
    {
        CategoriaDAO categoriaDAO = new CategoriaDAO();
        public SubCategoriaEditar()
        {
            InitializeComponent();
        }

        private void SubCategoriaEditar_Load(object sender, EventArgs e)
        {
            CargarCategorias();

            cmbEstado.Items.Clear();

            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Inactivo");

            cmbEstado.SelectedIndex = -1;

            LimpiarCampos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione una categoría.",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int idCategoria =
                Convert.ToInt32(cmbCategoria.SelectedValue);

            ClaseCategoria categoria =
                categoriaDAO.ObtenerCategoria(idCategoria);

            if (categoria != null)
            {
                idCategoriaSeleccionada =
                    categoria.IdCategoria;

                txtNombre.Text =
                    categoria.NombreCategoria;

                txtDescripcion.Text =
                    categoria.Descripcion;

                if (categoria.Estado)
                {
                    cmbEstado.SelectedItem = "Activo";
                }
                else
                {
                    cmbEstado.SelectedItem = "Inactivo";
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (idCategoriaSeleccionada == 0)
            {
                MessageBox.Show(
                    "Primero seleccione y busque una categoría.",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

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
                    "Seleccione el estado.",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbEstado.Focus();
                return;
            }

            ClaseCategoria categoria =
                new ClaseCategoria();

            categoria.IdCategoria =
                idCategoriaSeleccionada;

            categoria.NombreCategoria =
                txtNombre.Text.Trim();

            categoria.Descripcion =
                txtDescripcion.Text.Trim();

            categoria.Estado =
                cmbEstado.Text == "Activo";

            bool resultado =
                categoriaDAO.EditarCategoria(categoria);

            if (resultado)
            {
                MessageBox.Show(
                    "Categoría modificada correctamente.",
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
                    "No se pudo modificar la categoría.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}