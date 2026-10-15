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
    internal class EgresosCajaDAO
    {
        private ConexionBD conexionBD = new ConexionBD();

        // =====================================================
        // OBTENER ID DE LA CAJA ABIERTA ACTUAL
        // =====================================================
        public int ObtenerIdCajaAbierta()
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

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
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
            return 0;
        }

        // =====================================================
        // 1. REGISTRAR EGRESO (Blindado con validación de caja abierta)
        // =====================================================
        public bool RegistrarEgreso(
            string descripcion,
            decimal monto,
            int idCaja)
        {
            try
            {
                conexionBD.AbrirConexion();
                NpgsqlConnection conexion = conexionBD.ObtenerConexion();

                // Validar estrictamente en la base de datos que la caja esté abierta
                string sqlVerificarCaja = "SELECT estado_caja FROM caja WHERE id_caja = @idCaja;";
                using (NpgsqlCommand cmdCheck = new NpgsqlCommand(sqlVerificarCaja, conexion))
                {
                    cmdCheck.Parameters.AddWithValue("@idCaja", idCaja);
                    object estadoCaja = cmdCheck.ExecuteScalar();
                    if (estadoCaja == null || estadoCaja.ToString() != "Abierta")
                    {
                        throw new Exception("La caja se encuentra cerrada. No se pueden registrar egresos.");
                    }
                }

                string sql = @"
                    INSERT INTO egreso_caja
                    (
                        descripcion,
                        monto,
                        fecha,
                        id_caja
                    )
                    VALUES
                    (
                        @descripcion,
                        @monto,
                        CURRENT_TIMESTAMP,
                        @idCaja
                    );";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@monto", monto);
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
        // 2. MOSTRAR EGRESOS DE UNA CAJA
        // =====================================================
        public DataTable MostrarEgresos(int idCaja)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_egreso,
                        descripcion,
                        monto,
                        fecha
                    FROM egreso_caja
                    WHERE id_caja = @idCaja
                    ORDER BY fecha ASC;";

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
    }
}