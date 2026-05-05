using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    internal class ClaseVenta
    {

        private int _idVenta;
        private DateTime _fecha;
        private decimal _total;
        private bool _estadoVenta;

        private int _idCliente;
        private int _idUsuario;
        private int _idCaja;

        internal ClaseDevolucionVenta ClaseDevolucionVenta
        {
            get => default;
            set
            {
            }
        }

        internal ClaseDetalleVenta ClaseDetalleVenta
        {
            get => default;
            set
            {
            }
        }
    }
}
