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
using static Interfaces_de_Usuario_Propuestas_Payless.Ventas;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class EditarCliente : Form
    {
        ClienteDAO clienteDAO = new ClienteDAO();
        int idClienteSeleccionado = 0;

        public EditarCliente()
        {
            InitializeComponent();
        }
        //Cargar formulario
        private void EditarCliente_Load(object sender, EventArgs e)
        {
            // Cargar clientes guardados
            DataTable tabla = clienteDAO.MostrarClientes();

            CBclientes.DataSource = tabla;
            CBclientes.DisplayMember = "nombre";
            CBclientes.ValueMember = "id_cliente";
            CBclientes.SelectedIndex = -1;

            // Estado
            CBestado.Items.Add("Activo");
            CBestado.Items.Add("Inactivo");
            CBestado.SelectedIndex = -1;

            // No permitir escribir en el ComboBox
            CBestado.DropDownStyle = ComboBoxStyle.DropDownList;

            // El código solo se muestra
            txtCodigo.ReadOnly = true;

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtCedula.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();

            CBclientes.SelectedIndex = -1;
            CBestado.SelectedIndex = -1;

            idClienteSeleccionado = 0;
        }

        //Actualizar cliente
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (idClienteSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un cliente");
                return;
            }

            ClaseCliente cliente = new ClaseCliente();

            // Cliente seleccionado
            cliente.IdCliente = idClienteSeleccionado;

            // El código no se modifica
            cliente.Codigo = txtCodigo.Text.Trim();

            // Campos editables
            cliente.Nombre = txtNombre.Text.Trim();
            cliente.Cedula = txtCedula.Text.Trim();
            cliente.Telefono = txtTelefono.Text.Trim();
            cliente.Direccion = txtDireccion.Text.Trim();


            // Estado
            cliente.Estado = CBestado.Text == "Activo";

            bool resultado = clienteDAO.EditarCliente(cliente);

            if (resultado)
            {
                MessageBox.Show("Cliente actualizado correctamente");

                // Recargar clientes
                DataTable tabla = clienteDAO.MostrarClientes();

                CBclientes.DataSource = tabla;
                CBclientes.DisplayMember = "nombre";
                CBclientes.ValueMember = "id_cliente";

                // Limpiar campos
                txtCodigo.Clear();
                txtNombre.Clear();
                txtCedula.Clear();
                txtTelefono.Clear();
                txtDireccion.Clear();

                CBclientes.SelectedIndex = -1;
                CBestado.SelectedIndex = -1;

                idClienteSeleccionado = 0;
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el cliente");
            }

        }

        //Solo permite numeros
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
               !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        //Solo permite letras
        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
               !char.IsLetter(e.KeyChar) &&
               e.KeyChar != ' ')
            {
                e.Handled = true;

            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
              !char.IsLetter(e.KeyChar) &&
              e.KeyChar != ' ')
            {
                e.Handled = true;

            }
        }

        //Seleccionar un cliente
        private void CBclientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBclientes.SelectedIndex != -1)
            {


                idClienteSeleccionado =
                    Convert.ToInt32(CBclientes.SelectedValue.ToString());

                ClaseCliente cliente =
                    clienteDAO.ObtenerCliente(idClienteSeleccionado);

                // Llenar campos con datos de la base de datos
                txtCodigo.Text = cliente.Codigo;
                txtNombre.Text = cliente.Nombre;
                txtCedula.Text = cliente.Cedula;
                txtTelefono.Text = cliente.Telefono;
                txtDireccion.Text = cliente.Direccion;

                if (cliente.Estado)
                {
                    CBestado.SelectedItem = "Activo";
                }
                else
                {
                    CBestado.SelectedItem = "Inactivo";
                }
            }

        }

    }
}
