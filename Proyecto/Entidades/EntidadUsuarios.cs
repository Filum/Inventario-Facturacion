using System;

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
        public Int64 v_TelefonoOpcional { set; get; }
        public Int64 v_Telefono { set; get; }
        public string v_Correo { set; get; }
        public string v_Puesto { set; get; }
        public Int64 v_IdRol { set; get; }
        public string v_NombreRol { set; get; }
        public string v_UsuarioSistema { set; get; }
        public string v_Contrasena { set; get; }
        public DateTime v_Fecha { set; get; }
        public string v_EstadoSistema { set; get; }


        public EntidadUsuarios(Int64 v_IdUsuario, DateTime v_Fecha, Int64 v_CedIdentificacion, string v_NombreUsuario, string v_Apellidos, Int64 v_TelefonoOpcional, Int64 v_Telefono, string v_Correo, string v_Puesto, Int64 v_IdRol, string v_NombreRol, string v_UsuarioSistema, string v_Contrasena , string v_estadoSistema)
        {
            this.v_IdUsuario = v_IdUsuario;
            this.v_CedIdentificacion = v_CedIdentificacion;
            this.v_Fecha = v_Fecha;
            this.v_NombreUsuario = v_NombreUsuario;
            this.v_Apellidos = v_Apellidos;
            this.v_TelefonoOpcional = v_TelefonoOpcional;
            this.v_Telefono = v_Telefono;
            this.v_Correo = v_Correo;
            this.v_Puesto = v_Puesto;
            this.v_IdRol = v_IdRol;
            this.v_NombreRol = v_NombreRol;
            this.v_UsuarioSistema = v_UsuarioSistema;
            this.v_Contrasena = v_Contrasena;
            this.v_EstadoSistema = v_estadoSistema;
        }
    }
}
