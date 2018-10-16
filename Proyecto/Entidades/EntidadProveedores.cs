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

        public Int32 v_idProveedor { set; get; }
        public Int32 v_cedulaJuridica { set; get; }
        public string v_nombre { set; get; }
        public string v_correo { set; get; }
        public string v_descripccion { set; get; }
        public Int32 v_telefono { set; get; }
        public DateTime v_fecha { set; get; }


        public EntidadProveedores(Int32 v_idProveedor,Int32 v_cedulaJuridica, string v_nombre, string v_correo, string v_descripccion, Int32 v_telefono, DateTime v_fecha)
        {
            this.v_idProveedor = v_idProveedor;
            this.v_cedulaJuridica = v_cedulaJuridica;
            this.v_nombre = v_nombre;
            this.v_correo = v_correo;
            this.v_descripccion = v_descripccion;
            this.v_telefono = v_telefono;
            this.v_fecha = v_fecha;
        }

    }
}
