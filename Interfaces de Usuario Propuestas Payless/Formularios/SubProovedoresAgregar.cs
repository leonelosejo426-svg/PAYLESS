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
            this.Close();
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
            // VALIDAR CAMPOS VACÍOS Y LONGITUDES
            // =====================================================
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese el nombre del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            if (string.IsNullOrWhiteSpace(telefono))
            {
                MessageBox.Show("Ingrese el teléfono del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return;
            }

            if (telefono.Length != 8)
            {
                MessageBox.Show("El número de teléfono debe tener exactamente 8 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(correo))
            {
                MessageBox.Show("Ingrese el correo del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return;
            }

            if (!Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Ingrese un correo electrónico válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return;
            }

            if (correo.Length > 50)
            {
                MessageBox.Show("El campo [correo] no puede superar los [50] caracteres.", "Validación de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return;
            }

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

            if (string.IsNullOrWhiteSpace(ruc))
            {
                MessageBox.Show("Ingrese el código RUC del proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRUC.Focus();
                return;
            }

            Regex regexRuc = new Regex(@"^[A-Z0-9\-]{14}$");
            if (!regexRuc.IsMatch(ruc))
            {
                MessageBox.Show("El RUC de Nicaragua debe tener un formato válido de 14 caracteres (ej. J1310000252297).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRUC.Focus();
                return;
            }

            // =====================================================
            // VALIDAR DUPLICADOS EN LA BASE DE DATOS (NUEVO REGISTRO)
            // =====================================================
            if (proveedorDAO.ExisteNombre(nombre))
            {
                MessageBox.Show("Ya existe un proveedor registrado con este mismo nombre. Ingrese uno diferente.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (proveedorDAO.ExisteTelefono(telefono))
            {
                MessageBox.Show("El número de teléfono ingresado ya pertenece a otro proveedor registrado.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return;
            }

            if (proveedorDAO.ExisteCorreo(correo))
            {
                MessageBox.Show("El correo electrónico ingresado ya se encuentra registrado con otro proveedor.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return;
            }

            if (proveedorDAO.ExisteRuc(ruc))
            {
                MessageBox.Show("El número de RUC ingresado ya está asignado a otro proveedor.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRUC.Focus();
                return;
            }

            // =====================================================
            // CREAR OBJETO PROVEEDOR Y GUARDAR (Estado activo por defecto)
            // =====================================================
            ClaseProveedor proveedor = new ClaseProveedor();
            proveedor.Nombre = nombre;
            proveedor.Telefono = telefono;
            proveedor.Correo = correo;
            proveedor.Direccion = direccion;
            proveedor.Ruc = ruc;
            proveedor.EstadoProveedor = true; // Activo por defecto

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
            txtCorreo.MaxLength = 50;
            txtDireccion.MaxLength = 200;
            txtNombre.MaxLength = 50;
            txtTelefono.MaxLength = 8;
            txtRUC.MaxLength = 14;
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
