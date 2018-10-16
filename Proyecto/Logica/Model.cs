using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
using System.Data;

namespace Logica
{
    public class Model
    {
        Data data = new Data();

        public int AgregarClientes(EntidadClientes clt)
        {
            return data.AgregarClientes(clt);
        }

        public int ModificarClientes(EntidadClientes clt)
        {
            return data.ModificarClientes(clt);
        }

        public int EliminarClientes(EntidadClientes clt)
        {
            return data.EliminarClientes(clt);
        }

        public int AgregarProveedores(EntidadProveedores clt)
        {
            return data.AgregarProveedores(clt);
        }

        public int ListarProveedores(EntidadProveedores clt)
        {
            return data.ListarProveedores(clt);
        }

        public DataTable MostrarListaClientes()
        {
            return data.MostarListaClientes();
        }
    }
}
