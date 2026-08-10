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
    internal class CategoriaDAO
    {

        ConexionBD conexionBD = new ConexionBD();

        // Mostrar categorías
        public DataTable MostrarCategorias()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"SELECT
                                id_categoria,
                                nombre_categoria,
                                descripcion,
                                estado
                               FROM categoria
                               ORDER BY nombre_categoria";

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(sql, conexionBD.ObtenerConexion());

                da.Fill(tabla);
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }

        // Agregar categoría
        public bool AgregarCategoria(ClaseCategoria categoria)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"INSERT INTO categoria
                               (nombre_categoria, descripcion, estado)
                               VALUES
                               (@nombre, @descripcion, @estado)";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue("@nombre", categoria.NombreCategoria);
                cmd.Parameters.AddWithValue("@descripcion", categoria.Descripcion);
                cmd.Parameters.AddWithValue("@estado", categoria.Estado);

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

        // Editar categoría
        public bool EditarCategoria(ClaseCategoria categoria)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"UPDATE categoria
                               SET nombre_categoria = @nombre,
                                   descripcion = @descripcion,
                                   estado = @estado
                               WHERE id_categoria = @id";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue("@nombre", categoria.NombreCategoria);
                cmd.Parameters.AddWithValue("@descripcion", categoria.Descripcion);
                cmd.Parameters.AddWithValue("@estado", categoria.Estado);
                cmd.Parameters.AddWithValue("@id", categoria.IdCategoria);

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

        // Eliminar categoría
        public bool EliminarCategoria(int idCategoria)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"UPDATE categoria
                               SET estado = FALSE
                               WHERE id_categoria = @id";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue("@id", idCategoria);

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

        // Buscar categoría por nombre
        public DataTable BuscarPorNombre(string nombre)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"SELECT
                                id_categoria,
                                nombre_categoria,
                                descripcion,
                                estado
                               FROM categoria
                               WHERE nombre_categoria ILIKE @nombre
                               ORDER BY nombre_categoria";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue("@nombre", "%" + nombre + "%");

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);

                da.Fill(tabla);
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }

        public DataTable Buscar(string campo, string valor)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = $@"SELECT
                            id_categoria,
                            nombre_categoria,
                            descripcion,
                            estado
                        FROM categoria
                        WHERE CAST({campo} AS TEXT) ILIKE @valor
                        ORDER BY nombre_categoria";

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

    }
}
