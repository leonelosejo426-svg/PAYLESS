using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless.Datos
{
    internal class ProductoDAO
    {

        private ConexionBD conexionBD = new ConexionBD();


        // =========================================================
        // MOSTRAR PRODUCTOS
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
        // CARGAR CATEGORÍAS ACTIVAS
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
        // CARGAR MARCAS ACTIVAS
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
        // CARGAR PROVEEDORES ACTIVOS
        // =========================================================

        public DataTable CargarProveedores()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_proveedor,
                        nombre
                    FROM proveedor
                    WHERE estado_proveedor = TRUE
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
        // AGREGAR PRODUCTO
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
                // INSERTAR PRODUCTO
                // -------------------------------------------------

                string sqlProducto = @"
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
                        NULL,
                        TRUE,
                        @id_categoria,
                        @id_marca,
                        @id_proveedor
                    )
                    RETURNING id_producto";

                NpgsqlCommand cmdProducto =
                    new NpgsqlCommand(
                        sqlProducto,
                        conexion);

                cmdProducto.Parameters.AddWithValue(
                    "@nombre",
                    producto.Nombre);

                cmdProducto.Parameters.AddWithValue(
                    "@id_categoria",
                    producto.IdCategoria);

                cmdProducto.Parameters.AddWithValue(
                    "@id_marca",
                    producto.IdMarca);

                cmdProducto.Parameters.AddWithValue(
                    "@id_proveedor",
                    producto.IdProveedor);


                int idProducto =
                    Convert.ToInt32(
                        cmdProducto.ExecuteScalar());


                // -------------------------------------------------
                // INSERTAR TALLA
                // -------------------------------------------------

                string sqlTalla = @"
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

                NpgsqlCommand cmdTalla =
                    new NpgsqlCommand(
                        sqlTalla,
                        conexion);

                cmdTalla.Parameters.AddWithValue(
                    "@talla",
                    talla);

                cmdTalla.Parameters.AddWithValue(
                    "@id_producto",
                    idProducto);


                int idProductoTalla =
                    Convert.ToInt32(
                        cmdTalla.ExecuteScalar());


                // -------------------------------------------------
                // INSERTAR INVENTARIO
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
                        @id_producto_talla
                    )";

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
                        id_producto_talla,
                        talla
                    FROM producto_talla
                    WHERE id_producto = @id_producto
                    ORDER BY talla";

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
        // ELIMINAR PRODUCTO
        // =========================================================

        public bool EliminarProducto(int idProducto)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    UPDATE producto
                    SET estado_producto = FALSE
                    WHERE id_producto = @id_producto";

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
        // BUSCAR POR NOMBRE
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
                        ON p.id_categoria = c.id_categoria

                    INNER JOIN marca m
                        ON p.id_marca = m.id_marca

                    INNER JOIN proveedor pr
                        ON p.id_proveedor = pr.id_proveedor

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
        // BUSCAR POR CATEGORÍA
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
                        ON p.id_categoria = c.id_categoria

                    INNER JOIN marca m
                        ON p.id_marca = m.id_marca

                    INNER JOIN proveedor pr
                        ON p.id_proveedor = pr.id_proveedor

                    WHERE p.id_categoria = @id_categoria

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
        // BUSCAR POR MARCA
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
                        ON p.id_categoria = c.id_categoria

                    INNER JOIN marca m
                        ON p.id_marca = m.id_marca

                    INNER JOIN proveedor pr
                        ON p.id_proveedor = pr.id_proveedor

                    WHERE p.id_marca = @id_marca

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
        // BUSCAR POR PROVEEDOR
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
                        ON p.id_categoria = c.id_categoria

                    INNER JOIN marca m
                        ON p.id_marca = m.id_marca

                    INNER JOIN proveedor pr
                        ON p.id_proveedor = pr.id_proveedor

                    WHERE p.id_proveedor = @id_proveedor

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
        // BUSCAR POR ESTADO
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
                        ON p.id_categoria = c.id_categoria

                    INNER JOIN marca m
                        ON p.id_marca = m.id_marca

                    INNER JOIN proveedor pr
                        ON p.id_proveedor = pr.id_proveedor

                    WHERE p.estado_producto = @estado

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
