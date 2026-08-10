using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
using Interfaces_de_Usuario_Propuestas_Payless.Datos;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Productos : Form
    {
        ProductoDAO productoDAO = new ProductoDAO();

        ClaseUsuario usuarioActual;

        public Productos()
        {
            InitializeComponent();
        }




        private void MostrarProductos()
        {
            ProductoDAO productsDAO = new ProductoDAO();

            try
            {
                dgvProductos.DataSource = productsDAO.MostrarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los productos: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            if (ClaseSesion.RolActual != "ADMIN")
            {
                MessageBox.Show("No tienes acceso");
                this.Hide();
                return;
            }

            dgvProductos.AutoGenerateColumns = false;

            MostrarProductos();
        }

        

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {
            new Proveedores().Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            new Usuario().Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            new Cliente().Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos(); ventana.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal(); ventana.Show();
            this.Hide();
        }

        private void label25_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja(); ventana.Show();
            this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito(); ventana.Show(); 
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
         
        }

        private void label23_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas(); ventana.Show();
            this.Hide();
        }

        private void label24_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo(); ventana.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            inventario ventana = new inventario(); ventana.Show(); this.Hide();
        }

        private void label9_Click_1(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento(); ventana.Show(); this.Hide();
        }

        private void cmbBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            SubProductoAgregar formulario = new SubProductoAgregar();

            formulario.ShowDialog();

            MostrarProductos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
           /* string json = JsonConvert.SerializeObject(
        listaProductos,
        Formatting.Indented);

            File.WriteAllText("productos.json", json);

          MessageBox.Show("Productos guardados correctamente."); */
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarProducto ventana = new EditarProducto();
            ventana.Show();
            this.Hide();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ProductoDAO productsDAO = new ProductoDAO();

            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione un producto.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            int idProducto = Convert.ToInt32(
                dgvProductos.CurrentRow.Cells["id_producto"].Value);

            DialogResult respuesta = MessageBox.Show(
                "¿Desea eliminar este producto?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            bool eliminado = productsDAO.EliminarProducto(idProducto);

            if (eliminado)
            {
                MessageBox.Show(
                    "Producto eliminado correctamente.",
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                MostrarProductos();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo eliminar el producto.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Reporte_Productos ventana = new Reporte_Productos();
            ventana.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Categoria ventana = new Categoria();
            ventana.Show();
            this.Hide();
        }

        private void btnMarca_Click(object sender, EventArgs e)
        {
          //  Marca ventana = new Marca();
          //  ventana.Show();
            //this.Hide();
        }
    }
}
