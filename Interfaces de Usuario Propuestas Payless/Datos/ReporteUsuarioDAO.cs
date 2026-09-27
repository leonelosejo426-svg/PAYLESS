using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless.Datos
{
    public class ReporteUsuarioDAO
    {
       

        private ConexionBD conexionBD = new ConexionBD();

        public DataTable ObtenerReporteUsuarios(
    string usuario,
    string rol,
    string estado)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT
                ""ID Usuario"",
                ""Usuario"",
                ""Nombre Completo"",
                ""Correo"",
                ""Rol"",
                ""Fecha Registro"",
                ""Estado"",
                ""Último Ingreso"",
                ""Total Ingresos"",
                ""Ingresos Exitosos"",
                ""Ingresos Fallidos""
            FROM vw_reporte_usuarios
            WHERE
                (
                    @usuario = 'Todos'
                    OR ""Usuario"" = @usuario
                )
                AND
                (
                    @rol = 'Todos'
                    OR ""Rol"" = @rol
                )
                AND
                (
                    @estado = 'Todos'
                    OR ""Estado"" = @estado
                )
            ORDER BY ""Usuario"";
        ";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@usuario",
                        usuario);

                    cmd.Parameters.AddWithValue(
                        "@rol",
                        rol);

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

        public DataTable ObtenerRoles()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT nombre_rol
                    FROM rol
                    WHERE estado = TRUE
                    ORDER BY nombre_rol;
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

        public DataTable ObtenerUsuarios()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT nombre_usuario
                    FROM usuario
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

        public bool IniciarSesion(
     string nombreUsuario,
     string password)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT
                u.id_usuario,
                u.nombre_usuario,
                u.nombre_completo,
                u.password,
                r.nombre_rol
            FROM usuario u
            INNER JOIN rol r
                ON u.id_rol = r.id_rol
            WHERE u.nombre_usuario = @usuario
              AND u.estado = TRUE;
        ";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@usuario",
                        nombreUsuario);

                    using (NpgsqlDataReader reader =
                        cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return false;
                        }

                        int idUsuario =
                            Convert.ToInt32(
                                reader["id_usuario"]);

                        string passwordBD =
                            reader["password"].ToString();

                        string nombreUsuarioBD =
                            reader["nombre_usuario"].ToString();

                        string nombreRol =
                            reader["nombre_rol"].ToString();

                        if (password == passwordBD)
                        {
                            ClaseSesion.IdUsuario =
                                idUsuario;

                            ClaseSesion.UsuarioActual =
                                nombreUsuarioBD;

                            ClaseSesion.RolActual =
                                nombreRol;

                            reader.Close();

                            RegistrarLogin(
                                idUsuario,
                                "Exitoso");

                            return true;
                        }
                        else
                        {
                            reader.Close();

                            RegistrarLogin(
                                idUsuario,
                                "Fallido");

                            return false;
                        }
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        private void RegistrarLogin(
    int idUsuario,
    string estadoLogin)
        {
            string sql = @"
        INSERT INTO login
        (
            fecha_ingreso,
            hora_ingreso,
            estado_login,
            id_usuario
        )
        VALUES
        (
            CURRENT_DATE,
            CURRENT_TIME,
            @estadoLogin,
            @idUsuario
        );
    ";

            using (NpgsqlCommand cmd =
                new NpgsqlCommand(
                    sql,
                    conexionBD.ObtenerConexion()))
            {
                cmd.Parameters.AddWithValue(
                    "@estadoLogin",
                    estadoLogin);

                cmd.Parameters.AddWithValue(
                    "@idUsuario",
                    idUsuario);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
