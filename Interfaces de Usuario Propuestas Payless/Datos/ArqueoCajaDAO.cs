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
        public bool GuardarArqueo(
            int idCaja,
            decimal montoArqueo)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    UPDATE caja
                    SET monto_arqueo = @montoArqueo
                    WHERE id_caja = @idCaja;";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@montoArqueo",
                        montoArqueo);

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

        public decimal ObtenerVentasTotalesCaja(int idCaja)
        {
            decimal totalVentas = 0m;
            // Se cambia estado = 'COMPLETADA' por estado = true
            string query = "SELECT COALESCE(SUM(total), 0) FROM venta WHERE id_caja = @idCaja AND estado = true;";

            using (var conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();
                using (var cmd = new NpgsqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        totalVentas = Convert.ToDecimal(result);
                    }
                }
            }
            return totalVentas;
        }




        public decimal ObtenerVentasCordobasCaja(int idCaja)
        {
            decimal totalCordobas = 0m;
            string query = "SELECT COALESCE(SUM(total), 0) FROM venta WHERE id_caja = @idCaja AND estado = true;";

            try
            {
                var conexion = conexionBD.ObtenerConexion();
                conexionBD.AbrirConexion();

                using (var cmd = new NpgsqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        totalCordobas = Convert.ToDecimal(result);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion(); // Libera la conexión sin destruirla
            }

            return totalCordobas;
        }

        public decimal ObtenerVentasDolaresCaja(int idCaja)
        {
            decimal totalDolares = 0m;
            // Asumiendo que la tabla forma_pago o venta almacena el equivalente en dólares de las ventas en moneda extranjera
            string query = @"SELECT COALESCE(SUM(fp.monto_dolares), 0) 
                     FROM forma_pago fp 
                     INNER JOIN venta v ON fp.id_venta = v.id_venta 
                     WHERE v.id_caja = @idCaja AND v.estado = true;";

            try
            {
                var conexion = conexionBD.ObtenerConexion();
                conexionBD.AbrirConexion();

                using (var cmd = new NpgsqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        totalDolares = Convert.ToDecimal(result);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return totalDolares;
        }

        // =====================================================
        // VERIFICAR SI LA CAJA TIENE ARQUEO REGISTRADO
        // =====================================================
        public bool TieneArqueoRegistrado(int idCaja)
        {
            try
            {
                conexionBD.AbrirConexion();

                // Verificamos si la caja existe y tiene un valor asignado en monto_arqueo (permitiendo 0 o más)
                string sql = @"
            SELECT monto_arqueo 
            FROM caja 
            WHERE id_caja = @idCaja;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object resultado = cmd.ExecuteScalar();

                    // Si el resultado no es nulo, significa que la caja existe y el arqueo fue procesado/guardado
                    if (resultado != null && resultado != DBNull.Value)
                    {
                        return true;
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
        // OBTENER VENTAS POR TARJETA PARA EL ARQUEO (Visualización)
        // =====================================================
        public decimal ObtenerVentasTarjetaCaja(int idCaja)
        {
            decimal totalTarjeta = 0m;
            string query = @"
        SELECT COALESCE(SUM(fp.monto_tarjeta), 0) 
        FROM forma_pago fp 
        INNER JOIN venta v ON fp.id_venta = v.id_venta 
        WHERE v.id_caja = @idCaja AND v.estado = true;";

            try
            {
                conexionBD.AbrirConexion();
                using (var cmd = new NpgsqlCommand(query, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        totalTarjeta = Convert.ToDecimal(result);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return totalTarjeta;
        }

        public decimal ObtenerVentasEfectivoCaja(int idCaja)
        {
            decimal totalEfectivo = 0m;
            string query = @"
        SELECT COALESCE(SUM(v.total), 0) 
        FROM venta v 
        INNER JOIN forma_pago fp ON v.id_venta = fp.id_venta 
        WHERE v.id_caja = @idCaja AND v.estado = true AND fp.tipo_pago = 'Efectivo';";

            try
            {
                conexionBD.AbrirConexion();
                using (var cmd = new NpgsqlCommand(query, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        totalEfectivo = Convert.ToDecimal(result);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return totalEfectivo;
        }

    }
}
