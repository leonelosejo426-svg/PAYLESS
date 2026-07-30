using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    internal class ClaseCaja
    {

        private int _idCaja;

        private DateTime _fechaApertura;
        private DateTime _fechaCierre;

        private decimal _saldoInicial;
        private decimal _saldoFinal;
        private decimal _tipoCambio;

        private int _idUsuario;

        internal ClaseEgresoCaja ClaseEgresoCaja
        {
            get => default;
            set
            {
            }
        }

        internal ClaseVenta ClaseVenta
        {
            get => default;
            set
            {
            }
        }
    }
}
