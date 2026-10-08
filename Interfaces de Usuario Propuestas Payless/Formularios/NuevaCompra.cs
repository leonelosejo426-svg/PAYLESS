using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using Interfaces_de_Usuario_Propuestas_Payless.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless.Formularios
{
    public partial class NuevaCompra : Form
    {
        private CompraDAO compraDAO = new CompraDAO();
        private ProductoDAO productoDAO = new ProductoDAO();
        private ProveedorDAO proveedorDAO = new ProveedorDAO();

        private ListaEnlazadaDetalleCompra listaDetalleCompra = new ListaEnlazadaDetalleCompra();

        // Bandera para silenciar los eventos mientras se cargan los ComboBoxes
        private bool cargandoDatos = false;
        public NuevaCompra()
        {
            InitializeComponent();
            InicializarFormulario();
            ConfigurarFormulario();

        }
        private void ConfigurarFormulario()
        {
            lblNoCompra.Text = "Nueva";
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            cbProveedor.SelectedIndex = -1;
            cbProducto.SelectedIndex = -1;
            cbCategoria.Text = "";
            cbMarca.Text = "";
            txtTalla.Clear();

            txtPrecioCompra.Clear();
            txtPrecioVenta.Clear();
            txtCantidad.Clear();

            lblSubtotal.Text = "C$ 0.00";
            lblIVA.Text = "C$ 0.00";
            lblTotal.Text = "C$ 0.00";

            ConfigurarDataGridView();
        }

        // =========================================
        // CONFIGURAR DATAGRIDVIEW
        // =========================================
        private void ConfigurarDataGridView()
        {
            dgvDetalleCompra.AutoGenerateColumns = false;

            if (dgvDetalleCompra.Columns.Contains("colProducto"))
                dgvDetalleCompra.Columns["colProducto"].DataPropertyName = "Producto";

            if (dgvDetalleCompra.Columns.Contains("colCategoria"))
                dgvDetalleCompra.Columns["colCategoria"].DataPropertyName = "Categoria";

            if (dgvDetalleCompra.Columns.Contains("colMarca"))
                dgvDetalleCompra.Columns["colMarca"].DataPropertyName = "Marca";

            if (dgvDetalleCompra.Columns.Contains("colTalla"))
                dgvDetalleCompra.Columns["colTalla"].DataPropertyName = "Talla";

            if (dgvDetalleCompra.Columns.Contains("colPrecioCompra"))
                dgvDetalleCompra.Columns["colPrecioCompra"].DataPropertyName = "PrecioCompra";

            if (dgvDetalleCompra.Columns.Contains("colCantidad"))
                dgvDetalleCompra.Columns["colCantidad"].DataPropertyName = "Cantidad";

            if (dgvDetalleCompra.Columns.Contains("colSubtotal"))
                dgvDetalleCompra.Columns["colSubtotal"].DataPropertyName = "Subtotal";
        }

        // =========================================
        // CARGAR PROVEEDORES
        // =========================================
        private void CargarProveedores()
        {
            cargandoDatos = true;
            DataTable tabla = proveedorDAO.MostrarProveedoresConProductos();
            cbProveedor.DataSource = tabla;
            cbProveedor.DisplayMember = "nombre";
            cbProveedor.ValueMember = "id_proveedor";
            cbProveedor.SelectedIndex = -1;
            cbProveedor.Text = "";
            cargandoDatos = false;
        }
        // =========================================
        // CARGAR PRODUCTOS
        // =========================================
        private void CargarProductos()
        {
            DataTable tabla = productoDAO.MostrarProductosParaCompra();
            cbProducto.DataSource = tabla;
            cbProducto.DisplayMember = "nombre";
            cbProducto.ValueMember = "id_producto";
            cbProducto.SelectedIndex = -1;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NuevaCompra_Load(object sender, EventArgs e)
        {
            try
            {
                // Generar el correlativo en el Label (Asegúrate de reemplazar 'lblNoCompra' por el Name de tu Label)
                lblNoCompra.Text = compraDAO.GenerarCodigoCompra();

                // Demás inicializaciones...
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la pantalla de compra: " + ex.Message);
            }
        }

        private void InicializarFormulario()
        {
            cargandoDatos = true;

            lblNoCompra.Text = "Nueva";
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            lblSubtotal.Text = "C$ 0.00";
            lblIVA.Text = "C$ 0.00";
            lblTotal.Text = "C$ 0.00";

            ConfigurarDataGridView();
            CargarProveedores();
            LimpiarCamposProducto();

            cargandoDatos = false;
        }

        private void cbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoDatos || cbProducto.SelectedIndex == -1 || cbProducto.SelectedItem == null)
                return;

            try
            {
                DataRowView fila = cbProducto.SelectedItem as DataRowView;
                if (fila == null) return;

                // Carga automáticamente Categoría y Marca
                cbCategoria.Text = fila["nombre_categoria"]?.ToString() ?? "";
                cbMarca.Text = fila["nombre_marca"]?.ToString() ?? "";

                // Limpia los campos para ingresar nueva talla, precio compra y precio venta
                txtTalla.Clear();
                txtPrecioCompra.Clear();
                txtPrecioVenta.Clear();
                txtCantidad.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar el producto: " + ex.Message);
            }
        }

        // =========================================
        // CARGAR TALLAS
        // =========================================


        private void btnAgregarProductos_Click(object sender, EventArgs e)
        {
            if (cbProveedor.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione primero un proveedor.");
                return;
            }

            if (cbProducto.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTalla.Text))
            {
                MessageBox.Show("Ingrese la talla del producto.");
                txtTalla.Focus();
                return;
            }

            if (!decimal.TryParse(txtPrecioCompra.Text, out decimal precioCompra) || precioCompra <= 0)
            {
                MessageBox.Show("Ingrese un precio de compra válido.");
                txtPrecioCompra.Focus();
                return;
            }

            if (!decimal.TryParse(txtPrecioVenta.Text, out decimal precioVenta) || precioVenta <= 0)
            {
                MessageBox.Show("Ingrese un precio de venta válido.");
                txtPrecioVenta.Focus();
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida mayor a cero.");
                txtCantidad.Focus();
                return;
            }

            DataRowView producto = cbProducto.SelectedItem as DataRowView;
            if (producto == null) return;

            decimal subtotal = precioCompra * cantidad;

            NodoDetalleCompra nuevo = new NodoDetalleCompra
            {
                IdProducto = Convert.ToInt32(producto["id_producto"]),
                Producto = producto["nombre"].ToString(),
                Categoria = producto["nombre_categoria"].ToString(),
                Marca = producto["nombre_marca"].ToString(),
                Talla = txtTalla.Text.Trim(),
                PrecioCompra = precioCompra,
                PrecioVenta = precioVenta,
                Cantidad = cantidad,
                Subtotal = subtotal
            };

            listaDetalleCompra.Agregar(nuevo);

            // Se insertan las celdas asegurando el orden correcto de columnas de la grilla
            dgvDetalleCompra.Rows.Add(
                nuevo.IdProducto,
                nuevo.Producto,
                nuevo.Categoria,
                nuevo.Marca,
                nuevo.Talla,
                nuevo.PrecioCompra,
                nuevo.Cantidad,
                nuevo.Subtotal
            );

            CalcularTotales();
            LimpiarCamposProducto();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (listaDetalleCompra.EstaVacia())
            {
                MessageBox.Show("No hay productos para eliminar.");
                return;
            }

            listaDetalleCompra.EliminarUltimo();

            if (dgvDetalleCompra.Rows.Count > 0)
            {
                dgvDetalleCompra.Rows.RemoveAt(dgvDetalleCompra.Rows.Count - 1);
            }

            CalcularTotales();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleCompra.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione un producto para editar.");
                return;
            }

            MessageBox.Show(
                "La edición se realizará desde los datos del producto seleccionado.");
        }
        private void CalcularTotales()
        {
            decimal subtotal = listaDetalleCompra.ObtenerSubtotal();
            decimal iva = subtotal * 0.15m;
            decimal total = subtotal + iva;

            lblSubtotal.Text = "C$ " + subtotal.ToString("N2");
            lblIVA.Text = "C$ " + iva.ToString("N2");
            lblTotal.Text = "C$ " + total.ToString("N2");
        }

        private void btnGuardarCompra_Click(object sender, EventArgs e)
        {
            if (cbProveedor.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un proveedor.");
                return;
            }

            if (listaDetalleCompra.EstaVacia())
            {
                MessageBox.Show("Agregue productos a la lista de compra.");
                return;
            }

            decimal subtotal = listaDetalleCompra.ObtenerSubtotal();
            decimal total = subtotal + (subtotal * 0.15m);
            int idProveedor = Convert.ToInt32(cbProveedor.SelectedValue);

            try
            {
                int idCompra = compraDAO.RegistrarCompraTransaccional(idProveedor, total, listaDetalleCompra);

                lblNoCompra.Text = idCompra.ToString();
                MessageBox.Show("Compra guardada e inventario actualizado correctamente.");

                listaDetalleCompra.Limpiar();
                dgvDetalleCompra.Rows.Clear();

                cargandoDatos = true;
                cbProveedor.SelectedIndex = -1;
                cbProveedor.Text = "";
                cbProducto.DataSource = null;
                LimpiarCamposProducto();
                cargandoDatos = false;

                CalcularTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la compra:\n" + ex.Message);
            }
        }






        private void LimpiarCamposProducto()
        {
            bool estadoAnterior = cargandoDatos;
            cargandoDatos = true;

            cbProducto.SelectedIndex = -1;
            cbProducto.Text = "";

            cbCategoria.Text = "";
            cbMarca.Text = "";

            txtTalla.Clear();
            txtPrecioCompra.Clear();
            txtPrecioVenta.Clear();
            txtCantidad.Clear();

            cargandoDatos = estadoAnterior;
        }

        private void LimpiarProducto()
        {
            bool estadoAnterior = cargandoDatos;
            cargandoDatos = true;

            cbProducto.DataSource = null;
            txtTalla.Clear();
            cbCategoria.Text = "";
            cbMarca.Text = "";

            txtPrecioCompra.Clear();
            txtPrecioVenta.Clear();
            txtCantidad.Clear();

            cargandoDatos = estadoAnterior;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            listaDetalleCompra.Limpiar();
            dgvDetalleCompra.Rows.Clear();

            cargandoDatos = true;
            cbProveedor.SelectedIndex = -1;
            cbProveedor.Text = "";
            cbProducto.DataSource = null;
            LimpiarCamposProducto();
            cargandoDatos = false;

            lblNoCompra.Text = "Nueva";
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            CalcularTotales();
        }

        private void cbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoDatos) return;

            LimpiarCamposProducto();
            cbProducto.DataSource = null;

            if (cbProveedor.SelectedIndex != -1 && cbProveedor.SelectedItem is DataRowView fila)
            {
                int idProveedor = Convert.ToInt32(fila["id_proveedor"]);
                CargarProductosPorProveedor(idProveedor);
            }
        }

        private void CargarProductosPorProveedor(int idProveedor)
        {
            try
            {
                cargandoDatos = true;

                DataTable tabla = productoDAO.MostrarProductosPorProveedor(idProveedor);

                cbProducto.DataSource = tabla;
                cbProducto.DisplayMember = "nombre";
                cbProducto.ValueMember = "id_producto";
                cbProducto.SelectedIndex = -1;
                cbProducto.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos del proveedor: " + ex.Message);
            }
            finally
            {
                cargandoDatos = false;
            }
        }
    }
}

