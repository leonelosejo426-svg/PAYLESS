using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless.Utilidades
{
    internal class RecursividadArqueo
    {

        /// <summary>
        /// Calcula la suma total de un conjunto de billetes/monedas recorriendo
        /// los arreglos de cantidades y valores de forma RECURSIVA.
        /// </summary>
        public static decimal SumarDenominacionesRecursivo(int[] cantidades, decimal[] valores, int indice = 0)
        {
            if (cantidades == null || valores == null || indice >= cantidades.Length || indice >= valores.Length)
                return 0m;

            decimal subtotalActual = cantidades[indice] * valores[indice];
            return subtotalActual + SumarDenominacionesRecursivo(cantidades, valores, indice + 1);
        }

        // 2. Suma recursiva para la lista de subtotales finales
        public static decimal SumarListaRecursivo(List<decimal> subtotales, int indice = 0)
        {
            if (subtotales == null || indice >= subtotales.Count)
                return 0m;

            return subtotales[indice] + SumarListaRecursivo(subtotales, indice + 1);
        }

        // 3. Evaluación de Faltante y Sobrante (Retorna Faltante o Sobrante segun corresponda)
        public static (decimal faltante, decimal sobrante) CalcularDiferenciaRecursivo(decimal esperado, decimal real)
        {
            decimal diferencia = real - esperado;

            if (diferencia < 0)
                return (Math.Abs(diferencia), 0m); // Existe Faltante
            else if (diferencia > 0)
                return (0m, diferencia);          // Existe Sobrante

            return (0m, 0m); // Caja cuadrada
        }
    }
}
