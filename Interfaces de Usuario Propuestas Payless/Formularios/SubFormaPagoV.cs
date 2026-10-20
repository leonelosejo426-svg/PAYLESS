using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextDocument = iTextSharp.text.Document;
using iTextFont = iTextSharp.text.Font;
using iTextElement = iTextSharp.text.Element;

namespace Interfaces_de_Usuario_Propuestas_Payless.Formularios
{
    public partial class SubFormaPagoV : Form
    {



        private VentaDAO ventaDAO;

        private string codigoVenta;
        private int idCliente;
        private int idUsuario;
        private int idCaja;

        private decimal subtotal;
        private decimal iva;
        private decimal total;

        private DataTable detalleVenta;

        private decimal tipoCambioActual;
        private bool pagoConfirmado = false;



        public SubFormaPagoV()
        {
            InitializeComponent();
            ventaDAO = new VentaDAO();
        }

        public SubFormaPagoV(
           string codigoVenta,
           int idCliente,
           int idUsuario,
           int idCaja,
           decimal subtotal,
           decimal iva,
           decimal total,
           DataTable detalleVenta)
        {
            InitializeComponent();

            ventaDAO = new VentaDAO();

            this.codigoVenta = codigoVenta;
            this.idCliente = idCliente;
            this.idUsuario = idUsuario;
            this.idCaja = idCaja;

            this.subtotal = subtotal;
            this.iva = iva;
            this.total = total;

            this.detalleVenta = detalleVenta;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void SubFormaPagoV_Load(object sender, EventArgs e)
        {
            try
            {
                CargarInformacionVenta();
                CargarTipoCambio();
                ConfigurarFormulario();

                rbEfectivo.Checked = true;

                // ==========================================
                // FORZAR LA CONEXIÓN DE LOS EVENTOS DE TEXTO
                // ==========================================
                txtMontoCordobas.TextChanged -= txtMontoCordobas_TextChanged;
                txtMontoCordobas.TextChanged += txtMontoCordobas_TextChanged;

                txtMontoDolares.TextChanged -= txtMontoDolares_TextChanged;
                txtMontoDolares.TextChanged += txtMontoDolares_TextChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar la forma de pago:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }


        }

        // =====================================================
        // INFORMACIÓN DE LA VENTA
        // =====================================================

        private void CargarInformacionVenta()
        {
            lblCodigoVenta.Text =
       "N° " + codigoVenta;

            lblSubtotal.Text =
                "C$ " + subtotal.ToString("N2");

            lblIVA.Text =
                "C$ " + iva.ToString("N2");

            lblTotal.Text =
                "C$ " + total.ToString("N2");

            lblCliente.Text =
                ventaDAO.ObtenerNombreCliente(idCliente);
        }

        // =====================================================
        // TIPO DE CAMBIO
        // =====================================================

        private void CargarTipoCambio()
        {
            tipoCambioActual =
                ventaDAO.ObtenerTipoCambioActual();

            if (tipoCambioActual <= 0)
            {
                tipoCambioActual = 36.50m;
            }

            lblTipoCambio.Text =
                "C$ " + tipoCambioActual.ToString("N2");
        }

        // =====================================================
        // CONFIGURAR FORMULARIO
        // =====================================================

        private void ConfigurarFormulario()
        {
            txtMontoCordobas.Clear();
            txtMontoDolares.Clear();
            txtDigitosTarjeta.Clear();

            lblTotalEntregado.Text = "C$ 0.00";
            lblCambio.Text = "C$ 0.00";

            // ==========================================
            // TIPOS DE TARJETA
            // ==========================================

            cbTipoTarjeta.Items.Clear();

            cbTipoTarjeta.Items.Add("Visa");
            cbTipoTarjeta.Items.Add("Mastercard");
            cbTipoTarjeta.Items.Add("American Express");

            cbTipoTarjeta.SelectedIndex = -1;

            // ==========================================
            // PANELES
            // ==========================================

            panelEfectivo.Enabled = true;
            panelTarjeta.Enabled = false;

            btnImprimirFactura.Enabled = false;
        }

        private void rbEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbEfectivo.Checked)
                return;

            panelEfectivo.Enabled = true;
            panelTarjeta.Enabled = false;

