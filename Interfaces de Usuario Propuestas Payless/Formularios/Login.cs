using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Interfaces_de_Usuario_Propuestas_Payless.Datos;
//using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
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
using System.IO;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Login: Form
    {

        private ConexionBD conexionBD = new ConexionBD();
        public ReporteUsuarioDAO reporteUsuarioDAO = new ReporteUsuarioDAO();
        public Login()
        {
            InitializeComponent();
        }

        private void btnSesion_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contraseña = txtContraseña.Text;

            if (string.IsNullOrEmpty(usuario))
            {
                MessageBox.Show("Ingrese el usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Ingrese la contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContraseña.Focus();
                return;
            }

            try
            {
                // 1. Guardamos el historial de log en tu clase de reportes
                ReporteUsuarioDAO reporteDAO = new ReporteUsuarioDAO();
                reporteDAO.IniciarSesion(usuario, contraseña);

                // 2. Ejecutamos la sesión operativa en tu UsuarioDAO
                UsuarioDAO usuarioDAO = new UsuarioDAO();

                if (usuarioDAO.IniciarSesion(usuario, contraseña))
                {
                    MessageBox.Show("Bienvenido " + ClaseSesion.RolActual + " " + ClaseSesion.UsuarioActual, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 3. CONTROL DE ROLES: Solo el cajero está amarrado al estado de la caja
                    if (ClaseSesion.RolActual.Trim().ToLower() == "cajero")
                    {
                        if (ClaseSesion.TieneCajaActiva)
                        {
                            // Si ya se había aperturado previamente, entra directo a vender
                            MessageBox.Show("Detectamos una caja activa. Abriendo módulo de ventas...", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Caja ventanaVenta = new Caja();
                            ventanaVenta.FormClosed += (s, args) => this.Close();
                            ventanaVenta.Show();
                        }
                        else
                        {
                            // Si no tiene caja abierta hoy, lo enviamos a aperturar pasándole el ID y Nombre
                            MessageBox.Show("Debe realizar la apertura de caja para iniciar operaciones.", "Apertura Obligatoria", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            AperturaCaja ventanaApertura = new AperturaCaja(ClaseSesion.IdUsuario, ClaseSesion.UsuarioActual);
                            ventanaApertura.FormClosed += (s, args) => this.Close();
                            ventanaApertura.Show();
                        }
                    }
                    else
                    {
                        // CUALQUIER OTRO ROL (Admin, Supervisor, etc.) entra directo al sistema normal
                        Menú_Principal ventana = new Menú_Principal();
                        ventana.FormClosed += (s, args) => this.Close();
                        ventana.Show();
                    }

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContraseña.Clear();
                    txtContraseña.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }






            /* switch (usuario)
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
                     if (contraseña == "keling123")
                     {
                         ClaseSesion.UsuarioActual = "Keling";
                         ClaseSesion.RolActual = "KELING";
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
             new Menú_Principal().Show();*/
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

        private void btnProbarConexion_Click(object sender, EventArgs e)
        {
           
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label38_Click(object sender, EventArgs e)
        {
            try
            {
                string rutaPDF = @"C:\Users\Lenovo\Desktop\PAYLESS\Interfaces de Usuario Propuestas Payless\Ayuda\Manual_Usuario.pdf";

                if (!File.Exists(rutaPDF))
                {
                    MessageBox.Show($"No se encontró el manual en la ruta:\n{rutaPDF}",
                                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Convertir la ruta del disco a formato URI web (maneja espacios y caracteres especiales)
                string uriPDF = new Uri(rutaPDF).AbsoluteUri;

                // 2. Cambia el número 4 por el número exacto de la página de Caja
                int numeroPaginaCaja = 4;

                // 3. Crear el argumento en formato file:///C:/...#page=2
                string argumentos = $"\"{uriPDF}#page={numeroPaginaCaja}\"";

                ProcessStartInfo edgeInfo = new ProcessStartInfo
                {
                    FileName = "msedge.exe",
                    Arguments = argumentos,
                    UseShellExecute = true
                };

                Process.Start(edgeInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el manual: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
