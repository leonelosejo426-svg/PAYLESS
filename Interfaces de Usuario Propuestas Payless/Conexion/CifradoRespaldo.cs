using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless.Conexion
{
    internal class CifradoRespaldo
    {

        // ==========================================================
        // CLAVE DE CIFRADO
        // ==========================================================

        private const string CLAVE =
            "LeonelF_241207";

        private const int ITERACIONES = 100000;

        // =====================================================
        // CIFRAR ARCHIVO
        // =====================================================
        public static void CifrarArchivo(
            string archivoOriginal,
            string archivoCifrado)
        {
            if (!File.Exists(archivoOriginal))
            {
                throw new FileNotFoundException(
                    "No se encontró el archivo que se desea cifrar.",
                    archivoOriginal);
            }

            byte[] salt = new byte[32];

            using (RandomNumberGenerator rng =
                   RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using (Rfc2898DeriveBytes derivador =
                   new Rfc2898DeriveBytes(
                       CLAVE,
                       salt,
                       ITERACIONES))
            {
                byte[] clave =
                    derivador.GetBytes(32);

                byte[] iv =
                    derivador.GetBytes(16);

                using (FileStream entrada =
                       new FileStream(
                           archivoOriginal,
                           FileMode.Open,
                           FileAccess.Read))
                using (FileStream salida =
                       new FileStream(
                           archivoCifrado,
                           FileMode.Create,
                           FileAccess.Write))
                {
                    // Guardar salt
                    salida.Write(
                        salt,
                        0,
                        salt.Length);

                    // Guardar IV
                    salida.Write(
                        iv,
                        0,
                        iv.Length);

                    using (Aes aes = Aes.Create())
                    {
                        aes.Key = clave;
                        aes.IV = iv;
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;

                        using (CryptoStream crypto =
                               new CryptoStream(
                                   salida,
                                   aes.CreateEncryptor(),
                                   CryptoStreamMode.Write))
                        {
                            entrada.CopyTo(crypto);
                        }
                    }
                }
            }
        }

        // =====================================================
        // DESCIFRAR ARCHIVO
        // =====================================================
        public static void DescifrarArchivo(
            string archivoCifrado,
            string archivoDestino)
        {
            if (!File.Exists(archivoCifrado))
            {
                throw new FileNotFoundException(
                    "No se encontró el archivo cifrado.",
                    archivoCifrado);
            }

            using (FileStream entrada =
                   new FileStream(
                       archivoCifrado,
                       FileMode.Open,
                       FileAccess.Read))
            {
                byte[] salt = new byte[32];

                int cantidadSalt =
                    entrada.Read(
                        salt,
                        0,
                        salt.Length);

                if (cantidadSalt != salt.Length)
                {
                    throw new Exception(
                        "El archivo cifrado está dañado.");
                }

                byte[] iv = new byte[16];

                int cantidadIV =
                    entrada.Read(
                        iv,
                        0,
                        iv.Length);

                if (cantidadIV != iv.Length)
                {
                    throw new Exception(
                        "El archivo cifrado está dañado.");
                }

                using (Rfc2898DeriveBytes derivador =
                       new Rfc2898DeriveBytes(
                           CLAVE,
                           salt,
                           ITERACIONES))
                {
                    byte[] clave =
                        derivador.GetBytes(32);

                    using (FileStream salida =
                           new FileStream(
                               archivoDestino,
                               FileMode.Create,
                               FileAccess.Write))
                    {
                        using (Aes aes = Aes.Create())
                        {
                            aes.Key = clave;
                            aes.IV = iv;
                            aes.Mode = CipherMode.CBC;
                            aes.Padding =
                                PaddingMode.PKCS7;

                            using (CryptoStream crypto =
                                   new CryptoStream(
                                       entrada,
                                       aes.CreateDecryptor(),
                                       CryptoStreamMode.Read))
                            {
                                crypto.CopyTo(salida);
                            }
                        }
                    }
                }
            }
        }
    }
}
