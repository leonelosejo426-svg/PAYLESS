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

namespace Interfaces_de_Usuario_Propuestas_Payless.Datos
{
    public partial class SubProveedorEditar : Form
    {
        ProveedorDAO proveedorDAO = new ProveedorDAO();
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

            int idProveedor =
                Convert.ToInt32(
                    cmbProveedor.SelectedValue);

            CargarProveedor(idProveedor);
        }
        private void CargarProveedor(int idProveedor)
        {
            ClaseProveedor proveedor =
                proveedorDAO.ObtenerProveedor(idProveedor);

            if (proveedor == null)
            {
                MessageBox.Show(
                    "No se encontró el proveedor.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            txtCodigo.Text =
                proveedor.IdProveedor.ToString();

            txtNombre.Text =
                proveedor.Nombre;

            txtTelefono.Text =
                proveedor.Telefono;

            txtCorreo.Text =
                proveedor.Correo;

            txtDireccion.Text =
                proveedor.Direccion;

            txtRuc.Text =
                proveedor.Ruc;

            cmbEstado.SelectedItem =
                proveedor.EstadoProveedor
                ? "Activo"
                : "Inactivo";
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            // 1. Verificar que se haya seleccionado y buscado un proveedor
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Primero busque un proveedor para editar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idProveedorActual = Convert.ToInt32(txtCodigo.Text);

            // 2. Validaciones de campos vacíos y longitud
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (txtNombre.Text.Trim().Length < 3)
            {
                MessageBox.Show("El nombre del proveedor debe tener al menos 3 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (txtNombre.Text.Trim().Length > 100)
            {
                MessageBox.Show("El campo [NOMBRE] no puede superar los 100 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (txtTelefono.Text.Length != 8)
            {
                MessageBox.Show("El número de teléfono debe tener exactamente 8 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCorreo.Text) || !Regex.IsMatch(txtCorreo.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Ingrese un correo electrónico válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return;
            }

            if (txtCorreo.Text.Trim().Length > 50)
            {
                MessageBox.Show("El campo [CORREO] no puede superar los 50 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("Ingrese la dirección del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccion.Focus();
                return;
            }

            if (txtDireccion.Text.Trim().Length > 200)
            {
                MessageBox.Show("El campo [DIRECCIÓN] no puede superar los 200 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccion.Focus();
                return;
            }

            Regex regexRuc = new Regex(@"^[A-Z0-9\-]{14}$");
            if (!regexRuc.IsMatch(txtRuc.Text.Trim()))
            {
                MessageBox.Show("El RUC de Nicaragua debe tener un formato válido de 14 caracteres (ej. J1310000252297).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRuc.Focus();
                return;
            }

            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el estado del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEstado.Focus();
                return;
            }

            // =========================================================
            // 3. VALIDAR DUPLICADOS EN LA BASE DE DATOS (EXCLUYENDO ESTE ID)
            // =========================================================
            string nombre = txtNombre.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            string ruc = txtRuc.Text.Trim();

            if (proveedorDAO.ExisteNombreEditar(nombre, idProveedorActual))
            {
                MessageBox.Show("Ya existe otro proveedor registrado con este mismo nombre. Ingrese uno diferente.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (proveedorDAO.ExisteTelefonoEditar(telefono, idProveedorActual))
            {
                MessageBox.Show("El número de teléfono ingresado ya pertenece a otro proveedor registrado.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return;
            }

            if (proveedorDAO.ExisteCorreoEditar(correo, idProveedorActual))
            {
                MessageBox.Show("El correo electrónico ingresado ya se encuentra registrado con otro proveedor.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return;
            }

            if (proveedorDAO.ExisteRucEditar(ruc, idProveedorActual))
            {
                MessageBox.Show("El número de RUC ingresado ya está asignado a otro proveedor.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRuc.Focus();
                return;
            }

            // =========================================================
            // 4. CREAR OBJETO Y ACTUALIZAR EN BD
            // =========================================================
            ClaseProveedor proveedor = new ClaseProveedor
            {
                IdProveedor = idProveedorActual,
                Nombre = nombre,
                Telefono = telefono,
                Correo = correo,
                Direccion = txtDireccion.Text.Trim(),
                Ruc = ruc,
                EstadoProveedor = cmbEstado.SelectedItem.ToString() == "Activo"
            };

            bool actualizado = proveedorDAO.ActualizarProveedor(proveedor);

            if (actualizado)
            {
                MessageBox.Show("Proveedor actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarProveedores();
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el proveedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
