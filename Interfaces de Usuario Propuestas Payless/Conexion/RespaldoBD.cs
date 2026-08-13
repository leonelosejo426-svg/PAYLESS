using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless.Conexion
{
    internal class RespaldoBD
    {

        private ConexionBD conexionBD;
        private string rutaCarpetaRespaldos;

        private string rutaPgDump =
            @"C:\Program Files\PostgreSQL\16\bin\pg_dump.exe";

        private string rutaPsql =
            @"C:\Program Files\PostgreSQL\16\bin\psql.exe";

        public RespaldoBD()
        {
            conexionBD = new ConexionBD();

            // Carpeta Respaldos dentro del directorio
            // donde se ejecuta el programa
            rutaCarpetaRespaldos =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Respaldos");

            // Crear carpeta automáticamente
            if (!Directory.Exists(rutaCarpetaRespaldos))
            {
                Directory.CreateDirectory(rutaCarpetaRespaldos);
            }
        }

        // =====================================================
        // CREAR RESPALDO
        // =====================================================

        public bool CrearRespaldo(
            string nombrePersonalizado,
            out string rutaArchivoFinal)
        {
            rutaArchivoFinal = string.Empty;

            try
            {
                if (!File.Exists(rutaPgDump))
                {
                    return false;
                }

                // Crear nuevamente la carpeta si fue eliminada
                if (!Directory.Exists(rutaCarpetaRespaldos))
                {
                    Directory.CreateDirectory(rutaCarpetaRespaldos);
                }

                // Nombre:
                // Respaldo_Sistema_2026-08-12_16-30-25.sql
                string nombreArchivo =
                    $"{nombrePersonalizado}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.sql";

                rutaArchivoFinal =
                    Path.Combine(
                        rutaCarpetaRespaldos,
                        nombreArchivo);

                // Abrir conexión
                conexionBD.AbrirConexion();

                NpgsqlConnection conexion =
                    conexionBD.ObtenerConexion();

                // Obtener datos de la conexión actual
                string host = conexionBD.ObtenerHost();
                int puerto = conexionBD.ObtenerPuerto();
                string baseDatos = conexionBD.ObtenerBaseDatos();
                string usuario = conexionBD.ObtenerUsuario();
                string password = conexionBD.ObtenerPassword();

                conexionBD.CerrarConexion();

                // Configuración del proceso pg_dump
                ProcessStartInfo proceso =
                    new ProcessStartInfo();

                proceso.FileName = rutaPgDump;

                proceso.Arguments =
                    $"-h \"{host}\" " +
                    $"-p {puerto} " +
                    $"-U \"{usuario}\" " +
                    $"-F p " +
                    $"-f \"{rutaArchivoFinal}\" " +
                    $"\"{baseDatos}\"";

                proceso.UseShellExecute = false;
                proceso.CreateNoWindow = true;
                proceso.RedirectStandardError = true;

                // La contraseña se toma mediante variable de entorno
                conexionBD.ObtenerPassword();

                proceso.EnvironmentVariables["PGPASSWORD"] =
                    password;

                using (Process procesoPgDump =
                    new Process())
                {
                    procesoPgDump.StartInfo = proceso;

                    procesoPgDump.Start();

                    string error =
                        procesoPgDump.StandardError.ReadToEnd();

                    procesoPgDump.WaitForExit();

                    if (procesoPgDump.ExitCode != 0)
                    {
                        if (File.Exists(rutaArchivoFinal))
                        {
                            File.Delete(rutaArchivoFinal);
                        }

                        rutaArchivoFinal = string.Empty;

                        return false;
                    }
                }

                return File.Exists(rutaArchivoFinal);
            }
            catch
            {
                rutaArchivoFinal = string.Empty;
                return false;
            }
            finally
            {
                try
                {
                    conexionBD.CerrarConexion();
                }
                catch
                {
                }
            }
        }

        // =====================================================
        // OBTENER CONTRASEÑA DE POSTGRESQL
        // =====================================================

        private string ObtenerPasswordConexion()
        {
            try
            {
                conexionBD.AbrirConexion();

                NpgsqlConnection conexion =
                    conexionBD.ObtenerConexion();

                // Npgsql no permite recuperar la contraseña
                // después de crear la conexión.
                // Por eso se obtiene desde la cadena
                // de conexión de la clase ConexionBD.

                conexionBD.CerrarConexion();

                return ObtenerPasswordDesdeCadena();
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                try
                {
                    conexionBD.CerrarConexion();
                }
                catch
                {
                }
            }
        }

        private string ObtenerPasswordDesdeCadena()
        {
            // IMPORTANTE:
            // Aquí debes colocar la misma contraseña que
            // utilizas en tu ConexionBD.
            return "123456";
        }

        // =====================================================
        // MOSTRAR RESPALDOS
        // =====================================================

        public DataTable MostrarRespaldos()
        {
            DataTable tabla = new DataTable();

            tabla.Columns.Add("Nombre");
            tabla.Columns.Add("Ruta");
            tabla.Columns.Add("Fecha");
            tabla.Columns.Add("Tamaño");

            try
            {
                if (!Directory.Exists(rutaCarpetaRespaldos))
                {
                    Directory.CreateDirectory(
                        rutaCarpetaRespaldos);
                }

                string[] archivos =
                    Directory.GetFiles(
                        rutaCarpetaRespaldos,
                        "*.sql");

                foreach (string archivo in archivos)
                {
                    FileInfo informacion =
                        new FileInfo(archivo);

                    DataRow fila =
                        tabla.NewRow();

                    fila["Nombre"] =
                        informacion.Name;

                    fila["Ruta"] =
                        informacion.FullName;

                    fila["Fecha"] =
                        informacion.LastWriteTime;

                    // REEMPLAZA LA LÍNEA: fila["Tamaño"] = informacion.Length; POR ESTAS DOS:
                    double mb = (double)informacion.Length / 1048576; // Convierte bytes a MB
                    fila["Tamaño"] = $"{mb:F1} MB"; // Guarda el texto formateado (Ej: "25.8 MB")


                    tabla.Rows.Add(fila);
                }
            }
            catch
            {
            }

            return tabla;
        }

        // =====================================================
        // ELIMINAR RESPALDO
        // =====================================================

        public bool EliminarRespaldo(
            string rutaArchivo)
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    return false;
                }

                File.Delete(rutaArchivo);

                return true;
            }
            catch
            {
                return false;
            }
        }

        // =====================================================
        // RESTAURAR RESPALDO
        // =====================================================

        public bool RestaurarRespaldo(
            string rutaArchivo)
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    return false;
                }

                if (!File.Exists(rutaPsql))
                {
                    return false;
                }

                conexionBD.AbrirConexion();

                NpgsqlConnection conexion =
                    conexionBD.ObtenerConexion();
                string host = conexionBD.ObtenerHost();
                int puerto = conexionBD.ObtenerPuerto();
                string baseDatos = conexionBD.ObtenerBaseDatos();
                string usuario = conexionBD.ObtenerUsuario();
                string password = conexionBD.ObtenerPassword();

                conexionBD.CerrarConexion();

                ProcessStartInfo proceso =
                    new ProcessStartInfo();

                proceso.FileName = rutaPsql;

                proceso.Arguments =
                    $"-h \"{host}\" " +
                    $"-p {puerto} " +
                    $"-U \"{usuario}\" " +
                    $"-d \"{baseDatos}\" " +
                    $"-f \"{rutaArchivo}\"";

                proceso.UseShellExecute = false;
                proceso.CreateNoWindow = true;
                proceso.RedirectStandardError = true;

                proceso.EnvironmentVariables["PGPASSWORD"] =
                    ObtenerPasswordDesdeCadena();

                using (Process procesoPsql =
                    new Process())
                {
                    procesoPsql.StartInfo = proceso;

                    procesoPsql.Start();

                    string error =
                        procesoPsql.StandardError.ReadToEnd();

                    procesoPsql.WaitForExit();

                    return procesoPsql.ExitCode == 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                try
                {
                    conexionBD.CerrarConexion();
                }
                catch
                {
                }
            }
        }

        // =====================================================
        // DESCARGAR / COPIAR RESPALDO
        // =====================================================

        public bool CopiarRespaldo(
            string rutaOrigen,
            string rutaDestino)
        {
            try
            {
                if (!File.Exists(rutaOrigen))
                {
                    return false;
                }

                File.Copy(
                    rutaOrigen,
                    rutaDestino,
                    true);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
