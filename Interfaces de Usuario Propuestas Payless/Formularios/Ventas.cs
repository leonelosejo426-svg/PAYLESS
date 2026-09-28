
using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using Interfaces_de_Usuario_Propuestas_Payless.Entidades;
using Interfaces_de_Usuario_Propuestas_Payless.Utilidades;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Net.WebRequestMethods;


namespace Interfaces_de_Usuario_Propuestas_Payless
{
    
    public partial class Ventas : Form
    {


        private VentaDAO ventaDAO = new VentaDAO();

        private ArbolBinarioVentas arbolVentas =
            new ArbolBinarioVentas();

        private DataTable tablaVentas;

        public Ventas()
        {
            InitializeComponent();
        }
       

    



        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show(); this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo();
            ventana.Show(); this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show(); this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
          
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito();
            ventana.Show(); this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show(); this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Reportes_Venta ventana = new Reportes_Venta();
            ventana.Show(); this.Hide();

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Devoluciones_cs ventana = new Devoluciones_cs(); 
            ventana.Show();
            this.Hide();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label22_Click_1(object sender, EventArgs e)
        {
            inventario ventana = new inventario();
            ventana.Show(); this.Hide();
        }

        private void label38_Click(object sender, EventArgs e)
        {
            try
            {
                string rutaPDF = @"C:\Users\Lenovo\Desktop\PAYLESS\Interfaces de Usuario Propuestas Payless\Ayuda\Manual_Usuario.pdf";

                if (!File.Exists(rutaPDF))
                {
                    MessageBox.Show($"No se encontró el manual en la ruta:\n{rutaPDF}",
                                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Convertir la ruta del disco a formato URI web (maneja espacios y caracteres especiales)
                string uriPDF = new Uri(rutaPDF).AbsoluteUri;

                // 2. Cambia el número 5 por el número exacto de la página de Caja
                int numeroPaginaCaja = 5;

                // 3. Crear el argumento en formato file:///C:/...#page=2
                string argumentos = $"\"{uriPDF}#page={numeroPaginaCaja}\"";

                ProcessStartInfo edgeInfo = new ProcessStartInfo
                {
                    FileName = "msedge.exe",
                    Arguments = argumentos,
                    UseShellExecute = true
                };

                Process.Start(edgeInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el manual: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private void btnNuevaVenta_Click(object sender, EventArgs e)
        {
           
        }



        private void Ventas_Load(object sender, EventArgs e)
        {
            cbBuscarPor.Items.Clear();

            cbBuscarPor.Items.Add("Código");
            cbBuscarPor.Items.Add("Cliente");
            cbBuscarPor.Items.Add("Fecha");
            cbBuscarPor.Items.Add("ID");

            cbBuscarPor.SelectedIndex = 0;

            CargarVentas();
        }

        private void CargarVentas()
        {
            tablaVentas = ventaDAO.MostrarVentas();

            arbolVentas = new ArbolBinarioVentas();

            foreach (DataRow fila in tablaVentas.Rows)
            {
                arbolVentas.Insertar(fila);
            }

            MostrarVentasEnGrid(
                arbolVentas.RecorridoInOrden());
        }


        private void MostrarVentasEnGrid(List<DataRow> ventas)
        {
            DataTable tablaResultado = tablaVentas.Clone();

            foreach (DataRow fila in ventas)
            {
                tablaResultado.ImportRow(fila);
            }

            DGVtabla1.DataSource = tablaResultado;

            ConfigurarGrid();
        }


        private void ConfigurarGrid()
        {
            if (DGVtabla1.Columns.Count == 0)
                return;

            DGVtabla1.Columns["id_venta"].Visible = false;

            DGVtabla1.Columns["codigo_venta"].HeaderText = "Código";
            DGVtabla1.Columns["fecha"].HeaderText = "Fecha";
            DGVtabla1.Columns["cliente"].HeaderText = "Cliente";
            DGVtabla1.Columns["subtotal"].HeaderText = "Subtotal";
            DGVtabla1.Columns["iva"].HeaderText = "IVA";
            DGVtabla1.Columns["total"].HeaderText = "Total";
            DGVtabla1.Columns["estado"].HeaderText = "Estado";

            DGVtabla1.Columns["subtotal"]
                .DefaultCellStyle.Format = "C2";

            DGVtabla1.Columns["iva"]
                .DefaultCellStyle.Format = "C2";

            DGVtabla1.Columns["total"]
                .DefaultCellStyle.Format = "C2";

            DGVtabla1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            DGVtabla1.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            DGVtabla1.MultiSelect = false;

            DGVtabla1.ReadOnly = true;

            DGVtabla1.AllowUserToAddRows = false;
        }




        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        
    }

        private void cmbTalla_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Creas la instancia de la subpantalla
            NuevaVenta ventana = new NuevaVenta();

            // Con .Show() se abre sin bloquear ni ocultar el formulario principal
            ventana.Show();
            this.Show();
        }
        
       
        private void btnEditar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (DGVtabla1.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione una venta.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int idVenta = Convert.ToInt32(
                DGVtabla1.SelectedRows[0]
                .Cells["id_venta"].Value);

            string codigoVenta =
                DGVtabla1.SelectedRows[0]
                .Cells["codigo_venta"]
                .Value.ToString();

            string estado =
                DGVtabla1.SelectedRows[0]
                .Cells["estado"]
                .Value.ToString();

            if (estado == "Anulado")
            {
                MessageBox.Show(
                    "Esta venta ya está anulada.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Desea anular la venta " +
                codigoVenta + "?",
                "Confirmar anulación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                bool resultado =
                    ventaDAO.EliminarVenta(idVenta);

                if (resultado)
                {
                    MessageBox.Show(
                        "Venta anulada correctamente.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CargarVentas();
                }
            }



        }

     

        private void rbEfectivo_CheckedChanged(object sender, EventArgs e)
        {
     
        }

        private void rbTarjeta_CheckedChanged(object sender, EventArgs e)
        {
           
        }

       
        private void txtMontoCordobas_TextChanged(object sender, EventArgs e)
        {
       
        }

        private void txtMontoDolares_TextChanged(object sender, EventArgs e)
        {
           
           
        }

       
        private void btnGuardarVenta_Click(object sender, EventArgs e)
        {
           
        }

       
      

        private void btnImprimirFactura_Click(object sender, EventArgs e)
        {
            if (DGVtabla1.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione una venta para imprimir.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int idVenta = Convert.ToInt32(DGVtabla1.SelectedRows[0].Cells["id_venta"].Value);

            // Llamada al método centralizado de impresión
            ImprimirFacturaPorId(idVenta);

        }

        private void GenerarFacturaPDF(
     string rutaPDF,
     string codigoVenta,
     DateTime fecha,
     string cliente,
     string usuario,
     decimal subtotal,
     decimal iva,
     decimal total,
     DataTable detalle)
        {
            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 30f, 30f, 30f, 30f);

            using (FileStream fs = new FileStream(rutaPDF, FileMode.Create))
            {
                iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, fs);
                doc.Open();

                // 1. PALETA DE COLORES CORPORATIVA PAYLESS (Naranja #F26522 y Gris Oscuro)
                iTextSharp.text.BaseColor colorNaranjaPayless = new iTextSharp.text.BaseColor(242, 101, 34);
                iTextSharp.text.BaseColor colorTexto = new iTextSharp.text.BaseColor(40, 40, 40);
                iTextSharp.text.BaseColor colorGrisClaro = new iTextSharp.text.BaseColor(248, 249, 250);
                iTextSharp.text.BaseColor colorBorde = new iTextSharp.text.BaseColor(220, 220, 220);

                iTextSharp.text.Font fuenteTitulo = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 16f, colorNaranjaPayless);
                iTextSharp.text.Font fuenteSubtitulo = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 9f, iTextSharp.text.BaseColor.GRAY);
                iTextSharp.text.Font fuenteEtiqueta = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 9f, colorTexto);
                iTextSharp.text.Font fuenteTexto = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 8.5f, colorTexto);
                iTextSharp.text.Font fuenteCabeceraTabla = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 8.5f, iTextSharp.text.BaseColor.WHITE);
                iTextSharp.text.Font fuenteTotalBold = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 10f, colorNaranjaPayless);

