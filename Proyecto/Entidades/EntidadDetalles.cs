using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EntidadDetalles
    {
        public Int32 v_IdDetalle { get; set; }
        public string v_Producto { get; set; }
        public Int32 v_Cantidad { get; set; }
        public Int32 v_CodigoFactura { get; set; }
        public Int32 v_Total { get; set; }
        public Int32 v_DescServicio { get; set; }

        public EntidadDetalles()
        {

        }

        public EntidadDetalles(Int32 v_IdDetalle, string v_Producto, Int32 v_Cantidad, Int32 v_CodigoFactura, Int32 v_Total, Int32 v_DescServicio)
        {
            this.v_IdDetalle = v_IdDetalle;
            this.v_Producto = v_Producto;
            this.v_Cantidad = v_Cantidad;
            this.v_CodigoFactura = v_CodigoFactura;
            this.v_Total = v_Total;
            this.v_DescServicio = v_DescServicio;
        }
    }

   
}
