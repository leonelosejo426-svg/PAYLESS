using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
     class Clase_Usuario
    {
        
        private int _idUsuario;
        private string _nombreUsuario;
        private string _passwordHash;
        private int _idRol;
    }

    class claseLogin
    {
        private int _idLogin;
        private DateTime _fecha;
        private int _idUsuario;


    }

    class Recuperacion

    {
        private int _idRecuperacion;
        private string _tokén;
        private int _idVenta;
    }

    class ClaseDevolucionVenta

    {
        private int _idDevolucion;
        private decimal _monto;
        private int _idVenta;

    }



}

    

