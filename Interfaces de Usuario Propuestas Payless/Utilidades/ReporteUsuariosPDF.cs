using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;

namespace Interfaces_de_Usuario_Propuestas_Payless.Utilidades
{
    internal class ReporteUsuariosPDF
    {
        public static void GenerarReporte(DataTable datos, string filtroUsuario, string filtroRol, string filtroEstado)
        {
            if (datos == null || datos.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No hay información de usuarios para imprimir.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Generar archivo en la carpeta temporal para visualizarse de inmediato sin descargas manuales
            string nombreArchivo = $"Reporte_Usuarios_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            string ruta = Path.Combine(Path.GetTempPath(), nombreArchivo);

            Document documento = new Document(PageSize.A4.Rotate(), 30f, 30f, 30f, 30f);

            try
            {
                using (FileStream fs = new FileStream(ruta, FileMode.Create))
                {
                    PdfWriter writer = PdfWriter.GetInstance(documento, fs);
                    documento.Open();

                    // PALETA DE COLORES PAYLESS
                    BaseColor colorNaranjaPayless = new BaseColor(242, 101, 34);
                    BaseColor colorTexto = new BaseColor(40, 40, 40);
                    BaseColor colorGrisClaro = new BaseColor(248, 249, 250);
                    BaseColor colorBorde = new BaseColor(220, 220, 220);

                    // FUENTES
                    Font fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16f, colorNaranjaPayless);
                    Font fuenteSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, BaseColor.GRAY);
                    Font fuenteEtiqueta = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, colorTexto);
                    Font fuenteTexto = FontFactory.GetFont(FontFactory.HELVETICA, 8.5f, colorTexto);
                    Font fuenteCabeceraTabla = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8.5f, BaseColor.WHITE);

                    // 1. ENCABEZADO Y LOGO
                    PdfPTable headerTable = new PdfPTable(2) { WidthPercentage = 100f };
                    headerTable.SetWidths(new float[] { 50f, 50f });

                    PdfPCell cellLeft = new PdfPCell { Border = Rectangle.NO_BORDER };

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

                    PdfPCell cellRight = new PdfPCell { Border = Rectangle.NO_BORDER };
                    cellRight.AddElement(new Paragraph("REPORTE DE USUARIOS", fuenteTitulo) { Alignment = Element.ALIGN_RIGHT });
                    cellRight.AddElement(new Paragraph($"Fecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm}", fuenteTexto) { Alignment = Element.ALIGN_RIGHT });

                    cellRight.AddElement(new Paragraph($"Filtros -> Usuario: {filtroUsuario} | Rol: {filtroRol} | Estado: {filtroEstado}", fuenteEtiqueta) { Alignment = Element.ALIGN_RIGHT });

                    headerTable.AddCell(cellRight);
                    documento.Add(headerTable);

                    // LÍNEA DIVISORA NARANJA
                    documento.Add(new Paragraph(" "));
                    PdfPTable line = new PdfPTable(1) { WidthPercentage = 100f };
                    PdfPCell lineCell = new PdfPCell(new Phrase(""))
                    {
                        BorderWidthBottom = 2f,
                        BorderColorBottom = colorNaranjaPayless,
                        Border = Rectangle.BOTTOM_BORDER
                    };
                    line.AddCell(lineCell);
                    documento.Add(line);

                    documento.Add(new Paragraph(" ", FontFactory.GetFont(FontFactory.HELVETICA, 6f)));

                    // 2. TABLA DE USUARIOS (11 COLUMNAS SEGÚN EL DAO)
                    int totalCols = datos.Columns.Count;
                    PdfPTable tabla = new PdfPTable(totalCols) { WidthPercentage = 100f };

                    // Anchos proporcionales para las 11 columnas
                    tabla.SetWidths(new float[] { 6f, 11f, 18f, 16f, 10f, 10f, 8f, 10f, 7f, 7f, 7f });

                    foreach (DataColumn columna in datos.Columns)
                    {
                        string enc = columna.ColumnName;
                        PdfPCell cellHeader = new PdfPCell(new Phrase(enc, fuenteCabeceraTabla))
                        {
                            BackgroundColor = colorNaranjaPayless,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE,
                            Padding = 5f,
                            BorderColor = colorNaranjaPayless
                        };
                        tabla.AddCell(cellHeader);
                    }

                    bool esGris = false;

                    foreach (DataRow fila in datos.Rows)
                    {
                        BaseColor bgFila = esGris ? colorGrisClaro : BaseColor.WHITE;

                        foreach (var valor in fila.ItemArray)
                        {
                            string textoValor = valor?.ToString() ?? "";
                            AgregarCeldaTabla(tabla, textoValor, fuenteTexto, bgFila, colorBorde, Element.ALIGN_CENTER);
                        }

                        esGris = !esGris;
                    }

                    documento.Add(tabla);
                    documento.Add(new Paragraph(" "));

                    // 3. PIE DE PÁGINA INSTITUCIONAL
                    documento.Add(new Paragraph(" \n"));
                    Paragraph leyenda = new Paragraph(
                        "Payless ShoeSource Nicaragua\n" +
                        "Dirección: Managua, Nicaragua | Teléfono: +505 2222-0000\n" +
                        "Documento generado automáticamente por el Sistema Administrativo.", fuenteSubtitulo)
                    {
                        Alignment = Element.ALIGN_CENTER
                    };

                    documento.Add(leyenda);
                    documento.Close();
                }

                // Abrir directamente en el visor de PDF predeterminado
                Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                if (documento.IsOpen())
                    documento.Close();

                MessageBox.Show(
                    "Error al generar el PDF:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static void AgregarCeldaTabla(PdfPTable tabla, string texto, Font fuente, BaseColor colorFondo, BaseColor colorBorde, int alineacion)
        {
            PdfPCell celda = new PdfPCell(new Phrase(texto, fuente))
            {
                BackgroundColor = colorFondo,
                BorderColor = colorBorde,
                HorizontalAlignment = alineacion,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Padding = 4f
            };
            tabla.AddCell(celda);
        }
    }
}