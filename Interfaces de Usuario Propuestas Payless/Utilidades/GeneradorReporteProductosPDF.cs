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

            Document documento = new Document(PageSize.A4.Rotate(), 20f, 20f, 25f, 25f);

            try
            {
                using (FileStream fs = new FileStream(ruta, FileMode.Create))
                {
                    PdfWriter writer = PdfWriter.GetInstance(documento, fs);
                    documento.Open();

                    // PALETA DE COLORES
                    BaseColor colorNaranjaPayless = new BaseColor(242, 101, 34);
                    BaseColor colorTexto = new BaseColor(40, 40, 40);
                    BaseColor colorGrisClaro = new BaseColor(248, 249, 250);
                    BaseColor colorBorde = new BaseColor(220, 220, 220);

                    // Colores de Alerta
                    BaseColor colorRojoFondo = new BaseColor(255, 228, 225); // Rojo claro
                    BaseColor colorRojoTexto = new BaseColor(178, 34, 34);   // Rojo oscuro

                    // FUENTES
                    Font fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16f, colorNaranjaPayless);
                    Font fuenteSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, BaseColor.GRAY);
                    Font fuenteEtiqueta = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, colorTexto);
                    Font fuenteTexto = FontFactory.GetFont(FontFactory.HELVETICA, 8f, colorTexto);
                    Font fuenteTextoAlerta = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f, colorRojoTexto);
                    Font fuenteCabeceraTabla = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f, BaseColor.WHITE);

                    // 1. ENCABEZADO
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
                    cellRight.AddElement(new Paragraph("REPORTE DE INVENTARIO Y STOCK", fuenteTitulo) { Alignment = Element.ALIGN_RIGHT });
                    cellRight.AddElement(new Paragraph($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}", fuenteTexto) { Alignment = Element.ALIGN_RIGHT });
                    cellRight.AddElement(new Paragraph($"Total de Registros: {datos.Rows.Count}", fuenteEtiqueta) { Alignment = Element.ALIGN_RIGHT });
                    headerTable.AddCell(cellRight);

                    documento.Add(headerTable);

                    // LÍNEA DIVISORIA NARANJA
                    PdfPTable line = new PdfPTable(1) { WidthPercentage = 100f };
                    line.AddCell(new PdfPCell(new Phrase(""))
                    {
                        BorderWidthBottom = 2f,
                        BorderColorBottom = colorNaranjaPayless,
                        Border = Rectangle.BOTTOM_BORDER
                    });
                    documento.Add(line);
                    documento.Add(new Paragraph(" ", FontFactory.GetFont(FontFactory.HELVETICA, 4f)));

                    // 2. SECCIÓN RESUMEN / ALERTAS DE STOCK BAJO
                    int totalStockBajo = 0;
                    foreach (DataRow r in datos.Rows)
                    {
                        string alertaVal = datos.Columns.Contains("Alerta") ? r["Alerta"].ToString() : "";
                        if (alertaVal.Contains("STOCK BAJO")) totalStockBajo++;
                    }

                    if (totalStockBajo > 0)
                    {
                        PdfPTable cajaAlerta = new PdfPTable(1) { WidthPercentage = 100f };
                        PdfPCell celdaAlerta = new PdfPCell(new Phrase($"⚠️ ATENCIÓN: Se han detectado {totalStockBajo} productos con STOCK BAJO o igual al límite mínimo reorden.", fuenteTextoAlerta))
                        {
                            BackgroundColor = colorRojoFondo,
                            BorderColor = colorRojoTexto,
                            Padding = 6f,
                            HorizontalAlignment = Element.ALIGN_CENTER
                        };
                        cajaAlerta.AddCell(celdaAlerta);
                        documento.Add(cajaAlerta);
                        documento.Add(new Paragraph(" ", FontFactory.GetFont(FontFactory.HELVETICA, 4f)));
                    }

                    // 3. TABLA DE PRODUCTOS (10 COLUMNAS AJUSTADAS)
                    PdfPTable tabla = new PdfPTable(10) { WidthPercentage = 100f };
                    tabla.SetWidths(new float[] { 5f, 18f, 11f, 11f, 13f, 6f, 10f, 6f, 6f, 14f });

                    string[] encabezados = { "ID", "Producto", "Categoría", "Marca", "Proveedor", "Talla", "Precio", "Stock", "Min", "Alerta" };
                    foreach (string enc in encabezados)
                    {
                        PdfPCell cellHeader = new PdfPCell(new Phrase(enc, fuenteCabeceraTabla))
                        {
                            BackgroundColor = colorNaranjaPayless,
                            HorizontalAlignment = (enc == "ID" || enc == "Talla" || enc == "Stock" || enc == "Min" || enc == "Alerta")
                                ? Element.ALIGN_CENTER
                                : (enc == "Precio" ? Element.ALIGN_RIGHT : Element.ALIGN_LEFT),
                            Padding = 4f,
                            BorderColor = colorNaranjaPayless
                        };
                        tabla.AddCell(cellHeader);
                    }

                    // 4. RECORRIDO DE FILAS Y RESALTADO ROJO
                    bool esGris = false;
                    foreach (DataRow fila in datos.Rows)
                    {
                        string alerta = datos.Columns.Contains("Alerta") ? fila["Alerta"].ToString() : "Normal";
                        bool esStockBajo = alerta.Contains("STOCK BAJO");

                        // Si es stock bajo, fondo rojo claro; de lo contrario, intercalado gris/blanco
                        BaseColor bgFila = esStockBajo ? colorRojoFondo : (esGris ? colorGrisClaro : BaseColor.WHITE);
                        Font fuenteAplicada = esStockBajo ? fuenteTextoAlerta : fuenteTexto;

                        string idProd = datos.Columns.Contains("ID Producto") ? fila["ID Producto"].ToString() : "";
                        string producto = datos.Columns.Contains("Producto") ? fila["Producto"].ToString() : "";
                        string categoria = datos.Columns.Contains("Categoria") ? fila["Categoria"].ToString() : (datos.Columns.Contains("Categoría") ? fila["Categoría"].ToString() : "");
                        string marca = datos.Columns.Contains("Marca") ? fila["Marca"].ToString() : "";
                        string proveedor = datos.Columns.Contains("Proveedor") ? fila["Proveedor"].ToString() : "";
                        string talla = datos.Columns.Contains("Talla") ? fila["Talla"].ToString() : "";
                        object valPrecio = datos.Columns.Contains("Precio Venta") ? fila["Precio Venta"] : 0;
                        string stock = datos.Columns.Contains("Stock") ? fila["Stock"].ToString() : "0";
                        string stockMin = datos.Columns.Contains("Stock Minimo") ? fila["Stock Minimo"].ToString() : (datos.Columns.Contains("Stock Mínimo") ? fila["Stock Mínimo"].ToString() : "0");

                        AgregarCeldaTabla(tabla, idProd, fuenteAplicada, bgFila, colorBorde, Element.ALIGN_CENTER);
                        AgregarCeldaTabla(tabla, producto, fuenteAplicada, bgFila, colorBorde, Element.ALIGN_LEFT);
                        AgregarCeldaTabla(tabla, categoria, fuenteAplicada, bgFila, colorBorde, Element.ALIGN_LEFT);
                        AgregarCeldaTabla(tabla, marca, fuenteAplicada, bgFila, colorBorde, Element.ALIGN_LEFT);
                        AgregarCeldaTabla(tabla, proveedor, fuenteAplicada, bgFila, colorBorde, Element.ALIGN_LEFT);
                        AgregarCeldaTabla(tabla, talla, fuenteAplicada, bgFila, colorBorde, Element.ALIGN_CENTER);
                        AgregarCeldaTabla(tabla, FormatoMoneda(valPrecio), fuenteAplicada, bgFila, colorBorde, Element.ALIGN_RIGHT);
                        AgregarCeldaTabla(tabla, stock, fuenteAplicada, bgFila, colorBorde, Element.ALIGN_CENTER);
                        AgregarCeldaTabla(tabla, stockMin, fuenteAplicada, bgFila, colorBorde, Element.ALIGN_CENTER);
                        AgregarCeldaTabla(tabla, alerta, fuenteAplicada, bgFila, colorBorde, Element.ALIGN_CENTER);

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

