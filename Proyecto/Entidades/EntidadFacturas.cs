using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EntidadFacturas
    {
        public Int32 v_Codigo { get; set; }
        public DateTime v_Fecha { get; set; }
        public string v_Usuario { get; set; }
        public string v_Cliente { get; set; }
        public Int32 v_Total { get; set; }
        public Int32 v_Descuento { get; set; }
        public string v_Moneda { get; set; }
        public String v_Impuesto { get; set; }

        //public List<EntidadDetalles> v_ListaDetalles { get; set; }


        public EntidadFacturas()
        {

        }

        public EntidadFacturas(Int32 v_Codigo, DateTime v_Fecha, string v_Usuario, string v_Cliente, Int32 v_Total, Int32 v_Descuento, string v_Moneda, String v_Impuesto, List<EntidadDetalles> v_ListaDetalles)
        {
            this.v_Codigo = v_Codigo;
            this.v_Fecha = v_Fecha;
            this.v_Usuario = v_Usuario;
            this.v_Cliente = v_Cliente;
            this.v_Total = v_Total;
            this.v_Descuento = v_Descuento;
            this.v_Moneda = v_Moneda;
            this.v_Impuesto = v_Impuesto;
        }
    }
}
