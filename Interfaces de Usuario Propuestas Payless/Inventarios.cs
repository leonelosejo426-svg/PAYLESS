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
    public partial class Inventarios : Form
    {
        public Inventarios()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {

            Compras ventana = new Compras();
            ventana.Show();
            this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {

            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {

            inventario ventana = new inventario();
            ventana.Show();
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento();
            ventana.Show();
            this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {

            Credito ventana = new Credito();
            ventana.Show();
            this.Hide();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Nivelacion_inventario ventana = new Nivelacion_inventario();
            ventana.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
