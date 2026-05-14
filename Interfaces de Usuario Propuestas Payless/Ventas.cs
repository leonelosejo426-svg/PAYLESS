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
    public partial class Ventas : Form
    {
        public Ventas()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void label26_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
                ventana.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
                ventana.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Clientes ventana = new Clientes();
                ventana.Show(); 
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Usuarios ventana = new Usuarios();
                ventana.Show(); this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            Compras ventana = new Compras();
                venta.Show(); this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
                veana.Show(); this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Devoluciones ventana = new Devoluciones();
                ventana.Show(); this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito();
                ventana.Show(); this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show(); this.Hide();
        }
    
