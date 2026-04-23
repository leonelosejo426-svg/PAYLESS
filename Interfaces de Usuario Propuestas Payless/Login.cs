using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Interfaces_de_Usuario_Propuestas_Payless.ClaseUsuario;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Login : Form
    {
        List<ClaseUsuario> usuarios = new List<ClaseUsuario>()
        {
            new ClaseUsuario("gerente", "Admin123!", ClaseUsuario.Rol.Gerente),
            new ClaseUsuario("cajero", "Caja123!", ClaseUsuario.Rol.Cajero),
            new ClaseUsuario("bodega", "Bodega123!", ClaseUsuario.Rol.Bodega),
            new ClaseUsuario("supervisor", "Super123!", ClaseUsuario.Rol.Supervisor),
            new ClaseUsuario("contador", "Conta123!", ClaseUsuario.Rol.Contador)
        };


        public Login()  
        {
            InitializeComponent();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var usuario = usuarios.FirstOrDefault(u =>
                u.Usuario == txtUsuario.Text &&
                u.Contraseña == txtContraseña.Text
                );

                if (usuario == null)
                {
                    MessageBox.Show("Usuario o contraseña incorrectos");
                    return;
                }

                MessageBox.Show($"Bienvenido {txtUsuario.Text} al Sistema de la Empresa Payless");

                Menú_Principal ventana = new Menú_Principal(usuario);
                ventana.Show();

                this.Hide();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Recuperacion_de_Cuenta ventana = new Recuperacion_de_Cuenta();
            ventana.Show();

            this.Hide();
        }

        private void Login_Load(object sender, EventArgs e)
        {
           
        }
    }
}
