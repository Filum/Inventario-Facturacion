using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    class EntidadBitacora
    {
        public string fecha { get; set; }
        public string accion { get; set; }
        public string usuario_Responsable { get; set; }
        public string ventana_Mantenimiento { get; set; }
        public string descripcion { get; set; }
    }
}
