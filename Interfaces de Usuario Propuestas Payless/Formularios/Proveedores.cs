
using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
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

            //ConfigurarDataGridView();
            // ConfigurarComboBuscar();

            //CargarProveedores();

            // Conectar el botón Buscar con su evento
            btnBuscar.Click += btnBuscar_Click;

            // Agregar esta línea para capturar el error de formato del DataGridView
            DGVtabla1.DataError += new DataGridViewDataErrorEventHandler(DGVtabla1_DataError);

            btnBuscar.Click += btnBuscar_Click;
            this.Load += Proveedores_Load;
        }





        private void ConfigurarComboBuscar()
        {

        }


        private void ConfigurarMenuRoles()
        {
            string rol = ClaseSesion.RolActual;

            bool esAdmin = (rol == "Administrador");
            bool esGerente = (rol == "Gerente");
            bool esCajero = (rol == "Cajero");

            // Módulos que cambian según el rol (estos suben/bajan sin afectar a Ayuda)
            lblMenu.Visible = esAdmin || esGerente || esCajero;
            lblVenta.Visible = esAdmin || esGerente || esCajero;
            lblCaja.Visible = esAdmin || esGerente || esCajero;
            lblReportes.Visible = esAdmin || esGerente || esCajero;
            lblCredito.Visible = esAdmin || esGerente;
            lblInventario.Visible = esAdmin || esGerente;
            lblCompras.Visible = esAdmin || esGerente;
            lblCliente.Visible = esAdmin || esGerente || esCajero;
            lblMantenimiento.Visible = esAdmin || esGerente;

            lblProductos.Visible = esAdmin;
            lblProveedores.Visible = esAdmin;
            lblUsuarios.Visible = esAdmin;


            // Ayuda se mantiene siempre visible y fija en la parte inferior gracias al Anchor = Bottom
            lblAyuda.Visible = true;
        }


        private void Proveedores_Load(object sender, EventArgs e)
        {
            ConfigurarMenuRoles();
            CargarProveedores();    

            cmbBuscar.Items.Clear();

            cmbBuscar.Items.Add("Todos");
            cmbBuscar.Items.Add("Activos");
            cmbBuscar.Items.Add("Inactivos");

            cmbBuscar.SelectedIndex = 0;




            lblCaja.Enabled = false;
            lblProveedores.Enabled = false;
            lblProductos.Enabled = false;
            lblVenta.Enabled = false;
            lblCompras.Enabled = false;
            lblUsuarios.Enabled = false;


            lblCliente.Enabled = false;
            lblVenta.Enabled = false;
            lblInventario.Enabled = false;
            lblMantenimiento.Enabled = false;
            lblReportes.Enabled = false;


            switch (ClaseSesion.RolActual)
            {
                case "Administrador":

                    lblCaja.Enabled = true;
                    lblCompras.Enabled = true;
                    lblVenta.Enabled = true;
                    lblUsuarios.Enabled = true;
                    lblMantenimiento.Enabled = true;
                    lblCliente.Enabled = true;
                    lblVenta.Enabled = true;
                    lblInventario.Enabled = true;
                    lblProveedores.Enabled = true;
                    lblProductos.Enabled = true;
                    lblReportes.Enabled = true;


                    break;

                case "Gerente":

                    lblCaja.Enabled = true;
                    lblCompras.Enabled = true;
                    lblVenta.Enabled = true;
                    lblMantenimiento.Enabled = true;
                    lblCliente.Enabled = true;
                    lblCredito.Enabled = true;
                    lblInventario.Enabled = true;
                    lblReportes.Enabled = true;


                    break;

                case "Cajero":

                    lblCaja.Enabled = true;
                    lblVenta.Enabled = true;
                    lblCliente.Enabled = true;
                    lblReportes.Enabled = true;

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
            ventana.Show();
            this.Hide();
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
            SubProovedoresAgregar ventana = new SubProovedoresAgregar();
            ventana.ShowDialog(); CargarProveedores(); this.Show ();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            SubProveedorEditar ventana = new SubProveedorEditar();
            ventana.ShowDialog(); CargarProveedores(); this.Show();
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

                if (tabla == null || tabla.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron datos de proveedores.");
                    return;
                }

                // Asignar los datos
                DGVtabla1.DataSource = tabla;

                // Ocultar el ID si existe en la fuente de datos
                if (DGVtabla1.Columns.Contains("id_proveedor"))
                {
                    DGVtabla1.Columns["id_proveedor"].Visible = false;
                }

                AjustarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores:\n\n" + ex.Message);
            }
        }


        private void AjustarColumnas()
        {
            // Ocultar el ID siempre
            if (DGVtabla1.Columns.Contains("id_proveedor"))
                DGVtabla1.Columns["id_proveedor"].Visible = false;

            if (DGVtabla1.Columns.Contains("nombre"))
            {
                DGVtabla1.Columns["nombre"].HeaderText = "Nombre";
                DGVtabla1.Columns["nombre"].Width = 230;
            }

            if (DGVtabla1.Columns.Contains("direccion"))
            {
                DGVtabla1.Columns["direccion"].HeaderText = "Dirección";
                DGVtabla1.Columns["direccion"].Width = 125;
            }

            if (DGVtabla1.Columns.Contains("ruc"))
            {
                DGVtabla1.Columns["ruc"].HeaderText = "RUC";
                DGVtabla1.Columns["ruc"].Width = 150;
            }

            // Aquí usamos "Estado" con la E mayúscula tal como lo definimos en el SELECT del DAO
            if (DGVtabla1.Columns.Contains("Estado"))
            {
                DGVtabla1.Columns["Estado"].HeaderText = "Estado";
                DGVtabla1.Columns["Estado"].Width = 125;
            }
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
                string filtro = cmbBuscar.Text.Trim();

                DataTable tabla;

                if (filtro == "Todos")
                {
                    tabla = proveedorDAO.MostrarProveedores();
                }
                else if (filtro == "Activos")
                {
                    tabla = proveedorDAO.BuscarPorEstado(true);
                }
                else if (filtro == "Inactivos")
                {
                    tabla = proveedorDAO.BuscarPorEstado(false);
                }
                else
                {
                    return;
                }

                DGVtabla1.DataSource = tabla;

                if (DGVtabla1.Columns.Contains("estado"))
                {
                    DGVtabla1.Columns["estado"].HeaderText = "Estado";
                }

                AjustarColumnas();
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

        private void lblReportes_Click(object sender, EventArgs e)
        {
            Reportes ventana = new Reportes();
            ventana.Show();
            this.Hide();
        }

        private void DGVtabla1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
        }

        private void DGVtabla1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void lblCaja_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja(); ventana.Show(); this.Hide();
        }

        private void lblMenu_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal(); ventana.Show(); this.Hide();
        }

        private void lblUsuarios_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario(); ventana.Show(); this.Hide();
        }

        private void lblCliente_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente(); ventana.Show(); this.Hide();
        }

        private void lblProveedores_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores(); ventana.Show(); this.Hide();
        }

        private void lblProductos_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos(); ventana.Show(); this.Hide();
        }

        private void lblCompras_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo(); ventana.Show(); this.Hide();
        }

        private void lblVenta_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas(); ventana.Show(); this.Hide();
        }

        private void lblCredito_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito(); ventana.Show(); this.Hide();
        }

        private void lblInventario_Click(object sender, EventArgs e)
        {
            inventario ventana = new inventario(); ventana.Show(); this.Hide();
        }

        private void lblMantenimiento_Click(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento(); ventana.Show(); this.Hide();
        }

        private void lblReportes_Click_1(object sender, EventArgs e)
        {
            Reportes
                ventana = new Reportes(); ventana.Show(); this.Hide();
        }

        private void lblAyuda_Click(object sender, EventArgs e)
        {

        }
    }

}






    



