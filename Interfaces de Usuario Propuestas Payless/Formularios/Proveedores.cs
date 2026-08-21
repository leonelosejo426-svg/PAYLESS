
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



            cmbBuscar.Items.Add("Todos");
            cmbBuscar.Items.Add("Activos");
            cmbBuscar.Items.Add("Inactivos");


            cmbBuscar.SelectedIndex = 0;


            CargarProveedores();


            lblCaja.Enabled = false;
            lblProveedores.Enabled = false;
            lblProductos.Enabled = false;
            lblVenta.Enabled = false;
            lblCompras.Enabled = false;
            lblUsuarios.Enabled = false;


            lblCliente.Enabled = false;
            lblCredito.Enabled = false;
            lblInventario.Enabled = false;
            lblMantenimiento.Enabled = false;


            switch (ClaseSesion.RolActual)
            {
                case "Administrador":

                    lblCaja.Enabled = true;
                    lblCompras.Enabled = true;
                    lblVenta.Enabled = true;
                    lblUsuarios.Enabled = true;
                    lblMantenimiento.Enabled = true;
                    lblCliente.Enabled = true;
                    lblCredito.Enabled = true;
                    lblInventario.Enabled = true;
                    lblProveedores.Enabled = true;
                    lblProductos.Enabled = true;


                    break;

                case "Gerente":

                    lblCaja.Enabled = true;
                    lblCompras.Enabled = true;
                    lblVenta.Enabled = true;

                    break;

                case "Cajero":

                    lblCaja.Enabled = true;
                    lblVenta.Enabled = true;

                    break;
            }
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

                if (tabla == null || tabla.Columns.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontraron datos de proveedores.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DGVtabla1.DataSource = tabla;

                // Cambiar el encabezado de la columna estado
                if (DGVtabla1.Columns.Contains("estado"))
                {
                    DGVtabla1.Columns["estado"].HeaderText = "Estado";
                }

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
            if (DGVtabla1.Columns.Contains("id_proveedor"))
                DGVtabla1.Columns["id_proveedor"].Width = 80;

            if (DGVtabla1.Columns.Contains("nombre"))
                DGVtabla1.Columns["nombre"].Width = 200;

            if (DGVtabla1.Columns.Contains("direccion"))
                DGVtabla1.Columns["direccion"].Width = 250;

            if (DGVtabla1.Columns.Contains("ruc"))
                DGVtabla1.Columns["ruc"].Width = 150;

            if (DGVtabla1.Columns.Contains("estado"))
            {
                DGVtabla1.Columns["estado"].Width = 100;
                DGVtabla1.Columns["estado"].HeaderText = "Estado";
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

            try
            {
                DataTable tabla = proveedorDAO.MostrarProveedores();

                if (tabla == null || tabla.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No hay proveedores para mostrar.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                string filtro = cmbBuscar.SelectedItem?.ToString();

                DataTable tablaFiltrada = tabla.Clone();

                foreach (DataRow fila in tabla.Rows)
                {
                    bool estado = Convert.ToBoolean(fila["estado"]);

                    if (filtro == "Activos" && !estado)
                        continue;

                    if (filtro == "Inactivos" && estado)
                        continue;

                    tablaFiltrada.ImportRow(fila);
                }

                DGVtabla1.DataSource = tablaFiltrada;

                // Mostrar Activo/Inactivo sin modificar el tipo de la columna original
                if (DGVtabla1.Columns.Contains("estado"))
                {
                    DGVtabla1.Columns["estado"].HeaderText = "Estado";

                    foreach (DataGridViewRow fila in DGVtabla1.Rows)
                    {
                        if (fila.Cells["estado"].Value != null)
                        {
                            bool estado = Convert.ToBoolean(
                                fila.Cells["estado"].Value);

                            fila.Cells["estado"].Value =
                                estado ? "Activo" : "Inactivo";
                        }
                    }
                }

                AjustarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar proveedores:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

    }
}
 




    



