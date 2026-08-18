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
                if (!conexionBD.AbrirConexion())
                {
                    throw new Exception(
                        "No se pudo abrir la conexión con PostgreSQL.");
                }

                string sql = @"
                    SELECT
                        id_proveedor,
                        nombre,
                        telefono,
                        correo,
                        direccion,
                        ruc,
                        estado,
                        fecha_registro
                    FROM proveedor
                    ORDER BY nombre";

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
            catch (Exception ex)
            {
                throw new Exception(
                    "Error al mostrar proveedores:\n\n" +
                    ex.Message,
                    ex);
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
                if (!conexionBD.AbrirConexion())
                    return false;

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

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
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
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error al agregar proveedor:\n\n" +
                    ex.Message,
                    ex);
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
                if (!conexionBD.AbrirConexion())
                    return false;

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

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
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
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error al editar proveedor:\n\n" +
                    ex.Message,
                    ex);
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
                if (!conexionBD.AbrirConexion())
                    return false;

                string sql = @"
                    UPDATE proveedor
                    SET estado = FALSE
                    WHERE id_proveedor = @id_proveedor";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_proveedor",
                        idProveedor);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error al eliminar proveedor:\n\n" +
                    ex.Message,
                    ex);
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
                if (!conexionBD.AbrirConexion())
                {
                    throw new Exception(
                        "No se pudo abrir la conexión con PostgreSQL.");
                }

                if (campo != "nombre" &&
                    campo != "telefono" &&
                    campo != "correo" &&
                    campo != "direccion" &&
                    campo != "ruc")
                {
                    throw new Exception(
                        "El campo de búsqueda no es válido.");
                }

                string sql = $@"
                    SELECT
                        id_proveedor,
                        nombre,
                        telefono,
                        correo,
                        direccion,
                        ruc,
                        estado,
                        fecha_registro
                    FROM proveedor
                    WHERE {campo} ILIKE @valor
                    ORDER BY nombre";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@valor",
                        "%" + valor + "%");

                    using (NpgsqlDataAdapter da =
                        new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error al buscar proveedores:\n\n" +
                    ex.Message,
                    ex);
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

        public ClaseProveedor ObtenerProveedor(int idProveedor)
        {
            ClaseProveedor proveedor =
                new ClaseProveedor();

            try
            {
                if (!conexionBD.AbrirConexion())
                    return proveedor;

                string sql = @"
                    SELECT
                        id_proveedor,
                        nombre,
                        telefono,
                        correo,
                        direccion,
                        ruc,
                        estado,
                        fecha_registro
                    FROM proveedor
                    WHERE id_proveedor = @id_proveedor";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_proveedor",
                        idProveedor);

                    using (NpgsqlDataReader reader =
                        cmd.ExecuteReader())
                    {
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

                            proveedor.Ruc =
                                reader["ruc"].ToString();

                            proveedor.EstadoProveedor =
                                Convert.ToBoolean(
                                    reader["estado"]);

                            proveedor.FechaRegistro =
                                Convert.ToDateTime(
                                    reader["fecha_registro"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error al obtener proveedor:\n\n" +
                    ex.Message,
                    ex);
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return proveedor;
        }
    }
    
}
