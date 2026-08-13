using iTextSharp.text.pdf.qrcode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            DataTable tabla = clienteDAO.MostrarClientes();

            CBclientes.DataSource = tabla;
            CBclientes.DisplayMember = "nombre";
            CBclientes.ValueMember = "id_cliente";
            CBclientes.SelectedIndex = -1;

            
            CBestado.Items.Add("Activo");
            CBestado.Items.Add("Inactivo");
            CBestado.SelectedIndex = -1;

        }

        //Seleccionar un cliente
        private void CBclientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(CBclientes.SelectedIndex != -1)
            {
                idClienteSeleccionado = Convert.ToInt32(CBclientes.SelectedValue);

                ClaseCliente cliente = clienteDAO.ObtenerCliente(idClienteSeleccionado);

                txtcodigo.Text = cliente.Codigo;
                txtNombre.Text = cliente.Nombre;
                txtcedula.Text = cliente.Cedula;
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

        //Actualizar cliente
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (idClienteSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un cliente");
                return;
            }
            ClaseCliente cliente = new ClaseCliente();

            cliente.IdCliente = idClienteSeleccionado;
            cliente.Codigo = txtcodigo.Text.Trim();
            cliente.Nombre = txtNombre.Text.Trim();
            cliente.Cedula = txtcedula.Text.Trim();
            cliente.Telefono = txtTelefono.Text.Trim();
            cliente.Direccion = txtDireccion.Text.Trim();

            //editar estado del cliente 
            cliente.Estado = CBestado.Text == "Activo";

            bool resultado = clienteDAO.EditarCliente(cliente);

            if (resultado)
            {
                MessageBox.Show("Cliente actualizado correctamente");

                //Recargar clientes 

                CBclientes.DataSource = clienteDAO.MostrarClientes();
                CBclientes.DisplayMember = "nombre";
                CBclientes.ValueMember = "id_cliente";

                txtcodigo.Clear();
                txtNombre.Clear();
                txtcedula.Clear();
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtcodigo.Clear();
            txtNombre.Clear();
            txtcedula.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();

            CBestado.SelectedIndex = -1;
            CBestado.SelectedIndex = -1;
            idClienteSeleccionado = 0;
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Solo permite escribir numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Solo permite letras y espacios
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtcodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //solo permite escribir numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            //solo permite escribir letras y espacios
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
    }
}
