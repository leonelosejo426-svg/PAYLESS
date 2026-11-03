using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using Interfaces_de_Usuario_Propuestas_Payless.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Interfaces_de_Usuario_Propuestas_Payless.Utilidades;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Reportes : Form
    {
        private ReporteCajaDAO reporteCajaDAO = new ReporteCajaDAO();
        private ReporteProductoDAO reporteProductoDAO = new ReporteProductoDAO();
        private ReporteUsuarioDAO reporteUsuarioDAO = new ReporteUsuarioDAO();
        private ReporteVentaDAO reporteVentaDAO = new ReporteVentaDAO();
        private ReporteCompraDAO reporteCompraDAO = new ReporteCompraDAO();


        private DataTable tablaReporte;

        public Reportes()
        {
            InitializeComponent();
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

        private void Reportes_Load(object sender, EventArgs e)
        {
            btnGenerar.Visible = false;
            ConfigurarMenuRoles();

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
            cbTipoReporte.Items.Add("Reporte de Compras"); 

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
            if (cbTipoReporte.SelectedItem == null || cbCriterio.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un reporte y un criterio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🌟 1. Llamar la validación aquí antes de generar
            ValidarFechasSeleccionadas();
        }

        

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            EjecutarImpresion(true);
           

        }

        private void EjecutarImpresion(bool esHistorico)
        {
            if (cbTipoReporte.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un tipo de reporte.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string reporte = cbTipoReporte.SelectedItem.ToString();

            try
            {
                // ==========================================
                // 1. REPORTE DE CAJA
                // ==========================================
                if (reporte == "Reporte de Caja")
                {
                    if (esHistorico)
                    {
                        // Copiamos la tabla actual para no afectar la visualización en pantalla
                        DataTable datosParaImprimir = tablaReporte.Copy();

                        // Eliminamos las columnas de ID para cumplir con la regla operativa en el PDF
                        if (datosParaImprimir.Columns.Contains("ID Caja")) datosParaImprimir.Columns.Remove("ID Caja");
                        if (datosParaImprimir.Columns.Contains("id_caja")) datosParaImprimir.Columns.Remove("id_caja");

                        // Llama al método que imprime todo el histórico de cajas limpio en PDF
                        GeneradorReporteCajaPDF.Generar(datosParaImprimir);
                    }
                    else
                    {
                        // Imprime individualmente la caja que tengas seleccionada en la tabla
                        if (DGVtabla1.CurrentRow == null)
                        {
                            MessageBox.Show("Seleccione una caja de la tabla para imprimir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        int idCaja = Convert.ToInt32(DGVtabla1.CurrentRow.Cells["ID Caja"].Value);
                        GeneradorReporteCajaPDF.Generar(tablaReporte, idCaja);
                    }
                }
                // ==========================================
                // 2. REPORTE DE PRODUCTOS
                // ==========================================
                else if (reporte == "Reporte de Productos")
                {
                    DataTable dtAImprimir;

                    if (esHistorico)
                    {
                        dtAImprimir = reporteProductoDAO.ObtenerReporteProductos("Todos");
                    }
                    else
                    {
                        if (DGVtabla1.CurrentRow != null)
                        {
                            dtAImprimir = tablaReporte.Clone();
                            DataRowView filaSeleccionada = (DataRowView)DGVtabla1.CurrentRow.DataBoundItem;
                            dtAImprimir.ImportRow(filaSeleccionada.Row);
                        }
                        else
                        {
                            dtAImprimir = tablaReporte;
                        }
                    }

                    if (dtAImprimir == null || dtAImprimir.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay información para imprimir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    GeneradorReporteProductosPDF.Generar(dtAImprimir);
                }
                // ==========================================
                // 3. REPORTE DE VENTAS
                // ==========================================
                else if (reporte == "Reporte de Ventas")
                {
                    DataTable dtAImprimir;

                    if (esHistorico)
                    {
                        dtAImprimir = reporteVentaDAO.ObtenerReporteVentas(DateTime.MinValue, DateTime.MaxValue, false, "Todos", "Todos");
                    }
                    else
                    {
                        if (DGVtabla1.CurrentRow != null)
                        {
                            dtAImprimir = tablaReporte.Clone();
                            DataRowView filaSeleccionada = (DataRowView)DGVtabla1.CurrentRow.DataBoundItem;
                            dtAImprimir.ImportRow(filaSeleccionada.Row);
                        }
                        else
                        {
                            dtAImprimir = tablaReporte;
                        }
                    }

                    if (dtAImprimir == null || dtAImprimir.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay información para imprimir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    bool usarFechas = !esHistorico && cbCriterio.SelectedItem != null && cbCriterio.SelectedItem.ToString() == "Por período";
                    string usuario = cbUsuario.SelectedItem == null ? "Todos" : cbUsuario.SelectedItem.ToString();
                    string estado = cbEstado.SelectedItem == null ? "Todos" : cbEstado.SelectedItem.ToString();

                    GeneradorReporteVentasPDF.Generar(dtAImprimir, dtpFechaDesde.Value, dtpFechaHasta.Value, usarFechas, usuario, estado);
                }
                // ==========================================
                // 4. REPORTE DE COMPRAS
                // ==========================================
                else if (reporte == "Reporte de Compras")
                {
                    DataTable dtAImprimir;

                    if (esHistorico)
                    {
                        dtAImprimir = reporteCompraDAO.ObtenerReporteCompras(DateTime.MinValue, DateTime.MaxValue, false, "Todos", "Todos");
                    }
                    else
                    {
                        if (DGVtabla1.CurrentRow != null)
                        {
                            dtAImprimir = tablaReporte.Clone();
                            DataRowView filaSeleccionada = (DataRowView)DGVtabla1.CurrentRow.DataBoundItem;
                            dtAImprimir.ImportRow(filaSeleccionada.Row);
                        }
                        else
                        {
                            dtAImprimir = tablaReporte;
                        }
                    }

                    if (dtAImprimir == null || dtAImprimir.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay información para imprimir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    bool usarFechas = !esHistorico && cbCriterio.SelectedItem != null && cbCriterio.SelectedItem.ToString() == "Por período";
                    string usuario = cbUsuario.SelectedItem == null ? "Todos" : cbUsuario.SelectedItem.ToString();
                    string estado = cbEstado.SelectedItem == null ? "Todos" : cbEstado.SelectedItem.ToString();

                    GeneradorReporteCompraPDF.Generar(dtAImprimir, dtpFechaDesde.Value, dtpFechaHasta.Value, usarFechas, usuario, estado);
                }
                // ==========================================
                // 5. REPORTE DE USUARIOS
                // ==========================================
                else if (reporte == "Reporte de Usuarios")
                {
                    string usuarioSeleccionado = cbUsuario != null && cbUsuario.SelectedItem != null ? cbUsuario.SelectedItem.ToString() : "Todos";
                    string rolSeleccionado = cbRol != null && cbRol.SelectedItem != null ? cbRol.SelectedItem.ToString() : "Todos";
                    string estadoSeleccionado = cbEstado != null && cbEstado.SelectedItem != null ? cbEstado.SelectedItem.ToString() : "Todos";

                    ReporteUsuarioDAO dao = new ReporteUsuarioDAO();
                    DataTable dt;

                    if (esHistorico)
                    {
                        dt = dao.ObtenerReporteUsuarios("Todos", "Todos", "Todos");
                    }
                    else
                    {
                        if (DGVtabla1.CurrentRow != null)
                        {
                            dt = tablaReporte.Clone();
                            DataRowView filaSeleccionada = (DataRowView)DGVtabla1.CurrentRow.DataBoundItem;
                            dt.ImportRow(filaSeleccionada.Row);
                        }
                        else
                        {
                            dt = dao.ObtenerReporteUsuarios(usuarioSeleccionado, rolSeleccionado, estadoSeleccionado);
                        }
                    }

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay registros disponibles para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    ReporteUsuariosPDF.GenerarReporte(dt, usuarioSeleccionado, rolSeleccionado, estadoSeleccionado);
                }

                MessageBox.Show("Reporte exportado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir el reporte:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (cbCriterio.SelectedItem == null) return;

            bool usarFechas = cbCriterio.SelectedItem.ToString() == "Por período";
            dtpFechaDesde.Enabled = usarFechas;
            dtpFechaHasta.Enabled = usarFechas;

            if (usarFechas)
            {
                // 🌟 2. Llamar aquí también
                ValidarFechasSeleccionadas();
            }

            GenerarReporteSeleccionado();
        }

        private void dtpFechaDesde_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaDesde.Enabled)
            {
                // 🌟 3. Llamar aquí al cambiar la fecha de inicio
                ValidarFechasSeleccionadas();
                GenerarReporteSeleccionado();
            }
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

            string reporte = cbTipoReporte.SelectedItem.ToString();

            // Resetear etiquetas por defecto
            lblRol.Text = "Rol:";
            lblUsuario.Text = "Usuario:";

            if (reporte == "Reporte de Caja")
            {
                cbCriterio.Enabled = true;
                cbUsuario.Enabled = true;
                cbEstado.Enabled = true;
                cbRol.Enabled = false;

                dtpFechaDesde.Enabled = cbCriterio.SelectedItem != null && cbCriterio.SelectedItem.ToString() == "Por período";
                dtpFechaHasta.Enabled = cbCriterio.SelectedItem != null && cbCriterio.SelectedItem.ToString() == "Por período";

                CargarEstadosCaja();
            }
            else if (reporte == "Reporte de Productos")
            {
                lblRol.Text = "Categoría:";
                lblUsuario.Text = "Producto:";

                cbCriterio.SelectedIndex = 0;
                cbCriterio.Enabled = false;

                cbUsuario.Enabled = true;
                CargarProductosReporteProductos();

                cbRol.Enabled = true;
                CargarCategoriasReporteProductos();

                cbEstado.Enabled = true;
                dtpFechaDesde.Enabled = false;
                dtpFechaHasta.Enabled = false;

                // 🌟 NUEVO: Carga ordenada con opción de Stock Bajo
                CargarEstadosProductosParaFiltroStock();
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
            else if (reporte == "Reporte de Compras")
            {
                cbCriterio.Enabled = true;
                cbUsuario.Enabled = true;
                cbRol.Enabled = false;
                cbEstado.Enabled = true;

                dtpFechaDesde.Enabled = cbCriterio.SelectedItem != null && cbCriterio.SelectedItem.ToString() == "Por período";
                dtpFechaHasta.Enabled = cbCriterio.SelectedItem != null && cbCriterio.SelectedItem.ToString() == "Por período";

                CargarEstadosCompras();
            }
        }



        private void CargarEstadosProductosParaFiltroStock()
        {
            cbEstado.Items.Clear();
            cbEstado.Items.Add("Todos");
            cbEstado.Items.Add("Activo");
            cbEstado.Items.Add("Inactivo");
            cbEstado.Items.Add("Stock Bajo"); // Filtro prioritario requerido
            cbEstado.SelectedIndex = 0;
        }






        private void CargarEstadosCompras()
        {
            cbEstado.Items.Clear();
            cbEstado.Items.Add("Todos");
            cbEstado.Items.Add("Activo");
            cbEstado.Items.Add("Anulado");
            cbEstado.SelectedIndex = 0;
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
            if (cbTipoReporte.SelectedItem != null && cbTipoReporte.SelectedItem.ToString() == "Reporte de Productos")
            {
                GenerarReporteSeleccionado();
            }
        }
        

        private void cbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
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

            string reporte = cbTipoReporte.SelectedItem.ToString();

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
                GenerarReporteVentas();
            }
            else if (reporte == "Reporte de Compras")
            {
                GenerarReporteCompras();
            }
            OcultarColumnasId();
        }

        private DateTime ObtenerPrimeraFechaSistema()
        {
            try
            {
                return new DateTime(2025, 1, 1); // Fecha base de respaldo segura para tu sistema
            }
            catch
            {
                return new DateTime(2025, 1, 1);
            }
        }

        private void ValidarFechasSeleccionadas()
        {
            bool usarFechas = cbCriterio.SelectedItem != null && cbCriterio.SelectedItem.ToString() == "Por período";
            if (!usarFechas) return;

            DateTime primeraFechaPermitida = ObtenerPrimeraFechaSistema();
            bool huboCorreccion = false;

            // 1. Validar si la fecha "desde" es anterior al primer registro del sistema
            if (dtpFechaDesde.Value.Date < primeraFechaPermitida.Date)
            {
                MessageBox.Show(
                    $"La fecha de inicio no puede ser anterior al primer registro del sistema ({primeraFechaPermitida:dd/MM/yyyy}). Se ha restablecido a la fecha mínima permitida.",
                    "Validación de Fecha",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                dtpFechaDesde.Value = primeraFechaPermitida; // Restablecer a la fecha por defecto segura
                huboCorreccion = true;
            }

            // 2. Validar si la fecha "desde" es mayor que la fecha "hasta"
            if (dtpFechaDesde.Value.Date > dtpFechaHasta.Value.Date)
            {
                MessageBox.Show(
                    "La fecha 'desde' no puede ser mayor que la fecha 'hasta'. Se ha restablecido al valor correspondiente.",
                    "Validación de Fecha",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                dtpFechaDesde.Value = dtpFechaHasta.Value.Date;
                huboCorreccion = true;
            }

            // 🌟 Si hubo una corrección automática, regeneramos el reporte de inmediato para que salgan los registros
            if (huboCorreccion)
            {
                GenerarReporteSeleccionado();
            }
        }

        private void GenerarReporteCompras()
        {
            try
            {
                bool usarFechas = cbCriterio.SelectedItem != null && cbCriterio.SelectedItem.ToString() == "Por período";

                if (usarFechas && dtpFechaDesde.Value.Date > dtpFechaHasta.Value.Date)
                {
                    MessageBox.Show("La fecha desde no puede ser mayor que la fecha hasta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string usuario = cbUsuario.SelectedItem == null ? "Todos" : cbUsuario.SelectedItem.ToString();
                string estado = cbEstado.SelectedItem == null ? "Todos" : cbEstado.SelectedItem.ToString();

                tablaReporte = reporteCompraDAO.ObtenerReporteCompras(
                    dtpFechaDesde.Value,
                    dtpFechaHasta.Value,
                    usarFechas,
                    usuario,
                    estado);

                DGVtabla1.DataSource = tablaReporte;

                if (tablaReporte.Rows.Count > 0)
                {
                    FormatearReporteCompras();
                    btnImprimir.Enabled = true;
                }
                else
                {
                    btnImprimir.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte de compras:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnImprimir.Enabled = false;
            }
        }

        private void FormatearReporteCompras()
        {
            if (DGVtabla1.Columns.Contains("ID Compra"))
                DGVtabla1.Columns["ID Compra"].Width = 70;

            if (DGVtabla1.Columns.Contains("Código"))
                DGVtabla1.Columns["Código"].Width = 90;

            if (DGVtabla1.Columns.Contains("Fecha"))
            {
                DGVtabla1.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }

            if (DGVtabla1.Columns.Contains("Total"))
            {
                DGVtabla1.Columns["Total"].DefaultCellStyle.Format = "C$ #,##0.00";
            }
        }

        // Cargar Categorías desde la Base de Datos para el ComboBox cbRol (Reutilizado como Categoría)
        private void CargarCategoriasReporteProductos()
        {
            cbRol.Items.Clear();
            cbRol.Items.Add("Todas");

            ProductoDAO productoDAO = new ProductoDAO();
            DataTable categorias = productoDAO.CargarCategorias(); // Método DAO existente

            foreach (DataRow fila in categorias.Rows)
            {
                if (fila["nombre_categoria"] != DBNull.Value)
                {
                    cbRol.Items.Add(fila["nombre_categoria"].ToString());
                }
            }

            cbRol.SelectedIndex = 0;
        }

        // Cargar Productos desde la Base de Datos para el ComboBox cbUsuario (Reutilizado como Producto)
        private void CargarProductosReporteProductos()
        {
            cbUsuario.Items.Clear();
            cbUsuario.Items.Add("Todos");

            ProductoDAO productoDAO = new ProductoDAO();
            DataTable dtProductos = productoDAO.MostrarProductos();

            foreach (DataRow row in dtProductos.Rows)
            {
                string nombreProd = row["nombre"].ToString();
                if (!cbUsuario.Items.Contains(nombreProd))
                {
                    cbUsuario.Items.Add(nombreProd);
                }
            }

            cbUsuario.SelectedIndex = 0;

            // Configurar autocompletado para facilitar la búsqueda
            cbUsuario.DropDownStyle = ComboBoxStyle.DropDown;
            cbUsuario.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbUsuario.AutoCompleteSource = AutoCompleteSource.ListItems;
        }


        private void GenerarReporteProductos()
        {
            try
            {
                string estado = cbEstado.SelectedItem == null ? "Todos" : cbEstado.SelectedItem.ToString();
                string categoriaFiltro = cbRol.Enabled && cbRol.SelectedItem != null ? cbRol.SelectedItem.ToString() : "Todas";

                string productoFiltro = "Todos";
                if (cbUsuario.Enabled && !string.IsNullOrWhiteSpace(cbUsuario.Text))
                {
                    productoFiltro = cbUsuario.Text.Trim();
                }

                // 🌟 CORRECCIÓN: Si el estado es "Stock Bajo", pedimos "Todos" al DAO para poder evaluarlo localmente con LINQ
                string estadoParaDAO = (estado == "Stock Bajo") ? "Todos" : estado;
                tablaReporte = reporteProductoDAO.ObtenerReporteProductos(estadoParaDAO);

                if (tablaReporte != null && tablaReporte.Rows.Count > 0)
                {
                    var query = tablaReporte.AsEnumerable();

                    // 1. Filtrar por Estado / Stock Bajo de forma local
                    if (estado == "Stock Bajo")
                    {
                        string colStock = tablaReporte.Columns.Contains("stock") ? "stock" : (tablaReporte.Columns.Contains("Stock") ? "Stock" : "");
                        string colMin = tablaReporte.Columns.Contains("stock_minimo") ? "stock_minimo" : (tablaReporte.Columns.Contains("Stock Minimo") ? "Stock Minimo" : "");

                        if (!string.IsNullOrEmpty(colStock) && !string.IsNullOrEmpty(colMin))
                        {
                            query = query.Where(row => row[colStock] != DBNull.Value && row[colMin] != DBNull.Value &&
                                                      Convert.ToInt32(row[colStock]) <= Convert.ToInt32(row[colMin]));
                        }
                    }

                    // 2. Filtrar por Categoría
                    if (categoriaFiltro != "Todas")
                    {
                        string colCat = tablaReporte.Columns.Contains("categoria") ? "categoria" :
                                       (tablaReporte.Columns.Contains("nombre_categoria") ? "nombre_categoria" :
                                       (tablaReporte.Columns.Contains("Categoría") ? "Categoría" : ""));

                        if (!string.IsNullOrEmpty(colCat))
                        {
                            query = query.Where(row => row[colCat] != DBNull.Value &&
                                row[colCat].ToString().Trim().Equals(categoriaFiltro, StringComparison.OrdinalIgnoreCase));
                        }
                    }

                    // 3. Filtrar por Nombre de Producto
                    if (productoFiltro != "Todos" && !string.IsNullOrEmpty(productoFiltro))
                    {
                        string colNombre = tablaReporte.Columns.Contains("nombre") ? "nombre" :
                                          (tablaReporte.Columns.Contains("Producto") ? "Producto" : "");

                        if (!string.IsNullOrEmpty(colNombre))
                        {
                            query = query.Where(row => row[colNombre] != DBNull.Value &&
                                row[colNombre].ToString().IndexOf(productoFiltro, StringComparison.OrdinalIgnoreCase) >= 0);
                        }
                    }

                    tablaReporte = query.Any() ? query.CopyToDataTable() : tablaReporte.Clone();
                }

                DGVtabla1.DataSource = tablaReporte;

                if (tablaReporte != null && tablaReporte.Rows.Count > 0)
                {
                    FormatearReporteProductos();
                    OcultarColumnasId(); // Oculta los IDs por directiva
                    btnImprimir.Enabled = true;
                    if (btnImprimir != null) btnImprimir.Enabled = true;
                }
                else
                {
                    btnImprimir.Enabled = false;
                    if (btnImprimir != null) btnImprimir.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte de productos:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnImprimir.Enabled = false;
            }
        }

        private void FormatearReporteProductos()
        {
            if (DGVtabla1.Columns.Contains("ID Producto"))
                DGVtabla1.Columns["ID Producto"].Width = 80;

            if (DGVtabla1.Columns.Contains("Precio Venta"))
            {
                DGVtabla1.Columns["Precio Venta"].DefaultCellStyle.Format = "C$ #,##0.00";
                DGVtabla1.Columns["Precio Venta"].DefaultCellStyle.NullValue = "C$ 0.00";
            }

            if (DGVtabla1.Columns.Contains("Stock"))
            {
                DGVtabla1.Columns["Stock"].DefaultCellStyle.Format = "N0";
            }

            if (DGVtabla1.Columns.Contains("Stock Minimo"))
            {
                DGVtabla1.Columns["Stock Minimo"].DefaultCellStyle.Format = "N0";
            }

            if (DGVtabla1.Columns.Contains("Alerta"))
                DGVtabla1.Columns["Alerta"].Width = 110;
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
                string usuario = cbUsuario.SelectedItem == null ? "Todos" : cbUsuario.SelectedItem.ToString();
                string rol = cbRol.SelectedItem == null ? "Todos" : cbRol.SelectedItem.ToString();
                string estado = cbEstado.SelectedItem == null ? "Todos" : cbEstado.SelectedItem.ToString();

                tablaReporte = reporteUsuarioDAO.ObtenerReporteUsuarios(usuario, rol, estado);

                DGVtabla1.DataSource = tablaReporte;

                if (tablaReporte != null && tablaReporte.Rows.Count > 0)
                {
                    btnImprimir.Enabled = true;
                }
                else
                {
                    btnImprimir.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte de usuarios:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private void GenerarReporteVentas()
        {
            try
            {
                bool usarFechas = cbCriterio.SelectedItem != null && cbCriterio.SelectedItem.ToString() == "Por período";

                if (usarFechas && dtpFechaDesde.Value.Date > dtpFechaHasta.Value.Date)
                {
                    MessageBox.Show(
                        "La fecha desde no puede ser mayor que la fecha hasta.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                string usuario = cbUsuario.SelectedItem == null ? "Todos" : cbUsuario.SelectedItem.ToString();
                string estado = cbEstado.SelectedItem == null ? "Todos" : cbEstado.SelectedItem.ToString();

                tablaReporte = reporteVentaDAO.ObtenerReporteVentas(
                    dtpFechaDesde.Value,
                    dtpFechaHasta.Value,
                    usarFechas,
                    usuario,
                    estado);

                DGVtabla1.DataSource = tablaReporte;

                if (tablaReporte.Rows.Count > 0)
                {
                    FormatearReporteVentas();
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
                    "Error al cargar el reporte de ventas:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnImprimir.Enabled = false;
            }
        }

        private void FormatearReporteVentas()
        {
            if (DGVtabla1.Columns.Contains("ID Venta"))
                DGVtabla1.Columns["ID Venta"].Width = 70;

            if (DGVtabla1.Columns.Contains("Código"))
                DGVtabla1.Columns["Código"].Width = 90;

            if (DGVtabla1.Columns.Contains("Fecha"))
            {
                DGVtabla1.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }

            string[] columnasMoneda = { "Subtotal", "IVA", "Total" };
            foreach (string col in columnasMoneda)
            {
                if (DGVtabla1.Columns.Contains(col))
                {
                    DGVtabla1.Columns[col].DefaultCellStyle.Format = "C$ #,##0.00";
                }
            }
        }

        private void cbUsuario_TextChanged(object sender, EventArgs e)
        {
            if (cbTipoReporte.SelectedItem != null && cbTipoReporte.SelectedItem.ToString() == "Reporte de Productos")
            {
                GenerarReporteSeleccionado();
            }
        }

        private void cbUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true; // Evita el pitido de Windows

                if (cbTipoReporte.SelectedItem != null && cbTipoReporte.SelectedItem.ToString() == "Reporte de Productos")
                {
                    GenerarReporteSeleccionado();
                }
            }
        }

        private void DGVtabla1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verificamos que la columna actual sea la de "Alerta"
            if (DGVtabla1.Columns[e.ColumnIndex].Name == "Alerta")
            {
                if (e.Value != null && e.Value.ToString().Contains("STOCK BAJO"))
                {
                    // Pintamos la fila completa de rojo claro de alerta
                    DGVtabla1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
                    DGVtabla1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                }
            }
        }

        private void label27_Click(object sender, EventArgs e)
        {
            try
            {
                string rutaPDF = @"C:\Users\Lenovo\Desktop\PAYLESS\Interfaces de Usuario Propuestas Payless\Ayuda\Manual_Usuario.pdf";

                if (!File.Exists(rutaPDF))
                {
                    MessageBox.Show($"No se encontró el manual en la ruta:\n{rutaPDF}",
                                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Convertir la ruta del disco a formato URI web (maneja espacios y caracteres especiales)
                string uriPDF = new Uri(rutaPDF).AbsoluteUri;

                // 2. Cambia el número 8 por el número exacto de la página de Caja
                int numeroPaginaCaja = 8;

                // 3. Crear el argumento en formato file:///C:/...#page=2
                string argumentos = $"\"{uriPDF}#page={numeroPaginaCaja}\"";

                ProcessStartInfo edgeInfo = new ProcessStartInfo
                {
                    FileName = "msedge.exe",
                    Arguments = argumentos,
                    UseShellExecute = true
                };

                Process.Start(edgeInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el manual: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblMenu_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal(); ventana.Show(); this.Hide();
        }

        private void lblCaja_Click_1(object sender, EventArgs e)
        {
            Caja ventana = new Caja(); ventana.Show(); this.Hide();
        }

        private void lblUsuarios_Click_1(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario(); ventana.Show(); this.Hide();
        }

        private void lblCliente_Click_1(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente(); ventana.Show(); this.Hide();
        }

        private void lblProveedores_Click_1(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores(); ventana.Show(); this.Hide();
        }

        private void lblProductos_Click_1(object sender, EventArgs e)
        {
            Productos ventana = new Productos(); ventana.Show(); this.Hide();
        }

        private void lblCompras_Click_1(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo(); ventana.Show(); this.Hide();
        }

        private void lblVenta_Click_1(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas(); ventana.Show(); this.Hide();
        }

        private void lblCredito_Click_1(object sender, EventArgs e)
        {
            Credito ventana = new Credito(); ventana.Show(); this.Hide();
        }

        private void lblInventario_Click_1(object sender, EventArgs e)
        {
            inventario ventana = new inventario(); ventana.Show(); this.Hide();
        }

        private void lblMantenimiento_Click_1(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento(); ventana.Show(); this.Hide();
        }

        private void lblReportes_Click(object sender, EventArgs e)
        {
            Reportes ventana = new Reportes(); ventana.Show(); this.Hide();
        }

        private void lblAyuda_Click(object sender, EventArgs e)
        {
            try
            {
                string rutaPDF = @"C:\Users\Lenovo\Desktop\PAYLESS\Interfaces de Usuario Propuestas Payless\Ayuda\Manual_Usuario.pdf";

                if (!File.Exists(rutaPDF))
                {
                    MessageBox.Show($"No se encontró el manual en la ruta:\n{rutaPDF}",
                                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Convertir la ruta del disco a formato URI web (maneja espacios y caracteres especiales)
                string uriPDF = new Uri(rutaPDF).AbsoluteUri;

                // 2. Cambia el número 8 por el número exacto de la página de Caja
                int numeroPaginaCaja = 8;

                // 3. Crear el argumento en formato file:///C:/...#page=2
                string argumentos = $"\"{uriPDF}#page={numeroPaginaCaja}\"";

                ProcessStartInfo edgeInfo = new ProcessStartInfo
                {
                    FileName = "msedge.exe",
                    Arguments = argumentos,
                    UseShellExecute = true
                };

                Process.Start(edgeInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el manual: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Método auxiliar estricto para ocultar cualquier columna de ID en las tablas
        private void OcultarColumnasId()
        {
            foreach (DataGridViewColumn col in DGVtabla1.Columns)
            {
                string nombreCol = col.Name.ToLower();
                string headerCol = col.HeaderText.ToLower();

                if (nombreCol.StartsWith("id") || headerCol.StartsWith("id") || nombreCol.Contains("_id") || headerCol.Contains("id "))
                {
                    col.Visible = false;
                }
            }
        }

        private void btnImprimirSeleccionado_Click(object sender, EventArgs e)
        {
            EjecutarImpresion(false);
        }

        // Método auxiliar para clonar el DataTable y quitarle cualquier columna que sea un ID
        private static DataTable OcultarColumnasIdEnDataTable(DataTable datosOriginales)
        {
            DataTable dtLimpio = datosOriginales.Copy();

            // Recorremos las columnas de derecha a izquierda para eliminarlas con seguridad sin romper el índice
            for (int i = dtLimpio.Columns.Count - 1; i >= 0; i--)
            {
                string nombreColumna = dtLimpio.Columns[i].ColumnName;

                // Si la columna se llama "ID", empieza con "ID " o contiene "id" (case-insensitive), la eliminamos del PDF
                if (nombreColumna.Equals("ID", StringComparison.OrdinalIgnoreCase) ||
                    nombreColumna.StartsWith("ID_", StringComparison.OrdinalIgnoreCase) ||
                    nombreColumna.StartsWith("ID ", StringComparison.OrdinalIgnoreCase) ||
                    nombreColumna.IndexOf("id", StringComparison.OrdinalIgnoreCase) >= 0 && (nombreColumna.Length <= 5 || nombreColumna.StartsWith("id_")))
                {
                    // O si simplemente quieres buscar cualquier columna que contenga "ID":
                    // if (nombreColumna.IndexOf("ID", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // dtLimpio.Columns.RemoveAt(i);
                    }
                }
            }

            return dtLimpio;
        }
    }
}
