using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EntidadDetalleFactura
    {
        public string tipo { get; set; }
        public string id_detalle { get; set; }
        public string subtotal { get; set; }
        public string descripcion_servicio { get; set; }
        public string id_producto { get; set; }
        public string id_factura { get; set; }
        public string cantidad { get; set; }

        public string impuesto { get; set; }
        public string descuento { get; set; }

        public EntidadDetalleFactura()
        {

        }

        public EntidadDetalleFactura(string id_Detalle, string subtotal, string descripcion_servicio, string id_producto, string id_factura, string cantidad,string tipo,string descuento,string impuesto)
        {
            this.id_detalle = id_Detalle;
            this.subtotal = subtotal;
            this.descripcion_servicio = descripcion_servicio;
            this.id_producto = id_producto;
            this.id_factura = id_factura;
            this.cantidad = cantidad;
            this.tipo = tipo;
            this.descuento = descuento;
            this.impuesto = impuesto;
        }
    }
}
