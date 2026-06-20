
using System.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Interfaces_de_Usuario_Propuestas_Payless.ClaseProveedor;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Proveedores : Form
    {
        ClaseUsuario usuarioActual;


        List<Proveedor> listaProveedores = new List<Proveedor>();
        int indiceEditar = -1;



        public Proveedores()
        {
            InitializeComponent();
            
        }

        private void Proveedores_Load(object sender, EventArgs e)
        {

            dgvProveedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProveedores.MultiSelect = false;




            if (ClaseSesion.RolActual != "ADMIN" && ClaseSesion.RolActual != "KELLY")
            {
                MessageBox.Show("No tienes acceso");

                new Menú_Principal().Show(); // 🔥 lo regresa
                this.Hide();

                return;
            }

        }

        private void label15_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();

            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();

            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();

            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();

            this.Hide();
        }

        private void label20_Click_1(object sender, EventArgs e)
        {
            

            new Cliente().Show();
            this.Hide();
        }

        private void label17_Click_1(object sender, EventArgs e)
        {
            

            new Usuario().Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click_1(object sender, EventArgs e)
        {

            new Productos().Show();
            this.Hide();

        }

        private void label9_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show(); 
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();
            this.Hide();
        }

        private void label23_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo();
            ventana.Show();
            this.Hide();
        }

        private void label24_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();
        }

        private void label25_Click(object sender, EventArgs e)
        {
            inventario ventana = new inventario();
            ventana.Show(); this.Hide();
        }

        private void label27_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito();
            ventana.Show();
            this.Hide();
        }

        private void label28_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            

            Proveedor p = new Proveedor();

            p.RUC = txtRUC.Text;
            p.Nombre = txtNombre.Text;
            p.Telefono = txtTelefono.Text;
            p.Direccion = txtDireccion.Text;
            p.Estado = cmbEstado.Text;

            listaProveedores.Add(p);

            dgvProveedores.DataSource = null;
            dgvProveedores.DataSource = listaProveedores;

            Limpiar();

            if (indiceEditar >= 0)
            {
                listaProveedores[indiceEditar].RUC = txtRUC.Text;
                listaProveedores[indiceEditar].Nombre = txtNombre.Text;
                listaProveedores[indiceEditar].Telefono = txtTelefono.Text;
                listaProveedores[indiceEditar].Direccion = txtDireccion.Text;
                listaProveedores[indiceEditar].Estado = cmbEstado.Text;

                indiceEditar = -1;
            }
            else
            {
                listaProveedores.Add(p);
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
        
        {
            if (dgvProveedores.CurrentRow != null)
            {
                indiceEditar = dgvProveedores.CurrentRow.Index;

                txtRUC.Text = listaProveedores[indiceEditar].RUC;
                txtNombre.Text = listaProveedores[indiceEditar].Nombre;
                txtTelefono.Text = listaProveedores[indiceEditar].Telefono;
                txtDireccion.Text = listaProveedores[indiceEditar].Direccion;
                cmbEstado.Text = listaProveedores[indiceEditar].Estado;

                MessageBox.Show("Edita los datos y presiona Agregar para guardar cambios");
            }
        }
    }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            /*
        {
            if (dgvProveedores.CurrentRow != null)
            {
                int indice = dgvProveedores.CurrentRow.Index;

                DialogResult r = MessageBox.Show(
                    "¿Eliminar proveedor?",
                    "Confirmar",
                    MessageBoxButtons.YesNo
                );

                if (r == DialogResult.Yes)
                {
                    listaProveedores.RemoveAt(indice);

                    dgvProveedores.DataSource = null;
                    dgvProveedores.DataSource = listaProveedores;
                }
            }
        }*/
    }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            string json = JsonConvert.SerializeObject(listaProveedores, Formatting.Indented);
            File.WriteAllText("proveedores.json", json);

            MessageBox.Show("Datos guardados");
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
       
        
            if (File.Exists("proveedores.json"))
            {
                string json = File.ReadAllText("proveedores.json");

                listaProveedores = JsonConvert.DeserializeObject<List<Proveedor>>(json);

                dgvProveedores.DataSource = null;
                dgvProveedores.DataSource = listaProveedores;

                MessageBox.Show("Datos cargados");
            }
            else
            {
                MessageBox.Show("No existe el archivo");
            }
        }

        void Limpiar()
        {
            txtRUC.Clear();
            txtNombre.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            cmbEstado.SelectedIndex = -1;
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void label30_Click(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento();
            ventana.Show();
            this.Hide();
        }
    }
    
}


