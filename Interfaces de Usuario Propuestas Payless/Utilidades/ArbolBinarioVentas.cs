using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless.Utilidades
{
    internal class ArbolBinarioVentas
    {


            public NodoVenta Raiz { get; set; }

            public ArbolBinarioVentas()
            {
                Raiz = null;
            }

            // ============================================================
            // INSERTAR
            // ============================================================

            public void Insertar(DataRow venta)
            {
                Raiz = InsertarRecursivo(Raiz, venta);
            }

            private NodoVenta InsertarRecursivo(
                NodoVenta actual,
                DataRow venta)
            {
                if (actual == null)
                    return new NodoVenta(venta);

                int idNuevo = Convert.ToInt32(venta["id_venta"]);
                int idActual = Convert.ToInt32(actual.Venta["id_venta"]);

                if (idNuevo < idActual)
                {
                    actual.Izquierdo =
                        InsertarRecursivo(actual.Izquierdo, venta);
                }
                else if (idNuevo > idActual)
                {
                    actual.Derecho =
                        InsertarRecursivo(actual.Derecho, venta);
                }

                return actual;
            }

            // ============================================================
            // RECORRIDO INORDEN
            // ============================================================

            public List<DataRow> RecorridoInOrden()
            {
                List<DataRow> resultados = new List<DataRow>();

                RecorridoInOrdenRecursivo(Raiz, resultados);

                return resultados;
            }

            private void RecorridoInOrdenRecursivo(
                NodoVenta actual,
                List<DataRow> resultados)
            {
                if (actual == null)
                    return;

                RecorridoInOrdenRecursivo(
                    actual.Izquierdo,
                    resultados);

                resultados.Add(actual.Venta);

                RecorridoInOrdenRecursivo(
                    actual.Derecho,
                    resultados);
            }

            // ============================================================
            // BUSCAR POR RECORRIDO
            // ============================================================

            public List<DataRow> Buscar(
                string campo,
                string valor)
            {
                List<DataRow> resultados = new List<DataRow>();

                BuscarRecursivo(
                    Raiz,
                    campo,
                    valor,
                    resultados);

                return resultados;
            }

            private void BuscarRecursivo(
                NodoVenta actual,
                string campo,
                string valor,
                List<DataRow> resultados)
            {
                if (actual == null)
                    return;

                // Primero recorremos izquierda
                BuscarRecursivo(
                    actual.Izquierdo,
                    campo,
                    valor,
                    resultados);

                // Revisamos la venta actual
                string dato = "";

                if (campo == "Código")
                {
                    dato = actual.Venta["codigo_venta"].ToString();
                }
                else if (campo == "Cliente")
                {
                    dato = actual.Venta["cliente"].ToString();
                }
                else if (campo == "Fecha")
                {
                    dato = actual.Venta["fecha"].ToString();
                }
                else if (campo == "ID")
                {
                    dato = actual.Venta["id_venta"].ToString();
                }

                if (dato.IndexOf(
                    valor,
                    StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    resultados.Add(actual.Venta);
                }

                // Finalmente recorremos derecha
                BuscarRecursivo(
                    actual.Derecho,
                    campo,
                    valor,
                    resultados);
            }   
    }
}
