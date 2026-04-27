using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Menú_Principal : Form
    {

        public Menú_Principal()
        {
            InitializeComponent();
        }
        private void FormMenu_Load(object sender, EventArgs e)
        {
        }
        private void label15_Click(object sender, EventArgs e)
        {

            if (ClaseSesion.RolActual != "ADMIN")
            {
                MessageBox.Show("No tienes acceso");
                return;
            }

            new Productos().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cerrar sesión?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Login ventana = new Login();
                ventana.Show();

                this.Hide();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de Salir de la aplicacion de escritorio?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();

                this.Hide();
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {


            if (ClaseSesion.RolActual != "ADMIN" && ClaseSesion.RolActual != "KELLY")
            {
                MessageBox.Show("No tienes acceso");
                return;
            }

            new Proveedores().Show();
            this.Hide();

        }

        private void label17_Click(object sender, EventArgs e)
        {

            if (ClaseSesion.RolActual != "ADMIN" && ClaseSesion.RolActual != "FELIPE")
            {
                MessageBox.Show("No tienes acceso");
                return;
            }

            new Usuario().Show();
            this.Hide();
        }
        

        private void label20_Click(object sender, EventArgs e)
        {


            if (ClaseSesion.RolActual != "ADMIN" && ClaseSesion.RolActual != "YUBELKIS")
            {
                MessageBox.Show("No tienes acceso");
                return;
            }

            new Cliente().Show();
            this.Hide();

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Menú_Principal_Load(object sender, EventArgs e)
        {
            lblSesion.Text =  ClaseSesion.UsuarioActual;

            // Deshabilitar todo primero
            lblCaja.Enabled = false;
            lblProveedores.Enabled = false;
            lblProductos.Enabled = false;
            lblVenta.Enabled = false;
            //lblCompras.Enabled = false;
            lblUsuarios.Enabled = false;
            lblCliente.Enabled = false;
            //lblCredito.Enabled = false;
            //lblDevoluciones.Enabled = false;

            // Activar según usuario
            switch (ClaseSesion.RolActual)
            {
                case "ADMIN":
                    lblCaja.Enabled = true;
                    lblProveedores.Enabled = true;
                    lblProductos.Enabled = true;
                    lblVenta.Enabled = true;
                    //lblCompras.Enabled = true;
                    lblUsuarios.Enabled = true;
                    lblCliente.Enabled = true;
                    //lblCredito.Enabled = true;
                    //lblDevoluciones.Enabled = true;
                    break;

                case "KELLY":
                    lblCaja.Enabled = true;
                    lblProveedores.Enabled = true;
                    break;

                case "PAOLA":
                    //lblDevoluciones.Enabled = true;
                    break;

                case "FELIPE":
                    //lblCompras.Enabled = true;
                    lblUsuarios.Enabled = true;
                    break;

                case "YUBELKIS":
                    lblCliente.Enabled = true;
                    //lblCredito.Enabled = true;
                    break;
            }
        }

        private void lblCaja_Click(object sender, EventArgs e)
        {
            if (ClaseSesion.RolActual != "ADMIN" && ClaseSesion.RolActual != "YUBELKIS")
            {
                MessageBox.Show("No tienes acceso");
                return;
            }

            new Caja().Show();
            this.Hide();
        }

        private void lblVenta_Click(object sender, EventArgs e)
        {
            if (ClaseSesion.RolActual != "ADMIN" && ClaseSesion.RolActual != "YUBELKIS")
            {
                MessageBox.Show("No tienes acceso");
                return;
            }

            new Ventas().Show();
            this.Hide();
        }
    
    }
    
}
