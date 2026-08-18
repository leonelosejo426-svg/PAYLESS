
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

            ConfigurarDataGridView();
            ConfigurarComboBuscar();

            MostrarProveedores();


        }

        private void ConfigurarDataGridView()
        {
            // No crear columnas automáticamente
            dgvProveedores.AutoGenerateColumns = false;

            // No permitir editar directamente
            dgvProveedores.ReadOnly = true;

            // Seleccionar fila completa
            dgvProveedores.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            // Una sola fila seleccionada
            dgvProveedores.MultiSelect = false;

            // No permitir agregar filas manualmente
            dgvProveedores.AllowUserToAddRows = false;

            // Fuente
            dgvProveedores.Font =
                new Font("Times New Roman", 12);

            dgvProveedores.ColumnHeadersDefaultCellStyle.Font =
                new Font("Times New Roman", 12);

            // Limpiar columnas existentes
            dgvProveedores.Columns.Clear();


            // =====================================================
            // ID
            // =====================================================

            DataGridViewTextBoxColumn columnaID =
                new DataGridViewTextBoxColumn();

            columnaID.Name = "colID";
            columnaID.HeaderText = "Código";
            columnaID.DataPropertyName = "id_proveedor";
            columnaID.ReadOnly = true;
            columnaID.Width = 90;

            dgvProveedores.Columns.Add(columnaID);


            // =====================================================
            // NOMBRE
            // =====================================================

            DataGridViewTextBoxColumn columnaNombre =
                new DataGridViewTextBoxColumn();

            columnaNombre.Name = "colNombre";
            columnaNombre.HeaderText = "Nombre";
            columnaNombre.DataPropertyName = "nombre";
            columnaNombre.ReadOnly = true;
            columnaNombre.Width = 200;

            dgvProveedores.Columns.Add(columnaNombre);


            // =====================================================
            // TELÉFONO
            // =====================================================

            DataGridViewTextBoxColumn columnaTelefono =
                new DataGridViewTextBoxColumn();

            columnaTelefono.Name = "colTelefono";
            columnaTelefono.HeaderText = "Teléfono";
            columnaTelefono.DataPropertyName = "telefono";
            columnaTelefono.ReadOnly = true;
            columnaTelefono.Width = 120;

            dgvProveedores.Columns.Add(columnaTelefono);


            // =====================================================
            // CORREO
            // =====================================================

            DataGridViewTextBoxColumn columnaCorreo =
                new DataGridViewTextBoxColumn();

            columnaCorreo.Name = "colCorreo";
            columnaCorreo.HeaderText = "Correo";
            columnaCorreo.DataPropertyName = "correo";
            columnaCorreo.ReadOnly = true;
            columnaCorreo.Width = 200;

            dgvProveedores.Columns.Add(columnaCorreo);


            // =====================================================
            // DIRECCIÓN
            // =====================================================

            DataGridViewTextBoxColumn columnaDireccion =
                new DataGridViewTextBoxColumn();

            columnaDireccion.Name = "colDireccion";
            columnaDireccion.HeaderText = "Dirección";
            columnaDireccion.DataPropertyName = "direccion";
            columnaDireccion.ReadOnly = true;
            columnaDireccion.Width = 220;

            dgvProveedores.Columns.Add(columnaDireccion);


            // =====================================================
            // RUC
            // =====================================================

            DataGridViewTextBoxColumn columnaRuc =
                new DataGridViewTextBoxColumn();

            columnaRuc.Name = "colRuc";
            columnaRuc.HeaderText = "RUC";
            columnaRuc.DataPropertyName = "ruc";
            columnaRuc.ReadOnly = true;
            columnaRuc.Width = 140;

            dgvProveedores.Columns.Add(columnaRuc);


            // =====================================================
            // ESTADO
            // =====================================================

            DataGridViewTextBoxColumn columnaEstado =
                new DataGridViewTextBoxColumn();

            columnaEstado.Name = "colEstado";
            columnaEstado.HeaderText = "Estado";
            columnaEstado.DataPropertyName = "estado";
            columnaEstado.ReadOnly = true;
            columnaEstado.Width = 100;

            dgvProveedores.Columns.Add(columnaEstado);


            // =====================================================
            // FECHA DE REGISTRO
            // =====================================================

            DataGridViewTextBoxColumn columnaFecha =
                new DataGridViewTextBoxColumn();

            columnaFecha.Name = "colFecha";
            columnaFecha.HeaderText = "Fecha de registro";
            columnaFecha.DataPropertyName = "fecha_registro";
            columnaFecha.ReadOnly = true;
            columnaFecha.Width = 150;

            dgvProveedores.Columns.Add(columnaFecha);
        }

        private void ConfigurarComboBuscar()
        {
            cmbBuscar.DataSource = null;
            cmbBuscar.Items.Clear();
        }

        private void Proveedores_Load(object sender, EventArgs e)
        {

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

        private void MostrarProveedores()
        {
            try
            {
                tablaProveedores =
                    proveedorDAO.MostrarProveedores();

                if (tablaProveedores == null)
                {
                    MessageBox.Show(
                        "No se pudieron cargar los proveedores.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // =====================================================
                // MOSTRAR EN DATAGRIDVIEW
                // =====================================================

                dgvProveedores.DataSource = null;
                dgvProveedores.DataSource = tablaProveedores;


                // =====================================================
                // MOSTRAR EN COMBOBOX
                // =====================================================

                cmbBuscar.DataSource = null;

                if (tablaProveedores.Rows.Count > 0)
                {
                    cmbBuscar.DisplayMember = "nombre";
                    cmbBuscar.ValueMember = "id_proveedor";
                    cmbBuscar.DataSource = tablaProveedores;
                    cmbBuscar.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los proveedores:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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

            if (dgvProveedores.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione un proveedor para eliminar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (dgvProveedores.CurrentRow.Cells["colID"].Value == null)
            {
                MessageBox.Show(
                    "No se pudo obtener el proveedor seleccionado.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            int idProveedor =
                Convert.ToInt32(
                    dgvProveedores.CurrentRow
                    .Cells["colID"]
                    .Value);

            DialogResult respuesta =
                MessageBox.Show(
                    "¿Está seguro de eliminar este proveedor?",
                    "Eliminar proveedor",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            try
            {
                bool eliminado =
                    proveedorDAO.EliminarProveedor(idProveedor);

                if (eliminado)
                {
                    MessageBox.Show(
                        "Proveedor eliminado correctamente.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    MostrarProveedores();
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
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al eliminar el proveedor:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }
    


        private void btnBuscar_Click(object sender, EventArgs e)
        {


            if (cmbBuscar.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione un proveedor.",
                    "Búsqueda",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                string nombreProveedor =
                    cmbBuscar.Text.Trim();

                if (string.IsNullOrWhiteSpace(nombreProveedor))
                {
                    MessageBox.Show(
                        "Seleccione un proveedor válido.",
                        "Búsqueda",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DataTable resultado =
                    proveedorDAO.BuscarProveedores(
                        "nombre",
                        nombreProveedor);

                dgvProveedores.DataSource = null;
                dgvProveedores.DataSource = resultado;

                if (resultado.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontraron proveedores.",
                        "Resultado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }


        }
    }    
}



    



