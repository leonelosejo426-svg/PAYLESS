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
                txtCodigo.Text =
                    fila["id_usuario"].ToString();

                txtNombreUsuario.Text =
                    fila["nombre_usuario"].ToString();

                txtNombreCompleto.Text =
                    fila["nombre_completo"].ToString();

                txtGmail.Text = fila["correo"].ToString();

                txtCedula.Text = fila["cedula"].ToString();

                txtTelefono.Text = fila["telefono"].ToString();

                txtContraseña.Text =
                    fila["password"].ToString();

                txtConfirmar.Text =
                    fila["password"].ToString();

               

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

            // Validar nombre de usuario
            if (txtNombreUsuario.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Ingrese el nombre de usuario.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNombreUsuario.Focus();
                return;
            }

            // Validar nombre completo
            if (txtNombreCompleto.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Ingrese el nombre completo.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNombreCompleto.Focus();
                return;
            }

            // Validar contraseña
            if (txtContraseña.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Ingrese la contraseña.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtContraseña.Focus();
                return;
            }

            // Validar confirmación de contraseña
            if (txtConfirmar.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Confirme la contraseña.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtConfirmar.Focus();
                return;
            }

            // Comprobar que las contraseñas coincidan
            if (txtContraseña.Text != txtConfirmar.Text)
            {
                MessageBox.Show(
                    "Las contraseñas no coinciden.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtConfirmar.Focus();
                return;
            }

            // Validar rol
            if (cmbRol.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione un rol.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbRol.Focus();
                return;
            }

            // Validar estado
            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione un estado.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbEstado.Focus();
                return;
            }

            try
            {
                int idRol =
                    Convert.ToInt32(cmbRol.SelectedValue);

                bool estado =
                    cmbEstado.SelectedIndex == 0;



                // =================================================
                // LLAMADA AL MÉTODO EditarUsuario DEL DAO
                // =================================================

                bool resultado = usuarioDAO.EditarUsuario(
                    idUsuarioSeleccionado,
                    txtNombreUsuario.Text.Trim(),
                    txtNombreCompleto.Text.Trim(),
                    txtContraseña.Text,
                    idRol,
                    txtGmail.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    txtCedula.Text.Trim(),
                    estado
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
    }
}
