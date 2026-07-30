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
     
    
     
     
     
     private string _codigo;
     
     private DateTime _fechaCreacion;
     private DateTime _fechaActualizacion;
     
     private bool _estado_producto;
     
     private int _idCategoria;
     private int _idMarca;
     private int _idProveedor;
     private int Id_inventario;

        internal ClaseDetalleVenta ClaseDetalleVenta
        {
            get => default;
            set
            {
            }
        }

        internal ClaseDetalleCompra ClaseDetalleCompra
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
