using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public class ClaseUsuario
    {





        private int _idUsuario;

        private string _nombre;

        private string _username;

        private string _password;

        private bool _estado;

        private int _idRol;
        

        // Usuario con múltiples validaciones en una sola línea
        public string Usuario
        {
            get => _username;
            set => _username =
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
            get => _password;
            set => _password =
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

        internal ClaseVenta ClaseVenta
        {
            get => default;
            set
            {
            }
        }

        internal ClaseCaja ClaseCaja
        {
            get => default;
            set
            {
            }
        }

        // Constructor
        public ClaseUsuario(string usuario, string contraseña)
        {
            this.Usuario = usuario;
            this.Contraseña = contraseña;
           

        }



    }
}
