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

            string carpeta =
                Path.Combine(
                    Application.StartupPath,
                    "Reportes");

            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            string nombreArchivo =
                "Reporte_Productos_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") +
                ".pdf";

            string ruta =
                Path.Combine(carpeta, nombreArchivo);

            Document documento =
                new Document(
                    PageSize.A4.Rotate(),
                    30,
                    30,
                    30,
                    30);

            try
            {
                PdfWriter.GetInstance(
                    documento,
                    new FileStream(
                        ruta,
                        FileMode.Create));

                documento.Open();

                Font titulo =
                    FontFactory.GetFont(
                        FontFactory.HELVETICA_BOLD,
                        18);

                Font subtitulo =
                    FontFactory.GetFont(
                        FontFactory.HELVETICA_BOLD,
                        12);

                Font normal =
                    FontFactory.GetFont(
                        FontFactory.HELVETICA,
                        9);

                Paragraph encabezado =
                    new Paragraph(
                        "PAYLESS SHOESOURCE",
                        titulo);

                encabezado.Alignment =
                    Element.ALIGN_CENTER;

                documento.Add(encabezado);

                Paragraph reporte =
                    new Paragraph(
                        "REPORTE DE PRODUCTOS",
                        subtitulo);

                reporte.Alignment =
                    Element.ALIGN_CENTER;

                documento.Add(reporte);

                documento.Add(
                    new Paragraph(
                        "Fecha de generación: " +
                        DateTime.Now.ToString(
                            "dd/MM/yyyy HH:mm"),
                        normal));

                documento.Add(
                    new Paragraph(" "));

                PdfPTable tabla =
                    new PdfPTable(9);

                tabla.WidthPercentage = 100;

                tabla.SetWidths(new float[]
                {
                    7,
                    18,
                    14,
                    14,
                    16,
                    12,
                    14,
                    10,
                    10
                });

                AgregarEncabezado(
                    tabla,
                    "ID");

                AgregarEncabezado(
                    tabla,
                    "Producto");

                AgregarEncabezado(
                    tabla,
                    "Categoría");

                AgregarEncabezado(
                    tabla,
                    "Marca");

                AgregarEncabezado(
                    tabla,
                    "Proveedor");

                AgregarEncabezado(
                    tabla,
                    "Precio");

                AgregarEncabezado(
                    tabla,
                    "Tallas");

                AgregarEncabezado(
                    tabla,
                    "Stock");

                AgregarEncabezado(
                    tabla,
                    "Estado");

                foreach (DataRow fila in datos.Rows)
                {
                    tabla.AddCell(
                        new Phrase(
                            fila["ID Producto"].ToString(),
                            normal));

                    tabla.AddCell(
                        new Phrase(
                            fila["Producto"].ToString(),
                            normal));

                    tabla.AddCell(
                        new Phrase(
                            fila["Categoría"].ToString(),
                            normal));

                    tabla.AddCell(
                        new Phrase(
                            fila["Marca"].ToString(),
                            normal));

                    tabla.AddCell(
                        new Phrase(
                            fila["Proveedor"].ToString(),
                            normal));

                    tabla.AddCell(
                        new Phrase(
                            FormatoMoneda(
                                fila["Precio Venta"]),
                            normal));

                    tabla.AddCell(
                        new Phrase(
                            fila["Tallas"].ToString(),
                            normal));

                    tabla.AddCell(
                        new Phrase(
                            fila["Stock Total"].ToString(),
                            normal));

                    tabla.AddCell(
                        new Phrase(
                            fila["Estado"].ToString(),
                            normal));
                }

                documento.Add(tabla);

                documento.Add(
                    new Paragraph(" "));

                Paragraph pie =
                    new Paragraph(
                        "Payless ShoeSource Nicaragua\n" +
                        "Dirección: Managua, Nicaragua\n" +
                        "Teléfono: +505 2222-0000",
                        normal);

                pie.Alignment =
                    Element.ALIGN_CENTER;

                documento.Add(pie);

                documento.Close();

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
                    "Error al generar el PDF:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static void AgregarEncabezado(
            PdfPTable tabla,
            string texto)
        {
            Font negrita =
                FontFactory.GetFont(
                    FontFactory.HELVETICA_BOLD,
                    9);

            PdfPCell celda =
                new PdfPCell(
                    new Phrase(
                        texto,
                        negrita));

            celda.HorizontalAlignment =
                Element.ALIGN_CENTER;

            tabla.AddCell(celda);
        }

        private static string FormatoMoneda(
            object valor)
        {
            if (valor == null ||
                valor == DBNull.Value)
            {
                return "C$ 0.00";
            }

            decimal numero =
                Convert.ToDecimal(valor);

            return "C$ " +
                   numero.ToString("N2");
        }

    }
}
