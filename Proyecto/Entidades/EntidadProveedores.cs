using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entidades
{
    //Clase la cual va a ser utilizada para enviar datos al Data con el fin de cumplir diferentes actividades
    public class EntidadProveedores
    {
        public EntidadProveedores()
        {

        }
        public Int64 v_IdProveedor { set; get; }
        public Int64 v_CedulaJuridica { set; get; }
        public string v_Nombre { set; get; }
        public string v_Correo { set; get; }
        public string v_CorreoOpcional { set; get; }
        public string v_Descripcion { set; get; }
        public Int64 v_Telefono { set; get; }
        public Int64 v_TelefonoOpcional { set; get; }
        public DateTime v_Fecha { set; get; }
        public string v_EstadoSistema { set; get; }


        public EntidadProveedores(Int64 v_IdProveedor, Int64 v_CedulaJuridica, string v_Nombre, string v_Correo, string v_CorreoOpcional, string v_Descripcion, Int64 v_Telefono, Int64 v_TelefonoOpcional, DateTime v_Fecha, string v_EstadoSistema)
        {
            this.v_IdProveedor = v_IdProveedor;
            this.v_CedulaJuridica = v_CedulaJuridica;
            this.v_Nombre = v_Nombre;
            this.v_Correo = v_Correo;
            this.v_CorreoOpcional = v_CorreoOpcional;
            this.v_Descripcion = v_Descripcion;
            this.v_Telefono = v_Telefono;
            this.v_TelefonoOpcional = v_TelefonoOpcional;
            this.v_Fecha = v_Fecha;
            this.v_EstadoSistema = v_EstadoSistema;
        }
    }
}

