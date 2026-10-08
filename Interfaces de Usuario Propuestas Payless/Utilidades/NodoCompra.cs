using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless.Utilidades
{
    internal class NodoCompra
    {
        public int IdCompra { get; set; }
        public DateTime Fecha { get; set; }
        public string Proveedor { get; set; }
        public decimal Total { get; set; }
        public bool Estado { get; set; }

        public NodoCompra Izquierdo { get; set; }
        public NodoCompra Derecho { get; set; }

        public NodoCompra(
            int idCompra,
            DateTime fecha,
            string proveedor,
            decimal total,
            bool estado)
        {
            IdCompra = idCompra;
            Fecha = fecha;
            Proveedor = proveedor;
            Total = total;
            Estado = estado;

            Izquierdo = null;
            Derecho = null;
        }

    }
}
