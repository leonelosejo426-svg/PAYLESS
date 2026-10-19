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
        UsuarioDAO usuarioDAO = new UsuarioDAO();

        private int idUsuarioSeleccionado = 0;

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
            

            cmbEstado.Items.Clear();
            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Inactivo");

            cmbEstado.SelectedIndex = -1;
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
                int idUsuario =
                    Convert.ToInt32(cmbUsuario.SelectedValue);

                DataTable tabla =
                    usuarioDAO.ObtenerUsuario(idUsuario);

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

                // Guardar ID seleccionado
                idUsuarioSeleccionado =
                    Convert.ToInt32(fila["id_usuario"]);

                // Cargar datos
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
                    cmbRol.SelectedValue =
                        Convert.ToInt32(fila["id_rol"]);
                }

                // Estado
                if (fila["estado"] != DBNull.Value)
                {
                    bool estado =
                        Convert.ToBoolean(fila["estado"]);

                    cmbEstado.SelectedIndex =
                        estado ? 0 : 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar el usuario:\n\n" +
                    ex.Message,
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

                btnActualizar.Focus();
                return;
            }

            string nombreUsuario = txtNombreUsuario.Text.Trim();
            string nombreCompleto = txtNombreCompleto.Text.Trim();
            string password = txtContraseña.Text;
            string confirmarPassword = txtConfirmar.Text;
            string telefono = txtTelefono.Text.Trim();
            string cedula = txtCedula.Text.Trim();
            string correo = txtGmail.Text.Trim();

            // Validar nombre de usuario (Maysa a balikas, agrugi iti dakkel a letra)
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

            // Validar nombre completo (Nagan ken apelyido, tunggal balikas agrugi iti dakkel)
            if (string.IsNullOrWhiteSpace(nombreCompleto))
            {
                MessageBox.Show("Ingrese el nombre completo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCompleto.Focus();
                return;
            }

            if (password.Trim().Length > 20)
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

            if (txtNombreUsuario.Text.Trim().Length > 50)
            {
                MessageBox.Show(
                    "El campo [nombre de usuario] no puede superar los [50] caracteres.",
                    "Validación de Longitud",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtNombreUsuario.Focus();
                return;
            }

            if (txtNombreCompleto.Text.Trim().Length > 100)
            {
                MessageBox.Show(
                    "El campo [nombre completo] no puede superar los [100] caracteres.",
                    "Validación de Longitud",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtNombreCompleto.Focus();
                return;
            }

            Regex regexCompleto = new Regex(@"^([A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)(\s[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)+$");
            if (!regexCompleto.IsMatch(nombreCompleto))
            {
                MessageBox.Show("El nombre completo debe tener al menos un nombre y un apellido, y cada palabra debe empezar con mayúscula.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCompleto.Focus();
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

            // Validar contraseña (Minimo 6, 1 numero, 1 espesial)
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

            // Confirmar contraseña
            if (password != confirmarPassword)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmar.Focus();
                return;
            }

            // Validar Teléfono (Exactamente 8 dígitos)
            if (telefono.Length != 8)
            {
                MessageBox.Show("El número de teléfono debe tener exactamente 8 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return;
            }

            // Validar Cédula (Ejemplo: 441-241207-1002B)
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

            try
            {
                int idRol = Convert.ToInt32(cmbRol.SelectedValue);
                bool estadoDefault = true; // Naikkat ti cmbEstado, naipatulod ti default estado

                bool resultado = usuarioDAO.EditarUsuario(
                    idUsuarioSeleccionado,
                    nombreUsuario,
                    nombreCompleto,
                    password,
                    idRol,
                    correo,
                    telefono,
                    cedula,
                    estadoDefault
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
    }
}
