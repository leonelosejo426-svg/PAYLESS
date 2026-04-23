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
    public partial class Menú_Principal : Form
    {


        ClaseUsuario usuarioActual;






        public Menú_Principal(ClaseUsuario user)
        {
            InitializeComponent();
            usuarioActual = user;

        }




        private bool TieneAcceso(string pantalla)
        {
            switch (usuarioActual.RolUsuario)
            {
                case ClaseUsuario.Rol.Gerente:
                    return true; // accede a todo

                case ClaseUsuario.Rol.Cajero:
                    return pantalla == "Clientes" || pantalla ==  "Productos";

                case ClaseUsuario.Rol.Bodega:
                    return pantalla == "Productos" || pantalla == "Proveedores";

                case ClaseUsuario.Rol.Supervisor:
                    return pantalla == "Productos" || pantalla == "Proveedores" || pantalla == "Clientes";

                case ClaseUsuario.Rol.Contador:
                    return pantalla == "Clientes";

                default:
                    return false;
            }
        }






        private void FormMenu_Load(object sender, EventArgs e)
        {
           /* if (usuarioActual.RolUsuario == CuentaUsuario.Rol.Cajero)
            {
                lblProductos.Enabled = false;
                lblProveedores.Enabled = false;
                lblUsuario.Enabled = false;
            }

            if (usuarioActual.RolUsuario == CuentaUsuario.Rol.Bodega)
            {
                lblCliente.Enabled = false;
                lblUsuario.Enabled = false;
            }*/
        }




        private void label15_Click(object sender, EventArgs e)
        {
            if (!TieneAcceso("Productos"))
            {
                MessageBox.Show("No tienes acceso");
                return;
            }

            Productos ventana = new Productos();
            ventana.Show();

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
            if (!TieneAcceso("Proveedores"))
            {
                MessageBox.Show("No tienes acceso");
                return;
            }

            Proveedores ventana = new Proveedores();
            ventana.Show();

            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            if (!TieneAcceso("Usuarios"))
            {
                MessageBox.Show("No tienes acceso");
                return;
            }

            Usuario ventana = new Usuario();
            ventana.Show();

            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            if (!TieneAcceso("Clientes"))
            {
                MessageBox.Show("No tienes acceso");
                return;
            }

            Cliente ventana = new Cliente();
            ventana.Show();

            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Menú_Principal_Load(object sender, EventArgs e)
        {

        }
    }
}
