using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    class ClaseCliente
    {

        private int _idCliente;

        private string _nombre;

        private string _telefono;

        private string _direccion;

        private bool _estado;

        internal ClaseVenta ClaseVenta
        {
            get => default;
            set
            {
            }
        }

        internal Clase_credito Clase_credito
        {
            get => default;
            set
            {
            }
        }
    }
}
