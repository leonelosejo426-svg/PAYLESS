using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless.Formularios
{
    public partial class Pusuario : Form
    {
        UsuarioDAO usuarioDAO = new UsuarioDAO();

        public Pusuario()
        {
            InitializeComponent();
            // Eventos de los botones
            btnGuardar.Click += btnGuardar_Click;
            btnCancelar.Click += btnCancelar_Click;

            // Evento Load
            this.Load += SubUsuarioAgregar_Load;
            MostrarUsuarios()
        }

        private void CargarRoles()
        {
            try
            {
                DataTable tabla = usuarioDAO.CargarRoles();

                cmbRol.DataSource = tabla;
                cmbRol.DisplayMember = "nombre_rol";
                cmbRol.ValueMember = "id_rol";

                cmbRol.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron cargar los roles.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // CARGAR ESTADOS
        // =========================================================

        private void CargarEstados()
        {
            cmbEstado.Items.Clear();

            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Inactivo");

            cmbEstado.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreUsuario = txtNombreUsuario.Text.Trim();
                string nombreCompleto = txtNombreCompleto.Text.Trim();
                string password = txtPassword.Text;
                string confirmarPassword = txtConfirmarPassword.Text;

                if (string.IsNullOrWhiteSpace(nombreUsuario))
                {
                    MessageBox.Show("Ingrese el nombre de usuario.");
                    txtNombreUsuario.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(nombreCompleto))
                {
                    MessageBox.Show("Ingrese el nombre completo.");
                    txtNombreCompleto.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Ingrese una contraseña.");
                    txtPassword.Focus();
                    return;
                }

                if (password != confirmarPassword)
                {
                    MessageBox.Show("Las contraseñas no coinciden.");
                    txtConfirmarPassword.Focus();
                    return;
                }

                if (cmbRol.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un rol.");
                    cmbRol.Focus();
                    return;
                }

                int idRol = Convert.ToInt32(cmbRol.SelectedValue);

                bool estado = true;

                if (cmbEstado.SelectedItem != null)
                {
                    estado = cmbEstado.SelectedItem.ToString() == "Activo";
                }

                bool resultado = usuarioDAO.AgregarUsuario(
                    nombreUsuario,
                    nombreCompleto,
                    password,
                    idRol,
                    estado);

                if (resultado)
                {
                    MessageBox.Show(
                        "Usuario agregado correctamente.",
                        "Guardar usuario",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo agregar el usuario.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al guardar el usuario.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void MostrarUsuarios()
        {
            try
            {
                dgvUsuarios.DataSource = usuarioDAO.MostrarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los usuarios: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Pusuario_Load(object sender, EventArgs e)
        CargarRoles();
        CargarEstados();

        // Configuración de contraseñas
        txtPassword.PasswordChar = '●';
            txtConfirmarPassword.PasswordChar = '●';
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();
            this.Hide();
        }
    }
}
