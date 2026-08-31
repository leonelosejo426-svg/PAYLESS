using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Interfaces_de_Usuario_Propuestas_Payless.Ventas;

namespace Interfaces_de_Usuario_Propuestas_Payless.Formularios
{
    public partial class NuevaVenta : Form
    {


        private VentaDAO ventaDAO = new VentaDAO();

      

        private int idClienteSeleccionado = 0;
        private decimal tipoCambio = 36.5m;

        public NuevaVenta()
        {
            InitializeComponent();
        }

        private void NuevaVenta_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();

            CargarClientes();
            CargarProductos();

            GenerarCodigoVenta();

            CargarTipoCambio();
            CargarUsuario();

            LimpiarCamposProducto();
        }
        private void ConfigurarDataGridView()
        {
            dgvVenta.AllowUserToAddRows = false;
            dgvVenta.ReadOnly = true;
            dgvVenta.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvVenta.MultiSelect = false;

            dgvVenta.AutoGenerateColumns = false;

            dgvVenta.Columns.Clear();

            // ID PRODUCTO TALLA
            DataGridViewTextBoxColumn colId =
                new DataGridViewTextBoxColumn();

            colId.Name = "id_producto_talla";
            colId.HeaderText = "ID";
            colId.DataPropertyName = "id_producto_talla";
            colId.Visible = false;

            dgvVenta.Columns.Add(colId);

            // PRODUCTO
            DataGridViewTextBoxColumn colProducto =
                new DataGridViewTextBoxColumn();

            colProducto.Name = "producto";
            colProducto.HeaderText = "Producto";
            colProducto.DataPropertyName = "producto";

            dgvVenta.Columns.Add(colProducto);

            // CATEGORIA
            DataGridViewTextBoxColumn colCategoria =
                new DataGridViewTextBoxColumn();

            colCategoria.Name = "categoria";
            colCategoria.HeaderText = "Categoría";
            colCategoria.DataPropertyName = "categoria";

            dgvVenta.Columns.Add(colCategoria);

            // TALLA
            DataGridViewTextBoxColumn colTalla =
                new DataGridViewTextBoxColumn();

            colTalla.Name = "talla";
            colTalla.HeaderText = "Talla";
            colTalla.DataPropertyName = "talla";

            dgvVenta.Columns.Add(colTalla);

            // MARCA
            DataGridViewTextBoxColumn colMarca =
                new DataGridViewTextBoxColumn();

            colMarca.Name = "marca";
            colMarca.HeaderText = "Marca";
            colMarca.DataPropertyName = "marca";

            dgvVenta.Columns.Add(colMarca);

            // PRECIO
            DataGridViewTextBoxColumn colPrecio =
                new DataGridViewTextBoxColumn();

            colPrecio.Name = "precio_venta";
            colPrecio.HeaderText = "Precio V.";
            colPrecio.DataPropertyName = "precio_venta";

            dgvVenta.Columns.Add(colPrecio);

            // STOCK
            DataGridViewTextBoxColumn colStock =
                new DataGridViewTextBoxColumn();

            colStock.Name = "stock_actual";
            colStock.HeaderText = "Stock A.";
            colStock.DataPropertyName = "stock_actual";

            dgvVenta.Columns.Add(colStock);

            // CANTIDAD
            DataGridViewTextBoxColumn colCantidad =
                new DataGridViewTextBoxColumn();

            colCantidad.Name = "cantidad";
            colCantidad.HeaderText = "Cantidad";
            colCantidad.DataPropertyName = "cantidad";

            dgvVenta.Columns.Add(colCantidad);

            // SUBTOTAL
            DataGridViewTextBoxColumn colSubtotal =
                new DataGridViewTextBoxColumn();

            colSubtotal.Name = "subtotal";
            colSubtotal.HeaderText = "Subtotal";
            colSubtotal.DataPropertyName = "subtotal";

            dgvVenta.Columns.Add(colSubtotal);
        }
        private void ConfigurarFormulario()
        {
            txtCodigoVenta.ReadOnly = true;

            txtPrecioVenta.ReadOnly = true;
            txtStockActual.ReadOnly = true;
            txtTipoCambio.ReadOnly = true;

            cmbCategoria.Enabled = false;
            cmbMarca.Enabled = false;
            cmbTalla.Enabled = false;

            nudCantidad.Minimum = 1;
            nudCantidad.Value = 1;

            txtPrecioVenta.Text = "0.00";
            txtStockActual.Text = "0";
            txtTipoCambio.Text = "0.00";

            lblSubtotal.Text = "C$ 0.00";
            lblIVA.Text = "C$ 0.00";
            lblTotal.Text = "C$ 0.00";
        }
        private void GenerarCodigoVenta()
        {
            txtCodigoVenta.Text =
                ventaDAO.GenerarCodigoVenta();
        }
        private void CargarClientes()
        {
            DataTable clientes =
                ventaDAO.CargarClientes();

            cmbCliente.DataSource = clientes;
            cmbCliente.DisplayMember = "nombre";
            cmbCliente.ValueMember = "id_cliente";

            cmbCliente.SelectedIndex = -1;
        }
        private void CargarProductos()
        {
            DataTable productos =
                ventaDAO.CargarProductos();

            cmbProducto.DataSource = productos;
            cmbProducto.DisplayMember = "nombre";
            cmbProducto.ValueMember = "id_producto";

            cmbProducto.SelectedIndex = -1;
        }
        private void CargarCategorias()
        {
            DataTable categorias =
                ventaDAO.CargarCategorias();

            cmbCategoria.DataSource = categorias;
            cmbCategoria.DisplayMember =
                "nombre_categoria";
            cmbCategoria.ValueMember =
                "id_categoria";

            cmbCategoria.SelectedIndex = -1;
        }
        private void CargarMarcas()
        {
            DataTable marcas =
                ventaDAO.CargarMarcas();

            cmbMarca.DataSource = marcas;
            cmbMarca.DisplayMember =
                "nombre_marca";
            cmbMarca.ValueMember =
                "id_marca";

            cmbMarca.SelectedIndex = -1;
        }
        private void CargarTipoCambio()
        {
            tipoCambioActual =
                ventaDAO.ObtenerTipoCambio();

            txtTipoCambio.Text =
                tipoCambioActual.ToString("N2");
        }
        private void cmbProducto_SelectedIndexChanged(object sender,EventArgs e)
        {
            if (cmbProducto.SelectedIndex == -1)
                return;

            if (cmbProducto.SelectedValue == null)
                return;

            if (cmbProducto.SelectedValue is DataRowView)
                return;

            if (!int.TryParse(
                cmbProducto.SelectedValue.ToString(),
                out int idProducto))
            {
                return;
            }

            idProductoSeleccionado = idProducto;

            DataTable datos =
                ventaDAO.ObtenerProducto(
                    idProducto);

            if (datos.Rows.Count == 0)
                return;

            DataRow fila = datos.Rows[0];

            // Precio
            if (fila["precio_venta"] != DBNull.Value)
            {
                precioVentaActual =
                    Convert.ToDecimal(
                        fila["precio_venta"]);

                txtPrecioVenta.Text =
                    precioVentaActual.ToString("N2");
            }
            else
            {
                precioVentaActual = 0;
                txtPrecioVenta.Text = "0.00";
            }

            // Categoría
            cmbCategoria.SelectedValue =
                Convert.ToInt32(
                    fila["id_categoria"]);

            // Marca
            cmbMarca.SelectedValue =
                Convert.ToInt32(
                    fila["id_marca"]);

            // Cargar tallas
            CargarTallas(idProducto);

            txtStockActual.Text = "0";
        }

