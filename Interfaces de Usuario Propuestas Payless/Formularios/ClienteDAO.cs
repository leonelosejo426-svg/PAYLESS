using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Interfaces_de_Usuario_Propuestas_Payless.Cliente;

namespace Interfaces_de_Usuario_Propuestas_Payless.Formularios
{
    internal class ClienteDAO
    {
        ConexionBD conexionBD = new ConexionBD();

        public DataTable MostrarClientes()
        {
            return new DataTable();
        }

        public bool AgregarCliente(cliente cliente)
        {
            return false;
        }

        public bool EditarCliente(cliente cliente)
        {
            return false;
        }

        public bool EliminarCliente(int idCliente)
        {
            return false;
        }

        public cliente ObtenerCliente(int idCliente)
        {
            return null;
        }

        public DataTable BuscarClientes(string campo, string dato)
        {
            return new DataTable();
        }
    }
}