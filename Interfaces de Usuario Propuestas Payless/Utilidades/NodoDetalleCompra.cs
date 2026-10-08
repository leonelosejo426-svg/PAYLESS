using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless.Utilidades
{
    internal class NodoDetalleCompra
    {
        public int IdProducto { get; set; }
        public int IdProductoTalla { get; set; }
        public string Producto { get; set; }
        public string Categoria { get; set; }
        public string Marca { get; set; }
        public string Talla { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public NodoDetalleCompra Siguiente { get; set; }
    }
}
