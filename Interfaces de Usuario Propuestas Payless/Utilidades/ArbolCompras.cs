using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless.Utilidades
{
    internal class ArbolCompras
    {
        private NodoCompra raiz;

        public ArbolCompras()
        {
            raiz = null;
        }

        // ============================
        // INSERTAR
        // ============================
        public void Insertar(NodoCompra nuevaCompra)
        {
            raiz = InsertarRecursivo(raiz, nuevaCompra);
        }

        private NodoCompra InsertarRecursivo(
            NodoCompra actual,
            NodoCompra nuevaCompra)
        {
            if (actual == null)
            {
                return nuevaCompra;
            }

            if (nuevaCompra.IdCompra < actual.IdCompra)
            {
                actual.Izquierdo =
                    InsertarRecursivo(actual.Izquierdo, nuevaCompra);
            }
            else if (nuevaCompra.IdCompra > actual.IdCompra)
            {
                actual.Derecho =
                    InsertarRecursivo(actual.Derecho, nuevaCompra);
            }

            return actual;
        }

        // ============================
        // BUSCAR
        // ============================
        public NodoCompra Buscar(int idCompra)
        {
            return BuscarRecursivo(raiz, idCompra);
        }

        private NodoCompra BuscarRecursivo(
            NodoCompra actual,
            int idCompra)
        {
            if (actual == null)
            {
                return null;
            }

            if (idCompra == actual.IdCompra)
            {
                return actual;
            }

            if (idCompra < actual.IdCompra)
            {
                return BuscarRecursivo(
                    actual.Izquierdo,
                    idCompra);
            }

            return BuscarRecursivo(
                actual.Derecho,
                idCompra);
        }

        // ============================
        // ELIMINAR
        // ============================
        public void Eliminar(int idCompra)
        {
            raiz = EliminarRecursivo(raiz, idCompra);
        }

        private NodoCompra EliminarRecursivo(
            NodoCompra actual,
            int idCompra)
        {
            if (actual == null)
            {
                return null;
            }

            if (idCompra < actual.IdCompra)
            {
                actual.Izquierdo =
                    EliminarRecursivo(
                        actual.Izquierdo,
                        idCompra);

                return actual;
            }

            if (idCompra > actual.IdCompra)
            {
                actual.Derecho =
                    EliminarRecursivo(
                        actual.Derecho,
                        idCompra);

                return actual;
            }

            // Caso 1: no tiene hijos
            if (actual.Izquierdo == null &&
                actual.Derecho == null)
            {
                return null;
            }

            // Caso 2: solo tiene hijo derecho
            if (actual.Izquierdo == null)
            {
                return actual.Derecho;
            }

            // Caso 3: solo tiene hijo izquierdo
            if (actual.Derecho == null)
            {
                return actual.Izquierdo;
            }

            // Caso 4: tiene dos hijos
            NodoCompra sucesor =
                ObtenerMenor(actual.Derecho);

            actual.IdCompra = sucesor.IdCompra;
            actual.Fecha = sucesor.Fecha;
            actual.Proveedor = sucesor.Proveedor;
            actual.Total = sucesor.Total;
            actual.Estado = sucesor.Estado;

            actual.Derecho =
                EliminarRecursivo(
                    actual.Derecho,
                    sucesor.IdCompra);

            return actual;
        }

        private NodoCompra ObtenerMenor(NodoCompra actual)
        {
            NodoCompra temporal = actual;

            while (temporal.Izquierdo != null)
            {
                temporal = temporal.Izquierdo;
            }

            return temporal;
        }

        // ============================
        // LIMPIAR ÁRBOL
        // ============================
        public void Limpiar()
        {
            raiz = null;
        }

    }
}
