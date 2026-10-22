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
                // =================================================
                // OBTENER DATOS
                // =================================================

                string nombreUsuario = txtUsuario.Text.Trim();
                string nombreCompleto = txtCompleto.Text.Trim();
                string password = txtContraseña.Text;
                string confirmarPassword = txtConfirmar.Text;
                string telefono = txtTelefono.Text.Trim();
                string cedula = txtCedula.Text.Trim();
                string correo = txtGmail.Text.Trim();


                // =================================================
                // VALIDAR NOMBRE DE USUARIO
                // (Maysa a nagan, agrugi iti dakkel a letra)
                // =================================================

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
                    MessageBox.Show(
                        "El campo [nombre completo] no puede superar los [100] caracteres.",
                        "Validación de Longitud",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtCompleto.Focus();
                    return;
                }

                if (txtContraseña.Text.Trim().Length > 20)
                {
                    MessageBox.Show(
                        "El campo [contraseña] no puede superar los [20] caracteres.",
                        "Validación de Longitud",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtContraseña.Focus();
                    return;
                }

                if (txtGmail.Text.Trim().Length > 50)
                {
                    MessageBox.Show(
                        "El campo [gmail] no puede superar los [50] caracteres.",
                        "Validación de Longitud",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtGmail.Focus();
                    return;
                }

                // =================================================
                // VALIDAR NOMBRE COMPLETO
                // (Maysa a nagan ken apelyido, tunggal balikas dakkel a letra)
                // =================================================

                if (string.IsNullOrWhiteSpace(nombreCompleto))
                {
                    MessageBox.Show("Ingrese el nombre completo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCompleto.Focus();
                    return;
                }

                Regex regexCompleto = new Regex(@"^([A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)(\s[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)+$");
                if (!regexCompleto.IsMatch(nombreCompleto))
                {
                    MessageBox.Show("El nombre completo debe tener al menos un nombre y un apellido, y cada palabra debe empezar con mayúscula.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCompleto.Focus();
                    return;
                }


                // =================================================
                // VALIDAR CONTRASEÑA
                // (Minimo 6 caracteres, 1 especial, 1 numero)
                // =================================================

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
                    MessageBox.Show(
                        "La contraseña debe contener al menos una letra mayúscula.",
                        "Validación de Contraseña",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtContraseña.Focus();
                    return;
                }


                // =================================================
                // CONFIRMAR CONTRASEÑA
                // =================================================

                if (password != confirmarPassword)
                {
                    MessageBox.Show("Las contraseñas no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtConfirmar.Focus();
                    return;
                }


                // =================================================
                // VALIDAR NUMERO DE TELEFONO
                // =================================================

                if (telefono.Length != 8)
                {
                    MessageBox.Show("El número de teléfono debe tener exactamente 8 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTelefono.Focus();
                    return;
                }


                // =================================================
                // VALIDAR CEDULA (Ejemplo: 441-241207-1002B)
                // =================================================

                Regex regexCedula = new Regex(@"^\d{3}-\d{6}-\d{4}[A-Z]$");
                if (!regexCedula.IsMatch(cedula))
                {
                    MessageBox.Show("La cédula debe cumplir con el formato correcto, por ejemplo: 441-241207-1002B", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCedula.Focus();
                    return;
                }


                // =================================================
                // VALIDAR GMAIL (@gmail.com)
                // =================================================

                Regex regexGmail = new Regex(@"^[^@\s]+@gmail\.com$");
                if (!regexGmail.IsMatch(correo))
                {
                    MessageBox.Show("El correo debe ser una dirección válida de Gmail (@gmail.com).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGmail.Focus();
                    return;
                }


                // =================================================
                // VALIDAR ROL
                // =================================================

                if (cmbRol.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un rol.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbRol.Focus();
                    return;
                }


                // =================================================
                // OBTENER ID DEL ROL
                // =================================================

                int idRol = Convert.ToInt32(cmbRol.SelectedValue);


                // =================================================
                // GUARDAR MEDIANTE USUARIODAO 
                // (Naikkaten ti estado, inkabil ti default a true wenno kasapulan)
                // =================================================

                bool estadoDefault = true; // Kas state a naikkaten ti UI, ipatulodtayon ti default

                bool resultado =
                    usuarioDAO.AgregarUsuario(
                        nombreUsuario,
                        nombreCompleto,
                        password,
                        idRol,
                        correo,
                        telefono,
                        cedula,
                        estadoDefault);


                // =================================================
                // RESULTADO
                // =================================================

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
    }
}