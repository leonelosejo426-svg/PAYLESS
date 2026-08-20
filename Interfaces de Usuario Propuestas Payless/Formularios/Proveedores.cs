
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


namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Proveedores : Form
    {
        ProveedorDAO proveedorDAO = new ProveedorDAO();

        private DataTable tablaProveedores;

        public Proveedores()
        {
            InitializeComponent();

            ///ConfigurarDataGridView();
           // ConfigurarComboBuscar();

           // CargarProveedores();


        }

       

        private void ConfigurarComboBuscar()
        {

        }

        private void Proveedores_Load(object sender, EventArgs e)
        {
            cmbBuscar.Items.Clear();


            cmbBuscar.Items.Add("Nombre");
            cmbBuscar.Items.Add("Dirección");
            cmbBuscar.Items.Add("RUC");


            cmbBuscar.SelectedIndex = 0;


            CargarProveedores();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click_1(object sender, EventArgs e)
        {

        }

        private void label17_Click_1(object sender, EventArgs e)
        {

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
            try
            {
                DataTable tabla = proveedorDAO.MostrarProveedores();


                DGVtabla1.DataSource = tabla;


                AjustarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar proveedores:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void AjustarColumnas()
        {
            if (DGVtabla1.Columns.Count >= 5)
            {
                DGVtabla1.Columns["id_proveedor"].Width = 80;
                DGVtabla1.Columns["nombre"].Width = 200;
                DGVtabla1.Columns["direccion"].Width = 250;
                DGVtabla1.Columns["ruc"].Width = 150;
                DGVtabla1.Columns["estado_proveedor"].Width = 100;
               
            }
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
            if (DGVtabla1.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione un proveedor.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);


                return;
            }


            int idProveedor =
                Convert.ToInt32(
                    DGVtabla1.CurrentRow.Cells["id_proveedor"].Value);


            string nombre =
                DGVtabla1.CurrentRow.Cells["nombre"].Value.ToString();


            DialogResult respuesta =
                MessageBox.Show(
                    "¿Está seguro de eliminar al proveedor:\n\n" +
                    nombre + "?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);


            if (respuesta == DialogResult.Yes)
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


            string valor = cmbBuscar.Text.Trim();


            if (string.IsNullOrWhiteSpace(valor))
            {
                CargarProveedores();
                return;
            }


            string campo = "";


            switch (cmbBuscar.Text)
            {
                case "Nombre":
                    campo = "nombre";
                    break;


                case "Dirección":
                    campo = "direccion";
                    break;


                case "RUC":
                    campo = "ruc";
                    break;
            }


            DataTable resultado =
                proveedorDAO.BuscarProveedores(campo, valor);


            DGVtabla1.DataSource = resultado;


            AjustarColumnas();
        }


    }
}
 




    



