using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Interfaces_de_Usuario_Propuestas_Payless.Utilidades;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless.Datos
{
    internal class CompraDAO
    {
        private ConexionBD conexionBD = new ConexionBD();

        public DataTable MostrarCompras()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT
                c.id_compra,
                'C' || LPAD(c.id_compra::text, 5, '0') AS codigo_compra,
                c.fecha,
                p.nombre AS proveedor,
                c.total,
                c.estado
            FROM compra c
            INNER JOIN proveedor p ON c.id_proveedor = p.id_proveedor
            WHERE c.estado = TRUE
            ORDER BY c.id_compra DESC;";

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

        public DataTable BuscarCompraPorId(int idCompra)
        {
            DataTable tabla = new DataTable();

            using (NpgsqlConnection conexion = conexionBD.ObtenerConexion())
            {
                string sql = @"
                    SELECT
                        c.id_compra,
                        c.fecha,
                        p.nombre AS proveedor,
                        c.total,
                        c.estado
                    FROM compra c
                    INNER JOIN proveedor p ON c.id_proveedor = p.id_proveedor
                    WHERE c.id_compra = @idCompra AND c.estado = TRUE;
                ";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCompra", idCompra);
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }

            return tabla;
        }

        // =========================================================================
        // OBTENER ID DE LA CAJA ABIERTA ACTUAL
        // =========================================================================
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

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    object resultado = cmd.ExecuteScalar();
                    if (resultado != null && resultado != DBNull.Value)
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

        // =========================================================================
        // GUARDA LA COMPRA, DETALLES Y ACTUALIZA STOCK DE TALLAS EN UNA TRANSACCIÓN
        // =========================================================================
        public int RegistrarCompraTransaccional(int idProveedor, decimal total, ListaEnlazadaDetalleCompra detalles)
        {
            // 1. Obtener y validar que exista una caja abierta antes de iniciar
            int idCaja = ObtenerIdCajaAbierta();
            if (idCaja <= 0)
            {
                throw new Exception("No existe una caja abierta. No se pueden registrar compras con la caja cerrada.");
            }

            // Creamos la conexión directamente usando la contraseña explícita de tu clase ConexionBD para evitar problemas de pool
            string cadenaConexionDirecta = $"Host={conexionBD.ObtenerHost()};Port={conexionBD.ObtenerPuerto()};Database={conexionBD.ObtenerBaseDatos()};Username={conexionBD.ObtenerUsuario()};Password={conexionBD.ObtenerPassword()}";

            using (NpgsqlConnection conexion = new NpgsqlConnection(cadenaConexionDirecta))
            {
                conexion.Open();

                // Validar estrictamente en la base de datos que la caja siga abierta justo al momento de registrar
                string sqlVerificarCaja = "SELECT estado_caja FROM caja WHERE id_caja = @idCaja;";
                using (NpgsqlCommand cmdCheck = new NpgsqlCommand(sqlVerificarCaja, conexion))
                {
                    cmdCheck.Parameters.AddWithValue("@idCaja", idCaja);
                    object estadoCaja = cmdCheck.ExecuteScalar();
                    if (estadoCaja == null || estadoCaja.ToString() != "Abierta")
                    {
                        throw new Exception("La caja se encuentra cerrada. No se pueden procesar más compras en este turno.");
                    }
                }

                using (NpgsqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        // 2. Insertar Encabezado de Compra incluyendo id_caja
                        string sqlCompra = @"
                    INSERT INTO compra (id_proveedor, total, estado, fecha, id_caja)
                    VALUES (@idProveedor, @total, TRUE, CURRENT_TIMESTAMP, @idCaja)
                    RETURNING id_compra;";

                        int idCompra;
                        using (NpgsqlCommand cmdCompra = new NpgsqlCommand(sqlCompra, conexion, transaccion))
                        {
                            cmdCompra.Parameters.AddWithValue("@idProveedor", idProveedor);
                            cmdCompra.Parameters.AddWithValue("@total", total);
                            cmdCompra.Parameters.AddWithValue("@idCaja", idCaja);
                            idCompra = Convert.ToInt32(cmdCompra.ExecuteScalar());
                        }

                        NodoDetalleCompra actual = detalles.ObtenerPrimero();

                        while (actual != null)
                        {
                            // 3. Buscar si la Talla existe para este producto
                            int idProductoTalla = 0;
                            string sqlBuscarTalla = @"
                        SELECT id_producto_talla 
                        FROM producto_talla 
                        WHERE id_producto = @idProducto 
                          AND LOWER(TRIM(talla)) = LOWER(TRIM(@talla));";

                            using (NpgsqlCommand cmdBuscar = new NpgsqlCommand(sqlBuscarTalla, conexion, transaccion))
                            {
                                cmdBuscar.Parameters.AddWithValue("@idProducto", actual.IdProducto);
                                cmdBuscar.Parameters.AddWithValue("@talla", actual.Talla);
                                object res = cmdBuscar.ExecuteScalar();
                                if (res != null && res != DBNull.Value)
                                {
                                    idProductoTalla = Convert.ToInt32(res);
                                }
                            }

                            // 4. Si no existe, crear la Talla en producto_talla
                            if (idProductoTalla == 0)
                            {
                                string sqlCrearTalla = @"
                        INSERT INTO producto_talla (id_producto, talla)
                        VALUES (@idProducto, @talla)
                        RETURNING id_producto_talla;";

                                using (NpgsqlCommand cmdCrear = new NpgsqlCommand(sqlCrearTalla, conexion, transaccion))
                                {
                                    cmdCrear.Parameters.AddWithValue("@idProducto", actual.IdProducto);
                                    cmdCrear.Parameters.AddWithValue("@talla", actual.Talla);
                                    idProductoTalla = Convert.ToInt32(cmdCrear.ExecuteScalar());
                                }
                            }

                            // 5. Insertar Detalle de Compra
                            string sqlDetalle = @"
                        INSERT INTO detalle_compra (id_compra, id_producto_talla, cantidad, precio_compra, subtotal)
                        VALUES (@idCompra, @idProductoTalla, @cantidad, @precioCompra, @subtotal);";

                            using (NpgsqlCommand cmdDetalle = new NpgsqlCommand(sqlDetalle, conexion, transaccion))
                            {
                                cmdDetalle.Parameters.AddWithValue("@idCompra", idCompra);
                                cmdDetalle.Parameters.AddWithValue("@idProductoTalla", idProductoTalla);
                                cmdDetalle.Parameters.AddWithValue("@cantidad", actual.Cantidad);
                                cmdDetalle.Parameters.AddWithValue("@precioCompra", actual.PrecioCompra);
                                cmdDetalle.Parameters.AddWithValue("@subtotal", actual.Subtotal);
                                cmdDetalle.ExecuteNonQuery();
                            }

                            // 6. Actualizar Precio de Venta en la tabla 'producto_talla' si aplica
                            if (actual.PrecioVenta > 0)
                            {
                                string sqlPrecio = @"
                            UPDATE producto_talla
                            SET precio_venta = @precioVenta
                            WHERE id_producto_talla = @idProductoTalla;";

                                using (NpgsqlCommand cmdPrecio = new NpgsqlCommand(sqlPrecio, conexion, transaccion))
                                {
                                    cmdPrecio.Parameters.AddWithValue("@precioVenta", actual.PrecioVenta);
                                    cmdPrecio.Parameters.AddWithValue("@idProductoTalla", idProductoTalla);
                                    cmdPrecio.ExecuteNonQuery();
                                }
                            }

                            actual = actual.Siguiente;
                        }

                        transaccion.Commit();
                        return idCompra;
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }


        public bool EliminarCompra(int idCompra)
        {
            using (NpgsqlConnection conexion = new NpgsqlConnection(conexionBD.ObtenerConexion().ConnectionString))
            {
                string sql = @"
                    UPDATE compra
                    SET estado = FALSE
                    WHERE id_compra = @id_compra;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@id_compra", idCompra);
                    conexion.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public string GenerarCodigoCompra()
        {
            try
            {
                conexionBD.AbrirConexion();
                string sql = "SELECT COALESCE(MAX(id_compra), 0) + 1 FROM compra;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    int siguiente = Convert.ToInt32(cmd.ExecuteScalar());
                    return "C" + siguiente.ToString("D5");
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        public DataTable MostrarComprasDeCajaAbierta()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT
                c.id_compra,
                'C' || LPAD(c.id_compra::text, 5, '0') AS codigo_compra,
                c.fecha,
                p.nombre AS proveedor,
                c.total,
                c.estado
            FROM compra c
            INNER JOIN proveedor p
                ON c.id_proveedor = p.id_proveedor
            WHERE c.estado = TRUE
              AND c.id_caja = (
                  SELECT id_caja
                  FROM caja
                  WHERE estado_caja = 'Abierta'
                  ORDER BY id_caja DESC
                  LIMIT 1
              )
            ORDER BY c.id_compra DESC;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    sql, conexionBD.ObtenerConexion()))
                using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                {
                    da.Fill(tabla);
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


    }
}

