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
    public partial class SubMarcaAgregar : Form
    {
        MarcaDAO marcaDAO = new MarcaDAO();
        public SubMarcaAgregar()
        {
            InitializeComponent();
        }

        private void SubMarcaAgregar_Load(object sender, EventArgs e)
        {
            txtNombre.MaxLength = 50;
            txtDescripcion.MaxLength = 100;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();

            // 1. Validar nombre vacío
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show(
                    "El campo 'Nombre de la marca' es obligatorio.",
                    "Campo Requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNombre.Focus();
                return;
            }

            // 2. Validar longitudes
            if (nombre.Length > 100)
            {
                MessageBox.Show(
                    "El nombre de la marca no puede superar los 100 caracteres.",
                    "Validación de Longitud",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtNombre.Focus();
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
                return;
            }

            // 3. Validar Duplicados (Sin excluir ID, ya que es nuevo)
            if (marcaDAO.ExisteNombre(nombre))
            {
                MessageBox.Show(
                    "Ya existe una marca registrada con este mismo nombre.",
                    "Registro Duplicado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNombre.Focus();
                txtNombre.SelectAll();
                return;
            }

            // 4. Guardar
            ClaseMarca marca = new ClaseMarca();
            marca.NombreMarca = nombre;
            marca.Descripcion = descripcion;
            marca.Estado = true; // Activo por defecto al crear

            if (marcaDAO.AgregarMarca(marca))
            {
                MessageBox.Show(
                    "La marca ha sido agregada correctamente.",
                    "Operación Exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LimpiarCampos();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo agregar la marca a la base de datos.",
                    "Error de Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();

         

            txtNombre.Focus();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
