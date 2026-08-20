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

            txtCodigo.ReadOnly = true;
        }


        private void EditarProducto_Load(object sender, EventArgs e)
        {
            DataTable productos = productoDAO.CargarProductos();

            CBnombreP.DataSource = productos;
            CBnombreP.DisplayMember = "nombre";
            CBnombreP.ValueMember = "id_producto";
            CBnombreP.SelectedIndex = -1;
            CBnombreP.Text = "";
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
                    "Seleccione o escriba un producto.",
                    "Buscar producto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Se llama al método EXISTENTE del ProductoDAO
            DataTable resultado =
                productoDAO.BuscarPorNombre(nombreProducto);

            if (resultado.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No se encontró el producto.",
                    "Buscar producto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            DataRow producto = resultado.Rows[0];

            // Guardar el ID para utilizarlo posteriormente al guardar
            idProductoSeleccionado =
                Convert.ToInt32(producto["id_producto"]);

           
            // LLENAR LOS CONTROLES DEL FORMULARIO
           

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

            txtCantidad.Text =
                producto["stock_actual"].ToString();

            // Talla registrada para ese producto
            CBTalla.Text =
                producto["talla"].ToString();

            // ID de la relación producto-talla
            idProductoTallaSeleccionado =
                Convert.ToInt32(producto["id_producto_talla"]);

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CBTalla_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }
    }
}
