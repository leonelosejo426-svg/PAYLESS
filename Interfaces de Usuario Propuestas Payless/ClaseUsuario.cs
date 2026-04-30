using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public class ClaseUsuario
    {





        private string _nombreCompleto;
        private string _estado;
        private string telefono;
        private string _rol;
        private string _direccion;
        private string _cedula;
        private string Username;
        private string Password;


        

        

        // Usuario con múltiples validaciones en una sola línea
        public string Usuario
        {
            get => Username;
            set => Username =
                (!string.IsNullOrWhiteSpace(value) &&
                 value.Length >= 4 &&
                 value.Length <= 15 &&
                 !value.Contains(" ") &&
                 value.All(char.IsLetterOrDigit))
                ? value
                : throw new Exception("Usuario inválido: debe tener 4-15 caracteres, sin espacios y solo letras/números");
        }

        // Contraseña con múltiples validaciones en una sola línea
        public string Contraseña
        {
            get => Password;
            set => Password =
                (!string.IsNullOrWhiteSpace(value) &&
                 value.Length >= 6 &&
                 value.Length <= 40 &&
                 value.Any(char.IsUpper) &&   // al menos una mayúscula
                 value.Any(char.IsLower) &&   // al menos una minúscula
                 value.Any(char.IsDigit) &&   // al menos un número
                 value.Any(ch => "!@#$%^&*_-".Contains(ch)) && // carácter especial
                 !value.Contains(" "))        // sin espacios
                ? value
                : throw new Exception("Contraseña inválida: debe tener mayúscula, minúscula, número, símbolo y 6-40 caracteres");
        }

        // Constructor
        public ClaseUsuario(string usuario, string contraseña)
        {
            this.Usuario = usuario;
            this.Contraseña = contraseña;
           

        }



    }
}
