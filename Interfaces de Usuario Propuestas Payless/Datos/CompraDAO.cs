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
        // GUARDA LA COMPRA, DETALLES Y ACTUALIZA STOCK DE TALLAS EN UNA TRANSACCIÓN
        // =========================================================================
        public int RegistrarCompraTransaccional(int idProveedor, decimal total, ListaEnlazadaDetalleCompra detalles)
        {
            using (NpgsqlConnection conexion = new NpgsqlConnection(conexionBD.ObtenerConexion().ConnectionString))
            {
                conexion.Open();
                using (NpgsqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        // 1. Insertar Encabezado de Compra
                        string sqlCompra = @"
                            INSERT INTO compra (id_proveedor, total, estado, fecha)
                            VALUES (@idProveedor, @total, TRUE, CURRENT_TIMESTAMP)
                            RETURNING id_compra;";

                        int idCompra;
                        using (NpgsqlCommand cmdCompra = new NpgsqlCommand(sqlCompra, conexion, transaccion))
                        {
                            cmdCompra.Parameters.AddWithValue("@idProveedor", idProveedor);
                            cmdCompra.Parameters.AddWithValue("@total", total);
                            idCompra = Convert.ToInt32(cmdCompra.ExecuteScalar());
                        }

                        NodoDetalleCompra actual = detalles.ObtenerPrimero();

                        while (actual != null)
                        {
                            // 2. Buscar si la Talla existe para este producto
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

                            // 3. Si no existe, crear la Talla en producto_talla (solo id_producto y talla)
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

                            // 4. Insertar Detalle de Compra
                            // El Trigger 'trg_actualizar_inventario_compra' de PostgreSQL 
                            // actualizará o creará automáticamente el registro en la tabla 'inventario'.
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

                            // 5. Actualizar Precio de Venta en la tabla 'producto' si aplica
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
    }
}

