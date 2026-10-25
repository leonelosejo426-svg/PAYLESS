using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Interfaces_de_Usuario_Propuestas_Payless.Utilidades;
using static Interfaces_de_Usuario_Propuestas_Payless.Ventas;

namespace Interfaces_de_Usuario_Propuestas_Payless.Entidades
{
    public partial class NuevaVenta : Form
    {
        private VentaDAO ventaDAO;
        private ClaseSesion clasesesion  = new ClaseSesion();

        private int idProductoSeleccionado = 0;
        private int idProductoTallaSeleccionado = 0;

        private decimal precioVentaActual = 0;
        private decimal tipoCambioActual = 0;
        private DataTable dtProductosOriginal = null;
        private bool isFiltering = false;

        // LISTA ENLAZADA DE LOS PRODUCTOS DE LA VENTA
        private ListaEnlazadaDetalleVenta listaDetalleVenta =
            new ListaEnlazadaDetalleVenta();
        public NuevaVenta()
        {
            InitializeComponent();
            ventaDAO = new VentaDAO();
        }

        private void NuevaVenta_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = ClaseSesion.UsuarioActual;

            txtCantidad.KeyPress += (s, ev) =>
            {
                // Permitir solo dígitos y control (como backspace)
                if (!char.IsControl(ev.KeyChar) && !char.IsDigit(ev.KeyChar))
                {
                    ev.Handled = true;
                }
            };

