using Logica;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Proyecto
{
    /// <summary>
    /// Interaction logic for Facturacion.xaml
    /// </summary>
    public partial class Facturacion : Window
    {
        Model datos = new Model();
        public Facturacion()
        {
            InitializeComponent();
            //Formato para la hora
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            txt_fecha.Content = DateTime.Now.ToString();

        }
        private void btn_Salir_Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.Show();
            this.Close();
        }



        private void btn_Usuario_Click(object sender, RoutedEventArgs e)
        {
            Login ventana = new Login();
            ventana.Show();
            this.Close();
        }

        private void btn_ayuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Secciones Mantenimiento de Facturas:\n" + "Listar: usted podra imprimir la lista de facturas, aparte de ordenarlos ya sea por código o por nombre del cliente.\n" +
                            "Buscar: aca usted podra buscar los clientes que desee.\n" +
                            "Facturación de productos: esta sección le permite la facturación de productos.\n" +
                            "Facturación de servicios: esta sección le permite la facturación de servicios.\n" +
                            "Historial: esta ventana le mostrara todos los cambios realizados en esta sección.\n"
                           , "Ayuda", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_agregar_producto_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("No se puede agregar\n Hacen falta productos en inventario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Button_agregar_servicio_Click(object sender, RoutedEventArgs e)
        {
            if ( textbox_codigo_factura_servicio.Text == "" || textbox_descuento_servicios.Text == "" || textbox_subtotal_factura_servicios.Text == "" || textbox_total_factura_servicios.Text == "")
            {
                MessageBox.Show("No se puede agregar\n Hacen falta campos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Datos guardados", "Guardando", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_imprimir_factura_producto_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_codigo_factura.Text == "" || textbox_descuento_Producto.Text == "" || textbox_subtotal_factura.Text == "" || textbox_total_factura.Text == "")
            {
                MessageBox.Show("No se puede imprimir\n Hacen falta campos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Imprimiendo...", "Imprimiendo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_imprimir_factura_servicio_Click(object sender, RoutedEventArgs e)
        {
            if ( textbox_codigo_factura_servicio.Text == "" || textbox_descuento_servicios.Text == "" || textbox_subtotal_factura_servicios.Text == "" || textbox_total_factura_servicios.Text == "")
            {
                MessageBox.Show("No se puede imprimir\n Hacen falta campos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Imprimiendo...","Imprimiendo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btn_limpiar_factura_Prod_Click(object sender, RoutedEventArgs e)
        {

            textbox_codigo_factura.Text = "";
            textbox_descuento_Producto.Text = "";
            textbox_subtotal_factura.Text = "";
            textbox_total_factura.Text = "";
        }

        private void btn_minimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btn_maximizar_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;

            }
            else
            {
                WindowStyle = WindowStyle.None;
                WindowState = WindowState.Normal;

            }
        }
        private void barra_movil__MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                this.WindowStyle = WindowStyle.None;
                WindowState = WindowState.Normal;
            }

            this.DragMove();
        }
        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_limpiar_factura_Serv_Click(object sender, RoutedEventArgs e)
        {
            textbox_codigo_factura_servicio.Text = "";
            textbox_descripcion.Text = "";
            textbox_descuento_servicios.Text = "";
            textbox_subtotal_factura_servicios.Text = "";
            textbox_total_factura_servicios.Text = "";
            txb_Cantidad.Text = "";
            txb_Precio.Text = "";
        }

        private void descuento_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txb_Cantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txb_Precio_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void btn_imprimir_Click(object sender, RoutedEventArgs e)
        {

        }
        //Iniciamos los combobox de facturacion de productos y servicios con los clientes activos.
        private void cmb_Cliente_Productos_Initialized(object sender, EventArgs e)
        {
            Iniciar_Clientes(cmb_Cliente_Productos);
        }
        private void cmb_Cliente_servicios_Initialized(object sender, EventArgs e)
        {
            Iniciar_Clientes(cmb_Cliente_servicios);
        }
        private void Iniciar_Clientes(ComboBox combo)
        {
            DataTable dt = datos.Clientes();

            foreach (DataRow fila in dt.Rows)
            {
                combo.Items.Add(Convert.ToString(fila["NOMBRE"]));
            }
        }


    }
}
