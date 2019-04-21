using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EntidadProductos
    {
        public EntidadProductos()
        {

        }

        public Int64 v_IdProducto { set; get; }
        public string v_CodigoProducto { set; get; }
        public string v_NombreProducto { set; get; }
        public string v_MarcaProducto { set; get; }
        public Int64 v_CantidadExistencia { set; get; }
        public Int64 v_CantidadMinima { set; get; }
        public Int64 v_IdProveedor { set; get; }
        public string v_NombreProveedor { set; get; }
        public Int64 v_PrecioUnitario { set; get; }
        public string v_Descripcion { set; get; }
        public string v_Fabricante { set; get; }
        public string v_Estado { set; get; }
        public DateTime v_Fecha { set; get; }

        public string v_EstadoSistema { set; get; }
        public string v_EstadoProducto { set; get; }

        public EntidadProductos(Int64 v_IdProducto, string v_CodigoProducto, string v_NombreProducto, string v_MarcaProducto, Int64 v_CantidadExistencia, Int64 v_CantidadMinima, Int64 v_IdProveedor, string v_NombreProveedor, Int64 v_PrecioUnitario, string v_Descripcion, string v_Fabricante, string v_EstadoProducto, DateTime v_Fecha, string v_EstadoSistema)
        {
            this.v_IdProducto = v_IdProducto;
            this.v_CodigoProducto = v_CodigoProducto;
            this.v_NombreProducto = v_NombreProducto;
            this.v_MarcaProducto = v_MarcaProducto;
            this.v_CantidadExistencia = v_CantidadExistencia;
            this.v_CantidadMinima = v_CantidadMinima;
            this.v_IdProveedor = v_IdProveedor;
            this.v_NombreProveedor = v_NombreProveedor;
            this.v_PrecioUnitario = v_PrecioUnitario;
            this.v_Descripcion = v_Descripcion;
            this.v_Fabricante = v_Fabricante;
            this.v_Estado = v_Estado;
            this.v_Fecha = v_Fecha;
        }
    }
}
