using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Proyecto.Vista
{
    /// <summary>
    /// Interaction logic for DetalleFactura.xaml
    /// </summary>
    public partial class DetalleFactura : Window
    {
        Model datos = new Model();
        public DetalleFactura()
        {
            InitializeComponent();
        }

        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_pagar_Factura_Click(object sender, RoutedEventArgs e)
        {
            datos.CambiarestadoFactura("Cancelado", txt_codigo.Content.ToString());
            Pago.Content = "FECHA DE CANCELACIÓN: ";
            EstadoFactura.Content = "Cancelado";
            DateTime dia = DateTime.Now;
            pagoFactura.Content = dia.ToString("dd/MMM/yyyy");
            btn_pagar_Factura.Visibility = Visibility.Hidden;
        }

        private void Btn_imprimir_Click(object sender, RoutedEventArgs e)
        {
            string tipoFactura = datos.tipoFactura(txt_codigo.Content.ToString());
            EntidadFacturas miFactura = new EntidadFacturas();
            EntidadDetalleFactura miDetalle = new EntidadDetalleFactura();
            Imprimir imprimir = new Imprimir();
            DateTime fecha = DateTime.Parse(txt_fecha.Content.ToString());
            EntidadClientes micliente = new EntidadClientes();
            micliente = datos.id_Cliente(nombreCliente.Content.ToString());
            miFactura.v_Cliente = micliente.v_Codigo.ToString();
            miFactura.v_Codigo = int.Parse(txt_codigo.Content.ToString());
            miFactura.v_Usuario = "";

            miFactura.v_Total = totalGeneralFactura.Content.ToString();
            miFactura.v_Descuento = descuentoFactura.Content.ToString();
            if (tipoFactura == "Productos")
            {
            }
            miFactura.v_Moneda = txt_moneda.Content.ToString();
            miFactura.v_Impuesto = impuestoFactura.Content.ToString();
            miFactura.v_tipoFactura = "Productos";
            miFactura.v_Subtotal = subtoalFactura.Content.ToString();
            miFactura.v_SubtotalNeto = subtotalNetoFactura.Content.ToString();
            miFactura.v_tipoPago = tipoPago.Content.ToString();
            if (dias.Content != null)
                miFactura.v_diasCredito = dias.Content.ToString();
            else
                miFactura.v_diasCredito = "";

            miFactura.v_estadoFactura = EstadoFactura.Content.ToString();
            if (fechaVence.Content != null)
                miFactura.v_fechaPago = fechaVence.Content.ToString();
            else
                miFactura.v_fechaPago = "";

            if (pagoFactura.Content != null)
                miFactura.v_fechaCancelacion = pagoFactura.Content.ToString();
            else
                miFactura.v_fechaCancelacion = "";
            miFactura.v_tipoCambio = tipoCambio.Content.ToString();

            miDetalle.cantidad = Cantidad.Content.ToString();
            miDetalle.precioProducto = Precio.Content.ToString();
            imprimir.imprimirFactura(dtg_listar_detalle_facturas, miFactura, fecha, micliente,"",miDetalle);
        }
    }
}
