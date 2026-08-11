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
            CargarTallas();

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
        private void CargarTallas()
        {
            cmbTalla.Items.Clear();

            for (int i = 30; i <= 45; i++)
            {
                cmbTalla.Items.Add(i.ToString());
            }
            cmbTalla.SelectedIndex = -1;
        }
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombredelProducto.Text))
            {
                MessageBox.Show("Ingrese el nombre del producto");
                MessageBox.Show("Ingrese el nombre del producto", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtNombredelProducto.Focus();
                return false;
            }

            //Validar cantidad
            if (string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Ingrese la cantidad del Producto", "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtCantidad.Focus();
                return false;
            }


            if (!int.TryParse(txtCantidad.Text.Trim(), out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad valida mayor que cero.", "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtCantidad.Focus();
                return false;
            }

            //Validar categoria
            if (cmbCategoria.SelectedIndex == -1 || cmbCategoria.SelectedValue == null)
            {
                MessageBox.Show("Seleccione categoria", "Validacion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbCategoria.Focus();
                return false;
            }

            //Validar marca
            if (cmbMarca.SelectedIndex == -1 || cmbMarca.SelectedValue == null)
            {
                MessageBox.Show("Seleccione una Marca", "Validacion",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                cmbMarca.Focus();
                return false;
            }

            //Validar proveedor
            if (cmbProveedor.SelectedIndex == -1 || cmbProveedor.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un Proveedot", "validavio",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

                cmbProveedor.Focus();
                return false;
            }

            // Validar talla
            if (cmbTalla.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cmbTalla.Text))
            {
                MessageBox.Show("Seleccione una talla", "Validacion",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProveedor.Focus();
                return false;
            }
            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            try
            {


                ClaseProducto ProductoDAO = new ClaseProducto();

                //Nombre del producto
                ProductoDAO.Nombre = txtNombredelProducto.Text.Trim();

                // Estado actico
                ProductoDAO.EstadoProducto = true;

                // Validar categoría

                if (!int.TryParse(cmbCategoria.SelectedValue?.ToString(), out int idCategoria))

                {
                    MessageBox.Show("La categoría seleccionada no es válida.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                // Validar marca

                if (!int.TryParse(cmbMarca.SelectedValue?.ToString(), out int idMarca))

                {
                    MessageBox.Show("La marca seleccionada no es válida.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);


                    return;
                }

                //Validar proveedpr

                {
                    if (!int.TryParse(cmbProveedor.SelectedValue?.ToString(), out int idProveedor))
                    {
                        MessageBox.Show("El proveedor seleccionado no es válido.", "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                        return;
                    }

                    ProductoDAO.IdCategoria = idCategoria;
                    ProductoDAO.IdMarca = idMarca;
                    ProductoDAO.IdProveedor = idProveedor;

                    //Talla
                    string talla = cmbTalla.Text.Trim();

                    // Cantidad
                    if (!int.TryParse(txtCantidad.Text.Trim(), out int cantidad) || cantidad <= 0)

                    {
                        MessageBox.Show("La cantidad ingresada no es válida.", "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        txtCantidad.Focus();
                        return;
                    }

                    // Stock mínimo establecido para todos los productos
                    int stockMinimo = 5;

                    // Guardar producto
                    bool resultado = DAO.AgregarProducto(ProductoDAO, talla, cantidad, stockMinimo);

                    if (resultado)
                    {
                        MessageBox.Show("Producto guardaddo correctamente", "Exito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo guardar el producto. Verifique los datos e intente nuevamente.", "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al guardar el producto:\n\n" + ex.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }
        private void LimpiarCampos()

        { txtNombredelProducto.Clear();
            txtCantidad.Clear();

            cmbCategoria.SelectedIndex = -1;
            cmbMarca.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
            cmbTalla.SelectedIndex = -1;
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

   
