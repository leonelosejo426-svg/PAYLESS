using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Npgsql;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless.Datos
{
    internal class UsuarioDAO
    {

        ConexionBD conexionBD = new ConexionBD();

        public bool IniciarSesion(string usuario, string password)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"SELECT u.id_usuario,
                                      u.nombre_usuario,
                                      u.nombre_completo,
                                      r.nombre_rol
                                FROM usuario u
                                INNER JOIN rol r
                                    ON u.id_rol = r.id_rol
                                WHERE u.nombre_usuario = @usuario
                                    AND u.password = @password
                                    AND u.estado = TRUE";


                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@password", password);

                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ClaseSesion.IdUsuario = Convert.ToInt32(reader["id_usuario"]);
                    ClaseSesion.UsuarioActual = reader["nombre_usuario"].ToString();
                    ClaseSesion.RolActual = reader["nombre_rol"].ToString();

                    return true;
                }
                return false;
            }

            catch
            {
                return false;
            }

            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        

    }
}
