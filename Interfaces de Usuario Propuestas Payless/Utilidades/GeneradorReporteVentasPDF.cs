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

namespace Interfaces_de_Usuario_Propuestas_Payless.Utilidades
{
    internal class GeneradorReporteVentasPDF
    {
        public static void Generar(DataTable datos, DateTime desde, DateTime hasta, bool usarFechas, string usuarioFiltro, string estadoFiltro)
        {
            if (datos == null || datos.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No hay información de ventas para imprimir.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string carpeta = Path.Combine(Application.StartupPath, "Reportes");
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            string nombreArchivo = $"Reporte_Ventas_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            string ruta = Path.Combine(carpeta, nombreArchivo);

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
                    Font fuenteTotalBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, colorNaranjaPayless);

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
                    cellRight.AddElement(new Paragraph("REPORTE DE VENTAS", fuenteTitulo) { Alignment = Element.ALIGN_RIGHT });
                    cellRight.AddElement(new Paragraph($"Fecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm}", fuenteTexto) { Alignment = Element.ALIGN_RIGHT });

                    string textoPeriodo = usarFechas ? $"Período: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}" : "Período: Todos los registros";
                    cellRight.AddElement(new Paragraph(textoPeriodo, fuenteEtiqueta) { Alignment = Element.ALIGN_RIGHT });

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

                    // 2. TABLA DE VENTAS (9 COLUMNAS)
                    PdfPTable tabla = new PdfPTable(9) { WidthPercentage = 100f };
                    tabla.SetWidths(new float[] { 8f, 12f, 16f, 22f, 12f, 10f, 10f, 10f, 10f });

                    string[] encabezados = { "ID", "Código", "Fecha", "Cliente", "Usuario", "Subtotal", "IVA", "Total", "Estado" };
                    foreach (string enc in encabezados)
                    {
                        PdfPCell cellHeader = new PdfPCell(new Phrase(enc, fuenteCabeceraTabla))
                        {
                            BackgroundColor = colorNaranjaPayless,
                            HorizontalAlignment = (enc == "Subtotal" || enc == "IVA" || enc == "Total") ? Element.ALIGN_RIGHT : Element.ALIGN_CENTER,
                            Padding = 5f,
                            BorderColor = colorNaranjaPayless
                        };
                        tabla.AddCell(cellHeader);
                    }

                    decimal acumuladoSubtotal = 0;
                    decimal acumuladoIVA = 0;
                    decimal acumuladoTotal = 0;
                    bool esGris = false;

                    foreach (DataRow fila in datos.Rows)
                    {
                        BaseColor bgFila = esGris ? colorGrisClaro : BaseColor.WHITE;

                        string idVenta = fila["ID Venta"].ToString();
                        string codigo = fila["Código"].ToString();
                        DateTime fecha = Convert.ToDateTime(fila["Fecha"]);
                        string cliente = fila["Cliente"].ToString();
                        string usuario = fila["Usuario"].ToString();
                        decimal subtotal = Convert.ToDecimal(fila["Subtotal"]);
                        decimal iva = Convert.ToDecimal(fila["IVA"]);
                        decimal total = Convert.ToDecimal(fila["Total"]);
                        string estado = fila["Estado"].ToString();

                        // Sumar solo si la venta está activa
                        if (estado == "Activa")
                        {
                            acumuladoSubtotal += subtotal;
                            acumuladoIVA += iva;
                            acumuladoTotal += total;
                        }

                        AgregarCeldaTabla(tabla, idVenta, fuenteTexto, bgFila, colorBorde, Element.ALIGN_CENTER);
                        AgregarCeldaTabla(tabla, codigo, fuenteTexto, bgFila, colorBorde, Element.ALIGN_CENTER);
                        AgregarCeldaTabla(tabla, fecha.ToString("dd/MM/yyyy HH:mm"), fuenteTexto, bgFila, colorBorde, Element.ALIGN_CENTER);
                        AgregarCeldaTabla(tabla, cliente, fuenteTexto, bgFila, colorBorde, Element.ALIGN_LEFT);
                        AgregarCeldaTabla(tabla, usuario, fuenteTexto, bgFila, colorBorde, Element.ALIGN_CENTER);
                        AgregarCeldaTabla(tabla, $"C$ {subtotal:N2}", fuenteTexto, bgFila, colorBorde, Element.ALIGN_RIGHT);
                        AgregarCeldaTabla(tabla, $"C$ {iva:N2}", fuenteTexto, bgFila, colorBorde, Element.ALIGN_RIGHT);
                        AgregarCeldaTabla(tabla, $"C$ {total:N2}", fuenteTexto, bgFila, colorBorde, Element.ALIGN_RIGHT);
                        AgregarCeldaTabla(tabla, estado, fuenteTexto, bgFila, colorBorde, Element.ALIGN_CENTER);

                        esGris = !esGris;
                    }

                    documento.Add(tabla);
                    documento.Add(new Paragraph(" "));

                    // 3. RESUMEN DE TOTALES
                    PdfPTable tablaTotales = new PdfPTable(2)
                    {
                        WidthPercentage = 40f,
                        HorizontalAlignment = Element.ALIGN_RIGHT
                    };
                    tablaTotales.SetWidths(new float[] { 50f, 50f });

                    AgregarFilaTotal(tablaTotales, "Subtotal Activas:", $"C$ {acumuladoSubtotal:N2}", fuenteTexto, false, colorBorde);
                    AgregarFilaTotal(tablaTotales, "IVA Total (15%):", $"C$ {acumuladoIVA:N2}", fuenteTexto, false, colorBorde);
                    AgregarFilaTotal(tablaTotales, "TOTAL DE VENTAS:", $"C$ {acumuladoTotal:N2}", fuenteTotalBold, true, colorNaranjaPayless);

                    documento.Add(tablaTotales);

                    // 4. PIE DE PÁGINA
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

                Process.Start(ruta);

                MessageBox.Show(
                    "Reporte de ventas generado correctamente.",
                    "Reporte",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
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
                Padding = 4f
            };
            tabla.AddCell(celda);
        }

        private static void AgregarFilaTotal(PdfPTable tabla, string etiqueta, string valor, Font fuente, bool esTotal, BaseColor colorBorde)
        {
            PdfPCell celdaEtiqueta = new PdfPCell(new Phrase(etiqueta, fuente))
            {
                BorderColor = colorBorde,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Padding = 4f
            };

            PdfPCell celdaValor = new PdfPCell(new Phrase(valor, fuente))
            {
                BorderColor = colorBorde,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Padding = 4f
            };

            if (esTotal)
            {
                celdaEtiqueta.BackgroundColor = new BaseColor(255, 243, 235);
                celdaValor.BackgroundColor = new BaseColor(255, 243, 235);
            }

            tabla.AddCell(celdaEtiqueta);
            tabla.AddCell(celdaValor);
        }
    }
}
