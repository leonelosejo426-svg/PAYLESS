using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless.Utilidades
{
    internal class GeneradorReporteCompraPDF
    {

        public static void Generar(DataTable tablaReporte, DateTime fechaDesde, DateTime fechaHasta, bool usarFechas, string usuarioFiltro, string estadoFiltro)
        {
            SaveFileDialog guardarArchivo = new SaveFileDialog();
            guardarArchivo.Filter = "Documento PDF (*.pdf)|*.pdf";
            guardarArchivo.FileName = "Reporte_De_Compras_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";

            if (guardarArchivo.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 1. Configurar documento PDF (Tamaño A4, márgenes de 30 puntos)
                    Document doc = new Document(PageSize.A4, 30f, 30f, 30f, 30f);
                    PdfWriter.GetInstance(doc, new FileStream(guardarArchivo.FileName, FileMode.Create));
                    doc.Open();

                    // 2. PALETA DE COLORES CORPORATIVA PAYLESS (Naranja #F26522 y Gris)
                    BaseColor colorNaranjaPayless = new BaseColor(242, 101, 34);
                    BaseColor colorTexto = new BaseColor(40, 40, 40);
                    BaseColor colorGrisClaro = new BaseColor(248, 249, 250);
                    BaseColor colorBorde = new BaseColor(220, 220, 220);

                    Font fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 15f, colorNaranjaPayless);
                    Font fuenteSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, BaseColor.GRAY);
                    Font fuenteEtiqueta = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8.5f, colorTexto);
                    Font fuenteTexto = FontFactory.GetFont(FontFactory.HELVETICA, 8.5f, colorTexto);
                    Font fuenteCabeceraTabla = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8.5f, BaseColor.WHITE);
                    Font fuenteTotalBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, colorNaranjaPayless);

                    // 3. ENCABEZADO Y LOGO
                    PdfPTable headerTable = new PdfPTable(2);
                    headerTable.WidthPercentage = 100f;
                    headerTable.SetWidths(new float[] { 50f, 50f });

                    PdfPCell cellLeft = new PdfPCell();
                    cellLeft.Border = Rectangle.NO_BORDER;

                    string rutaBase = Path.Combine(Application.StartupPath, "Respaldos", "WhatsApp Image 2026-06-17 at 11.49.19 AM");
                    string rutaLogo = File.Exists(rutaBase + ".jpeg") ? rutaBase + ".jpeg" : (File.Exists(rutaBase + ".jpg") ? rutaBase + ".jpg" : rutaBase);

                    if (File.Exists(rutaLogo))
                    {
                        Image logo = Image.GetInstance(rutaLogo);
                        logo.ScaleToFit(140f, 60f);
                        cellLeft.AddElement(logo);
                    }
                    else
                    {
                        cellLeft.AddElement(new Paragraph("PAYLESS SHOESOURCE", fuenteTitulo));
                        cellLeft.AddElement(new Paragraph("Calzado y Accesorios", fuenteSubtitulo));
                    }

                    headerTable.AddCell(cellLeft);

                    PdfPCell cellRight = new PdfPCell();
                    cellRight.Border = Rectangle.NO_BORDER;

                    Paragraph pTitulo = new Paragraph("REPORTE DE COMPRAS", fuenteTitulo);
                    pTitulo.Alignment = Element.ALIGN_RIGHT;
                    cellRight.AddElement(pTitulo);

                    Paragraph pFechaEmision = new Paragraph($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}", fuenteTexto);
                    pFechaEmision.Alignment = Element.ALIGN_RIGHT;
                    cellRight.AddElement(pFechaEmision);

                    headerTable.AddCell(cellRight);
                    doc.Add(headerTable);

                    // Línea divisora Naranja
                    doc.Add(new Paragraph(" "));
                    PdfPTable line = new PdfPTable(1);
                    line.WidthPercentage = 100f;
                    PdfPCell lineCell = new PdfPCell(new Phrase(""));
                    lineCell.BorderWidthBottom = 2f;
                    lineCell.BorderColorBottom = colorNaranjaPayless;
                    lineCell.Border = Rectangle.BOTTOM_BORDER;
                    line.AddCell(lineCell);
                    doc.Add(line);

                    doc.Add(new Paragraph(" ", FontFactory.GetFont(FontFactory.HELVETICA, 4f)));

                    // 4. FILTROS APLICADOS
                    PdfPTable infoTable = new PdfPTable(2);
                    infoTable.WidthPercentage = 100f;
                    infoTable.SetWidths(new float[] { 60f, 40f });

                    PdfPCell cellFiltros = new PdfPCell();
                    cellFiltros.Border = Rectangle.NO_BORDER;
                    string periodoTexto = usarFechas ? $"{fechaDesde:dd/MM/yyyy} al {fechaHasta:dd/MM/yyyy}" : "Todos los registros (Histórico)";
                    cellFiltros.AddElement(new Phrase($"Período: {periodoTexto}", fuenteEtiqueta));
                    cellFiltros.AddElement(new Phrase($"Usuario: {usuarioFiltro} | Estado: {estadoFiltro}", fuenteTexto));
                    infoTable.AddCell(cellFiltros);

                    PdfPCell cellTotalRegistros = new PdfPCell();
                    cellTotalRegistros.Border = Rectangle.NO_BORDER;
                    Paragraph pReg = new Paragraph($"Total Registros: {tablaReporte.Rows.Count}", fuenteEtiqueta);
                    pReg.Alignment = Element.ALIGN_RIGHT;
                    cellTotalRegistros.AddElement(pReg);
                    infoTable.AddCell(cellTotalRegistros);

                    doc.Add(infoTable);
                    doc.Add(new Paragraph(" \n"));

                    // 5. TABLA DE DATOS DE COMPRAS (6 COLUMNAS)
                    PdfPTable tabla = new PdfPTable(6);
                    tabla.WidthPercentage = 100f;
                    tabla.SetWidths(new float[] { 14f, 20f, 26f, 20f, 12f, 8f });

                    string[] encabezados = { "Código", "Fecha", "Proveedor", "Registrado por", "Total", "Estado" };
                    foreach (string enc in encabezados)
                    {
                        PdfPCell cellHeader = new PdfPCell(new Phrase(enc, fuenteCabeceraTabla));
                        cellHeader.BackgroundColor = colorNaranjaPayless;
                        cellHeader.HorizontalAlignment = (enc == "Proveedor" || enc == "Registrado por") ? Element.ALIGN_LEFT : Element.ALIGN_CENTER;
                        cellHeader.Padding = 5f;
                        cellHeader.BorderColor = colorNaranjaPayless;
                        tabla.AddCell(cellHeader);
                    }

                    bool esGris = false;
                    decimal sumaTotalGeneral = 0;

                    foreach (DataRow fila in tablaReporte.Rows)
                    {
                        BaseColor bgFila = esGris ? colorGrisClaro : BaseColor.WHITE;

                        AgregarCeldaTabla(tabla, fila["Código"].ToString(), fuenteTexto, bgFila, colorBorde, Element.ALIGN_CENTER);

                        string fechaFormateada = Convert.ToDateTime(fila["Fecha"]).ToString("dd/MM/yyyy HH:mm");
                        AgregarCeldaTabla(tabla, fechaFormateada, fuenteTexto, bgFila, colorBorde, Element.ALIGN_CENTER);

                        AgregarCeldaTabla(tabla, fila["Proveedor"].ToString(), fuenteTexto, bgFila, colorBorde, Element.ALIGN_LEFT);
                        AgregarCeldaTabla(tabla, fila["Usuario"].ToString(), fuenteTexto, bgFila, colorBorde, Element.ALIGN_LEFT);

                        decimal totalFila = Convert.ToDecimal(fila["Total"]);
                        sumaTotalGeneral += totalFila;
                        AgregarCeldaTabla(tabla, $"C$ {totalFila:N2}", fuenteTexto, bgFila, colorBorde, Element.ALIGN_RIGHT);

                        AgregarCeldaTabla(tabla, fila["Estado"].ToString(), fuenteTexto, bgFila, colorBorde, Element.ALIGN_CENTER);

                        esGris = !esGris;
                    }

                    doc.Add(tabla);
                    doc.Add(new Paragraph(" "));

                    // 6. TOTAL GENERAL ACUMULADO
                    PdfPTable tablaTotales = new PdfPTable(2);
                    tablaTotales.WidthPercentage = 45f;
                    tablaTotales.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tablaTotales.SetWidths(new float[] { 45f, 55f });

                    AgregarFilaTotal(tablaTotales, "TOTAL COMPRAS:", $"C$ {sumaTotalGeneral:N2}", fuenteTotalBold, colorNaranjaPayless, colorBorde);

                    doc.Add(tablaTotales);

                    // 7. PIE DE PÁGINA
                    doc.Add(new Paragraph(" \n"));
                    Paragraph leyenda = new Paragraph(
                        "Payless ShoeSource Nicaragua - Control de Compras e Inventario\n" +
                        "Este reporte es un comprobante informativo de auditoría interna.", fuenteSubtitulo)
                    {
                        Alignment = Element.ALIGN_CENTER
                    };

                    doc.Add(leyenda);
                    doc.Close();

                    MessageBox.Show("Reporte de compras exportado a PDF correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Diagnostics.Process.Start(guardarArchivo.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el PDF de compras: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static void AgregarCeldaTabla(PdfPTable tabla, string texto, Font fuente, BaseColor backgroundColor, BaseColor borderColor, int alignment)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texto, fuente))
            {
                BackgroundColor = backgroundColor,
                HorizontalAlignment = alignment,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Padding = 5f,
                BorderColor = borderColor
            };
            tabla.AddCell(cell);
        }

        private static void AgregarFilaTotal(PdfPTable tabla, string etiqueta, string valor, Font fuente, BaseColor textColor, BaseColor borderColor)
        {
            PdfPCell cell1 = new PdfPCell(new Phrase(etiqueta, fuente))
            {
                BorderColor = borderColor,
                Padding = 5f,
                HorizontalAlignment = Element.ALIGN_RIGHT
            };
            tabla.AddCell(cell1);

            PdfPCell cell2 = new PdfPCell(new Phrase(valor, fuente))
            {
                BorderColor = borderColor,
                Padding = 5f,
                HorizontalAlignment = Element.ALIGN_RIGHT
            };
            tabla.AddCell(cell2);
        }

    }
}
