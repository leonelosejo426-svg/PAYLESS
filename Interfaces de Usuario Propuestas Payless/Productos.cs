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

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Productos : Form
    {

        List<Producto> listaProductos = new List<Producto>();

        int indiceEditar = -1;


        ClaseUsuario usuarioActual;
        public Productos()
        {
            InitializeComponent();
        }

        public class Producto
{
    public string Codigo { get; set; }

    public string NombreProducto { get; set; }

    public string Categoria { get; set; }

    public string Medida { get; set; }

    public string Marca { get; set; }

    public string Proveedor { get; set; }

    public DateTime FechaRegistro { get; set; }
}


        private void GuardarJSON()
        {
            string json = JsonConvert.SerializeObject(
                listaProductos,
                Formatting.Indented);

            File.WriteAllText("productos.json", json);
        }

        private void MostrarProductos()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = listaProductos;
        }
        private void Limpiar()
        {
            txtCodigo.Clear();
            txtNombreProducto.Clear();

            cmbCategoria.SelectedIndex = -1;
            cmbMedida.SelectedIndex = -1;
            cmbMarca.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Productos_Load(object sender, EventArgs e)
        {
            
            
            if (ClaseSesion.RolActual != "ADMIN")
            {
                MessageBox.Show("No tienes acceso");
                this.Hide();
            }



            if (File.Exists("productos.json"))
            {
                string json = File.ReadAllText("productos.json");

                listaProductos = JsonConvert.DeserializeObject<List<Producto>>(json);

                MostrarProductos();
            }

            cmbBuscarPor.Items.Add("Producto");
            cmbBuscarPor.Items.Add("Fecha más reciente");
            cmbBuscarPor.Items.Add("Fecha más antigua");


            this.Size = new Size(1250, 700);
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
            Compras ventana = new Compras(); ventana.Show();
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
            cmbBuscarPor.Items.Add("Producto");
            cmbBuscarPor.Items.Add("Fecha más reciente");
            cmbBuscarPor.Items.Add("Fecha más antigua");
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (indiceEditar >= 0)
            {
                // ACTUALIZAR
                listaProductos[indiceEditar].Codigo = txtCodigo.Text;
                listaProductos[indiceEditar].NombreProducto = txtNombreProducto.Text;
                listaProductos[indiceEditar].Categoria = cmbCategoria.Text;
                listaProductos[indiceEditar].Medida = cmbMedida.Text;
                listaProductos[indiceEditar].Marca = cmbMarca.Text;
                listaProductos[indiceEditar].Proveedor = cmbProveedor.Text;

                indiceEditar = -1;

                MessageBox.Show("Producto actualizado.");
            }
            else
            {
                // AGREGAR NUEVO
                Producto p = new Producto();

                p.Codigo = txtCodigo.Text;
                p.NombreProducto = txtNombreProducto.Text;
                p.Categoria = cmbCategoria.Text;
                p.Medida = cmbMedida.Text;
                p.Marca = cmbMarca.Text;
                p.Proveedor = cmbProveedor.Text;
                p.FechaRegistro = DateTime.Now;

                listaProductos.Add(p);

                MessageBox.Show("Producto agregado.");
            }

            MostrarProductos();
            GuardarJSON();
            Limpiar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string json = JsonConvert.SerializeObject(
        listaProductos,
        Formatting.Indented);

            File.WriteAllText("productos.json", json);

            MessageBox.Show("Productos guardados correctamente.");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbBuscarPor.Text == "Producto")
            {
                dgvProductos.DataSource = null;

                dgvProductos.DataSource =
                    listaProductos
                    .OrderBy(x => x.NombreProducto)
                    .ToList();
            }
            if (cmbBuscarPor.Text == "Fecha más reciente")
            {
                dgvProductos.DataSource = null;

                dgvProductos.DataSource =
                    listaProductos
                    .OrderByDescending(x => x.FechaRegistro)
                    .ToList();
            }
            if (cmbBuscarPor.Text == "Fecha más antigua")
            {
                dgvProductos.DataSource = null;

                dgvProductos.DataSource =
                    listaProductos
                    .OrderBy(x => x.FechaRegistro)
                    .ToList();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow != null)
            {
                indiceEditar = dgvProductos.CurrentRow.Index;

                Producto p = listaProductos[indiceEditar];

                txtCodigo.Text = p.Codigo;
                txtNombreProducto.Text = p.NombreProducto;
                cmbCategoria.Text = p.Categoria;
                cmbMedida.Text = p.Medida;
                cmbMarca.Text = p.Marca;
                cmbProveedor.Text = p.Proveedor;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow != null)
            {
                int indice = dgvProductos.CurrentRow.Index;

                DialogResult respuesta = MessageBox.Show(
                    "¿Desea eliminar este producto?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    listaProductos.RemoveAt(indice);

                    MostrarProductos();

                    btnGuardar.PerformClick();

                    MessageBox.Show("Producto eliminado.");
                }
            }
        }
    }
}
