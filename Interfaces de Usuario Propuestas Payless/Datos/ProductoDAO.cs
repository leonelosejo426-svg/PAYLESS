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
                nombre_marca,
                estado
            FROM marca
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
                nombre,
                estado_proveedor
            FROM proveedor
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

        public bool AgregarProducto(ClaseProducto producto)
        {
            NpgsqlTransaction transaccion = null;

            try
            {
                conexionBD.AbrirConexion();
                NpgsqlConnection conexion = conexionBD.ObtenerConexion();

                // Iniciamos la transacción
                transaccion = conexion.BeginTransaction();

                // ==========================================
                // 1. BUSCAR SI EL PRODUCTO YA EXISTE
                // ==========================================
                string sqlBuscarProducto = @"
            SELECT id_producto
            FROM producto
            WHERE nombre = @nombre
            AND id_categoria = @id_categoria
            AND id_marca = @id_marca
            AND id_proveedor = @id_proveedor
            AND estado_producto = TRUE";

                NpgsqlCommand cmdBuscarProducto = new NpgsqlCommand(sqlBuscarProducto, conexion, transaccion);
                cmdBuscarProducto.Parameters.AddWithValue("@nombre", producto.Nombre);
                cmdBuscarProducto.Parameters.AddWithValue("@id_categoria", producto.IdCategoria);
                cmdBuscarProducto.Parameters.AddWithValue("@id_marca", producto.IdMarca);
                cmdBuscarProducto.Parameters.AddWithValue("@id_proveedor", producto.IdProveedor);

                object resultado = cmdBuscarProducto.ExecuteScalar();

                // ==========================================
                // 2. SI EL PRODUCTO YA EXISTE
                // ==========================================
                if (resultado != null)
                {
                    MessageBox.Show(
                        "El producto ya se encuentra registrado.",
                        "Producto existente",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    transaccion.Rollback();
                    return false;
                }

                // ==========================================
                // 3. INSERTAR NUEVO PRODUCTO
                // ==========================================
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
                NULL,
                TRUE,
                @id_categoria,
                @id_marca,
                @id_proveedor
            )";

                NpgsqlCommand cmdInsertar = new NpgsqlCommand(sqlInsertarProducto, conexion, transaccion);
                cmdInsertar.Parameters.AddWithValue("@nombre", producto.Nombre);
                cmdInsertar.Parameters.AddWithValue("@id_categoria", producto.IdCategoria);
                cmdInsertar.Parameters.AddWithValue("@id_marca", producto.IdMarca);
                cmdInsertar.Parameters.AddWithValue("@id_proveedor", producto.IdProveedor);

                cmdInsertar.ExecuteNonQuery();

                // Confirmamos los cambios en la BD
                transaccion.Commit();
                return true;
            }
            catch (Exception ex)
            {
                // Si hay error, deshacemos los cambios
                if (transaccion != null)
                {
                    transaccion.Rollback();
                }

                MessageBox.Show(
                    "Error al guardar producto:\n\n" + ex.Message,
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
            DataTable dt = new DataTable();

            try
            {
                conexionBD.AbrirConexion();
                NpgsqlConnection conexion = conexionBD.ObtenerConexion();

                // Usamos ILIKE para que ignore mayúsculas/minúsculas y espacios
                string sql = @"
            SELECT 
                p.id_producto,
                p.nombre,
                c.nombre_categoria AS categoria,
                m.nombre_marca AS marca,
                pr.nombre AS proveedor,
                p.id_producto AS codigo,
                p.id_categoria,
                p.id_marca,
                p.id_proveedor
            FROM producto p
            LEFT JOIN categoria c ON p.id_categoria = c.id_categoria
            LEFT JOIN marca m ON p.id_marca = m.id_marca
            LEFT JOIN proveedor pr ON p.id_proveedor = pr.id_proveedor
            WHERE p.nombre ILIKE @nombre
            AND p.estado_producto = TRUE";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexion))
                {
                    // Búsqueda flexible con % para que coincida aunque el usuario escriba parte del nombre
                    cmd.Parameters.AddWithValue("@nombre", "%" + nombre.Trim() + "%");

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar producto: " + ex.Message, "Error BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return dt;
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


        // =========================================================
        // OBTENER PRODUCTO PARA EDITAR
        // =========================================================

        public ClaseProducto ObtenerProducto(int idProducto)
        {
            ClaseProducto producto = new ClaseProducto();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT
                id_producto,
                nombre,
                precio_venta,
                estado_producto,
                id_categoria,
                id_marca,
                id_proveedor
            FROM producto
            WHERE id_producto = @id_producto";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id_producto",
                    idProducto);

                NpgsqlDataReader reader =
                    cmd.ExecuteReader();

                if (reader.Read())
                {
                    producto.IdProducto =
                        Convert.ToInt32(reader["id_producto"]);

                    producto.Nombre =
                        reader["nombre"].ToString();

                    // Se conserva de la BD,
                    // aunque no se edite desde el formulario.
                    if (reader["precio_venta"] != DBNull.Value)
                    {
                        producto.PrecioVenta =
                            Convert.ToDecimal(reader["precio_venta"]);
                    }

                    producto.EstadoProducto =
                        Convert.ToBoolean(reader["estado_producto"]);

                    producto.IdCategoria =
                        Convert.ToInt32(reader["id_categoria"]);

                    producto.IdMarca =
                        Convert.ToInt32(reader["id_marca"]);

                    producto.IdProveedor =
                        Convert.ToInt32(reader["id_proveedor"]);
                }
            }
            catch
            {
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return producto;
        }

        // =========================================================
        // EDITAR PRODUCTO
        // =========================================================

        public bool EditarProducto(ClaseProducto producto)
        {
            NpgsqlTransaction transaccion = null;

            try
            {
                conexionBD.AbrirConexion();
                NpgsqlConnection conexion = conexionBD.ObtenerConexion();

                // Iniciamos la transacción para asegurar la operación
                transaccion = conexion.BeginTransaction();

                // =====================================================
                // ACTUALIZAR PRODUCTO
                // =====================================================
                string sqlProducto = @"
            UPDATE producto
            SET
                nombre = @nombre,
                id_categoria = @id_categoria,
                id_marca = @id_marca,
                id_proveedor = @id_proveedor
            WHERE id_producto = @id_producto";

                using (NpgsqlCommand cmdProducto = new NpgsqlCommand(sqlProducto, conexion, transaccion))
                {
                    cmdProducto.Parameters.AddWithValue("@nombre", producto.Nombre);
                    cmdProducto.Parameters.AddWithValue("@id_categoria", producto.IdCategoria);
                    cmdProducto.Parameters.AddWithValue("@id_marca", producto.IdMarca);
                    cmdProducto.Parameters.AddWithValue("@id_proveedor", producto.IdProveedor);
                    cmdProducto.Parameters.AddWithValue("@id_producto", producto.IdProducto);

                    int filasAfectadas = cmdProducto.ExecuteNonQuery();

                    if (filasAfectadas == 0)
                    {
                        // Si no se actualizó ninguna fila, el producto no existía
                        transaccion.Rollback();
                        return false;
                    }
                }

                // Confirmar cambios
                transaccion.Commit();
                return true;
            }
            catch (Exception ex)
            {
                // Si ocurrió un error, deshacer cambios
                if (transaccion != null)
                {
                    transaccion.Rollback();
                }

                MessageBox.Show(
                    "Error al editar producto:\n\n" + ex.Message,
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

        public DataTable ObtenerProductoTalla(int idProducto, int idProductoTalla)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT
                pt.id_producto_talla,
                pt.talla,
                i.stock_actual,
                i.stock_minimo
            FROM producto_talla pt
            INNER JOIN inventario i
                ON pt.id_producto_talla = i.id_producto_talla
            WHERE pt.id_producto = @id_producto
              AND pt.id_producto_talla = @id_producto_talla";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
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

        public DataTable MostrarTallasPorProducto(int idProducto)
        {
            DataTable tabla = new DataTable();

            using (NpgsqlConnection conexion = conexionBD.ObtenerConexion())
            {
                string sql = @"
            SELECT
                pt.id_producto_talla,
                pt.talla
            FROM producto_talla pt
            WHERE pt.id_producto = @idProducto
            ORDER BY pt.talla;
        ";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue(
                        "@idProducto", idProducto);

                    using (NpgsqlDataAdapter da =
                        new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }

            return tabla;
        }

        public DataTable MostrarProductosParaCompra()
        {
            DataTable tabla = new DataTable();

            using (NpgsqlConnection conexion = new NpgsqlConnection(conexionBD.ObtenerConexion().ConnectionString))
            {
                string sql = @"
            SELECT 
                p.id_producto,
                p.nombre,
                c.nombre_categoria,
                m.nombre_marca,
                p.precio_venta
            FROM producto p
            INNER JOIN categoria c ON p.id_categoria = c.id_categoria
            INNER JOIN marca m ON p.id_marca = m.id_marca
            ORDER BY p.nombre ASC;";

                using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conexion))
                {
                    da.Fill(tabla);
                }
            }

            return tabla;
        }


        public DataTable MostrarProductosPorProveedor(int idProveedor)
        {
            DataTable tabla = new DataTable();

            using (NpgsqlConnection conexion = new NpgsqlConnection(conexionBD.ObtenerConexion().ConnectionString))
            {
                string sql = @"
            SELECT 
                p.id_producto,
                p.nombre,
                c.nombre_categoria,
                m.nombre_marca
            FROM producto p
            INNER JOIN categoria c ON p.id_categoria = c.id_categoria
            INNER JOIN marca m ON p.id_marca = m.id_marca
            WHERE p.id_proveedor = @idProveedor
            ORDER BY p.nombre ASC;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }

            return tabla;
        }

        // =========================================
        // MOSTRAR TALLAS POR PRODUCTO
        // =========================================
        public DataTable MostrarTallasPorProductoC(int idProducto)
        {
            DataTable tabla = new DataTable();

            using (NpgsqlConnection conexion = new NpgsqlConnection(conexionBD.ObtenerConexion().ConnectionString))
            {
                string sql = @"
                    SELECT 
                        id_producto_talla, 
                        talla 
                    FROM producto_talla 
                    WHERE id_producto = @idProducto
                    ORDER BY talla ASC;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);

                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }

            return tabla;
        }



    }



}
