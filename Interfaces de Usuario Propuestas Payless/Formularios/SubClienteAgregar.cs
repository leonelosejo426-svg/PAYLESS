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

            // Validar nombre (Al menos un nombre y un apellido, cada palabra con mayúscula inicial)
            if (string.IsNullOrWhiteSpace(nombreCliente))
            {
                MessageBox.Show("Ingrese el nombre del cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                q.Focus();
                return;
            }

            // 1. Validar longitud exacta (por ejemplo, exactamente 150 caracteres)
            if (richtxtDirreccion.Text.Trim().Length != 150)
            {
                MessageBox.Show(
                    "El campo debe contener exactamente 150 caracteres.",
                    "Validación de Longitud",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                richtxtDirreccion.Focus();
                return;
            }

            // 1. Validar longitud exacta (por ejemplo, exactamente 100 caracteres)
            if (nombreCliente.Length != 100)
            {
                MessageBox.Show(
                    "El campo debe contener exactamente 100 caracteres.",
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

            // Validar cédula (Formato ejemplo: 441-241207-1002B)
            if (string.IsNullOrWhiteSpace(cedula))
            {
                MessageBox.Show("Ingrese el número de cédula.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcedula.Focus();
                return;
            }

            Regex regexCedula = new Regex(@"^\d{3}-\d{6}-\d{4}[A-Z]$");
            if (!regexCedula.IsMatch(cedula))
            {
                MessageBox.Show("La cédula debe cumplir con el formato correcto, por ejemplo: 441-241207-1002B", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcedula.Focus();
                return;
            }

            // Validar teléfono (Exactamente 8 dígitos)
            if (telefono.Length != 8)
            {
                MessageBox.Show("El número de teléfono debe tener exactamente 8 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return;
            }

           

            // Validar dirección
            if (string.IsNullOrWhiteSpace(direccion))
            {
                MessageBox.Show("Ingrese la dirección.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                richtxtDirreccion.Focus();
                return;
            }

            // Crear objeto cliente
            ClaseCliente cliente = new ClaseCliente();

            cliente.Nombre = nombreCliente;
            cliente.Cedula = cedula;
            cliente.Telefono = telefono;
            cliente.Direccion = direccion;
          

            // Guardar cliente
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
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void SubClienteAgregar_Load(object sender, EventArgs e)
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
