using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public class ProductoDAO
    {
        private ConexionBD conexionBD = new ConexionBD();

        // =========================================================
        // 1. MOSTRAR PRODUCTOS
        // =========================================================

        public DataTable MostrarProductos()
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
                        p.estado_producto,
                        c.nombre_categoria AS categoria,
                        m.nombre_marca AS marca,
                        pr.nombre AS proveedor
                    FROM producto p

                    INNER JOIN categoria c
                        ON p.id_categoria = c.id_categoria

                    INNER JOIN marca m
                        ON p.id_marca = m.id_marca

                    INNER JOIN proveedor pr
                        ON p.id_proveedor = pr.id_proveedor

                    ORDER BY p.nombre";

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(
                        sql,
                        conexionBD.ObtenerConexion());

                da.Fill(tabla);
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
        // 2. CARGAR PRODUCTOS PARA COMBOBOX
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

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(
                        sql,
                        conexionBD.ObtenerConexion());

                da.Fill(tabla);
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
        // 3. CARGAR TALLAS DE UN PRODUCTO
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
                    WHERE pt.id_producto = @id_producto
                    AND p.estado_producto = TRUE
                    ORDER BY pt.talla";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id_producto",
                    idProducto);

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(cmd);

                da.Fill(tabla);
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
        // 4. AGREGAR PRODUCTO
        // =========================================================

        public bool AgregarProducto(
            ClaseProducto producto,
            string talla,
            int cantidad,
            int stockMinimo)
        {
            try
            {
                conexionBD.AbrirConexion();

                NpgsqlConnection conexion =
                    conexionBD.ObtenerConexion();

                // -------------------------------------------------
                // BUSCAR SI EL PRODUCTO YA EXISTE
                // -------------------------------------------------

                string sqlBuscarProducto = @"
                    SELECT id_producto
                    FROM producto
                    WHERE nombre = @nombre
                    AND id_categoria = @id_categoria
                    AND id_marca = @id_marca
                    AND id_proveedor = @id_proveedor
                    AND estado_producto = TRUE";

                NpgsqlCommand cmdBuscarProducto =
                    new NpgsqlCommand(
                        sqlBuscarProducto,
                        conexion);

                cmdBuscarProducto.Parameters.AddWithValue(
                    "@nombre",
                    producto.Nombre);

                cmdBuscarProducto.Parameters.AddWithValue(
                    "@id_categoria",
                    producto.IdCategoria);

                cmdBuscarProducto.Parameters.AddWithValue(
                    "@id_marca",
                    producto.IdMarca);

                cmdBuscarProducto.Parameters.AddWithValue(
                    "@id_proveedor",
                    producto.IdProveedor);

                object resultado =
                    cmdBuscarProducto.ExecuteScalar();

                int idProducto;


                // -------------------------------------------------
                // SI EL PRODUCTO YA EXISTE
                // -------------------------------------------------

                if (resultado != null)
                {
                    idProducto =
                        Convert.ToInt32(resultado);
                }

                // -------------------------------------------------
                // SI EL PRODUCTO NO EXISTE
                // -------------------------------------------------

                else
                {
                    string sqlInsertarProducto = @"
                        INSERT INTO producto
                        (
                            nombre,
                            precio_venta,
                            estado_producto,
                            id_categoria,
                            id_marca,
                            id_proveedor
                        )
                        VALUES
                        (
                            @nombre,
                            @precio_venta,
                            TRUE,
                            @id_categoria,
                            @id_marca,
                            @id_proveedor
                        )
                        RETURNING id_producto";

                    NpgsqlCommand cmdInsertar =
                        new NpgsqlCommand(
                            sqlInsertarProducto,
                            conexion);

                    cmdInsertar.Parameters.AddWithValue(
                        "@nombre",
                        producto.Nombre);

                    cmdInsertar.Parameters.AddWithValue(
                        "@precio_venta",
                        producto.PrecioVenta);

                    cmdInsertar.Parameters.AddWithValue(
                        "@id_categoria",
                        producto.IdCategoria);

                    cmdInsertar.Parameters.AddWithValue(
                        "@id_marca",
                        producto.IdMarca);

                    cmdInsertar.Parameters.AddWithValue(
                        "@id_proveedor",
                        producto.IdProveedor);

                    idProducto =
                        Convert.ToInt32(
                            cmdInsertar.ExecuteScalar());
                }


                // -------------------------------------------------
                // BUSCAR SI YA EXISTE LA TALLA
                // -------------------------------------------------

                string sqlBuscarTalla = @"
                    SELECT id_producto_talla
                    FROM producto_talla
                    WHERE id_producto = @id_producto
                    AND talla = @talla";

                NpgsqlCommand cmdBuscarTalla =
                    new NpgsqlCommand(
                        sqlBuscarTalla,
                        conexion);

                cmdBuscarTalla.Parameters.AddWithValue(
                    "@id_producto",
                    idProducto);

                cmdBuscarTalla.Parameters.AddWithValue(
                    "@talla",
                    talla);

                object resultadoTalla =
                    cmdBuscarTalla.ExecuteScalar();


                // -------------------------------------------------
                // SI LA TALLA YA EXISTE → SUMAR STOCK
                // -------------------------------------------------

                if (resultadoTalla != null)
                {
                    int idProductoTalla =
                        Convert.ToInt32(resultadoTalla);

                    string sqlActualizarStock = @"
                        UPDATE inventario
                        SET
                            stock_actual =
                                stock_actual + @cantidad,
                            stock_minimo =
                                @stock_minimo,
                            fecha_actualizacion =
                                CURRENT_TIMESTAMP
                        WHERE id_producto_talla =
                              @id_producto_talla";

                    NpgsqlCommand cmdStock =
                        new NpgsqlCommand(
                            sqlActualizarStock,
                            conexion);

                    cmdStock.Parameters.AddWithValue(
                        "@cantidad",
                        cantidad);

                    cmdStock.Parameters.AddWithValue(
                        "@stock_minimo",
                        stockMinimo);

                    cmdStock.Parameters.AddWithValue(
                        "@id_producto_talla",
                        idProductoTalla);

                    cmdStock.ExecuteNonQuery();
                }

                // -------------------------------------------------
                // SI LA TALLA NO EXISTE → CREAR TALLA
                // -------------------------------------------------

                else
                {
                    string sqlNuevaTalla = @"
                        INSERT INTO producto_talla
                        (
                            talla,
                            id_producto
                        )
                        VALUES
                        (
                            @talla,
                            @id_producto
                        )
                        RETURNING id_producto_talla";

                    NpgsqlCommand cmdNuevaTalla =
                        new NpgsqlCommand(
                            sqlNuevaTalla,
                            conexion);

                    cmdNuevaTalla.Parameters.AddWithValue(
                        "@talla",
                        talla);

                    cmdNuevaTalla.Parameters.AddWithValue(
                        "@id_producto",
                        idProducto);

                    int idProductoTalla =
                        Convert.ToInt32(
                            cmdNuevaTalla.ExecuteScalar());


                    // -------------------------------------------------
                    // CREAR INVENTARIO PARA LA NUEVA TALLA
                    // -------------------------------------------------

                    string sqlInventario = @"
                        INSERT INTO inventario
                        (
                            stock_actual,
                            stock_minimo,
                            fecha_actualizacion,
                            id_producto_talla
                        )
                        VALUES
                        (
                            @stock_actual,
                            @stock_minimo,
                            CURRENT_TIMESTAMP,
                            @id_producto_talla)";

                    NpgsqlCommand cmdInventario =
                        new NpgsqlCommand(
                            sqlInventario,
                            conexion);

                    cmdInventario.Parameters.AddWithValue(
                        "@stock_actual",
                        cantidad);

                    cmdInventario.Parameters.AddWithValue(
                        "@stock_minimo",
                        stockMinimo);

                    cmdInventario.Parameters.AddWithValue(
                        "@id_producto_talla",
                        idProductoTalla);

                    cmdInventario.ExecuteNonQuery();
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        // =========================================================
        // 5. OBTENER PRODUCTO + TALLA PARA EDITAR
        // =========================================================

        public DataTable ObtenerProductoTalla(
            int idProducto,
            int idProductoTalla)
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
                        p.estado_producto,
                        p.id_categoria,
                        p.id_marca,
                        p.id_proveedor,

                        pt.id_producto_talla,
                        pt.talla,

                        i.id_inventario,
                        i.stock_actual,
                        i.stock_minimo,
                        i.fecha_actualizacion

                    FROM producto p

                    INNER JOIN producto_talla pt
                        ON p.id_producto =
                           pt.id_producto

                    INNER JOIN inventario i
                        ON pt.id_producto_talla =
                           i.id_producto_talla

                    WHERE p.id_producto =
                          @id_producto

                    AND pt.id_producto_talla =
                        @id_producto_talla";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id_producto",
                    idProducto);

                cmd.Parameters.AddWithValue(
                    "@id_producto_talla",
                    idProductoTalla);

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(cmd);

                da.Fill(tabla);
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
        // 6. EDITAR PRODUCTO
        // =========================================================

        public bool EditarProducto(
            ClaseProducto producto,
            int idProductoTalla,
            string talla,
            int cantidad,
            int stockMinimo)
        {
            try
            {
                conexionBD.AbrirConexion();

                NpgsqlConnection conexion =
                    conexionBD.ObtenerConexion();


                // -------------------------------------------------
                // ACTUALIZAR PRODUCTO
                // -------------------------------------------------

                string sqlProducto = @"
                    UPDATE producto
                    SET
                        nombre = @nombre,
                        precio_venta = @precio_venta,
                        estado_producto = @estado_producto,
                        id_categoria = @id_categoria,
                        id_marca = @id_marca,
                        id_proveedor = @id_proveedor
                    WHERE id_producto =
                          @id_producto";

                NpgsqlCommand cmdProducto =
                    new NpgsqlCommand(
                        sqlProducto,
                        conexion);

                cmdProducto.Parameters.AddWithValue(
                    "@nombre",
                    producto.Nombre);

                cmdProducto.Parameters.AddWithValue(
                    "@precio_venta",
                    producto.PrecioVenta);

                cmdProducto.Parameters.AddWithValue(
                    "@estado_producto",
                    producto.EstadoProducto);

                cmdProducto.Parameters.AddWithValue(
                    "@id_categoria",
                    producto.IdCategoria);

                cmdProducto.Parameters.AddWithValue(
                    "@id_marca",
                    producto.IdMarca);

                cmdProducto.Parameters.AddWithValue(
                    "@id_proveedor",
                    producto.IdProveedor);

                cmdProducto.Parameters.AddWithValue(
                    "@id_producto",
                    producto.IdProducto);

                cmdProducto.ExecuteNonQuery();


                // -------------------------------------------------
                // ACTUALIZAR TALLA
                // -------------------------------------------------

                string sqlTalla = @"
                    UPDATE producto_talla
                    SET
                        talla = @talla
                    WHERE id_producto_talla =
                          @id_producto_talla";

                NpgsqlCommand cmdTalla =
                    new NpgsqlCommand(
                        sqlTalla,
                        conexion);

                cmdTalla.Parameters.AddWithValue(
                    "@talla",
                    talla);

                cmdTalla.Parameters.AddWithValue(
                    "@id_producto_talla",
                    idProductoTalla);

                cmdTalla.ExecuteNonQuery();


                // -------------------------------------------------
                // ACTUALIZAR INVENTARIO
                // -------------------------------------------------

                string sqlInventario = @"
                    UPDATE inventario
                    SET
                        stock_actual = @cantidad,
                        stock_minimo = @stock_minimo,
                        fecha_actualizacion =
                            CURRENT_TIMESTAMP
                    WHERE id_producto_talla =
                          @id_producto_talla";

                NpgsqlCommand cmdInventario =
                    new NpgsqlCommand(
                        sqlInventario,
                        conexion);

                cmdInventario.Parameters.AddWithValue(
                    "@cantidad",
                    cantidad);

                cmdInventario.Parameters.AddWithValue(
                    "@stock_minimo",
                    stockMinimo);

                cmdInventario.Parameters.AddWithValue(
                    "@id_producto_talla",
                    idProductoTalla);

                cmdInventario.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        // =========================================================
        // 7. ELIMINAR PRODUCTO
        // =========================================================

        public bool EliminarProducto(int idProducto)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    UPDATE producto
                    SET estado_producto = FALSE
                    WHERE id_producto =
                          @id_producto";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id_producto",
                    idProducto);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        // =========================================================
        // 8. BUSCAR POR CÓDIGO / ID
        // =========================================================

        public DataTable BuscarPorCodigo(int idProducto)
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
                        p.estado_producto,
                        c.nombre_categoria AS categoria,
                        m.nombre_marca AS marca,
                        pr.nombre AS proveedor
                    FROM producto p

                    INNER JOIN categoria c
                        ON p.id_categoria =
                           c.id_categoria

                    INNER JOIN marca m
                        ON p.id_marca =
                           m.id_marca

                    INNER JOIN proveedor pr
                        ON p.id_proveedor =
                           pr.id_proveedor

                    WHERE p.id_producto =
                          @id_producto";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id_producto",
                    idProducto);

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(cmd);

                da.Fill(tabla);
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
        // 9. BUSCAR POR NOMBRE
        // =========================================================

        public DataTable BuscarPorNombre(string nombre)
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
                        p.estado_producto,
                        c.nombre_categoria AS categoria,
                        m.nombre_marca AS marca,
                        pr.nombre AS proveedor
                    FROM producto p

                    INNER JOIN categoria c
                        ON p.id_categoria =
                           c.id_categoria

                    INNER JOIN marca m
                        ON p.id_marca =
                           m.id_marca

                    INNER JOIN proveedor pr
                        ON p.id_proveedor =
                           pr.id_proveedor

                    WHERE p.nombre ILIKE @nombre
                    ORDER BY p.nombre";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@nombre",
                    "%" + nombre + "%");

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(cmd);

                da.Fill(tabla);
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
        // 10. BUSCAR POR CATEGORÍA
        // =========================================================

        public DataTable BuscarPorCategoria(int idCategoria)
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
                        p.estado_producto,
                        c.nombre_categoria AS categoria,
                        m.nombre_marca AS marca,
                        pr.nombre AS proveedor
                    FROM producto p

                    INNER JOIN categoria c
                        ON p.id_categoria =
                           c.id_categoria

                    INNER JOIN marca m
                        ON p.id_marca =
                           m.id_marca

                    INNER JOIN proveedor pr
                        ON p.id_proveedor =
                           pr.id_proveedor

                    WHERE p.id_categoria =
                          @id_categoria
                    ORDER BY p.nombre";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id_categoria",
                    idCategoria);

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(cmd);

                da.Fill(tabla);
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
        // 11. BUSCAR POR MARCA
        // =========================================================

        public DataTable BuscarPorMarca(int idMarca)
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
                        p.estado_producto,
                        c.nombre_categoria AS categoria,
                        m.nombre_marca AS marca,
                        pr.nombre AS proveedor
                    FROM producto p

                    INNER JOIN categoria c
                        ON p.id_categoria =
                           c.id_categoria

                    INNER JOIN marca m
                        ON p.id_marca =
                           m.id_marca

                    INNER JOIN proveedor pr
                        ON p.id_proveedor =
                           pr.id_proveedor

                    WHERE p.id_marca =
                          @id_marca
                    ORDER BY p.nombre";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id_marca",
                    idMarca);

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(cmd);

                da.Fill(tabla);
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
        // 12. BUSCAR POR PROVEEDOR
        // =========================================================

        public DataTable BuscarPorProveedor(int idProveedor)
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
                        p.estado_producto,
                        c.nombre_categoria AS categoria,
                        m.nombre_marca AS marca,
                        pr.nombre AS proveedor
                    FROM producto p

                    INNER JOIN categoria c
                        ON p.id_categoria =
                           c.id_categoria

                    INNER JOIN marca m
                        ON p.id_marca =
                           m.id_marca

                    INNER JOIN proveedor pr
                        ON p.id_proveedor =
                           pr.id_proveedor

                    WHERE p.id_proveedor =
                          @id_proveedor
                    ORDER BY p.nombre";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id_proveedor",
                    idProveedor);

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(cmd);

                da.Fill(tabla);
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
        // 13. BUSCAR POR ESTADO
        // =========================================================

        public DataTable BuscarPorEstado(bool estado)
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
                        p.estado_producto,
                        c.nombre_categoria AS categoria,
                        m.nombre_marca AS marca,
                        pr.nombre AS proveedor
                    FROM producto p

                    INNER JOIN categoria c
                        ON p.id_categoria =
                           c.id_categoria

                    INNER JOIN marca m
                        ON p.id_marca =
                           m.id_marca

                    INNER JOIN proveedor pr
                        ON p.id_proveedor =
                           pr.id_proveedor

                    WHERE p.estado_producto =
                          @estado
                    ORDER BY p.nombre";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@estado",
                    estado);

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(cmd);

                da.Fill(tabla);
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

    }


}
