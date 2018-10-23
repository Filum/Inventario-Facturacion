using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entidades
{
    public class EntidadProveedores
    {
        public EntidadProveedores()
        {

        }

        public Int64 v_idProveedor { set; get; }
        public Int64 v_cedulaJuridica { set; get; }
        public string v_nombre { set; get; }
        public string v_correo { set; get; }
        public string v_descripcion { set; get; }
        public Int64 v_telefono { set; get; }
        public DateTime v_fecha { set; get; }


        public EntidadProveedores(Int64 v_idProveedor, Int64 v_cedulaJuridica, string v_nombre, string v_correo, string v_descripcion, Int64 v_telefono, DateTime v_fecha)
        {
            this.v_idProveedor = v_idProveedor;
            this.v_cedulaJuridica = v_cedulaJuridica;
            this.v_nombre = v_nombre;
            this.v_correo = v_correo;
            this.v_descripcion = v_descripcion;
            this.v_telefono = v_telefono;
            this.v_fecha = v_fecha;
        }

    }
}
