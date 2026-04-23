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
    public partial class Usuario : Form
    {
        ClaseUsuario usuarioActual;

        public Usuario()
        {
            InitializeComponent();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();

            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();

            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();

            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal(usuarioActual);
            ventana.Show();

            this.Hide();
        }
    }
}
