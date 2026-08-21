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

            DataTable productos = productoDAO.CargarProductos();

            CBnombreP.DataSource = productos;
            CBnombreP.DisplayMember = "nombre";
            CBnombreP.ValueMember = "id_producto";

            CBnombreP.SelectedIndex = -1;

            //Cargar categorias
            DataTable categorias = productoDAO.CargarCategorias();
            CBcategoria.DataSource = categorias;
            CBcategoria.DisplayMember = "nombre_categoria";
            CBcategoria.ValueMember = "id_categoria";
            CBcategoria.SelectedIndex = -1;

            //Limpiar tallas
            CBTalla.DataSource = null;
            CBTalla.Items.Clear();
            CBTalla.Text = "";

            idProductoSeleccionado = 0;
            idProductoTallaSeleccionado = 0;
        }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombre = CBnombreP.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Seleccione o escriba el nombre del producto", "Buscar producto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            //Buscar producto

            DataTable resultado = productoDAO.BuscarPorNombre(nombre);

            if (resultado.Rows.Count == 0)
            {
                MessageBox.Show("El producto no fue encontrado", "Buscar producto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }
            DataRow fila = resultado.Rows[0];

            //Guardar id del producto
            idProductoSeleccionado = Convert.ToInt32(fila["id_producto"]);

            //Cargar datos del producto

            txtCodigo.Text = fila["codigo"].ToString();
            CBnombreP.Text = fila["nombre"].ToString();
            txtMarca.Text = fila["marca"].ToString();
            txtProveedor.Text = fila["proveedor"].ToString();

            //categoria
            string categoria = fila["categoria"].ToString();
            CBcategoria.Text = categoria;

            //Cargar tallas
            DataTable tallas = productoDAO.CargarTallasProducto(idProductoSeleccionado);
            CBTalla.DataSource = null;
            
            if(tallas.Rows.Count > 0)
            {
                CBTalla.DataSource = tallas;

                CBTalla.DisplayMember = "talla";
                CBTalla.ValueMember = "id_producto_talla";
                CBTalla.SelectedIndex = 0;
            }
            //Obtener datos 
            idProductoTallaSeleccionado = Convert.ToInt32(CBTalla.SelectedValue);

            DataTable datosTalla = productoDAO.ObtenerProductoTalla(idProductoSeleccionado,
                idProductoTallaSeleccionado);

            if(datosTalla.Rows.Count > 0)
            {
                DataRow filaTalla = datosTalla.Rows[0];

                txtCantidad.Text = filaTalla["stock_actual"].ToString();
            }
            else
            {
                CBTalla.DataSource = null;
                CBTalla.Items.Clear();
                CBTalla.Text = "";
                txtCantidad.Clear();
                idProductoTallaSeleccionado = 0;
            }
        }
        
        

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CBTalla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBTalla.SelectedValue == null)
                return;
            if (CBTalla.SelectedValue is DataRowView)
                return;
            if(!int.TryParse(
                CBTalla.SelectedValue.ToString(),
                out idProductoTallaSeleccionado))
            {
                return;
            }

            //Obtener datos de la talla
            DataTable datosTalla = productoDAO.ObtenerProductoTalla(
                idProductoSeleccionado,
                idProductoTallaSeleccionado);
            if (datosTalla.Rows.Count == 0)
                return;
            DataRow fila = datosTalla.Rows[0];

            //Cargar cantidad correspondiente a la talla
            txtCantidad.Text = fila["stock_actual"].ToString();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (idProductoSeleccionado <= 0)
            {
                MessageBox.Show("Primero debe de buscar un producto",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            //Validar nombre
            string nombre = CBnombreP.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre del producto no puede estar vacio",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            //Validar cantidad
            
            if (!int.TryParse(txtCantidad.Text.Trim(), out int cantidad))
            {
                MessageBox.Show("La cantidad debe ser un número entero",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtCantidad.Focus();
                return;
            }
            if(cantidad < 0)
            {
                MessageBox.Show("La cantidad no puede ser negativa",
                    "Cantidad invalida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtCantidad.Focus();
                return;
            }
            //Validar talla
            if (CBTalla.SelectedIndex == -1 || CBTalla.SelectedValue == null)
            {
                MessageBox.Show("Debe de seleccioar una talla",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                CBTalla.Focus();
                return;
            }
            //Obtener datos de las tallas
            DataTable datosTalla = productoDAO.ObtenerProductoTalla(
                idProductoSeleccionado,
                idProductoTallaSeleccionado);
            if (datosTalla.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron los datos de la talla",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            DataRow filaTalla = datosTalla.Rows[0];

            int stocMinimo = Convert.ToInt32(filaTalla["stock_minimo"]);

            //obtener la talla seleccionada 
            string talla = CBTalla.Text.Trim();

            //Obtener marca
            DataTable marcas = productoDAO.CargarMarcas();
            DataRow filaMarca = marcas.AsEnumerable().FirstOrDefault(
                x =>
                x["nombre_marca"]
                .ToString()
                .Equals(txtMarca.Text.Trim(), StringComparison.OrdinalIgnoreCase));

            if(filaMarca == null)
            {
                MessageBox.Show("La marca indicada no existe",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            int idMarca = Convert.ToInt32(filaMarca["id_marca"]);

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

            //Obtener categoria
            int idCategoria =
               Convert.ToInt32(
                   CBcategoria.SelectedValue);

            //Guardar
            ClaseProducto producto = new ClaseProducto();

            producto.IdProducto = idProductoSeleccionado;
            producto.Nombre = CBnombreP.Text.Trim();
            producto.IdCategoria = idCategoria;
            producto.IdMarca = idMarca;
            producto.IdProveedor = idProveedor;

            bool resultado = productoDAO.EditarProducto(
                producto,
                idProductoTallaSeleccionado,
                CBTalla.Text.Trim(),
                cantidad,
                stocMinimo);

            //Limpiar campos
            idProductoSeleccionado = 0;

            idProductoTallaSeleccionado = 0;

            txtCodigo.Clear();

            CBnombreP.DataSource = null;
            CBnombreP.Items.Clear();
            CBnombreP.Text = "";

            txtMarca.Clear();

            txtProveedor.Clear();

            CBcategoria.SelectedIndex = -1;
            CBcategoria.Text = "";

            CBTalla.DataSource = null;
            CBTalla.Items.Clear();
            CBTalla.Text = "";

            txtCantidad.Clear();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Limpiar campos

            idProductoSeleccionado = 0;

            idProductoTallaSeleccionado = 0;

            txtCodigo.Clear();

            CBnombreP.DataSource = null;
            CBnombreP.Items.Clear();
            CBnombreP.Text = "";

            txtMarca.Clear();

            txtProveedor.Clear();

            CBcategoria.SelectedIndex = -1;
            CBcategoria.Text = "";

            CBTalla.DataSource = null;
            CBTalla.Items.Clear();
            CBTalla.Text = "";

            txtCantidad.Clear();

            this.Close();

        }
    }
}
