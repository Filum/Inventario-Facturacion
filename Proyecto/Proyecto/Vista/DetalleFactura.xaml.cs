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
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            DateTime dia = DateTime.Now;
            pagoFactura.Content = dia.ToShortDateString();
            btn_pagar_Factura.Visibility = Visibility.Hidden;
        }
    }
}
