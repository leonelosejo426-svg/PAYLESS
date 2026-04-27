using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Interfaces_de_Usuario_Propuestas_Payless.ClaseUsuario;
using static System.Collections.Specialized.BitVector32;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Login : Form
    {
        public Login()  
        {
            InitializeComponent();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        
                string usuario = txtUsuario.Text;
                string contraseña = txtContraseña.Text;

            switch (usuario)
            {
                case "Leonel":
                    if (contraseña == "leonel123")
                    {
                        ClaseSesion.UsuarioActual = "Leonel";
                        ClaseSesion.RolActual = "ADMIN";
                    }
                    else { MessageBox.Show("Contraseña incorrecta"); return; }
                    break;

                case "Kelly":
                    if (contraseña == "kelly123")
                    {
                        ClaseSesion.UsuarioActual = "Kelly";
                        ClaseSesion.RolActual = "KELLY";
                    }
                    else { MessageBox.Show("Contraseña incorrecta"); return; }
                    break;

                case "Paola":
                    if (contraseña == "paola123")
                    {
                        ClaseSesion.UsuarioActual = "Paola";
                        ClaseSesion.RolActual = "PAOLA";
                    }
                    else { MessageBox.Show("Contraseña incorrecta"); return; }
                    break;

                case "Felipe":
                    if (contraseña == "felipe123")
                    {
                        ClaseSesion.UsuarioActual = "Felipe";
                        ClaseSesion.RolActual = "FELIPE";
                    }
                    else { MessageBox.Show("Contraseña incorrecta"); return; }
                    break;

                case "Yubelkis":
                    if (contraseña == "yubelkis123")
                    {
                        ClaseSesion.UsuarioActual = "Yubelkis";
                        ClaseSesion.RolActual = "YUBELKIS";
                    }
                    else { MessageBox.Show("Contraseña incorrecta"); return; }
                    break;

                default:
                    MessageBox.Show("Usuario no existe");
                    return;
            }

            MessageBox.Show("Bienvenido " + ClaseSesion.UsuarioActual + "!");
            this.Hide();
            new Menú_Principal().Show();

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
