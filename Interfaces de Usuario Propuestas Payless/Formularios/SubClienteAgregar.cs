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
    public partial class SubClienteAgregar : Form
    {
        public SubClienteAgregar()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(txtcodigo.Text))
            {
                MessageBox.Show("Ingrese el código del cliente.");
                txtcodigo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(q.Text))
            {
                MessageBox.Show("Ingrese el nombre del cliente.");
                q.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtcedula.Text))
            {
                MessageBox.Show("Ingrese el número de cédula.");
                txtcedula.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("Ingrese el número de teléfono.");
                txtTelefono.Focus();
                return;
            }

            if (CBestado.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un estado.");
                CBestado.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(richtxtDirreccion.Text))
            {
                MessageBox.Show("Ingrese la dirección.");
                richtxtDirreccion.Focus();
                return;
            }

            // Crear objeto
            ClaseCliente cliente = new ClaseCliente();

            cliente.Codigo = txtcodigo.Text.Trim();
            cliente.Nombre = q.Text.Trim();
            cliente.Cedula = txtcedula.Text.Trim();
            cliente.Telefono = txtTelefono.Text.Trim();
            cliente.Direccion = richtxtDirreccion.Text.Trim();

            // Estado
            cliente.Estado = CBestado.Text == "Activo";

            // Llamar al método
            ClienteDAO dao = new ClienteDAO();

            if (dao.AgregarCliente(cliente))
            {
                MessageBox.Show("Cliente agregado correctamente.");

                // Limpiar controles (opcional)
                txtcodigo.Clear();
                q.Clear();
                txtcedula.Clear();
                txtTelefono.Clear();
                richtxtDirreccion.Clear();
                CBestado.SelectedIndex = -1;
                txtcodigo.Focus();
            }
            else
            {
                MessageBox.Show("No fue posible agregar el cliente.");
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }
    }
}
