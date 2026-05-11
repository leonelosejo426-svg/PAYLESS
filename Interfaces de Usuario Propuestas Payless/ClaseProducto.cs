using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    internal class ClaseProducto
    {

        private int _idProducto;
        private string _nombre;
        private decimal _precio;
        private int _stock;
        private string _codigo;
        private DateTime _fechaCreacion;
        private DateTime _fechaActualizacion;
        private bool _estadoProducto;

        private int _idCategoria;
        private int _idProveedor;
        private int _idMarca;

        internal ClaseDetalleCompra ClaseDetalleCompra
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

        internal ClaseInventario ClaseInventario
        {
            get => default;
            set
            {
            }
        }
    }
}
