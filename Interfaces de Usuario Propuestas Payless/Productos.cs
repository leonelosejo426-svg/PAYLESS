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
        ClaseUsuario usuarioActual;
        public Productos()
        {
            InitializeComponent();
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
    }
}
