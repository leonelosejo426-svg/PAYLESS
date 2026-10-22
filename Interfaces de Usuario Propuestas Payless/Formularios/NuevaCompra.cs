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

            // NUEVO: Enlazar la columna del Precio de Venta (Asegúrate de que tu columna en el diseñador se llame colPrecioVenta)
            if (dgvDetalleCompra.Columns.Contains("colPrecioVenta"))
                dgvDetalleCompra.Columns["colPrecioVenta"].DataPropertyName = "PrecioVenta";

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
            txtCantidad.KeyPress += (s, ev) =>
            {
                // Permitir solo dígitos y control (como backspace)
                if (!char.IsControl(ev.KeyChar) && !char.IsDigit(ev.KeyChar))
                {
                    ev.Handled = true;
                }
            };
            txtPrecioCompra.KeyPress += (s, ev) =>
            {
                // Permitir solo dígitos, punto y control
                if (!char.IsControl(ev.KeyChar) && !char.IsDigit(ev.KeyChar) && ev.KeyChar != '.')
                {
                    ev.Handled = true;
                }
            };
            txtPrecioVenta.KeyPress += (s, ev) =>
            {
                // Permitir solo dígitos, punto y control
                if (!char.IsControl(ev.KeyChar) && !char.IsDigit(ev.KeyChar) && ev.KeyChar != '.')
                {
                    ev.Handled = true;
                }
            };
            txtTalla.KeyPress += (s, ev) =>
            {
                // Permitir solo dígitos, punto y control
                if (!char.IsControl(ev.KeyChar) && !char.IsDigit(ev.KeyChar) && ev.KeyChar != '.')
                {
                    ev.Handled = true;
                }
            };

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
                MessageBox.Show("Seleccione primero un proveedor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbProveedor.Focus();
                return;
            }

            if (cbProducto.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un producto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbProducto.Focus();
                return;
            }

            // =====================================================
            // VALIDACIÓN DE TALLA (Solo números, formato 45 o 45.1)
            // =====================================================
            string tallaTexto = txtTalla.Text.Trim().Replace(",", ".");
            bool formatoTallaValido = System.Text.RegularExpressions.Regex.IsMatch(tallaTexto, @"^\d{1,2}(\.\d)?$");

            if (string.IsNullOrWhiteSpace(tallaTexto) || !formatoTallaValido)
            {
                MessageBox.Show(
                    "Ingrese una talla válida.\nDebe ser un número entero o con un solo decimal (Ejemplo: 42 o 42.5).",
                    "Validación de Talla",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtTalla.Focus();
                return;
            }

            // =====================================================
            // VALIDACIÓN DE PRECIO DE COMPRA
            // =====================================================
            string precioCompraTexto = txtPrecioCompra.Text.Trim().Replace(",", ".");
            if (!decimal.TryParse(precioCompraTexto, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal precioCompra) || precioCompra <= 0)
            {
                MessageBox.Show(
                    "Ingrese un precio de compra válido mayor a cero.",
                    "Validación de Precio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtPrecioCompra.Focus();
                return;
            }

            // =====================================================
            // VALIDACIÓN DE PRECIO DE VENTA
            // =====================================================
            string precioVentaTexto = txtPrecioVenta.Text.Trim().Replace(",", ".");
            if (!decimal.TryParse(precioVentaTexto, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal precioVenta) || precioVenta <= 0)
            {
                MessageBox.Show(
                    "Ingrese un precio de venta válido mayor a cero.",
                    "Validación de Precio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtPrecioVenta.Focus();
                return;
            }

            // =====================================================
            // VALIDACIÓN: PRECIO DE COMPRA MENOR AL DE VENTA
            // =====================================================
            if (precioCompra >= precioVenta)
            {
                MessageBox.Show(
                    "El precio de venta debe de ser mayor a precio compra.",
                    "Validación de Precios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtPrecioVenta.Focus();
                return;
            }

            // =====================================================
            // RECOMENDACIÓN DE MARGEN (Mínimo 20% expresado en C$)
            // =====================================================
            decimal precioRecomendado = precioCompra * 1.20m; // Se le suma el 20% al precio de compra

            if (precioVenta < precioRecomendado)
            {
                DialogResult confirmacion = MessageBox.Show(
                    $"SUGERENCIA DE PRECIO DE VENTA:\n\n" +
                    $"• Precio de Compra: C$ {precioCompra:N2}\n" +
                    $"• Precio de Venta Ingresado: C$ {precioVenta:N2}\n" +
                    $"• Precio Recomendado (+20%): C$ {precioRecomendado:N2}\n\n" +
                    $"El precio ingresado es menor a la recomendación de C$ {precioRecomendado:N2}.\n" +
                    $"¿Desea continuar e ingresar este precio de todas formas?",
                    "Recomendación de Margen",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.No)
                {
                    txtPrecioVenta.Focus();
                    return;
                }
            }

            // =====================================================
            // VALIDACIÓN DE CANTIDAD
            // =====================================================
            if (!int.TryParse(txtCantidad.Text.Trim(), out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show(
                    "Ingrese una cantidad válida mayor a cero.",
                    "Validación de Cantidad",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtCantidad.Focus();
                return;
            }

            DataRowView producto = cbProducto.SelectedItem as DataRowView;
            if (producto == null) return;

            int idProducto = Convert.ToInt32(producto["id_producto"]);

            // 1. VERIFICAR SI YA EXISTE EL PRODUCTO CON LA MISMA TALLA EN LA LISTA
            NodoDetalleCompra nodoExistente = listaDetalleCompra.BuscarPorProductoYTalla(idProducto, tallaTexto);

            if (nodoExistente != null)
            {
                // CASO A: Misma talla y MISMOS precios -> Se suman las cantidades
                if (nodoExistente.PrecioCompra == precioCompra && nodoExistente.PrecioVenta == precioVenta)
                {
                    nodoExistente.Cantidad += cantidad;
                    nodoExistente.Subtotal = nodoExistente.PrecioCompra * nodoExistente.Cantidad;

                    MessageBox.Show(
                        "El producto ya se encontraba en el detalle con la misma talla y precios. Se ha actualizado sumando la cantidad.",
                        "Cantidad Actualizada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                // CASO B: Misma talla pero DIFERENTES precios -> BLOQUEO y mensaje de advertencia
                else
                {
                    MessageBox.Show(
                        $"Ya existe el producto '{nodoExistente.Producto}' con la talla {tallaTexto}, pero con precios diferentes:\n\n" +
                        $"• Registrado en lista -> Compra: C$ {nodoExistente.PrecioCompra:N2} | Venta: C$ {nodoExistente.PrecioVenta:N2}\n" +
                        $"• Intentando agregar -> Compra: C$ {precioCompra:N2} | Venta: C$ {precioVenta:N2}\n\n" +
                        $"No se puede agregar el mismo producto/talla con precios distintos. Debe eliminar el registro anterior o cambiar los precios.",
                        "Conflicto de Precios",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtPrecioCompra.Focus();
                    return;
                }
            }
            else
            {
                // CASO C: Es completamente nuevo o tiene otra talla -> Se crea una fila nueva
                decimal subtotal = precioCompra * cantidad;

                NodoDetalleCompra nuevo = new NodoDetalleCompra
                {
                    IdProducto = idProducto,
                    Producto = producto["nombre"].ToString(),
                    Categoria = producto["nombre_categoria"].ToString(),
                    Marca = producto["nombre_marca"].ToString(),
                    Talla = tallaTexto,
                    PrecioCompra = precioCompra,
                    PrecioVenta = precioVenta,
                    Cantidad = cantidad,
                    Subtotal = subtotal
                };

                listaDetalleCompra.Agregar(nuevo);
            }

            // 2. Refrescar la grilla y recalcular totales
            ActualizarGridDetalle();
            CalcularTotales();
            LimpiarCamposProducto();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (listaDetalleCompra.EstaVacia())
            {
                MessageBox.Show("No hay productos en el detalle para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Eliminamos de nuestra estructura de Lista Enlazada personalizada
            listaDetalleCompra.EliminarUltimo();

            // 2. Refrescamos visualmente el DataGridView de forma limpia
            // En lugar de RemoveAt(), volvemos a cargar los elementos actuales de la lista enlazada a la grilla
            ActualizarGridDetalle();

            // 3. Recalculamos subtotales y totales
            CalcularTotales();
        }
        private void ActualizarGridDetalle()
        {
            // 1. Limpiar las filas existentes del DataGridView de forma segura
            dgvDetalleCompra.Rows.Clear();

            NodoDetalleCompra actual = listaDetalleCompra.ObtenerPrimero();

            while (actual != null)
            {
                dgvDetalleCompra.Rows.Add(
                    actual.IdProducto,     // Columna 0: ID
                    actual.Producto,       // Columna 1: Producto
                    actual.Categoria,      // Columna 2: Categoría
                    actual.Marca,          // Columna 3: Marca
                    actual.Talla,          // Columna 4: Talla
                    actual.PrecioCompra,   // Columna 5: P. Compra
                    actual.PrecioVenta,    // Columna 6: P. Venta (NUEVO)
                    actual.Cantidad,       // Columna 7: Cantidad
                    actual.Subtotal        // Columna 8: SubTotal
                );

                actual = actual.Siguiente;
            }
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

        private void SincronizarListaEnlazadaDesdeGrid()
        {
            // 1. Limpiamos la lista enlazada actual
            listaDetalleCompra.Limpiar();

            // 2. Recorremos todas las filas del DataGridView para volver a poblarla
            foreach (DataGridViewRow row in dgvDetalleCompra.Rows)
            {
                if (row.Cells[0].Value == null) continue;

                NodoDetalleCompra nodo = new NodoDetalleCompra
                {
                    IdProducto = Convert.ToInt32(row.Cells[0].Value),
                    Producto = row.Cells[1].Value.ToString(),
                    Categoria = row.Cells[2].Value.ToString(),
                    Marca = row.Cells[3].Value.ToString(),
                    Talla = row.Cells[4].Value.ToString(),
                    PrecioCompra = Convert.ToDecimal(row.Cells[5].Value),
                    PrecioVenta = Convert.ToDecimal(row.Cells[6].Value),
                    Cantidad = Convert.ToInt32(row.Cells[7].Value),
                    Subtotal = Convert.ToDecimal(row.Cells[8].Value)
                };

                listaDetalleCompra.Agregar(nodo);
            }
        }

        private void dgvDetalleCompra_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDetalleCompra.Rows.Count)
                return;

            DataGridViewRow row = dgvDetalleCompra.Rows[e.RowIndex];

            if (row.Cells[0].Value == null)
                return;

            try
            {
                // 1. Extraer los datos de la fila seleccionada
                int idProducto = Convert.ToInt32(row.Cells[0].Value);
                string talla = row.Cells[4].Value.ToString();
                decimal precioCompra = Convert.ToDecimal(row.Cells[5].Value);
                decimal precioVenta = Convert.ToDecimal(row.Cells[6].Value); // Ajusta el índice si la columna está en otra posición
                int cantidad = Convert.ToInt32(row.Cells[7].Value);

                // 2. Seleccionar el producto en el ComboBox
                cargandoDatos = true;
                cbProducto.SelectedValue = idProducto;
                cargandoDatos = false;

                // Disparar o asignar manualmente categoría y marca según el producto seleccionado
                if (cbProducto.SelectedItem is DataRowView filaProducto)
                {
                    cbCategoria.Text = filaProducto["nombre_categoria"]?.ToString() ?? "";
                    cbMarca.Text = filaProducto["nombre_marca"]?.ToString() ?? "";
                }

                // 3. Devolver los valores a los TextBox
                txtTalla.Text = talla;
                txtPrecioCompra.Text = precioCompra.ToString("0.00");
                txtPrecioVenta.Text = precioVenta.ToString("0.00");
                txtCantidad.Text = cantidad.ToString();

                // 4. Eliminar la fila de la grilla temporalmente
                dgvDetalleCompra.Rows.RemoveAt(e.RowIndex);

                // 5. Sincronizar la lista enlazada y recalcular totales
                SincronizarListaEnlazadaDesdeGrid();
                CalcularTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el producto para editar:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

