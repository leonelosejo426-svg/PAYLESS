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
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();

            this.Hide();
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
            Proveedores ventana = new Proveedores();
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

        private void label15_Click(object sender, EventArgs e)
        {

        }
    }
}
