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
    public partial class Proveedores : Form
    {
        ClaseUsuario usuarioActual;
        public Proveedores()
        {
            InitializeComponent();
            
        }

        private void Proveedores_Load(object sender, EventArgs e)
        {
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
    }
}
