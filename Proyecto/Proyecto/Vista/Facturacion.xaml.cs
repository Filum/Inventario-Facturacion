using Logica;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

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

        private ObservableCollection<FacturasProductos> Factura = new ObservableCollection<FacturasProductos>();
        private void Button_agregar_producto_Click(object sender, RoutedEventArgs e)
        {
            Factura.Add(new FacturasProductos()
            {
                Codigo = "0",
                Productos = datos.ListaProductos(),
                Cantidad = "0",
                Precio = "0",
                Subtotal = "0"

            });
            DataContext = Factura;
            //MessageBox.Show("No se puede agregar\n Hacen falta productos en inventario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FacturasProductos item = (FacturasProductos)dtg_facturar_productos.SelectedItem;
            
            item.Codigo = "10";
            item.Precio = "10000";
            item.Productos = datos.ListaProductos();
            dtg_facturar_productos.Items.Refresh();
        }

        public class FacturasProductos
        {
            public string Codigo { get; set; }
            public ObservableCollection<string> Productos { get; set; }
            public string Cantidad { get; set; }
            public string Precio { get; set; }
            public string Subtotal { get; set; }

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
            if (txb_codigo_factura.Text == "" || txb_descuento_Producto.Text == "" || txb_subtotal_factura.Text == "" || txb_total_factura.Text == "")
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

            txb_codigo_factura.Text = "";
            txb_Cantidad.Text = "0";
            txb_Precio.Text = "0";
            txb_subtotal_factura_servicios.Text = "0";
            txb_descuento_servicios.Text = "0";
            txb_total_factura_servicios.Text = "0";
            txt_error_numFactura.Visibility = Visibility.Hidden;
            txt_error_descuento.Visibility = Visibility.Hidden;
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
            txt_error_numFactura_servicio.Visibility = Visibility.Hidden;
        }

        private void descuento_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
            {
                e.Handled = false;
                txt_error_descuento.Visibility = Visibility.Collapsed;
                if (ValidarCaracteresEspeciales(txb_descuento_Producto.Text) == true)
                {
                    txt_error_descuento.Content = "No se permiten caracteres especiales";
                    txt_error_descuento.Visibility = Visibility.Visible;
                }
            }
            else
            {
                e.Handled = true;
                txt_error_descuento.Content = "No se permite ingresar letras";
                txt_error_descuento.Visibility = Visibility.Visible;
            }
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

        private void btn_imprimir_buscar_Click(object sender, RoutedEventArgs e)
        {
            Vista.DetalleFactura ventana = new Vista.DetalleFactura();
            ventana.Show();
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e) //Evento declarado en el datagrid
        {
            DataGridRow row = sender as DataGridRow;
        
            Vista.DetalleFactura ventana = new Vista.DetalleFactura();
            ventana.txt_codigo.Content = (dtg_listar_facturas.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text;

            var detalle = new List<string>();
            detalle = datos.DetalleFactura(1);
            ventana.txt_fecha.Content = detalle[1];
            ventana.txt_Usuario.Content = detalle[2];
            ventana.txt_Cliente.Content = detalle[3];
            int codigo = Convert.ToInt32(ventana.txt_codigo.Content);
            ventana.dtg_listar_detalle_facturas.ItemsSource = datos.MostrarDetalleFactura(codigo).DefaultView;
            /*ventana.dtg_listar_detalle_facturas.Columns[0].Header = "Código Factura";
            ventana.dtg_listar_detalle_facturas.Columns[1].Width = 60;
            ventana.dtg_listar_detalle_facturas.Columns[1].Header = "CódigoProducto";
            ventana.dtg_listar_detalle_facturas.Columns[1].Width = 133;
            ventana.dtg_listar_detalle_facturas.Columns[2].Header = "NombreProducto";
            ventana.dtg_listar_detalle_facturas.Columns[2].Width = 260;
            ventana.dtg_listar_detalle_facturas.Columns[3].Header = "MarcaProducto";
            ventana.dtg_listar_detalle_facturas.Columns[3].Width = 260;
            ventana.dtg_listar_detalle_facturas.Columns[4].Header = "PrecioUnitario";
            ventana.dtg_listar_detalle_facturas.Columns[4].Width = 90;
            ventana.dtg_listar_detalle_facturas.Columns[5].Header = "Cantidad";
            ventana.dtg_listar_detalle_facturas.Columns[5].Width = 90;
            ventana.dtg_listar_detalle_facturas.Columns[6].Header = "Total";
            ventana.dtg_listar_detalle_facturas.Columns[6].Width = 90;
            ventana.dtg_listar_detalle_facturas.Columns[7].Header = "Moneda";
            ventana.dtg_listar_detalle_facturas.Columns[7].Width = 90;*/
            ventana.Show();
        }

        //Método el cual valida si en las cajas de texto recibidos contiene caracteres especiales
        private Boolean ValidarCaracteresEspeciales(String v_Txb)
        {
            //caracteres que permite si la cadena es de int
            String v_Caracteres = "[a-zA-Z !@#$%^&*())+=.,<>{}¬º´/\"':;|ñÑ~¡?`¿-]";
            if (Regex.IsMatch(v_Txb, v_Caracteres))
            {
                return true;
            }

            return false;
        }

        //Método el cual recibe parametros necesarios para la validacion y la muestra de mensajes de erroes en las cajas de texto
        private void ValidarErroresTxb(TextBox txb_facturas, Label lbl_error)
        {
            string v_TamanoTxb = txb_facturas.Text;
            if (txb_facturas.Text == "")
            {
                lbl_error.Content = "Espacio vacío";
                lbl_error.Visibility = Visibility.Visible;
            }
            else if (txb_facturas.Text == " ")
            {
                txb_facturas.Text = "";
            }
            else if (txb_facturas.Text.Contains("  "))
            {
                lbl_error.Content = "Parámetros incorrectos (espacios seguidos)";
                lbl_error.Visibility = Visibility.Visible;
            }
            else if (ValidarCaracteresEspeciales(txb_facturas.Text) == true)
            {
                lbl_error.Content = "No se permiten caracteres especiales";
                lbl_error.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_error.Visibility = Visibility.Collapsed;
            }
        }


        private void txb_descuento_Producto_TextChanged(object sender, TextChangedEventArgs e)
        {
           ValidarErroresTxb(txb_descuento_Producto, txt_error_descuento);
            if (txb_descuento_Producto.Text == "")
            {
                txt_error_descuento.Visibility = Visibility.Collapsed;
            }
        }

    }
}
