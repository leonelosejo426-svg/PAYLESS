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
    public partial class SubClienteAgregar : Form
    {
        public SubClienteAgregar()
        {
            InitializeComponent();
            txtTelefono.KeyPress += txtTelefono_KeyPress;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombreCliente = q.Text.Trim();
            string cedula = txtcedula.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string direccion = richtxtDirreccion.Text.Trim();

            // =========================================================================
            // 1. VALIDACIONES DE CAMPOS VACÍOS, FORMATO Y LONGITUD (EN ORDEN)
            // =========================================================================

            // 1.1 Validar Nombre del Cliente
            if (string.IsNullOrWhiteSpace(nombreCliente))
            {
                MessageBox.Show("Ingrese el nombre del cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                q.Focus();
                return;
            }

            if (nombreCliente.Length > 100)
            {
                MessageBox.Show(
                    "El nombre del cliente no puede superar los 100 caracteres.",
                    "Validación de Longitud",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                q.Focus();
                return;
            }

            Regex regexNombre = new Regex(@"^([A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)(\s[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)+$");
            if (!regexNombre.IsMatch(nombreCliente))
            {
                MessageBox.Show("El nombre completo debe tener al menos un nombre y un apellido, y cada palabra debe empezar con mayúscula.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                q.Focus();
                return;
            }

            // 1.2 Validar Cédula (Formato estricto con validación lógica de fecha: DDMMYY)
            if (string.IsNullOrWhiteSpace(cedula))
            {
                MessageBox.Show("Ingrese el número de cédula.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcedula.Focus();
                return;
            }

            // Expresión regular base: 3 dígitos - 6 dígitos (fecha) - 4 dígitos numéricos y 1 letra Mayúscula
            Regex regexCedula = new Regex(@"^\d{3}-\d{6}-\d{4}[A-Z]$");
            if (!regexCedula.IsMatch(cedula))
            {
                MessageBox.Show("La cédula debe cumplir con el formato correcto, por ejemplo: 441-241207-1002B", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcedula.Focus();
                txtcedula.SelectAll();
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
                txtcedula.Focus();
                txtcedula.SelectAll();
                return;
            }

            // 1.3 Validar Teléfono (Exactamente 8 dígitos numéricos)
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

            // 1.4 Validar Dirección
            if (string.IsNullOrWhiteSpace(direccion))
            {
                MessageBox.Show("Ingrese la dirección.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                richtxtDirreccion.Focus();
                return;
            }

            if (direccion.Length > 150)
            {
                MessageBox.Show(
                    "El campo dirección no puede superar los 150 caracteres.",
                    "Validación de Longitud",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                richtxtDirreccion.Focus();
                return;
            }

            // =========================================================================
            // 2. VALIDAR DUPLICADOS EN LA BASE DE DATOS
            // =========================================================================
            ClienteDAO daoVerificar = new ClienteDAO();

            if (daoVerificar.ExisteCedula(cedula))
            {
                MessageBox.Show("El número de cédula ingresado ya pertenece a otro cliente.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcedula.Focus();
                txtcedula.SelectAll();
                return;
            }

            if (daoVerificar.ExisteTelefono(telefono))
            {
                MessageBox.Show("El número de teléfono ingresado ya se encuentra registrado.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                txtTelefono.SelectAll();
                return;
            }

            // =========================================================================
            // 3. GUARDAR CLIENTE EN LA BASE DE DATOS
            // =========================================================================
            ClaseCliente cliente = new ClaseCliente();
            cliente.Nombre = nombreCliente;
            cliente.Cedula = cedula;
            cliente.Telefono = telefono;
            cliente.Direccion = direccion;

            ClienteDAO dao = new ClienteDAO();

            if (dao.AgregarCliente(cliente))
            {
                MessageBox.Show(
                    "Cliente agregado correctamente.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Limpiar campos
                q.Clear();
                txtcedula.Clear();
                txtTelefono.Clear();
                richtxtDirreccion.Clear();

                q.Focus();
            }
            else
            {
                MessageBox.Show(
                    "No fue posible agregar el cliente.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SubClienteAgregar_Load(object sender, EventArgs e)
        {
            txtTelefono.MaxLength = 8; // Limitar el número de caracteres a 8
            txtcedula.MaxLength = 16; // Limitar el número de caracteres a 16
            richtxtDirreccion.MaxLength = 150; // Limitar el número de caracteres a 150
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
