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
                        p.precio_venta,
                        c.nombre_categoria,
                        m.nombre_marca
                    FROM producto p
                    INNER JOIN categoria c 
                        ON p.id_categoria = c.id_categoria
                    INNER JOIN marca m 
                        ON p.id_marca = m.id_marca
                    WHERE p.estado_producto = TRUE
                    ORDER BY p.nombre;";

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
                        p.precio_venta,
                        p.id_categoria,
                        c.nombre_categoria,
                        p.id_marca,
                        m.nombre_marca
                    FROM producto p
                    INNER JOIN categoria c 
                        ON p.id_categoria = c.id_categoria
                    INNER JOIN marca m 
                        ON p.id_marca = m.id_marca
                    WHERE p.id_producto = @idProducto;";

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
        // 6. OBTENER PRECIO DE VENTA
        // =====================================================
        public decimal? ObtenerPrecioProducto(int idProducto)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT precio_venta
                    FROM producto
                    WHERE id_producto = @idProducto;";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@idProducto",
                        idProducto);

                    object resultado = cmd.ExecuteScalar();

                    if (resultado == null ||
                        resultado == DBNull.Value)
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

                NpgsqlConnection conexion =
                    conexionBD.ObtenerConexion();

                using (NpgsqlTransaction transaccion =
                    conexion.BeginTransaction())
                {
                    try
                    {
                        // =================================================
                        // A. INSERTAR ENCABEZADO DE LA VENTA
                        // =================================================

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

                        using (NpgsqlCommand cmdVenta =
                            new NpgsqlCommand(
                                sqlVenta,
                                conexion,
                                transaccion))
                        {
                            cmdVenta.Parameters.AddWithValue(
                                "@codigoVenta",
                                codigoVenta);

                            cmdVenta.Parameters.AddWithValue(
                                "@subtotal",
                                subtotal);

                            cmdVenta.Parameters.AddWithValue(
                                "@iva",
                                iva);

                            cmdVenta.Parameters.AddWithValue(
                                "@total",
                                total);

                            cmdVenta.Parameters.AddWithValue(
                                "@idCliente",
                                idCliente > 0
                                    ? (object)idCliente
                                    : DBNull.Value);

                            cmdVenta.Parameters.AddWithValue(
                                "@idUsuario",
                                idUsuario);

                            cmdVenta.Parameters.AddWithValue(
                                "@idCaja",
                                idCaja);

                            idVenta =
                                Convert.ToInt32(
                                    cmdVenta.ExecuteScalar());
                        }

                        // =================================================
                        // B. INSERTAR DETALLE DE VENTA
                        // =================================================

                        foreach (DataRow fila in detalleVenta.Rows)
                        {
                            int idProductoTalla =
                                Convert.ToInt32(
                                    fila["id_producto_talla"]);

                            int cantidad =
                                Convert.ToInt32(
                                    fila["cantidad"]);

                            decimal precioUnitario =
                                Convert.ToDecimal(
                                    fila["precio_venta"]);

                            decimal subtotalLinea =
                                Convert.ToDecimal(
                                    fila["subtotal"]);

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

                            using (NpgsqlCommand cmdDetalle =
                                new NpgsqlCommand(
                                    sqlDetalle,
                                    conexion,
                                    transaccion))
                            {
                                cmdDetalle.Parameters.AddWithValue(
                                    "@idVenta",
                                    idVenta);

                                cmdDetalle.Parameters.AddWithValue(
                                    "@idProductoTalla",
                                    idProductoTalla);

                                cmdDetalle.Parameters.AddWithValue(
                                    "@cantidad",
                                    cantidad);

                                cmdDetalle.Parameters.AddWithValue(
                                    "@precioUnitario",
                                    precioUnitario);

                                cmdDetalle.Parameters.AddWithValue(
                                    "@subtotal",
                                    subtotalLinea);

                                cmdDetalle.ExecuteNonQuery();
                            }

                            // =================================================
                            // C. ACTUALIZAR INVENTARIO
                            // =================================================
                            //
                            // IMPORTANTE:
                            // Si vas a utilizar el trigger de venta que
                            // actualiza inventario, NO hagas este UPDATE aquí.
                            //
                            // El trigger será el encargado de disminuir
                            // el stock automáticamente.
                            //
                        }

                        // =================================================
                        // D. INSERTAR FORMA DE PAGO
                        // =================================================

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

                        using (NpgsqlCommand cmdPago =
                            new NpgsqlCommand(
                                sqlPago,
                                conexion,
                                transaccion))
                        {
                            cmdPago.Parameters.AddWithValue(
                                "@tipoPago",
                                tipoPago);

                            cmdPago.Parameters.AddWithValue(
                                "@montoCordobas",
                                montoCordobas);

                            cmdPago.Parameters.AddWithValue(
                                "@montoDolares",
                                montoDolares);

                            cmdPago.Parameters.AddWithValue(
                                "@tipoCambio",
                                tipoCambio);

                            cmdPago.Parameters.AddWithValue(
                                "@cambio",
                                cambio);

                            cmdPago.Parameters.AddWithValue(
                                "@tipoTarjeta",
                                string.IsNullOrWhiteSpace(tipoTarjeta)
                                    ? (object)DBNull.Value
                                    : tipoTarjeta);

                            cmdPago.Parameters.AddWithValue(
                                "@montoTarjeta",
                                montoTarjeta);

                            cmdPago.Parameters.AddWithValue(
                                "@idVenta",
                                idVenta);

                            cmdPago.ExecuteNonQuery();
                        }

                        // =================================================
                        // E. CONFIRMAR TRANSACCIÓN
                        // =================================================

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
        public DataTable ObtenerDetalleParaFactura(
            DataTable detalleVenta)
        {
            DataTable dtResultado = new DataTable();

            dtResultado.Columns.Add(
                "producto",
                typeof(string));

            dtResultado.Columns.Add(
                "marca",
                typeof(string));

            dtResultado.Columns.Add(
                "categoria",
                typeof(string));

            dtResultado.Columns.Add(
                "talla",
                typeof(string));

            dtResultado.Columns.Add(
                "cantidad",
                typeof(int));

            dtResultado.Columns.Add(
                "precio_venta",
                typeof(decimal));

            dtResultado.Columns.Add(
                "subtotal",
                typeof(decimal));

            try
            {
                conexionBD.AbrirConexion();

                foreach (DataRow fila in detalleVenta.Rows)
                {
                    int idProductoTalla =
                        Convert.ToInt32(
                            fila["id_producto_talla"]);

                    int cantidad =
                        Convert.ToInt32(
                            fila["cantidad"]);

                    decimal precioVenta =
                        Convert.ToDecimal(
                            fila["precio_venta"]);

                    decimal subtotal =
                        Convert.ToDecimal(
                            fila["subtotal"]);

                    string sql = @"
                        SELECT
                            p.nombre AS producto,
                            m.nombre_marca AS marca,
                            c.nombre_categoria AS categoria,
                            pt.talla
                        FROM producto_talla pt
                        INNER JOIN producto p
                            ON pt.id_producto = p.id_producto
                        INNER JOIN marca m
                            ON p.id_marca = m.id_marca
                        INNER JOIN categoria c
                            ON p.id_categoria = c.id_categoria
                        WHERE pt.id_producto_talla =
                              @idProductoTalla;";

                    using (NpgsqlCommand cmd =
                        new NpgsqlCommand(
                            sql,
                            conexionBD.ObtenerConexion()))
                    {
                        cmd.Parameters.AddWithValue(
                            "@idProductoTalla",
                            idProductoTalla);

                        using (NpgsqlDataReader reader =
                            cmd.ExecuteReader())
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

        private void RegistrarDetalleVenta(
    NpgsqlConnection conexion,
    NpgsqlTransaction transaccion,
    int idVenta,
    DataTable detalleVenta)
        {
            string sql = @"
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

            foreach (DataRow fila in detalleVenta.Rows)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexion, transaccion))
                {
                    cmd.Parameters.AddWithValue("@idVenta", idVenta);
                    cmd.Parameters.AddWithValue(
                        "@idProductoTalla",
                        Convert.ToInt32(fila["id_producto_talla"]));
                    cmd.Parameters.AddWithValue(
                        "@cantidad",
                        Convert.ToInt32(fila["cantidad"]));
                    cmd.Parameters.AddWithValue(
                        "@precioUnitario",
                        Convert.ToDecimal(fila["precio_venta"]));
                    cmd.Parameters.AddWithValue(
                        "@subtotal",
                        Convert.ToDecimal(fila["subtotal"]));

                    cmd.ExecuteNonQuery();
                }
            }
        }

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
            ORDER BY v.id_venta DESC;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    sql, conexionBD.ObtenerConexion()))
                {
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
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


public DataTable BuscarVentas(string campo, string valor)
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
            WHERE ";

                if (campo == "Código")
                {
                    sql += "v.codigo_venta ILIKE @valor ";
                }
                else if (campo == "Cliente")
                {
                    sql += "c.nombre ILIKE @valor ";
                }
                else if (campo == "Fecha")
                {
                    sql += "CAST(v.fecha AS TEXT) ILIKE @valor ";
                }
                else
                {
                    sql += "CAST(v.id_venta AS TEXT) ILIKE @valor ";
                }

                sql += "ORDER BY v.id_venta DESC;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@valor", "%" + valor + "%");

                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar la venta: " + ex.Message,
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
                    sql, conexionBD.ObtenerConexion()))
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


        public DataTable ObtenerVentaPorId(int idVenta)
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
                v.subtotal,
                v.descuento,
                v.iva,
                v.total,
                v.estado,
                v.id_cliente,
                COALESCE(c.nombre, 'Consumidor final') AS cliente
            FROM venta v
            LEFT JOIN cliente c
                ON c.id_cliente = v.id_cliente
            WHERE v.id_venta = @idVenta;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idVenta", idVenta);

                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al obtener la venta: " + ex.Message,
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


        public DataTable ObtenerDetalleVentaPorId(int idVenta)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT
                p.nombre AS producto,
                m.nombre_marca AS marca,
                c.nombre_categoria AS categoria,
                pt.talla,
                dv.cantidad,
                dv.precio_unitario AS precio_venta,
                dv.subtotal
            FROM detalle_venta dv
            INNER JOIN producto_talla pt
                ON pt.id_producto_talla = dv.id_producto_talla
            INNER JOIN producto p
                ON p.id_producto = pt.id_producto
            INNER JOIN marca m
                ON m.id_marca = p.id_marca
            INNER JOIN categoria c
                ON c.id_categoria = p.id_categoria
            WHERE dv.id_venta = @idVenta
            ORDER BY dv.id_detalle_venta;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idVenta", idVenta);

                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al obtener el detalle de la venta: " + ex.Message,
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







    }
}
