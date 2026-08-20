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
        int idMarcaSeleccionada = 0;
        int idProveedorSeleccionado = 0;
        int idCategoriaSeleccionada = 0;



        public EditarProducto()
        {
            InitializeComponent();

            txtCodigo.ReadOnly = true;
            CBnombreP.DropDownStyle = ComboBoxStyle.DropDown;
            CBTalla.DropDownStyle = ComboBoxStyle.DropDown;
        }


        private void EditarProducto_Load(object sender, EventArgs e)
        {
            // Método existente de ProductoDAO.
            DataTable productos = productoDAO.CargarProductos();

            CBnombreP.DataSource = productos;
            CBnombreP.DisplayMember = "nombre";
            CBnombreP.ValueMember = "id_producto";
            CBnombreP.SelectedIndex = -1;
            CBnombreP.Text = "";

            // Método existente de ProductoDAO.
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
            string nombreProducto = CBnombreP.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreProducto))
            {
                MessageBox.Show(
                    "Seleccione o escriba el nombre del producto.",
                    "Buscar producto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            //Buscar en la base de datos

            DataTable resultado =
               productoDAO.BuscarPorNombre(nombreProducto);


            if (resultado.Rows.Count == 0)
            {
                MessageBox.Show(
                    "El producto no fue encontrado.",
                    "Buscar producto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            DataRow producto = resultado.Rows[0];

            //Guardar ID del producto

            idProductoSeleccionado =
              Convert.ToInt32(producto["id_producto"]);

            txtCodigo.Text =
              producto["codigo"].ToString();

            CBnombreP.Text =
                producto["nombre"].ToString();

            txtMarca.Text =
                producto["marca"].ToString();

            txtProveedor.Text =
               producto["proveedor"].ToString();

            CBcategoria.Text =
               producto["categoria"].ToString();

            //Cargar tallas de los productos

            DataTable tallas = productoDAO.CargarTallasProducto(idProductoSeleccionado);

            CBTalla.DataSource = null;

            if(tallas.Rows.Count > 0)
            {
                CBTalla.DataSource = tallas;
                CBTalla.DisplayMember = "talla";
                CBTalla.ValueMember = "id_producto_talla";
                CBTalla.SelectedIndex = 0;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CBTalla_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Validar que se haya buscado un producto

            if (idProductoSeleccionado <= 0)
            {
                MessageBox.Show(
                    "Primero debe buscar un producto.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            //Validar nombre del producto 

            if (string.IsNullOrWhiteSpace(CBnombreP.Text))
            {
                MessageBox.Show(
                    "El nombre del producto no puede estar vacío.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            //Validar categoria

            if (CBcategoria.SelectedValue == null)
            {
                MessageBox.Show(
                    "Seleccione una categoría.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            //Obtener ID de categoria 

            int idCategoria =
            Convert.ToInt32(CBcategoria.SelectedValue);

            //Buscar marca

            DataTable marcas =
        productoDAO.CargarMarcas();

            DataRow marca = marcas.AsEnumerable()
                .FirstOrDefault(
                    x => x["nombre_marca"]
                        .ToString()
                        .Equals(
                            txtMarca.Text.Trim(),
                            StringComparison.OrdinalIgnoreCase));

            if (marca == null)
            {
                MessageBox.Show(
                    "La marca indicada no existe.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int idMarca =
                Convert.ToInt32(marca["id_marca"]);

            //Buscar proveedor 

            DataTable proveedores =
        productoDAO.CargarProveedores();

            DataRow proveedor = proveedores.AsEnumerable()
                .FirstOrDefault(
                    x => x["nombre"]
                        .ToString()
                        .Equals(
                            txtProveedor.Text.Trim(),
                            StringComparison.OrdinalIgnoreCase));

            if (proveedor == null)
            {
                MessageBox.Show(
                    "El proveedor indicado no existe.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int idProveedor =
                Convert.ToInt32(proveedor["id_proveedor"]);


        }
    }
}
