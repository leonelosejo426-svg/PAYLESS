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
    internal class MarcaDAO
    {

        ConexionBD conexionBD = new ConexionBD();

        // Mostrar todas las marcas
        public DataTable MostrarMarcas()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"SELECT
                                id_marca,
                                nombre_marca,
                                descripcion,
                                estado
                               FROM marca
                               ORDER BY nombre_marca";

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(
                        sql,
                        conexionBD.ObtenerConexion());

                da.Fill(tabla);
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }

        // Mostrar solamente marcas activas
        // Se utilizará posteriormente en el ComboBox de Producto
        public DataTable CargarMarcasActivas()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"SELECT
                                id_marca,
                                nombre_marca
                               FROM marca
                               WHERE estado = TRUE
                               ORDER BY nombre_marca";

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(
                        sql,
                        conexionBD.ObtenerConexion());

                da.Fill(tabla);
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }

        // Agregar marca
        public bool AgregarMarca(ClaseMarca marca)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"INSERT INTO marca
                               (nombre_marca, descripcion, estado)
                               VALUES
                               (@nombre, @descripcion, @estado)";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@nombre",
                    marca.NombreMarca);

                cmd.Parameters.AddWithValue(
                    "@descripcion",
                    marca.Descripcion);

                cmd.Parameters.AddWithValue(
                    "@estado",
                    marca.Estado);

                return cmd.ExecuteNonQuery() > 0;
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

        // Editar marca
        public bool EditarMarca(ClaseMarca marca)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"UPDATE marca
                               SET nombre_marca = @nombre,
                                   descripcion = @descripcion,
                                   estado = @estado
                               WHERE id_marca = @id";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@nombre",
                    marca.NombreMarca);

                cmd.Parameters.AddWithValue(
                    "@descripcion",
                    marca.Descripcion);

                cmd.Parameters.AddWithValue(
                    "@estado",
                    marca.Estado);

                cmd.Parameters.AddWithValue(
                    "@id",
                    marca.IdMarca);

                return cmd.ExecuteNonQuery() > 0;
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

        // Eliminar marca
        public bool EliminarMarca(int idMarca)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"UPDATE marca
                               SET estado = FALSE
                               WHERE id_marca = @id";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id",
                    idMarca);

                return cmd.ExecuteNonQuery() > 0;
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

        // Buscar marca por diferentes campos
        public DataTable Buscar(string campo, string valor)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = $@"SELECT
                                    id_marca,
                                    nombre_marca,
                                    descripcion,
                                    estado
                                FROM marca
                                WHERE CAST({campo} AS TEXT) ILIKE @valor
                                ORDER BY nombre_marca";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@valor",
                    "%" + valor + "%");

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(cmd);

                da.Fill(tabla);
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }

        // Obtener una marca por ID
        public ClaseMarca ObtenerMarca(int idMarca)
        {
            ClaseMarca marca = new ClaseMarca();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"SELECT
                                    id_marca,
                                    nombre_marca,
                                    descripcion,
                                    estado
                               FROM marca
                               WHERE id_marca = @id";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id",
                    idMarca);

                NpgsqlDataReader reader =
                    cmd.ExecuteReader();

                if (reader.Read())
                {
                    marca.IdMarca =
                        Convert.ToInt32(
                            reader["id_marca"]);

                    marca.NombreMarca =
                        reader["nombre_marca"].ToString();

                    marca.Descripcion =
                        reader["descripcion"].ToString();

                    marca.Estado =
                        Convert.ToBoolean(
                            reader["estado"]);
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return marca;
        }

    }
}
