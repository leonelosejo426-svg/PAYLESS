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

        private DataTable tablaReporte;

        public Reportes()
        {
            InitializeComponent();
        }

        private void Reportes_Load(object sender, EventArgs e)
        {
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

            // Cargar automáticamente el reporte seleccionado
            GenerarReporteSeleccionado();
        }

        private void CargarTiposReporte()
        {
            cbTipoReporte.Items.Clear();

            cbTipoReporte.Items.Add(
                "Reporte de Caja");

            cbTipoReporte.Items.Add(
                "Reporte de Ventas");

            cbTipoReporte.Items.Add(
                "Reporte de Inventario");

            cbTipoReporte.SelectedIndex = 0;
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
            if (DGVtabla1.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione una caja para imprimir.",
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

            try
            {
                int idCaja =
                    Convert.ToInt32(
                        DGVtabla1.CurrentRow
                        .Cells["ID Caja"]
                        .Value);

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
            GenerarReporteSeleccionado();
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
                return;

            string reporte =
                cbTipoReporte.SelectedItem.ToString();

            if (reporte == "Reporte de Caja")
            {
                GenerarReporteCaja();
            }
            else
            {
                DGVtabla1.DataSource = null;

                btnImprimir.Enabled = false;
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

    }
}
