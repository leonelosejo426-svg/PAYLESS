using Interfaces_de_Usuario_Propuestas_Payless.Datos;
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
            try
            {
                // 1. Ruta temporal automática para el PDF
                string nombreArchivo = "Reporte_De_Compras_Detallado_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
                string rutaArchivo = Path.Combine(Path.GetTempPath(), nombreArchivo);

                // 2. Configurar documento PDF (Tamaño A4, márgenes de 30 puntos)
                Document doc = new Document(PageSize.A4, 30f, 30f, 30f, 30f);
                PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
                doc.Open();

                // 3. PALETA DE COLORES PAYLESS
                BaseColor colorNaranjaPayless = new BaseColor(242, 101, 34);
                BaseColor colorTexto = new BaseColor(40, 40, 40);
                BaseColor colorGrisClaro = new BaseColor(248, 249, 250);
                BaseColor colorBorde = new BaseColor(220, 220, 220);
                BaseColor colorFondoCompra = new BaseColor(255, 243, 230); // Naranja muy suave para cabecera de compra

                Font fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 15f, colorNaranjaPayless);
                Font fuenteSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, BaseColor.GRAY);
                Font fuenteEtiqueta = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8.5f, colorTexto);
                Font fuenteTexto = FontFactory.GetFont(FontFactory.HELVETICA, 8.5f, colorTexto);
                Font fuenteCabeceraTabla = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f, BaseColor.WHITE);
                Font fuenteDetalle = FontFactory.GetFont(FontFactory.HELVETICA, 7.5f, colorTexto);
                Font fuenteTotalBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, colorNaranjaPayless);

                // 4. ENCABEZADO Y LOGO
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

                Paragraph pTitulo = new Paragraph("REPORTE DETALLADO DE COMPRAS", fuenteTitulo);
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

                // 5. FILTROS APLICADOS
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
                Paragraph pReg = new Paragraph($"Total Compras: {tablaReporte.Rows.Count}", fuenteEtiqueta);
                pReg.Alignment = Element.ALIGN_RIGHT;
                cellTotalRegistros.AddElement(pReg);
                infoTable.AddCell(cellTotalRegistros);

                doc.Add(infoTable);
                doc.Add(new Paragraph(" \n"));

                ReporteCompraDAO reporteDao = new ReporteCompraDAO();
                decimal sumaTotalGeneral = 0;

                // 6. RECORRER CADA COMPRA Y SU DETALLE DE PRODUCTOS
                foreach (DataRow filaCompra in tablaReporte.Rows)
                {
                    int idCompra = Convert.ToInt32(filaCompra["id_compra"]);
                    string codigo = filaCompra["Código"].ToString();
                    string fecha = Convert.ToDateTime(filaCompra["Fecha"]).ToString("dd/MM/yyyy HH:mm");
                    string proveedor = filaCompra["Proveedor"].ToString();
                    string usuario = filaCompra["Usuario"].ToString();
                    string rol = filaCompra["Rol"].ToString();
                    decimal totalCompra = Convert.ToDecimal(filaCompra["Total"]);
                    string estado = filaCompra["Estado"].ToString();

                    sumaTotalGeneral += totalCompra;

                    // Tarjeta / Encabezado de la Compra
                    PdfPTable tablaEncabezadoCompra = new PdfPTable(4);
                    tablaEncabezadoCompra.WidthPercentage = 100f;
                    tablaEncabezadoCompra.SetWidths(new float[] { 20f, 40f, 25f, 15f });

                    AgregarCeldaCabeceraCompra(tablaEncabezadoCompra, $"Compra: {codigo}", fuenteEtiqueta, colorFondoCompra, colorBorde, Element.ALIGN_LEFT);
                    AgregarCeldaCabeceraCompra(tablaEncabezadoCompra, $"Proveedor: {proveedor}", fuenteTexto, colorFondoCompra, colorBorde, Element.ALIGN_LEFT);
                    AgregarCeldaCabeceraCompra(tablaEncabezadoCompra, $"Fecha: {fecha}", fuenteTexto, colorFondoCompra, colorBorde, Element.ALIGN_CENTER);
                    AgregarCeldaCabeceraCompra(tablaEncabezadoCompra, $"Estado: {estado}", fuenteTexto, colorFondoCompra, colorBorde, Element.ALIGN_CENTER);

                    doc.Add(tablaEncabezadoCompra);

                    // Subtabla con los productos de esta compra
                    DataTable dtDetalle = reporteDao.ObtenerDetallesPorCompra(idCompra);

                    if (dtDetalle.Rows.Count > 0)
                    {
                        PdfPTable tablaDetalle = new PdfPTable(6);
                        tablaDetalle.WidthPercentage = 95f;
                        tablaDetalle.HorizontalAlignment = Element.ALIGN_RIGHT;
                        tablaDetalle.SetWidths(new float[] { 32f, 14f, 13f, 13f, 10f, 18f });

                        string[] headersDetalle = { "Producto / Cat. / Marca", "Talla", "P. Compra", "P. Venta", "Cant", "Subtotal" };
                        foreach (string h in headersDetalle)
                        {
                            PdfPCell cellH = new PdfPCell(new Phrase(h, fuenteCabeceraTabla))
                            {
                                BackgroundColor = colorNaranjaPayless,
                                HorizontalAlignment = (h.Contains("Producto")) ? Element.ALIGN_LEFT : Element.ALIGN_CENTER,
                                Padding = 4f,
                                BorderColor = colorNaranjaPayless
                            };
                            tablaDetalle.AddCell(cellH);
                        }

                        foreach (DataRow filaDet in dtDetalle.Rows)
                        {
                            string infoProd = $"{filaDet["producto"]} ({filaDet["categoria"]} - {filaDet["marca"]})";
                            string talla = filaDet["talla"].ToString();
                            decimal pCompra = Convert.ToDecimal(filaDet["precio_compra"]);
                            decimal pVenta = Convert.ToDecimal(filaDet["precio_venta"]);
                            int cantidad = Convert.ToInt32(filaDet["cantidad"]);
                            decimal subtotal = Convert.ToDecimal(filaDet["subtotal"]);

                            AgregarCeldaTabla(tablaDetalle, infoProd, fuenteDetalle, BaseColor.WHITE, colorBorde, Element.ALIGN_LEFT);
                            AgregarCeldaTabla(tablaDetalle, talla, fuenteDetalle, BaseColor.WHITE, colorBorde, Element.ALIGN_CENTER);
                            AgregarCeldaTabla(tablaDetalle, $"C$ {pCompra:N2}", fuenteDetalle, BaseColor.WHITE, colorBorde, Element.ALIGN_RIGHT);
                            AgregarCeldaTabla(tablaDetalle, $"C$ {pVenta:N2}", fuenteDetalle, BaseColor.WHITE, colorBorde, Element.ALIGN_RIGHT);
                            AgregarCeldaTabla(tablaDetalle, cantidad.ToString(), fuenteDetalle, BaseColor.WHITE, colorBorde, Element.ALIGN_CENTER);
                            AgregarCeldaTabla(tablaDetalle, $"C$ {subtotal:N2}", fuenteDetalle, BaseColor.WHITE, colorBorde, Element.ALIGN_RIGHT);
                        }

                        doc.Add(tablaDetalle);
                    }

                    // Fila de total de la compra actual
                    PdfPTable tablaSubtotalCompra = new PdfPTable(2);
                    tablaSubtotalCompra.WidthPercentage = 95f;
                    tablaSubtotalCompra.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tablaSubtotalCompra.SetWidths(new float[] { 80f, 20f });

                    AgregarCeldaTabla(tablaSubtotalCompra, $"Registrado por: {usuario} ({rol}) | Total Compra:", fuenteEtiqueta, colorGrisClaro, colorBorde, Element.ALIGN_RIGHT);
                    AgregarCeldaTabla(tablaSubtotalCompra, $"C$ {totalCompra:N2}", fuenteEtiqueta, colorGrisClaro, colorBorde, Element.ALIGN_RIGHT);

                    doc.Add(tablaSubtotalCompra);
                    doc.Add(new Paragraph(" ", FontFactory.GetFont(FontFactory.HELVETICA, 6f))); // Espacio separador entre compras
                }

                doc.Add(new Paragraph(" "));

                // 7. TOTAL GENERAL ACUMULADO
                PdfPTable tablaTotales = new PdfPTable(2);
                tablaTotales.WidthPercentage = 45f;
                tablaTotales.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablaTotales.SetWidths(new float[] { 45f, 55f });

                AgregarFilaTotal(tablaTotales, "TOTAL GENERAL COMPRAS:", $"C$ {sumaTotalGeneral:N2}", fuenteTotalBold, colorNaranjaPayless, colorBorde);
                doc.Add(tablaTotales);

                // 8. PIE DE PÁGINA
                doc.Add(new Paragraph(" \n"));
                Paragraph leyenda = new Paragraph(
                    "Payless ShoeSource Nicaragua - Control de Compras e Inventario\n" +
                    "Este reporte detallado es un comprobante informativo de auditoría interna.", fuenteSubtitulo)
                {
                    Alignment = Element.ALIGN_CENTER
                };

                doc.Add(leyenda);
                doc.Close();

                // Abrir PDF automáticamente
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(rutaArchivo) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF detallado de compras: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void AgregarCeldaCabeceraCompra(PdfPTable tabla, string texto, Font fuente, BaseColor backgroundColor, BaseColor borderColor, int alignment)
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

        private static void AgregarCeldaTabla(PdfPTable tabla, string texto, Font fuente, BaseColor backgroundColor, BaseColor borderColor, int alignment)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texto, fuente))
            {
                BackgroundColor = backgroundColor,
                HorizontalAlignment = alignment,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Padding = 4f,
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
