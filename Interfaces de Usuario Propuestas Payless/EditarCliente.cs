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
            DataTable tabla = clienteDAO.MostrarClientes();

            CBclientes.DataSource = tabla;
            CBclientes.DisplayMember = "nombre";
            CBclientes.ValueMember = "id_cliente";
            CBclientes.SelectedIndex = -1;


            CBestado.Items.Add("Activo");
            CBestado.Items.Add("Inactivo");
            CBestado.SelectedIndex = -1;
            //El codigo no puede editarse
            txtCodigo.ReadOnly = true;
            //No permitir escribir en el combobox
            CBestado.DropDownStyle = ComboBoxStyle.DropDownList;
            CBclientes.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        //Seleccionar un cliente
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Verificar que exista el cliente
            if (CBclientes.SelectedValue != null)
            {
                idClienteSeleccionado = Convert.ToInt32(CBclientes.SelectedValue);

                //obtener los datos desde la base de datos
                ClaseCliente cliente = clienteDAO.ObtenerCliente(idClienteSeleccionado);

                //Llenar todos los campos del formulario
                txtCodigo.Text = cliente.Codigo;
                txtNombre.Text = cliente.Nombre;
                txtCedula.Text = cliente.Telefono;
                txtDireccion.Text = cliente.Direccion;

                if (cliente.Estado == true)
                {
                    CBestado.SelectedItem = "Activo";
                }
                else
                {
                    CBestado.SelectedItem = "Inactivo";
                }
            }

               
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
            //Id del cliente seleccionado
            cliente.IdCliente = idClienteSeleccionado;
            //No se modifica el codigo
            cliente.Codigo = txtCodigo.Text.Trim();

            //Campos que si se pueden editar 
            cliente.Nombre = txtNombre.Text.Trim();
            cliente.Cedula = txtCedula.Text.Trim();
            cliente.Telefono = txtTelefono.Text.Trim();
            cliente.Direccion = txtDireccion.Text.Trim();
            cliente.Estado = CBestado.Text == "Activo";

            //Guardar cambios en la base de datos
            bool resultado = clienteDAO.EditarCliente(cliente);
            if (resultado)
            {
                MessageBox.Show("Cliente actualizado correctamente");

                CBclientes.DataSource = clienteDAO.MostrarClientes();
                CBclientes.DisplayMember = "nombre";
                CBclientes.ValueMember = "id_cliente";

                //Limpiar campos
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

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Solo permite escribir numeros
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

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
    }

}
