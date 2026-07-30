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

        List<Producto> catalogoProductos = new List<Producto>();
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
            public string Productos { get; set; }
            public string Categoria { get; set; }
            public string Medida { get; set; }
            public string Marca { get; set; }
            public string Proveedor { get; set; }
            public int Cantidad { get; set; }
         
        }


        

        private void MostrarProductos()
        {
            dgvProductos.Rows.Clear();

            foreach (Producto p in listaProductos)
            {
                dgvProductos.Rows.Add(
                    p.Codigo,
                    p.Productos,
                    p.Categoria,
                    p.Medida,
                    p.Marca,
                    p.Cantidad,
                     p.Proveedor
                );
            }
        }

        private void CargarComboProductos()
        {
            cmbProductos.DataSource = null;
            cmbProductos.DataSource = catalogoProductos;
            cmbProductos.DisplayMember = "Productos";
            cmbProductos.ValueMember = "Codigo";

            cmbProductos.SelectedIndex = -1;
        }

        private void Limpiar()
        {
            txtCodigo.Clear();
           // txtNombreProducto.Clear();
            txtCategoria.Clear();
            txtMedida.Clear();
            txtMarca.Clear();
            txtProveedor.Clear();
            txtCantidad.Clear();

            cmbProductos.SelectedIndex = -1;

            indiceEditar = -1;
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

            dgvProductos.AutoGenerateColumns = false;

            listaProductos.Clear();

            if (listaProductos.Count == 0)
            {
                catalogoProductos.Add(new Producto()
                {
                    Codigo = "P001",
                    Productos = "Zapato Deportivo",
                    Categoria = "Calzado",
                    Medida = "42",
                    Marca = "Nike",
                    Proveedor = "Distribuidora ABC"
                });

                catalogoProductos.Add(new Producto()
                {
                    Codigo = "P002",
                    Productos = "Sandalia",
                    Categoria = "Calzado",
                    Medida = "38",
                    Marca = "Adidas",
                    Proveedor = "Proveedor Central"
                });

                catalogoProductos.Add(new Producto()
                {
                    Codigo = "P003",
                    Productos = "Tacones",
                    Categoria = "Dama",
                    Medida = "37",
                    Marca = "Payless",
                    Proveedor = "Importadora Central"
                });

                catalogoProductos.Add(new Producto()
                {
                    Codigo = "P004",
                    Productos = "Botas",
                    Categoria = "Caballero",
                    Medida = "43",
                    Marca = "Puma",
                    Proveedor = "Proveedor Norte"
                });



                // MostrarProductos();

                cmbProductos.DataSource = null;
                cmbProductos.DataSource = catalogoProductos;
                cmbProductos.DisplayMember = "Productos";
                cmbProductos.ValueMember = "Codigo";

                cmbProductos.SelectedIndex = -1;
                dgvProductos.Rows.Clear();

                txtCodigo.Clear();
                txtCategoria.Clear();
                txtMedida.Clear();
                txtMarca.Clear();
                txtProveedor.Clear();
                txtCantidad.Clear();


                cmbBuscarPor.Items.Clear();
                cmbBuscarPor.Items.Add("Producto");
                cmbBuscarPor.Items.Add("Código");
                cmbBuscarPor.Items.Add("Marca");
            }
        }


            //this.Size = new Size(1250, 750);
        

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
            if (indiceEditar >= 0)
            {
                // ACTUALIZAR
                listaProductos[indiceEditar].Codigo = txtCodigo.Text;
               // listaProductos[indiceEditar].NombreProducto = txtNombreProducto.Text;
                listaProductos[indiceEditar].Categoria = txtCategoria.Text;
                listaProductos[indiceEditar].Medida = txtMedida.Text;
                listaProductos[indiceEditar].Marca = txtMarca.Text;
                listaProductos[indiceEditar].Proveedor = txtProveedor.Text;
                listaProductos[indiceEditar].Productos = cmbProductos.Text;
                listaProductos[indiceEditar].Cantidad = Convert.ToInt32(txtCantidad.Text);

                indiceEditar = -1;

                MessageBox.Show("Producto actualizado.");
            }
            else
            {
                // AGREGAR NUEVO
                Producto p = new Producto();

                p.Codigo = txtCodigo.Text;
                p.Productos = cmbProductos.Text;
                p.Categoria = txtCategoria.Text;
                p.Medida = txtMedida.Text;
                p.Marca = txtMarca.Text;
                p.Proveedor = txtProveedor.Text;
                p.Cantidad = Convert.ToInt32(txtCantidad.Text);

                bool existe = listaProductos.Any(x => x.Codigo == txtCodigo.Text);

                if (existe)
                {
                    MessageBox.Show("Este producto ya existe.");
                    return;
                }



                listaProductos.Add(p);

                MostrarProductos();

                MessageBox.Show("Producto agregado.");


            }

            MostrarProductos();
         
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
            dgvProductos.DataSource = null;

            if (cmbBuscarPor.Text == "Producto")
            {
                dgvProductos.DataSource = listaProductos
                    .OrderBy(x => x.Productos)
                    .ToList();
            }
            else if (cmbBuscarPor.Text == "Código")
            {
                dgvProductos.DataSource = listaProductos
                    .OrderBy(x => x.Codigo)
                    .ToList();
            }
            else if (cmbBuscarPor.Text == "Marca")
            {
                dgvProductos.DataSource = listaProductos
                    .OrderBy(x => x.Marca)
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
                //txtNombreProducto.Text = p.NombreProducto;
                txtCategoria.Text = p.Categoria;
                txtMedida.Text = p.Medida;
                txtMarca.Text = p.Marca;
                txtProveedor.Text = p.Proveedor;
                txtCantidad.Text = p.Cantidad.ToString();
                
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
                    CargarComboProductos();

                    btnGuardar.PerformClick();

                    MessageBox.Show("Producto eliminado.");
                }
            }
        }

        private void cmbProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedItem != null)
            {
                Producto p = (Producto)cmbProductos.SelectedItem;

                txtCodigo.Text = p.Codigo;
                txtCategoria.Text = p.Categoria;
                txtMedida.Text = p.Medida;
                txtMarca.Text = p.Marca;
                txtProveedor.Text = p.Proveedor;

                
                txtCantidad.Clear();
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
    }
}
