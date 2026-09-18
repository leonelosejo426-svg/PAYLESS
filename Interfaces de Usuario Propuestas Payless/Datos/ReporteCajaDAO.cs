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
    internal class ReporteCajaDAO
    {

        private ConexionBD conexionBD = new ConexionBD();

        public DataTable ObtenerReporteCaja(
            DateTime fechaDesde,
            DateTime fechaHasta,
            bool usarFechas,
            string usuario,
            string estado)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        c.id_caja AS ""ID Caja"",
                        u.nombre_usuario AS ""Usuario"",
                        c.fecha_apertura AS ""Fecha Apertura"",
                        c.fecha_cierre AS ""Fecha Cierre"",
                        c.saldo_inicial AS ""Saldo Inicial"",

                        COALESCE((
                            SELECT SUM(
                                COALESCE(fp.monto_cordobas, 0) +
                                (
                                    COALESCE(fp.monto_dolares, 0)
                                    * COALESCE(fp.tipo_cambio, 0)
                                )
                            )
                            FROM venta v
                            INNER JOIN forma_pago fp
                                ON v.id_venta = fp.id_venta
                            WHERE v.id_caja = c.id_caja
                              AND v.estado = TRUE
                              AND fp.tipo_pago <> 'Tarjeta'
                        ), 0) AS ""Ingresos"",

                        COALESCE((
                            SELECT SUM(e.monto)
                            FROM egreso_caja e
                            WHERE e.id_caja = c.id_caja
                        ), 0) AS ""Egresos"",

                        c.monto_esperado AS ""Monto Esperado"",
                        c.monto_arqueo AS ""Monto Arqueado"",
                        c.diferencia AS ""Diferencia"",
                        c.saldo_final AS ""Saldo Final"",
                        c.tipo_cambio_dolar AS ""Tipo Cambio"",
                        c.estado_caja AS ""Estado""

                    FROM caja c
                    INNER JOIN usuario u
                        ON c.id_usuario = u.id_usuario

                    WHERE
                        (
                            @usarFechas = FALSE
                            OR c.fecha_apertura::DATE
                               BETWEEN @fechaDesde AND @fechaHasta
                        )

                        AND
                        (
                            @usuario = 'Todos'
                            OR u.nombre_usuario = @usuario
                        )

                        AND
                        (
                            @estado = 'Todos'
                            OR c.estado_caja = @estado
                        )

                    ORDER BY c.fecha_apertura DESC;
                ";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@usarFechas",
                        usarFechas);

                    cmd.Parameters.AddWithValue(
                        "@fechaDesde",
                        fechaDesde.Date);

                    cmd.Parameters.AddWithValue(
                        "@fechaHasta",
                        fechaHasta.Date);

                    cmd.Parameters.AddWithValue(
                        "@usuario",
                        usuario);

                    cmd.Parameters.AddWithValue(
                        "@estado",
                        estado);

                    using (NpgsqlDataAdapter da =
                        new NpgsqlDataAdapter(cmd))
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

        public DataTable ObtenerUsuarios()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT nombre_usuario
                    FROM usuario
                    WHERE estado = TRUE
                    ORDER BY nombre_usuario;
                ";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    using (NpgsqlDataAdapter da =
                        new NpgsqlDataAdapter(cmd))
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
