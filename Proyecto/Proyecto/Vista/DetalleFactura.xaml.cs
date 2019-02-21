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

        private void dtg_listar_detalle_facturas_Initialized(object sender, EventArgs e)
        {
            dtg_listar_detalle_facturas.ItemsSource = datos.MostrarDetalleFactura(1).DefaultView;
            /*dtg_listar_detalle_facturas.Columns[0].Header = "Código Factura";
            dtg_listar_detalle_facturas.Columns[1].Width = 60;
            dtg_listar_detalle_facturas.Columns[1].Header = "CódigoProducto";
            dtg_listar_detalle_facturas.Columns[1].Width = 133;
            dtg_listar_detalle_facturas.Columns[2].Header = "NombreProducto";
            dtg_listar_detalle_facturas.Columns[2].Width = 260;
            dtg_listar_detalle_facturas.Columns[3].Header = "MarcaProducto";
            dtg_listar_detalle_facturas.Columns[3].Width = 260;
            dtg_listar_detalle_facturas.Columns[4].Header = "PrecioUnitario";
            dtg_listar_detalle_facturas.Columns[4].Width = 90;
            dtg_listar_detalle_facturas.Columns[5].Header = "Cantidad";
            dtg_listar_detalle_facturas.Columns[5].Width = 90;
            dtg_listar_detalle_facturas.Columns[6].Header = "Total";
            dtg_listar_detalle_facturas.Columns[6].Width = 90;
            dtg_listar_detalle_facturas.Columns[7].Header = "Moneda";
            dtg_listar_detalle_facturas.Columns[7].Width = 90;*/
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            var detalle = new List<string>();
            detalle=datos.DetalleFactura(1);
            txt_codigo.Content = detalle[0];
            txt_fecha.Content = detalle[1];
            txt_Usuario.Content = detalle[2];
            txt_Cliente.Content = detalle[3];
        }

    }
}
