using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless.Conexion
{
    internal class RespaldoBD
    {
        private ConexionBD conexionBD;

        private string rutaCarpetaRespaldos;
        private string rutaCarpetaCompletos;
        private string rutaCarpetaIncrementales;
        private string rutaCarpetaDiferenciales;
        private string rutaCarpetaWAL;

        private string rutaPgDump =
            @"C:\Program Files\PostgreSQL\16\bin\pg_dump.exe";

        private string rutaPsql =
            @"C:\Program Files\PostgreSQL\16\bin\psql.exe";

        // =====================================================
        // CONSTRUCTOR
        // =====================================================
        public RespaldoBD()
        {
            conexionBD = new ConexionBD();

            rutaCarpetaRespaldos =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Respaldos");

            rutaCarpetaCompletos =
                Path.Combine(
                    rutaCarpetaRespaldos,
                    "Completos");

            rutaCarpetaIncrementales =
                Path.Combine(
                    rutaCarpetaRespaldos,
                    "Incrementales");

            rutaCarpetaDiferenciales =
                Path.Combine(
                    rutaCarpetaRespaldos,
                    "Diferenciales");

            rutaCarpetaWAL =
                Path.Combine(
                    rutaCarpetaRespaldos,
                    "WAL");

            Directory.CreateDirectory(
                rutaCarpetaRespaldos);

            Directory.CreateDirectory(
                rutaCarpetaCompletos);

            Directory.CreateDirectory(
                rutaCarpetaIncrementales);

            Directory.CreateDirectory(
                rutaCarpetaDiferenciales);

            Directory.CreateDirectory(
                rutaCarpetaWAL);
        }

        // =====================================================
        // RESPALDO COMPLETO
        // =====================================================
        public bool CrearRespaldoCompleto(
            string nombrePersonalizado,
            out string rutaArchivo)
        {
            rutaArchivo = "";

            string archivoTemporal = "";

            try
            {
                if (!File.Exists(rutaPgDump))
                {
                    throw new Exception(
                        "No se encontró pg_dump.exe:\n" +
                        rutaPgDump);
                }

                string nombre =
                    nombrePersonalizado + "_" +
                    DateTime.Now.ToString(
                        "yyyy-MM-dd_HH-mm-ss");

                archivoTemporal =
                    Path.Combine(
                        rutaCarpetaCompletos,
                        nombre +
                        "_temporal.sql");

                rutaArchivo =
                    Path.Combine(
                        rutaCarpetaCompletos,
                        nombre +
                        ".sql");

                // Crear SQL temporal
                EjecutarPgDump(
                    archivoTemporal);

                // Cifrar
                CifradoRespaldo.CifrarArchivo(
                    archivoTemporal,
                    rutaArchivo);

                // Eliminar SQL sin cifrar
                if (File.Exists(archivoTemporal))
                {
                    File.Delete(archivoTemporal);
                }

                return File.Exists(rutaArchivo);
            }
            catch
            {
                if (!string.IsNullOrEmpty(archivoTemporal) &&
                    File.Exists(archivoTemporal))
                {
                    try
                    {
                        File.Delete(archivoTemporal);
                    }
                    catch
                    {
                    }
                }

                rutaArchivo = "";
                throw;
            }
        }

        // =====================================================
        // RESPALDO DIFERENCIAL
        // =====================================================
        public bool CrearRespaldoDiferencial(
            out string rutaArchivo)
        {
            rutaArchivo = "";

            string archivoTemporal = "";

            try
            {
                if (!File.Exists(rutaPgDump))
                {
                    throw new Exception(
                        "No se encontró pg_dump.exe:\n" +
                        rutaPgDump);
                }

                string nombre =
                    "Diferencial_" +
                    DateTime.Now.ToString(
                        "yyyy-MM-dd_HH-mm-ss");

                archivoTemporal =
                    Path.Combine(
                        rutaCarpetaDiferenciales,
                        nombre +
                        "_temporal.sql");

                rutaArchivo =
                    Path.Combine(
                        rutaCarpetaDiferenciales,
                        nombre +
                        ".sql");

                // Crear SQL
                EjecutarPgDump(
                    archivoTemporal);

                // Cifrar
                CifradoRespaldo.CifrarArchivo(
                    archivoTemporal,
                    rutaArchivo);

                // Eliminar SQL original
                if (File.Exists(archivoTemporal))
                {
                    File.Delete(archivoTemporal);
                }

                return File.Exists(rutaArchivo);
            }
            catch
            {
                if (!string.IsNullOrEmpty(archivoTemporal) &&
                    File.Exists(archivoTemporal))
                {
                    try
                    {
                        File.Delete(archivoTemporal);
                    }
                    catch
                    {
                    }
                }

                rutaArchivo = "";
                throw;
            }
        }

        // =====================================================
        // EJECUTAR PG_DUMP
        // =====================================================
        private void EjecutarPgDump(
            string rutaArchivoSQL)
        {
            string host =
                conexionBD.ObtenerHost();

            int port =
                conexionBD.ObtenerPuerto();

            string database =
                conexionBD.ObtenerBaseDatos();

            string user =
                conexionBD.ObtenerUsuario();

            string password =
                conexionBD.ObtenerPassword();

            ProcessStartInfo proceso =
                new ProcessStartInfo();

            proceso.FileName =
                rutaPgDump;

            proceso.Arguments =
                "-h \"" + host + "\" " +
                "-p \"" + port + "\" " +
                "-U \"" + user + "\" " +
                "-F p " +
                "-f \"" + rutaArchivoSQL + "\" " +
                "\"" + database + "\"";

            proceso.UseShellExecute = false;
            proceso.CreateNoWindow = true;

            proceso.RedirectStandardError = true;
            proceso.RedirectStandardOutput = true;

            proceso.EnvironmentVariables["PGPASSWORD"] =
                password;

            using (Process procesoPG =
                   Process.Start(proceso))
            {
                string error =
                    procesoPG.StandardError.ReadToEnd();

                procesoPG.WaitForExit();

                if (procesoPG.ExitCode != 0)
                {
                    throw new Exception(
                        "Error al crear el respaldo:\n" +
                        error);
                }
            }
        }

        // =====================================================
        // RESPALDO INCREMENTAL
        // =====================================================
        public bool CrearRespaldoIncremental(
            out string carpetaIncremental)
        {
            carpetaIncremental = "";

            try
            {
                string fecha =
                    DateTime.Now.ToString(
                        "yyyy-MM-dd_HH-mm-ss");

                carpetaIncremental =
                    Path.Combine(
                        rutaCarpetaIncrementales,
                        "Incremental_" + fecha);

                Directory.CreateDirectory(
                    carpetaIncremental);

                // Forzar cambio de WAL
                using (NpgsqlConnection conexion =
                       new NpgsqlConnection(
                           ObtenerCadenaConexion()))
                {
                    conexion.Open();

                    using (NpgsqlCommand comando =
                           new NpgsqlCommand(
                               "SELECT pg_switch_wal()::text;",
                               conexion))
                    {
                        comando.ExecuteScalar();
                    }
                }

                // Dar tiempo a PostgreSQL
                Thread.Sleep(3000);

                if (!Directory.Exists(rutaCarpetaWAL))
                {
                    throw new Exception(
                        "No existe la carpeta WAL:\n" +
                        rutaCarpetaWAL);
                }

                string[] archivosWAL =
                    Directory.GetFiles(
                        rutaCarpetaWAL,
                        "*",
                        SearchOption.TopDirectoryOnly);

                int cantidad = 0;

                foreach (string archivoWAL in archivosWAL)
                {
                    string nombre =
                        Path.GetFileName(archivoWAL);

                    string archivoTemporal =
                        Path.Combine(
                            carpetaIncremental,
                            nombre);

                    string archivoCifrado =
                        Path.Combine(
                            carpetaIncremental,
                            nombre +
                            ".enc");

                    // Copiar temporalmente
                    File.Copy(
                        archivoWAL,
                        archivoTemporal,
                        true);

                    // Cifrar
                    CifradoRespaldo.CifrarArchivo(
                        archivoTemporal,
                        archivoCifrado);

                    // Borrar temporal
                    if (File.Exists(archivoTemporal))
                    {
                        File.Delete(archivoTemporal);
                    }

                    cantidad++;
                }

                // Crear información temporal
                string informacionTemporal =
                    Path.Combine(
                        carpetaIncremental,
                        "Informacion_temporal.txt");

                string informacionCifrada =
                    Path.Combine(
                        carpetaIncremental,
                        "Informacion.enc");

                File.WriteAllText(
                    informacionTemporal,
                    "RESPALDO INCREMENTAL\r\n" +
                    "=====================\r\n" +
                    "Fecha: " +
                    DateTime.Now.ToString(
                        "dd/MM/yyyy HH:mm:ss") +
                    "\r\n" +
                    "Archivos WAL protegidos: " +
                    cantidad +
                    "\r\n" +
                    "Este respaldo requiere un " +
                    "respaldo completo para su " +
                    "recuperación.\r\n");

                // Cifrar información
                CifradoRespaldo.CifrarArchivo(
                    informacionTemporal,
                    informacionCifrada);

                // Eliminar TXT
                if (File.Exists(
                    informacionTemporal))
                {
                    File.Delete(
                        informacionTemporal);
                }

                return true;
            }
            catch
            {
                carpetaIncremental = "";
                throw;
            }
        }

        // =====================================================
        // MOSTRAR RESPALDOS
        // =====================================================
        public DataTable MostrarRespaldos()
        {
            DataTable tabla =
                new DataTable();

            tabla.Columns.Add("Nombre");
            tabla.Columns.Add("Tipo");
            tabla.Columns.Add("Ruta");
            tabla.Columns.Add("Fecha");
            tabla.Columns.Add("Tamaño");

            AgregarArchivos(
                tabla,
                rutaCarpetaCompletos,
                "Completo");

            AgregarArchivos(
                tabla,
                rutaCarpetaDiferenciales,
                "Diferencial");

            AgregarArchivosRecursivos(
                tabla,
                rutaCarpetaIncrementales,
                "Incremental");

            return tabla;
        }

        // =====================================================
        // ARCHIVOS DIRECTOS
        // =====================================================
        private void AgregarArchivos(
            DataTable tabla,
            string carpeta,
            string tipo)
        {
            if (!Directory.Exists(carpeta))
                return;

            string[] archivos =
                Directory.GetFiles(
                    carpeta,
                    "*.*",
                    SearchOption.TopDirectoryOnly);

            foreach (string archivo in archivos)
            {
                AgregarFila(
                    tabla,
                    archivo,
                    tipo);
            }
        }

        // =====================================================
        // ARCHIVOS RECURSIVOS
        // =====================================================
        private void AgregarArchivosRecursivos(
            DataTable tabla,
            string carpeta,
            string tipo)
        {
            if (!Directory.Exists(carpeta))
                return;

            string[] archivos =
                Directory.GetFiles(
                    carpeta,
                    "*.*",
                    SearchOption.AllDirectories);

            foreach (string archivo in archivos)
            {
                // Solo mostrar archivos cifrados
                if (archivo.EndsWith(".enc",
                    StringComparison.OrdinalIgnoreCase))
                {
                    AgregarFila(
                        tabla,
                        archivo,
                        tipo);
                }
            }
        }

        // =====================================================
        // AGREGAR FILA
        // =====================================================
        private void AgregarFila(
            DataTable tabla,
            string archivo,
            string tipo)
        {
            FileInfo informacion =
                new FileInfo(archivo);

            tabla.Rows.Add(
                informacion.Name,
                tipo,
                informacion.FullName,
                informacion.LastWriteTime.ToString(
                    "dd/MM/yyyy HH:mm:ss"),
                ConvertirTamaño(
                    informacion.Length));
        }

        // =====================================================
        // TAMAÑO
        // =====================================================
        private string ConvertirTamaño(
            long bytes)
        {
            if (bytes < 1024)
            {
                return bytes + " bytes";
            }

            if (bytes <
                1024L * 1024L)
            {
                return (bytes / 1024.0)
                    .ToString("F2") +
                    " KB";
            }

            if (bytes <
                1024L * 1024L * 1024L)
            {
                return (bytes /
                    (1024.0 * 1024.0))
                    .ToString("F2") +
                    " MB";
            }

            return (bytes /
                (1024.0 * 1024.0 * 1024.0))
                .ToString("F2") +
                " GB";
        }

        // =====================================================
        // RESTAURAR
        // SOLO RESPALDOS COMPLETOS
        // =====================================================
        public bool RestaurarRespaldo(
            string rutaArchivo)
        {
            string archivoTemporal = "";

            try
            {
                if (string.IsNullOrWhiteSpace(
                    rutaArchivo))
                {
                    throw new Exception(
                        "No se seleccionó ningún respaldo.");
                }

                if (!File.Exists(rutaArchivo))
                {
                    throw new Exception(
                        "El respaldo seleccionado no existe.");
                }

                string carpetaCompleta =
                    Path.GetFullPath(
                        rutaCarpetaCompletos);

                string seleccionado =
                    Path.GetFullPath(
                        rutaArchivo);

                if (!seleccionado.StartsWith(
                    carpetaCompleta +
                    Path.DirectorySeparatorChar,
                    StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception(
                        "Solo se pueden restaurar " +
                        "respaldos completos.");
                }

                if (!seleccionado.EndsWith(
                    ".sql",
                    StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception(
                        "El archivo seleccionado " +
                        "no es un respaldo SQL.");
                }

                if (!File.Exists(rutaPsql))
                {
                    throw new Exception(
                        "No se encontró psql.exe:\n" +
                        rutaPsql);
                }

                // Crear SQL temporal
                archivoTemporal =
                    Path.Combine(
                        Path.GetTempPath(),
                        "Restauracion_" +
                        Guid.NewGuid().ToString("N") +
                        ".sql");

                // Descifrar
                CifradoRespaldo.DescifrarArchivo(
                    rutaArchivo,
                    archivoTemporal);

                string host =
                    conexionBD.ObtenerHost();

                int port =
                    conexionBD.ObtenerPuerto();

                string database =
                    conexionBD.ObtenerBaseDatos();

                string user =
                    conexionBD.ObtenerUsuario();

                string password =
                    conexionBD.ObtenerPassword();

                // Limpiar conexión de la aplicación
                try
                {
                    conexionBD.CerrarConexion();
                }
                catch
                {
                }

                // Eliminar esquema
                using (NpgsqlConnection conexion =
                       new NpgsqlConnection(
                           ObtenerCadenaConexion()))
                {
                    conexion.Open();

                    using (NpgsqlCommand comando =
                           new NpgsqlCommand(
                               "DROP SCHEMA public CASCADE;" +
                               "CREATE SCHEMA public;",
                               conexion))
                    {
                        comando.ExecuteNonQuery();
                    }
                }

                // Restaurar
                ProcessStartInfo proceso =
                    new ProcessStartInfo();

                proceso.FileName =
                    rutaPsql;

                proceso.Arguments =
                    "--host=\"" + host + "\" " +
                    "--port=\"" + port + "\" " +
                    "--username=\"" + user + "\" " +
                    "--dbname=\"" + database + "\" " +
                    "-v ON_ERROR_STOP=1 " +
                    "--file=\"" + archivoTemporal + "\"";

                proceso.UseShellExecute = false;
                proceso.CreateNoWindow = true;

                proceso.RedirectStandardError = true;
                proceso.RedirectStandardOutput = true;

                proceso.EnvironmentVariables["PGPASSWORD"] =
                    password;

                using (Process procesoPSQL =
                       Process.Start(proceso))
                {
                    string error =
                        procesoPSQL.StandardError.ReadToEnd();

                    procesoPSQL.WaitForExit();

                    if (procesoPSQL.ExitCode != 0)
                    {
                        throw new Exception(
                            "Error durante la restauración:\n" +
                            error);
                    }
                }

                return true;
            }
            finally
            {
                // Borrar SQL temporal
                if (!string.IsNullOrEmpty(
                    archivoTemporal) &&
                    File.Exists(archivoTemporal))
                {
                    try
                    {
                        File.Delete(
                            archivoTemporal);
                    }
                    catch
                    {
                    }
                }
            }
        }

        // =====================================================
        // COPIAR RESPALDO
        // =====================================================
        public bool CopiarRespaldo(
            string origen,
            string destino)
        {
            if (!File.Exists(origen))
            {
                throw new Exception(
                    "El respaldo no existe.");
            }

            File.Copy(
                origen,
                destino,
                true);

            return true;
        }

        // =====================================================
        // ELIMINAR RESPALDO
        // =====================================================
        public bool EliminarRespaldo(
            string rutaArchivo)
        {
            if (!File.Exists(rutaArchivo))
            {
                throw new Exception(
                    "El respaldo no existe.");
            }

            File.Delete(rutaArchivo);

            return true;
        }

        // =====================================================
        // CADENA DE CONEXIÓN
        // =====================================================
        private string ObtenerCadenaConexion()
        {
            string host =
                conexionBD.ObtenerHost();

            int port =
                conexionBD.ObtenerPuerto();

            string database =
                conexionBD.ObtenerBaseDatos();

            string user =
                conexionBD.ObtenerUsuario();

            string password =
                conexionBD.ObtenerPassword();

            return
                "Host=" + host + ";" +
                "Port=" + port + ";" +
                "Database=" + database + ";" +
                "Username=" + user + ";" +
                "Password=" + password + ";";
        }
    }
}
