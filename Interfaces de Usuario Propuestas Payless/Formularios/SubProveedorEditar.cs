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

            // ---------------------------------------------
            // Verificar que primero se haya buscado
            // ---------------------------------------------

            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show(
                    "Primero busque un proveedor.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // 1. Validar longitud exacta (por ejemplo, exactamente 100 caracteres)
            if (txtCorreo.Text.Trim().Length != 100)
            {
                MessageBox.Show(
                    "El campo debe contener exactamente 100 caracteres.",
                    "Validación de Longitud",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtCorreo.Focus();
                return;
            }
            // 1. Validar longitud exacta (por ejemplo, exactamente 200 caracteres)
            if (txtDireccion.Text.Trim().Length != 200)
            {
                MessageBox.Show(
                    "El campo debe contener exactamente 200 caracteres.",
                    "Validación de Longitud",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtDireccion.Focus();
                return;
            }
            // 1. Validar longitud exacta (por ejemplo, exactamente 100 caracteres)
            if (txtNombre.Text.Trim().Length != 100)
            {
                MessageBox.Show(
                    "El campo debe contener exactamente 100 caracteres.",
                    "Validación de Longitud",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtNombre.Focus();
                return;
            }


            // ---------------------------------------------
            // Validar nombre
            // ---------------------------------------------

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show(
                    "Ingrese el nombre del proveedor.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNombre.Focus();
                return;
            }

            // ---------------------------------------------
            // Validar estado
            // ---------------------------------------------

            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione el estado del proveedor.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbEstado.Focus();
                return;
            }

            // ---------------------------------------------
            // Crear objeto proveedor
            // ---------------------------------------------

            ClaseProveedor proveedor =
                new ClaseProveedor();

            proveedor.IdProveedor =
                Convert.ToInt32(
                    txtCodigo.Text);

            proveedor.Nombre =
                txtNombre.Text.Trim();

            proveedor.Telefono =
                txtTelefono.Text.Trim();

            proveedor.Correo =
                txtCorreo.Text.Trim();

            proveedor.Direccion =
                txtDireccion.Text.Trim();

            proveedor.Ruc =
                txtRuc.Text.Trim();

            proveedor.EstadoProveedor =
                cmbEstado.SelectedItem.ToString()
                == "Activo";

            // ---------------------------------------------
            // Actualizar
            // ---------------------------------------------

            // ---------------------------------------------
            // Validar nombre
            // ---------------------------------------------
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (txtNombre.Text.Length < 3)
            {
                MessageBox.Show("El nombre del proveedor debe tener al menos 3 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            // ---------------------------------------------
            // Validar teléfono (Exactamente 8 dígitos)
            // ---------------------------------------------
            if (txtTelefono.Text.Length != 8)
            {
                MessageBox.Show("El número de teléfono debe tener exactamente 8 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return;
            }

            // ---------------------------------------------
            // Validar correo electrónico
            // ---------------------------------------------
            if (string.IsNullOrWhiteSpace(txtCorreo.Text) || !Regex.IsMatch(txtCorreo.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Ingrese un correo electrónico válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return;
            }

            // ---------------------------------------------
            // Validar dirección
            // ---------------------------------------------
            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("Ingrese la dirección del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccion.Focus();
                return;
            }

            // ---------------------------------------------
            // Validar RUC de Nicaragua (14 caracteres)
            // ---------------------------------------------
            Regex regexRuc = new Regex(@"^[A-Z0-9\-]{14}$");
            if (!regexRuc.IsMatch(txtRuc.Text))
            {
                MessageBox.Show("El RUC de Nicaragua debe tener un formato válido de 14 caracteres (ej. J1310000252297).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRuc.Focus();
                return;
            }

            // ---------------------------------------------
            // Validar estado
            // ---------------------------------------------
            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el estado del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEstado.Focus();
                return;
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
