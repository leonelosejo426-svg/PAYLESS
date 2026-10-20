using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;
using iTextBaseColor = iTextSharp.text.BaseColor;
using iTextDocument = iTextSharp.text.Document;
using iTextElement = iTextSharp.text.Element;
using iTextFont = iTextSharp.text.Font;
using iTextImage = iTextSharp.text.Image;
using iTextParagraph = iTextSharp.text.Paragraph;
using iTextPdfContentByte = iTextSharp.text.pdf.PdfContentByte;
using iTextPdfPCell = iTextSharp.text.pdf.PdfPCell;
using iTextPdfPTable = iTextSharp.text.pdf.PdfPTable;
using iTextPdfWriter = iTextSharp.text.pdf.PdfWriter;
using iTextPhrase = iTextSharp.text.Phrase;
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

        private ArqueoCajaDAO arqueoDAO = new ArqueoCajaDAO();


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
                cajaDAO.ObtenerCajaAbierta(ClaseSesion.IdUsuario);

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
                    "No hay una caja abierta para realizar el arqueo.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Verificar si ya existe un arqueo guardado
            bool yaTieneArqueo = arqueoDAO.TieneArqueoRegistrado(idCajaActual);

            if (yaTieneArqueo)
            {
                DialogResult respuesta = MessageBox.Show(
                    "Esta caja ya cuenta con un arqueo registrado.\n\n" +
                    "¿Desea reevaluar o modificar el arqueo guardado?",
                    "Arqueo existente",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                {
                    return;
                }
            }

            ArqueodeCaja formulario = new ArqueodeCaja();
            formulario.ShowDialog();

            CargarCaja();
        }

        private void btnCierredecaja_Click(object sender, EventArgs e)
        {
            // 1. Validar que exista una caja abierta
            if (idCajaActual == 0)
            {
                MessageBox.Show(
                    "No hay ninguna caja abierta para realizar el cierre.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // 2. Validar que la caja actual PRIMERO haya realizado el arqueo
            bool yaTieneArqueo = arqueoDAO.TieneArqueoRegistrado(idCajaActual);

            if (!yaTieneArqueo)
            {
                MessageBox.Show(
                    "Debe realizar el Arqueo de Caja antes de proceder al Cierre definitivo.",
                    "Arqueo Requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                return;
            }

            // 3. Abrir el formulario de Cierre de Caja
            CierredeCaja formulario = new CierredeCaja();
            formulario.ShowDialog();

            // 4. Recargar el estado de la caja en el formulario principal
            // (Al cerrar la caja en la BD con estado_caja = 'Cerrada', CargarCaja() detectará idCajaActual = 0 y deshabilitará las acciones)
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
                string carpeta = Path.Combine(Application.StartupPath, "Reportes");
                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }

                // =====================================================
                // NOMBRE Y RUTA DEL ARCHIVO
                // =====================================================
                string nombreArchivo = "Reporte_Caja_" + idCajaActual + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
                string ruta = Path.Combine(carpeta, nombreArchivo);

                // =====================================================
                // CONFIGURAR DOCUMENTO PDF (Márgenes profesionales)
                // =====================================================
                iTextSharp.text.Document documento = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 30f, 30f, 30f, 30f);
                iTextSharp.text.pdf.PdfWriter.GetInstance(documento, new FileStream(ruta, FileMode.Create));
                documento.Open();

                // =====================================================
                // PALETA DE COLORES CORPORATIVA PAYLESS
                // =====================================================
                iTextSharp.text.BaseColor colorNaranjaPayless = new iTextSharp.text.BaseColor(242, 101, 34);
                iTextSharp.text.BaseColor colorTexto = new iTextSharp.text.BaseColor(40, 40, 40);
                iTextSharp.text.BaseColor colorGrisClaro = new iTextSharp.text.BaseColor(248, 249, 250);
                iTextSharp.text.BaseColor colorBorde = new iTextSharp.text.BaseColor(220, 220, 220);

                iTextSharp.text.Font fuenteTitulo = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 15f, colorNaranjaPayless);
                iTextSharp.text.Font fuenteSubtitulo = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 9f, iTextSharp.text.BaseColor.GRAY);
                iTextSharp.text.Font fuenteEtiqueta = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 8.5f, colorTexto);
                iTextSharp.text.Font fuenteTexto = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 8.5f, colorTexto);
                iTextSharp.text.Font fuenteCabeceraTabla = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 8.5f, iTextSharp.text.BaseColor.WHITE);

                // =====================================================
                // ENCABEZADO Y LOGO (Diseño Dos Columnas)
                // =====================================================
                iTextSharp.text.pdf.PdfPTable headerTable = new iTextSharp.text.pdf.PdfPTable(2);
                headerTable.WidthPercentage = 100f;
                headerTable.SetWidths(new float[] { 50f, 50f });

                iTextSharp.text.pdf.PdfPCell cellLeft = new iTextSharp.text.pdf.PdfPCell();
                cellLeft.Border = iTextSharp.text.Rectangle.NO_BORDER;

                string rutaBase = Path.Combine(Application.StartupPath, "Respaldos", "WhatsApp Image 2026-06-17 at 11.49.19 AM");
                string rutaLogo = File.Exists(rutaBase + ".jpeg") ? rutaBase + ".jpeg" : (File.Exists(rutaBase + ".jpg") ? rutaBase + ".jpg" : Path.Combine(Application.StartupPath, "Imagenes", "logo.png"));

                if (File.Exists(rutaLogo))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(rutaLogo);
                    logo.ScaleToFit(140f, 60f);
                    cellLeft.AddElement(logo);
                }
                else
                {
                    cellLeft.AddElement(new iTextSharp.text.Paragraph("PAYLESS SHOESOURCE", fuenteTitulo));
                    cellLeft.AddElement(new iTextSharp.text.Paragraph("Calzado y Accesorios", fuenteSubtitulo));
                }

                headerTable.AddCell(cellLeft);

                iTextSharp.text.pdf.PdfPCell cellRight = new iTextSharp.text.pdf.PdfPCell();
                cellRight.Border = iTextSharp.text.Rectangle.NO_BORDER;

                iTextSharp.text.Paragraph pTitulo = new iTextSharp.text.Paragraph("REPORTE DE CAJA", fuenteTitulo);
                pTitulo.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
                cellRight.AddElement(pTitulo);

                iTextSharp.text.Paragraph pFechaEmision = new iTextSharp.text.Paragraph($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}", fuenteTexto);
                pFechaEmision.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
                cellRight.AddElement(pFechaEmision);

                headerTable.AddCell(cellRight);
                documento.Add(headerTable);

                // Línea divisora Naranja Corporativa
                documento.Add(new iTextSharp.text.Paragraph(" "));
                iTextSharp.text.pdf.PdfPTable line = new iTextSharp.text.pdf.PdfPTable(1);
                line.WidthPercentage = 100f;
                iTextSharp.text.pdf.PdfPCell lineCell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(""));
                lineCell.BorderWidthBottom = 2f;
                lineCell.BorderColorBottom = colorNaranjaPayless;
                lineCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                line.AddCell(lineCell);
                documento.Add(line);

                documento.Add(new iTextSharp.text.Paragraph(" ", iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 4f)));

                // =====================================================
                // INFORMACIÓN GENERAL DE LA CAJA
                // =====================================================
                iTextSharp.text.pdf.PdfPTable infoTable = new iTextSharp.text.pdf.PdfPTable(2);
                infoTable.WidthPercentage = 100f;
                infoTable.SetWidths(new float[] { 60f, 40f });

                iTextSharp.text.pdf.PdfPCell cellInfoCaja = new iTextSharp.text.pdf.PdfPCell();
                cellInfoCaja.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellInfoCaja.AddElement(new iTextSharp.text.Phrase($"Número de Caja: {idCajaActual}", fuenteEtiqueta));
                cellInfoCaja.AddElement(new iTextSharp.text.Phrase($"Cajero / Usuario: {lblUsuario.Text}", fuenteTexto));
                infoTable.AddCell(cellInfoCaja);

                iTextSharp.text.pdf.PdfPCell cellInfoFecha = new iTextSharp.text.pdf.PdfPCell();
                cellInfoFecha.Border = iTextSharp.text.Rectangle.NO_BORDER;
                iTextSharp.text.Paragraph pReg = new iTextSharp.text.Paragraph($"Fecha y Hora: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", fuenteEtiqueta);
                pReg.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
                cellInfoFecha.AddElement(pReg);
                infoTable.AddCell(cellInfoFecha);

                documento.Add(infoTable);
                documento.Add(new iTextSharp.text.Paragraph(" \n"));

                // =====================================================
                // RESUMEN FINANCIERO DE CAJA (Tabla Estilizada)
                // =====================================================
                iTextSharp.text.Paragraph pSubResumen = new iTextSharp.text.Paragraph("RESUMEN FINANCIERO", fuenteTitulo);
                pSubResumen.Font.Size = 11f;
                documento.Add(pSubResumen);
                documento.Add(new iTextSharp.text.Paragraph(" ", iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 4f)));

                iTextSharp.text.pdf.PdfPTable tablaResumen = new iTextSharp.text.pdf.PdfPTable(2);
                tablaResumen.WidthPercentage = 100f;
                tablaResumen.SetWidths(new float[] { 60f, 40f });

                string[,] datosResumen = {
            { "Saldo Inicial", lblSaldoInicial.Text },
            { "Total Ingresos", lblTotalIngresos.Text },
            { "Total Egresos", lblTotalEgresos.Text },
            { "Saldo Esperado", lblSaldoEsperado.Text }
        };

                for (int i = 0; i < datosResumen.GetLength(0); i++)
                {
                    iTextSharp.text.BaseColor bg = (i % 2 == 0) ? colorGrisClaro : iTextSharp.text.BaseColor.WHITE;

                    AgregarCeldaTabla(tablaResumen, datosResumen[i, 0], fuenteTexto, bg, colorBorde, iTextSharp.text.Element.ALIGN_LEFT);
                    AgregarCeldaTabla(tablaResumen, datosResumen[i, 1], fuenteTexto, bg, colorBorde, iTextSharp.text.Element.ALIGN_RIGHT);
                }

                documento.Add(tablaResumen);
                documento.Add(new iTextSharp.text.Paragraph(" \n"));

                // =====================================================
                // TABLA DE MOVIMIENTOS DE CAJA (4 Columnas)
                // =====================================================
                iTextSharp.text.Paragraph pSubMov = new iTextSharp.text.Paragraph("MOVIMIENTOS REGISTRADOS", fuenteTitulo);
                pSubMov.Font.Size = 11f;
                documento.Add(pSubMov);
                documento.Add(new iTextSharp.text.Paragraph(" ", iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 4f)));

                iTextSharp.text.pdf.PdfPTable tablaMovimientos = new iTextSharp.text.pdf.PdfPTable(4);
                tablaMovimientos.WidthPercentage = 100f;
                tablaMovimientos.SetWidths(new float[] { 15f, 35f, 25f, 25f });

                string[] encabezadosMov = { "Tipo", "Concepto", "Monto", "Fecha y Hora" };
                foreach (string enc in encabezadosMov)
                {
                    iTextSharp.text.pdf.PdfPCell cellHeader = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(enc, fuenteCabeceraTabla));
                    cellHeader.BackgroundColor = colorNaranjaPayless;
                    cellHeader.HorizontalAlignment = (enc == "Monto") ? iTextSharp.text.Element.ALIGN_RIGHT : iTextSharp.text.Element.ALIGN_CENTER;
                    cellHeader.Padding = 5f;
                    cellHeader.BorderColor = colorNaranjaPayless;
                    tablaMovimientos.AddCell(cellHeader);
                }

                bool esGrisMov = false;
                foreach (DataGridViewRow fila in DGVMovimientosCaja.Rows)
                {
                    if (fila.IsNewRow) continue;

                    iTextSharp.text.BaseColor bgFila = esGrisMov ? colorGrisClaro : iTextSharp.text.BaseColor.WHITE;

                    string tipo = fila.Cells["tipo"].Value?.ToString() ?? "";
                    string concepto = fila.Cells["concepto"].Value?.ToString() ?? "";

                    string montoStr = "C$ 0.00";
                    if (fila.Cells["monto"].Value != null && fila.Cells["monto"].Value != DBNull.Value)
                    {
                        decimal valor = Convert.ToDecimal(fila.Cells["monto"].Value);
                        montoStr = "C$ " + valor.ToString("N2");
                    }

                    string fechaStr = "";
                    if (fila.Cells["fecha_hora"].Value != null && fila.Cells["fecha_hora"].Value != DBNull.Value)
                    {
                        fechaStr = Convert.ToDateTime(fila.Cells["fecha_hora"].Value).ToString("dd/MM/yyyy HH:mm");
                    }

                    AgregarCeldaTabla(tablaMovimientos, tipo, fuenteTexto, bgFila, colorBorde, iTextSharp.text.Element.ALIGN_CENTER);
                    AgregarCeldaTabla(tablaMovimientos, concepto, fuenteTexto, bgFila, colorBorde, iTextSharp.text.Element.ALIGN_LEFT);
                    AgregarCeldaTabla(tablaMovimientos, montoStr, fuenteTexto, bgFila, colorBorde, iTextSharp.text.Element.ALIGN_RIGHT);
                    AgregarCeldaTabla(tablaMovimientos, fechaStr, fuenteTexto, bgFila, colorBorde, iTextSharp.text.Element.ALIGN_CENTER);

                    esGrisMov = !esGrisMov;
                }

                documento.Add(tablaMovimientos);
                documento.Add(new iTextSharp.text.Paragraph(" \n"));

                // =====================================================
                // PIE DE PÁGINA CORPORATIVO
                // =====================================================
                iTextSharp.text.Paragraph leyenda = new iTextSharp.text.Paragraph(
                    "Payless ShoeSource Nicaragua - Control de Caja e Ingresos\n" +
                    "Este reporte es un comprobante informativo de auditoría interna.", fuenteSubtitulo)
                {
                    Alignment = iTextSharp.text.Element.ALIGN_CENTER
                };

                documento.Add(leyenda);
                documento.Close();

                // =====================================================
                // ABRIR PDF AUTOMÁTICAMENTE
                // =====================================================
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(ruta) { UseShellExecute = true });

                MessageBox.Show(
                    "Reporte de caja generado correctamente.",
                    "Imprimir",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo generar el reporte de caja.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Método auxiliar reutilizable
        private static void AgregarCeldaTabla(iTextSharp.text.pdf.PdfPTable tabla, string texto, iTextSharp.text.Font fuente, iTextSharp.text.BaseColor backgroundColor, iTextSharp.text.BaseColor borderColor, int alignment)
        {
            iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(texto, fuente))
            {
                BackgroundColor = backgroundColor,
                HorizontalAlignment = alignment,
                VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,
                Padding = 5f,
                BorderColor = borderColor
            };
            tabla.AddCell(cell);
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            // 1. Validar si hay una caja abierta
            if (idCajaActual == 0)
            {
                MessageBox.Show(
                    "No hay ninguna caja abierta para registrar egresos.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // 2. Abrir la pantalla de Egresos
            EgresosCaja formulario = new EgresosCaja();
            formulario.ShowDialog();

            // 3. Recargar los datos de la caja por si se registró un nuevo egreso
            CargarCaja();
        }
    }
}
