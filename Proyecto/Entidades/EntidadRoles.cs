using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EntidadRoles
    {

        public EntidadRoles()
        {

        }

        public Int64 v_IdRol { set; get; }
        public DateTime v_Fecha { set; get; }
        public string v_Nombre { set; get; }
        public string v_Mantenimientos { set; get; }
        public string v_Estado { set; get; }

        public EntidadRoles(Int64 v_IdRol, DateTime v_Fecha, string v_Nombre, string v_Mantenimientos, string v_Estado)
        {
            this.v_IdRol = v_IdRol;
            this.v_Fecha = v_Fecha;
            this.v_Nombre = v_Nombre;
            this.v_Mantenimientos = v_Mantenimientos;
            this.v_Estado = v_Estado;
        }

    }
}
