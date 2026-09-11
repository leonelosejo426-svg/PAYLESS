using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless.Utilidades
{
    internal class NodoVenta
    {

        public DataRow Venta { get; set; }

        public NodoVenta Izquierdo { get; set; }
        public NodoVenta Derecho { get; set; }

        public NodoVenta(DataRow venta)
        {
            Venta = venta;
            Izquierdo = null;
            Derecho = null;
        }

    }
}
