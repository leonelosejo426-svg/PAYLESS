using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using Interfaces_de_Usuario_Propuestas_Payless.Utilidades;
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
    public partial class Reportes : Form
    {
        private ReporteCajaDAO reporteCajaDAO = new ReporteCajaDAO();
        private ReporteProductoDAO reporteProductoDAO = new ReporteProductoDAO();
        private ReporteUsuarioDAO reporteUsuarioDAO = new ReporteUsuarioDAO();


        private DataTable tablaReporte;

        public Reportes()
        {
            InitializeComponent();
        }

        private void Reportes_Load(object sender, EventArgs e)
        {


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

            CargarTiposReporte();
            CargarCriterios();
            CargarUsuarios();
            CargarEstados();

            dtpFechaDesde.Value =
                DateTime.Today.AddMonths(-1);

            dtpFechaHasta.Value =
                DateTime.Today;

            dtpFechaDesde.Enabled = false;
            dtpFechaHasta.Enabled = false;

            ConfigurarGrid();

            btnImprimir.Enabled = false;

            DGVtabla1.DataSource = null;

            cbTipoReporte.SelectedIndex = -1;
        }

        private void CargarTiposReporte()
        {
            cbTipoReporte.Items.Clear();

            cbTipoReporte.Items.Add("Reporte de Caja");
            cbTipoReporte.Items.Add("Reporte de Productos");
            cbTipoReporte.Items.Add("Reporte de Usuarios");
            cbTipoReporte.Items.Add("Reporte de Ventas");

            cbTipoReporte.SelectedIndex = -1;
            cbTipoReporte.Text = "Seleccione un reporte...";
        }

        private void CargarCriterios()
        {
            cbCriterio.Items.Clear();

            cbCriterio.Items.Add(
                "Todos los registros");

            cbCriterio.Items.Add(
                "Por período");

            cbCriterio.SelectedIndex = 0;
        }

        // =====================================================
        // USUARIOS
        // =====================================================

        private void CargarUsuarios()
        {
            cbUsuario.Items.Clear();

            cbUsuario.Items.Add("Todos");

            DataTable usuarios =
                reporteCajaDAO.ObtenerUsuarios();

            foreach (DataRow fila in usuarios.Rows)
            {
                cbUsuario.Items.Add(
                    fila["nombre_usuario"].ToString());
            }

            cbUsuario.SelectedIndex = 0;
        }

        // =====================================================
        // ESTADOS
        // =====================================================

        private void CargarEstados()
        {
            cbEstado.Items.Clear();

            cbEstado.Items.Add("Todos");
            cbEstado.Items.Add("Abierta");
            cbEstado.Items.Add("Cerrada");

            cbEstado.SelectedIndex = 0;
        }

        // =====================================================
        // CONFIGURAR DATAGRIDVIEW
        // =====================================================

        private void ConfigurarGrid()
        {
            DGVtabla1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            DGVtabla1.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            DGVtabla1.MultiSelect = false;

            DGVtabla1.ReadOnly = true;

            DGVtabla1.AllowUserToAddRows = false;

            DGVtabla1.AllowUserToDeleteRows = false;

            DGVtabla1.AutoGenerateColumns = true;
        }





        private void label26_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();
        }

        private void label26_Click_1(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void GenerarReporteCaja(bool usarFechas)
        {
            try
            {
                string usuario =
                    cbUsuario.SelectedItem.ToString();

                string estado =
                    cbEstado.SelectedItem.ToString();

                tablaReporte =
                    reporteCajaDAO.ObtenerReporteCaja(
                        dtpFechaDesde.Value,
                        dtpFechaHasta.Value,
                        usarFechas,
                        usuario,
                        estado);

                DGVtabla1.DataSource =
                    tablaReporte;

                if (tablaReporte.Rows.Count == 0)
                {
                    btnImprimir.Enabled = false;

                    MessageBox.Show(
                        "No se encontraron registros con los filtros seleccionados.",
                        "Reporte de Caja",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                FormatearReporteCaja();

                btnImprimir.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al generar el reporte:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (cbTipoReporte.SelectedItem == null)
            {
                MessageBox.Show(
                    "Seleccione un reporte.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (cbCriterio.SelectedItem == null)
            {
                MessageBox.Show(
                    "Seleccione un criterio de consulta.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            bool usarFechas =
                cbCriterio.SelectedItem.ToString()
                == "Por período";

            if (usarFechas &&
                dtpFechaDesde.Value.Date >
                dtpFechaHasta.Value.Date)
            {
                MessageBox.Show(
                    "La fecha desde no puede ser mayor que la fecha hasta.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string reporte =
                cbTipoReporte.SelectedItem.ToString();

            if (reporte == "Reporte de Caja")
            {
                GenerarReporteCaja(usarFechas);
            }
            else
            {
                MessageBox.Show(
                    "Este reporte todavía no ha sido programado.",
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (cbTipoReporte.SelectedItem == null)
            {
                MessageBox.Show(
                    "Seleccione un tipo de reporte.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (tablaReporte == null ||
                tablaReporte.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No hay información para imprimir.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string reporte =
                cbTipoReporte.SelectedItem.ToString();

            try
            {
                if (reporte == "Reporte de Caja")
                {
                    if (DGVtabla1.CurrentRow == null)
                    {
                        MessageBox.Show(
                            "Seleccione una caja para imprimir.",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    int idCaja =
                        Convert.ToInt32(
                            DGVtabla1.CurrentRow
                            .Cells["ID Caja"]
                            .Value);

                    GeneradorReporteCajaPDF.Generar(
                        tablaReporte,
                        idCaja);
                }
                else if (reporte == "Reporte de Productos")
                {
                    GeneradorReporteProductosPDF.Generar(
                        tablaReporte);
                }
                else if (reporte == "Reporte de Usuarios")
                {
                    GeneradorReporteUsuariosPDF.Generar(
                        tablaReporte);
                }
                else if (reporte == "Reporte de Ventas")
                {
                    MessageBox.Show(
                        "El reporte de ventas todavía no tiene impresión configurada.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al imprimir el reporte:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ImprimirReporteCaja(int idCaja)
        {
            try
            {
                GeneradorReporteCajaPDF.Generar(
                    tablaReporte,
                    idCaja);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al imprimir el reporte:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCriterio.SelectedItem == null)
                return;

            bool usarFechas =
                cbCriterio.SelectedItem.ToString()
                == "Por período";

            dtpFechaDesde.Enabled = usarFechas;
            dtpFechaHasta.Enabled = usarFechas;

            // Actualizar automáticamente
            // cuando cambie el criterio
            GenerarReporteSeleccionado();
        }





        private void FormatearReporteCaja()
        {
            if (DGVtabla1.Columns.Contains("ID Caja"))
            {
                DGVtabla1.Columns["ID Caja"].Width = 70;
            }

            if (DGVtabla1.Columns.Contains("Fecha Apertura"))
            {
                DGVtabla1.Columns["Fecha Apertura"]
                    .DefaultCellStyle.Format =
                    "dd/MM/yyyy HH:mm";
            }

            if (DGVtabla1.Columns.Contains("Fecha Cierre"))
            {
                DGVtabla1.Columns["Fecha Cierre"]
                    .DefaultCellStyle.Format =
                    "dd/MM/yyyy HH:mm";
            }

            string[] columnasMoneda =
            {
                "Saldo Inicial",
                "Ingresos",
                "Egresos",
                "Monto Esperado",
                "Monto Arqueado",
                "Diferencia",
                "Saldo Final",
                "Tipo Cambio"
            };

            foreach (string columna in columnasMoneda)
            {
                if (DGVtabla1.Columns.Contains(columna))
                {
                    DGVtabla1.Columns[columna]
                        .DefaultCellStyle.Format =
                        "C$ #,##0.00";
                }
            }
        }

        private void cbTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigurarFiltrosSegunReporte();
            GenerarReporteSeleccionado();
        }

        private void ConfigurarFiltrosSegunReporte()
        {
            if (cbTipoReporte.SelectedItem == null)
                return;

            string reporte =
                cbTipoReporte.SelectedItem.ToString();

            if (reporte == "Reporte de Caja")
            {
                cbCriterio.Enabled = true;
                cbUsuario.Enabled = true;
                cbEstado.Enabled = true;

                cbRol.Enabled = false;

                dtpFechaDesde.Enabled =
                    cbCriterio.SelectedItem != null &&
                    cbCriterio.SelectedItem.ToString() == "Por período";

                dtpFechaHasta.Enabled =
                    cbCriterio.SelectedItem != null &&
                    cbCriterio.SelectedItem.ToString() == "Por período";

                CargarEstadosCaja();
            }
            else if (reporte == "Reporte de Productos")
            {
                cbCriterio.SelectedIndex = 0;
                cbCriterio.Enabled = false;

                cbUsuario.SelectedIndex = 0;
                cbUsuario.Enabled = false;

                cbRol.Enabled = false;

                cbEstado.Enabled = true;

                dtpFechaDesde.Enabled = false;
                dtpFechaHasta.Enabled = false;

                CargarEstadosProductos();
            }
            else if (reporte == "Reporte de Usuarios")
            {
                cbCriterio.SelectedIndex = 0;
                cbCriterio.Enabled = false;

                cbUsuario.Enabled = true;

                cbRol.Enabled = true;

                cbEstado.Enabled = true;

                dtpFechaDesde.Enabled = false;
                dtpFechaHasta.Enabled = false;

                CargarUsuariosReporteUsuarios();
                CargarEstadosUsuarios();
                CargarRoles();
            }
            else if (reporte == "Reporte de Ventas")
            {
                cbCriterio.Enabled = true;
                cbUsuario.Enabled = true;

                cbRol.Enabled = false;

                cbEstado.Enabled = true;

                CargarEstadosVentas();
            }
        }

        private void CargarEstadosProductos()
        {
            cbEstado.Items.Clear();

            cbEstado.Items.Add("Todos");
            cbEstado.Items.Add("Activo");
            cbEstado.Items.Add("Inactivo");

            cbEstado.SelectedIndex = 0;
        }

        private void CargarEstadosCaja()
        {
            cbEstado.Items.Clear();

            cbEstado.Items.Add("Todos");
            cbEstado.Items.Add("Abierta");
            cbEstado.Items.Add("Cerrada");

            cbEstado.SelectedIndex = 0;
        }

        private void CargarEstadosVentas()
        {
            cbEstado.Items.Clear();

            cbEstado.Items.Add("Todos");
            cbEstado.Items.Add("Activas");
            cbEstado.Items.Add("Anuladas");

            cbEstado.SelectedIndex = 0;
        }

        private void cbUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerarReporteSeleccionado();
        }

        private void cbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerarReporteSeleccionado();
        }

        private void dtpFechaDesde_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaDesde.Enabled)
                GenerarReporteSeleccionado();
        }

        private void dtpFechaHasta_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaHasta.Enabled)
                GenerarReporteSeleccionado();
        }

        private void GenerarReporteSeleccionado()
        {
            if (cbTipoReporte.SelectedItem == null)
            {
                DGVtabla1.DataSource = null;
                tablaReporte = null;
                btnImprimir.Enabled = false;
                return;
            }

            string reporte =
                cbTipoReporte.SelectedItem.ToString();

            if (reporte == "Reporte de Caja")
            {
                GenerarReporteCaja();
            }
            else if (reporte == "Reporte de Productos")
            {
                GenerarReporteProductos();
            }
            else if (reporte == "Reporte de Usuarios")
            {
                GenerarReporteUsuarios();
            }
            else if (reporte == "Reporte de Ventas")
            {
              //  GenerarReporteVentas();
            }
        }

        private void GenerarReporteProductos()
        {
            try
            {
                string estado =
                    cbEstado.SelectedItem == null
                    ? "Todos"
                    : cbEstado.SelectedItem.ToString();

                tablaReporte =
                    reporteProductoDAO.ObtenerReporteProductos(
                        estado);

                DGVtabla1.DataSource = tablaReporte;

                if (tablaReporte.Rows.Count > 0)
                {
                    FormatearReporteProductos();
                    btnImprimir.Enabled = true;
                }
                else
                {
                    btnImprimir.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar el reporte de productos:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnImprimir.Enabled = false;
            }
        }

        private void FormatearReporteProductos()
        {
            if (DGVtabla1.Columns.Contains("ID Producto"))
                DGVtabla1.Columns["ID Producto"].Width = 80;

            if (DGVtabla1.Columns.Contains("Precio Venta"))
            {
                DGVtabla1.Columns["Precio Venta"]
                    .DefaultCellStyle.Format = "C$ #,##0.00";
            }

            if (DGVtabla1.Columns.Contains("Stock Total"))
            {
                DGVtabla1.Columns["Stock Total"]
                    .DefaultCellStyle.Format = "N0";
            }
        }

        private void GenerarReporteCaja()
        {
            try
            {
                bool usarFechas =
                    cbCriterio.SelectedItem != null &&
                    cbCriterio.SelectedItem.ToString()
                    == "Por período";

                if (usarFechas &&
                    dtpFechaDesde.Value.Date >
                    dtpFechaHasta.Value.Date)
                {
                    return;
                }

                string usuario =
                    cbUsuario.SelectedItem == null
                    ? "Todos"
                    : cbUsuario.SelectedItem.ToString();

                string estado =
                    cbEstado.SelectedItem == null
                    ? "Todos"
                    : cbEstado.SelectedItem.ToString();

                tablaReporte =
                    reporteCajaDAO.ObtenerReporteCaja(
                        dtpFechaDesde.Value,
                        dtpFechaHasta.Value,
                        usarFechas,
                        usuario,
                        estado);

                DGVtabla1.DataSource =
                    tablaReporte;

                if (tablaReporte.Rows.Count > 0)
                {
                    FormatearReporteCaja();

                    btnImprimir.Enabled = true;
                }
                else
                {
                    btnImprimir.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar el reporte de caja:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnImprimir.Enabled = false;
            }
        }

        private void CargarEstadosUsuarios()
        {
            cbEstado.Items.Clear();

            cbEstado.Items.Add("Todos");
            cbEstado.Items.Add("Activo");
            cbEstado.Items.Add("Inactivo");

            cbEstado.SelectedIndex = 0;
        }

        private void CargarRoles()
        {
            cbRol.Items.Clear();

            cbRol.Items.Add("Todos");

            DataTable roles =
                reporteUsuarioDAO.ObtenerRoles();

            foreach (DataRow fila in roles.Rows)
            {
                cbRol.Items.Add(
                    fila["nombre_rol"].ToString());
            }

            cbRol.SelectedIndex = 0;
        }
        private void CargarUsuariosReporteUsuarios()
        {
            cbUsuario.Items.Clear();

            cbUsuario.Items.Add("Todos");

            DataTable usuarios =
                reporteUsuarioDAO.ObtenerUsuarios();

            foreach (DataRow fila in usuarios.Rows)
            {
                cbUsuario.Items.Add(
                    fila["nombre_usuario"].ToString());
            }

            cbUsuario.SelectedIndex = 0;
        }

        private void GenerarReporteUsuarios()
        {
            try
            {
                string usuario =
                    cbUsuario.SelectedItem == null
                    ? "Todos"
                    : cbUsuario.SelectedItem.ToString();

                string rol =
                    cbRol.SelectedItem == null
                    ? "Todos"
                    : cbRol.SelectedItem.ToString();

                string estado =
                    cbEstado.SelectedItem == null
                    ? "Todos"
                    : cbEstado.SelectedItem.ToString();

                tablaReporte =
                    reporteUsuarioDAO.ObtenerReporteUsuarios(
                        usuario,
                        rol,
                        estado);

                DGVtabla1.DataSource = tablaReporte;

                if (tablaReporte.Rows.Count > 0)
                {
                    FormatearReporteUsuarios();
                    btnImprimir.Enabled = false;
                }
                else
                {
                    btnImprimir.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar el reporte de usuarios:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnImprimir.Enabled = false;
            }
        }

        private void FormatearReporteUsuarios()
        {
            if (DGVtabla1.Columns.Contains("ID Usuario"))
                DGVtabla1.Columns["ID Usuario"].Width = 70;

            if (DGVtabla1.Columns.Contains("Usuario"))
                DGVtabla1.Columns["Usuario"].Width = 100;

            if (DGVtabla1.Columns.Contains("Nombre Completo"))
                DGVtabla1.Columns["Nombre Completo"].Width = 180;

            if (DGVtabla1.Columns.Contains("Correo"))
                DGVtabla1.Columns["Correo"].Width = 180;

            if (DGVtabla1.Columns.Contains("Rol"))
                DGVtabla1.Columns["Rol"].Width = 120;

            if (DGVtabla1.Columns.Contains("Fecha Registro"))
            {
                DGVtabla1.Columns["Fecha Registro"]
                    .DefaultCellStyle.Format =
                    "dd/MM/yyyy HH:mm";
            }

            if (DGVtabla1.Columns.Contains("Último Ingreso"))
            {
                DGVtabla1.Columns["Último Ingreso"]
                    .DefaultCellStyle.Format =
                    "dd/MM/yyyy HH:mm";
            }

            if (DGVtabla1.Columns.Contains("Total Ingresos"))
            {
                DGVtabla1.Columns["Total Ingresos"]
                    .DefaultCellStyle.Format = "N0";
            }

            if (DGVtabla1.Columns.Contains("Ingresos Exitosos"))
            {
                DGVtabla1.Columns["Ingresos Exitosos"]
                    .DefaultCellStyle.Format = "N0";
            }

            if (DGVtabla1.Columns.Contains("Ingresos Fallidos"))
            {
                DGVtabla1.Columns["Ingresos Fallidos"]
                    .DefaultCellStyle.Format = "N0";
            }
        }

        private void cbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerarReporteSeleccionado();
        }

        private void label26_Click_2(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Close();
        }

        private void lblCaja_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Close();
        }

        private void lblUsuarios_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();
            this.Close();
        }

        private void lblCliente_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Close();
        }

        private void lblProductos_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Close();
        }

        private void lblProveedores_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Close();
        }

        private void lblCompras_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo();
            ventana.Show();
            this.Close();
        }

        private void lblVenta_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Close();
        }

        private void lblCredito_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito();
            ventana.Show();
            this.Close();
        }

        private void lblInventario_Click(object sender, EventArgs e)
        {
            inventario ventana = new inventario();
            ventana.Show();
            this.Close();
        }

        private void lblMantenimiento_Click(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento();
            ventana.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Reportes ventana = new Reportes();
            ventana.Show();
            this.Close();
        }
    }
}
