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
            txtNombre.MaxLength = 50;
            txtDescripcion.MaxLength =  100;
            // Cargar TODAS las marcas
            // activas e inactivas
            CargarMarcas();

            // Cargar estados
            cmbEstado.Items.Clear();

            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Inactivo");

            cmbEstado.SelectedIndex = -1;

            // El código no se modifica
            txtCodigo.ReadOnly = true;
        }

        private void CargarMarcas()
        {
            cmbMarca.DataSource =
              marcaDAO.CargarTodasLasMarcas();

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
                    "Seleccione una marca.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!int.TryParse(
                cmbMarca.SelectedValue?.ToString(),
                out int idMarca))
            {
                MessageBox.Show(
                    "La marca seleccionada no es válida.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            CargarMarca(idMarca);
        }
        private void CargarMarca(int idMarca)
        {
            ClaseMarca marca =
               marcaDAO.ObtenerMarca(idMarca);

            if (marca == null)
            {
                MessageBox.Show(
                    "No se encontró la marca.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            txtCodigo.Text =
                marca.IdMarca.ToString();

            txtNombre.Text =
                marca.NombreMarca;

            txtDescripcion.Text =
                marca.Descripcion;

            // Estado
            if (marca.Estado)
            {
                cmbEstado.SelectedItem = "Activo";
            }
            else
            {
                cmbEstado.SelectedItem = "Inactivo";
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            // 1. Verificar que se haya seleccionado y buscado una marca previamente
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show(
                    "Primero debe seleccionar y buscar una marca para poder editarla.",
                    "Aviso de Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int idMarcaActual = Convert.ToInt32(txtCodigo.Text);
            string nombre = txtNombre.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();

            // 2. Validar campo nombre vacío
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show(
                    "El campo 'Nombre de la marca' es obligatorio. Por favor, ingréselo.",
                    "Campo Requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNombre.Focus();
                return;
            }

            // 3. Validar longitudes máximas
            if (nombre.Length > 100)
            {
                MessageBox.Show(
                    "El nombre de la marca no puede superar los 100 caracteres.",
                    "Validación de Longitud",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtNombre.Focus();
                txtNombre.SelectAll();
                return;
            }

            if (descripcion.Length > 200)
            {
                MessageBox.Show(
                    "La descripción no puede superar los 200 caracteres.",
                    "Validación de Longitud",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtDescripcion.Focus();
                txtDescripcion.SelectAll();
                return;
            }

            // 4. Validar Estado seleccionado
            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Debe seleccionar el estado de la marca (Activo o Inactivo).",
                    "Campo Requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbEstado.Focus();
                return;
            }

            // =========================================================
            // 5. VALIDAR DUPLICADOS EN LA BASE DE DATOS (EXCLUYENDO ESTE ID)
            // =========================================================
            if (marcaDAO.ExisteNombre(nombre, idMarcaActual))
            {
                MessageBox.Show(
                    "Ya existe otra marca registrada con este mismo nombre. Ingrese uno diferente.",
                    "Registro Duplicado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNombre.Focus();
                txtNombre.SelectAll();
                return;
            }

            // =========================================================
            // 6. CREAR OBJETO Y ACTUALIZAR EN BASE DE DATOS
            // =========================================================
            ClaseMarca marca = new ClaseMarca();
            marca.IdMarca = idMarcaActual;
            marca.NombreMarca = nombre;
            marca.Descripcion = descripcion;
            marca.Estado = cmbEstado.SelectedItem.ToString() == "Activo";

            if (marcaDAO.EditarMarca(marca))
            {
                MessageBox.Show(
                    "La marca se ha actualizado correctamente.",
                    "Operación Exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo actualizar la marca en la base de datos. Verifique su conexión e intente nuevamente.",
                    "Error de Sistema",
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
