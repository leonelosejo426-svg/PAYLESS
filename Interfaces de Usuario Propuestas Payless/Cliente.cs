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
    public partial class Cliente : Form
    {
        ClaseUsuario usuarioActual;


        public Cliente()
        {
            InitializeComponent();


        }


        private void panel2_Paint(object sender, PaintEventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();

            this.Hide();
        }

        private void Cliente_Load(object sender, EventArgs e)
        {
            if (ClaseSesion.RolActual != "ADMIN" && ClaseSesion.RolActual != "YUBELKIS")
            {
                MessageBox.Show("No tienes acceso");

                new Menú_Principal().Show(); 
                this.Hide();

                return;
            }

        }

        private void label15_Click(object sender, EventArgs e)
        {
            new Productos().Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void label14_Click_1(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Hide();
        }

        private void label15_Click_1(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();
            this.Hide();
        }

        private void label17_Click_1(object sender, EventArgs e)
        {
            Compras ventana = new Compras();
            ventana.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Devoluciones ventana = new Devoluciones();
            ventana.Show();
            this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {
           Credito ventana = new Credito(); 
           ventana.Show();
           this.Hide();
        }

        private void label20_Click_1(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            inventario ventana = new inventario();
            ventana.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento();
            ventana.Show();
            this.Hide();
        }

        private void CBbusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNombre.Visible = false;
            txtcedula.Visible = false;
            txtcodigo.Visible = false;

            txtNombre.Clear();
            txtcedula.Clear();
            txtcodigo.Clear();

            switch (CBbusqueda.Text)
            {
                case "Nombre":
                    txtNombre.Visible = true;
                    LblNombre.Visible = true;
                    txtNombre.Focus();
                    break;

                case "Numero de Cédula":
                    txtcedula.Visible = true;
                    LblNumero.Visible = true;
                    txtcedula.Focus();
                    break;

                case "Código":
                    txtcodigo.Visible = true;
                    Lblcodigo.Visible = true;
                    txtcodigo.Focus();
                    break;
            }
                
        }

        private void CBestado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CBestado.Items.Add("Activo");
            CBestado.Items.Add("Inactivo");
            CBestado.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
