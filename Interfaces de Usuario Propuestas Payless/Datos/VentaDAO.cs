using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
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
    internal class VentaDAO
    {

        private ConexionBD conexionBD = new ConexionBD();


        // IVA establecido para el sistema
        private const decimal IVA = 0.15m;


        // =========================================================
        // MOSTRAR TODAS LAS VENTAS
        // =========================================================

        public DataTable MostrarVentas()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        v.id_venta,
                        'V' || LPAD(v.id_venta::text, 5, '0') AS codigo_venta,
                        v.fecha,
                        COALESCE(c.nombre, 'Consumidor final') AS cliente,
                        v.subtotal,
                        v.descuento,
                        v.iva,
                        v.total,
                        CASE
                            WHEN v.estado = TRUE THEN 'Activa'
                            ELSE 'Anulada'
                        END AS estado
                    FROM venta v
                    LEFT JOIN cliente c
                        ON v.id_cliente = c.id_cliente
                    ORDER BY v.id_venta DESC";

                using (NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    da.Fill(tabla);
                }
            }
            catch
            {
                return tabla;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // =========================================================
        // BUSCAR VENTAS
        // =========================================================

        public DataTable BuscarVentas(string texto)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        v.id_venta,
                        'V' || LPAD(v.id_venta::text, 5, '0') AS codigo_venta,
                        v.fecha,
                        COALESCE(c.nombre, 'Consumidor final') AS cliente,
                        v.subtotal,
                        v.descuento,
                        v.iva,
                        v.total,
                        CASE
                            WHEN v.estado = TRUE THEN 'Activa'
                            ELSE 'Anulada'
                        END AS estado
                    FROM venta v
                    LEFT JOIN cliente c
                        ON v.id_cliente = c.id_cliente
                    WHERE
                        ('V' || LPAD(v.id_venta::text, 5, '0'))
                            ILIKE @texto
                        OR COALESCE(c.nombre, '')
                            ILIKE @texto
                    ORDER BY v.id_venta DESC";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@texto",
                        "%" + texto + "%");

                    using (NpgsqlDataAdapter da =
                        new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            catch
            {
                return tabla;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // =========================================================
        // CARGAR CLIENTES ACTIVOS
        // =========================================================

        public DataTable CargarClientes()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT
                id_cliente,
                nombre
            FROM cliente
            WHERE estado = TRUE
            ORDER BY nombre";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
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
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // =========================================================
        // CARGAR PRODUCTOS ACTIVOS
        // =========================================================

        public DataTable CargarProductos()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT
                id_producto,
                nombre
            FROM producto
            WHERE estado_producto = TRUE
            ORDER BY nombre";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
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
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // =========================================================
        // CARGAR CATEGORÍAS
        // =========================================================

        public DataTable CargarCategorias()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_categoria,
                        nombre_categoria
                    FROM categoria
                    WHERE estado = TRUE
                    ORDER BY nombre_categoria";

                using (NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    da.Fill(tabla);
                }
            }
            catch
            {
                return tabla;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // =========================================================
        // CARGAR MARCAS
        // =========================================================

        public DataTable CargarMarcas()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_marca,
                        nombre_marca
                    FROM marca
                    WHERE estado = TRUE
                    ORDER BY nombre_marca";

                using (NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    da.Fill(tabla);
                }
            }
            catch
            {
                return tabla;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // =========================================================
        // CARGAR TALLAS DE UN PRODUCTO
        // =========================================================

        public DataTable CargarTallasProducto(int idProducto)
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
                    INNER JOIN producto p
                        ON pt.id_producto = p.id_producto
                    WHERE
                        pt.id_producto = @id_producto
                        AND p.estado_producto = TRUE
                    ORDER BY pt.talla";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_producto",
                        idProducto);

                    using (NpgsqlDataAdapter da =
                        new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            catch
            {
                return tabla;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // =========================================================
        // OBTENER INFORMACIÓN DEL PRODUCTO Y TALLA
        // =========================================================

        public DataTable ObtenerProductoTalla(int idProductoTalla)
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
                        c.nombre_categoria AS categoria,
                        p.id_marca,
                        m.nombre_marca AS marca,
                        pt.id_producto_talla,
                        pt.talla,
                        i.stock_actual,
                        i.stock_minimo
                    FROM producto_talla pt

                    INNER JOIN producto p
                        ON pt.id_producto = p.id_producto

                    INNER JOIN categoria c
                        ON p.id_categoria = c.id_categoria

                    INNER JOIN marca m
                        ON p.id_marca = m.id_marca

                    LEFT JOIN inventario i
                        ON pt.id_producto_talla =
                           i.id_producto_talla

                    WHERE
                        pt.id_producto_talla =
                        @id_producto_talla";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_producto_talla",
                        idProductoTalla);

                    using (NpgsqlDataAdapter da =
                        new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            catch
            {
                return tabla;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // =========================================================
        // OBTENER PRECIO DE VENTA
        // =========================================================

        public decimal ObtenerPrecioVenta(int idProducto)
        {
            decimal precio = 0;

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT precio_venta
                    FROM producto
                    WHERE id_producto = @id_producto
                    AND estado_producto = TRUE";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_producto",
                        idProducto);

                    object resultado =
                        cmd.ExecuteScalar();

                    if (resultado != null &&
                        resultado != DBNull.Value)
                    {
                        precio =
                            Convert.ToDecimal(resultado);
                    }
                }
            }
            catch
            {
                precio = 0;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return precio;
        }


        // =========================================================
        // OBTENER STOCK ACTUAL
        // =========================================================

        public int ObtenerStock(int idProductoTalla)
        {
            int stock = 0;

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT stock_actual
                    FROM inventario
                    WHERE id_producto_talla =
                          @id_producto_talla";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_producto_talla",
                        idProductoTalla);

                    object resultado =
                        cmd.ExecuteScalar();

                    if (resultado != null &&
                        resultado != DBNull.Value)
                    {
                        stock =
                            Convert.ToInt32(resultado);
                    }
                }
            }
            catch
            {
                stock = 0;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return stock;
        }


        // =========================================================
        // OBTENER SIGUIENTE CÓDIGO DE VENTA
        // =========================================================

        public string ObtenerSiguienteCodigoVenta()
        {
            string codigo = "V00001";

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        'V' ||
                        LPAD(
                            (COALESCE(MAX(id_venta), 0) + 1)::text,
                            5,
                            '0'
                        )
                    FROM venta";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    object resultado =
                        cmd.ExecuteScalar();

                    if (resultado != null)
                    {
                        codigo = resultado.ToString();
                    }
                }
            }
            catch
            {
                codigo = "V00001";
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return codigo;
        }


        // =========================================================
        // OBTENER CAJA ABIERTA
        // =========================================================

        public DataTable ObtenerCajaAbierta()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_caja,
                        tipo_cambio_dolar,
                        saldo_inicial,
                        monto_esperado
                    FROM caja
                    WHERE estado_caja = 'Abierta'
                    ORDER BY id_caja DESC
                    LIMIT 1";

                using (NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    da.Fill(tabla);
                }
            }
            catch
            {
                return tabla;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // =========================================================
        // OBTENER TIPO DE CAMBIO ACTUAL
        // =========================================================

        public decimal ObtenerTipoCambioActual()
        {
            decimal cambio = 0;

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT tipo_cambio_dolar
            FROM caja
            WHERE estado_caja = 'Abierta'
            ORDER BY id_caja DESC
            LIMIT 1";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    object resultado =
                        cmd.ExecuteScalar();

                    if (resultado != null)
                    {
                        cambio =
                            Convert.ToDecimal(resultado);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return cambio;
        }


        // =========================================================
        // AGREGAR VENTA
        // =========================================================
        //
        // Guarda:
        // 1. Venta
        // 2. Detalles
        // 3. Descuenta inventario
        //
        // Todo dentro de una transacción.
        // =========================================================

        public int AgregarVenta(
            int idCliente,
            int idUsuario,
            int idCaja,
            decimal descuento,
            DataTable detalles)
        {
            int idVenta = 0;

            using (NpgsqlConnection conexion =
                new NpgsqlConnection(
                    ObtenerCadenaConexion()))
            {
                conexion.Open();

                using (NpgsqlTransaction transaccion =
                    conexion.BeginTransaction())
                {
                    try
                    {
                        // ==========================================
                        // 1. CALCULAR SUBTOTAL
                        // ==========================================

                        decimal subtotal = 0;

                        foreach (DataRow fila in detalles.Rows)
                        {
                            decimal precio =
                                Convert.ToDecimal(
                                    fila["precio_unitario"]);

                            int cantidad =
                                Convert.ToInt32(
                                    fila["cantidad"]);

                            subtotal += precio * cantidad;
                        }

                        // ==========================================
                        // 2. CALCULAR IVA Y TOTAL
                        // ==========================================

                        decimal baseImponible =
                            subtotal - descuento;

                        if (baseImponible < 0)
                            baseImponible = 0;

                        decimal iva =
                            baseImponible * IVA;

                        decimal total =
                            baseImponible + iva;

                        if (total <= 0)
                        {
                            transaccion.Rollback();
                            return 0;
                        }

                        // ==========================================
                        // 3. INSERTAR VENTA
                        // ==========================================

                        string sqlVenta = @"
                            INSERT INTO venta
                            (
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
                                CURRENT_TIMESTAMP,
                                @subtotal,
                                @descuento,
                                @iva,
                                @total,
                                TRUE,
                                @id_cliente,
                                @id_usuario,
                                @id_caja
                            )
                            RETURNING id_venta";

                        using (NpgsqlCommand cmdVenta =
                            new NpgsqlCommand(
                                sqlVenta,
                                conexion,
                                transaccion))
                        {
                            cmdVenta.Parameters.AddWithValue(
                                "@subtotal",
                                subtotal);

                            cmdVenta.Parameters.AddWithValue(
                                "@descuento",
                                descuento);

                            cmdVenta.Parameters.AddWithValue(
                                "@iva",
                                iva);

                            cmdVenta.Parameters.AddWithValue(
                                "@total",
                                total);

                            cmdVenta.Parameters.AddWithValue(
                                "@id_cliente",
                                idCliente);

                            cmdVenta.Parameters.AddWithValue(
                                "@id_usuario",
                                idUsuario);

                            cmdVenta.Parameters.AddWithValue(
                                "@id_caja",
                                idCaja);

                            idVenta =
                                Convert.ToInt32(
                                    cmdVenta.ExecuteScalar());
                        }


                        // ==========================================
                        // 4. INSERTAR DETALLES
                        // ==========================================

                        foreach (DataRow fila in detalles.Rows)
                        {
                            int idProductoTalla =
                                Convert.ToInt32(
                                    fila["id_producto_talla"]);

                            int cantidad =
                                Convert.ToInt32(
                                    fila["cantidad"]);

                            decimal precio =
                                Convert.ToDecimal(
                                    fila["precio_unitario"]);

                            decimal subtotalDetalle =
                                precio * cantidad;


                            // ======================================
                            // COMPROBAR STOCK
                            // ======================================

                            string sqlStock = @"
                                SELECT stock_actual
                                FROM inventario
                                WHERE id_producto_talla =
                                      @id_producto_talla
                                FOR UPDATE";

                            int stockActual;

                            using (NpgsqlCommand cmdStock =
                                new NpgsqlCommand(
                                    sqlStock,
                                    conexion,
                                    transaccion))
                            {
                                cmdStock.Parameters.AddWithValue(
                                    "@id_producto_talla",
                                    idProductoTalla);

                                object resultado =
                                    cmdStock.ExecuteScalar();

                                if (resultado == null ||
                                    resultado == DBNull.Value)
                                {
                                    transaccion.Rollback();
                                    return 0;
                                }

                                stockActual =
                                    Convert.ToInt32(resultado);
                            }

                            if (cantidad > stockActual)
                            {
                                transaccion.Rollback();

                                MessageBox.Show(
                                    "La cantidad solicitada supera el stock disponible.",
                                    "Stock insuficiente",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                                return 0;
                            }


                            // ======================================
                            // INSERTAR DETALLE
                            // ======================================

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
                                    @id_venta,
                                    @id_producto_talla,
                                    @cantidad,
                                    @precio_unitario,
                                    @subtotal
                                )";

                            using (NpgsqlCommand cmdDetalle =
                                new NpgsqlCommand(
                                    sqlDetalle,
                                    conexion,
                                    transaccion))
                            {
                                cmdDetalle.Parameters.AddWithValue(
                                    "@id_venta",
                                    idVenta);

                                cmdDetalle.Parameters.AddWithValue(
                                    "@id_producto_talla",
                                    idProductoTalla);

                                cmdDetalle.Parameters.AddWithValue(
                                    "@cantidad",
                                    cantidad);

                                cmdDetalle.Parameters.AddWithValue(
                                    "@precio_unitario",
                                    precio);

                                cmdDetalle.Parameters.AddWithValue(
                                    "@subtotal",
                                    subtotalDetalle);

                                cmdDetalle.ExecuteNonQuery();
                            }


                            // ======================================
                            // DESCONTAR INVENTARIO
                            // ======================================

                            string sqlInventario = @"
                                UPDATE inventario
                                SET
                                    stock_actual =
                                        stock_actual - @cantidad,
                                    fecha_actualizacion =
                                        CURRENT_TIMESTAMP
                                WHERE id_producto_talla =
                                      @id_producto_talla";

                            using (NpgsqlCommand cmdInventario =
                                new NpgsqlCommand(
                                    sqlInventario,
                                    conexion,
                                    transaccion))
                            {
                                cmdInventario.Parameters.AddWithValue(
                                    "@cantidad",
                                    cantidad);

                                cmdInventario.Parameters.AddWithValue(
                                    "@id_producto_talla",
                                    idProductoTalla);

                                cmdInventario.ExecuteNonQuery();
                            }
                        }


                        // ==========================================
                        // 5. CONFIRMAR
                        // ==========================================

                        transaccion.Commit();

                        return idVenta;
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            transaccion.Rollback();
                        }
                        catch
                        {
                        }

                        MessageBox.Show(
                            "Error al guardar la venta:\n\n" +
                            ex.Message,
                            "Error PostgreSQL",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return 0;
                    }
                }
            }
        }


        // =========================================================
        // OBTENER DETALLE DE UNA VENTA
        // =========================================================

        public DataTable ObtenerDetalleVenta(int idVenta)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        dv.id_detalle_venta,
                        dv.id_producto_talla,
                        p.id_producto,
                        p.nombre AS producto,
                        c.nombre_categoria AS categoria,
                        m.nombre_marca AS marca,
                        pt.talla,
                        dv.cantidad,
                        dv.precio_unitario,
                        dv.subtotal
                    FROM detalle_venta dv

                    INNER JOIN producto_talla pt
                        ON dv.id_producto_talla =
                           pt.id_producto_talla

                    INNER JOIN producto p
                        ON pt.id_producto =
                           p.id_producto

                    INNER JOIN categoria c
                        ON p.id_categoria =
                           c.id_categoria

                    INNER JOIN marca m
                        ON p.id_marca =
                           m.id_marca

                    WHERE dv.id_venta = @id_venta
                    ORDER BY dv.id_detalle_venta";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_venta",
                        idVenta);

                    using (NpgsqlDataAdapter da =
                        new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            catch
            {
                return tabla;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // =========================================================
        // OBTENER UNA VENTA
        // =========================================================

        public DataTable ObtenerVenta(int idVenta)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        v.id_venta,
                        'V' ||
                        LPAD(v.id_venta::text, 5, '0')
                        AS codigo_venta,
                        v.fecha,
                        v.subtotal,
                        v.descuento,
                        v.iva,
                        v.total,
                        v.estado,
                        v.id_cliente,
                        COALESCE(
                            c.nombre,
                            'Consumidor final'
                        ) AS cliente,
                        v.id_usuario,
                        v.id_caja
                    FROM venta v
                    LEFT JOIN cliente c
                        ON v.id_cliente =
                           c.id_cliente
                    WHERE v.id_venta =
                          @id_venta";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_venta",
                        idVenta);

                    using (NpgsqlDataAdapter da =
                        new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            catch
            {
                return tabla;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // =========================================================
        // ANULAR / ELIMINAR VENTA
        // =========================================================
        //
        // No se elimina físicamente.
        // Se cambia estado = FALSE.
        // =========================================================

        public bool EliminarVenta(int idVenta)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    UPDATE venta
                    SET estado = FALSE
                    WHERE id_venta = @id_venta
                    AND estado = TRUE";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_venta",
                        idVenta);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al anular la venta:\n\n" +
                    ex.Message,
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


        // =========================================================
        // GUARDAR FORMA DE PAGO
        // =========================================================

        public bool GuardarFormaPago(
            int idVenta,
            string tipoPago,
            decimal montoCordobas,
            decimal montoDolares,
            decimal tipoCambio,
            decimal cambio,
            string tipoTarjeta,
            decimal montoTarjeta)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
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
                        @tipo_pago,
                        @monto_cordobas,
                        @monto_dolares,
                        @tipo_cambio,
                        @cambio,
                        @tipo_tarjeta,
                        @monto_tarjeta,
                        @id_venta
                    )";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@tipo_pago",
                        tipoPago);

                    cmd.Parameters.AddWithValue(
                        "@monto_cordobas",
                        montoCordobas);

                    cmd.Parameters.AddWithValue(
                        "@monto_dolares",
                        montoDolares);

                    cmd.Parameters.AddWithValue(
                        "@tipo_cambio",
                        tipoCambio);

                    cmd.Parameters.AddWithValue(
                        "@cambio",
                        cambio);

                    cmd.Parameters.AddWithValue(
                        "@tipo_tarjeta",
                        string.IsNullOrWhiteSpace(tipoTarjeta)
                            ? (object)DBNull.Value
                            : tipoTarjeta);

                    cmd.Parameters.AddWithValue(
                        "@monto_tarjeta",
                        montoTarjeta);

                    cmd.Parameters.AddWithValue(
                        "@id_venta",
                        idVenta);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al guardar el pago:\n\n" +
                    ex.Message,
                    "Error PostgreSQL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        // =========================================================
        // OBTENER FORMA DE PAGO DE UNA VENTA
        // =========================================================

        public DataTable ObtenerFormaPago(int idVenta)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_pago,
                        tipo_pago,
                        monto_cordobas,
                        monto_dolares,
                        tipo_cambio,
                        cambio,
                        tipo_tarjeta,
                        monto_tarjeta,
                        id_venta
                    FROM forma_pago
                    WHERE id_venta = @id_venta";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_venta",
                        idVenta);

                    using (NpgsqlDataAdapter da =
                        new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            catch
            {
                return tabla;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // =========================================================
        // OBTENER TOTAL DE UNA VENTA
        // =========================================================

        public decimal ObtenerTotalVenta(int idVenta)
        {
            decimal total = 0;

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT total
                    FROM venta
                    WHERE id_venta = @id_venta";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_venta",
                        idVenta);

                    object resultado =
                        cmd.ExecuteScalar();

                    if (resultado != null &&
                        resultado != DBNull.Value)
                    {
                        total =
                            Convert.ToDecimal(resultado);
                    }
                }
            }
            catch
            {
                total = 0;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return total;
        }


