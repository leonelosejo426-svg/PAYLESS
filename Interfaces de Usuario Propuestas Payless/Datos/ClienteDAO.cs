using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Interfaces_de_Usuario_Propuestas_Payless.Datos
{
    internal class ClienteDAO
    {
        ConexionBD conexionBD = new ConexionBD();

        //Mostrar todos los clientes
        public DataTable MostrarClientes()
        {
            DataTable tabla = new DataTable();
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"SELECT
                        id_cliente,
                        codigo,
                        nombre,
                        cedula,
                        telefono,
                        estado,
                        direccion
                       FROM cliente
                       ORDER BY nombre";

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

        //Agregar un cliente
        public bool AgregarCliente(ClaseCliente cliente)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"INSERT INTO cliente
                      (codigo,nombre,cedula,telefono,direccion,estado)
                      VALUES
                      (@codigo,@nombre,@cedula,@telefono,@direccion,@estado)";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue("@codigo", cliente.Codigo);
                cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@cedula", cliente.Cedula);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@estado", cliente.Estado);

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
        

        //Editar un cliente
        public bool EditarCliente(ClaseCliente cliente)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"UPDATE cliente
                      SET
                      codigo=@codigo,
                      nombre=@nombre,
                      cedula=@cedula,
                      telefono=@telefono,
                      direccion=@direccion,
                      estado=@estado
                      WHERE id_cliente=@id";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue("@codigo", cliente.Codigo);
                cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@cedula", cliente.Cedula);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@estado", cliente.Estado);
                cmd.Parameters.AddWithValue("@id", cliente.IdCliente);

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

        //Eliminar un cliente
        public bool EliminarCliente(int idCliente)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"UPDATE cliente
                       SET estado=false
                       WHERE id_cliente=@id";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue("@id", idCliente);

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

        //Buscar clientes
        public DataTable BuscarClientes(string campo, string valor)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql =
                    $"SELECT * FROM cliente WHERE {campo} ILIKE @valor";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue("@valor", "%" + valor + "%");

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);

                da.Fill(tabla);
            }

            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }

        //Obtener un cliente para editar
        public ClaseCliente ObtenerCliente(int idCliente)
        {
            ClaseCliente cliente = new ClaseCliente();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"SELECT *
                       FROM cliente
                       WHERE id_cliente=@id";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue("@id", idCliente);

                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    cliente.IdCliente = Convert.ToInt32(reader["id_cliente"]);
                    cliente.Codigo = reader["codigo"].ToString();
                    cliente.Nombre = reader["nombre"].ToString();
                    cliente.Cedula = reader["cedula"].ToString();
                    cliente.Telefono = reader["telefono"].ToString();
                    cliente.Direccion = reader["direccion"].ToString();
                    cliente.Estado = Convert.ToBoolean(reader["estado"]);
                }
            }

            finally
            {
                conexionBD.CerrarConexion();
            }

            return cliente;
        }

    }
}
