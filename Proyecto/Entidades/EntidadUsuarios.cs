using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EntidadUsuarios
    {
        public EntidadUsuarios()
        {

        }

        public Int64 v_IdUsuario { set; get; }
        public Int64 v_CedIdentificacion { set; get; }
        public string v_NombreUsuario { set; get; }
        public string v_Apellidos { set; get; }
        public Int64 v_TelefonoHabitacion { set; get; }
        public Int64 v_TelefonoMovil { set; get; }
        public string v_Correo { set; get; }
        public string v_Puesto { set; get; }
        public Int64 v_IdRol { set; get; }
        public string v_UsuarioSistema { set; get; }
        public string v_Contrasena { set; get; }

        public string v_estadoSistema { set; get; }

        public EntidadUsuarios(Int64 v_IdUsuario, Int64 v_CedIdentificacion, string v_NombreUsuario, string v_Apellidos, Int64 v_TelefonoHabitacion, Int64 v_TelefonoMovil, string v_Correo, string v_Puesto, Int64 v_IdRol, string v_UsuarioSistema, string v_Contrasena, string v_estadoSistema)
        {
            this.v_IdUsuario = v_IdUsuario;
            this.v_CedIdentificacion = v_CedIdentificacion;
            this.v_NombreUsuario = v_NombreUsuario;
            this.v_Apellidos = v_Apellidos;
            this.v_TelefonoHabitacion = v_TelefonoHabitacion;
            this.v_TelefonoMovil = v_TelefonoMovil;
            this.v_Correo = v_Correo;
            this.v_Puesto = v_Puesto;
            this.v_IdRol = v_IdRol;
            this.v_UsuarioSistema = v_UsuarioSistema;
            this.v_Contrasena = v_Contrasena;
            this.v_estadoSistema = v_estadoSistema;
        }
    }
}