                // 2. ENCABEZADO Y CARGA DEL LOGO
                iTextSharp.text.pdf.PdfPTable headerTable = new iTextSharp.text.pdf.PdfPTable(2);
                headerTable.WidthPercentage = 100f;
                headerTable.SetWidths(new float[] { 50f, 50f });

                iTextSharp.text.pdf.PdfPCell cellLeft = new iTextSharp.text.pdf.PdfPCell();
                cellLeft.Border = iTextSharp.text.Rectangle.NO_BORDER;

                // Nombre de la imagen exacto especificado en Respaldos
                string rutaBase = Path.Combine(Application.StartupPath, "Respaldos", "WhatsApp Image 2026-06-17 at 11.49.19 AM");
                string rutaLogo = File.Exists(rutaBase + ".jpeg") ? rutaBase + ".jpeg" : (File.Exists(rutaBase + ".jpg") ? rutaBase + ".jpg" : rutaBase);

                if (File.Exists(rutaLogo))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(rutaLogo);
                    logo.ScaleToFit(140f, 60f);
                    cellLeft.AddElement(logo);
                }
                else
                {
                    cellLeft.AddElement(new iTextSharp.text.Paragraph("PAYLESS SHOESOURCE", fuenteTitulo));
                    cellLeft.AddElement(new iTextSharp.text.Paragraph("Calzado y Accesorios", fuenteSubtitulo));
                }

