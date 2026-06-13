using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    internal class ClaseProveedor
    {

        public class Proveedor
        {
            public string RUC { get; set; }
            public string Nombre { get; set; }
            public string Telefono { get; set; }
            public string Direccion { get; set; }
            public string Estado { get; set; }
        }










        private int _idProveedor;
        private string _nombre;
        private string _telefono;
        private string _correo;
        private string _direccion;

        private bool _estadoProveedor;

        private string _ruc;

        private DateTime _fechaRegistro;

        internal ClaseProducto ClaseProducto
        {
            get => default;
            set
            {
            }
        }

        internal ClaseCompra ClaseCompra
        {
            get => default;
            set
            {
            }
        }
    }
}
