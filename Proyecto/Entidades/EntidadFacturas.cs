using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EntidadFacturas
    {
        public float v_Codigo { get; set; }
        public DateTime v_Fecha { get; set; }
        public string v_Usuario { get; set; }
        public string v_Cliente { get; set; }
        public string v_Total { get; set; }
        public string v_Descuento { get; set; }
        public string v_Moneda { get; set; }
        public string v_Impuesto { get; set; }
        public string v_tipoFactura { get; set; }
        public string v_SubtotalNeto { get; set; }
        public string v_Subtotal { get; set; }
        public string v_tipoPago { get; set; }
        public string v_diasCredito { get; set; }
        public string v_estadoFactura { get; set; }
        public string v_fechaPago { get; set; }
        public string v_fechaCancelacion { get; set; }
        public string v_tipoCambio { get; set; }


        //public List<EntidadDetalles> v_ListaDetalles { get; set; }


        public EntidadFacturas()
        {

        }

        public EntidadFacturas(float v_Codigo, DateTime v_Fecha, string v_Usuario, string v_Cliente, string v_Total, string v_Descuento, string v_Moneda, string v_Impuesto)
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
