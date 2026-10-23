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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();

            // 1. Validar nombre vacío
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese el nombre de la categoría.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            // 2. Validar longitud del nombre
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

            // 3. Validar descripción vacía
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                MessageBox.Show("Ingrese la descripción de la categoría.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return;
            }

            // 4. Validar longitud de la descripción
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

            // 5. Validar que se haya seleccionado un estado
            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el estado de la categoría.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEstado.Focus();
                return;
            }

            // =========================================================
            // 6. VALIDAR DUPLICADOS EN LA BASE DE DATOS
            // =========================================================
            if (categoriaDAO.ExisteNombre(nombre))
            {
                MessageBox.Show("Ya existe una categoría registrada con este mismo nombre.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                txtNombre.SelectAll();
                return;
            }

            // =========================================================
            // 7. GUARDAR EN LA BASE DE DATOS
            // =========================================================
            ClaseCategoria categoria = new ClaseCategoria();
            categoria.NombreCategoria = nombre;
            categoria.Descripcion = descripcion;
            categoria.Estado = cmbEstado.SelectedItem.ToString() == "Activo";

            if (categoriaDAO.AgregarCategoria(categoria))
            {
                MessageBox.Show(
                    "Categoría agregada correctamente.",
                    "Correcto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LimpiarCampos();
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

        private void SubCategoriaAgregar_Load(object sender, EventArgs e)
        {
            txtNombre.MaxLength = 50;
            txtDescripcion.MaxLength = 100;
            cmbEstado.Items.Clear();

            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Inactivo");

            cmbEstado.SelectedIndex = 0;
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();

            cmbEstado.SelectedIndex = 0;

            txtNombre.Focus();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
