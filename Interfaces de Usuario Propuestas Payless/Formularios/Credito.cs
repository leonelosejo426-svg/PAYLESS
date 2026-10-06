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
    public partial class Credito : Form
    {
        public Credito()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Credito_Load(object sender, EventArgs e)
        {
            lblCaja.Enabled = false;
            lblProveedores.Enabled = false;
            lblProductos.Enabled = false;
            lblVenta.Enabled = false;
            lblCompras.Enabled = false;
            lblUsuarios.Enabled = false;


            lblCliente.Enabled = false;
            lblVenta.Enabled = false;
            lblInventario.Enabled = false;
            lblMantenimiento.Enabled = false;
            lblReportes.Enabled = false;


            switch (ClaseSesion.RolActual)
            {
                case "Administrador":

                    lblCaja.Enabled = true;
                    lblCompras.Enabled = true;
                    lblVenta.Enabled = true;
                    lblUsuarios.Enabled = true;
                    lblMantenimiento.Enabled = true;
                    lblCliente.Enabled = true;
                    lblVenta.Enabled = true;
                    lblInventario.Enabled = true;
                    lblProveedores.Enabled = true;
                    lblProductos.Enabled = true;
                    lblReportes.Enabled = true;


                    break;

                case "Gerente":

                    lblCaja.Enabled = true;
                    lblCompras.Enabled = true;
                    lblVenta.Enabled = true;

                    break;

                case "Cajero":

                    lblCaja.Enabled = true;
                    lblVenta.Enabled = true;

                    break;
            }

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito();
            ventana.Show();
            this.Hide();
        }

        private void label23_Click(object sender, EventArgs e)
        {
            Devoluciones_cs ventana = new Devoluciones_cs();
            ventana.Show();
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo();
            ventana.Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            inventario ventana = new inventario();
            ventana.Show();
            this.Hide();
        }

        private void label26_Click(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento();
            ventana.Show();
            this.Hide();
        }
    }
}
