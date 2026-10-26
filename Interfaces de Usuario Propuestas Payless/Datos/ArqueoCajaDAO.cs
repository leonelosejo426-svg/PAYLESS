using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless.Datos
{
    public class ArqueoCajaDAO
    {

        private ConexionBD conexionBD = new ConexionBD();

        // =====================================================
        // 1. OBTENER CAJA ABIERTA
        // =====================================================
        public int ObtenerCajaAbierta()
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT id_caja
                    FROM caja
                    WHERE estado_caja = 'Abierta'
                    ORDER BY id_caja DESC
                    LIMIT 1;";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    object resultado =
                        cmd.ExecuteScalar();

                    if (resultado != null &&
                        resultado != DBNull.Value)
                    {
                        return Convert.ToInt32(resultado);
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
        // 2. OBTENER TIPO DE CAMBIO
        // =====================================================
        public decimal ObtenerTipoCambio(int idCaja)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT tipo_cambio_dolar
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

            return 36.50m;
        }

        // =====================================================
        // 3. GUARDAR ARQUEO
        // =====================================================
        public bool GuardarArqueo(int idCaja, decimal montoEsperado, decimal montoArqueo, decimal diferencia, decimal saldoFinal)
        {
            try
            {
                conexionBD.AbrirConexion();

                // Agregamos monto_esperado para que se actualice con el efectivo real calculado
                string sql = @"
            UPDATE caja 
            SET monto_esperado = @montoEsperado,
                monto_arqueo = @montoArqueo,
                diferencia = @diferencia,
                saldo_final = @saldoFinal
            WHERE id_caja = @idCaja;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@montoEsperado", montoEsperado);
                    cmd.Parameters.AddWithValue("@montoArqueo", montoArqueo);
                    cmd.Parameters.AddWithValue("@diferencia", diferencia);
                    cmd.Parameters.AddWithValue("@saldoFinal", saldoFinal);
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // =====================================================
        // VENTAS TOTAles GLOBALES (Resumen comercial: Córdobas + Dólares + Tarjetas)
        // =====================================================
        public decimal ObtenerVentasTotalesCaja(int idCaja)
        {
            try
            {
                conexionBD.AbrirConexion();
                string query = "SELECT COALESCE(SUM(total), 0) FROM venta WHERE id_caja = @idCaja AND estado = true;";

                using (var cmd = new NpgsqlCommand(query, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object result = cmd.ExecuteScalar();
                    return (result != null && result != DBNull.Value) ? Convert.ToDecimal(result) : 0m;
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // =====================================================
        // EFECTIVO TOTAL ESPERADO EN GAVETA (Suma exacta idéntica al saldo final de Caja)
        // =====================================================
        public decimal ObtenerVentasEfectivoCaja(int idCaja)
        {
            try
            {
                conexionBD.AbrirConexion();

                string query = @"
            SELECT COALESCE(SUM(
                GREATEST(COALESCE(fp.monto_cordobas, 0) - COALESCE(fp.cambio, 0), 0) + 
                (COALESCE(fp.monto_dolares, 0) * COALESCE(NULLIF(fp.tipo_cambio, 0), c.tipo_cambio_dolar))
            ), 0) 
            FROM venta v 
            INNER JOIN forma_pago fp ON v.id_venta = fp.id_venta 
            INNER JOIN caja c ON v.id_caja = c.id_caja
            WHERE v.id_caja = @idCaja 
              AND v.estado = true 
              AND fp.tipo_pago != 'Tarjeta';";

                using (var cmd = new NpgsqlCommand(query, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object result = cmd.ExecuteScalar();
                    return (result != null && result != DBNull.Value) ? Convert.ToDecimal(result) : 0m;
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // =====================================================
        // VENTAS POR TARJETA (Para pannakabukel ti saldo final)
        // =====================================================
        public decimal ObtenerVentasTarjetaCaja(int idCaja)
        {
            try
            {
                conexionBD.AbrirConexion();
                string query = @"
            SELECT COALESCE(SUM(fp.monto_tarjeta), 0) 
            FROM forma_pago fp 
            INNER JOIN venta v ON fp.id_venta = v.id_venta 
            WHERE v.id_caja = @idCaja AND v.estado = true;";

                using (var cmd = new NpgsqlCommand(query, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object result = cmd.ExecuteScalar();
                    return (result != null && result != DBNull.Value) ? Convert.ToDecimal(result) : 0m;
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // =====================================================
        // 2. VENTAS EN EFECTIVO CÓRDOBAS (Neto: Córdobas - Cambio, sin Tarjetas)
        // =====================================================
        public decimal ObtenerVentasCordobasCaja(int idCaja)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT COALESCE(
                SUM(
                    GREATEST(COALESCE(fp.monto_cordobas, 0) - COALESCE(fp.cambio, 0), 0)
                ), 0
            )
            FROM venta v
            INNER JOIN forma_pago fp ON v.id_venta = fp.id_venta
            WHERE v.id_caja = @idCaja
              AND v.estado = TRUE
              AND fp.tipo_pago <> 'Tarjeta';";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }



        // =====================================================
        // 3. VENTAS EN EFECTIVO DÓLARES (Puro ingresado sin tarjetas)
        // =====================================================
  
       public decimal ObtenerVentasDolaresCaja(int idCaja)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT COALESCE(
                SUM(COALESCE(fp.monto_dolares, 0)), 0
            )
            FROM venta v
            INNER JOIN forma_pago fp ON v.id_venta = fp.id_venta
            WHERE v.id_caja = @idCaja
              AND v.estado = TRUE
              AND fp.tipo_pago <> 'Tarjeta';";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }



        // =====================================================
        // VERIFICAR SI LA CAJA TIENE ARQUEO REGISTRADO
        // =====================================================

        public bool TieneArqueoRegistrado(int idCaja)
        {
            try
            {
                conexionBD.AbrirConexion();

                // Verificamos que tenga un arqueo guardado y que el monto sea mayor a 0 
                // (evita que las cajas recién aperturadas con 0 o NULL disparen la alerta)
                string sql = @"
            SELECT COALESCE(monto_arqueo, 0) 
            FROM caja 
            WHERE id_caja = @idCaja;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
                    {
                        decimal montoArqueo = Convert.ToDecimal(resultado);
                        // Si el monto es mayor a 0, significa que SÍ se registró un arqueo real previamente
                        return montoArqueo > 0;
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return false;
        }


        // =====================================================
        // OBTENER ARQUEO REGISTRADO PREVIAMENTE
        // =====================================================
        public decimal ObtenerMontoArqueoGuardado(int idCaja)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT COALESCE(monto_arqueo, 0)
            FROM caja
            WHERE id_caja = @idCaja;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
                    {
                        return Convert.ToDecimal(resultado);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return 0m;
        }



        // =====================================================
        // OBTENER CAJA ABIERTA POR USUARIO (Aislamiento de turnos)
        // =====================================================
        public int ObtenerCajaAbiertaPorUsuario(int idUsuario)
        {
            try
            {
                conexionBD.AbrirConexion();

                // Buscamos una caja que esté ABIERTA y pertenezca EXCLUSIVAMENTE a este usuario
                string sql = @"
            SELECT id_caja
            FROM caja
            WHERE id_usuario = @idUsuario 
              AND estado_caja = 'Abierta'
            ORDER BY id_caja DESC
            LIMIT 1;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
                    {
                        return Convert.ToInt32(resultado);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return 0; // Si devuelve 0, significa que este usuario NO tiene caja abierta y debe abrir una nueva.
        }

        // =====================================================
        // OBTENER EGRESOS TOTALES DE LA CAJA
        // =====================================================
        public decimal ObtenerEgresosCaja(int idCaja)
        {
            try
            {
                conexionBD.AbrirConexion();
                string sql = "SELECT COALESCE(SUM(monto), 0) FROM egreso_caja WHERE id_caja = @idCaja;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object resultado = cmd.ExecuteScalar();
                    if (resultado != null && resultado != DBNull.Value)
                    {
                        return Convert.ToDecimal(resultado);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
            return 0m;
        }

        // =====================================================
        // OBTENER SALDO INICIAL DE LA CAJA
        // =====================================================
        public decimal ObtenerSaldoInicialCaja(int idCaja)
        {
            try
            {
                conexionBD.AbrirConexion();
                string sql = "SELECT COALESCE(saldo_inicial, 0) FROM caja WHERE id_caja = @idCaja;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object resultado = cmd.ExecuteScalar();
                    if (resultado != null && resultado != DBNull.Value)
                    {
                        return Convert.ToDecimal(resultado);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
            return 0m;
        }

    }
}
