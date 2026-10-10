using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static Interfaces_de_Usuario_Propuestas_Payless;
//using static Interfaces_de_Usuario_Propuestas_Payless.Ventas;


namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class SubProductoAgregar : Form
    {
        ProductoDAO DAO = new ProductoDAO();
        public SubProductoAgregar()
        {
            InitializeComponent();
        }

        private void SubProductoAgregar_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarMarcas();
            CargarProveedores();
           

        }
        private void CargarCategorias()
        {
            cmbCategoria.DataSource = DAO.CargarCategorias();
            cmbCategoria.DisplayMember = "nombre_categoria";
            cmbCategoria.ValueMember = "id_categoria";
            cmbCategoria.SelectedIndex = -1;
        }
        private void CargarMarcas()
        {
            cmbMarca.DataSource = DAO.CargarMarcas();
            cmbMarca.DisplayMember = "nombre_marca";
            cmbMarca.ValueMember = "id_marca";
            cmbMarca.SelectedIndex = -1;
        }
        private void CargarProveedores()
        {
            cmbProveedor.DataSource = DAO.CargarProveedores();
            cmbProveedor.DisplayMember = "nombre";
            cmbProveedor.ValueMember = "id_proveedor";
            cmbProveedor.SelectedIndex = -1;
        }
       
        private bool ValidarCampos()
        {
            // Validar nombre
            if (string.IsNullOrWhiteSpace(txtNombredelProducto.Text))
            {
                MessageBox.Show(
                    "Ingrese el nombre del producto",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNombredelProducto.Focus();
                return false;
            }

          

            // Validar categoría
            if (cmbCategoria.SelectedIndex == -1 ||
                cmbCategoria.SelectedValue == null)
            {
                MessageBox.Show(
                    "Seleccione una categoría",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbCategoria.Focus();
                return false;
            }

            // Validar marca
            if (cmbMarca.SelectedIndex == -1 ||
                cmbMarca.SelectedValue == null)
            {
                MessageBox.Show(
                    "Seleccione una marca",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbMarca.Focus();
                return false;
            }

            // Validar proveedor
            if (cmbProveedor.SelectedIndex == -1 ||
                cmbProveedor.SelectedValue == null)
            {
                MessageBox.Show(
                    "Seleccione un proveedor",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbProveedor.Focus();
                return false;
            }



            // Todos los campos son válidos
            return true;
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // 1. Validar que todos los campos requeridos estén llenos correctamente
            if (!ValidarCampos())
            {
                return;
            }

            try
            {
                // 2. Crear y poblar el objeto con los datos seleccionados en el formulario
                ClaseProducto nuevoProducto = new ClaseProducto()
                {
                    Nombre = txtNombredelProducto.Text.Trim(),
                    IdCategoria = Convert.ToInt32(cmbCategoria.SelectedValue),
                    IdMarca = Convert.ToInt32(cmbMarca.SelectedValue),
                    IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue)
                };

                // 3. Ejecutar la inserción mediante la capa de datos (DAO)
                bool resultado = DAO.AgregarProducto(nuevoProducto);

                if (resultado)
                {
                    MessageBox.Show(
                        "El producto se ha registrado exitosamente.",
                        "Registro exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Limpiar formulario tras guardar con éxito
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al intentar guardar el producto:\n\n" + ex.Message,
                    "Error de aplicación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }
        private void LimpiarCampos()

        { txtNombredelProducto.Clear();
            

            cmbCategoria.SelectedIndex = -1;
            cmbMarca.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
           
            txtNombredelProducto.Focus();

        }
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        
    }
}

   
