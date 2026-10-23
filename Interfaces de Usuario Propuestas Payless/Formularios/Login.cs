using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using Npgsql;
//using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Login: Form
    {
        private bool passwordVisible = false;
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

            bool usuarioVacio = string.IsNullOrWhiteSpace(usuario);
            bool contrasenaVacia = string.IsNullOrWhiteSpace(contraseña);

            // Validación de campos vacíos
            if (usuarioVacio && contrasenaVacia)
            {
                MessageBox.Show("El usuario y la contraseña no pueden estar vacíos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            if (usuarioVacio)
            {
                MessageBox.Show("Ingrese el usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            if (contrasenaVacia)
            {
                MessageBox.Show("Ingrese la contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContraseña.Focus();
                return;
            }

            try
            {
                // 1. Guardamos el historial de log
                ReporteUsuarioDAO reporteDAO = new ReporteUsuarioDAO();
                reporteDAO.IniciarSesion(usuario, contraseña);

                // 2. Ejecutamos la sesión operativa en tu UsuarioDAO
                UsuarioDAO usuarioDAO = new UsuarioDAO();

                if (usuarioDAO.IniciarSesion(usuario, contraseña))
                {
                    MessageBox.Show("Bienvenido " + ClaseSesion.RolActual + " " + ClaseSesion.UsuarioActual, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 3. CONTROL DE ROLES
                    if (ClaseSesion.RolActual.Trim().ToLower() == "cajero")
                    {
                        if (ClaseSesion.TieneCajaActiva)
                        {
                            MessageBox.Show("Detectamos una caja activa. Abriendo módulo de ventas...", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Ventas ventanaVenta = new Ventas();
                            ventanaVenta.FormClosed += (s, args) => this.Close();
                            ventanaVenta.Show();
                        }
                        else
                        {
                            MessageBox.Show("Debe realizar la apertura de caja para iniciar operaciones.", "Apertura Obligatoria", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            AperturaCaja ventanaApertura = new AperturaCaja(ClaseSesion.IdUsuario, ClaseSesion.UsuarioActual);
                            ventanaApertura.FormClosed += (s, args) => this.Close();
                            ventanaApertura.Show();
                        }
                    }
                    else
                    {
                        Menú_Principal ventana = new Menú_Principal();
                        ventana.FormClosed += (s, args) => this.Close();
                        ventana.Show();
                    }

                    this.Hide();
                }
                else
                {
                    // Mensaje unificado y profesional para cuando las credenciales son incorrectas
                    MessageBox.Show("Usuario y contraseña incorrectos.", "Error de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContraseña.Clear();
                    txtContraseña.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnProbarConexion_Click(object sender, EventArgs e)
        {
           
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtContraseña.MaxLength = 20;
            txtUsuario.MaxLength = 20;
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
                int numeroPaginaCaja = 1;

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

        private void btnVerPassword_Click(object sender, EventArgs e)
        {
            passwordVisible = !passwordVisible;

            if (passwordVisible)
            {
                // Muestra el texto real
                txtContraseña.PasswordChar = '\0';

                // btnVerPassword.Text = "🔒"; // Opcional si cambias el icono
            }
            else
            {
                // Oculta con viñetas
                txtContraseña.PasswordChar = '●';

                // btnVerPassword.Text = "👁"; // Opcional
            }
        }

        private void txtUsuario_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Evita el sonido molesto de Windows
                txtContraseña.Focus();     // Salta al campo de contraseña
            }
        }

        private void txtContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Evita el sonido molesto de Windows
                btnSesion.PerformClick();  // Simula el clic en el botón de iniciar sesión
            }
        }
    }
}
