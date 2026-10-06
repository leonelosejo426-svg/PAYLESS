using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless.Utilidades
{
    internal class ListaEnlazadaDetalleVenta
    {
        private NodoDetalleVenta cabeza;

        public ListaEnlazadaDetalleVenta()  
        {
            cabeza = null;
        }

        // Agregar un producto al final
        public void Agregar(NodoDetalleVenta nuevoNodo)
        {
            if (cabeza == null)
            {
                cabeza = nuevoNodo;
                return;
            }

            NodoDetalleVenta actual = cabeza;

            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }

            actual.Siguiente = nuevoNodo;
        }

        // Eliminar el último producto
        public bool EliminarUltimo()
        {
            if (cabeza == null)
                return false;

            if (cabeza.Siguiente == null)
            {
                cabeza = null;
                return true;
            }

            NodoDetalleVenta actual = cabeza;

            while (actual.Siguiente.Siguiente != null)
            {
                actual = actual.Siguiente;
            }

            actual.Siguiente = null;

            return true;
        }

        // Eliminar toda la lista
        public void Limpiar()
        {
            cabeza = null;
        }

        // Saber si está vacía
        public bool EstaVacia()
        {
            return cabeza == null;
        }

        // Cantidad de productos agregados
        public int Contar()
        {
            int contador = 0;

            NodoDetalleVenta actual = cabeza;

            while (actual != null)
            {
                contador++;
                actual = actual.Siguiente;
            }

            return contador;
        }

        // Obtener subtotal total de la venta
        public decimal ObtenerSubtotal()
        {
            decimal subtotal = 0;

            NodoDetalleVenta actual = cabeza;

            while (actual != null)
            {
                subtotal += actual.Subtotal;
                actual = actual.Siguiente;
            }

            return subtotal;
        }

        // Convertir la lista enlazada a DataTable
        // para enviarla posteriormente a la pantalla de pago.
        // Convertir la lista enlazada a DataTable
        // para enviarla posteriormente a la pantalla de pago.
        public DataTable CrearDetalleVenta()
        {
            DataTable tabla = new DataTable();

            tabla.Columns.Add("id_producto_talla", typeof(int));
            tabla.Columns.Add("cantidad", typeof(int));
            tabla.Columns.Add("precio_venta", typeof(decimal));
            tabla.Columns.Add("subtotal", typeof(decimal));

            NodoDetalleVenta actual = cabeza;

            while (actual != null)
            {
                DataRow nuevaFila = tabla.NewRow();

                // Se asignan las propiedades directamente desde el nodo de la lista enlazada
                nuevaFila["id_producto_talla"] = actual.IdProductoTalla; // Asegúrate de que el nombre de la propiedad coincida con el de tu clase NodoDetalleVenta
                nuevaFila["cantidad"] = actual.Cantidad;
                nuevaFila["precio_venta"] = actual.PrecioVenta;
                nuevaFila["subtotal"] = actual.Subtotal;

                tabla.Rows.Add(nuevaFila);

                actual = actual.Siguiente;
            }

            return tabla;
        }


    }
}
