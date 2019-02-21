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
            date_historial_datte3.SelectedDate = DateTime.Now.Date;
            date_historial_datte4.SelectedDate = DateTime.Now.Date;
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
            if ( txb_codigo_factura_servicio.Text == "" || txb_descuento_servicios.Text == "" || txb_subtotal_factura_servicios.Text == "" || txb_total_factura_servicios.Text == "")
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
            if ( txb_codigo_factura_servicio.Text == "" || txb_descuento_servicios.Text == "" || txb_subtotal_factura_servicios.Text == "" || txb_total_factura_servicios.Text == "")
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
            txb_Cantidad.Text = "0";
            txb_Precio.Text = "0";
            txb_subtotal_factura_servicios.Text = "0";
            txb_descuento_servicios.Text = "0";
            txb_total_factura_servicios.Text = "0";
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
            txb_codigo_factura_servicio.Text = "";
            txb_descripcion.Text = "";
            txb_descuento_servicios.Text = "";
            txb_subtotal_factura_servicios.Text = "";
            txb_total_factura_servicios.Text = "";
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
        //Metodo para mostrar los clientes activos en el sistema, por medio de un combobox
        private void Iniciar_Clientes(ComboBox combo)
        {
            DataTable dt = datos.Clientes();

            foreach (DataRow fila in dt.Rows)
            {
                combo.Items.Add(Convert.ToString(fila["NOMBRE"]));
            }
        }

        //Iniciar la facturacion de servicios con valores en 0
        private void Grid_Initialized(object sender, EventArgs e)
        {
            txb_Cantidad.Text = "0";
            txb_Precio.Text = "0";
            txb_subtotal_factura_servicios.Text = "0";
            txb_descuento_servicios.Text = "0";
            txb_total_factura_servicios.Text = "0";
        }

        private void txb_Cantidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            float cantidad;
            float precio;
            if (txb_Precio.Text == "")
            txb_Precio.Text = "0";
            else
            if (txb_Cantidad.Text == "" )
            {
                cantidad = float.Parse("0");
                precio = float.Parse(txb_Precio.Text);
                txb_subtotal_factura_servicios.Text = (cantidad * precio).ToString();
            }
            else
            {
                if (RadioButton_Colon_Servicio.IsChecked == true)
                {
                    cantidad = float.Parse(txb_Cantidad.Text);
                    precio = float.Parse(txb_Precio.Text);
                    txb_subtotal_factura_servicios.Text = (cantidad * precio).ToString();
                }
                else
                {
                    cantidad = float.Parse(txb_Cantidad.Text);
                    precio = float.Parse(txb_Precio.Text);
                    txb_subtotal_factura_servicios.Text = (cantidad * (precio * datos.ObtenerValorDolar())).ToString();
                }
                
            }
        }

        private void txb_Precio_TextChanged(object sender, TextChangedEventArgs e)
        {
            float cantidad;
            float precio;
            if (txb_Cantidad.Text == "")
                txb_Cantidad.Text = "0";
            else
            if (txb_Precio.Text == "")
            {
                precio = float.Parse("0");
                cantidad = float.Parse(txb_Cantidad.Text);
                txb_subtotal_factura_servicios.Text = (cantidad * precio).ToString();
            }
            else
            {
                if (RadioButton_Colon_Servicio.IsChecked == true)
                {
                    cantidad = float.Parse(txb_Cantidad.Text);
                    precio = float.Parse(txb_Precio.Text);
                    txb_subtotal_factura_servicios.Text = (cantidad * precio).ToString();
                }
                else
                {
                    cantidad = float.Parse(txb_Cantidad.Text);
                    precio = float.Parse(txb_Precio.Text);
                    txb_subtotal_factura_servicios.Text = (cantidad * (precio * datos.ObtenerValorDolar())).ToString();
                }
                
            }

        }

        private void txb_Cantidad_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txb_Cantidad.Text == "")
                txb_Cantidad.Text = "0";
        }

        private void txb_Precio_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txb_Precio.Text == "")
                txb_Precio.Text = "0";
        }

        private void txb_subtotal_factura_servicios_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txb_descuento_servicios.Text=="")
            {
                txb_descuento_servicios.Text = "0";
            }
            float subtotal = float.Parse(txb_subtotal_factura_servicios.Text);
            float porcentaje_descuento = float.Parse(txb_descuento_servicios.Text)/100;
            float descuento = subtotal * porcentaje_descuento;
            txb_total_factura_servicios.Text = (subtotal-descuento).ToString();
        }

        private void txb_descuento_servicios_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txb_descuento_servicios.Text == "")
            {
                txb_descuento_servicios.Text = "0";
            }
            float subtotal = float.Parse(txb_subtotal_factura_servicios.Text);
            float porcentaje_descuento = float.Parse(txb_descuento_servicios.Text) / 100;
            float descuento = subtotal * porcentaje_descuento;
            txb_total_factura_servicios.Text = (subtotal - descuento).ToString();
        }

        private void btn_Listar_Click(object sender, RoutedEventArgs e)
        {
            if (date_historial_datte3.SelectedDate != null && date_historial_datte4.SelectedDate != null)
            {
                DateTime v_Date1 = DateTime.Parse(date_historial_datte3.SelectedDate.Value.Date.ToShortDateString());
                DateTime v_Date2 = DateTime.Parse(date_historial_datte4.SelectedDate.Value.Date.ToShortDateString());
                String v_Fecha1;
                v_Fecha1 = date_historial_datte3.SelectedDate.Value.Date.ToShortDateString();
                String v_Fecha2;
                v_Fecha2 = date_historial_datte4.SelectedDate.Value.Date.ToShortDateString();
                if (v_Date1 > v_Date2)
                {
                    MessageBox.Show("El rango de fechas es incorrecto\nLa fecha inicial no puede ser mayor a la final", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (datos.MostrarListaFacturas(v_Fecha1, v_Fecha2).Rows.Count == 0)
                    {
                        MessageBox.Show("No hay datos registrados en el rango de fechas seleccionado", "Búsqueda", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        dtg_listar_facturas.ItemsSource = datos.MostrarListaFacturas(v_Fecha1, v_Fecha2).DefaultView;
                        dtg_listar_facturas.Columns[0].Header = "Código";
                        dtg_listar_facturas.Columns[0].Width = 60;
                        dtg_listar_facturas.Columns[1].Header = "Fecha";
                        dtg_listar_facturas.Columns[1].Width = 133;
                        dtg_listar_facturas.Columns[2].Header = "Usuario";
                        dtg_listar_facturas.Columns[2].Width = 260;
                        dtg_listar_facturas.Columns[3].Header = "Cliente";
                        dtg_listar_facturas.Columns[3].Width = 260;
                        dtg_listar_facturas.Columns[4].Header = "Total";
                        dtg_listar_facturas.Columns[4].Width = 90;
                        dtg_listar_facturas.Columns[5].Header = "Moneda";
                        dtg_listar_facturas.Columns[5].Width = 90;
                        dtg_listar_facturas.Columns[6].Header = "Impuesto";
                        dtg_listar_facturas.Columns[6].Width = 90;
                        dtg_listar_facturas.Columns[7].Header = "Descuento";
                        dtg_listar_facturas.Columns[7].Width = 90;
                    }
                }

            }
            else
            {
                MessageBox.Show("Seleccione un rango de fechas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
