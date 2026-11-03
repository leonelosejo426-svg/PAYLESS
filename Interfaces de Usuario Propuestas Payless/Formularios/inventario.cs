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
    public partial class inventario : Form
    {
        public inventario()
        {
            InitializeComponent();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();
        }

        private void label24_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void label27_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Hide();
        }

        private void label29_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void label31_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();
            this.Hide();
        }

        private void label30_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo();
            ventana.Show();
            this.Hide();
        }

        private void label28_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();
        }

        private void label25_Click(object sender, EventArgs e)
        {
            Devoluciones_cs ventana = new Devoluciones_cs();
            ventana.Show();
            this.Hide();
        }

        private void label23_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito();
            ventana.Show();
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            inventario ventana = new inventario();
            ventana.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Nivelacion_inventario ventana = new Nivelacion_inventario();
            ventana.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Nivelacion_inventario ventana = new Nivelacion_inventario();
            ventana.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Reportes ventana = new Reportes();
            ventana.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void ConfigurarMenuRoles()
        {
            string rol = ClaseSesion.RolActual;

            bool esAdmin = (rol == "Administrador");
            bool esGerente = (rol == "Gerente");
            bool esCajero = (rol == "Cajero");

            // Módulos que cambian según el rol (estos suben/bajan sin afectar a Ayuda)
            lblMenu.Visible = esAdmin || esGerente || esCajero;
            lblVenta.Visible = esAdmin || esGerente || esCajero;
            lblCaja.Visible = esAdmin || esGerente || esCajero;
            lblReportes.Visible = esAdmin || esGerente || esCajero;
            lblCredito.Visible = esAdmin || esGerente;
            lblInventario.Visible = esAdmin || esGerente;
            lblCompras.Visible = esAdmin || esGerente;
            lblCliente.Visible = esAdmin || esGerente || esCajero;
            lblMantenimiento.Visible = esAdmin || esGerente;

            lblProductos.Visible = esAdmin;
            lblProveedores.Visible = esAdmin;
            lblUsuarios.Visible = esAdmin;


            // Ayuda se mantiene siempre visible y fija en la parte inferior gracias al Anchor = Bottom
            lblAyuda.Visible = true;
        }

        private void inventario_Load(object sender, EventArgs e)
        {
            ConfigurarMenuRoles();


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
                    lblMantenimiento.Enabled = true;
                    lblCliente.Enabled = true;
                    lblCredito.Enabled = true;
                    lblInventario.Enabled = true;
                    lblReportes.Enabled = true;


                    break;

                case "Cajero":

                    lblCaja.Enabled = true;
                    lblVenta.Enabled = true;
                    lblCliente.Enabled = true;
                    lblReportes.Enabled = true;

                    break;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void label38_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click_1(object sender, EventArgs e)
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
            Usuario ventana = new Usuario ();
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
            Proveedores ventana = new  Proveedores ();
            ventana.Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo();
            ventana.Show();
            this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();
        }

        private void label22_Click_1(object sender, EventArgs e)
        {
            inventario ventana = new inventario();
            ventana.Show();
            this.Hide();
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

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void lblReportes_Click(object sender, EventArgs e)
        {
            Reportes ventana = new Reportes();
            ventana.Show();
            this.Hide();
        }

        private void lblMenu_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();
        }

        private void lblCaja_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja(); ventana.Show(); this.Hide();
        }

        private void lblUsuarios_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario(); ventana.Show(); this.Hide();
        }

        private void lblCliente_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente(); ventana.Show(); this.Hide();
        }

        private void lblProveedores_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores(); ventana.Show(); this.Hide();
        }

        private void lblProductos_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos(); ventana.Show(); this.Hide();
        }

        private void lblCompras_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo(); ventana.Show(); this.Hide();
        }

        private void lblVenta_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas(); ventana.Show(); this.Hide();
        }

        private void lblCredito_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito(); ventana.Show(); this.Hide();
        }

        private void lblInventario_Click(object sender, EventArgs e)
        {
            inventario ventana = new inventario(); ventana.Show(); this.Hide();
        }

        private void lblMantenimiento_Click(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento(); ventana.Show(); this.Hide();
        }

        private void lblReportes_Click_1(object sender, EventArgs e)
        {
            Reportes ventana = new Reportes(); ventana.Show(); this.Hide();
        }
    }
}
