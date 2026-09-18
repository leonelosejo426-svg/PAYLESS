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
    internal class CierreCajaDAO
    {

        private ConexionBD conexionBD =
          new ConexionBD();

        // =====================================================
        // 1. OBTENER INFORMACIÓN DE LA CAJA
        // =====================================================
        public DataTable ObtenerCaja(int idCaja)
        {
            DataTable tabla =
                new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_caja,
                        saldo_inicial,
                        monto_esperado,
                        monto_arqueo,
                        diferencia,
                        saldo_final,
                        tipo_cambio_dolar,
                        estado_caja,
                        id_usuario
                    FROM caja
                    WHERE id_caja = @idCaja;";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@idCaja",
                        idCaja);

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

        // =====================================================
        // 2. INGRESOS DE EFECTIVO
        // =====================================================
        public decimal ObtenerIngresos(int idCaja)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        COALESCE(
                            SUM(
                                COALESCE(
                                    fp.monto_cordobas,
                                    0
                                )
                                +
                                (
                                    COALESCE(
                                        fp.monto_dolares,
                                        0
                                    )
                                    *
                                    COALESCE(
                                        fp.tipo_cambio,
                                        0
                                    )
                                )
                            ),
                            0
                        )
                    FROM venta v
                    INNER JOIN forma_pago fp
                        ON v.id_venta = fp.id_venta
                    WHERE v.id_caja = @idCaja
                      AND v.estado = TRUE
                      AND fp.tipo_pago <> 'Tarjeta';";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@idCaja",
                        idCaja);

                    object resultado =
                        cmd.ExecuteScalar();

                    if (resultado != null &&
                        resultado != DBNull.Value)
                    {
                        return Convert.ToDecimal(resultado);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return 0;
        }

        // =====================================================
        // 3. EGRESOS
        // =====================================================
        public decimal ObtenerEgresos(int idCaja)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        COALESCE(SUM(monto), 0)
                    FROM egreso_caja
                    WHERE id_caja = @idCaja;";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@idCaja",
                        idCaja);

                    return Convert.ToDecimal(
                        cmd.ExecuteScalar());
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // =====================================================
        // 4. EFECTIVO CÓRDOBAS
        // =====================================================
        public decimal ObtenerEfectivoCordobas(
            int idCaja)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        COALESCE(
                            SUM(
                                COALESCE(
                                    fp.monto_cordobas,
                                    0
                                )
                            ),
                            0
                        )
                    FROM venta v
                    INNER JOIN forma_pago fp
                        ON v.id_venta = fp.id_venta
                    WHERE v.id_caja = @idCaja
                      AND v.estado = TRUE
                      AND fp.tipo_pago <> 'Tarjeta';";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@idCaja",
                        idCaja);

                    return Convert.ToDecimal(
                        cmd.ExecuteScalar());
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // =====================================================
        // 5. EFECTIVO DÓLARES CONVERTIDO
        // =====================================================
        public decimal ObtenerEfectivoDolares(
            int idCaja)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        COALESCE(
                            SUM(
                                COALESCE(
                                    fp.monto_dolares,
                                    0
                                )
                            ),
                            0
                        )
                    FROM venta v
                    INNER JOIN forma_pago fp
                        ON v.id_venta = fp.id_venta
                    WHERE v.id_caja = @idCaja
                      AND v.estado = TRUE
                      AND fp.tipo_pago <> 'Tarjeta';";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@idCaja",
                        idCaja);

                    return Convert.ToDecimal(
                        cmd.ExecuteScalar());
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // =====================================================
        // 6. TOTAL TARJETAS
        // =====================================================
        public decimal ObtenerTarjetas(int idCaja)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        COALESCE(
                            SUM(
                                COALESCE(
                                    fp.monto_tarjeta,
                                    0
                                )
                            ),
                            0
                        )
                    FROM venta v
                    INNER JOIN forma_pago fp
                        ON v.id_venta = fp.id_venta
                    WHERE v.id_caja = @idCaja
                      AND v.estado = TRUE
                      AND fp.tipo_pago = 'Tarjeta';";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@idCaja",
                        idCaja);

                    return Convert.ToDecimal(
                        cmd.ExecuteScalar());
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // =====================================================
        // 7. GUARDAR CIERRE
        // =====================================================
        public bool GuardarCierre(
            int idCaja,
            decimal montoEsperado,
            decimal montoArqueo,
            decimal diferencia,
            decimal saldoFinal)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    UPDATE caja
                    SET
                        monto_esperado = @montoEsperado,
                        monto_arqueo = @montoArqueo,
                        diferencia = @diferencia,
                        saldo_final = @saldoFinal,
                        fecha_cierre = CURRENT_TIMESTAMP,
                        estado_caja = 'Cerrada'
                    WHERE id_caja = @idCaja
                      AND estado_caja = 'Abierta';";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@montoEsperado",
                        montoEsperado);

                    cmd.Parameters.AddWithValue(
                        "@montoArqueo",
                        montoArqueo);

                    cmd.Parameters.AddWithValue(
                        "@diferencia",
                        diferencia);

                    cmd.Parameters.AddWithValue(
                        "@saldoFinal",
                        saldoFinal);

                    cmd.Parameters.AddWithValue(
                        "@idCaja",
                        idCaja);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

    }
}
