using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless.Utilidades
{
    internal class NodoDetalleVenta
    {
            public int IdProductoTalla { get; set; }
            public string Producto { get; set; }
            public string Categoria { get; set; }
            public string Marca { get; set; }
            public string Talla { get; set; }
            public decimal PrecioVenta { get; set; }
            public int Cantidad { get; set; }
            public decimal Subtotal { get; set; }

            public NodoDetalleVenta Siguiente { get; set; }

            public NodoDetalleVenta(
                int idProductoTalla,
                string producto,
                string categoria,
                string marca,
                string talla,
                decimal precioVenta,
                int cantidad,
                decimal subtotal)
            {
                IdProductoTalla = idProductoTalla;
                Producto = producto;
                Categoria = categoria;
                Marca = marca;
                Talla = talla;
                PrecioVenta = precioVenta;
                Cantidad = cantidad;
                Subtotal = subtotal;

                Siguiente = null;
            }


        }
}
