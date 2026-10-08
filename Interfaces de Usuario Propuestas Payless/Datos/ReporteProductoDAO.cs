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
    internal class ReporteProductoDAO
    {

        private ConexionBD conexionBD = new ConexionBD();

        public DataTable ObtenerReporteProductos(string estado)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        ""ID Producto"",
                        ""Producto"",
                        ""Categoría"",
                        ""Marca"",
                        ""Proveedor"",
                        ""Talla"",
                        ""Precio Venta"",
                        ""Stock"",
                        ""Estado""
                    FROM vw_reporte_productos
                    WHERE
                        @estado = 'Todos'
                        OR ""Estado"" = @estado
                    ORDER BY ""Producto"", ""Talla"";
                ";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@estado", estado);

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
    }
}
