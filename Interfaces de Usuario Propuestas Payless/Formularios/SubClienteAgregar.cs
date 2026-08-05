using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Interfaces_de_Usuario_Propuestas_Payless.Cliente;

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
            // Validar Código
            if (string.IsNullOrWhiteSpace(txtcodigo.Text))
            {
                MessageBox.Show("Ingrese el código del cliente.");
                txtcodigo.Focus();
                return;
            }

            // Validar Nombre
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre del cliente.");
                txtNombre.Focus();
                return;
            }

            // Validar Cédula
            if (string.IsNullOrWhiteSpace(txtcedula.Text))
            {
                MessageBox.Show("Ingrese el número de cédula.");
                txtcedula.Focus();
                return;
            }

            // Validar Teléfono
            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("Ingrese el teléfono.");
                txtTelefono.Focus();
                return;
            }

            // Validar Estado
            if (string.IsNullOrWhiteSpace(CBestado.Text))
            {
                MessageBox.Show("Seleccione el estado del cliente.");
                CBestado.Focus();
                return;
            }

            // Crear objeto Cliente
            cliente nuevoCliente = new cliente();

            nuevoCliente.Codigo = txtcodigo.Text.Trim();
            nuevoCliente.Nombre = txtNombre.Text.Trim();
            nuevoCliente.Cedula = txtcedula.Text.Trim();
            nuevoCliente.Telefono = txtTelefono.Text.Trim();
            nuevoCliente.Estado = CBestado.Text.Trim();

            // Crear DAO
            ClienteDAO clienteDAO = new ClienteDAO();

            // Llamar al método AgregarCliente
            if (clienteDAO.AgregarCliente(nuevoCliente))
            {
                MessageBox.Show("Cliente agregado correctamente.");
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el cliente.");
            }
        }
    }
}
