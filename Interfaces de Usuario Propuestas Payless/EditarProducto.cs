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

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class EditarProducto : Form
    {
        ProductoDAO productoDAO = new ProductoDAO();

        int idProductoSeleccionado = 0;
        int idProductoTallaSeleccionado = 0;

        public EditarProducto()
        {
            InitializeComponent();

           
        }


        private void EditarProducto_Load(object sender, EventArgs e)
        {
            //Cargar productos

            CargarProductos();
            CargarCategorias();

            //Limpiar tallas 
            CBTalla.DataSource = null;
            CBTalla.Items.Clear();

            idProductoSeleccionado = 0;
            idProductoTallaSeleccionado = 0;
        }

        //Cargar productos
        private void CargarProductos()
        {
            DataTable productos = productoDAO.MostrarProductos();

            CBnombreP.DataSource = productos;
            CBnombreP.DisplayMember = "nombre";
            CBnombreP.ValueMember = "id_producto";
            CBnombreP.SelectedIndex = -1;
        }

        //Cargar categorias
        private void CargarCategorias()
        {
            DataTable categorias = productoDAO.CargarCategorias();

            CBcategoria.DataSource = categorias;
            CBcategoria.DisplayMember = "nombre_categoria";
            CBcategoria.ValueMember = "id_categoria";
            CBcategoria.SelectedIndex = -1;
        }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (CBnombreP.SelectedIndex == -1 ||
                CBnombreP.SelectedValue == null)
            {
                MessageBox.Show(
                    "Seleccione un producto.",
                    "Buscar producto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!int.TryParse(
                CBnombreP.SelectedValue.ToString(),
                out int idProducto))
            {
                MessageBox.Show(
                    "No se pudo identificar el producto.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            // Guardar ID
            idProductoSeleccionado = idProducto;

            //obtener productos
            ClaseProducto producto =
                productoDAO.ObtenerProducto(
                    idProductoSeleccionado);

            if (producto == null ||
                producto.IdProducto <= 0)
            {
                MessageBox.Show(
                    "No se encontró el producto.",
                    "Buscar producto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }
            //Mostrar datos del producto
            CBnombreP.SelectedValue =
                producto.IdProducto;

            // Categoría
            CBcategoria.SelectedValue =
                producto.IdCategoria;

            //Buscar marca
            DataTable marcas =
                productoDAO.CargarMarcas();

            DataRow filaMarca =
                marcas.AsEnumerable()
                .FirstOrDefault(
                    x =>
                    Convert.ToInt32(
                        x["id_marca"]) ==
                    producto.IdMarca);

            if (filaMarca != null)
            {
                txtMarca.Text =
                    filaMarca["nombre_marca"]
                    .ToString();
            }
            else
            {
                txtMarca.Clear();
            }
            //Buscar proveedor 
            DataTable proveedores =
                productoDAO.CargarProveedores();

            DataRow filaProveedor =
                proveedores.AsEnumerable()
                .FirstOrDefault(
                    x =>
                    Convert.ToInt32(
                        x["id_proveedor"]) ==
                    producto.IdProveedor);

            if (filaProveedor != null)
            {
                txtProveedor.Text =
                    filaProveedor["nombre"]
                    .ToString();
            }
            else
            {
                txtProveedor.Clear();
            }
            //Cargar tallas
            CargarTallas();

        }
        //Cargar tallas del prodcuto
        private void CargarTallas()
        {
            CBTalla.DataSource = null;
            CBTalla.Items.Clear();
            CBTalla.Text = "";

            idProductoTallaSeleccionado = 0;

            if (idProductoSeleccionado <= 0)
                return;

            DataTable tallas =
                productoDAO.CargarTallasProducto(
                    idProductoSeleccionado);

            if (tallas.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Este producto no tiene tallas registradas.",
                    "Tallas",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }
            CBTalla.DataSource = tallas;

            CBTalla.DisplayMember = "talla";
            CBTalla.ValueMember =
                "id_producto_talla";

            CBTalla.SelectedIndex = 0;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Validar producto
            if (idProductoSeleccionado <= 0)
            {
                MessageBox.Show(
                    "Primero debe buscar un producto.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
            //Validar nombre
            string nombre =
               CBnombreP.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show(
                    "El nombre del producto no puede estar vacío.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                CBnombreP.Focus();
                return;
            }
            //Validar categoria
            if (CBcategoria.SelectedIndex == -1 ||
               CBcategoria.SelectedValue == null)
            {
                MessageBox.Show(
                    "Debe seleccionar una categoría.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                CBcategoria.Focus();
                return;
            }

            if (!int.TryParse(
                CBcategoria.SelectedValue.ToString(),
                out int idCategoria))
            {
                MessageBox.Show(
                    "La categoría seleccionada no es válida.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
            //Validar talla
            if (CBTalla.SelectedIndex == -1 ||
               CBTalla.SelectedValue == null)
            {
                MessageBox.Show(
                    "Debe seleccionar una talla.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                CBTalla.Focus();
                return;
            }
            //Validar cantidad
            if (!int.TryParse(
              txtCantidad.Text.Trim(),
              out int cantidad))
            {
                MessageBox.Show(
                    "La cantidad debe ser un número entero.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCantidad.Focus();
                return;
            }

            if (cantidad < 0)
            {
                MessageBox.Show(
                    "La cantidad no puede ser negativa.",
                    "Cantidad inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCantidad.Focus();
                return;
            }
            //Obtener stock minimo actual
            DataTable datosTalla =
               productoDAO.ObtenerProductoTalla(
                   idProductoSeleccionado,
                   idProductoTallaSeleccionado);

            if (datosTalla.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No se encontraron los datos de la talla.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DataRow filaTalla =
                datosTalla.Rows[0];

            int stockMinimo =
                Convert.ToInt32(
                    filaTalla["stock_minimo"]);

            //Obtener marca
            DataTable marcas =
               productoDAO.CargarMarcas();

            DataRow filaMarca =
                marcas.AsEnumerable()
                .FirstOrDefault(
                    x =>
                    x["nombre_marca"]
                    .ToString()
                    .Equals(
                        txtMarca.Text.Trim(),
                        StringComparison.OrdinalIgnoreCase));

            if (filaMarca == null)
            {
                MessageBox.Show(
                    "La marca indicada no existe.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int idMarca =
                Convert.ToInt32(
                    filaMarca["id_marca"]);

            //Obtener proveedor
            DataTable proveedores =
               productoDAO.CargarProveedores();

            DataRow filaProveedor =
                proveedores.AsEnumerable()
                .FirstOrDefault(
                    x =>
                    x["nombre"]
                    .ToString()
                    .Equals(
                        txtProveedor.Text.Trim(),
                        StringComparison.OrdinalIgnoreCase));

            if (filaProveedor == null)
            {
                MessageBox.Show(
                    "El proveedor indicado no existe.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int idProveedor =
                Convert.ToInt32(
                    filaProveedor["id_proveedor"]);

            //Crear objeto producto
            ClaseProducto producto =
                new ClaseProducto();

            producto.IdProducto =
                idProductoSeleccionado;

            producto.Nombre =
                nombre;

            producto.IdCategoria =
                idCategoria;

            producto.IdMarca =
                idMarca;

            producto.IdProveedor =
                idProveedor;

            //Guardar
            string talla =
                CBTalla.Text.Trim();

            bool resultado =
                productoDAO.EditarProducto(
                    producto,
                    idProductoTallaSeleccionado,
                    talla,
                    cantidad,
                    stockMinimo);

            if (resultado)
            {
                MessageBox.Show(
                    "Producto actualizado correctamente.",
                    "Correcto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LimpiarFormulario();


            }
        }

        //Limpiar
        private void LimpiarFormulario()
        {
            idProductoSeleccionado = 0;
            idProductoTallaSeleccionado = 0;

            CBnombreP.SelectedIndex = -1;
            CBcategoria.SelectedIndex = -1;

            txtMarca.Clear();
            txtProveedor.Clear();
            txtCantidad.Clear();

            CBTalla.DataSource = null;
            CBTalla.Items.Clear();
            CBTalla.Text = "";
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        //Cambio de talla
        private void CBTalla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBTalla.SelectedIndex == -1)
                return;

            if (CBTalla.SelectedValue == null)
                return;

            if (CBTalla.SelectedValue is DataRowView)
                return;

            if (!int.TryParse(
                CBTalla.SelectedValue.ToString(),
                out int idProductoTalla))
            {
                return;
            }
            idProductoTallaSeleccionado = idProductoTalla;

            //Obtener inventario de la talla
            DataTable datosTalla =
              productoDAO.ObtenerProductoTalla(
                  idProductoSeleccionado,
                  idProductoTallaSeleccionado);

            if (datosTalla.Rows.Count == 0)
            {
                txtCantidad.Clear();
                return;
            }

            DataRow fila =
                datosTalla.Rows[0];

            txtCantidad.Text =
                fila["stock_actual"]
                .ToString();
        }

        

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();

            this.Close();

        }
    }
}