                headerTable.AddCell(cellLeft);

                iTextSharp.text.pdf.PdfPCell cellRight = new iTextSharp.text.pdf.PdfPCell();
                cellRight.Border = iTextSharp.text.Rectangle.NO_BORDER;

                iTextSharp.text.Paragraph pTituloFactura = new iTextSharp.text.Paragraph("FACTURA DE VENTA", fuenteTitulo);
                pTituloFactura.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
                cellRight.AddElement(pTituloFactura);

                iTextSharp.text.Paragraph pNumFactura = new iTextSharp.text.Paragraph($"Nº: {codigoVenta}", iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 11f, colorTexto));
                pNumFactura.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
                cellRight.AddElement(pNumFactura);

                iTextSharp.text.Paragraph pFecha = new iTextSharp.text.Paragraph($"Fecha: {fecha:dd/MM/yyyy HH:mm}", fuenteTexto);
                pFecha.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
                cellRight.AddElement(pFecha);

                headerTable.AddCell(cellRight);
                doc.Add(headerTable);

                // Línea divisora en color Naranja
                doc.Add(new iTextSharp.text.Paragraph(" "));
                iTextSharp.text.pdf.PdfPTable line = new iTextSharp.text.pdf.PdfPTable(1);
                line.WidthPercentage = 100f;
                iTextSharp.text.pdf.PdfPCell lineCell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(""));
                lineCell.BorderWidthBottom = 2f;
                lineCell.BorderColorBottom = colorNaranjaPayless;
                lineCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                line.AddCell(lineCell);
                doc.Add(line);

                doc.Add(new iTextSharp.text.Paragraph(" ", iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 4f)));

                // 3. DATOS DE CLIENTE Y CAJERO
                iTextSharp.text.pdf.PdfPTable infoTable = new iTextSharp.text.pdf.PdfPTable(2);
                infoTable.WidthPercentage = 100f;
                infoTable.SetWidths(new float[] { 50f, 50f });

                iTextSharp.text.pdf.PdfPCell cellCliente = new iTextSharp.text.pdf.PdfPCell();
                cellCliente.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellCliente.AddElement(new iTextSharp.text.Phrase($"Cliente: {cliente}", fuenteEtiqueta));
                infoTable.AddCell(cellCliente);

                iTextSharp.text.pdf.PdfPCell cellAtendido = new iTextSharp.text.pdf.PdfPCell();
                cellAtendido.Border = iTextSharp.text.Rectangle.NO_BORDER;
                iTextSharp.text.Paragraph pAtendido = new iTextSharp.text.Paragraph($"Atendido por: {usuario}", fuenteTexto);
                pAtendido.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
                cellAtendido.AddElement(pAtendido);
                infoTable.AddCell(cellAtendido);

                doc.Add(infoTable);
                doc.Add(new iTextSharp.text.Paragraph(" \n"));

                // 4. TABLA DE PRODUCTOS (7 COLUMNAS) CON ENCABEZADO NARANJA
                iTextSharp.text.pdf.PdfPTable tabla = new iTextSharp.text.pdf.PdfPTable(7);
                tabla.WidthPercentage = 100f;
                tabla.SetWidths(new float[] { 28f, 14f, 14f, 10f, 8f, 13f, 13f });

                string[] encabezados = { "Producto", "Marca", "Categoría", "Talla", "Cant.", "Precio", "Subtotal" };
                foreach (string enc in encabezados)
                {
                    iTextSharp.text.pdf.PdfPCell cellHeader = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(enc, fuenteCabeceraTabla));
                    cellHeader.BackgroundColor = colorNaranjaPayless;
                    cellHeader.HorizontalAlignment = (enc == "Producto" || enc == "Marca" || enc == "Categoría")
                        ? iTextSharp.text.Element.ALIGN_LEFT
                        : iTextSharp.text.Element.ALIGN_CENTER;
                    cellHeader.Padding = 5f;
                    cellHeader.BorderColor = colorNaranjaPayless;
                    tabla.AddCell(cellHeader);
                }

                bool esGris = false;
                foreach (DataRow fila in detalle.Rows)
                {
                    iTextSharp.text.BaseColor bgFila = esGris ? colorGrisClaro : iTextSharp.text.BaseColor.WHITE;

                    AgregarCeldaTabla(tabla, fila["producto"].ToString(), fuenteTexto, bgFila, colorBorde, iTextSharp.text.Element.ALIGN_LEFT);
                    AgregarCeldaTabla(tabla, fila["marca"].ToString(), fuenteTexto, bgFila, colorBorde, iTextSharp.text.Element.ALIGN_LEFT);
                    AgregarCeldaTabla(tabla, fila["categoria"].ToString(), fuenteTexto, bgFila, colorBorde, iTextSharp.text.Element.ALIGN_LEFT);
                    AgregarCeldaTabla(tabla, fila["talla"].ToString(), fuenteTexto, bgFila, colorBorde, iTextSharp.text.Element.ALIGN_CENTER);
                    AgregarCeldaTabla(tabla, fila["cantidad"].ToString(), fuenteTexto, bgFila, colorBorde, iTextSharp.text.Element.ALIGN_CENTER);

                    decimal precio = Convert.ToDecimal(fila["precio_venta"]);
                    decimal sub = Convert.ToDecimal(fila["subtotal"]);

                    AgregarCeldaTabla(tabla, $"C$ {precio:N2}", fuenteTexto, bgFila, colorBorde, iTextSharp.text.Element.ALIGN_RIGHT);
                    AgregarCeldaTabla(tabla, $"C$ {sub:N2}", fuenteTexto, bgFila, colorBorde, iTextSharp.text.Element.ALIGN_RIGHT);

                    esGris = !esGris;
                }

                doc.Add(tabla);
                doc.Add(new iTextSharp.text.Paragraph(" "));

                // 5. RESUMEN DE TOTALES
                iTextSharp.text.pdf.PdfPTable tablaTotales = new iTextSharp.text.pdf.PdfPTable(2);
                tablaTotales.WidthPercentage = 45f;
                tablaTotales.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                tablaTotales.SetWidths(new float[] { 45f, 55f });

                AgregarFilaTotal(tablaTotales, "Subtotal:", $"C$ {subtotal:N2}", fuenteTexto, false, colorBorde);
                AgregarFilaTotal(tablaTotales, "IVA (15%):", $"C$ {iva:N2}", fuenteTexto, false, colorBorde);
                AgregarFilaTotal(tablaTotales, "TOTAL:", $"C$ {total:N2}", fuenteTotalBold, true, colorNaranjaPayless);

                doc.Add(tablaTotales);

                // 6. PIE DE PÁGINA
                doc.Add(new iTextSharp.text.Paragraph(" \n"));
                iTextSharp.text.Paragraph leyenda = new iTextSharp.text.Paragraph(
                    "Payless ShoeSource Nicaragua\n" +
                    "Dirección: Managua, Nicaragua | Teléfono: +505 2222-0000\n" +
                    "¡Gracias por su compra! Conserve este comprobante para cambios.", fuenteSubtitulo)
                {
                    Alignment = iTextSharp.text.Element.ALIGN_CENTER
                };

                doc.Add(leyenda);
                doc.Close();
            }
        }

        public void ImprimirFacturaPorId(int idVenta)
        {
            // 1. Obtener datos mediante tu DAO
            DataTable venta = ventaDAO.ObtenerVentaPorId(idVenta);
            DataTable detalle = ventaDAO.ObtenerDetalleVentaPorId(idVenta);

            if (venta == null || venta.Rows.Count == 0 || detalle == null || detalle.Rows.Count == 0)
            {
                MessageBox.Show("No se encontró la venta o no tiene productos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Extraer los datos necesarios
            DataRow f = venta.Rows[0];
            string codigo = f["codigo_venta"].ToString();
            string ruta = System.IO.Path.Combine(Application.StartupPath, $"Factura_{codigo}.pdf");

            // 3. Generar PDF (usando el método iTextSharp que ya tienes)
            GenerarFacturaPDF(
                ruta,
                codigo,
                Convert.ToDateTime(f["fecha"]),
                f["cliente"].ToString(),
                "N/A",
                Convert.ToDecimal(f["subtotal"]),
                Convert.ToDecimal(f["iva"]),
                Convert.ToDecimal(f["total"]),
                detalle
            );

            // 4. Abrir el PDF automáticamente
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(ruta) { UseShellExecute = true });
        }

        private void AgregarCeldaTabla(iTextSharp.text.pdf.PdfPTable tabla, string texto, iTextSharp.text.Font fuente, iTextSharp.text.BaseColor bg, iTextSharp.text.BaseColor borde, int alineacion)
        {
            iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(texto, fuente));
            cell.BackgroundColor = bg;
            cell.BorderColor = borde;
            cell.Padding = 4f;
            cell.HorizontalAlignment = alineacion;
            tabla.AddCell(cell);
        }

        private void AgregarFilaTotal(iTextSharp.text.pdf.PdfPTable tabla, string etiqueta, string valor, iTextSharp.text.Font fuente, bool destacar, iTextSharp.text.BaseColor colorBorde)
        {
            iTextSharp.text.pdf.PdfPCell cEtiq = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(etiqueta, fuente));
            cEtiq.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cEtiq.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            cEtiq.Padding = 3f;

            iTextSharp.text.pdf.PdfPCell cVal = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(valor, fuente));
            cVal.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            cVal.Padding = 3f;

            if (destacar)
            {
                cVal.BorderColor = colorBorde;
                cVal.BorderWidth = 1f;
                cEtiq.BorderColor = colorBorde;
            }
            else
            {
                cVal.Border = iTextSharp.text.Rectangle.NO_BORDER;
            }

            tabla.AddCell(cEtiq);
            tabla.AddCell(cVal);
        }




        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private void btnCargarVenta_Click(object sender, EventArgs e)
        {
             }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento();
            ventana.Show();
            this.Hide();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string valor = txtBuscar.Text.Trim();

            if (string.IsNullOrEmpty(valor))
            {
                CargarVentas();
                return;
            }

            string campo = cbBuscarPor.Text;

            List<DataRow> resultados =
                arbolVentas.Buscar(campo, valor);

            MostrarVentasEnGrid(resultados);

            if (resultados.Count == 0)
            {
                MessageBox.Show(
                    "No se encontraron ventas.",
                    "Búsqueda",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }


        }

        private void txtBuscar_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
                e.SuppressKeyPress = true;
            }
        }




    }

}
