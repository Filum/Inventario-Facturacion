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
        public int id_detalle { get; set; }
        public float subtotal { get; set; }
        public string descripcion_servicio { get; set; }
        public int id_producto { get; set; }
        public int id_factura { get; set; }
        public int cantidad { get; set; }

        public string impuesto { get; set; }
        public string descuento { get; set; }
        public string precioProducto { get; set; }

        public EntidadDetalleFactura()
        {

        }

        public EntidadDetalleFactura(int id_Detalle, float subtotal, string descripcion_servicio, int id_producto, int id_factura, int cantidad,string tipo,string descuento,string impuesto)
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
