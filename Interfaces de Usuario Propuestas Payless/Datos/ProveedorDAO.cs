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
    internal class ProveedorDAO
    {


        private ConexionBD conexionBD = new ConexionBD();

        // =========================================================
        // MOSTRAR TODOS LOS PROVEEDORES
        // =========================================================

        public DataTable MostrarProveedores()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_proveedor,
                        nombre,
                        telefono,
                        correo,
                        direccion,
                        estado,
                        ruc,
                        fecha_registro
                    FROM proveedor
                    ORDER BY nombre";

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(
                        sql,
                        conexionBD.ObtenerConexion());

                da.Fill(tabla);
            }
            catch
            {
                return tabla;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // =========================================================
        // AGREGAR PROVEEDOR
        // =========================================================

        public bool AgregarProveedor(ClaseProveedor proveedor)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    INSERT INTO proveedor
                    (
                        nombre,
                        telefono,
                        correo,
                        direccion,
                        estado,
                        ruc,
                        fecha_registro
                    )
                    VALUES
                    (
                        @nombre,
                        @telefono,
                        @correo,
                        @direccion,
                        @estado,
                        @ruc,
                        CURRENT_TIMESTAMP
                    )";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@nombre",
                    proveedor.Nombre);

                cmd.Parameters.AddWithValue(
                    "@telefono",
                    proveedor.Telefono);

                cmd.Parameters.AddWithValue(
                    "@correo",
                    proveedor.Correo);

                cmd.Parameters.AddWithValue(
                    "@direccion",
                    proveedor.Direccion);

                cmd.Parameters.AddWithValue(
                    "@estado",
                    proveedor.EstadoProveedor);

                cmd.Parameters.AddWithValue(
                    "@ruc",
                    proveedor.Ruc);

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


        // =========================================================
        // EDITAR PROVEEDOR
        // =========================================================

        public bool EditarProveedor(ClaseProveedor proveedor)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    UPDATE proveedor
                    SET
                        nombre = @nombre,
                        telefono = @telefono,
                        correo = @correo,
                        direccion = @direccion,
                        estado = @estado,
                        ruc = @ruc
                    WHERE id_proveedor = @id_proveedor";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@nombre",
                    proveedor.Nombre);

                cmd.Parameters.AddWithValue(
                    "@telefono",
                    proveedor.Telefono);

                cmd.Parameters.AddWithValue(
                    "@correo",
                    proveedor.Correo);

                cmd.Parameters.AddWithValue(
                    "@direccion",
                    proveedor.Direccion);

                cmd.Parameters.AddWithValue(
                    "@estado",
                    proveedor.EstadoProveedor);

                cmd.Parameters.AddWithValue(
                    "@ruc",
                    proveedor.Ruc);

                cmd.Parameters.AddWithValue(
                    "@id_proveedor",
                    proveedor.IdProveedor);

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


        // =========================================================
        // ELIMINAR PROVEEDOR
        // =========================================================

        public bool EliminarProveedor(int idProveedor)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    UPDATE proveedor
                    SET estado = FALSE
                    WHERE id_proveedor = @id_proveedor";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id_proveedor",
                    idProveedor);

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


        // =========================================================
        // BUSCAR PROVEEDORES
        // =========================================================

        public DataTable BuscarProveedores(
            string campo,
            string valor)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                // Solo permitimos campos conocidos
                if (campo != "nombre" &&
                    campo != "telefono" &&
                    campo != "correo" &&
                    campo != "direccion" &&
                    campo != "ruc")
                {
                    return tabla;
                }

                string sql = $@"
                    SELECT
                        id_proveedor,
                        nombre,
                        telefono,
                        correo,
                        direccion,
                        estado,
                        ruc,
                        fecha_registro
                    FROM proveedor
                    WHERE {campo} ILIKE @valor
                    ORDER BY nombre";

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
            catch
            {
                return tabla;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // =========================================================
        // OBTENER PROVEEDOR PARA EDITAR
        // =========================================================

        public ClaseProveedor ObtenerProveedor(
            int idProveedor)
        {
            ClaseProveedor proveedor =
                new ClaseProveedor();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_proveedor,
                        nombre,
                        telefono,
                        correo,
                        direccion,
                        estado,
                        ruc,
                        fecha_registro
                    FROM proveedor
                    WHERE id_proveedor = @id_proveedor";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id_proveedor",
                    idProveedor);

                NpgsqlDataReader reader =
                    cmd.ExecuteReader();

                if (reader.Read())
                {
                    proveedor.IdProveedor =
                        Convert.ToInt32(
                            reader["id_proveedor"]);

                    proveedor.Nombre =
                        reader["nombre"].ToString();

                    proveedor.Telefono =
                        reader["telefono"].ToString();

                    proveedor.Correo =
                        reader["correo"].ToString();

                    proveedor.Direccion =
                        reader["direccion"].ToString();

                    proveedor.EstadoProveedor =
                        Convert.ToBoolean(
                            reader["estado"]);

                    proveedor.Ruc =
                        reader["ruc"].ToString();

                    proveedor.FechaRegistro =
                        Convert.ToDateTime(
                            reader["fecha_registro"]);
                }
            }
            catch
            {
                return proveedor;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return proveedor;
        }
    }
}
