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
    internal class GeneradorReporteUsuariosPDF
    {

        public static void Generar(DataTable datos)
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

            string carpeta =
                Path.Combine(
                    Application.StartupPath,
                    "Reportes");

            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            string nombreArchivo =
                "Reporte_Usuarios_" +
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
                        8);

                Paragraph encabezado =
                    new Paragraph(
                        "PAYLESS SHOESOURCE",
                        titulo);

                encabezado.Alignment =
                    Element.ALIGN_CENTER;

                documento.Add(encabezado);

                Paragraph reporte =
                    new Paragraph(
                        "REPORTE DE USUARIOS",
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
                    new PdfPTable(11);

                tabla.WidthPercentage = 100;

                tabla.SetWidths(new float[]
                {
                    6,
                    10,
                    18,
                    20,
                    12,
                    13,
                    11,
                    14,
                    10,
                    12,
                    12
                });

                string[] encabezados =
                {
                    "ID",
                    "Usuario",
                    "Nombre",
                    "Correo",
                    "Rol",
                    "Registro",
                    "Estado",
                    "Último ingreso",
                    "Total",
                    "Exitosos",
                    "Fallidos"
                };

                foreach (string encabezadoColumna
                    in encabezados)
                {
                    AgregarEncabezado(
                        tabla,
                        encabezadoColumna);
                }

                foreach (DataRow fila in datos.Rows)
                {
                    tabla.AddCell(
                        new Phrase(
                            fila["ID Usuario"].ToString(),
                            normal));

                    tabla.AddCell(
                        new Phrase(
                            fila["Usuario"].ToString(),
                            normal));

                    tabla.AddCell(
                        new Phrase(
                            fila["Nombre Completo"].ToString(),
                            normal));

                    tabla.AddCell(
                        new Phrase(
                            fila["Correo"].ToString(),
                            normal));

                    tabla.AddCell(
                        new Phrase(
                            fila["Rol"].ToString(),
                            normal));

                    tabla.AddCell(
                        new Phrase(
                            FormatoFecha(
                                fila["Fecha Registro"]),
                            normal));

                    tabla.AddCell(
                        new Phrase(
                            fila["Estado"].ToString(),
                            normal));

                    tabla.AddCell(
                        new Phrase(
                            FormatoFecha(
                                fila["Último Ingreso"]),
                            normal));

                    tabla.AddCell(
                        new Phrase(
                            fila["Total Ingresos"].ToString(),
                            normal));

                    tabla.AddCell(
                        new Phrase(
                            fila["Ingresos Exitosos"].ToString(),
                            normal));

                    tabla.AddCell(
                        new Phrase(
                            fila["Ingresos Fallidos"].ToString(),
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
                    "Reporte de usuarios generado correctamente.",
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
                    8);

            PdfPCell celda =
                new PdfPCell(
                    new Phrase(
                        texto,
                        negrita));

            celda.HorizontalAlignment =
                Element.ALIGN_CENTER;

            tabla.AddCell(celda);
        }

        private static string FormatoFecha(
            object valor)
        {
            if (valor == null ||
                valor == DBNull.Value)
            {
                return "Sin registro";
            }

            return Convert.ToDateTime(valor)
                .ToString("dd/MM/yyyy HH:mm");
        }
    }
}
