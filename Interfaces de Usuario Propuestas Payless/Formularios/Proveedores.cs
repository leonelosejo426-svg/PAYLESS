
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

            ConfigurarDataGridView();
            ConfigurarComboBuscar();

            MostrarProveedores();


        }

        private void ConfigurarDataGridView()
        {
            dgvProveedores.AutoGenerateColumns = false;
            dgvProveedores.ReadOnly = true;
            dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProveedores.MultiSelect = false;
            dgvProveedores.AllowUserToAddRows = false;

            dgvProveedores.Columns.Clear();

            DataGridViewTextBoxColumn columnaID = new DataGridViewTextBoxColumn();
            columnaID.Name = "colID";
            columnaID.HeaderText = "Código";
            columnaID.DataPropertyName = "id_proveedor";
            columnaID.Width = 90;
            dgvProveedores.Columns.Add(columnaID);

            DataGridViewTextBoxColumn columnaNombre = new DataGridViewTextBoxColumn();
            columnaNombre.Name = "colNombre";
            columnaNombre.HeaderText = "Nombre";
            columnaNombre.DataPropertyName = "nombre";
            columnaNombre.Width = 200;
            dgvProveedores.Columns.Add(columnaNombre);

            DataGridViewTextBoxColumn columnaTelefono = new DataGridViewTextBoxColumn();
            columnaTelefono.Name = "colTelefono";
            columnaTelefono.HeaderText = "Teléfono";
            columnaTelefono.DataPropertyName = "telefono";
            columnaTelefono.Width = 120;
            dgvProveedores.Columns.Add(columnaTelefono);

            DataGridViewTextBoxColumn columnaCorreo = new DataGridViewTextBoxColumn();
            columnaCorreo.Name = "colCorreo";
            columnaCorreo.HeaderText = "Correo";
            columnaCorreo.DataPropertyName = "correo";
            columnaCorreo.Width = 200;
            dgvProveedores.Columns.Add(columnaCorreo);

            DataGridViewTextBoxColumn columnaDireccion = new DataGridViewTextBoxColumn();
            columnaDireccion.Name = "colDireccion";
            columnaDireccion.HeaderText = "Dirección";
            columnaDireccion.DataPropertyName = "direccion";
            columnaDireccion.Width = 220;
            dgvProveedores.Columns.Add(columnaDireccion);

            DataGridViewTextBoxColumn columnaRuc = new DataGridViewTextBoxColumn();
            columnaRuc.Name = "colRuc";
            columnaRuc.HeaderText = "RUC";
            columnaRuc.DataPropertyName = "ruc";
            columnaRuc.Width = 140;
            dgvProveedores.Columns.Add(columnaRuc);

            DataGridViewTextBoxColumn columnaEstado = new DataGridViewTextBoxColumn();
            columnaEstado.Name = "colEstado";
            columnaEstado.HeaderText = "Estado";
            columnaEstado.DataPropertyName = "estado";
            columnaEstado.Width = 100;
            dgvProveedores.Columns.Add(columnaEstado);

            DataGridViewTextBoxColumn columnaFecha = new DataGridViewTextBoxColumn();
            columnaFecha.Name = "colFecha";
            columnaFecha.HeaderText = "Fecha de registro";
            columnaFecha.DataPropertyName = "fecha_registro";
            columnaFecha.Width = 150;
            dgvProveedores.Columns.Add(columnaFecha);
        }

        private void ConfigurarComboBuscar()
        {
            cmbBuscar.DataSource = null;
            cmbBuscar.Items.Clear();

            cmbBuscar.Items.Add("Nombre");
            cmbBuscar.Items.Add("Teléfono");
            cmbBuscar.Items.Add("Correo");
            cmbBuscar.Items.Add("RUC");

            cmbBuscar.SelectedIndex = -1;
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
                tablaProveedores = proveedorDAO.MostrarProveedores();
                MessageBox.Show(
                "Cantidad de proveedores encontrados: " +
                tablaProveedores.Rows.Count.ToString());


                if (tablaProveedores == null)
                {
                    MessageBox.Show(
                        "El DAO devolvió NULL.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                if (tablaProveedores.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "La consulta no devolvió ningún proveedor.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                // Mostrar los datos
                dgvProveedores.DataSource = null;
                dgvProveedores.DataSource = tablaProveedores;

                // Actualizar el DataGridView
                dgvProveedores.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar proveedores:\n\n" +
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
                    "Seleccione qué desea buscar.",
                    "Búsqueda",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string campo = "";

            switch (cmbBuscar.Text)
            {
                case "Nombre":
                    campo = "nombre";
                    break;

                case "Teléfono":
                    campo = "telefono";
                    break;

                case "Correo":
                    campo = "correo";
                    break;

                case "RUC":
                    campo = "ruc";
                    break;
            }

            string valor = "";

            using (Form formularioBuscar = new Form())
            {
                formularioBuscar.Text = "Buscar proveedor";
                formularioBuscar.StartPosition = FormStartPosition.CenterParent;
                formularioBuscar.FormBorderStyle = FormBorderStyle.FixedDialog;
                formularioBuscar.MaximizeBox = false;
                formularioBuscar.MinimizeBox = false;
                formularioBuscar.ClientSize = new Size(350, 130);

                Label etiqueta = new Label();
                etiqueta.Text = "Ingrese el valor que desea buscar:";
                etiqueta.AutoSize = true;
                etiqueta.Location = new Point(15, 15);

                TextBox txtValor = new TextBox();
                txtValor.Width = 310;
                txtValor.Location = new Point(15, 40);

                Button botonAceptar = new Button();
                botonAceptar.Text = "Buscar";
                botonAceptar.Width = 90;
                botonAceptar.Location = new Point(145, 75);
                botonAceptar.DialogResult = DialogResult.OK;

                Button botonCancelar = new Button();
                botonCancelar.Text = "Cancelar";
                botonCancelar.Width = 90;
                botonCancelar.Location = new Point(240, 75);
                botonCancelar.DialogResult = DialogResult.Cancel;

                formularioBuscar.Controls.Add(etiqueta);
                formularioBuscar.Controls.Add(txtValor);
                formularioBuscar.Controls.Add(botonAceptar);
                formularioBuscar.Controls.Add(botonCancelar);

                formularioBuscar.AcceptButton = botonAceptar;
                formularioBuscar.CancelButton = botonCancelar;

                if (formularioBuscar.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                valor = txtValor.Text.Trim();
            }

            if (string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show(
                    "Ingrese un valor para buscar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            try
            {
                DataTable resultado =
                    proveedorDAO.BuscarProveedores(campo, valor);

                dgvProveedores.DataSource = null;
                dgvProveedores.DataSource = resultado;

                if (resultado.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontraron proveedores.",
                        "Resultado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    MostrarProveedores();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }


        }
    }    
}



    