        private void CargarClientes()
        {
            DataTable clientes =
                ventaDAO.CargarClientes();

            CBcliente.DataSource = clientes;

            CBcliente.DisplayMember = "nombre";
            CBcliente.ValueMember = "id_cliente";

            CBcliente.SelectedIndex = -1;
        }

        private void CargarTallas(int idProducto)
        {
            cmbTalla.DataSource = null;
            cmbTalla.Items.Clear();

            idProductoTallaSeleccionado = 0;

            DataTable tallas =
                ventaDAO.CargarTallas(idProducto);

            if (tallas.Rows.Count == 0)
            {
                cmbTalla.Enabled = false;

                MessageBox.Show(
                    "Este producto no tiene tallas registradas.",
                    "Tallas",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            cmbTalla.Enabled = true;

            cmbTalla.DataSource = tallas;
            cmbTalla.DisplayMember = "talla";
            cmbTalla.ValueMember =
                "id_producto_talla";

            cmbTalla.SelectedIndex = -1;
        }
        private void cmbTalla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTalla.SelectedIndex == -1)
                return;

            if (cmbTalla.SelectedValue == null)
                return;

            if (cmbTalla.SelectedValue is DataRowView)
                return;

            if (!int.TryParse(
                cmbTalla.SelectedValue.ToString(),
                out int idProductoTalla))
            {
                return;
            }

            idProductoTallaSeleccionado =
                idProductoTalla;

            int stock =
                ventaDAO.ObtenerStock(
                    idProductoTalla);

            txtStockActual.Text =
                stock.ToString();

            nudCantidad.Maximum =
                stock > 0 ? stock : 1;

            nudCantidad.Value = 1;
        }

