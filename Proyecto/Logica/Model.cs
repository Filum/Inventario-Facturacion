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

        public int AgregarProveedores(EntidadProveedores clt)
        {
            return data.AgregarProveedores(clt);
        }
        public List<EntidadClientes> BuscarClientes(String nombre)
        {
            return data.BuscarClientes(nombre);
        }

        public DataTable mostrarListaProveedores()
        {
            return data.MostarListaProveedores();
        }

        public DataTable MostrarListaClientes()
        {
            return data.MostarListaClientes();
        }
        public int validar_cedJur_proveedores(long v_CedJur)
        {
            return data.validar_cedJur_proveedores(v_CedJur);
        }
    }
}
