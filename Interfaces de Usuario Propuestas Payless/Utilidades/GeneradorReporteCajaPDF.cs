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

        public static void Generar(
            DataTable datos,
            int idCaja)
        {
            if (datos == null ||
                datos.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No hay información para imprimir.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DataRow fila = null;

            foreach (DataRow registro in datos.Rows)
            {
                if (Convert.ToInt32(
                    registro["ID Caja"]) == idCaja)
                {
                    fila = registro;
                    break;
                }
            }

            if (fila == null)
            {
                MessageBox.Show(
                    "No se encontró la caja seleccionada.",
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
                "Reporte_Caja_" +
                idCaja +
                "_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") +
                ".pdf";

            string ruta =
                Path.Combine(
                    carpeta,
                    nombreArchivo);

            Document documento =
                new Document(
                    PageSize.A4,
                    40,
                    40,
                    40,
                    40);

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
                        10);

                Paragraph encabezado =
                    new Paragraph(
                        "PAYLESS SHOESOURCE",
                        titulo);

                encabezado.Alignment =
                    Element.ALIGN_CENTER;

                documento.Add(encabezado);

                Paragraph reporte =
                    new Paragraph(
                        "REPORTE DE CAJA",
                        subtitulo);

                reporte.Alignment =
                    Element.ALIGN_CENTER;

                documento.Add(reporte);

                documento.Add(
                    new Paragraph(" "));

                PdfPTable tabla =
                    new PdfPTable(2);

                tabla.WidthPercentage = 100;

                AgregarFila(
                    tabla,
                    "ID Caja",
                    fila["ID Caja"].ToString());

                AgregarFila(
                    tabla,
                    "Usuario",
                    fila["Usuario"].ToString());

                AgregarFila(
                    tabla,
                    "Fecha Apertura",
                    Convert.ToDateTime(
                        fila["Fecha Apertura"])
                    .ToString("dd/MM/yyyy HH:mm"));

                if (fila["Fecha Cierre"] != DBNull.Value)
                {
                    AgregarFila(
                        tabla,
                        "Fecha Cierre",
                        Convert.ToDateTime(
                            fila["Fecha Cierre"])
                        .ToString("dd/MM/yyyy HH:mm"));
                }
                else
                {
                    AgregarFila(
                        tabla,
                        "Fecha Cierre",
                        "Caja abierta");
                }

                AgregarFila(
                    tabla,
                    "Saldo Inicial",
                    FormatoMoneda(
                        fila["Saldo Inicial"]));

                AgregarFila(
                    tabla,
                    "Ingresos",
                    FormatoMoneda(
                        fila["Ingresos"]));

                AgregarFila(
                    tabla,
                    "Egresos",
                    FormatoMoneda(
                        fila["Egresos"]));

                AgregarFila(
                    tabla,
                    "Monto Esperado",
                    FormatoMoneda(
                        fila["Monto Esperado"]));

                AgregarFila(
                    tabla,
                    "Monto Arqueado",
                    FormatoMoneda(
                        fila["Monto Arqueado"]));

                AgregarFila(
                    tabla,
                    "Diferencia",
                    FormatoMoneda(
                        fila["Diferencia"]));

                AgregarFila(
                    tabla,
                    "Saldo Final",
                    FormatoMoneda(
                        fila["Saldo Final"]));

                AgregarFila(
                    tabla,
                    "Tipo de Cambio",
                    FormatoMoneda(
                        fila["Tipo Cambio"]));

                AgregarFila(
                    tabla,
                    "Estado",
                    fila["Estado"].ToString());

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

        private static void AgregarFila(
            PdfPTable tabla,
            string nombre,
            string valor)
        {
            Font negrita =
                FontFactory.GetFont(
                    FontFactory.HELVETICA_BOLD,
                    10);

            Font normal =
                FontFactory.GetFont(
                    FontFactory.HELVETICA,
                    10);

            PdfPCell celdaNombre =
                new PdfPCell(
                    new Phrase(
                        nombre,
                        negrita));

            PdfPCell celdaValor =
                new PdfPCell(
                    new Phrase(
                        valor,
                        normal));

            tabla.AddCell(celdaNombre);
            tabla.AddCell(celdaValor);
        }

        private static string FormatoMoneda(
            object valor)
        {
            if (valor == null ||
                valor == DBNull.Value)
                return "C$ 0.00";

            decimal numero =
                Convert.ToDecimal(valor);

            return "C$ " +
                   numero.ToString("N2");
        }

    }
}
