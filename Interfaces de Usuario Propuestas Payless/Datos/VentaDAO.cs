using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using iTextSharp.text.pdf.codec.wmf;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Interfaces_de_Usuario_Propuestas_Payless.Ventas;

namespace Interfaces_de_Usuario_Propuestas_Payless.Datos
{
    public class VentaDAO
    {

        private ConexionBD conexionBD = new ConexionBD();

        // =====================================================
        // 1. CARGAR CLIENTES
        // =====================================================
        public DataTable CargarClientes()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT id_cliente, nombre
                    FROM cliente
                    WHERE estado = TRUE
                    ORDER BY nombre;";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    using (NpgsqlDataAdapter da =
                        new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }

        // =====================================================
        // 2. CARGAR PRODUCTOS
        // =====================================================

        public DataTable CargarProductos()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT 
                p.id_producto,
                p.nombre,
                c.nombre_categoria,
                m.nombre_marca
            FROM producto p
            INNER JOIN categoria c ON p.id_categoria = c.id_categoria
            INNER JOIN marca m ON p.id_marca = m.id_marca
            WHERE p.estado_producto = TRUE
            ORDER BY p.nombre;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }

        // =====================================================
        // 3. OBTENER INFORMACIÓN DEL PRODUCTO
        // =====================================================
        public DataTable ObtenerProducto(int idProducto)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT 
                p.id_producto,
                p.nombre,
                p.id_categoria,
                c.nombre_categoria,
                p.id_marca,
                m.nombre_marca
            FROM producto p
            INNER JOIN categoria c ON p.id_categoria = c.id_categoria
            INNER JOIN marca m ON p.id_marca = m.id_marca
            WHERE p.id_producto = @idProducto;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);

                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }

        // =====================================================
        // 4. CARGAR TALLAS DEL PRODUCTO
        // =====================================================
        public DataTable CargarTallas(int idProducto)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT 
                        pt.id_producto_talla,
                        pt.talla
                    FROM producto_talla pt
                    INNER JOIN inventario i 
                        ON pt.id_producto_talla = i.id_producto_talla
                    WHERE pt.id_producto = @idProducto
                      AND i.stock_actual > 0
                    ORDER BY pt.talla;";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@idProducto",
                        idProducto);

                    using (NpgsqlDataAdapter da =
                        new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }

        // =====================================================
        // 5. OBTENER STOCK DE PRODUCTO-TALLA
        // =====================================================
        public int ObtenerStockProductoTalla(int idProductoTalla)
        {
            int stock = 0;

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT stock_actual
                    FROM inventario
                    WHERE id_producto_talla = @idProductoTalla;";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@idProductoTalla",
                        idProductoTalla);

                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null &&
                        resultado != DBNull.Value)
                    {
                        stock = Convert.ToInt32(resultado);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return stock;
        }

        // =====================================================
        // 6. OBTENER PRECIO DE VENTA POR TALLA
        // =====================================================
        public decimal? ObtenerPrecioProductoTalla(int idProductoTalla)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT precio_venta
            FROM producto_talla
            WHERE id_producto_talla = @idProductoTalla;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idProductoTalla", idProductoTalla);

                    object resultado = cmd.ExecuteScalar();

                    if (resultado == null || resultado == DBNull.Value)
                    {
                        return null;
                    }

                    return Convert.ToDecimal(resultado);
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // =====================================================
        // 7. OBTENER TIPO DE CAMBIO DE LA CAJA ABIERTA
        // =====================================================
        public decimal ObtenerTipoCambioActual()
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT tipo_cambio_dolar
                    FROM caja
                    WHERE estado_caja = 'Abierta'
                    ORDER BY id_caja DESC
                    LIMIT 1;";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null &&
                        resultado != DBNull.Value)
                    {
                        return Convert.ToDecimal(resultado);
                    }
                }
            }
            catch
            {
                // Se utiliza el valor por defecto
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return 36.50m;
        }

        // =====================================================
        // 8. GENERAR CÓDIGO DE VENTA
        // =====================================================
        public string GenerarCodigoVenta()
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT COALESCE(MAX(id_venta), 0) + 1
                    FROM venta;";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    int siguiente =
                        Convert.ToInt32(cmd.ExecuteScalar());

                    return "V" + siguiente.ToString("D5");
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // =====================================================
        // 9. OBTENER ID DE LA CAJA ABIERTA
        // =====================================================
        public int ObtenerIdCajaAbierta()
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT id_caja
                    FROM caja
                    WHERE estado_caja = 'Abierta'
                    ORDER BY id_caja DESC
                    LIMIT 1;";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null &&
                        resultado != DBNull.Value)
                    {
                        return Convert.ToInt32(resultado);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return 0;
        }

        // =====================================================
        // 10. REGISTRAR VENTA Y PAGO
        // =====================================================
        
        public bool RegistrarVentaConPago(
            string codigoVenta,
            int idCliente,
            int idUsuario,
            int idCaja,
            decimal subtotal,
            decimal iva,
            decimal total,
            string tipoPago,
            decimal montoCordobas,
            decimal montoDolares,
            decimal tipoCambio,
            decimal cambio,
            string tipoTarjeta,
            decimal montoTarjeta,
            DataTable detalleVenta)
        {
            try
            {
                conexionBD.AbrirConexion();
                NpgsqlConnection conexion = conexionBD.ObtenerConexion();

                using (NpgsqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        // A. Insertar Encabezado de Venta
                        string sqlVenta = @"
                    INSERT INTO venta
                    (
                        codigo_venta,
                        fecha,
                        subtotal,
                        descuento,
                        iva,
                        total,
                        estado,
                        id_cliente,
                        id_usuario,
                        id_caja
                    )
                    VALUES
                    (
                        @codigoVenta,
                        CURRENT_TIMESTAMP,
                        @subtotal,
                        0,
                        @iva,
                        @total,
                        TRUE,
                        @idCliente,
                        @idUsuario,
                        @idCaja
                    )
                    RETURNING id_venta;";

                        int idVenta;

                        using (NpgsqlCommand cmdVenta = new NpgsqlCommand(sqlVenta, conexion, transaccion))
                        {
                            cmdVenta.Parameters.AddWithValue("@codigoVenta", codigoVenta);
                            cmdVenta.Parameters.AddWithValue("@subtotal", subtotal);
                            cmdVenta.Parameters.AddWithValue("@iva", iva);
                            cmdVenta.Parameters.AddWithValue("@total", total);
                            cmdVenta.Parameters.AddWithValue("@idCliente", idCliente > 0 ? (object)idCliente : DBNull.Value);
                            cmdVenta.Parameters.AddWithValue("@idUsuario", idUsuario);
                            cmdVenta.Parameters.AddWithValue("@idCaja", idCaja);

                            idVenta = Convert.ToInt32(cmdVenta.ExecuteScalar());
                        }

                        // B. Insertar Detalle de Venta
                        foreach (DataRow fila in detalleVenta.Rows)
                        {
                            int idProductoTalla = Convert.ToInt32(fila["id_producto_talla"]);
                            int cantidad = Convert.ToInt32(fila["cantidad"]);
                            decimal precioUnitario = Convert.ToDecimal(fila["precio_venta"]);
                            decimal subtotalLinea = Convert.ToDecimal(fila["subtotal"]);

                            string sqlDetalle = @"
                        INSERT INTO detalle_venta
                        (
                            id_venta,
                            id_producto_talla,
                            cantidad,
                            precio_unitario,
                            subtotal
                        )
                        VALUES
                        (
                            @idVenta,
                            @idProductoTalla,
                            @cantidad,
                            @precioUnitario,
                            @subtotal
                        );";

                            using (NpgsqlCommand cmdDetalle = new NpgsqlCommand(sqlDetalle, conexion, transaccion))
                            {
                                cmdDetalle.Parameters.AddWithValue("@idVenta", idVenta);
                                cmdDetalle.Parameters.AddWithValue("@idProductoTalla", idProductoTalla);
                                cmdDetalle.Parameters.AddWithValue("@cantidad", cantidad);
                                cmdDetalle.Parameters.AddWithValue("@precioUnitario", precioUnitario);
                                cmdDetalle.Parameters.AddWithValue("@subtotal", subtotalLinea);

                                cmdDetalle.ExecuteNonQuery();
                            }
                        }

                        // C. Insertar Forma de Pago
                        string sqlPago = @"
                    INSERT INTO forma_pago
                    (
                        tipo_pago,
                        monto_cordobas,
                        monto_dolares,
                        tipo_cambio,
                        cambio,
                        tipo_tarjeta,
                        monto_tarjeta,
                        id_venta
                    )
                    VALUES
                    (
                        @tipoPago,
                        @montoCordobas,
                        @montoDolares,
                        @tipoCambio,
                        @cambio,
                        @tipoTarjeta,
                        @montoTarjeta,
                        @idVenta
                    );";

                        using (NpgsqlCommand cmdPago = new NpgsqlCommand(sqlPago, conexion, transaccion))
                        {
                            cmdPago.Parameters.AddWithValue("@tipoPago", tipoPago);
                            cmdPago.Parameters.AddWithValue("@montoCordobas", montoCordobas);
                            cmdPago.Parameters.AddWithValue("@montoDolares", montoDolares);
                            cmdPago.Parameters.AddWithValue("@tipoCambio", tipoCambio);
                            cmdPago.Parameters.AddWithValue("@cambio", cambio);
                            cmdPago.Parameters.AddWithValue("@tipoTarjeta", string.IsNullOrWhiteSpace(tipoTarjeta) ? (object)DBNull.Value : tipoTarjeta);
                            cmdPago.Parameters.AddWithValue("@montoTarjeta", montoTarjeta);
                            cmdPago.Parameters.AddWithValue("@idVenta", idVenta);

                            cmdPago.ExecuteNonQuery();
                        }

                        transaccion.Commit();
                        return true;
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // =====================================================
        // 11. OBTENER DETALLE PARA FACTURA
        // =====================================================
        // Se utiliza id_producto_talla para obtener:
        // producto + marca + categoria + talla.
        // =====================================================
        public DataTable ObtenerDetalleParaFactura(DataTable detalleVenta)
        {
            DataTable dtResultado = new DataTable();

            dtResultado.Columns.Add("producto", typeof(string));
            dtResultado.Columns.Add("marca", typeof(string));
            dtResultado.Columns.Add("categoria", typeof(string));
            dtResultado.Columns.Add("talla", typeof(string));
            dtResultado.Columns.Add("cantidad", typeof(int));
            dtResultado.Columns.Add("precio_venta", typeof(decimal));
            dtResultado.Columns.Add("subtotal", typeof(decimal));

            try
            {
                conexionBD.AbrirConexion();

                foreach (DataRow fila in detalleVenta.Rows)
                {
                    int idProductoTalla = Convert.ToInt32(fila["id_producto_talla"]);
                    int cantidad = Convert.ToInt32(fila["cantidad"]);
                    decimal precioVenta = Convert.ToDecimal(fila["precio_venta"]);
                    decimal subtotal = Convert.ToDecimal(fila["subtotal"]);

                    string sql = @"
                SELECT
                    p.nombre AS producto,
                    m.nombre_marca AS marca,
                    c.nombre_categoria AS categoria,
                    pt.talla
                FROM producto_talla pt
                INNER JOIN producto p ON pt.id_producto = p.id_producto
                INNER JOIN marca m ON p.id_marca = m.id_marca
                INNER JOIN categoria c ON p.id_categoria = c.id_categoria
                WHERE pt.id_producto_talla = @idProductoTalla;";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                    {
                        cmd.Parameters.AddWithValue("@idProductoTalla", idProductoTalla);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                dtResultado.Rows.Add(
                                    reader["producto"].ToString(),
                                    reader["marca"].ToString(),
                                    reader["categoria"].ToString(),
                                    reader["talla"].ToString(),
                                    cantidad,
                                    precioVenta,
                                    subtotal
                                );
                            }
                        }
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return dtResultado;
        }

        // =====================================================
        // 12. OBTENER NOMBRE DEL CLIENTE
        // =====================================================
        public string ObtenerNombreCliente(int idCliente)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT nombre
                    FROM cliente
                    WHERE id_cliente = @idCliente;";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@idCliente",
                        idCliente);

                    object resultado =
                        cmd.ExecuteScalar();

                    if (resultado != null &&
                        resultado != DBNull.Value)
                    {
                        return resultado.ToString();
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return "Cliente general";
        }

        // ============================================================
        // 13. MOSTRAR VENTAS
        // ============================================================
        public DataTable MostrarVentas()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT
                v.id_venta,
                v.codigo_venta,
                v.fecha,
                COALESCE(c.nombre, 'Consumidor final') AS cliente,
                v.subtotal,
                v.iva,
                v.total,
                CASE
                    WHEN v.estado = TRUE THEN 'Activo'
                    ELSE 'Anulado'
                END AS estado
            FROM venta v
            LEFT JOIN cliente c
                ON c.id_cliente = v.id_cliente
            ORDER BY v.id_venta ASC;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    sql,
                    conexionBD.ObtenerConexion()))
                {
                    using (NpgsqlDataAdapter da =
                        new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las ventas: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }

        // ============================================================
        // 14. ANULAR VENTA
        // ============================================================
        public bool EliminarVenta(int idVenta)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            UPDATE venta
            SET estado = FALSE
            WHERE id_venta = @idVenta;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    sql,
                    conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idVenta", idVenta);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al anular la venta: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // ============================================================
        // 15. OBTENER VENTA POR ID
        // ============================================================
        public DataTable ObtenerVentaPorId(int idVenta)
        {
            DataTable dt = new DataTable();

            string query = @"SELECT 
                        v.id_venta, 
                        'V' || LPAD(v.id_venta::text, 5, '0') AS codigo_venta, 
                        v.fecha, 
                        COALESCE(c.nombre, 'Cliente General') AS cliente, 
                        u.nombre_completo AS usuario, 
                        v.subtotal, 
                        v.descuento, 
                        v.iva, 
                        v.total 
                    FROM venta v 
                    LEFT JOIN cliente c ON v.id_cliente = c.id_cliente 
                    INNER JOIN usuario u ON v.id_usuario = u.id_usuario 
                    WHERE v.id_venta = @id_venta;";

            try
            {
                var conexion = conexionBD.ObtenerConexion();
                conexionBD.AbrirConexion();

                using (var cmd = new NpgsqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id_venta", idVenta);
                    using (var adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return dt;
        }

        public DataTable ObtenerDetalleVentaPorId(int idVenta)
        {
            DataTable dt = new DataTable();
            ConexionBD conexionBD = new ConexionBD();

            using (NpgsqlConnection con = new NpgsqlConnection(conexionBD.ObtenerConexion().ConnectionString))
            {
                string query = @"
            SELECT 
                p.nombre AS producto,
                m.nombre_marca AS marca,
                cat.nombre_categoria AS categoria,
                pt.talla,
                dv.cantidad,
                dv.precio_unitario AS precio_venta,
                dv.subtotal
            FROM detalle_venta dv
            INNER JOIN producto_talla pt ON dv.id_producto_talla = pt.id_producto_talla
            INNER JOIN producto p ON pt.id_producto = p.id_producto
            LEFT JOIN marca m ON p.id_marca = m.id_marca
            LEFT JOIN categoria cat ON p.id_categoria = cat.id_categoria
            WHERE dv.id_venta = @id_venta;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id_venta", idVenta);
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        con.Open();
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }






    }
}
