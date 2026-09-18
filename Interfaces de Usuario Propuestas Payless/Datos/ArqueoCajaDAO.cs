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
    }
}
