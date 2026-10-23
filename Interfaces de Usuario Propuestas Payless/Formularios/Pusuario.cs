using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless.Formularios
{
    public partial class Pusuario : Form
    {
        // =========================================================
        // OBJETO DAO
        // =========================================================

        UsuarioDAO usuarioDAO = new UsuarioDAO();
        private bool passwordVisible = false;


        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public Pusuario()
        {
            InitializeComponent();
            txtTelefono.KeyPress += TxtTelefono_KeyPress;

            // // Cargar eventos
            //btnGuardar.Click += btnGuardar_Click;
            //btnCancelar.Click += btnCancelar_Click;
            //this.Load += Pusuario_Load;
        }


        // =========================================================
        // EVENTO LOAD
        // =========================================================

        private void Pusuario_Load(object sender, EventArgs e)
        {
            txtUsuario.MaxLength = 50;
            txtCompleto.MaxLength = 100;
            txtTelefono.MaxLength = 8;
            txtCedula.MaxLength = 16;
            txtGmail.MaxLength = 50;
            txtContraseña.MaxLength = 20;
            txtConfirmar.MaxLength = 20;

            CargarRoles();
     

            // Contraseñas ocultas
            txtContraseña.PasswordChar = '●';
            txtConfirmar.PasswordChar = '●';
        }


        // =========================================================
        // CARGAR ROLES
        // =========================================================

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


        // =========================================================
        // CARGAR ESTADOS
        // =========================================================

      


        // =========================================================
        // BOTÓN GUARDAR
        // =========================================================

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreUsuario = txtUsuario.Text.Trim();
                string nombreCompleto = txtCompleto.Text.Trim();
                string password = txtContraseña.Text;
                string confirmarPassword = txtConfirmar.Text;
                string telefono = txtTelefono.Text.Trim();
                string cedula = txtCedula.Text.Trim();
                string correo = txtGmail.Text.Trim();

                // 1. VALIDACIONES DE TEXTO Y FORMATO (Tus validaciones existentes)
                if (string.IsNullOrWhiteSpace(nombreUsuario))
                {
                    MessageBox.Show("Ingrese el nombre del usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsuario.Focus();
                    return;
                }

                Regex regexUsuario = new Regex(@"^[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+$");
                if (!regexUsuario.IsMatch(nombreUsuario))
                {
                    MessageBox.Show("El nombre de usuario debe ser una sola palabra y comenzar con mayúscula.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsuario.Focus();
                    return;
                }

                if (txtCompleto.Text.Trim().Length > 100)
                {
                    MessageBox.Show("El campo [nombre completo] no puede superar los 100 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCompleto.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(nombreCompleto))
                {
                    MessageBox.Show("Ingrese el nombre completo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCompleto.Focus();
                    return;
                }

                Regex regexCorreo = new Regex(@"^[a-zA-Z0-9._-]+@(gmail|yahoo)\.com$");

                if (!regexCorreo.IsMatch(correo))
                {
                    MessageBox.Show(
                        "El correo electrónico no es válido.\n\n" +
                        "• Debe pertenecer a Gmail o Yahoo (ejemplo: usuario456@gmail.com o usuario@yahoo.com).\n" +
                        "• No se permiten caracteres especiales extraños (solo puntos, guiones o guiones bajos).",
                        "Validación de Correo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtGmail.Focus();
                    txtGmail.SelectAll();
                    return;
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Ingrese una contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                if (password != confirmarPassword)
                {
                    MessageBox.Show("Las contraseñas no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtConfirmar.Focus();
                    return;
                }

                if (telefono.Length != 8)
                {
                    MessageBox.Show("El número de teléfono debe tener exactamente 8 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTelefono.Focus();
                    return;
                }

                Regex regexCedula = new Regex(@"^\d{3}-\d{6}-\d{4}[A-Z]$");
                if (!regexCedula.IsMatch(cedula))
                {
                    MessageBox.Show("La cédula debe cumplir con el formato correcto, por ejemplo: 441-241207-1002B", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCedula.Focus();
                    return;
                }

                Regex regexGmail = new Regex(@"^[^@\s]+@gmail\.com$");
                if (!regexGmail.IsMatch(correo))
                {
                    MessageBox.Show("El correo debe ser una dirección válida de Gmail (@gmail.com).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGmail.Focus();
                    return;
                }

                if (cmbRol.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un rol.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbRol.Focus();
                    return;
                }

                int idRol = Convert.ToInt32(cmbRol.SelectedValue);

                // =========================================================================
                // 2. VALIDAR DUPLICADOS ANTES DE INSERTAR EN LA BASE DE DATOS
                // =========================================================================
                if (usuarioDAO.ExisteNombreUsuario(nombreUsuario))
                {
                    MessageBox.Show("El nombre de usuario ya se encuentra registrado. Elija otro.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsuario.Focus();
                    txtUsuario.SelectAll();
                    return;
                }

                if (usuarioDAO.ExisteCedula(cedula))
                {
                    MessageBox.Show("El número de cédula ingresado ya pertenece a otro usuario.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCedula.Focus();
                    txtCedula.SelectAll();
                    return;
                }

                if (usuarioDAO.ExisteCorreo(correo))
                {
                    MessageBox.Show("El correo electrónico ya está registrado en el sistema.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGmail.Focus();
                    txtGmail.SelectAll();
                    return;
                }

                // =========================================================================
                // 3. INTENTAR GUARDAR
                // =========================================================================
                bool estadoDefault = true;

                bool resultado = usuarioDAO.AgregarUsuario(
                    nombreUsuario,
                    nombreCompleto,
                    password,
                    idRol,
                    correo,
                    telefono,
                    cedula,
                    estadoDefault);

                if (resultado)
                {
                    MessageBox.Show("Usuario agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el usuario por un problema desconocido en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al guardar el usuario:\n\n" + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    


        // =========================================================
        // BOTÓN CANCELAR
        // =========================================================

        private void btnCancelar_Click(object sender, EventArgs e)
        {



            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void TxtTelefono_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnConfirmarVC_Click(object sender, EventArgs e)
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