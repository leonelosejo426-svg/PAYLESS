
using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using Newtonsoft.Json;
using Npgsql;
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

            
        }

        private void label9_Click(object sender, EventArgs e)
        {
            
        }

        private void label10_Click(object sender, EventArgs e)
        {
            
        }

        private void label11_Click(object sender, EventArgs e)
        {
        }

        private void label12_Click(object sender, EventArgs e)
        {
        }

        private void label19_Click(object sender, EventArgs e)
        {
            
        }

        private void label23_Click(object sender, EventArgs e)
        {
          
        }

        private void label24_Click(object sender, EventArgs e)
        {
           
        }

        private void label25_Click(object sender, EventArgs e)
        {
            
        }

        private void label27_Click(object sender, EventArgs e)
        {
            
        }

        private void label28_Click(object sender, EventArgs e)
        {
           
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
            DataTable tabla = proveedorDAO.MostrarProveedores();

            // Cargar proveedores en el ComboBox
            cmbBuscar.DataSource = null;
            cmbBuscar.DataSource = tabla;
            cmbBuscar.DisplayMember = "nombre";
            cmbBuscar.ValueMember = "id_proveedor";
            cmbBuscar.SelectedIndex = -1;

            // Cargar proveedores en el DataGridView
            dgvProveedores.DataSource = null;
            dgvProveedores.DataSource = tabla;
        }

        private void label30_Click(object sender, EventArgs e)
        {
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
                MessageBox.Show(
                    "Seleccione un proveedor para eliminar.",
                    "Eliminar proveedor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (dgvProveedores.CurrentRow.Cells["id_proveedor"].Value == null)
            {
                MessageBox.Show(
                    "No se pudo obtener el proveedor seleccionado.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            int idProveedor = Convert.ToInt32(
                dgvProveedores.CurrentRow.Cells["id_proveedor"].Value);

            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de eliminar este proveedor?",
                "Eliminar proveedor",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                bool eliminado =
                    proveedorDAO.EliminarProveedor(idProveedor);

                if (eliminado)
                {
                    MessageBox.Show(
                        "Proveedor eliminado correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CargarProveedores();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo eliminar el proveedor.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                }

            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            // Verificar que haya seleccionado un proveedor
            if (cmbBuscar.SelectedIndex == -1 ||
                cmbBuscar.SelectedValue == null)
            {
                MessageBox.Show(
                    "Seleccione un proveedor.",
                    "Búsqueda",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbBuscar.Focus();
                return;
            }

            try
            {
                // Obtener el nombre del proveedor seleccionado
                string nombreProveedor = cmbBuscar.Text.Trim();

                if (string.IsNullOrEmpty(nombreProveedor))
                {
                    MessageBox.Show(
                        "Seleccione un proveedor válido.",
                        "Búsqueda",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Buscar por nombre
                DataTable tabla =
                    proveedorDAO.BuscarProveedores(
                        "nombre",
                        nombreProveedor);

                // Mostrar resultados
                dgvProveedores.DataSource = null;
                dgvProveedores.DataSource = tabla;

                // Verificar si encontró resultados
                if (tabla.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontraron proveedores.",
                        "Búsqueda",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al buscar el proveedor:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }


        }
    }    
}



    



