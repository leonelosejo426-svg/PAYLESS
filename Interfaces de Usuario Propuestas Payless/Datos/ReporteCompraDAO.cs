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

                string sql = @"
                    SELECT 
                        id_compra AS ""ID Compra"",
                        codigo_compra AS ""Código"",
                        fecha AS ""Fecha"",
                        proveedor AS ""Proveedor"",
                        usuario AS ""Usuario"",
                        total AS ""Total"",
                        estado_texto AS ""Estado""
                    FROM vw_reporte_compras
                    WHERE 1 = 1";

                if (usarFechas)
                {
                    sql += " AND fecha::date >= @fechaDesde AND fecha::date <= @fechaHasta";
                }

                if (usuario != "Todos" && !string.IsNullOrEmpty(usuario))
                {
                    sql += " AND usuario = @usuario";
                }

                if (estado != "Todos" && !string.IsNullOrEmpty(estado))
                {
                    sql += " AND estado_texto = @estado";
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

    }
}
