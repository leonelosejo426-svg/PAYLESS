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
        public EditarProducto()
        {
            InitializeComponent();

           
        }

        //Cargar Formulario

        private void EditarProducto_Load(object sender, EventArgs e)
        {
            DataTable productos = productoDAO.MostrarProductos();

            CBnombreP.DataSource = productos;
            CBnombreP.DisplayMember = "nombre";
            CBnombreP.ValueMember = "id_producto";

            CBnombreP.DropDownStyle = ComboBoxStyle.DropDown;
            CBnombreP.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            CBnombreP.AutoCompleteSource = AutoCompleteSource.ListItems;

            CBnombreP.SelectedIndex = -1;
            CBnombreP.Text = "";

            // Cargar Proveedores en ComboBox
            DataTable tablaProveedores = productoDAO.CargarProveedores();
            cmbProveedor.DataSource = tablaProveedores;
            cmbProveedor.DisplayMember = "nombre";         
            cmbProveedor.ValueMember = "id_proveedor";    
            cmbProveedor.SelectedIndex = -1;

            // Cargar Marcas en ComboBox
            DataTable tablaMarcas = productoDAO.CargarMarcas();
            cmbMarca.DataSource = tablaMarcas;
            cmbMarca.DisplayMember = "nombre_marca"; // Ajusta el nombre si en tu BD es distinto
            cmbMarca.ValueMember = "id_marca";
            cmbMarca.SelectedIndex = -1;

            // Cargar Categorías en ComboBox
            DataTable categorias = productoDAO.CargarCategorias();
            CBcategoria.DataSource = categorias;
            CBcategoria.DisplayMember = "nombre_categoria";
            CBcategoria.ValueMember = "id_categoria";
            CBcategoria.SelectedIndex = -1;
            CBcategoria.Text = "";

            txtCodigo.ReadOnly = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //Buscar producto

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombreProducto = CBnombreP.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreProducto))
            {
                MessageBox.Show(
                    "Seleccione o escriba un producto.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                CBnombreP.Focus();
                return;
            }

            DataTable resultado = productoDAO.BuscarPorNombre(nombreProducto);

            if (resultado == null || resultado.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No se encontró el producto con el nombre indicado.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            DataRow producto = resultado.Rows[0];

            // ID
            idProductoSeleccionado = Convert.ToInt32(producto["id_producto"]);

            // Nombre
            CBnombreP.Text = producto["nombre"]?.ToString() ?? "";

            // Asignar Categoría por ID si existe
            if (producto.Table.Columns.Contains("id_categoria") && producto["id_categoria"] != DBNull.Value)
            {
                CBcategoria.SelectedValue = Convert.ToInt32(producto["id_categoria"]);
            }

            // Asignar Marca por ID si existe
            if (producto.Table.Columns.Contains("id_marca") && producto["id_marca"] != DBNull.Value)
            {
                cmbMarca.SelectedValue = Convert.ToInt32(producto["id_marca"]);
            }

            // Asignar Proveedor por ID si existe
            if (producto.Table.Columns.Contains("id_proveedor") && producto["id_proveedor"] != DBNull.Value)
            {
                cmbProveedor.SelectedValue = Convert.ToInt32(producto["id_proveedor"]);
            }

            // Código / ID Producto
            if (producto.Table.Columns.Contains("codigo"))
                txtCodigo.Text = producto["codigo"]?.ToString() ?? "";
            else
                txtCodigo.Text = idProductoSeleccionado.ToString();
        }




        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (idProductoSeleccionado <= 0)
            {
                MessageBox.Show(
                    "Primero debe buscar un producto antes de intentar guardar.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Validar Nombre del Producto
            string nombre = CBnombreP.Text.Trim();

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

            // Validar Categoría
            if (CBcategoria.SelectedIndex == -1 || CBcategoria.SelectedValue == null)
            {
                MessageBox.Show(
                    "Debe seleccionar una categoría de la lista.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                CBcategoria.Focus();
                return;
            }

            int idCategoria = Convert.ToInt32(CBcategoria.SelectedValue);

            // Validar Marca
            if (cmbMarca.SelectedIndex == -1 || cmbMarca.SelectedValue == null)
            {
                MessageBox.Show("Seleccione una marca.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbMarca.Focus();
                return;
            }

            int idMarca = Convert.ToInt32(cmbMarca.SelectedValue);

            // Validar Proveedor
            if (cmbProveedor.SelectedIndex == -1 || cmbProveedor.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProveedor.Focus();
                return;
            }

            int idProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);

            // Construir el objeto Producto
            ClaseProducto producto = new ClaseProducto
            {
                IdProducto = idProductoSeleccionado,
                Nombre = nombre,
                IdCategoria = idCategoria,
                IdMarca = idMarca,
                IdProveedor = idProveedor
            };

            // Guardar cambios en la BD mediante el DAO
            bool actualizado = productoDAO.EditarProducto(producto);

            if (actualizado)
            {
                MessageBox.Show(
                    "Producto actualizado correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo actualizar el producto.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //Limpiar
        private void LimpiarFormulario()
        {
            idProductoSeleccionado = 0;

            CBnombreP.SelectedIndex = -1;
            CBnombreP.Text = "";
            CBcategoria.SelectedIndex = -1;
            CBcategoria.Text = "";

            cmbMarca.SelectedIndex = -1;
            cmbMarca.Text = "";
            cmbProveedor.SelectedIndex = -1;
            cmbProveedor.Text = "";
            txtCodigo.Clear();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        
        private void CBTalla_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();

            this.Close();

        }
    }
}
