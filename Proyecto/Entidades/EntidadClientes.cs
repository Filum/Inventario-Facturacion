using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EntidadClientes
    {
        public int v_Codigo { get; set; }
        public string v_NombreCompleto { get ; set ; }
        public string v_Correo { get ; set ; }
        public string v_CorreoOpc { get; set ; }
        public int v_Teleoficina { get; set ; }
        public int v_Telemovil { get ; set ; }
        public int v_Inactividad { get; set ; }
        public string v_Observaciones { get; set ; }

        public EntidadClientes()
        {

        }

        public EntidadClientes(int v_Codigo, string v_NombreCompleto, string v_Correo, string v_CorreoOpc, int v_Teleoficina, int v_Telemovil, Int32 v_Inactividad, string v_Observaciones)
        {
            this.v_Codigo = v_Codigo;
            this.v_NombreCompleto = v_NombreCompleto;
            this.v_Correo = v_Correo;
            this.v_CorreoOpc = v_CorreoOpc;
            this.v_Teleoficina = v_Teleoficina;
            this.v_Telemovil = v_Telemovil;
            this.v_Inactividad = v_Inactividad;
            this.v_Observaciones = v_Observaciones;
        }
    }
}
