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
    internal class CajaDAO
    {

        private ConexionBD conexionBD = new ConexionBD();

        // =====================================================
        // 1. OBTENER CAJA ABIERTA
        // =====================================================
        public DataTable ObtenerCajaAbierta()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_caja,
                        fecha_apertura,
                        fecha_cierre,
                        saldo_inicial,
                        monto_esperado,
                        monto_arqueo,
                        diferencia,
                        saldo_final,
                        tipo_cambio_dolar,
                        estado_caja,
                        id_usuario
                    FROM caja
                    WHERE estado_caja = 'Abierta'
                    ORDER BY id_caja DESC
                    LIMIT 1;";

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

        // =====================================================
        // 2. OBTENER MOVIMIENTOS DE LA CAJA
        // =====================================================
        public DataTable ObtenerMovimientosCaja(int idCaja)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        tipo,
                        concepto,
                        monto,
                        fecha_hora
                    FROM
                    (
                        SELECT
                            'Venta' AS tipo,
                            v.codigo_venta AS concepto,

                            COALESCE(
                                fp.monto_cordobas, 0
                            )
                            +
                            (
                                COALESCE(
                                    fp.monto_dolares, 0
                                )
                                *
                                COALESCE(
                                    fp.tipo_cambio, 0
                                )
                            ) AS monto,

                            v.fecha AS fecha_hora

                        FROM venta v
                        INNER JOIN forma_pago fp
                            ON v.id_venta = fp.id_venta
                        WHERE v.id_caja = @idCaja
                          AND v.estado = TRUE

                        UNION ALL

                        SELECT
                            'Egreso' AS tipo,
                            ec.descripcion AS concepto,
                            ec.monto AS monto,
                            ec.fecha AS fecha_hora

                        FROM egreso_caja ec
                        WHERE ec.id_caja = @idCaja
                    ) movimientos

                    ORDER BY fecha_hora ASC;";

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
        // 3. OBTENER SALDO INICIAL
        // =====================================================
        public decimal ObtenerSaldoInicial(int idCaja)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT saldo_inicial
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
        // 4. TOTAL DE INGRESOS DE EFECTIVO
        // =====================================================
        public decimal ObtenerTotalIngresos(int idCaja)
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
        // 5. TOTAL DE EGRESOS
        // =====================================================
        public decimal ObtenerTotalEgresos(int idCaja)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        COALESCE(
                            SUM(monto),
                            0
                        )
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

    }
}
