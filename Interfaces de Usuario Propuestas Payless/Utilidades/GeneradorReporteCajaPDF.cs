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
    internal class GeneradorReporteCajaPDF
    {

        // =========================================================================
        // REPORTE HISTÓRICO DETALLADO (TABLA COMPLETA CON FORMATO HORIZONTAL)
        // =========================================================================
        public static void Generar(DataTable datos)
        {
            if (datos == null || datos.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No hay información histórica para exportar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string carpeta = Path.Combine(Application.StartupPath, "Reportes");
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            string nombreArchivo = $"Reporte_Historico_Cajas_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            string ruta = Path.Combine(carpeta, nombreArchivo);

            // Documento en formato Horizontal (Landscape) para aprovechar el espacio de múltiples columnas
            Document documento = new Document(PageSize.A4.Rotate(), 30f, 30f, 30f, 30f);

            try
            {
                using (FileStream fs = new FileStream(ruta, FileMode.Create))
                {
                    PdfWriter writer = PdfWriter.GetInstance(documento, fs);
                    documento.Open();

                    // PALETA DE COLORES CORPORATIVA
                    BaseColor colorNaranjaPayless = new BaseColor(242, 101, 34);
                    BaseColor colorTexto = new BaseColor(40, 40, 40);
                    BaseColor colorGrisClaro = new BaseColor(248, 249, 250);
                    BaseColor colorBorde = new BaseColor(220, 220, 220);

                    // FUENTES
                    Font fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14f, colorNaranjaPayless);
                    Font fuenteSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, BaseColor.GRAY);
                    Font fuenteTexto = FontFactory.GetFont(FontFactory.HELVETICA, 8f, colorTexto);
                    Font fuenteCabeceraTabla = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f, BaseColor.WHITE);

                    // 1. ENCABEZADO DETALLADO
                    PdfPTable headerTable = new PdfPTable(2) { WidthPercentage = 100f };
                    headerTable.SetWidths(new float[] { 60f, 40f });

                    PdfPCell cellLeft = new PdfPCell { Border = Rectangle.NO_BORDER };
                    cellLeft.AddElement(new Paragraph("PAYLESS SHOESOURCE", fuenteTitulo));
                    cellLeft.AddElement(new Paragraph("Sistema de Gestión Administrativa - Módulo de Caja", fuenteSubtitulo));
                    headerTable.AddCell(cellLeft);

                    PdfPCell cellRight = new PdfPCell { Border = Rectangle.NO_BORDER };
                    cellRight.AddElement(new Paragraph("REPORTE HISTÓRICO GLOBAL", fuenteTitulo) { Alignment = Element.ALIGN_RIGHT });
                    cellRight.AddElement(new Paragraph($"Fecha de Emisión: {DateTime.Now:dd/MM/yyyy HH:mm}", fuenteSubtitulo) { Alignment = Element.ALIGN_RIGHT });
                    cellRight.AddElement(new Paragraph($"Total de Registros: {datos.Rows.Count}", fuenteSubtitulo) { Alignment = Element.ALIGN_RIGHT });
                    headerTable.AddCell(cellRight);

                    documento.Add(headerTable);
                    documento.Add(new Paragraph(" ", FontFactory.GetFont(FontFactory.HELVETICA, 4f)));

                    // LÍNEA DIVISORIA NARANJA
                    PdfPTable line = new PdfPTable(1) { WidthPercentage = 100f };
                    line.AddCell(new PdfPCell(new Phrase(""))
                    {
                        BorderWidthBottom = 2f,
                        BorderColorBottom = colorNaranjaPayless,
                        Border = Rectangle.BOTTOM_BORDER
                    });
                    documento.Add(line);
                    documento.Add(new Paragraph(" ", FontFactory.GetFont(FontFactory.HELVETICA, 8f)));

                    // 2. TABLA DE DATOS HISTÓRICOS (Dinamica según columnas del DataTable)
                    int totalColumnas = datos.Columns.Count;
                    PdfPTable tabla = new PdfPTable(totalColumnas) { WidthPercentage = 100f };

                    // Agregar Cabeceras de la Tabla
                    foreach (DataColumn col in datos.Columns)
                    {
                        PdfPCell th = new PdfPCell(new Phrase(col.ColumnName, fuenteCabeceraTabla))
                        {
                            BackgroundColor = colorNaranjaPayless,
                            Padding = 5f,
                            BorderColor = colorNaranjaPayless,
                            HorizontalAlignment = Element.ALIGN_CENTER
                        };
                        tabla.AddCell(th);
                    }

                    // Llenado de Filas con Efecto Zebra (Intercalado de colores)
                    bool esGris = false;
                    foreach (DataRow fila in datos.Rows)
                    {
                        BaseColor bg = esGris ? colorGrisClaro : BaseColor.WHITE;
                        foreach (DataColumn col in datos.Columns)
                        {
                            string valorCelda = fila[col] != DBNull.Value ? fila[col].ToString() : "";

                            // Formato amigable para fechas o montos si aplica
                            if (col.DataType == typeof(DateTime) && !string.IsNullOrEmpty(valorCelda))
                            {
                                valorCelda = Convert.ToDateTime(fila[col]).ToString("dd/MM/yyyy HH:mm");
                            }

                            PdfPCell celda = new PdfPCell(new Phrase(valorCelda, fuenteTexto))
                            {
                                BackgroundColor = bg,
                                BorderColor = colorBorde,
                                Padding = 4f,
                                HorizontalAlignment = Element.ALIGN_LEFT
                            };
                            tabla.AddCell(celda);
                        }
                        esGris = !esGris;
                    }

                    documento.Add(tabla);
                    documento.Add(new Paragraph(" ", FontFactory.GetFont(FontFactory.HELVETICA, 10f)));

                    // 3. PIE DE PÁGINA
                    Paragraph leyenda = new Paragraph(
                        "Payless ShoeSource Nicaragua — Reporte Histórico Oficial\n" +
                        "Este documento refleja la totalidad de las operaciones almacenadas en la base de datos central.", fuenteSubtitulo)
                    {
                        Alignment = Element.ALIGN_CENTER
                    };
                    documento.Add(leyenda);

                    documento.Close();
                }

                // Abrir automáticamente el documento PDF generado
                Process.Start(ruta);

                MessageBox.Show(
                    "Reporte histórico detallado generado correctamente.",
                    "Reporte Histórico",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (documento.IsOpen())
                    documento.Close();

                MessageBox.Show(
                    "Error al generar el PDF histórico:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================================
        // REPORTE INDIVIDUAL / SELECCIONADO (Mantiene el formato original de 2 columnas)
        // =========================================================================
        public static void Generar(DataTable datos, int idCaja)
        {
            if (datos == null || datos.Rows.Count == 0)
            {
                MessageBox.Show("No hay información para imprimir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow fila = null;
            foreach (DataRow registro in datos.Rows)
            {
                if (Convert.ToInt32(registro["ID Caja"]) == idCaja)
                {
                    fila = registro;
                    break;
                }
            }

            if (fila == null)
            {
                MessageBox.Show("No se encontró la caja seleccionada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string carpeta = Path.Combine(Application.StartupPath, "Reportes");
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            string nombreArchivo = $"Reporte_Caja_{idCaja}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            string ruta = Path.Combine(carpeta, nombreArchivo);

            Document documento = new Document(PageSize.A4, 30f, 30f, 30f, 30f);

            try
            {
                using (FileStream fs = new FileStream(ruta, FileMode.Create))
                {
                    PdfWriter writer = PdfWriter.GetInstance(documento, fs);
                    documento.Open();

                    BaseColor colorNaranjaPayless = new BaseColor(242, 101, 34);
                    BaseColor colorTexto = new BaseColor(40, 40, 40);
                    BaseColor colorGrisClaro = new BaseColor(248, 249, 250);
                    BaseColor colorBorde = new BaseColor(220, 220, 220);

                    Font fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 15f, colorNaranjaPayless);
                    Font fuenteSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, BaseColor.GRAY);
                    Font fuenteEtiqueta = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, colorTexto);
                    Font fuenteTexto = FontFactory.GetFont(FontFactory.HELVETICA, 9f, colorTexto);
                    Font fuenteCabeceraTabla = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, BaseColor.WHITE);

                    PdfPTable headerTable = new PdfPTable(2) { WidthPercentage = 100f };
                    headerTable.SetWidths(new float[] { 50f, 50f });

                    PdfPCell cellLeft = new PdfPCell { Border = Rectangle.NO_BORDER };
                    cellLeft.AddElement(new Paragraph("PAYLESS SHOESOURCE", fuenteTitulo));
                    cellLeft.AddElement(new Paragraph("Calzado y Accesorios", fuenteSubtitulo));
                    headerTable.AddCell(cellLeft);

                    PdfPCell cellRight = new PdfPCell { Border = Rectangle.NO_BORDER };
                    cellRight.AddElement(new Paragraph("REPORTE DE CIERRE Y ARQUEO DE CAJA", fuenteTitulo) { Alignment = Element.ALIGN_RIGHT });
                    cellRight.AddElement(new Paragraph($"Fecha de Emisión: {DateTime.Now:dd/MM/yyyy HH:mm}", fuenteTexto) { Alignment = Element.ALIGN_RIGHT });
                    cellRight.AddElement(new Paragraph($"Caja ID: #{idCaja}", fuenteEtiqueta) { Alignment = Element.ALIGN_RIGHT });
                    headerTable.AddCell(cellRight);

                    documento.Add(headerTable);

                    PdfPTable line = new PdfPTable(1) { WidthPercentage = 100f };
                    line.AddCell(new PdfPCell(new Phrase(""))
                    {
                        BorderWidthBottom = 2f,
                        BorderColorBottom = colorNaranjaPayless,
                        Border = Rectangle.BOTTOM_BORDER
                    });
                    documento.Add(line);
                    documento.Add(new Paragraph(" ", FontFactory.GetFont(FontFactory.HELVETICA, 6f)));

                    PdfPTable tabla = new PdfPTable(2) { WidthPercentage = 100f };
                    tabla.SetWidths(new float[] { 45f, 55f });

                    PdfPCell thConcepto = new PdfPCell(new Phrase("Concepto / Parámetro", fuenteCabeceraTabla)) { BackgroundColor = colorNaranjaPayless, Padding = 6f };
                    PdfPCell thDetalle = new PdfPCell(new Phrase("Valor / Registro", fuenteCabeceraTabla)) { BackgroundColor = colorNaranjaPayless, Padding = 6f };
                    tabla.AddCell(thConcepto);
                    tabla.AddCell(thDetalle);

                    bool esGris = false;
                    AgregarFilaDetalle(tabla, "ID Caja", fila["ID Caja"].ToString(), fuenteEtiqueta, fuenteTexto, esGris, colorGrisClaro, colorBorde);
                    esGris = !esGris;
                    AgregarFilaDetalle(tabla, "Usuario Apertura", fila["Usuario Apertura"].ToString(), fuenteEtiqueta, fuenteTexto, esGris, colorGrisClaro, colorBorde);
                    esGris = !esGris;
                    AgregarFilaDetalle(tabla, "Usuario Cierre", fila["Usuario Cierre"].ToString(), fuenteEtiqueta, fuenteTexto, esGris, colorGrisClaro, colorBorde);
                    esGris = !esGris;
                    string fechaApertura = Convert.ToDateTime(fila["Fecha Apertura"]).ToString("dd/MM/yyyy HH:mm");
                    AgregarFilaDetalle(tabla, "Fecha Apertura", fechaApertura, fuenteEtiqueta, fuenteTexto, esGris, colorGrisClaro, colorBorde);
                    esGris = !esGris;
                    string fechaCierre = fila["Fecha Cierre"] != DBNull.Value ? Convert.ToDateTime(fila["Fecha Cierre"]).ToString("dd/MM/yyyy HH:mm") : "Caja abierta";
                    AgregarFilaDetalle(tabla, "Fecha Cierre", fechaCierre, fuenteEtiqueta, fuenteTexto, esGris, colorGrisClaro, colorBorde);
                    esGris = !esGris;
                    AgregarFilaDetalle(tabla, "Saldo Inicial", FormatoMoneda(fila["Saldo Inicial"]), fuenteEtiqueta, fuenteTexto, esGris, colorGrisClaro, colorBorde);
                    esGris = !esGris;
                    AgregarFilaDetalle(tabla, "Ingresos", FormatoMoneda(fila["Ingresos"]), fuenteEtiqueta, fuenteTexto, esGris, colorGrisClaro, colorBorde);
                    esGris = !esGris;
                    AgregarFilaDetalle(tabla, "Egresos", FormatoMoneda(fila["Egresos"]), fuenteEtiqueta, fuenteTexto, esGris, colorGrisClaro, colorBorde);
                    esGris = !esGris;
                    AgregarFilaDetalle(tabla, "Monto Esperado", FormatoMoneda(fila["Monto Esperado"]), fuenteEtiqueta, fuenteTexto, esGris, colorGrisClaro, colorBorde);
                    esGris = !esGris;
                    AgregarFilaDetalle(tabla, "Monto Arqueado", FormatoMoneda(fila["Monto Arqueado"]), fuenteEtiqueta, fuenteTexto, esGris, colorGrisClaro, colorBorde);
                    esGris = !esGris;
                    AgregarFilaDetalle(tabla, "Diferencia", FormatoMoneda(fila["Diferencia"]), fuenteEtiqueta, fuenteTexto, esGris, colorGrisClaro, colorBorde);
                    esGris = !esGris;
                    AgregarFilaDetalle(tabla, "Saldo Final", FormatoMoneda(fila["Saldo Final"]), fuenteEtiqueta, fuenteTexto, esGris, colorGrisClaro, colorBorde);
                    esGris = !esGris;
                    AgregarFilaDetalle(tabla, "Tipo de Cambio", FormatoMoneda(fila["Tipo Cambio"]), fuenteEtiqueta, fuenteTexto, esGris, colorGrisClaro, colorBorde);
                    esGris = !esGris;
                    AgregarFilaDetalle(tabla, "Estado", fila["Estado"].ToString(), fuenteEtiqueta, fuenteTexto, esGris, colorGrisClaro, colorBorde);

                    documento.Add(tabla);
                    documento.Close();
                }

                Process.Start(ruta);
            }
            catch (Exception ex)
            {
                if (documento.IsOpen()) documento.Close();
                MessageBox.Show("Error al generar el PDF:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void AgregarFilaDetalle(PdfPTable tabla, string etiqueta, string valor, Font fuenteEt, Font fuenteVal, bool esGris, BaseColor colorGris, BaseColor colorBorde)
        {
            BaseColor bg = esGris ? colorGris : BaseColor.WHITE;
            PdfPCell celdaEtiqueta = new PdfPCell(new Phrase(etiqueta, fuenteEt)) { BackgroundColor = bg, BorderColor = colorBorde, Padding = 5f };
            PdfPCell celdaValor = new PdfPCell(new Phrase(valor, fuenteVal)) { BackgroundColor = bg, BorderColor = colorBorde, Padding = 5f };
            tabla.AddCell(celdaEtiqueta);
            tabla.AddCell(celdaValor);
        }

        private static string FormatoMoneda(object valor)
        {
            if (valor == null || valor == DBNull.Value) return "C$ 0.00";
            decimal numero = Convert.ToDecimal(valor);
            return $"C$ {numero:N2}";
        }
    }
}