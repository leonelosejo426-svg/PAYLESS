using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    internal class ClaseInventario
    {

        private int _id_inventario;
        private int _id_producto;
        private int _stockactual;
        private int _stockminimo;
        private string _unidadmedida;
        private DateTime _fecha_actualizacion;

        internal NivelacionInventario NivelacionInventario
        {
            get => default;
            set
            {
            }
        }
    }
}
