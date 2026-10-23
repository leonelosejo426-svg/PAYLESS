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
            txtNombre.MaxLength = 50;
            txtDescripcion.MaxLength = 100;
            CargarCategorias();

            cmbEstado.Items.Clear();

            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Inactivo");

            cmbEstado.SelectedIndex = 0;

            txtCodigo.ReadOnly = true;
        }

        private void CargarCategorias()
        {
            cmbCategoria.DataSource =
                categoriaDAO.CargarCategorias();

            cmbCategoria.DisplayMember = "nombre_categoria";
            cmbCategoria.ValueMember = "id_categoria";

            cmbCategoria.SelectedIndex = -1;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una categoría.");
                return;
            }

            int idCategoria =
                Convert.ToInt32(cmbCategoria.SelectedValue);

            CargarCategoria(idCategoria);
        }

        private void CargarCategoria(int idCategoria)
        {
            ClaseCategoria categoria =
                categoriaDAO.ObtenerCategoria(idCategoria);

            if (categoria == null)
            {
                MessageBox.Show("No se encontró la categoría.");
                return;
            }

            txtCodigo.Text =
                categoria.IdCategoria.ToString();

            txtNombre.Text =
                categoria.NombreCategoria;

            txtDescripcion.Text =
                categoria.Descripcion;

            cmbEstado.SelectedItem =
                categoria.Estado ? "Activo" : "Inactivo";
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            // 1. Verificar que se haya seleccionado y buscado una categoría
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show(
                    "Primero busque una categoría para poder editarla.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int idCategoriaActual = Convert.ToInt32(txtCodigo.Text);
            string nombre = txtNombre.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();

            // 2. Validar nombre vacío
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese el nombre de la categoría.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            // 3. Validar longitud del nombre
            if (nombre.Length > 100)
            {
                MessageBox.Show(
                    "El campo [nombre] no puede superar los 100 caracteres.",
                    "Validación de Longitud",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtNombre.Focus();
                return;
            }

            // 4. Validar descripción vacía
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                MessageBox.Show("Ingrese la descripción de la categoría.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return;
            }

            // 5. Validar longitud de la descripción
            if (descripcion.Length > 200)
            {
                MessageBox.Show(
                    "El campo [descripción] no puede superar los 200 caracteres.",
                    "Validación de Longitud",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtDescripcion.Focus();
                return;
            }

            // 6. Validar estado seleccionado
            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el estado de la categoría.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEstado.Focus();
                return;
            }

            // =========================================================
            // 7. VALIDAR DUPLICADOS EN LA BASE DE DATOS (EXCLUYENDO ESTE ID)
            // =========================================================
            if (categoriaDAO.ExisteNombre(nombre, idCategoriaActual))
            {
                MessageBox.Show("Ya existe otra categoría registrada con este mismo nombre.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                txtNombre.SelectAll();
                return;
            }

            // =========================================================
            // 8. ACTUALIZAR EN LA BASE DE DATOS
            // =========================================================
            ClaseCategoria categoria = new ClaseCategoria();
            categoria.IdCategoria = idCategoriaActual;
            categoria.NombreCategoria = nombre;
            categoria.Descripcion = descripcion;
            categoria.Estado = cmbEstado.SelectedItem.ToString() == "Activo";

            if (categoriaDAO.EditarCategoria(categoria))
            {
                MessageBox.Show(
                    "Categoría actualizada correctamente.",
                    "Correcto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo actualizar la categoría.",
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
