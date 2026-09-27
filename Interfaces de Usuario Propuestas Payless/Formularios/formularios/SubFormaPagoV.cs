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

            cbTipoTarjeta.SelectedIndex = -1;

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
                total.ToString("N2");
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

            decimal cordobas = ObtenerDecimal(
                txtMontoCordobas.Text
            );

            decimal dolares = ObtenerDecimal(
                txtMontoDolares.Text
            );

            decimal equivalenteDolares =
                dolares * tipoCambioActual;

            decimal totalEntregado =
                cordobas + equivalenteDolares;

            decimal cambio =
                totalEntregado - total;

            lblTotalEntregado.Text =
                "C$ " + totalEntregado.ToString("N2");

            if (cambio > 0)
            {
                lblCambio.Text =
                    "C$ " + cambio.ToString("N2");
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
            decimal montoCordobas =
                ObtenerDecimal(txtMontoCordobas.Text);

            decimal montoDolares =
                ObtenerDecimal(txtMontoDolares.Text);

            decimal totalEntregado =
                montoCordobas +
                (montoDolares * tipoCambioActual);

            if (totalEntregado < total)
            {
                MessageBox.Show(
                    "El monto entregado es insuficiente.\n\n" +
                    "Total a pagar: C$ " +
                    total.ToString("N2") +
                    "\nTotal entregado: C$ " +
                    totalEntregado.ToString("N2"),
                    "Pago insuficiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            decimal cambio =
                totalEntregado - total;

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
                MessageBox.Show(
                    "Seleccione el tipo de tarjeta.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string digitos =
                txtDigitosTarjeta.Text.Trim();

            if (string.IsNullOrWhiteSpace(digitos))
            {
                MessageBox.Show(
                    "Ingrese los últimos 4 dígitos de la tarjeta.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (digitos.Length != 4 ||
                !int.TryParse(digitos, out _))
            {
                MessageBox.Show(
                    "Debe ingresar exactamente 4 dígitos numéricos.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            decimal montoTarjeta =
                ObtenerDecimal(txtDigitosTarjeta.Text);

            if (montoTarjeta <= 0)
            {
                MessageBox.Show(
                    "Ingrese un monto válido para la tarjeta.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (montoTarjeta < total)
            {
                MessageBox.Show(
                    "El monto de la tarjeta es insuficiente.\n\n" +
                    "Total: C$ " +
                    total.ToString("N2") +
                    "\nMonto ingresado: C$ " +
                    montoTarjeta.ToString("N2"),
                    "Pago insuficiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Se guarda únicamente como referencia
            // el tipo de tarjeta.
            RegistrarPago(
                "Tarjeta",
                0,
                0,
                0,
                cbTipoTarjeta.Text,
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

            MessageBox.Show(
                "Pago confirmado correctamente.\n\n" +
                "Venta: " + codigoVenta +
                "\nTotal: C$ " + total.ToString("N2") +
                "\n\nAhora puede imprimir la factura.",
                "Pago confirmado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                        );
            pagoConfirmado = true;

            btnConfirmarPago.Enabled = false;
            btnImprimirFactura.Enabled = true;

        

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
            
        }


        private void GenerarFacturaPDF(string ruta)
        {
            DataTable detalleFactura = ventaDAO.ObtenerDetalleParaFactura(detalleVenta);

            using (iTextDocument documento = new iTextDocument(iTextSharp.text.PageSize.A4, 40, 40, 40, 40))
            {
                PdfWriter.GetInstance(
                    documento,
                    new FileStream(ruta, FileMode.Create)
                );

                documento.Open();

                // Estilos de fuente
                iTextFont titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                iTextFont normal = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                iTextFont negrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                iTextFont piePaginaFont = FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 9, iTextSharp.text.BaseColor.GRAY);

                // =====================================================
                // AGREGAR LOGO
                // =====================================================
                string rutaLogo = System.IO.Path.Combine(Application.StartupPath, "Imagenes", "logo.png"); // Ajusta la ruta/nombre de tu imagen
                if (File.Exists(rutaLogo))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(rutaLogo);
                    logo.Alignment = iTextElement.ALIGN_CENTER;
                    logo.ScaleToFit(120f, 60f); // Redimensiona la imagen (ancho, alto)
                    documento.Add(logo);
                }

                // Encabezados
                iTextSharp.text.Paragraph encabezado = new iTextSharp.text.Paragraph("PAYLESS SHOESOURCE", titulo)
                {
                    Alignment = iTextElement.ALIGN_CENTER
                };
                documento.Add(encabezado);

                iTextSharp.text.Paragraph factura = new iTextSharp.text.Paragraph("FACTURA DE VENTA\n\n", negrita)
                {
                    Alignment = iTextElement.ALIGN_CENTER
                };
                documento.Add(factura);

                // Datos Generales
                documento.Add(new iTextSharp.text.Paragraph("Código de venta: " + codigoVenta, normal));
                documento.Add(new iTextSharp.text.Paragraph("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"), normal));
                documento.Add(new iTextSharp.text.Paragraph("\n"));

               
                PdfPTable tabla = new PdfPTable(7)
                {
                    WidthPercentage = 100
                };

                // Encabezados
                tabla.AddCell(new PdfPCell(new iTextSharp.text.Phrase("Producto", negrita)));
                tabla.AddCell(new PdfPCell(new iTextSharp.text.Phrase("Marca", negrita)));
                tabla.AddCell(new PdfPCell(new iTextSharp.text.Phrase("Categoría", negrita)));
                tabla.AddCell(new PdfPCell(new iTextSharp.text.Phrase("Talla", negrita)));
                tabla.AddCell(new PdfPCell(new iTextSharp.text.Phrase("Cant.", negrita)));
                tabla.AddCell(new PdfPCell(new iTextSharp.text.Phrase("Precio", negrita)));
                tabla.AddCell(new PdfPCell(new iTextSharp.text.Phrase("Subtotal", negrita)));

                // Recorrido de los datos traídos desde VentaDAO
                foreach (DataRow fila in detalleFactura.Rows)
                {
                    tabla.AddCell(fila["producto"].ToString());
                    tabla.AddCell(fila["marca"].ToString());
                    tabla.AddCell(fila["categoria"].ToString());
                    tabla.AddCell(fila["talla"].ToString());
                    tabla.AddCell(fila["cantidad"].ToString());
                    tabla.AddCell("C$ " + Convert.ToDecimal(fila["precio_venta"]).ToString("N2"));
                    tabla.AddCell("C$ " + Convert.ToDecimal(fila["subtotal"]).ToString("N2"));
                }

                documento.Add(tabla);
                documento.Add(new iTextSharp.text.Paragraph("\n"));

                // Totales
                documento.Add(new iTextSharp.text.Paragraph("Subtotal: C$ " + subtotal.ToString("N2"), normal));
                documento.Add(new iTextSharp.text.Paragraph("IVA (15%): C$ " + iva.ToString("N2"), normal));
                documento.Add(new iTextSharp.text.Paragraph("TOTAL: C$ " + total.ToString("N2"), negrita));
                documento.Add(new iTextSharp.text.Paragraph("\n\n"));

                // =====================================================
                // LEYENDA INFERIOR (PIE DE PÁGINA / DATOS EMPRESA)
                // =====================================================
                iTextSharp.text.Paragraph leyenda = new iTextSharp.text.Paragraph(
                    "Payless ShoeSource Nicaragua\n" +
                    "Dirección: Managua, Nicaragua\n" +
                    "Teléfono: +505 2222-0000\n" +
                    "¡Gracias por su compra!", piePaginaFont)
                {
                    Alignment = iTextElement.ALIGN_CENTER
                };

                documento.Add(leyenda);

                documento.Close();
            }
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
