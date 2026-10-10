using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;
using System.Diagnostics;
using iTextDocument = iTextSharp.text.Document;
using iTextParagraph = iTextSharp.text.Paragraph;
using iTextFont = iTextSharp.text.Font;
using iTextElement = iTextSharp.text.Element;
using iTextBaseColor = iTextSharp.text.BaseColor;
using iTextImage = iTextSharp.text.Image;
using iTextPdfPTable = iTextSharp.text.pdf.PdfPTable;
using iTextPdfPCell = iTextSharp.text.pdf.PdfPCell;
using iTextPdfWriter = iTextSharp.text.pdf.PdfWriter;
using iTextPdfContentByte = iTextSharp.text.pdf.PdfContentByte;
using iTextPhrase = iTextSharp.text.Phrase;

using System.IO;
namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Caja : Form
    {
        private CajaDAO cajaDAO = new CajaDAO();

        private int idCajaActual = 0;

        private decimal saldoInicial = 0;
        private decimal totalIngresos = 0;
        private decimal totalEgresos = 0;
        private decimal saldoEsperado = 0;


        public Caja()
        {
            InitializeComponent();

          
        }

        private void Caja_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = ClaseSesion.UsuarioActual;

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

                    break;

                case "Cajero":

                    lblCaja.Enabled = true;
                    lblVenta.Enabled = true;

                    break;
            }

            CargarCaja();
        }

        private void CargarCaja()
        {
            DataTable tabla =
                cajaDAO.ObtenerCajaAbierta();

            if (tabla.Rows.Count == 0)
            {
                idCajaActual = 0;

                lblSaldoInicial.Text = "C$ 0.00";
                lblTotalIngresos.Text = "C$ 0.00";
                lblTotalEgresos.Text = "C$ 0.00";
                lblSaldoEsperado.Text = "C$ 0.00";

                DGVMovimientosCaja.DataSource = null;

                return;
            }

            DataRow fila = tabla.Rows[0];

            idCajaActual =
                Convert.ToInt32(
                    fila["id_caja"]);

            saldoInicial =
                Convert.ToDecimal(
                    fila["saldo_inicial"]);

            CargarResumen();

            CargarMovimientos();
        }


        // CARGAR RESUMEN
        // =====================================================
        private void CargarResumen()
        {
            totalIngresos =
                cajaDAO.ObtenerTotalIngresos(
                    idCajaActual);

            totalEgresos =
                cajaDAO.ObtenerTotalEgresos(
                    idCajaActual);

            saldoEsperado =
                saldoInicial
                + totalIngresos
                - totalEgresos;

            lblSaldoInicial.Text =
                "C$ " +
                saldoInicial.ToString("N2");

            lblTotalIngresos.Text =
                "C$ " +
                totalIngresos.ToString("N2");

            lblTotalEgresos.Text =
                "C$ " +
                totalEgresos.ToString("N2");

            lblSaldoEsperado.Text =
                "C$ " +
                saldoEsperado.ToString("N2");
        }


        // CARGAR MOVIMIENTOS
        // =====================================================
        private void CargarMovimientos()
        {
            if (idCajaActual == 0)
            {
                DGVMovimientosCaja.DataSource = null;
                return;
            }

            DataTable tabla =
                cajaDAO.ObtenerMovimientosCaja(
                    idCajaActual);

            DGVMovimientosCaja.DataSource =
                tabla;

            ConfigurarGrid();
        }


        // CONFIGURAR DATAGRIDVIEW
        // =====================================================
        private void ConfigurarGrid()
        {
            if (DGVMovimientosCaja.Columns.Count == 0)
                return;

            DGVMovimientosCaja.Columns["tipo"]
                .HeaderText = "Tipo";

            DGVMovimientosCaja.Columns["concepto"]
                .HeaderText = "Concepto";

            DGVMovimientosCaja.Columns["monto"]
                .HeaderText = "Monto";

            DGVMovimientosCaja.Columns["fecha_hora"]
                .HeaderText = "Fecha y Hora";

            DGVMovimientosCaja.Columns["monto"]
                .DefaultCellStyle.Format = "C2";

            DGVMovimientosCaja.Columns["fecha_hora"]
                .DefaultCellStyle.Format =
                "dd/MM/yyyy HH:mm:ss";

            DGVMovimientosCaja.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            DGVMovimientosCaja.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            DGVMovimientosCaja.MultiSelect = false;

            DGVMovimientosCaja.ReadOnly = true;

            DGVMovimientosCaja.AllowUserToAddRows =
                false;
        }






        private void button2_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo();
            ventana.Show();
            this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            inventario ventana = new inventario();
            ventana.Show();
            this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito();
            ventana.Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnAperturadecaja_Click(object sender, EventArgs e)
        {
            AperturaCaja ventana =
                new AperturaCaja(
                    ClaseSesion.IdUsuario,
                    ClaseSesion.UsuarioActual);

            ventana.ShowDialog();

            this.Hide();
        }

        private void btnArqueodecaja_Click(object sender, EventArgs e)
        {

            if (idCajaActual == 0)
            {
                MessageBox.Show(
                    "No hay una caja abierta.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            ArqueodeCaja formulario =
                new ArqueodeCaja();

            formulario.ShowDialog();

            CargarCaja();
        }

        private void btnCierredecaja_Click(object sender, EventArgs e)
        {


            if (idCajaActual == 0)
            {
                MessageBox.Show(
                    "No hay una caja abierta.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            CierredeCaja formulario =
                new CierredeCaja();

            formulario.ShowDialog();

            CargarCaja();
        }

        private void label25_Click(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento();
            ventana.Show();
            this.Hide();
        }

        private void groupBox3_Enter_1(object sender, EventArgs e)
        {

        }

        private void btnGuardarMovimiento_Click(object sender, EventArgs e)
        {
           

           
          
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (idCajaActual == 0)
            {
                MessageBox.Show(
                    "No hay una caja abierta para imprimir.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                // =====================================================
                // CREAR CARPETA DE REPORTES
                // =====================================================

                string carpeta =
                    Path.Combine(
                        Application.StartupPath,
                        "Reportes");

                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }

                // =====================================================
                // NOMBRE DEL ARCHIVO
                // =====================================================

                string nombreArchivo =
                    "Reporte_Caja_" +
                    idCajaActual +
                    "_" +
                    DateTime.Now.ToString("yyyyMMdd_HHmmss") +
                    ".pdf";

                string ruta =
                    Path.Combine(
                        carpeta,
                        nombreArchivo);

                // =====================================================
                // CREAR DOCUMENTO
                // =====================================================

                iTextDocument documento =
                    new iTextDocument(
                        iTextSharp.text.PageSize.A4,
                        40,
                        40,
                        50,
                        50);

                iTextPdfWriter writer =
                    iTextPdfWriter.GetInstance(
                        documento,
                        new FileStream(
                            ruta,
                            FileMode.Create));

                documento.Open();

                // =====================================================
                // FUENTES
                // =====================================================

                iTextFont titulo =
                    iTextSharp.text.FontFactory.GetFont(
                        iTextSharp.text.FontFactory.HELVETICA_BOLD,
                        20);

                iTextFont subtitulo =
                    iTextSharp.text.FontFactory.GetFont(
                        iTextSharp.text.FontFactory.HELVETICA_BOLD,
                        13);

                iTextFont normal =
                    iTextSharp.text.FontFactory.GetFont(
                        iTextSharp.text.FontFactory.HELVETICA,
                        10);

                iTextFont negrita =
                    iTextSharp.text.FontFactory.GetFont(
                        iTextSharp.text.FontFactory.HELVETICA_BOLD,
                        10);

                iTextFont pie =
                    iTextSharp.text.FontFactory.GetFont(
                        iTextSharp.text.FontFactory.HELVETICA_OBLIQUE,
                        9,
                        iTextBaseColor.GRAY);

                // =====================================================
                // MARCA DE AGUA
                // =====================================================

                iTextPdfContentByte canvas =
                    writer.DirectContentUnder;

                iTextFont fuenteMarcaAgua =
                    iTextSharp.text.FontFactory.GetFont(
                        iTextSharp.text.FontFactory.HELVETICA_BOLD,
                        55,
                        iTextBaseColor.LIGHT_GRAY);

                iTextPhrase fraseMarcaAgua =
                    new iTextPhrase(
                        "PAYLESS",
                        fuenteMarcaAgua);

                iTextSharp.text.pdf.ColumnText.ShowTextAligned(
                    canvas,
                    iTextElement.ALIGN_CENTER,
                    fraseMarcaAgua,
                    300,
                    400,
                    45);

                // =====================================================
                // LOGO
                // =====================================================

                string rutaLogo =
                    Path.Combine(
                        Application.StartupPath,
                        "Imagenes",
                        "logo.png");

                if (File.Exists(rutaLogo))
                {
                    iTextImage logo =
                        iTextImage.GetInstance(
                            rutaLogo);

                    logo.Alignment =
                        iTextElement.ALIGN_CENTER;

                    logo.ScaleToFit(
                        120f,
                        60f);

                    documento.Add(logo);
                }

                // =====================================================
                // ENCABEZADO
                // =====================================================

                iTextParagraph encabezado =
                    new iTextParagraph(
                        "PAYLESS SHOESOURCE",
                        titulo);

                encabezado.Alignment =
                    iTextElement.ALIGN_CENTER;

                documento.Add(encabezado);

                iTextParagraph reporte =
                    new iTextParagraph(
                        "REPORTE DE CAJA",
                        subtitulo);

                reporte.Alignment =
                    iTextElement.ALIGN_CENTER;

                documento.Add(reporte);

                documento.Add(
                    new iTextParagraph("\n"));

                // =====================================================
                // INFORMACIÓN GENERAL
                // =====================================================

                documento.Add(
                    new iTextParagraph(
                        "Número de caja: " +
                        idCajaActual,
                        normal));

                documento.Add(
                    new iTextParagraph(
                        "Usuario: " +
                        lblUsuario.Text,
                        normal));

                documento.Add(
                    new iTextParagraph(
                        "Fecha del reporte: " +
                        DateTime.Now.ToString(
                            "dd/MM/yyyy HH:mm:ss"),
                        normal));

                documento.Add(
                    new iTextParagraph("\n"));

                // =====================================================
                // RESUMEN DE CAJA
                // =====================================================

                documento.Add(
                    new iTextParagraph(
                        "RESUMEN DE CAJA",
                        subtitulo));

                iTextPdfPTable tablaResumen =
                    new iTextPdfPTable(2);

                tablaResumen.WidthPercentage = 100;

                tablaResumen.AddCell(
                    new iTextPdfPCell(
                        new iTextPhrase(
                            "Concepto",
                            negrita)));

                tablaResumen.AddCell(
                    new iTextPdfPCell(
                        new iTextPhrase(
                            "Monto",
                            negrita)));

                tablaResumen.AddCell(
                    "Saldo inicial");

                tablaResumen.AddCell(
                    lblSaldoInicial.Text);

                tablaResumen.AddCell(
                    "Total ingresos");

                tablaResumen.AddCell(
                    lblTotalIngresos.Text);

                tablaResumen.AddCell(
                    "Total egresos");

                tablaResumen.AddCell(
                    lblTotalEgresos.Text);

                tablaResumen.AddCell(
                    "Saldo esperado");

                tablaResumen.AddCell(
                    lblSaldoEsperado.Text);

                documento.Add(
                    tablaResumen);

                documento.Add(
                    new iTextParagraph("\n"));

                // =====================================================
                // MOVIMIENTOS
                // =====================================================

                documento.Add(
                    new iTextParagraph(
                        "MOVIMIENTOS DE CAJA",
                        subtitulo));

                iTextPdfPTable tablaMovimientos =
                    new iTextPdfPTable(4);

                tablaMovimientos.WidthPercentage = 100;

                tablaMovimientos.SetWidths(
                    new float[]
                    {
                1.2f,
                2.5f,
                1.5f,
                2.2f
                    });

                tablaMovimientos.AddCell(
                    new iTextPdfPCell(
                        new iTextPhrase(
                            "Tipo",
                            negrita)));

                tablaMovimientos.AddCell(
                    new iTextPdfPCell(
                        new iTextPhrase(
                            "Concepto",
                            negrita)));

                tablaMovimientos.AddCell(
                    new iTextPdfPCell(
                        new iTextPhrase(
                            "Monto",
                            negrita)));

                tablaMovimientos.AddCell(
                    new iTextPdfPCell(
                        new iTextPhrase(
                            "Fecha y Hora",
                            negrita)));

                // =====================================================
                // RECORRER MOVIMIENTOS
                // =====================================================

                foreach (
                    DataGridViewRow fila
                    in DGVMovimientosCaja.Rows)
                {
                    if (fila.IsNewRow)
                        continue;

                    string tipo =
                        fila.Cells["tipo"].Value == null
                        ? ""
                        : fila.Cells["tipo"].Value.ToString();

                    string concepto =
                        fila.Cells["concepto"].Value == null
                        ? ""
                        : fila.Cells["concepto"].Value.ToString();

                    string monto = "C$ 0.00";

                    if (fila.Cells["monto"].Value != null &&
                        fila.Cells["monto"].Value != DBNull.Value)
                    {
                        decimal valor =
                            Convert.ToDecimal(
                                fila.Cells["monto"].Value);

                        monto =
                            "C$ " +
                            valor.ToString("N2");
                    }

                    string fecha = "";

                    if (fila.Cells["fecha_hora"].Value != null &&
                        fila.Cells["fecha_hora"].Value != DBNull.Value)
                    {
                        fecha =
                            Convert.ToDateTime(
                                fila.Cells["fecha_hora"].Value)
                            .ToString(
                                "dd/MM/yyyy HH:mm");
                    }

                    tablaMovimientos.AddCell(tipo);
                    tablaMovimientos.AddCell(concepto);
                    tablaMovimientos.AddCell(monto);
                    tablaMovimientos.AddCell(fecha);
                }

                documento.Add(
                    tablaMovimientos);

                documento.Add(
                    new iTextParagraph("\n"));

                // =====================================================
                // INFORMACIÓN DE PAYLESS
                // =====================================================

                iTextParagraph informacionEmpresa =
                    new iTextParagraph(
                        "Payless ShoeSource Nicaragua\n" +
                        "Dirección: Managua, Nicaragua\n" +
                        "Teléfono: +505 2222-0000\n" +
                        "Documento generado por el sistema Payless\n" +
                        "¡Gracias por su trabajo!",
                        pie);

                informacionEmpresa.Alignment =
                    iTextElement.ALIGN_CENTER;

                documento.Add(
                    informacionEmpresa);

                // =====================================================
                // CERRAR DOCUMENTO
                // =====================================================

                documento.Close();

                // =====================================================
                // ABRIR PDF
                // =====================================================

                Process.Start(ruta);

                MessageBox.Show(
                    "Reporte de caja generado correctamente.",
                    "Imprimir",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo generar el reporte de caja.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            EgresosCaja formulario =
               new EgresosCaja();

            formulario.ShowDialog();

            CargarCaja();
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

                // 2. Cambia el número 2 por el número exacto de la página de Caja
                int numeroPaginaCaja = 2;

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

        private void lblReportes_Click(object sender, EventArgs e)
        {
            Reportes ventana = new Reportes();
            ventana.Show();
            this.Hide();
        }
    }
}
