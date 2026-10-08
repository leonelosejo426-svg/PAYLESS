using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless.Utilidades
{
    internal class ListaEnlazadaDetalleCompra
    {
        private NodoDetalleCompra primero;
        private NodoDetalleCompra ultimo;

        public void Agregar(NodoDetalleCompra nuevo)
        {
            if (primero == null)
            {
                primero = nuevo;
                ultimo = nuevo;
            }
            else
            {
                ultimo.Siguiente = nuevo;
                ultimo = nuevo;
            }
        }

        public NodoDetalleCompra EliminarUltimo()
        {
            if (primero == null)
                return null;

            if (primero == ultimo)
            {
                NodoDetalleCompra eliminado = primero;

                primero = null;
                ultimo = null;

                return eliminado;
            }

            NodoDetalleCompra actual = primero;

            while (actual.Siguiente != ultimo)
            {
                actual = actual.Siguiente;
            }

            NodoDetalleCompra eliminadoUltimo = ultimo;

            ultimo = actual;
            ultimo.Siguiente = null;

            return eliminadoUltimo;
        }

        public bool EstaVacia()
        {
            return primero == null;
        }

        public decimal ObtenerSubtotal()
        {
            decimal subtotal = 0;

            NodoDetalleCompra actual = primero;

            while (actual != null)
            {
                subtotal += actual.Subtotal;
                actual = actual.Siguiente;
            }

            return subtotal;
        }

        public int CantidadElementos()
        {
            int cantidad = 0;

            NodoDetalleCompra actual = primero;

            while (actual != null)
            {
                cantidad++;
                actual = actual.Siguiente;
            }

            return cantidad;
        }

        public NodoDetalleCompra ObtenerPrimero()
        {
            return primero;
        }

        public void Limpiar()
        {
            primero = null;
            ultimo = null;
        }
    }
}