        private void cmbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBcliente.SelectedIndex == -1)
            {
                idClienteSeleccionado = 0;
                return;
            }

            if (CBcliente.SelectedValue == null)
                return;

            if (CBcliente.SelectedValue is DataRowView)
                return;

            if (int.TryParse(
                CBcliente.SelectedValue.ToString(),
                out int idCliente))
            {
                idClienteSeleccionado = idCliente;
            }
        }

        private void CargarProductos()
        {
            DataTable productos =
                ventaDAO.CargarProductos();

            CBproducto.DataSource = productos;

            CBproducto.DisplayMember = "nombre";
            CBproducto.ValueMember = "id_producto";

            CBproducto.SelectedIndex = -1;
        }

        private void cmbProducto_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (CBproducto.SelectedIndex == -1)
                return;

            if (CBproducto.SelectedValue == null)
                return;

            if (CBproducto.SelectedValue is DataRowView)
                return;

            if (!int.TryParse(
                CBproducto.SelectedValue.ToString(),
                out int idProducto))
            {
                return;
            }

            CargarDatosProducto(idProducto);
            CargarTallas(idProducto);
        }
        private void CargarDatosProducto(int idProducto)
        {
            ClaseProducto producto =
                ventaDAO.ObtenerProducto(idProducto);

            if (producto == null)
                return;

            txtCategoria.Text =
                producto.NombreCategoria;

            txtMarca.Text =
                producto.NombreMarca;

            if (producto.PrecioVenta > 0)
            {
                txtPrecioVenta.Text =
                    producto.PrecioVenta.ToString("N2");
            }
            else
            {
                txtPrecioVenta.Clear();
            }
        }
        private void CargarTallas(int idProducto)
        {
            CBTalla.DataSource = null;
            CBTalla.Items.Clear();

            DataTable tallas =
                ventaDAO.CargarTallas(idProducto);

            if (tallas.Rows.Count == 0)
            {
                CBTalla.SelectedIndex = -1;
                txtStockActual.Clear();

                return;
            }

            CBTalla.DataSource = tallas;

            CBTalla.DisplayMember = "talla";
            CBTalla.ValueMember = "id_producto_talla";

            CBTalla.SelectedIndex = -1;
        }

        private void cmbTalla_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (CBTalla.SelectedIndex == -1)
                return;

            if (CBTalla.SelectedValue == null)
                return;

            if (CBTalla.SelectedValue is DataRowView)
                return;

            if (!int.TryParse(
                CBTalla.SelectedValue.ToString(),
                out int idProductoTalla))
            {
                return;
            }

            int stock =
                ventaDAO.ObtenerStockProductoTalla(
                    idProductoTalla);

            txtStockActual.Text =
                stock.ToString();
        }
        private void GenerarCodigoVenta()
        {
            string codigo =
                ventaDAO.GenerarCodigoVenta();

            txtCodigoVenta.Text = codigo;
        }
        private void CargarTipoCambio()
        {
            decimal cambio =
                ventaDAO.ObtenerTipoCambioActual();

            if (cambio > 0)
            {
                tipoCambio = cambio;
            }

            lblTipoCambio.Text =
                "Tipo de cambio: C$ " +
                tipoCambio.ToString("N2");
        }

        private void CargarUsuario()
        {
            if (ClaseSesion.UsuarioActual != null)
            {
                lblUsuario.Text =
                    "Usuario: " +
                    ClaseSesion.UsuarioActual;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (CBcliente.SelectedIndex == -1 ||
               idClienteSeleccionado <= 0)
            {
                MessageBox.Show(
                    "Debe seleccionar un cliente.",
                    "Venta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                CBcliente.Focus();
                return;
            }

            // ------------------------------------------------------
            // PRODUCTO
            // ------------------------------------------------------

            if (CBproducto.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Debe seleccionar un producto.",
                    "Venta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                CBproducto.Focus();
                return;
            }

            // ------------------------------------------------------
            // TALLA
            // ------------------------------------------------------

            if (CBTalla.SelectedIndex == -1 ||
                CBTalla.SelectedValue == null)
            {
                MessageBox.Show(
                    "Debe seleccionar una talla.",
                    "Venta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                CBTalla.Focus();
                return;
            }

            if (!int.TryParse(
                CBTalla.SelectedValue.ToString(),
                out int idProductoTalla))
            {
                MessageBox.Show(
                    "La talla seleccionada no es válida.",
                    "Venta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // ------------------------------------------------------
            // PRECIO
            // ------------------------------------------------------

            if (!decimal.TryParse(
                txtPrecioVenta.Text.Trim(),
                out decimal precio))
            {
                MessageBox.Show(
                    "El precio de venta no es válido.",
                    "Venta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (precio <= 0)
            {
                MessageBox.Show(
                    "El producto no tiene un precio de venta válido.",
                    "Precio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // ------------------------------------------------------
            // STOCK
            // ------------------------------------------------------

            if (!int.TryParse(
                txtStockActual.Text.Trim(),
                out int stock))
            {
                MessageBox.Show(
                    "No se pudo obtener el stock.",
                    "Venta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // ------------------------------------------------------
            // CANTIDAD
            // ------------------------------------------------------

            if (!int.TryParse(
                txtCantidad.Text.Trim(),
                out int cantidad))
            {
                MessageBox.Show(
                    "La cantidad debe ser un número entero.",
                    "Cantidad",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCantidad.Focus();
                return;
            }

            if (cantidad <= 0)
            {
                MessageBox.Show(
                    "La cantidad debe ser mayor que cero.",
                    "Cantidad",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCantidad.Focus();
                return;
            }

            if (cantidad > stock)
            {
                MessageBox.Show(
                    "La cantidad solicitada supera el stock disponible.",
                    "Stock insuficiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCantidad.Focus();
                return;
            }

            // ------------------------------------------------------
            // EVITAR REPETIR MISMA TALLA
            // ------------------------------------------------------

            foreach (DataGridViewRow fila in dgvVenta.Rows)
            {
                if (fila.Cells["id_producto_talla"].Value == null)
                    continue;

                int idExistente =
                    Convert.ToInt32(
                        fila.Cells["id_producto_talla"].Value);

                if (idExistente == idProductoTalla)
                {
                    MessageBox.Show(
                        "Esta talla ya fue agregada a la venta.",
                        "Producto repetido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }
            }

            // ------------------------------------------------------
            // SUBTOTAL
            // ------------------------------------------------------

            decimal subtotal =
                precio * cantidad;

            // ------------------------------------------------------
            // OBTENER DATOS
            // ------------------------------------------------------

            DataTable datos =
                ventaDAO.ObtenerProductoParaVenta(
                    idProductoTalla);

            if (datos.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No se encontraron los datos del producto.",
                    "Venta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DataRow filaProducto =
                datos.Rows[0];

            // ------------------------------------------------------
            // AGREGAR A LA PILA TEMPORAL
            // ------------------------------------------------------

            int filaNueva =
                dgvVenta.Rows.Add();

            dgvVenta.Rows[filaNueva]
                .Cells["id_producto_talla"]
                .Value = idProductoTalla;

            dgvVenta.Rows[filaNueva]
                .Cells["producto"]
                .Value =
                filaProducto["producto"].ToString();

            dgvVenta.Rows[filaNueva]
                .Cells["categoria"]
                .Value =
                filaProducto["categoria"].ToString();

            dgvVenta.Rows[filaNueva]
                .Cells["talla"]
                .Value =
                filaProducto["talla"].ToString();

            dgvVenta.Rows[filaNueva]
                .Cells["marca"]
                .Value =
                filaProducto["marca"].ToString();

            dgvVenta.Rows[filaNueva]
                .Cells["precio_venta"]
                .Value =
                precio.ToString("N2");

            dgvVenta.Rows[filaNueva]
                .Cells["stock_actual"]
                .Value =
                stock;

            dgvVenta.Rows[filaNueva]
                .Cells["cantidad"]
                .Value =
                cantidad;

            dgvVenta.Rows[filaNueva]
                .Cells["subtotal"]
                .Value =
                subtotal.ToString("N2");

            // ------------------------------------------------------
            // ACTUALIZAR RESUMEN
            // ------------------------------------------------------

            CalcularTotales();

            LimpiarCamposProducto();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvVenta.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No hay productos para eliminar.",
                    "Venta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            // La última fila es la que entró de último.
            int ultimaFila =
                dgvVenta.Rows.Count - 1;

            dgvVenta.Rows.RemoveAt(ultimaFila);

            // Recalcular
            CalcularTotales();
        }
        private void CalcularTotales()
        {
            decimal subtotal = 0;

            foreach (DataGridViewRow fila
                     in dgvVenta.Rows)
            {
                if (fila.Cells["subtotal"].Value == null)
                    continue;

                decimal valor;

                if (decimal.TryParse(
                    fila.Cells["subtotal"].Value.ToString(),
                    out valor))
                {
                    subtotal += valor;
                }
            }

            decimal iva =
                subtotal * 0.15m;

            decimal total =
                subtotal + iva;

            lblSubtotal.Text =
                subtotal.ToString("N2");

            lblIVA.Text =
                iva.ToString("N2");

            lblTotal.Text =
                total.ToString("N2");
        }
        private void LimpiarCamposProducto()
        {
            CBproducto.SelectedIndex = -1;

            CBTalla.DataSource = null;
            CBTalla.Items.Clear();
            CBTalla.Text = "";

            txtCategoria.Clear();
            txtMarca.Clear();
            txtPrecioVenta.Clear();
            txtStockActual.Clear();
            txtCantidad.Clear();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (dgvVenta.Rows.Count > 0)
            {
                DialogResult respuesta =
                    MessageBox.Show(
                        "¿Desea limpiar todos los productos de la venta?",
                        "Nueva venta",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                    return;
            }

            dgvVenta.Rows.Clear();

            CBcliente.SelectedIndex = -1;

            idClienteSeleccionado = 0;

            GenerarCodigoVenta();

            CalcularTotales();

            LimpiarCamposProducto();
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (idClienteSeleccionado <= 0)
            {
                MessageBox.Show(
                    "Debe seleccionar un cliente.",
                    "Pago",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (dgvVenta.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Debe agregar al menos un producto.",
                    "Pago",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            decimal subtotal = 0;

            foreach (DataGridViewRow fila
                     in dgvVenta.Rows)
            {
                if (fila.Cells["subtotal"].Value == null)
                    continue;

                subtotal +=
                    Convert.ToDecimal(
                        fila.Cells["subtotal"].Value);
            }

            decimal iva =
                subtotal * 0.15m;

            decimal total =
                subtotal + iva;

            // ------------------------------------------------------
            // ABRIR FORMA DE PAGO
            // ------------------------------------------------------

            FormaPago formaPago =
                new FormaPago(
                    txtCodigoVenta.Text,
                    idClienteSeleccionado,
                    subtotal,
                    iva,
                    total,
                    tipoCambio,
                    dgvVenta);

            FormaPagoV.ShowDialog();

            // Si se guardó la venta, limpiar
            if (FormaPagoV.VentaGuardada)
            {
                dgvVenta.Rows.Clear();

                CBcliente.SelectedIndex = -1;

                idClienteSeleccionado = 0;

                CalcularTotales();

                GenerarCodigoVenta();

                LimpiarCamposProducto();
            }

        }
    }
}