            LimpiarTarjeta();
            CalcularEfectivo();
        }

        private void rbTarjeta_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbTarjeta.Checked)
                return;

            panelEfectivo.Enabled = false;
            panelTarjeta.Enabled = true;

            LimpiarEfectivo();

            lblMonto.Text =
                "C$ " + total.ToString("N2");
        }

        private void txtMontoCordobas_TextChanged(
            object sender,
            EventArgs e)
        {
            CalcularEfectivo();
        }

        // =====================================================
        // CAMBIO AL ESCRIBIR DOLARES
        // =====================================================

        private void txtMontoDolares_TextChanged(
            object sender,
            EventArgs e)
        {
            CalcularEfectivo();
        }

        // =====================================================
        // CALCULAR EFECTIVO
        // =====================================================

        private void CalcularEfectivo()
        {
            if (!rbEfectivo.Checked)
                return;

            decimal cordobas = ObtenerDecimal(txtMontoCordobas.Text);
            decimal dolares = ObtenerDecimal(txtMontoDolares.Text);

            decimal totalEntregado = cordobas + (dolares * tipoCambioActual);
            decimal cambio = totalEntregado - total;

            // Mostrar el total entregado en su etiqueta
            lblTotalEntregado.Text = "C$ " + totalEntregado.ToString("N2");

            // Mostrar el cambio en la etiqueta lblCambio
            if (cambio >= 0)
            {
                lblCambio.Text = "C$ " + cambio.ToString("N2");
            }
            else
            {
                lblCambio.Text = "C$ 0.00";
            }
        }

        // =====================================================
        // CONVERTIR TEXTO A DECIMAL
        // =====================================================

        private decimal ObtenerDecimal(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return 0;

            texto = texto.Replace(",", ".");

            if (decimal.TryParse(
                texto,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out decimal resultado))
            {
                return resultado;
            }

            return 0;
        }

        private void btnConfirmarPago_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbEfectivo.Checked)
                {
                    ConfirmarPagoEfectivo();
                }
                else if (rbTarjeta.Checked)
                {
                    ConfirmarPagoTarjeta();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al confirmar el pago:\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ConfirmarPagoEfectivo()
        {
            decimal montoCordobas = ObtenerDecimal(txtMontoCordobas.Text);
            decimal montoDolares = ObtenerDecimal(txtMontoDolares.Text);

            decimal totalEntregado = montoCordobas + (montoDolares * tipoCambioActual);

            if (totalEntregado < total)
            {
                MessageBox.Show(
                    "El monto entregado es insuficiente.\n\n" +
                    "Total a pagar: C$ " + total.ToString("N2") +
                    "\nTotal entregado: C$ " + totalEntregado.ToString("N2"),
                    "Pago insuficiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            decimal cambio = totalEntregado - total;

            // Validación de efectivo en caja para el cambio
            decimal efectivoEnCaja = ventaDAO.ObtenerEfectivoDisponibleEnCaja(idCaja);

            if (cambio > efectivoEnCaja)
            {
                MessageBox.Show(
                    "No hay suficiente efectivo en la caja para dar el cambio requerido.\n\n" +
                    "Efectivo disponible en caja: C$ " + efectivoEnCaja.ToString("N2") + "\n" +
                    "Cambio a devolver: C$ " + cambio.ToString("N2"),
                    "Cambio Insuficiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (montoCordobas <= 0 && montoDolares <= 0)
            {
                MessageBox.Show("Ingrese un monto válido mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMontoCordobas.Focus();
                return;
            }

            // Registramos el pago en efectivo pasando correctamente el cambio calculado
            RegistrarPago(
                "Efectivo",
                montoCordobas,
                montoDolares,
                cambio,
                "",
                0
                 
            );
           
        }

        private void ConfirmarPagoTarjeta()
        {
            if (cbTipoTarjeta.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el tipo de tarjeta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbTipoTarjeta.Focus();
                return;
            }

            string digitos = txtDigitosTarjeta.Text.Trim();

            // 1. Validación de longitud: Exactamente 4 dígitos numéricos
            if (string.IsNullOrWhiteSpace(digitos) || digitos.Length != 4 || !digitos.All(char.IsDigit))
            {
                MessageBox.Show("Debe ingresar exactamente los 4 dígitos de la tarjeta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDigitosTarjeta.Focus();
                return;
            }

            string fechaExp = txtVencimiento.Text.Trim();

            // 2. Validación de formato MM/YY
            if (!System.Text.RegularExpressions.Regex.IsMatch(fechaExp, @"^(0[1-9]|1[0-2])\/\d{2}$"))
            {
                MessageBox.Show("Ingrese una fecha de expiración válida en formato MM/YY (Ejemplo: 04/27).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVencimiento.Focus();
                return;
            }

            // 3. Validación de fecha no vencida
            string[] partes = fechaExp.Split('/');
            int mes = int.Parse(partes[0]);
            int anio = 2000 + int.Parse(partes[1]);

            DateTime ultimoDiaMes = new DateTime(anio, mes, 1).AddMonths(1).AddDays(-1);
            if (ultimoDiaMes < DateTime.Today)
            {
                MessageBox.Show("La tarjeta ingresada está vencida. Ingrese una fecha de expiración vigente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVencimiento.Focus();
                return;
            }

            string tipoTarjetaSeleccionada = cbTipoTarjeta.SelectedItem.ToString();
            decimal montoTarjeta = total;

            // Esto registrará el pago y activará el botón de imprimir factura correctamente
            RegistrarPago(
                "Tarjeta",
                0,
                0,
                0,
                tipoTarjetaSeleccionada,
                montoTarjeta
            );

           
        }

        // =====================================================
        // REGISTRAR VENTA Y PAGO
        // =====================================================

        private void RegistrarPago(
    string tipoPago,
    decimal montoCordobas,
    decimal montoDolares,
    decimal cambio,
    string tipoTarjeta,
    decimal montoTarjeta)
        {
            bool resultado =
                ventaDAO.RegistrarVentaConPago(
                    codigoVenta,
                    idCliente,
                    idUsuario,
                    idCaja,
                    subtotal,
                    iva,
                    total,
                    tipoPago,
                    montoCordobas,
                    montoDolares,
                    tipoCambioActual,
                    cambio,
                    tipoTarjeta,
                    montoTarjeta,
                    detalleVenta
                );

            if (!resultado)
            {
                MessageBox.Show(
                    "No se pudo registrar la venta.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            pagoConfirmado = true;

            btnConfirmarPago.Enabled = false;
            btnImprimirFactura.Enabled = true;

            // Mostramos el cambio de forma dinámica en el mensaje si fue pago en efectivo
            string mensajeCambio = tipoPago == "Efectivo" ? "\nCambio a devolver: C$ " + cambio.ToString("N2") : "\nMétodo: Pago con Tarjeta (" + tipoTarjeta + ")";

            MessageBox.Show(
                "Pago confirmado correctamente.\n\n" +
                "Venta: " + codigoVenta +
                "\nTotal: C$ " + total.ToString("N2") +
                mensajeCambio +
                "\n\nAhora puede imprimir la factura.",
                "Pago confirmado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                
            );
          
        }

        // =====================================================
        // CANCELAR
        // =====================================================

        private void btnCancelar_Click(
            object sender,
            EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // =====================================================
        // LIMPIAR EFECTIVO
        // =====================================================

        private void LimpiarEfectivo()
        {
            txtMontoCordobas.Clear();
            txtMontoDolares.Clear();

            lblTotalEntregado.Text = "C$ 0.00";
            lblCambio.Text = "C$ 0.00";
        }

        // =====================================================
        // LIMPIAR TARJETA
        // =====================================================

        private void LimpiarTarjeta()
        {
            cbTipoTarjeta.SelectedIndex = -1;
            txtDigitosTarjeta.Clear();

            lblMonto.Text = "C$ 0.00";
        }


        private void GenerarFacturaPDF(string ruta)
        {
            DataTable detalleFactura = ventaDAO.ObtenerDetalleParaFactura(detalleVenta);

            // Márgenes profesionales de 30f
            using (iTextDocument documento = new iTextDocument(iTextSharp.text.PageSize.A4, 30f, 30f, 30f, 30f))
            {
                PdfWriter.GetInstance(documento, new FileStream(ruta, FileMode.Create));
                documento.Open();

                // =====================================================
                // PALETA DE COLORES CORPORATIVA PAYLESS (#F26522 y Grises)
                // =====================================================
                iTextSharp.text.BaseColor colorNaranjaPayless = new iTextSharp.text.BaseColor(242, 101, 34);
                iTextSharp.text.BaseColor colorTexto = new iTextSharp.text.BaseColor(40, 40, 40);
                iTextSharp.text.BaseColor colorGrisClaro = new iTextSharp.text.BaseColor(248, 249, 250);
                iTextSharp.text.BaseColor colorBorde = new iTextSharp.text.BaseColor(220, 220, 220);

                // =====================================================
                // ESTILOS DE FUENTE
                // =====================================================
                iTextFont fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 15f, colorNaranjaPayless);
                iTextFont fuenteSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, iTextSharp.text.BaseColor.GRAY);
                iTextFont fuenteEtiqueta = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8.5f, colorTexto);
                iTextFont fuenteTexto = FontFactory.GetFont(FontFactory.HELVETICA, 8.5f, colorTexto);
                iTextFont fuenteCabeceraTabla = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8.5f, iTextSharp.text.BaseColor.WHITE);
                iTextFont fuenteTotalBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, colorNaranjaPayless);

                // =====================================================
                // ENCABEZADO Y LOGO (Diseño Dos Columnas)
                // =====================================================
                PdfPTable headerTable = new PdfPTable(2)
                {
                    WidthPercentage = 100f
                };
                headerTable.SetWidths(new float[] { 50f, 50f });

                // Columna Izquierda: Logo o Nombre Comercial
                PdfPCell cellLeft = new PdfPCell
                {
                    Border = iTextSharp.text.Rectangle.NO_BORDER
                };

                string rutaBase = Path.Combine(Application.StartupPath, "Respaldos", "WhatsApp Image 2026-06-17 at 11.49.19 AM");
                string rutaLogo = File.Exists(rutaBase + ".jpeg") ? rutaBase + ".jpeg" :
                                 (File.Exists(rutaBase + ".jpg") ? rutaBase + ".jpg" :
                                 Path.Combine(Application.StartupPath, "Imagenes", "logo.png"));

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

                // Columna Derecha: Título Factura y Fecha
                PdfPCell cellRight = new PdfPCell
                {
                    Border = iTextSharp.text.Rectangle.NO_BORDER
                };

                iTextSharp.text.Paragraph pTitulo = new iTextSharp.text.Paragraph("FACTURA DE VENTA", fuenteTitulo)
                {
                    Alignment = iTextElement.ALIGN_RIGHT
                };
                cellRight.AddElement(pTitulo);

                iTextSharp.text.Paragraph pFechaEmision = new iTextSharp.text.Paragraph($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}", fuenteTexto)
                {
                    Alignment = iTextElement.ALIGN_RIGHT
                };
                cellRight.AddElement(pFechaEmision);

                headerTable.AddCell(cellRight);
                documento.Add(headerTable);

                // Línea divisora Naranja Corporativa
                documento.Add(new iTextSharp.text.Paragraph(" "));
                PdfPTable line = new PdfPTable(1) { WidthPercentage = 100f };
                PdfPCell lineCell = new PdfPCell(new iTextSharp.text.Phrase(""))
                {
                    BorderWidthBottom = 2f,
                    BorderColorBottom = colorNaranjaPayless,
                    Border = iTextSharp.text.Rectangle.BOTTOM_BORDER
                };
                line.AddCell(lineCell);
                documento.Add(line);

                documento.Add(new iTextSharp.text.Paragraph(" ", FontFactory.GetFont(FontFactory.HELVETICA, 4f)));

                // =====================================================
                // INFORMACIÓN GENERAL
                // =====================================================
                PdfPTable infoTable = new PdfPTable(2) { WidthPercentage = 100f };
                infoTable.SetWidths(new float[] { 60f, 40f });

                PdfPCell cellInfoLeft = new PdfPCell { Border = iTextSharp.text.Rectangle.NO_BORDER };
                cellInfoLeft.AddElement(new iTextSharp.text.Phrase($"Código de Venta: {codigoVenta}", fuenteEtiqueta));
                infoTable.AddCell(cellInfoLeft);

                PdfPCell cellInfoRight = new PdfPCell { Border = iTextSharp.text.Rectangle.NO_BORDER };
                iTextSharp.text.Paragraph pInfoCli = new iTextSharp.text.Paragraph("Moneda: Córdobas (C$)", fuenteEtiqueta)
                {
                    Alignment = iTextElement.ALIGN_RIGHT
                };
                cellInfoRight.AddElement(pInfoCli);
                infoTable.AddCell(cellInfoRight);

                documento.Add(infoTable);
                documento.Add(new iTextSharp.text.Paragraph(" \n"));

                // =====================================================
                // TABLA DE DETALLE DE PRODUCTOS (7 COLUMNAS)
                // =====================================================
                PdfPTable tabla = new PdfPTable(7) { WidthPercentage = 100f };
                tabla.SetWidths(new float[] { 25f, 15f, 15f, 10f, 8f, 13f, 14f });

                string[] encabezados = { "Producto", "Marca", "Categoría", "Talla", "Cant.", "Precio", "Subtotal" };
                foreach (string enc in encabezados)
                {
                    PdfPCell cellHeader = new PdfPCell(new iTextSharp.text.Phrase(enc, fuenteCabeceraTabla))
                    {
                        BackgroundColor = colorNaranjaPayless,
                        HorizontalAlignment = (enc == "Precio" || enc == "Subtotal") ? iTextElement.ALIGN_RIGHT :
                                             ((enc == "Talla" || enc == "Cant.") ? iTextElement.ALIGN_CENTER : iTextElement.ALIGN_LEFT),
                        Padding = 5f,
                        BorderColor = colorNaranjaPayless
                    };
                    tabla.AddCell(cellHeader);
                }

                bool esGris = false;
                foreach (DataRow fila in detalleFactura.Rows)
                {
                    iTextSharp.text.BaseColor bgFila = esGris ? colorGrisClaro : iTextSharp.text.BaseColor.WHITE;

                    AgregarCeldaTabla(tabla, fila["producto"].ToString(), fuenteTexto, bgFila, colorBorde, iTextElement.ALIGN_LEFT);
                    AgregarCeldaTabla(tabla, fila["marca"].ToString(), fuenteTexto, bgFila, colorBorde, iTextElement.ALIGN_LEFT);
                    AgregarCeldaTabla(tabla, fila["categoria"].ToString(), fuenteTexto, bgFila, colorBorde, iTextElement.ALIGN_LEFT);
                    AgregarCeldaTabla(tabla, fila["talla"].ToString(), fuenteTexto, bgFila, colorBorde, iTextElement.ALIGN_CENTER);
                    AgregarCeldaTabla(tabla, fila["cantidad"].ToString(), fuenteTexto, bgFila, colorBorde, iTextElement.ALIGN_CENTER);

                    decimal pVenta = Convert.ToDecimal(fila["precio_venta"]);
                    decimal sTotal = Convert.ToDecimal(fila["subtotal"]);

                    AgregarCeldaTabla(tabla, $"C$ {pVenta:N2}", fuenteTexto, bgFila, colorBorde, iTextElement.ALIGN_RIGHT);
                    AgregarCeldaTabla(tabla, $"C$ {sTotal:N2}", fuenteTexto, bgFila, colorBorde, iTextElement.ALIGN_RIGHT);

                    esGris = !esGris;
                }

                documento.Add(tabla);
                documento.Add(new iTextSharp.text.Paragraph(" "));

                // =====================================================
                // TABLA DE TOTALES (Alineada a la Derecha)
                // =====================================================
                PdfPTable tablaTotales = new PdfPTable(2)
                {
                    WidthPercentage = 45f,
                    HorizontalAlignment = iTextElement.ALIGN_RIGHT
                };
                tablaTotales.SetWidths(new float[] { 45f, 55f });

                // Calculamos el cambio de forma segura según el tipo de pago seleccionado
                decimal cambioPdf = 0;
                if (rbEfectivo.Checked)
                {
                    decimal cordobas = ObtenerDecimal(txtMontoCordobas.Text);
                    decimal dolares = ObtenerDecimal(txtMontoDolares.Text);
                    decimal totalEntregado = cordobas + (dolares * tipoCambioActual);
                    cambioPdf = totalEntregado - total;
                    if (cambioPdf < 0) cambioPdf = 0;
                }

                AgregarFilaTotal(tablaTotales, "Subtotal:", $"C$ {subtotal:N2}", fuenteEtiqueta, colorTexto, colorBorde);
                AgregarFilaTotal(tablaTotales, "IVA (15%):", $"C$ {iva:N2}", fuenteEtiqueta, colorTexto, colorBorde);
                AgregarFilaTotal(tablaTotales, "TOTAL:", $"C$ {total:N2}", fuenteTotalBold, colorNaranjaPayless, colorBorde);

                // Si es efectivo mostramos el cambio, si es tarjeta mostramos que se pagó con tarjeta
                if (rbEfectivo.Checked)
                {
                    AgregarFilaTotal(tablaTotales, "Cambio:", $"C$ {cambioPdf:N2}", fuenteEtiqueta, colorTexto, colorBorde);
                }
                else
                {
                    string tarjetaNombre = cbTipoTarjeta.SelectedItem != null ? cbTipoTarjeta.SelectedItem.ToString() : "Tarjeta";
                    AgregarFilaTotal(tablaTotales, "Método:", tarjetaNombre, fuenteEtiqueta, colorTexto, colorBorde);
                }

                documento.Add(tablaTotales);
                documento.Add(new iTextSharp.text.Paragraph(" \n\n"));

                // =====================================================
                // LEYENDA / PIE DE PÁGINA CORPORATIVO
                // =====================================================
                iTextSharp.text.Paragraph leyenda = new iTextSharp.text.Paragraph(
                    "Payless ShoeSource Nicaragua\n" +
                    "Dirección: Managua, Nicaragua | Teléfono: +505 2222-0000\n" +
                    "¡Gracias por su compra!", fuenteSubtitulo)
                {
                    Alignment = iTextElement.ALIGN_CENTER
                };

                documento.Add(leyenda);
                documento.Close();
            }
        }

        // =====================================================
        // MÉTODOS AUXILIARES REUTILIZABLES
        // =====================================================
        private static void AgregarCeldaTabla(PdfPTable tabla, string texto, iTextSharp.text.Font fuente, iTextSharp.text.BaseColor backgroundColor, iTextSharp.text.BaseColor borderColor, int alignment)
        {
            PdfPCell cell = new PdfPCell(new iTextSharp.text.Phrase(texto, fuente))
            {
                BackgroundColor = backgroundColor,
                HorizontalAlignment = alignment,
                VerticalAlignment = iTextElement.ALIGN_MIDDLE,
                Padding = 5f,
                BorderColor = borderColor
            };
            tabla.AddCell(cell);
        }

        private static void AgregarFilaTotal(PdfPTable tabla, string etiqueta, string valor, iTextSharp.text.Font fuente, iTextSharp.text.BaseColor textColor, iTextSharp.text.BaseColor borderColor)
        {
            PdfPCell cell1 = new PdfPCell(new iTextSharp.text.Phrase(etiqueta, fuente))
            {
                BorderColor = borderColor,
                Padding = 4f,
                HorizontalAlignment = iTextElement.ALIGN_RIGHT
            };
            tabla.AddCell(cell1);

            PdfPCell cell2 = new PdfPCell(new iTextSharp.text.Phrase(valor, fuente))
            {
                BorderColor = borderColor,
                Padding = 4f,
                HorizontalAlignment = iTextElement.ALIGN_RIGHT
            };
            tabla.AddCell(cell2);
        }

        private void btnImprimirFactura_Click(object sender, EventArgs e)
        {
            if (!pagoConfirmado)
            {
                MessageBox.Show(
                    "Primero debe confirmar el pago.",
                    "Factura",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                // Ruta temporal para guardar y abrir el PDF directamente
                string tempFolder = System.IO.Path.GetTempPath();
                string rutaArchivo = System.IO.Path.Combine(tempFolder, $"Factura_{codigoVenta}.pdf");

                // Genera el archivo en la carpeta temporal
                GenerarFacturaPDF(rutaArchivo);

                // Abre el PDF en el visor predeterminado del sistema operativo
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(rutaArchivo)
                {
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al visualizar la factura:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