=

        private string ObtenerCadenaConexion()
        {
            return conexionBD.ObtenerConexion().ToString();
        }

        public string GenerarCodigoVenta()
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT COALESCE(MAX(id_venta), 0) + 1
            FROM venta";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    int siguiente =
                        Convert.ToInt32(
                            cmd.ExecuteScalar());

                    return "V" +
                           siguiente.ToString("D5");
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }
        // =========================================================
        // OBTENER PRODUCTO
        // =========================================================
        public ClaseProducto ObtenerProducto(int idProducto)
        {
            ClaseProducto producto = null;

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT
                p.id_producto,
                p.nombre,
                p.precio_venta,
                p.estado_producto,
                p.id_categoria,
                c.nombre_categoria,
                p.id_marca,
                m.nombre_marca,
                p.id_proveedor
            FROM producto p

            INNER JOIN categoria c
                ON p.id_categoria = c.id_categoria

            INNER JOIN marca m
                ON p.id_marca = m.id_marca

            WHERE p.id_producto = @id_producto";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_producto",
                        idProducto);

                    using (NpgsqlDataReader reader =
                        cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            producto = new ClaseProducto();

                            producto.IdProducto =
                                Convert.ToInt32(
                                    reader["id_producto"]);

                            producto.Nombre =
                                reader["nombre"].ToString();

                            if (reader["precio_venta"] != DBNull.Value)
                            {
                                producto.PrecioVenta =
                                    Convert.ToDecimal(
                                        reader["precio_venta"]);
                            }

                            producto.EstadoProducto =
                                Convert.ToBoolean(
                                    reader["estado_producto"]);

                            producto.IdCategoria =
                                Convert.ToInt32(
                                    reader["id_categoria"]);

                            producto.IdMarca =
                                Convert.ToInt32(
                                    reader["id_marca"]);

                            producto.IdProveedor =
                                Convert.ToInt32(
                                    reader["id_proveedor"]);

                            producto.NombreCategoria =
                                reader["nombre_categoria"].ToString();

                            producto.NombreMarca =
                                reader["nombre_marca"].ToString();
                        }
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return producto;
        }
        // =========================================================
        // CARGAR TALLAS DE UN PRODUCTO
        // =========================================================
        public DataTable CargarTallas(int idProducto)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT
                pt.id_producto_talla,
                pt.talla,
                i.stock_actual
            FROM producto_talla pt
            LEFT JOIN inventario i
                ON pt.id_producto_talla = i.id_producto_talla
            WHERE pt.id_producto = @id_producto
            ORDER BY pt.talla";

                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    sql,
                    conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@id_producto", idProducto);

                    using (NpgsqlDataAdapter da =
                        new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            catch
            {
                return tabla;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }

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
            WHERE pt.id_producto = @id_producto
            ORDER BY pt.talla";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_producto",
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
        public int ObtenerStockProductoTalla(int idProductoTalla)
        {
            int stock = 0;

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT stock_actual
            FROM inventario
            WHERE id_producto_talla = @id_producto_talla";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_producto_talla",
                        idProductoTalla);

                    object resultado =
                        cmd.ExecuteScalar();

                    if (resultado != null)
                    {
                        stock =
                            Convert.ToInt32(resultado);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return stock;
        }
        public int ObtenerStockProductoTalla(int idProductoTalla)
        {
            int stock = 0;

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT stock_actual
            FROM inventario
            WHERE id_producto_talla = @id_producto_talla";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_producto_talla",
                        idProductoTalla);

                    object resultado =
                        cmd.ExecuteScalar();

                    if (resultado != null)
                    {
                        stock =
                            Convert.ToInt32(resultado);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return stock;
        }

    }
}