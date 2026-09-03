using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    p.precio_venta,
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
                INNER JOIN inventario i ON pt.id_producto_talla = i.id_producto_talla
                WHERE pt.id_producto = @idProducto
                  AND i.stock_actual > 0
                ORDER BY pt.talla;";

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

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idProductoTalla", idProductoTalla);

                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
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

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);

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

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    object resultado = cmd.ExecuteScalar();

                    if (resultado == null || resultado == DBNull.Value)
                    {
                        return 0;
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

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    int siguiente = Convert.ToInt32(cmd.ExecuteScalar());

                    return "V" + siguiente.ToString("D5");
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }
    }
}
