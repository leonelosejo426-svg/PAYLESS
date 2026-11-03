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
        private bool cargandoFormulario = true;
        private bool cargandoDatos = false;

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

            cargandoFormulario = false;
        }

        // =========================================================
        // CARGAR USUARIOS EN EL COMBOBOX
        // =========================================================



        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cargandoDatos = true;
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
            cargandoDatos = false;
        }





        private void btnActualizar_Click(object sender, EventArgs e)
        {
            // 1. Validar que primero se haya seleccionado un usuario
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
            string correo = txtGmail.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string cedula = txtCedula.Text.Trim();
            string password = txtContraseña.Text;
            string confirmarPassword = txtConfirmar.Text;

            // 2. Validar Nombre de Usuario (Vacío, mínimo 3 caracteres, formato)
            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                MessageBox.Show("Ingrese el nombre del usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreUsuario.Focus();
                return;
            }

            if (nombreUsuario.Length < 3)
            {
                MessageBox.Show("El nombre de usuario debe tener al menos 3 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreUsuario.Focus();
                txtNombreUsuario.SelectAll();
                return;
            }

            Regex regexUsuario = new Regex(@"^[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+$");
            if (!regexUsuario.IsMatch(nombreUsuario))
            {
                MessageBox.Show("El nombre de usuario debe ser una sola palabra y comenzar con mayúscula.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreUsuario.Focus();
                txtNombreUsuario.SelectAll();
                return;
            }

            if (nombreUsuario.Length > 50)
            {
                MessageBox.Show("El campo [nombre de usuario] no puede superar los 50 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreUsuario.Focus();
                return;
            }

            // 3. Validar Nombre Completo (Vacío, sin números, al menos dos palabras)
            if (string.IsNullOrWhiteSpace(nombreCompleto))
            {
                MessageBox.Show("Ingrese el nombre completo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCompleto.Focus();
                return;
            }

            if (nombreCompleto.Length > 100)
            {
                MessageBox.Show("El campo [nombre completo] no puede superar los 100 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCompleto.Focus();
                return;
            }

            Regex regexNombreCompleto = new Regex(@"^[A-Za-záéíóúñÁÉÍÓÚÑ]+(\s+[A-Za-záéíóúñÁÉÍÓÚÑ]+)+$");
            if (!regexNombreCompleto.IsMatch(nombreCompleto))
            {
                MessageBox.Show("El nombre completo debe contener al menos un nombre y un apellido, y no debe incluir números ni símbolos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCompleto.Focus();
                txtNombreCompleto.SelectAll();
                return;
            }

            // 4. Validar Correo Electrónico
            if (string.IsNullOrWhiteSpace(correo))
            {
                MessageBox.Show("Ingrese el correo electrónico.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGmail.Focus();
                return;
            }

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

            // 5. Validar Rol
            // 5. Validar Rol y Límites permitidos
            if (cmbRol.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un rol.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRol.Focus();
                return;
            }

            string rolSeleccionado = cmbRol.Text.Trim();
            int idRol = Convert.ToInt32(cmbRol.SelectedValue);
            int idVerificar = idUsuarioSeleccionado;

            // Validación de límites por tipo de rol (excluyendo al usuario actual en edición)
            if (rolSeleccionado.Equals("Administrador", StringComparison.OrdinalIgnoreCase))
            {
                int cantAdmins = usuarioDAO.ContarUsuariosPorRol("Administrador", idVerificar);
                if (cantAdmins >= 1)
                {
                    MessageBox.Show("Ya existe otro Administrador registrado en el sistema. Solo se permite un Administrador.", "Límite de Rol", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbRol.Focus();
                    return;
                }
            }
            else if (rolSeleccionado.Equals("Gerente", StringComparison.OrdinalIgnoreCase))
            {
                int cantGerentes = usuarioDAO.ContarUsuariosPorRol("Gerente", idVerificar);
                int limiteGerentes = 2; // Puedes cambiar este número si deseas otro límite
                if (cantGerentes >= limiteGerentes)
                {
                    MessageBox.Show($"Se ha alcanzado el límite máximo de gerentes permitidos ({limiteGerentes}).", "Límite de Rol", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbRol.Focus();
                    return;
                }
            }
            // Los cajeros quedan con acceso libre e ilimitado

            // 6. Validar Teléfono
            if (string.IsNullOrWhiteSpace(telefono))
            {
                MessageBox.Show("Ingrese el número de teléfono.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return;
            }

            if (telefono.Length != 8 || !telefono.All(char.IsDigit))
            {
                MessageBox.Show("El número de teléfono debe tener exactamente 8 dígitos numéricos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                txtTelefono.SelectAll();
                return;
            }

            // 7. Validar Cédula
            // Validar Cédula (Formato estricto con validación lógica de fecha: DDMMYY)
            if (string.IsNullOrWhiteSpace(cedula))
            {
                MessageBox.Show("Ingrese el número de cédula.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCedula.Focus();
                return;
            }

            Regex regexCedula = new Regex(@"^\d{3}-\d{6}-\d{4}[A-Z]$");
            if (!regexCedula.IsMatch(cedula))
            {
                MessageBox.Show("La cédula debe cumplir con el formato correcto, por ejemplo: 441-241207-1002B", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCedula.Focus();
                txtCedula.SelectAll();
                return;
            }

            // Validación lógica de los 6 dígitos del medio como fecha (DDMMYY: Días 01-31, Meses 01-12)
            string parteFecha = cedula.Substring(4, 6); // Extrae los 6 dígitos centrales ej: "241207"
            if (int.TryParse(parteFecha.Substring(0, 2), out int dia) && dia >= 1 && dia <= 31 &&
                int.TryParse(parteFecha.Substring(2, 2), out int mes) && mes >= 1 && mes <= 12 &&
                int.TryParse(parteFecha.Substring(4, 2), out int anio))
            {
                // La estructura de fecha interna es válida
            }
            else
            {
                MessageBox.Show("Los 6 dígitos centrales de la cédula no corresponden a una fecha válida (Día: 01-31, Mes: 01-12).\nEjemplo válido: 441-241207-1002B", "Validación de Cédula", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCedula.Focus();
                txtCedula.SelectAll();
                return;
            }

            // 8. Validar Estado
            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un estado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEstado.Focus();
                return;
            }

            // 9. Validar Contraseña
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

            // 10. Confirmar Contraseña
            if (password != confirmarPassword)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmar.Focus();
                txtConfirmar.SelectAll();
                return;
            }

            // =========================================================================
            // VALIDACIÓN DE DUPLICADOS EXCLUYENDO AL USUARIO ACTUAL
            // =========================================================================
            idVerificar = idUsuarioSeleccionado; // Reutilizamos sin declarar 'int' de nuevo

            if (usuarioDAO.ExisteNombreUsuario(nombreUsuario, idVerificar))
            {
                MessageBox.Show("El nombre de usuario ya pertenece a otra cuenta.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreUsuario.Focus();
                txtNombreUsuario.SelectAll();
                return;
            }

            if (usuarioDAO.ExisteCedula(cedula, idVerificar))
            {
                MessageBox.Show("El número de cédula ya está registrado en otra cuenta.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCedula.Focus();
                txtCedula.SelectAll();
                return;
            }

            if (usuarioDAO.ExisteCorreo(correo, idVerificar))
            {
                MessageBox.Show("El correo electrónico ya está registrado en otra cuenta.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGmail.Focus();
                txtGmail.SelectAll();
                return;
            }

            if (usuarioDAO.ExisteTelefono(telefono, idVerificar))
            {
                MessageBox.Show("El número de teléfono ya está registrado en otra cuenta.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                txtTelefono.SelectAll();
                return;
            }

            try
            {
                idRol = Convert.ToInt32(cmbRol.SelectedValue); // Asignamos directamente sin declarar 'int'
                bool estadoSeleccionado = (cmbEstado.SelectedIndex == 0);

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

        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 🌟 3. Si el formulario está cargando, o si estamos cargando los datos del usuario buscado, no hacemos NADA
            if (cargandoFormulario || cargandoDatos || cmbRol.SelectedIndex == -1) return;

            string rolSeleccionado = cmbRol.Text.Trim();
            int idVerificar = idUsuarioSeleccionado;

            if (rolSeleccionado.Equals("Administrador", StringComparison.OrdinalIgnoreCase))
            {
                int cantAdmins = usuarioDAO.ContarUsuariosPorRol("Administrador", idVerificar);
                if (cantAdmins >= 1)
                {
                    MessageBox.Show("Ya existe otro Administrador registrado en el sistema. Solo se permite un Administrador.", "Límite de Rol", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbRol.SelectedIndex = -1;
                }
            }
            else if (rolSeleccionado.Equals("Gerente", StringComparison.OrdinalIgnoreCase))
            {
                int cantGerentes = usuarioDAO.ContarUsuariosPorRol("Gerente", idVerificar);
                int limiteGerentes = 2;
                if (cantGerentes >= limiteGerentes)
                {
                    MessageBox.Show($"Se ha alcanzado el límite máximo de gerentes permitidos ({limiteGerentes}).", "Límite de Rol", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbRol.SelectedIndex = -1;
                }
            }
        }
    }
}
