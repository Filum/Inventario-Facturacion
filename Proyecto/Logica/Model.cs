using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;

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
    }
}