            try
            {
                ConfigurarFormulario();

                CargarClientes();
                CargarProductos();
                CargarTipoCambio();
                GenerarCodigoVenta();

                CalcularTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar la nueva venta:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarClientes()
        {
            try
            {
                DataTable clientes = ventaDAO.CargarClientes();

                CBcliente.DataSource = clientes;
                CBcliente.DisplayMember = "nombre";
                CBcliente.ValueMember = "id_cliente";
                CBcliente.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los clientes:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarProductos()
        {
            try
            {
                dtProductosOriginal = ventaDAO.ObtenerProductosConStockYPreciosValidos();


                CBproducto.DataSource = dtProductosOriginal;
                CBproducto.DisplayMember = "nombre";
                CBproducto.ValueMember = "id_producto";
                CBproducto.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los productos: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void MostrarUsuario()
        {
            lblUsuario.Text = ClaseSesion.UsuarioActual;
        }



        private void GenerarCodigoVenta()
        {
            try
            {
                txtCodigoVenta.Text = ventaDAO.GenerarCodigoVenta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al generar el código de venta: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }



        private void ConfigurarFormulario()
        {
            txtCodigoVenta.ReadOnly = true;
            txtPrecioVenta.ReadOnly = true;
            txtStockActual.ReadOnly = true;

            CBcategoria.Enabled = false;
            CBmarca.Enabled = false;

            CBcliente.SelectedIndex = -1;
            CBproducto.SelectedIndex = -1;
            CBTalla.SelectedIndex = -1;

            txtPrecioVenta.Clear();
            txtStockActual.Clear();

            txtCantidad.Text = "1";

            // LIMPIAR LISTA ENLAZADA
            listaDetalleVenta.Limpiar();

            dgvDetalleVenta.Rows.Clear();

            dgvDetalleVenta.AllowUserToAddRows = false;
            dgvDetalleVenta.AutoGenerateColumns = false;

            lblSubtotal.Text = "C$ 0.00";
            lblIVA.Text = "C$ 0.00";
            lblTotal.Text = "C$ 0.00";
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void CBproducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBproducto.SelectedIndex == -1 || CBproducto.SelectedValue == null)
                return;

            try
            {
                if (CBproducto.SelectedValue is DataRowView)
                    return;

                idProductoSeleccionado = Convert.ToInt32(CBproducto.SelectedValue);

                CargarInformacionProducto();

                // Limpiamos precio y stock hasta que el usuario elija la talla
                txtPrecioVenta.Clear();
                precioVentaActual = 0;
                txtStockActual.Clear();

                CargarTallas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al seleccionar el producto:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarInformacionProducto()
        {
            DataTable producto = ventaDAO.ObtenerProducto(idProductoSeleccionado);

            if (producto.Rows.Count == 0)
                return;

            DataRow fila = producto.Rows[0];

            CBcategoria.Text = fila["nombre_categoria"].ToString();
            CBmarca.Text = fila["nombre_marca"].ToString();
        }



        private void CargarTallas()
        {
            try
            {
                CBTalla.DataSource = null;

                DataTable tallas =
                    ventaDAO.CargarTallas(idProductoSeleccionado);

                if (tallas.Rows.Count == 0)
                {
                    CBTalla.Enabled = false;

                    MessageBox.Show(
                        "Este producto no tiene tallas disponibles.",
                        "Sin stock",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                CBTalla.Enabled = true;

                CBTalla.DataSource = tallas;
                CBTalla.DisplayMember = "talla";
                CBTalla.ValueMember = "id_producto_talla";
                CBTalla.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las tallas:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarTipoCambio()
        {
            try
            {
                tipoCambioActual = ventaDAO.ObtenerTipoCambioActual();

                if (tipoCambioActual <= 0)
                {
                    tipoCambioActual = 0;
                }
            }
            catch
            {
                tipoCambioActual = 0;
            }
        }


        private void cmbTalla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBTalla.SelectedIndex == -1 || CBTalla.SelectedValue == null)
            {
                txtStockActual.Clear();
                txtPrecioVenta.Clear();
                precioVentaActual = 0;
                return;
            }

            try
            {
                if (CBTalla.SelectedValue is DataRowView)
                    return;

                idProductoTallaSeleccionado = Convert.ToInt32(CBTalla.SelectedValue);

                CargarStock();
                CargarPrecio(); // Muestra el precio una vez elegida la talla
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al seleccionar la talla:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarPrecio()
        {
            if (idProductoTallaSeleccionado <= 0)
                return;

            decimal? precio = ventaDAO.ObtenerPrecioProductoTalla(idProductoTallaSeleccionado);

            if (precio == null || precio.Value <= 0)
            {
                precioVentaActual = 0;
                txtPrecioVenta.Clear();
                return;
            }

            precioVentaActual = precio.Value;
            txtPrecioVenta.Text = precioVentaActual.ToString("N2");
        }

        private void CargarStock()
        {
            try
            {
                // 1. Obtener el stock real de la base de datos
                int stockBD = ventaDAO.ObtenerStockProductoTalla(idProductoTallaSeleccionado);

                // 2. Calcular cuánto de este producto ya tenemos agregado en el carrito actual (grilla)
                int cantidadEnCarrito = 0;
                foreach (DataGridViewRow row in dgvDetalleVenta.Rows)
                {
                    if (row.Cells[0].Value != null && Convert.ToInt32(row.Cells[0].Value) == idProductoTallaSeleccionado)
                    {
                        // Asumiendo que la celda 6 es la cantidad en tu DataGridView
                        cantidadEnCarrito += Convert.ToInt32(row.Cells[6].Value);
                    }
                }

                // 3. El stock disponible real para esta venta es el de la BD menos lo que ya apartamos en pantalla
                int stockDisponible = stockBD - cantidadEnCarrito;
                txtStockActual.Text = stockDisponible.ToString();

                if (stockDisponible <= 0)
                {
                    MessageBox.Show(
                        "No hay stock disponible para esta talla.",
                        "Sin stock",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al obtener el stock:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarProducto())
                return;

            AgregarDetalle();
            CalcularTotales();
            LimpiarCampos();
        }

        // ==========================================================
        // VALIDAR PRODUCTO
        // ==========================================================
        private bool ValidarProducto()
        {
            if (CBproducto.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione un producto.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            if (CBTalla.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione una talla.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            if (precioVentaActual <= 0)
            {
                MessageBox.Show(
                    "El producto no tiene un precio de venta válido.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show(
                    "Ingrese una cantidad válida mayor que cero.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }

            if (!int.TryParse(txtStockActual.Text, out int stock))
            {
                MessageBox.Show(
                    "No se pudo obtener el stock disponible.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            if (stock <= 0)
            {
                MessageBox.Show(
                    "No hay stock disponible para esta talla.",
                    "Sin stock",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            // =========================================================================
            // NUEVA VALIDACIÓN DE STOCK MÍNIMO CON ALERTA OK/CANCEL
            // =========================================================================

            
            int stockMinimoBD = 5; 

            // Calculamos cuánto es lo máximo que se puede vender respetando el stock mínimo
            int cantidadMaximaPermitida = stock - stockMinimoBD;

            // Si la cantidad que quiere vender supera lo permitido por el stock mínimo:
            if (cantidad > cantidadMaximaPermitida)
            {
                if (cantidadMaximaPermitida <= 0)
                {
                    MessageBox.Show(
                        "No se puede realizar la venta. El producto se encuentra en su límite de stock mínimo.",
                        "Stock Insuficiente",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return false;
                }

                // Mostramos la alerta con OK / Cancel
                DialogResult respuesta = MessageBox.Show(
                    $"¡Alerta de Stock Mínimo!\n\n" +
                    $"Si vende {cantidad} unidades, se quedará sin stock o por debajo del mínimo permitido ({stockMinimoBD}).\n\n" +
                    $"La cantidad máxima que puede vender respetando el stock mínimo es de: {cantidadMaximaPermitida} unidades.\n\n" +
                    $"¿Desea agregar únicamente los {cantidadMaximaPermitida} productos permitidos?",
                    "Advertencia de Stock Mínimo",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question
                );

                if (respuesta == DialogResult.OK)
                {
                    // Forzamos el TextBox de cantidad a que tome solo lo permitido automáticamente
                    txtCantidad.Text = cantidadMaximaPermitida.ToString();
                    return true; // Permite continuar y agregar los productos ajustados
                }
                else
                {
                    // Si le da en Cancelar, frena el proceso
                    return false;
                }
            }
            // =========================================================================

            if (cantidad > stock)
            {
                MessageBox.Show(
                    "La cantidad solicitada supera el stock disponible.\n\n" +
                    "Stock disponible: " + stock,
                    "Stock insuficiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            return true;
        }

        private void AgregarDetalle()
        {
            int cantidadIngresada = Convert.ToInt32(txtCantidad.Text);
            bool productoExistente = false;

            // 1. Recorrer la grilla para verificar si la combinación de producto y talla ya fue agregada
            foreach (DataGridViewRow row in dgvDetalleVenta.Rows)
            {
                if (row.Cells[0].Value != null && Convert.ToInt32(row.Cells[0].Value) == idProductoTallaSeleccionado)
                {
                    int cantidadActualEnGrilla = Convert.ToInt32(row.Cells[6].Value);
                    int nuevaCantidadTotal = cantidadActualEnGrilla + cantidadIngresada;

                    // Validar que la nueva cantidad acumulada no exceda el stock real en la base de datos
                    int stockRealBD = ventaDAO.ObtenerStockProductoTalla(idProductoTallaSeleccionado);
                    if (nuevaCantidadTotal > stockRealBD)
                    {
                        MessageBox.Show(
                            "La cantidad total acumulada supera el stock disponible.\n\n" +
                            "Stock disponible en inventario: " + stockRealBD,
                            "Stock insuficiente",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    decimal nuevoSubtotal = precioVentaActual * nuevaCantidadTotal;

                    // Actualizar la fila existente en el DataGridView
                    row.Cells[6].Value = nuevaCantidadTotal;
                    row.Cells[7].Value = nuevoSubtotal;

                    productoExistente = true;
                    break;
                }
            }

            // 2. Si no existe previamente en la grilla, agregarlo como una fila nueva
            if (!productoExistente)
            {
                decimal subtotal = CalcularSubtotalLinea(precioVentaActual, cantidadIngresada);

                NodoDetalleVenta nuevoNodo = new NodoDetalleVenta(
                    idProductoTallaSeleccionado,
                    CBproducto.Text,
                    CBcategoria.Text,
                    CBmarca.Text,
                    CBTalla.Text,
                    precioVentaActual,
                    cantidadIngresada,
                    subtotal
                );

                listaDetalleVenta.Agregar(nuevoNodo);

                dgvDetalleVenta.Rows.Add(
                    nuevoNodo.IdProductoTalla,
                    nuevoNodo.Producto,
                    nuevoNodo.Categoria,
                    nuevoNodo.Marca,
                    nuevoNodo.Talla,
                    nuevoNodo.PrecioVenta,
                    nuevoNodo.Cantidad,
                    nuevoNodo.Subtotal
                );
            }
            else
            {
                // Sincronizar los cambios acumulados con la lista enlazada interna
                SincronizarListaEnlazadaDesdeGrid();
            }

            // 3. Reflejar la reducción de stock de inmediato en el campo de texto visual
            ActualizarStockPantallaEnTiempoReal(cantidadIngresada);
        }



        private void ActualizarStockPantallaEnTiempoReal(int cantidadAgregada)
        {
            if (int.TryParse(txtStockActual.Text, out int stockActual))
            {
                int stockRestante = stockActual - cantidadAgregada;
                txtStockActual.Text = stockRestante.ToString();
            }
        }

        private void SincronizarListaEnlazadaDesdeGrid()
        {
            listaDetalleVenta.Limpiar();
            foreach (DataGridViewRow row in dgvDetalleVenta.Rows)
            {
                if (row.Cells[0].Value == null) continue;

                NodoDetalleVenta nodo = new NodoDetalleVenta(
                    Convert.ToInt32(row.Cells[0].Value),
                    row.Cells[1].Value.ToString(),
                    row.Cells[2].Value.ToString(),
                    row.Cells[3].Value.ToString(),
                    row.Cells[4].Value.ToString(),
                    Convert.ToDecimal(row.Cells[5].Value),
                    Convert.ToInt32(row.Cells[6].Value),
                    Convert.ToDecimal(row.Cells[7].Value)
                );
                listaDetalleVenta.Agregar(nodo);
            }
        }

        private decimal CalcularSubtotalLinea(
           decimal precio,
           int cantidad)
        {
            return precio * cantidad;
        }

        private void CalcularTotales()
        {
            decimal subtotal = listaDetalleVenta.ObtenerSubtotal();

            decimal iva = subtotal * 0.15m;

            decimal total = subtotal + iva;

            lblSubtotal.Text =
                "C$ " + subtotal.ToString("N2");

            lblIVA.Text =
                "C$ " + iva.ToString("N2");

            lblTotal.Text =
                "C$ " + total.ToString("N2");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarUltimoDetalle();
            CalcularTotales();
        }

        private void EliminarUltimoDetalle()
        {
            if (listaDetalleVenta.EstaVacia())
            {
                MessageBox.Show(
                    "No hay productos para eliminar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            // ELIMINAR DE LA LISTA ENLAZADA
            listaDetalleVenta.EliminarUltimo();

            // ELIMINAR DEL DATAGRIDVIEW
            if (dgvDetalleVenta.Rows.Count > 0)
            {
                dgvDetalleVenta.Rows.RemoveAt(
                    dgvDetalleVenta.Rows.Count - 1
                );
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            listaDetalleVenta.Limpiar();

            dgvDetalleVenta.Rows.Clear();

            LimpiarCampos();

            CalcularTotales();

        }

        private void LimpiarCampos()
        {
            idProductoSeleccionado = 0;
            idProductoTallaSeleccionado = 0;
            precioVentaActual = 0;

            CBproducto.SelectedIndex = -1;

            CBcategoria.Text = "";
            CBmarca.Text = "";

            CBTalla.DataSource = null;
            CBTalla.Enabled = false;

            txtPrecioVenta.Clear();
            txtStockActual.Clear();

            txtCantidad.Text = "1";
        }

        private decimal ObtenerSubtotal()
        {
            return listaDetalleVenta.ObtenerSubtotal();
        }

        private int ObtenerIdCliente()
        {
            if (CBcliente.SelectedIndex == -1)
                return 0;

            if (CBcliente.SelectedValue is DataRowView)
                return 0;

            return Convert.ToInt32(
                CBcliente.SelectedValue
            );
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
        if (CBcliente.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione un cliente.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (listaDetalleVenta.EstaVacia())
            {
                MessageBox.Show(
                    "Debe agregar al menos un producto a la venta.",
                    "Venta vacía",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            decimal subtotal =
                listaDetalleVenta.ObtenerSubtotal();

            decimal iva =
                subtotal * 0.15m;

            decimal total =
                subtotal + iva;

            DataTable detalleVenta =
                CrearDetalleVenta();

            int idCliente =
                ObtenerIdCliente();

            int idCaja =
                ventaDAO.ObtenerIdCajaAbierta();

            if (idCaja <= 0)
            {
                MessageBox.Show(
                    "No existe una caja abierta.",
                    "Caja",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // USUARIO QUE INICIÓ SESIÓN
            int idUsuario = ClaseSesion.IdUsuario;

            if (idUsuario <= 0)
            {
                MessageBox.Show(
                    "No se pudo identificar al usuario actual.",
                    "Usuario",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            SubFormaPagoV formaPago =
                new SubFormaPagoV(
                    txtCodigoVenta.Text,
                    idCliente,
                    idUsuario,
                    idCaja,
                    subtotal,
                    iva,
                    total,
                    detalleVenta
                );

            DialogResult resultado =
                formaPago.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                listaDetalleVenta.Limpiar();

                dgvDetalleVenta.Rows.Clear();

                Close();
            }
        }

        private DataTable CrearDetalleVenta()
        {
            return listaDetalleVenta.CrearDetalleVenta();
        }
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void CBproducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Evita el sonido de alerta

                if (CBproducto.SelectedIndex != -1 || CBproducto.SelectedValue != null)
                {
                    // Oculta el desplegable y pasa el foco a la talla o cantidad
                    CBproducto.DroppedDown = false;
                    CBTalla.Focus(); // O la siguiente caja que uses
                }
            }
        }

        private void dgvDetalleVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDetalleVenta_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validar que no sea la cabecera y que sea una fila válida
            if (e.RowIndex < 0 || e.RowIndex >= dgvDetalleVenta.Rows.Count)
                return;

            DataGridViewRow row = dgvDetalleVenta.Rows[e.RowIndex];

            if (row.Cells[0].Value == null)
                return;

            try
            {
                // 1. Obtener los valores de la fila seleccionada
                int idProductoTalla = Convert.ToInt32(row.Cells[0].Value);
                string nombreProducto = row.Cells[1].Value.ToString();
                string talla = row.Cells[4].Value.ToString();
                int cantidadActual = Convert.ToInt32(row.Cells[6].Value);

                // 2. Seleccionar el producto en el ComboBox buscando por texto o valor
                CBproducto.Text = nombreProducto;

                // Esto disparará el evento SelectedIndexChanged del producto y cargará sus tallas automáticamente
                idProductoSeleccionado = Convert.ToInt32(CBproducto.SelectedValue);
                CargarInformacionProducto();
                CargarTallas();

                // 3. Seleccionar la talla correspondiente en el ComboBox de tallas
                CBTalla.Text = talla;
                idProductoTallaSeleccionado = idProductoTalla;

                // 4. Cargar precio y stock actual en pantalla
                CargarPrecio();
                CargarStock();

                // IMPORTANTE: Devolvemos la cantidad que tenía en la línea al stock visual temporalmente 
                // para que no afecte la validación al sobreescribir o sumar.
                if (int.TryParse(txtStockActual.Text, out int stockActual))
                {
                    txtStockActual.Text = (stockActual + cantidadActual).ToString();
                }

                // 5. Cargar la cantidad en el TextBox para editarla
                txtCantidad.Text = cantidadActual.ToString();

                // 6. Eliminar temporalmente la fila de la grilla (para que al darle "Agregar" de nuevo se actualice limpia)
                dgvDetalleVenta.Rows.RemoveAt(e.RowIndex);

                // 7. Sincronizar la lista enlazada interna y recalcular totales
                SincronizarListaEnlazadaDesdeGrid();
                CalcularTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar el producto para editar:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void dgvDetalleVenta_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Asegúrate de cambiar "colPrecioVenta" y "colSubtotal" por los nombres reales de tus columnas en el DataGridView
            if (dgvDetalleVenta.Columns[e.ColumnIndex].Name == "colPrecioVenta" ||
                dgvDetalleVenta
                .Columns[e.ColumnIndex].Name == "colSubtotal")
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    if (decimal.TryParse(e.Value.ToString(), out decimal valor))
                    {
                        // Formato con comas para los miles y puntos para los decimales (ej. 2,000.50)
                        // Si el número es entero (ej. 2000), se mostrará como 2,000 sin decimales sobrantes.
                        // Si tiene decimales, respetará los que tenga (ej. 2,000.75).
                        e.Value = valor.ToString("#,##0.##");
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        private void CBproducto_TextUpdate(object sender, EventArgs e)
        {
            if (isFiltering || dtProductosOriginal == null) return;

            isFiltering = true;

            string textoBusqueda = CBproducto.Text;
            int cursorPos = CBproducto.SelectionStart; // Guardamos dónde está el cursor

            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                CBproducto.DataSource = dtProductosOriginal;
            }
            else
            {
                DataView dv = new DataView(dtProductosOriginal);
                // Filtra coincidencias parciales en cualquier parte del nombre
                dv.RowFilter = $"nombre LIKE '%{textoBusqueda}%'";
                CBproducto.DataSource = dv;
            }

            // Restauramos el texto y la posición del cursor para que te deje borrar sin trabarse
            // Restauramos el texto y la posición del cursor para que te deje borrar sin trabarse
            CBproducto.Text = textoBusqueda;
            CBproducto.SelectionStart = cursorPos;
            CBproducto.SelectionLength = 0;

            // Corrección aquí: Se eliminó el texto extra y la flecha, dejando un IF limpio
            if (CBproducto.Items.Count > 0)
            {
                CBproducto.DroppedDown = true;
            }

            isFiltering = false;
        }
    }
}
