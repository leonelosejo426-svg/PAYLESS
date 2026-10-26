using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless.Formularios
{
    public partial class Editar_usuarios : Form
    {

        private bool verPassword = false;
        UsuarioDAO usuarioDAO = new UsuarioDAO();
        private int idUsuarioSeleccionado = 0;
        private bool passwordVisible = false;

        public Editar_usuarios()
        {
            InitializeComponent();

        }



        // =========================================================
        // CARGAR LA PANTALLA
        // =========================================================



        private void Editar_usuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
            CargarRoles();
            idUsuarioSeleccionado = 0;

            cmbEstado.Items.Clear();
            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Inactivo");
            cmbEstado.SelectedIndex = -1;

            txtCodigo.Enabled = false; // Deshabilitar el TextBox de código

            // Contraseñas ocultas por defecto
            txtContraseña.PasswordChar = '●';
            txtConfirmar.PasswordChar = '●';

            txtGmail.MaxLength = 50;
            txtNombreUsuario.MaxLength = 50;
            txtNombreCompleto.MaxLength = 50;
            txtCedula.MaxLength = 16;
            txtTelefono.MaxLength = 8;
        }

        // =========================================================
        // CARGAR USUARIOS EN EL COMBOBOX
        // =========================================================



        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbUsuario.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione un usuario para editar.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbUsuario.Focus();
                return;
            }

            try
            {
                int idUsuario = Convert.ToInt32(cmbUsuario.SelectedValue);
                DataTable tabla = usuarioDAO.ObtenerUsuario(idUsuario);

                if (tabla == null || tabla.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontró el usuario seleccionado.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                DataRow fila = tabla.Rows[0];

                // Guardar ID seleccionado en la variable correcta
                idUsuarioSeleccionado = Convert.ToInt32(fila["id_usuario"]);

                // Cargar datos en los controles
                txtCodigo.Text = fila["id_usuario"].ToString();
                txtNombreUsuario.Text = fila["nombre_usuario"].ToString();
                txtNombreCompleto.Text = fila["nombre_completo"].ToString();

                txtGmail.Text = fila["correo"] != DBNull.Value ? fila["correo"].ToString() : "";
                txtCedula.Text = fila["cedula"] != DBNull.Value ? fila["cedula"].ToString() : "";
                txtTelefono.Text = fila["telefono"] != DBNull.Value ? fila["telefono"].ToString() : "";

                txtContraseña.Text = fila["password"].ToString();
                txtConfirmar.Text = fila["password"].ToString();

                // Rol
                if (fila["id_rol"] != DBNull.Value)
                {
                    cmbRol.SelectedValue = Convert.ToInt32(fila["id_rol"]);
                }

                // Estado
                if (fila["estado"] != DBNull.Value)
                {
                    bool estado = Convert.ToBoolean(fila["estado"]);
                    cmbEstado.SelectedIndex = estado ? 0 : 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar el usuario:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }





        private void btnActualizar_Click(object sender, EventArgs e)
        {
            // Validar que primero se haya seleccionado un usuario
            if (idUsuarioSeleccionado == 0)
            {
                MessageBox.Show(
                    "Primero seleccione y busque un usuario.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbUsuario.Focus();
                return;
            }

            string nombreUsuario = txtNombreUsuario.Text.Trim();
            string nombreCompleto = txtNombreCompleto.Text.Trim();
            string password = txtContraseña.Text;
            string confirmarPassword = txtConfirmar.Text;
            string telefono = txtTelefono.Text.Trim();
            string cedula = txtCedula.Text.Trim();
            string correo = txtGmail.Text.Trim();

            // Validar nombre de usuario
            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                MessageBox.Show("Ingrese el nombre del usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreUsuario.Focus();
                return;
            }

            Regex regexUsuario = new Regex(@"^[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+$");
            if (!regexUsuario.IsMatch(nombreUsuario))
            {
                MessageBox.Show("El nombre de usuario debe ser una sola palabra y comenzar con mayúscula.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreUsuario.Focus();
                return;
            }

            if (txtNombreUsuario.Text.Trim().Length > 50)
            {
                MessageBox.Show("El campo [nombre de usuario] no puede superar los 50 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreUsuario.Focus();
                return;
            }

            // Validar nombre completo
            if (string.IsNullOrWhiteSpace(nombreCompleto))
            {
                MessageBox.Show("Ingrese el nombre completo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCompleto.Focus();
                return;
            }

            if (txtNombreCompleto.Text.Trim().Length > 100)
            {
                MessageBox.Show("El campo [nombre completo] no puede superar los 100 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCompleto.Focus();
                return;
            }

            // Se eliminó 'A-Z' para prohibir las mayúsculas
            Regex regexCorreo = new Regex(@"^[a-z0-9._-]+@(gmail|yahoo)\.com$");

            if (!regexCorreo.IsMatch(correo))
            {
                MessageBox.Show(
                    "El correo electrónico no es válido.\n\n" +
                    "• Debe estar completamente en minúsculas.\n" +
                    "• Debe pertenecer a Gmail o Yahoo (ejemplo: usuario456@gmail.com).\n" +
                    "• Solo se permiten puntos, guiones o guiones bajos.",
                    "Validación de Correo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtGmail.Focus();
                txtGmail.SelectAll();
                return;
            }


            // Validar contraseña
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Ingrese una contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContraseña.Focus();
                return;
            }

            if (password.Trim().Length > 20)
            {
                MessageBox.Show("El campo [contraseña] no puede superar los 20 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContraseña.Focus();
                return;
            }

            Regex regexPassword = new Regex(@"^(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{6,}$");
            if (!regexPassword.IsMatch(password))
            {
                MessageBox.Show("La contraseña debe tener al menos 6 caracteres, incluir al menos un número y un carácter especial.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContraseña.Focus();
                return;
            }

            if (!password.Any(char.IsUpper))
            {
                MessageBox.Show("La contraseña debe contener al menos una letra mayúscula.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContraseña.Focus();
                return;
            }

            // Confirmar contraseña
            if (password != confirmarPassword)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmar.Focus();
                return;
            }

            // Validar Teléfono
            if (telefono.Length != 8)
            {
                MessageBox.Show("El número de teléfono debe tener exactamente 8 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return;
            }

            // Validar Cédula
            Regex regexCedula = new Regex(@"^\d{3}-\d{6}-\d{4}[A-Z]$");
            if (!regexCedula.IsMatch(cedula))
            {
                MessageBox.Show("La cédula debe cumplir con el formato correcto, por ejemplo: 441-241207-1002B", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCedula.Focus();
                return;
            }

            // Validar Gmail
            Regex regexGmail = new Regex(@"^[^@\s]+@gmail\.com$");
            if (!regexGmail.IsMatch(correo))
            {
                MessageBox.Show("El correo debe ser una dirección válida de Gmail (@gmail.com).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGmail.Focus();
                return;
            }

            // Validar rol
            if (cmbRol.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un rol.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRol.Focus();
                return;
            }

            // Validar estado
            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un estado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEstado.Focus();
                return;
            }

            // =========================================================================
            // VALIDACIÓN DE DUPLICADOS EXCLUYENDO AL USUARIO ACTUAL
            // =========================================================================
            int idVerificar = idUsuarioSeleccionado; // Pasamos el ID real para que no se choque consigo mismo

            if (usuarioDAO.ExisteNombreUsuario(nombreUsuario, idVerificar))
            {
                MessageBox.Show("El nombre de usuario ya pertenece a otra cuenta.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreUsuario.Focus();
                return;
            }

            if (usuarioDAO.ExisteCedula(cedula, idVerificar))
            {
                MessageBox.Show("El número de cédula ya está registrado en otra cuenta.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCedula.Focus();
                return;
            }

            if (usuarioDAO.ExisteCorreo(correo, idVerificar))
            {
                MessageBox.Show("El correo electrónico ya está registrado en otra cuenta.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGmail.Focus();
                return;
            }

            if (usuarioDAO.ExisteTelefono(telefono, idVerificar))
            {
                MessageBox.Show("El número de teléfono ya está registrado en otra cuenta.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return;
            }

            try
            {
                int idRol = Convert.ToInt32(cmbRol.SelectedValue);
                bool estadoSeleccionado = (cmbEstado.SelectedIndex == 0); // 0 = Activo (true), 1 = Inactivo (false)

                bool resultado = usuarioDAO.EditarUsuario(
                    idUsuarioSeleccionado,
                    nombreUsuario,
                    nombreCompleto,
                    password,
                    idRol,
                    correo,
                    telefono,
                    cedula,
                    estadoSeleccionado
                );

                if (resultado)
                {
                    MessageBox.Show(
                        "Usuario actualizado correctamente.",
                        "Actualización",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CargarUsuarios();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo actualizar el usuario.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al actualizar el usuario: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                DataTable tabla = usuarioDAO.MostrarUsuarios();

                cmbUsuario.DataSource = tabla;
                cmbUsuario.DisplayMember = "nombre_usuario";
                cmbUsuario.ValueMember = "id_usuario";

                cmbUsuario.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron cargar los usuarios.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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
                    "No se pudieron cargar los roles.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }



        private void btnRegresar_Click(object sender, EventArgs e)
        {
            
            this.Close();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (txtTelefono.Text.Length >= 8 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
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

        private void btnVerPasswordC_Click(object sender, EventArgs e)
        {
            passwordVisible = !passwordVisible;

            if (passwordVisible)
            {
                // Muestra el texto real
                txtConfirmar.PasswordChar = '\0';

                // btnVerPassword.Text = "🔒"; // Opcional si cambias el icono
            }
            else
            {
                // Oculta con viñetas
                txtConfirmar.PasswordChar = '●';

                // btnVerPassword.Text = "👁"; // Opcional
            }
        }
    }
}
