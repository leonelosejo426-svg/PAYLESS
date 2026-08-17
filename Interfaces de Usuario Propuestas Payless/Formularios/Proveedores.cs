
using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Interfaces_de_Usuario_Propuestas_Payless.ClaseProveedor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Proveedores : Form
    {
        ProveedorDAO proveedorDAO = new ProveedorDAO();
        public Proveedores()
        {
            InitializeComponent();
            CargarProveedores();

        }

        private void Proveedores_Load(object sender, EventArgs e)
        {





        }

        private void label15_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();

            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();

            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();

            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();

            this.Hide();
        }

        private void label20_Click_1(object sender, EventArgs e)
        {


            new Cliente().Show();
            this.Hide();
        }

        private void label17_Click_1(object sender, EventArgs e)
        {


            new Usuario().Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click_1(object sender, EventArgs e)
        {

            new Productos().Show();
            this.Hide();

        }

        private void label9_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();
            this.Hide();
        }

        private void label23_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo();
            ventana.Show();
            this.Hide();
        }

        private void label24_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();
        }

        private void label25_Click(object sender, EventArgs e)
        {
            inventario ventana = new inventario();
            ventana.Show(); this.Hide();
        }

        private void label27_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito();
            ventana.Show();
            this.Hide();
        }

        private void label28_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {



        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {


        }

        private void btnCargar_Click(object sender, EventArgs e)
        {

        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {

        }

        private void CargarProveedores()
        {
            DataTable table = proveedorDAO.MostrarProveedores();
            dgvProveedores.DataSource = table;
        }

        private void label30_Click(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento();
            ventana.Show();
            this.Hide();
        }

        private void dgvProveedores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (dgvProveedores.CurrentRow == null)

            {

                MessageBox.Show("Seleccione un proveedor para eliminar.");

                return;

            }

            int idProveedor = Convert.ToInt32(

                dgvProveedores.CurrentRow.Cells["id_proveedor"].Value

            );

            DialogResult resultado = MessageBox.Show(

                "¿Está seguro de eliminar este proveedor?",

                "Eliminar proveedor",

                MessageBoxButtons.YesNo,

                MessageBoxIcon.Question

            );

            if (resultado == DialogResult.Yes)

            {

                bool eliminado = proveedorDAO.EliminarProveedor(idProveedor);

                if (eliminado)

                {

                    MessageBox.Show("Proveedor eliminado correctamente.");

                    CargarProveedores();

                }

                else

                {

                    MessageBox.Show("No se pudo eliminar el proveedor.");

                }

            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string valor = cmbBuscar.Text.Trim();

            if (string.IsNullOrEmpty(valor))

            {

                CargarProveedores();

                return;

            }

            string campo = "";

            switch (cmbBuscar.SelectedIndex)

            {

                case 0:

                    campo = "nombre";

                    break;

                case 1:

                    campo = "telefono";

                    break;

                case 2:

                    campo = "correo";

                    break;

                case 3:

                    campo = "direccion";

                    break;

                case 4:

                    campo = "ruc";

                    break;

                default:

                    MessageBox.Show("Seleccione un criterio de búsqueda.");

                    return;

            }

            DataTable tabla =

                proveedorDAO.BuscarProveedores(campo, valor);

            dgvProveedores.DataSource = tabla;

        }
    }
}


    



