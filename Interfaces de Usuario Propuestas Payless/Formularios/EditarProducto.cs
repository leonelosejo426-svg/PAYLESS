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

            // No seleccionar ningún producto al abrir
            CBnombreP.SelectedIndex = -1;
            CBnombreP.Text = "";

            DataTable categorias = productoDAO.CargarCategorias();

            CBcategoria.DataSource = categorias;
            CBcategoria.DisplayMember = "nombre_categoria";
            CBcategoria.ValueMember = "id_categoria";

            // No seleccionar ninguna categoría al abrir
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

            // Asignar Categoría de forma segura por ID si existe o por Nombre
            if (producto.Table.Columns.Contains("id_categoria") && producto["id_categoria"] != DBNull.Value)
            {
                CBcategoria.SelectedValue = Convert.ToInt32(producto["id_categoria"]);
            }
            else
            {
                // Fallback por nombre de columna (soporta tanto "categoria" como "nombre_categoria")
                string nombreColumnaCat = producto.Table.Columns.Contains("categoria") ? "categoria" : "nombre_categoria";

                if (producto.Table.Columns.Contains(nombreColumnaCat) && producto[nombreColumnaCat] != DBNull.Value)
                {
                    string categoriaBuscada = producto[nombreColumnaCat].ToString().Trim();

                    for (int i = 0; i < CBcategoria.Items.Count; i++)
                    {
                        if (CBcategoria.Items[i] is DataRowView fila)
                        {
                            string nomCat = fila["nombre_categoria"].ToString().Trim();
                            if (nomCat.Equals(categoriaBuscada, StringComparison.OrdinalIgnoreCase))
                            {
                                CBcategoria.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
            }

            // Marca
            if (producto.Table.Columns.Contains("marca"))
                txtMarca.Text = producto["marca"]?.ToString() ?? "";
            else if (producto.Table.Columns.Contains("nombre_marca"))
                txtMarca.Text = producto["nombre_marca"]?.ToString() ?? "";

            // Proveedor
            if (producto.Table.Columns.Contains("proveedor"))
                txtProveedor.Text = producto["proveedor"]?.ToString() ?? "";
            else if (producto.Table.Columns.Contains("nombre_proveedor"))
                txtProveedor.Text = producto["nombre_proveedor"]?.ToString() ?? "";

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

            // 2. Validar Nombre del Producto
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

            // 3. Validar Selección de Categoría
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

            if (!int.TryParse(CBcategoria.SelectedValue.ToString(), out int idCategoria))
            {
                MessageBox.Show(
                    "La categoría seleccionada no es válida.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // 4. Validar y Obtener ID de la Marca escrita
            string nombreMarca = txtMarca.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreMarca))
            {
                MessageBox.Show(
                    "Debe ingresar el nombre de la marca.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtMarca.Focus();
                return;
            }

            DataTable marcas = productoDAO.CargarMarcas();
            DataRow filaMarca = marcas.AsEnumerable()
                .FirstOrDefault(x => x["nombre_marca"].ToString().Trim().Equals(nombreMarca, StringComparison.OrdinalIgnoreCase));

            if (filaMarca == null)
            {
                MessageBox.Show(
                    "La marca indicada no existe en el sistema.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtMarca.Focus();
                return;
            }

            int idMarca = Convert.ToInt32(filaMarca["id_marca"]);

            // 5. Validar y Obtener ID del Proveedor escrito
            string nombreProveedor = txtProveedor.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreProveedor))
            {
                MessageBox.Show(
                    "Debe ingresar el nombre del proveedor.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtProveedor.Focus();
                return;
            }

            DataTable proveedores = productoDAO.CargarProveedores();
            DataRow filaProveedor = proveedores.AsEnumerable()
                .FirstOrDefault(x => x["nombre"].ToString().Trim().Equals(nombreProveedor, StringComparison.OrdinalIgnoreCase));

            if (filaProveedor == null)
            {
                MessageBox.Show(
                    "El proveedor indicado no existe en el sistema.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtProveedor.Focus();
                return;
            }

            int idProveedor = Convert.ToInt32(filaProveedor["id_proveedor"]);

            // 6. Construir el objeto Producto
            ClaseProducto producto = new ClaseProducto
            {
                IdProducto = idProductoSeleccionado,
                Nombre = nombre,
                IdCategoria = idCategoria,
                IdMarca = idMarca,
                IdProveedor = idProveedor
            };

            // 7. Guardar cambios en la BD mediante el DAO
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
        }

        //Limpiar
        private void LimpiarFormulario()
        {
            idProductoSeleccionado = 0;

            CBnombreP.SelectedIndex = -1;
            CBnombreP.Text = "";
            CBcategoria.SelectedIndex = -1;
            CBcategoria.Text = "";

            txtMarca.Clear();
            txtProveedor.Clear();
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
