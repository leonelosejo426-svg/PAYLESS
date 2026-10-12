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
    public partial class SubProovedoresAgregar : Form
    {
        private ProveedorDAO proveedorDAO = new ProveedorDAO();
        public SubProovedoresAgregar()
        {
            InitializeComponent();
            txtTelefono.KeyPress += txtTelefono_KeyPress;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Hide();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Quitar espacios innecesarios
            string nombre = txtNombre.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            string direccion = txtDireccion.Text.Trim();
            string ruc = txtRUC.Text.Trim();

            // =====================================================
            // VALIDAR CAMPOS VACÍOS
            // =====================================================

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese el nombre del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(telefono))
            {
                MessageBox.Show("Ingrese el teléfono del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(correo))
            {
                MessageBox.Show("Ingrese el correo del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(direccion))
            {
                MessageBox.Show("Ingrese la dirección del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccion.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(ruc))
            {
                MessageBox.Show("Ingrese el código RUC del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRUC.Focus();
                return;
            }

            // =====================================================
            // VALIDAR NOMBRE
            // =====================================================

            if (nombre.Length < 3)
            {
                MessageBox.Show("El nombre del proveedor debe tener al menos 3 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            // =====================================================
            // VALIDAR TELÉFONO (Exactamente 8 dígitos)
            // =====================================================

            if (telefono.Length != 8)
            {
                MessageBox.Show("El número de teléfono debe tener exactamente 8 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return;
            }

            // =====================================================
            // VALIDAR CORREO
            // =====================================================

            if (!Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Ingrese un correo electrónico válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return;
            }

            // =====================================================
            // VALIDAR RUC DE NICARAGUA (14 caracteres)
            // =====================================================

            Regex regexRuc = new Regex(@"^[A-Z0-9\-]{14}$");
            if (!regexRuc.IsMatch(ruc))
            {
                MessageBox.Show("El RUC de Nicaragua debe tener un formato válido de 14 caracteres (ej. J1310000252297).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRUC.Focus();
                return;
            }

            // =====================================================
            // CREAR OBJETO PROVEEDOR
            // =====================================================

            ClaseProveedor proveedor = new ClaseProveedor();

            proveedor.Nombre = nombre;
            proveedor.Telefono = telefono;
            proveedor.Correo = correo;
            proveedor.Direccion = direccion;
            proveedor.Ruc = ruc;
            proveedor.EstadoProveedor = true;

            // =====================================================
            // GUARDAR
            // =====================================================

            bool agregado = proveedorDAO.AgregarProveedor(proveedor);

            if (agregado)
            {
                MessageBox.Show(
                    "Proveedor agregado correctamente.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Proveedores ventana = new Proveedores();
                ventana.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo agregar el proveedor. Verifique los datos e intente nuevamente.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SubProovedoresAgregar_Load(object sender, EventArgs e)
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
