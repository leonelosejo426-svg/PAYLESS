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
    internal class ReporteCompraDAO
    {

        private ConexionBD conexionBD = new ConexionBD();

        public DataTable ObtenerReporteCompras(DateTime fechaDesde, DateTime fechaHasta, bool usarFechas, string usuario, string estado)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                // Seleccionamos directamente todas las columnas que ya formatea la vista
                string sql = @"SELECT * FROM vw_reporte_compras WHERE 1 = 1";

                if (usarFechas)
                {
                    sql += " AND \"Fecha\"::date >= @fechaDesde AND \"Fecha\"::date <= @fechaHasta";
                }

                if (usuario != "Todos" && !string.IsNullOrEmpty(usuario))
                {
                    sql += " AND \"Usuario\" = @usuario";
                }

                if (estado != "Todos" && !string.IsNullOrEmpty(estado))
                {
                    sql += " AND \"Estado\" = @estado";
                }

                sql += " ORDER BY id_compra DESC;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    if (usarFechas)
                    {
                        cmd.Parameters.AddWithValue("@fechaDesde", fechaDesde.Date);
                        cmd.Parameters.AddWithValue("@fechaHasta", fechaHasta.Date);
                    }

                    if (usuario != "Todos" && !string.IsNullOrEmpty(usuario))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                    }

                    if (estado != "Todos" && !string.IsNullOrEmpty(estado))
                    {
                        cmd.Parameters.AddWithValue("@estado", estado);
                    }

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

        public DataTable ObtenerDetallesPorCompra(int idCompra)
        {
            DataTable tablaDetalle = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                // Consulta ajustada a las tablas y nombres reales de tu esquema
                string sql = @"
            SELECT 
                p.nombre AS producto,
                cat.nombre_categoria AS categoria,
                m.nombre_marca AS marca,
                pt.talla AS talla,
                dc.precio_compra AS precio_compra,
                pt.precio_venta AS precio_venta,
                dc.cantidad AS cantidad,
                dc.subtotal AS subtotal
            FROM detalle_compra dc
            JOIN producto_talla pt ON dc.id_producto_talla = pt.id_producto_talla
            JOIN producto p ON pt.id_producto = p.id_producto
            JOIN categoria cat ON p.id_categoria = cat.id_categoria
            JOIN marca m ON p.id_marca = m.id_marca
            WHERE dc.id_compra = @idCompra;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCompra", idCompra);

                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tablaDetalle);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tablaDetalle;
        }
    }
}