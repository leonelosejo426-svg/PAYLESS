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
    public partial class Login: Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnSesion_Click(object sender, EventArgs e)
        {
            try
            {
                // Lista de usuarios del sistema
                var usuarios = new List<(string Usuario, string Password, string Rol)>
                {
                    ("Leonel", "leonel123", "ADMIN"),
                    ("Kelly", "kelly123", "KELLY"),
                    ("Paola", "paola123", "PAOLA"),
                    ("Felipe", "felipe123", "FELIPE"),
                    ("Yubelkis", "yubelkis123", "YUBELKIS")
                };

                string usuarioIngresado = txtUsuario.Text.Trim();
                string contraseñaIngresada = txtPassword.Text.Trim();

                var usuarioValido = usuarios.FirstOrDefault(u =>
                    u.Usuario.Equals(usuarioIngresado, StringComparison.OrdinalIgnoreCase)
                    && u.Password == contraseñaIngresada);

                if (!string.IsNullOrEmpty(usuarioValido.Usuario))
                {
                    ClaseSesion.UsuarioActual = usuarioValido.Usuario;
                    ClaseSesion.RolActual = usuarioValido.Rol;

                    MessageBox.Show("Bienvenido " + usuarioValido.Usuario);

                    Menú_Principal menu = new Menú_Principal();
                    menu.Show();

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
    }
}
