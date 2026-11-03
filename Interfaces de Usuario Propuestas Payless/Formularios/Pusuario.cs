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
        private bool cargandoFormulario = true;
        private bool cargandoDatos = false;



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
            cargandoFormulario = false;
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

                // =========================================================================
                // FASE 1: VALIDACIONES DE FORMATO, LONGITUD Y REGLAS LOCALES
                // =========================================================================

                // 1. Nombre de Usuario
                if (string.IsNullOrWhiteSpace(nombreUsuario))
                {
                    MessageBox.Show("Ingrese el nombre del usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsuario.Focus();
                    return;
                }

                if (nombreUsuario.Length < 3)
                {
                    MessageBox.Show("El nombre de usuario debe tener al menos 3 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsuario.Focus();
                    txtUsuario.SelectAll();
                    return;
                }

                Regex regexUsuario = new Regex(@"^[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+$");
                if (!regexUsuario.IsMatch(nombreUsuario))
                {
                    MessageBox.Show("El nombre de usuario debe ser una sola palabra y comenzar con mayúscula.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsuario.Focus();
                    txtUsuario.SelectAll();
                    return;
                }

                // 2. Nombre Completo (Validado: sin números y al menos dos palabras)
                if (string.IsNullOrWhiteSpace(nombreCompleto))
                {
                    MessageBox.Show("Ingrese el nombre completo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCompleto.Focus();
                    return;
                }

                if (nombreCompleto.Length > 100)
                {
                    MessageBox.Show("El campo [nombre completo] no puede superar los 100 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCompleto.Focus();
                    return;
                }

                Regex regexNombreCompleto = new Regex(@"^[A-Za-záéíóúñÁÉÍÓÚÑ]+(\s+[A-Za-záéíóúñÁÉÍÓÚÑ]+)+$");
                if (!regexNombreCompleto.IsMatch(nombreCompleto))
                {
                    MessageBox.Show("El nombre completo debe contener al menos un nombre y un apellido, y no debe incluir números ni símbolos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCompleto.Focus();
                    txtCompleto.SelectAll();
                    return;
                }

                // 3. Número de Teléfono
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

                // 4. Cédula
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

                // 5. Correo Electrónico (Gmail / Yahoo)
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

                // 6. Contraseña
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
                    txtConfirmar.SelectAll();
                    return;
                }

                // 7. Rol
                // Validar Rol seleccionado
                if (cmbRol.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un rol.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbRol.Focus();
                    return;
                }

                // Obtener el texto del rol seleccionado (asegúrate de que cmbRol muestre el nombre del rol o ajústalo según tu binding)
                string rolSeleccionado = cmbRol.Text.Trim();
                int idRol = Convert.ToInt32(cmbRol.SelectedValue);

                // Validar límites por tipo de rol
                if (rolSeleccionado.Equals("Administrador", StringComparison.OrdinalIgnoreCase))
                {
                    int cantAdmins = usuarioDAO.ContarUsuariosPorRol("Administrador", 0);
                    if (cantAdmins >= 1)
                    {
                        MessageBox.Show("Ya existe un Administrador registrado en el sistema. Solo se permite un Administrador.", "Límite de Rol", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbRol.Focus();
                        return;
                    }
                }
                else if (rolSeleccionado.Equals("Gerente", StringComparison.OrdinalIgnoreCase))
                {
                    int cantGerentes = usuarioDAO.ContarUsuariosPorRol("Gerente", 0);
                    int limiteGerentes = 5; // Puedes cambiar este número si deseas otro límite
                    if (cantGerentes >= limiteGerentes)
                    {
                        MessageBox.Show($"Se ha alcanzado el límite máximo de gerentes permitidos ({limiteGerentes}).", "Límite de Rol", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbRol.Focus();
                        return;
                    }
                }
                // Los cajeros quedan con acceso libre (infinito)

                // =========================================================================
                // FASE 2: VALIDAR DUPLICADOS EN LA BASE DE DATOS
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
                // FASE 3: INTENTAR GUARDAR EN LA BD
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

        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 3. Si se está cargando el formulario o no hay selección, no hacemos nada
            if (cargandoFormulario || cmbRol.SelectedIndex == -1) return;

            string rolSeleccionado = cmbRol.Text.Trim();
            int idVerificar = 0; // Como es nuevo usuario, siempre es 0

            if (rolSeleccionado.Equals("Administrador", StringComparison.OrdinalIgnoreCase))
            {
                int cantAdmins = usuarioDAO.ContarUsuariosPorRol("Administrador", idVerificar);
                if (cantAdmins >= 1)
                {
                    MessageBox.Show("Ya existe un Administrador registrado en el sistema. Solo se permite un Administrador.", "Límite de Rol", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbRol.SelectedIndex = -1; // Limpia la selección
                }
            }
            else if (rolSeleccionado.Equals("Gerente", StringComparison.OrdinalIgnoreCase))
            {
                int cantGerentes = usuarioDAO.ContarUsuariosPorRol("Gerente", idVerificar);
                int limiteGerentes = 2; // Límite máximo de gerentes
                if (cantGerentes >= limiteGerentes)
                {
                    MessageBox.Show($"Se ha alcanzado el límite máximo de gerentes permitidos ({limiteGerentes}).", "Límite de Rol", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbRol.SelectedIndex = -1; // Limpia la selección
                }
            }
        }
    }
}