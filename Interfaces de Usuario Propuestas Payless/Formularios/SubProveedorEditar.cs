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
using System.Windows.Media.TextFormatting;

namespace Interfaces_de_Usuario_Propuestas_Payless.Datos
{
    public partial class SubProveedorEditar : Form
    {
        ProveedorDAO proveedorDAO = new ProveedorDAO();
        private int idProveedorSeleccionado = 0;
        public SubProveedorEditar()
        {
            InitializeComponent();
            txtTelefono.KeyPress += txtTelefono_KeyPress;
        }

        private void SubProveedorEditar_Load(object sender, EventArgs e)
        {
            txtCorreo.MaxLength = 50;
            txtNombre.MaxLength = 50;
            txtTelefono.MaxLength = 8;
            txtDireccion.MaxLength = 200;
            txtRuc.MaxLength = 14;

            CargarProveedores();

            cmbEstado.Items.Clear();

            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Inactivo");

            cmbEstado.SelectedIndex = 0;

            txtCodigo.ReadOnly = true;
        }
        private void CargarProveedores()
        {
            cmbProveedor.DataSource =
                proveedorDAO.CargarProveedoresEditar();

            cmbProveedor.DisplayMember =
                "nombre";

            cmbProveedor.ValueMember =
                "id_proveedor";

            cmbProveedor.SelectedIndex = -1;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione un proveedor.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (cmbProveedor.SelectedValue == null)
            {
                MessageBox.Show(
                    "Seleccione un proveedor válido.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // 🌟 CORRECCIÓN: Guardamos el ID seleccionado en la variable global
            idProveedorSeleccionado = Convert.ToInt32(cmbProveedor.SelectedValue);

            CargarProveedor(idProveedorSeleccionado);
        }

        private void CargarProveedor(int idProveedor)
        {
            ClaseProveedor proveedor = proveedorDAO.ObtenerProveedor(idProveedor);

            if (proveedor == null)
            {
                MessageBox.Show(
                    "No se encontró el proveedor.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            txtCodigo.Text = proveedor.IdProveedor.ToString();
            txtNombre.Text = proveedor.Nombre;
            txtTelefono.Text = proveedor.Telefono;
            txtCorreo.Text = proveedor.Correo;
            txtDireccion.Text = proveedor.Direccion;
            txtRuc.Text = proveedor.Ruc;

            cmbEstado.SelectedItem = proveedor.EstadoProveedor
                ? "Activo"
                : "Inactivo";
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            /// 1. Validar que se haya seleccionado un proveedor previamente
            if (idProveedorSeleccionado == 0)
            {
                MessageBox.Show("Seleccione y busque un proveedor para editar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProveedor.Focus();
                return;
            }

            // Quitar espacios innecesarios
            string nombre = txtNombre.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            string direccion = txtDireccion.Text.Trim();
            string ruc = txtRuc.Text.Trim();

            // =====================================================
            // 1. VALIDAR CAMPOS VACÍOS, LONGITUDES Y FORMATOS (EN ORDEN ESTRICTO)
            // =====================================================

            // 1.1 Validar Nombre
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese el nombre del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (nombre.Length < 3)
            {
                MessageBox.Show("El nombre del proveedor debe tener al menos 3 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (nombre.Length > 100)
            {
                MessageBox.Show("El campo [nombre] no puede superar los [100] caracteres.", "Validación de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            Regex regexNombre = new Regex(@"^([A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)(\s[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)+$");
            if (!regexNombre.IsMatch(nombre))
            {
                MessageBox.Show("El nombre completo debe tener al menos un nombre y un apellido, y cada palabra debe empezar con mayúscula.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            // 1.2 Validar Teléfono (Segundo campo a validar)
            if (string.IsNullOrWhiteSpace(telefono))
            {
                MessageBox.Show("Ingrese el teléfono del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            // 1.3 Validar Correo (Tercer campo a validar)
            if (string.IsNullOrWhiteSpace(correo))
            {
                MessageBox.Show("Ingrese el correo del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return;
            }

            if (correo.Length > 50)
            {
                MessageBox.Show("El campo [correo] no puede superar los [50] caracteres.", "Validación de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
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
                txtCorreo.Focus();
                txtCorreo.SelectAll();
                return;
            }

            // 1.4 Validar Dirección (Cuarto campo a validar)
            if (string.IsNullOrWhiteSpace(direccion))
            {
                MessageBox.Show("Ingrese la dirección del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccion.Focus();
                return;
            }

            if (direccion.Length > 200)
            {
                MessageBox.Show("El campo [dirección] no puede superar los [200] caracteres.", "Validación de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccion.Focus();
                return;
            }

            // 1.5 Validar Código RUC (Quinto campo a validar)
            if (string.IsNullOrWhiteSpace(ruc))
            {
                MessageBox.Show("Ingrese el código RUC del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRuc.Focus();
                return;
            }

            Regex regexRuc = new Regex(@"^[A-Z0-9\-]{14}$");
            if (!regexRuc.IsMatch(ruc))
            {
                MessageBox.Show("El RUC de Nicaragua debe tener un formato válido de 14 caracteres (ej. J1310000252297).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRuc.Focus();
                return;
            }

            // 1.6 Validar Estado (Sexto campo a validar)
            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un estado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEstado.Focus();
                return;
            }

            // =====================================================
            // 2. VALIDAR DUPLICADOS EN LA BASE DE DATOS (EXCLUYENDO AL PROVEEDOR ACTUAL)
            // =====================================================
            int idVerificar = idProveedorSeleccionado;

            if (proveedorDAO.ExisteNombre(nombre, idVerificar))
            {
                MessageBox.Show("Ya existe otro proveedor registrado con este mismo nombre.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (proveedorDAO.ExisteTelefono(telefono, idVerificar))
            {
                MessageBox.Show("El número de teléfono ingresado ya pertenece a otro proveedor.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return;
            }

            if (proveedorDAO.ExisteCorreo(correo, idVerificar))
            {
                MessageBox.Show("El correo electrónico ingresado ya se encuentra registrado con otro proveedor.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return;
            }

            if (proveedorDAO.ExisteRuc(ruc, idVerificar))
            {
                MessageBox.Show("El número de RUC ingresado ya está asignado a otro proveedor.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRuc.Focus();
                return;
            }

            // =====================================================
            // 3. ACTUALIZAR PROVEEDOR EN LA BASE DE DATOS
            // =====================================================
            ClaseProveedor proveedor = new ClaseProveedor();
            proveedor.IdProveedor = idProveedorSeleccionado;
            proveedor.Nombre = nombre;
            proveedor.Telefono = telefono;
            proveedor.Correo = correo;
            proveedor.Direccion = direccion;
            proveedor.Ruc = ruc;
            proveedor.EstadoProveedor = (cmbEstado.Text == "Activo");

            bool actualizado = proveedorDAO.EditarProveedor(proveedor);

            if (actualizado)
            {
                MessageBox.Show(
                    "Proveedor actualizado correctamente.",
                    "Actualización exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Proveedores ventana = new Proveedores();
                ventana.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo actualizar el proveedor. Verifique los datos e intente nuevamente.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }




        private void btnRegresar_Click(object sender, EventArgs e)
        {

            this.Close();
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
