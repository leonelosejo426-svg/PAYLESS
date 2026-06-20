using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    class Clase_credito
    {
        private int _idCredito;

        private decimal _monto;

        private DateTime _fechaInicio;
        private DateTime _fechaLimite;

        private string _estado;

        private int _idCliente;

        internal ClaseDetalleVenta ClaseDetalleVenta
        {
            get => default;
            set
            {
            }
        }

        internal ClaseFormaPago ClaseFormaPago
        {
            get => default;
            set
            {
            }
        }
    }
}
