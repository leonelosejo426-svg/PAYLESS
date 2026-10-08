using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless.Conexion
{
    internal class ReporteVentaDAO
    {

        private ConexionBD conexionBD = new ConexionBD();

        public DataTable ObtenerReporteVentas(DateTime desde, DateTime hasta, bool usarFechas, string usuario, string estado)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        v.id_venta AS ""ID Venta"",
                        v.codigo_venta AS ""Código"",
                        v.fecha AS ""Fecha"",
                        COALESCE(c.nombre, 'Consumidor Final') AS ""Cliente"",
                        u.nombre_usuario AS ""Usuario"",
                        v.subtotal AS ""Subtotal"",
                        v.iva AS ""IVA"",
                        v.total AS ""Total"",
                        CASE 
                            WHEN v.estado = TRUE THEN 'Activa' 
                            ELSE 'Anulada' 
                        END AS ""Estado""
                    FROM venta v
                    LEFT JOIN cliente c ON v.id_cliente = c.id_cliente
                    INNER JOIN usuario u ON v.id_usuario = u.id_usuario
                    WHERE
                        (@usarFechas = FALSE OR v.fecha::date BETWEEN @desde::date AND @hasta::date)
                        AND (@usuario = 'Todos' OR u.nombre_usuario = @usuario)
                        AND (
                            @estado = 'Todos' 
                            OR (@estado = 'Activas' AND v.estado = TRUE)
                            OR (@estado = 'Anuladas' AND v.estado = FALSE)
                        )
                    ORDER BY v.id_venta DESC;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@usarFechas", usarFechas);
                    cmd.Parameters.AddWithValue("@desde", desde);
                    cmd.Parameters.AddWithValue("@hasta", hasta);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
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
