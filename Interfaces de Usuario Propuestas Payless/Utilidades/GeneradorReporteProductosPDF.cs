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
    internal class GeneradorReporteProductosPDF
    {

        public static void Generar(DataTable datos)
        {
            if (datos == null || datos.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No hay información de productos para imprimir.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string carpeta = Path.Combine(Application.StartupPath, "Reportes");
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            string nombreArchivo = $"Reporte_Productos_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            string ruta = Path.Combine(carpeta, nombreArchivo);

            Document documento = new Document(PageSize.A4.Rotate(), 30f, 30f, 30f, 30f);

            try
            {
                using (FileStream fs = new FileStream(ruta, FileMode.Create))
                {
                    PdfWriter writer = PdfWriter.GetInstance(documento, fs);
                    documento.Open();

                    // 1. PALETA DE COLORES CORPORATIVA PAYLESS
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

                    // 2. ENCABEZADO Y CARGA DEL LOGO
                    PdfPTable headerTable = new PdfPTable(2);
                    headerTable.WidthPercentage = 100f;
                    headerTable.SetWidths(new float[] { 50f, 50f });

                    PdfPCell cellLeft = new PdfPCell { Border = Rectangle.NO_BORDER };

                    // Ruta del logo corporativo
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

                    Paragraph pTituloReporte = new Paragraph("REPORTE DE PRODUCTOS", fuenteTitulo)
                    {
                        Alignment = Element.ALIGN_RIGHT
                    };
                    cellRight.AddElement(pTituloReporte);

                    Paragraph pFecha = new Paragraph($"Fecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm}", fuenteTexto)
                    {
                        Alignment = Element.ALIGN_RIGHT
                    };
                    cellRight.AddElement(pFecha);

                    Paragraph pTotalReg = new Paragraph($"Total de Registros: {datos.Rows.Count}", fuenteEtiqueta)
                    {
                        Alignment = Element.ALIGN_RIGHT
                    };
                    cellRight.AddElement(pTotalReg);

                    headerTable.AddCell(cellRight);
                    documento.Add(headerTable);

                    // Línea divisora corporativa en color Naranja
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

                    // 3. TABLA DE PRODUCTOS (9 COLUMNAS) CON ENCABEZADO NARANJA
                    PdfPTable tabla = new PdfPTable(9) { WidthPercentage = 100f };
                    tabla.SetWidths(new float[] { 6f, 20f, 13f, 13f, 15f, 8f, 11f, 7f, 7f });

                    string[] encabezados = { "ID", "Producto", "Categoría", "Marca", "Proveedor", "Talla", "Precio", "Stock", "Estado" };
                    foreach (string enc in encabezados)
                    {
                        PdfPCell cellHeader = new PdfPCell(new Phrase(enc, fuenteCabeceraTabla))
                        {
                            BackgroundColor = colorNaranjaPayless,
                            HorizontalAlignment = (enc == "ID" || enc == "Talla" || enc == "Stock" || enc == "Estado")
                                ? Element.ALIGN_CENTER
                                : (enc == "Precio" ? Element.ALIGN_RIGHT : Element.ALIGN_LEFT),
                            Padding = 5f,
                            BorderColor = colorNaranjaPayless
                        };
                        tabla.AddCell(cellHeader);
                    }

                    // 4. FILAS CON ESTILO ALTERNADO (BLANCO / GRIS CLARO)
                    bool esGris = false;
                    foreach (DataRow fila in datos.Rows)
                    {
                        BaseColor bgFila = esGris ? colorGrisClaro : BaseColor.WHITE;

                        // Obtener columnas con compatibilidad
                        string idProd = datos.Columns.Contains("ID Producto") ? fila["ID Producto"].ToString() : (datos.Columns.Contains("ID") ? fila["ID"].ToString() : "");
                        string producto = datos.Columns.Contains("Producto") ? fila["Producto"].ToString() : "";
                        string categoria = datos.Columns.Contains("Categoría") ? fila["Categoría"].ToString() : "";
                        string marca = datos.Columns.Contains("Marca") ? fila["Marca"].ToString() : "";
                        string proveedor = datos.Columns.Contains("Proveedor") ? fila["Proveedor"].ToString() : "";
                        string talla = datos.Columns.Contains("Talla") ? fila["Talla"].ToString() : (datos.Columns.Contains("Tallas") ? fila["Tallas"].ToString() : "");
                        object valPrecio = datos.Columns.Contains("Precio Venta") ? fila["Precio Venta"] : (datos.Columns.Contains("Precio") ? fila["Precio"] : 0);
                        string stock = datos.Columns.Contains("Stock") ? fila["Stock"].ToString() : (datos.Columns.Contains("Stock Total") ? fila["Stock Total"].ToString() : "0");
                        string estado = datos.Columns.Contains("Estado") ? fila["Estado"].ToString() : "";

                        AgregarCeldaTabla(tabla, idProd, fuenteTexto, bgFila, colorBorde, Element.ALIGN_CENTER);
                        AgregarCeldaTabla(tabla, producto, fuenteTexto, bgFila, colorBorde, Element.ALIGN_LEFT);
                        AgregarCeldaTabla(tabla, categoria, fuenteTexto, bgFila, colorBorde, Element.ALIGN_LEFT);
                        AgregarCeldaTabla(tabla, marca, fuenteTexto, bgFila, colorBorde, Element.ALIGN_LEFT);
                        AgregarCeldaTabla(tabla, proveedor, fuenteTexto, bgFila, colorBorde, Element.ALIGN_LEFT);
                        AgregarCeldaTabla(tabla, talla, fuenteTexto, bgFila, colorBorde, Element.ALIGN_CENTER);
                        AgregarCeldaTabla(tabla, FormatoMoneda(valPrecio), fuenteTexto, bgFila, colorBorde, Element.ALIGN_RIGHT);
                        AgregarCeldaTabla(tabla, stock, fuenteTexto, bgFila, colorBorde, Element.ALIGN_CENTER);
                        AgregarCeldaTabla(tabla, estado, fuenteTexto, bgFila, colorBorde, Element.ALIGN_CENTER);

                        esGris = !esGris;
                    }

                    documento.Add(tabla);
                    documento.Add(new Paragraph(" "));

                    // 5. PIE DE PÁGINA
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
                    "Reporte de productos generado correctamente.",
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




        private static string FormatoMoneda(object valor)
        {
            if (valor == null || valor == DBNull.Value)
                return "C$ 0.00";

            decimal numero = Convert.ToDecimal(valor);
            return $"C$ {numero:N2}";
        }
    }

}

