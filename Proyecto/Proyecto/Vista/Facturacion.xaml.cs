using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
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
        public string nombreUsuario; //variable para cargar el nombre del usuario en esta pantalla
        Model datos = new Model();//variable para llamar los metodos de acceso a la base de datos
        EntidadBitacora bitacora = new EntidadBitacora();
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
        //Funciones basicas de la pantalla de facturacion
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            txt_fecha.Content = DateTime.Now.ToString();
        }
        //funcion para volver al menu , le devolvemos el usuario con el que estamos en sesion
        private void btn_Salir_Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.cargarMenu(nombreUsuario);
            ventana.nombreUser = nombreUsuario;
            ventana.Show();
            this.Close();
        }


        //boton para cerrar sesion
        private void btn_Usuario_Click(object sender, RoutedEventArgs e)
        {
            Login ventana = new Login();
            ventana.Show();
            this.Close();
        }
        //boton para mostrar la ayuda del sistema
        private void btn_ayuda_Click(object sender, RoutedEventArgs e)
        {
            Vista.Ayuda ventana = new Vista.Ayuda();
            ventana.Show();
            ventana.Pantalla = "Facturacion";
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

        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////
        /// Funciones para validacion de campos 

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

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////FUNCIONES DE LISTAR FACTURAS/////////////////////////////////////////////////////
       
            //Imprimir reporte de facturas
        private void btn_imprimir_Click(object sender, RoutedEventArgs e)
        {
            Imprimir print = new Imprimir();
            print.imprimir(dtg_listar_facturas, "REPORTE FACTURACIÓN");
        }

        //Cargamos el combobox del estado de la factura
        private void cmb_estado_Factura_Initialized(object sender, EventArgs e)
        {
            cmb_estado_Factura.Items.Add("Todas");
            cmb_estado_Factura.Items.Add("Vencida");
            cmb_estado_Factura.Items.Add("Pendiente");
            cmb_estado_Factura.Items.Add("Cancelado");
            cmb_estado_Factura.Items.Add("Nula");
        }
        //Metodo para mostrar las facturas una vez se eleccione el estado en el combobox
        private void cmb_estado_Factura_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (txb_buscar_cliente_factura.Text == "")
                {
                    if (cmb_estado_Factura.SelectedItem.ToString() == "Todas")
                    {
                    dtg_listar_facturas.ItemsSource = datos.BuscarFacturas().DefaultView;
                    }
                      else
                    dtg_listar_facturas.ItemsSource = datos.BuscarFacturaEstado(cmb_estado_Factura.SelectedItem.ToString()).DefaultView;
                }
                else
                {
                        dtg_listar_facturas.ItemsSource = datos.BuscarFacturaEstadoyCliente(txb_buscar_cliente_factura.Text, cmb_estado_Factura.SelectedItem.ToString()).DefaultView;
                }
            }
            catch(Exception m)
            {
                Console.WriteLine(m);
            }
        }
        //funcion para listar las facturas por rango de fechas
        private void btn_Listar_Click(object sender, RoutedEventArgs e)
        {
            if (date_historial_datte3.SelectedDate != null && date_historial_datte4.SelectedDate != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
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
                        dtg_listar_facturas.ItemsSource = null;
                    }
                    else
                    {
                        if(cmb_estado_Factura.SelectedItem == null && txb_buscar_cliente_factura.Text=="")
                             dtg_listar_facturas.ItemsSource = datos.MostrarListaFacturas(v_Fecha1, v_Fecha2).DefaultView;
                        else
                        {
                            try
                            {
                               dtg_listar_facturas.ItemsSource = datos.BuscarFacturaEstadoClienteFechas(txb_buscar_cliente_factura.Text, cmb_estado_Factura.SelectedItem.ToString(),v_Fecha1,v_Fecha2).DefaultView;
                            }
                            catch (Exception m)
                            {
                                Console.WriteLine(m);
                            }
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("Seleccione un rango de fechas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //funcion que imprime todas las facturas solicitadas
        private void btn_imprimir_buscar_Click(object sender, RoutedEventArgs e)
        {
            Vista.DetalleFactura ventana = new Vista.DetalleFactura();
            ventana.Show();
        }

        //Abrimos el detalle de la factura seleccionada en en el datagrid de listarfacutas
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e) //Evento declarado en el datagrid 
        {
            DataGridRow row = sender as DataGridRow;

            Vista.DetalleFactura ventana = new Vista.DetalleFactura();
            ventana.txt_codigo.Content = (dtg_listar_facturas.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text;

           int  n_Factura = int.Parse(((TextBlock)dtg_listar_facturas.Columns[1].GetCellContent(row)).Text);

            var detalle = new List<string>();
            detalle = datos.DetalleFactura(n_Factura);
            ventana.subtoalFactura.Content = detalle[3];
            ventana.descuentoFactura.Content = detalle[4];
            ventana.subtotalNetoFactura.Content = detalle[5];
            ventana.impuestoFactura.Content = detalle[6];
            ventana.totalGeneralFactura.Content = detalle[7];
            ventana.tipoPago.Content = detalle[8];
            if (detalle[8] == "Contado")
            {
                ventana.br_credito.Visibility = Visibility.Hidden;
            }
            else
            {
                ventana.br_contado.Visibility = Visibility.Hidden;
            }
            if(detalle[10] == "Cancelado")
            {
                ventana.dias.Visibility = Visibility.Hidden;
                ventana.Dias.Visibility = Visibility.Hidden;
                ventana.a.Visibility = Visibility.Hidden;
                ventana.Vence.Visibility = Visibility.Hidden;
                ventana.fechaVence.Visibility = Visibility.Hidden;
                ventana.btn_pagar_Factura.Visibility = Visibility.Hidden;
                ventana.Pago.Content = "FECHA DE CANCELACIÓN:";
                ventana.pagoFactura.Content = detalle[20];
                if(detalle[8] == "Credito")
                {
                    ventana.dias.Visibility = Visibility.Visible;
                    ventana.Dias.Visibility = Visibility.Visible;
                    ventana.a.Visibility = Visibility.Visible;
                    ventana.Vence.Visibility = Visibility.Visible;
                    ventana.fechaVence.Visibility = Visibility.Visible;
                    ventana.dias.Content = detalle[9];
                    ventana.fechaVence.Content = detalle[11];
                }
            }
            else if (detalle[10] == "Pendiente" || detalle[10] == "Vencida")
            {
                ventana.dias.Content = detalle[9];
                ventana.fechaVence.Content = detalle[11];
                ventana.br_pago1.Visibility = Visibility.Hidden;
                ventana.br_pago2.Visibility = Visibility.Hidden;
            } else if (detalle[10] == "Nula")
            {
                ventana.dias.Visibility = Visibility.Hidden;
                ventana.Dias.Visibility = Visibility.Hidden;
                ventana.a.Visibility = Visibility.Hidden;
                ventana.Vence.Visibility = Visibility.Hidden;
                ventana.fechaVence.Visibility = Visibility.Hidden;
                ventana.btn_anular.Visibility = Visibility.Hidden;
                ventana.btn_imprimir.Visibility = Visibility.Hidden;
                ventana.btn_pagar_Factura.Visibility = Visibility.Hidden;
            }
            ventana.EstadoFactura.Content = detalle[10];
            ventana.idCLiente.Content = detalle[12];
            ventana.cedulaCliente.Content = detalle[13];
            ventana.nombreCliente.Content = detalle[14];
            ventana.representanteCliente.Content = detalle[15];
            ventana.direccionCliente.Content = detalle[16];
            ventana.telefono1.Content = detalle[17];
            ventana.telefono2.Content = detalle[18];
            ventana.correCliente.Content = detalle[19];
            ventana.Garbado.Content = detalle[5];
            ventana.tipoCambio.Content = detalle[21];
            ventana.txt_fecha.Content = detalle[2];
            ventana.txt_fecha_emision.Content = detalle[2];
            int codigo = Convert.ToInt32(ventana.txt_codigo.Content);
            if(detalle[0] == "Productos")
                ventana.dtg_listar_detalle_facturas.ItemsSource = datos.MostrarDetalleFactura(codigo).DefaultView;
            if (detalle[0] == "Servicios")
            {
                var detalleServicio = new List<string>();
                detalleServicio = datos.DetalleFacturaServicios(n_Factura);
                ventana.dtg_listar_detalle_facturas.Visibility = Visibility.Hidden;
                ventana.txb_Descripcion.Visibility = Visibility.Visible;
                ventana.txt_Cantidad.Visibility = Visibility.Visible;
                ventana.txt_Precio.Visibility = Visibility.Visible;
                ventana.Cantidad.Visibility = Visibility.Visible;
                ventana.Precio.Visibility = Visibility.Visible;
                ventana.br_1.Visibility = Visibility.Visible;
                ventana.br_2.Visibility = Visibility.Visible;
                ventana.br_3.Visibility = Visibility.Visible;
                ventana.br_4.Visibility = Visibility.Visible;
                ventana.txb_Descripcion.Text = detalleServicio[0];
                ventana.Cantidad.Content = detalleServicio[1];
                ventana.Precio.Content = detalleServicio[2];
            }
            ventana.txt_moneda.Content = detalle[22];
            ventana.Show();
            dtg_listar_facturas.ItemsSource = null;
            cmb_estado_Factura.SelectedItem = null;
            txb_buscar_cliente_factura.Text = "";
        }


        //-------------------------Metodo que busca las facturas por nombre del cliente--------------
        private void txb_buscar_cliente_factura_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txb_buscar_cliente_factura.Text == "")
                {
                    dtg_listar_facturas.ItemsSource = null;
                    dtg_listar_facturas.ItemsSource = datos.BuscarFacturaEstado(cmb_estado_Factura.SelectedItem.ToString()).DefaultView;
                }
                else
                {
                    if (cmb_estado_Factura.SelectedItem == null)
                    {
                        dtg_listar_facturas.ItemsSource = datos.BuscarFactura(txb_buscar_cliente_factura.Text).DefaultView;
                    }
                    else
                    {
                        dtg_listar_facturas.ItemsSource = datos.BuscarFacturaEstadoyCliente(txb_buscar_cliente_factura.Text, cmb_estado_Factura.SelectedItem.ToString()).DefaultView;
                    }
                }
            }
            catch(Exception m)
            {
                Console.Write(m);
            }
            
        }



        //Facturacion productos ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        EntidadFacturas miFactura = new EntidadFacturas();
        EntidadDetalleFactura miDetalle = new EntidadDetalleFactura();
        private float montos = 0;
        private string tipoPago = "";
        private string diasCredito = "";
        private string estadoFactura = "";
        private string fechaPago = "";

        //INICIAMOS LA FACTURACION DE PRODUCTOS
        private void Productostab_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (dtg_facturar_productos_prueba.Items.Count == 0)
            {
                txb_subtotal_factura_prueba.Text = "0";
                txb_descuento_Producto_prueba.Text = "0";
                txb_subtotalneto.Text = "0";
                txb_impuestofactura.Text = "0";
                txb_total_factura_prueba.Text = "0";
                txb_descuento_linea_producto.Text = "0";
            }
            try
            {
                txt_codigo_factura.Content = (datos.MaximaFactura() + 1).ToString();
            }
            catch (Exception m)
            {
                txt_codigo_factura.Content = "1";
            }
        }

        //Clase necesaria para gregar una nueva factura al datagrid
        public class FacturasProducto
        {
            public string Servicio { get; set; }
            public string Codigo { get; set; }
            public string Producto { get; set; }
            public string Cantidad { get; set; }
            public string Precio { get; set; }
            public string Descuento { get; set; }
            public string Impuesto { get; set; }
            public string Total { get; set; }

        }
        //funcion para limpiar la factura de productos
        private void limpiar_Facturaprodutcos()
        {
            dtg_facturar_productos_prueba.Items.Clear();
            RadioButton_Si1.IsChecked = true;
            txb_subtotal_factura_prueba.Text = "0";
            txb_descuento_Producto_prueba.Text = "0";
            txb_subtotalneto.Text = "0";
            txb_impuestofactura.Text = "0";
            txb_total_factura_prueba.Text = "0";
            txb_descuento_linea_producto.Text = "0";
            txb_diasCredito.Text = "";
            txb_catidad_producto.Text = "";
            txb_descuento_linea_producto.Text = "";
            txt_codigo_factura.Content = (datos.MaximaFactura() + 1).ToString();
            cmb_Cliente_Productos_Prueba.SelectedItem = null;
            Rb_siProducto.IsChecked = true;
            RadioButton_Colon1.IsChecked = true;
            cmb_Productos.SelectedItem = null;
            Rb_Contado.IsChecked = true;
            montos = 0;
            tipoPago = "";
            diasCredito = "";
            estadoFactura = "";
            fechaPago = "";
        }

        //Funcion para gregar un nuevo producto con sus respectivas caracteristicas al datgrid
        private void AgregarRow ()
        {
            ObservableCollection<FacturasProducto> Row2 = new ObservableCollection<FacturasProducto>();// La fila que va a tener los valores del producto
            var detalle = new List<string>();

            if (txb_catidad_producto.Text != "" || txb_descuento_linea_producto.Text == "")//verificamos que la cantidad de productos tenga algun valor
            {
                detalle = datos.DetalleProducto(cmb_Productos.SelectedItem.ToString());//tomamos el detalle del producto solicitado
                //Damos los valores del detalle de la factura
                float valorUnitario = 0;
                float precio = float.Parse(detalle[1]);
                float cantidad = float.Parse(txb_catidad_producto.Text);
                float impuestoLinea = 0;
                float descuentoLinea = 0;

                //verificamos si el usuario quiere ponerle impuesto a sus productos
                if (Rb_siProducto.IsChecked == true)
                {
                    impuestoLinea = 13;
                }
                else if (Rb_noProducto.IsChecked == true)
                {
                    impuestoLinea = 0;
                }
                if (RadioButton_Colon1.IsChecked == true)
                {
                    valorUnitario = precio;
                }
                else if (RadioButton_Dolar1.IsChecked == true)
                {
                    valorUnitario = precio/datos.ObtenerValorDolar();
                    
                }
                //Calculamos el descuento que se le aplica al producto
                if (txb_descuento_linea_producto.Text != "")
                    descuentoLinea = (((valorUnitario * cantidad) * (impuestoLinea / 100) + (valorUnitario * cantidad)) * (float.Parse(txb_descuento_linea_producto.Text) / 100));
                else
                    descuentoLinea = 0;
                //Establecemos los valores del nuevo producto para agregarlo al datagrid
                //Lo logramos con un binding al datagrid, el mismo nombre de los atributos de la clase
                txb_descuento_linea_producto.Text = "0";
                Row2.Add(new FacturasProducto()
                {
                    Servicio = "Mercadería",
                    Codigo = detalle[0],
                    Producto = cmb_Productos.SelectedItem.ToString(),
                    Cantidad = txb_catidad_producto.Text,
                    Precio = valorUnitario.ToString("F"),
                    Descuento = txb_descuento_linea_producto.Text,
                    Impuesto = impuestoLinea.ToString("F"),
                    Total = ((valorUnitario * cantidad) * (impuestoLinea/100) + (valorUnitario * cantidad)-descuentoLinea).ToString("F")

                });
                //Le damos valor a "montos" la cual es una variable global para establecer el subtotal de la factura
                montos += ((valorUnitario * cantidad) * (impuestoLinea/100) + (valorUnitario * cantidad)-descuentoLinea);
                txb_subtotal_factura_prueba.Text = montos.ToString("F");
                //Verificamos si ya el producto fue ingresado prviamente
                if (verificarProductoFactura(cmb_Productos.SelectedItem.ToString()) == 1)
                {
                    dtg_facturar_productos_prueba.Items.Add(Row2);
                    txb_catidad_producto.Text = "";
                    cmb_Productos.SelectedItem = null;
                    txb_descuento_linea_producto.Text = "0";
                    Rb_siProducto.IsChecked = true;
                    float subtotal = float.Parse(txb_subtotal_factura_prueba.Text);
                    float porcentaje_descuento = float.Parse(txb_descuento_Producto_prueba.Text) / 100;
                    float descuento = subtotal * porcentaje_descuento;
                    txb_subtotalneto.Text = (subtotal - descuento).ToString("F");
                    CalculaMontos();// Calculamos los montos de la casillas con el nuevo valor

                }
                else if (verificarProductoFactura(cmb_Productos.SelectedItem.ToString()) == 0)
                {
                    montos -= ((valorUnitario * cantidad) * (impuestoLinea / 100) + (valorUnitario * cantidad) - descuentoLinea);
                    txb_subtotal_factura_prueba.Text = montos.ToString("F");
                    MessageBox.Show("El producto fue previamente ingresado, por favor verificar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Seleccione el producto,digite la cantidad,el descuento y seleccione el impuesto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Funcion para calcular las respectivas casillas de la factura de productos
        public void CalculaMontos()
        {
            if (txb_descuento_Producto_prueba != null)
            {
                //verificamos los valores que puede tener el descuento
                if (txb_descuento_Producto_prueba.Text == "" || txb_descuento_Producto_prueba.Text == "0" || txb_descuento_Producto_prueba.Text == " ")
                {
                    txb_subtotalneto.Text = txb_subtotal_factura_prueba.Text;
                    if (RadioButton_Si1.IsChecked == true)
                    {
                        txb_impuestofactura.Text = (float.Parse(txb_subtotalneto.Text) * 0.13).ToString("F");//sacamos el 13% del subtotalneto para tener el impuesto de la factura
                    }
                    else if (RadioButton_No1.IsChecked == true)
                    {
                        txb_impuestofactura.Text = "0";
                    }
                        //calculamos el total de la factura
                        if (txb_total_factura_prueba.Text != "")
                        txb_total_factura_prueba.Text = (float.Parse(txb_subtotalneto.Text) + float.Parse(txb_impuestofactura.Text)).ToString("F");
                }
                else
                {
                    //calculamos el descuento y le aplicamos el impuesto en caso de que exista 
                    float subtotal = float.Parse(txb_subtotal_factura_prueba.Text);
                    float porcentaje_descuento = float.Parse(txb_descuento_Producto_prueba.Text) / 100;
                    float descuento = subtotal * porcentaje_descuento;
                    txb_subtotalneto.Text = (subtotal - descuento).ToString("F");
                    if (RadioButton_Si1.IsChecked == true)
                    {
                        txb_impuestofactura.Text = (float.Parse(txb_subtotalneto.Text) * 0.13).ToString();
                        txb_total_factura_prueba.Text = (float.Parse(txb_subtotalneto.Text) + float.Parse(txb_impuestofactura.Text)).ToString("F");
                        if (txb_impuestofactura.Text != "")
                            txb_total_factura_prueba.Text = (float.Parse(txb_subtotalneto.Text) + float.Parse(txb_impuestofactura.Text)).ToString("F");
                    }
                    else if (RadioButton_No1.IsChecked == true)
                    {
                        txb_impuestofactura.Text = "0";
                        txb_total_factura_prueba.Text = (float.Parse(txb_subtotalneto.Text) + float.Parse(txb_impuestofactura.Text)).ToString("F");
                        if (txb_impuestofactura.Text != "")
                            txb_total_factura_prueba.Text = (float.Parse(txb_subtotalneto.Text) + float.Parse(txb_impuestofactura.Text)).ToString("F");
                    }
                }
            }
        }
        //Evento del boton para gregar una nueva fila
        private void btn_agregar_producto_prueba_Click(object sender, RoutedEventArgs e)
        {
            AgregarRow();
        }
        //funcion para eliminar un producto de la lista
        private void btn_eliminar_producto_Click(object sender, RoutedEventArgs e)
        {
            //le descontamos el precio que habia en la lista
            montos -= float.Parse((dtg_facturar_productos_prueba.SelectedCells[7].Column.GetCellContent(dtg_facturar_productos_prueba.SelectedItem) as TextBlock).Text);
            txb_subtotal_factura_prueba.Text = montos.ToString("F");
            CalculaMontos();//volvemos a recalcular los montos 
            //vemos cual es la fila que se desea eliminar
            ObservableCollection<FacturasProducto> row = (ObservableCollection<FacturasProducto>)dtg_facturar_productos_prueba.SelectedItem;
            //eliminamos la fila seleccionada
            dtg_facturar_productos_prueba.Items.Remove(row);
        }

        //Iniciamos el combobox de clientes en la facturacion de productos , con los clientes activos en el sistema
        private void cmb_Cliente_Productos_Prueba_Initialized(object sender, EventArgs e)
        {
            Iniciar_Clientes(cmb_Cliente_Productos_Prueba);
        }
        //Iniciaos el combobox de productos en la facturacion de productos, con los productos activos en el sistema,mostramos su respectiva descripcion
        private void cmb_Productos_Initialized(object sender, EventArgs e)
        {
            DataTable dt = datos.Productos();

            foreach (DataRow fila in dt.Rows)
            {
                cmb_Productos.Items.Add(Convert.ToString(fila["DESCRIPCION"]));
            }
        }
        //Validamos que solo se puedan digitar numeros
        private void Txb_catidad_producto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(txb_catidad_producto, lbl_error_cantidadproducto, e);
        }

        private void Txb_descuento_linea_producto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(txb_descuento_linea_producto, lbl_error_descuentoproducto, e);
        }

        private void Txb_descuento_Producto_prueba_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(txb_descuento_Producto_prueba, lbl_error_descuentofacturaproductos, e);
        }

        private void Txb_diasCredito_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(txb_diasCredito, lbl_error_creditoproductos, e);
        }
        //funcion para verificar los valores que se van a cambiar en el textbox de cantidad de producto solicitada
        private void txb_cantidad_producto_TextChanged(object sender, TextChangedEventArgs e)
        {
                var detalle = new List<string>();
                int cantidadsolicitada = 0;
            //tomamos los valores del producto solicitado. 
                detalle = datos.DetalleProducto(cmb_Productos.SelectedItem.ToString());
            //igualamos la variable existencia con la cantidad de productos existentes en el inventario.
                int existencia = int.Parse(detalle[2]);
            //verificamos el valor que tiene la cantidad de productos
            if(txb_catidad_producto.Text != "")
            {
                cantidadsolicitada = int.Parse(txb_catidad_producto.Text);
            }
            else
            {
                cantidadsolicitada = 0;
            }
            //verificamos que la cantidadsolicitada sea menor que la existencia, o nos dira el valor maximo que se puede comprar
                if (cantidadsolicitada > existencia)
                {
                    MessageBox.Show("La cantidad maxima en inventario es " + existencia, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txb_catidad_producto.Text = existencia.ToString();
                }
        }

        //verificamos que tenga algun valor el combo de productos para habilitar la cantidad y descuento del producto
        private void cmb_Productos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmb_Productos.SelectedItem == null)
            {
                txb_catidad_producto.IsReadOnly = true;
                txb_descuento_linea_producto.IsReadOnly = true;
            }
            else if (cmb_Productos.SelectedItem != null)
            {
                txb_catidad_producto.IsReadOnly = false;
                txb_descuento_linea_producto.IsReadOnly = false;
            }
        }
        //Funcion para modificar los productos ya ingresados en el datagrid 
        //esta funcion esta declarada en el xaml del datagrid
        private void Row_Double_Click(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;

            txt_Estado.Content = "Modificar";
            btn_agregar_producto_prueba.Visibility = Visibility.Hidden;
            btn_modificar_producto_prueba.Visibility = Visibility.Visible;

            //establecemos los diferentes valores a los campos correspondientes de la factura de productos
            montos -= float.Parse((dtg_facturar_productos_prueba.SelectedCells[7].Column.GetCellContent(row) as TextBlock).Text);
            cmb_Productos.SelectedItem = (dtg_facturar_productos_prueba.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;
            txb_catidad_producto.Text = (dtg_facturar_productos_prueba.SelectedCells[3].Column.GetCellContent(row) as TextBlock).Text;
            txb_descuento_linea_producto.Text = (dtg_facturar_productos_prueba.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text;

            if ((dtg_facturar_productos_prueba.SelectedCells[6].Column.GetCellContent(row) as TextBlock).Text == "13")
            {
                Rb_siProducto.IsChecked = true;
            }
            else if ((dtg_facturar_productos_prueba.SelectedCells[6].Column.GetCellContent(row) as TextBlock).Text == "0")
            {
                Rb_noProducto.IsChecked = true;
            }

        }

        //funcion para modificar el producto deseado
        private void btn_modificar_producto_prueba_Click(object sender, RoutedEventArgs e)
        {
            //primero eliminamos el producto que se solicita
            ObservableCollection<FacturasProducto> row = (ObservableCollection<FacturasProducto>)dtg_facturar_productos_prueba.SelectedItem;
            dtg_facturar_productos_prueba.Items.Remove(row);
            //despues agregamos el nuevo o el cambio solicitado
            AgregarRow();
            btn_modificar_producto_prueba.Visibility = Visibility.Hidden;
            txt_Estado.Content = "Agregar";
            btn_agregar_producto_prueba.Visibility = Visibility.Visible;
        }

        //Calculamos todos los montos de la factura de productos cuando se modifica el descueto de la factura
        private void txb_descuento_Producto_prueba_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculaMontos();
        }
        //verificamos montos cuando se cambia el valor del subtotal de la factura
        private void txb_subtotal_factura_prueba_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txb_descuento_Producto_prueba.Text == "")
            {
                txb_descuento_Producto_prueba.Text = "0";
            }
            float subtotal = float.Parse(txb_subtotal_factura_prueba.Text);
            float porcentaje_descuento = float.Parse(txb_descuento_Producto_prueba.Text) / 100;
            float descuento = subtotal * porcentaje_descuento;
            txb_subtotalneto.Text = (subtotal - descuento).ToString("F");
            if(txb_impuestofactura.Text != "")
            txb_total_factura_prueba.Text = (float.Parse(txb_subtotalneto.Text) + float.Parse(txb_impuestofactura.Text)).ToString("F");
        }
        //se calcula el impuesto dependiendo si el usuario lo quiere incluir.
        private void RadioButton_Si1_Checked(object sender, RoutedEventArgs e)
        {
            if(txb_impuestofactura != null && txb_subtotalneto != null)
            {
                txb_impuestofactura.Text = (float.Parse(txb_subtotalneto.Text) * 0.13).ToString("F");
                txb_total_factura_prueba.Text = (float.Parse(txb_subtotalneto.Text) + float.Parse(txb_impuestofactura.Text)).ToString("F");
            }
        }

        private void RadioButton_No1_Checked(object sender, RoutedEventArgs e)
        {
            if (txb_impuestofactura != null && txb_subtotalneto != null)
            {
                txb_impuestofactura.Text = "0";
                txb_total_factura_prueba.Text = (float.Parse(txb_subtotalneto.Text) + float.Parse(txb_impuestofactura.Text)).ToString("F");
            }
        }

        //funcion para verificar si el producto fue previamente agregado a la factura
        public int verificarProductoFactura(string n_producto)
        {
            string producto = "";
            int valor = 1;
            foreach (var item in dtg_facturar_productos_prueba.Items)
            {
                DataGridRow row = (DataGridRow)dtg_facturar_productos_prueba.ItemContainerGenerator.ContainerFromItem(item);

                if (dtg_facturar_productos_prueba.Columns[0].GetCellContent(row) is TextBox)
                {
                    producto = ((TextBox)dtg_facturar_productos_prueba.Columns[2].GetCellContent(row)).Text;
                }
                else if (dtg_facturar_productos_prueba.Columns[0].GetCellContent(row) is TextBlock)
                {
                    producto = ((TextBlock)dtg_facturar_productos_prueba.Columns[2].GetCellContent(row)).Text;
                }
                if (n_producto == producto)//si ya existe retorna 0 en caso contrario 1
                {
                    valor = 0;
                }
            }
            if (n_producto == producto)//si ya existe retorna 0 en caso contrario 1
            {
                return valor;
            }
            else
            {
                return valor;
            }
        }
        //funcion que hace el cambio a dolar en los productos del datagrid
        private void cambioDolar()
        {
            if (dtg_facturar_productos_prueba != null )
            {
                foreach (var item in dtg_facturar_productos_prueba.Items)
                {
                    DataGridRow row = (DataGridRow)dtg_facturar_productos_prueba.ItemContainerGenerator.ContainerFromItem(item);
                    string valor_colones = "";
                    string cantidad = "";
                    string impuesto = "";
                    float descuento= 0;
                    float valor_dolar = datos.ObtenerValorDolar();

                    if (dtg_facturar_productos_prueba.Columns[0].GetCellContent(row) is TextBlock)
                    {
                        cantidad = ((TextBlock)dtg_facturar_productos_prueba.Columns[3].GetCellContent(row)).Text;
                        valor_colones = ((TextBlock)dtg_facturar_productos_prueba.Columns[4].GetCellContent(row)).Text;
                        impuesto = ((TextBlock)dtg_facturar_productos_prueba.Columns[6].GetCellContent(row)).Text;
                    }
                    try
                    {
                        ((TextBlock)dtg_facturar_productos_prueba.Columns[4].GetCellContent(row)).Text = (float.Parse(valor_colones) / valor_dolar).ToString("F");
                        valor_colones = (float.Parse(valor_colones) / valor_dolar).ToString("F");//F es para que me tire el valor con dos decimales
                        descuento = (((float.Parse(valor_colones) * float.Parse(cantidad)) * (float.Parse(impuesto) / 100) + (float.Parse(valor_colones) * float.Parse(cantidad))) * (float.Parse(txb_descuento_linea_producto.Text) / 100));
                        ((TextBlock)dtg_facturar_productos_prueba.Columns[7].GetCellContent(row)).Text = ((float.Parse(valor_colones) * float.Parse(cantidad)) * (float.Parse(impuesto) / 100) + ((float.Parse(valor_colones) * float.Parse(cantidad)) - descuento)).ToString("F");
                        CalculaMontos();
                        montos = float.Parse(txb_subtotal_factura_prueba.Text);

                    }
                    catch (Exception m)
                    {
                        MessageBox.Show("Error al hacer el cambio", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Console.WriteLine(m.ToString());
                    }
                }
            }
        }

        //funcion para tomar el subtotal de la factura 
        public void subtotalFactura()
        {
            string subtotalLinea = "";
            float subtotal_monto = 0;
            if (dtg_facturar_productos_prueba != null )
                {
                    foreach (var item in dtg_facturar_productos_prueba.Items)
                    {
                        DataGridRow row = (DataGridRow)dtg_facturar_productos_prueba.ItemContainerGenerator.ContainerFromItem(item);

                        if (dtg_facturar_productos_prueba.Columns[0].GetCellContent(row) is TextBlock)
                        {
                        subtotalLinea = ((TextBlock)dtg_facturar_productos_prueba.Columns[7].GetCellContent(row)).Text;
                        }
                        try
                        {
                        subtotal_monto += float.Parse(subtotalLinea);
                        }
                        catch (Exception m)
                        {
                            MessageBox.Show("Error al hacer el cambio", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            Console.WriteLine(m.ToString());
                        }
                    }
                txb_subtotal_factura_prueba.Text = subtotal_monto.ToString("F");
                montos = float.Parse(subtotal_monto.ToString("F"));
                CalculaMontos();
            }
        }
        //funcion que hace el cambio a dolar en los productos del datagrid
        private void cambioColon()
        {
            if (dtg_facturar_productos_prueba != null )
            {
                if (dtg_facturar_productos_prueba.Items.Count == 0)
                {
                    limpiar_Facturaprodutcos();
                }
                else
                {

                    foreach (var item in dtg_facturar_productos_prueba.Items)
                    {
                        DataGridRow row = (DataGridRow)dtg_facturar_productos_prueba.ItemContainerGenerator.ContainerFromItem(item);
                        string valor_Producto = "";
                        string cantidad = "";
                        string impuesto = "";
                        float descuento = 0;
                        float valor_dolar = datos.ObtenerValorDolar();

                        if (dtg_facturar_productos_prueba.Columns[0].GetCellContent(row) is TextBlock)
                        {
                            cantidad = ((TextBlock)dtg_facturar_productos_prueba.Columns[3].GetCellContent(row)).Text;
                            valor_Producto = ((TextBlock)dtg_facturar_productos_prueba.Columns[4].GetCellContent(row)).Text;
                            impuesto = ((TextBlock)dtg_facturar_productos_prueba.Columns[6].GetCellContent(row)).Text;
                        }
                        try
                        {
                            ((TextBlock)dtg_facturar_productos_prueba.Columns[4].GetCellContent(row)).Text = (float.Parse(valor_Producto) * valor_dolar).ToString("F");
                            valor_Producto = (float.Parse(valor_Producto) * valor_dolar).ToString();
                            descuento = (((float.Parse(valor_Producto) * float.Parse(cantidad)) * (float.Parse(impuesto) / 100) + (float.Parse(valor_Producto) * float.Parse(cantidad))) * (float.Parse(txb_descuento_linea_producto.Text) / 100));
                            ((TextBlock)dtg_facturar_productos_prueba.Columns[7].GetCellContent(row)).Text = ((float.Parse(valor_Producto) * float.Parse(cantidad)) * (float.Parse(impuesto) / 100) + ((float.Parse(valor_Producto) * float.Parse(cantidad)) - descuento)).ToString("F");
                            CalculaMontos();
                            montos = float.Parse(txb_subtotal_factura_prueba.Text);
                        }
                        catch (Exception m)
                        {
                            MessageBox.Show("Error al hacer el cambio", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            Console.WriteLine(m.ToString());
                        }
                    }
                }
            }
        }


        //funcion para guardar la factura dentro de la base de datos
        private void GuardarDetalleFactura ()
        {
            string codigo = "";
            string producto = "";
            string cantidad = "";
            string precio = "";
            string subtotal = "";
            string descuento = "";
            string impuesto = "";
            string nuevo_valor_existencia = "";
            var detalle = new List<string>();

            //recorremos el datagrid y le damos el valor de cada campo de la fila a las diferentes variables,para agregar cada detalle de la factura solicitada.
            foreach (var item in dtg_facturar_productos_prueba.Items)
            {
                DataGridRow row = (DataGridRow)dtg_facturar_productos_prueba.ItemContainerGenerator.ContainerFromItem(item);
                    codigo = ((TextBlock)dtg_facturar_productos_prueba.Columns[1].GetCellContent(row)).Text;
                    producto = ((TextBlock)dtg_facturar_productos_prueba.Columns[2].GetCellContent(row)).Text;
                    cantidad = ((TextBlock)dtg_facturar_productos_prueba.Columns[3].GetCellContent(row)).Text;
                    precio = ((TextBlock)dtg_facturar_productos_prueba.Columns[4].GetCellContent(row)).Text;
                    descuento = ((TextBlock)dtg_facturar_productos_prueba.Columns[5].GetCellContent(row)).Text;
                    impuesto = ((TextBlock)dtg_facturar_productos_prueba.Columns[6].GetCellContent(row)).Text;
                    subtotal = ((TextBlock)dtg_facturar_productos_prueba.Columns[7].GetCellContent(row)).Text;
                try
                {

                    //tomamos los valores del producto solicitado. 
                    detalle = datos.DetalleProducto(producto);
                    //igualamos la variable existencia con la cantidad de productos existentes en el inventario.
                    int existencia = int.Parse(detalle[2]);
                    miDetalle.subtotal = float.Parse(subtotal);
                    miDetalle.descripcion_servicio = null;
                    miDetalle.id_producto = int.Parse(datos.id_Producto(producto).ToString());
                    miDetalle.id_factura = int.Parse(txt_codigo_factura.Content.ToString());
                    miDetalle.cantidad = int.Parse(cantidad);
                    miDetalle.impuesto = impuesto;
                    miDetalle.descuento = descuento;
                    miDetalle.precioProducto = precio;
                    int v_Resultado = datos.AgregarDetalle(miDetalle);
                    nuevo_valor_existencia = (existencia - int.Parse(cantidad)).ToString();
                    if (v_Resultado == -1)
                    {
                        datos.DescuentoInventario(nuevo_valor_existencia, producto);
                    }
                }
                catch (Exception m)
                {
                    MessageBox.Show(m.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Console.WriteLine(m.ToString());
                }
            }
        }

        //Agregamos la factura la base de datos 
        private void btn_imprimir_factura_prueba_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //verificamos que todos los campos tengan sus valores solicitados
                if (cmb_Cliente_Productos_Prueba.SelectedItem == null || dtg_facturar_productos_prueba.Items.Count == 0 || txb_descuento_Producto_prueba.Text == "")
                {
                    MessageBox.Show("No se puede imprimir\nHacen falta campos por rellenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    //agregamos los diferentes valores a la entidad de la factura
                    miFactura.v_Codigo = int.Parse(txt_codigo_factura.Content.ToString());
                    miFactura.v_Usuario = datos.id_Usuario(t_Usuario.Text).ToString();

                    EntidadClientes micliente = new EntidadClientes();
                    micliente = datos.id_Cliente(cmb_Cliente_Productos_Prueba.SelectedItem.ToString());
                    miFactura.v_Cliente = micliente.v_Codigo.ToString();

                    miFactura.v_Total = txb_total_factura_prueba.Text;
                    miFactura.v_Descuento = txb_descuento_Producto_prueba.Text;
                    if (RadioButton_Colon1.IsChecked == true)
                    {
                        miFactura.v_Moneda = "Colones";
                    }
                    else if (RadioButton_Dolar1.IsChecked == true)
                    {
                        miFactura.v_Moneda = "Dolares";
                    }
                    if (RadioButton_Si1.IsChecked == true)
                    {
                        miFactura.v_Impuesto = txb_impuestofactura.Text;
                    }
                    else if (RadioButton_No1.IsChecked == true)
                    {
                        miFactura.v_Impuesto = "0";
                    }
                    miFactura.v_tipoFactura = "Productos";
                    miFactura.v_Subtotal = txb_subtotal_factura_prueba.Text;
                    miFactura.v_SubtotalNeto = txb_subtotalneto.Text;
                    miFactura.v_tipoPago = tipoPago;
                    miFactura.v_diasCredito = diasCredito;
                    miFactura.v_estadoFactura = estadoFactura;
                    miFactura.v_fechaPago = fechaPago;
                    miFactura.v_fechaCancelacion = fechaPago;
                    miFactura.v_tipoCambio = datos.ObtenerValorDolar().ToString("F");
                    //verificamos si se puede agregar la factura o hay errores 
                        int v_Resultado = datos.AgregarFacturas(miFactura);
                        if (v_Resultado == -1)
                        {
                            GuardarDetalleFactura();
                           
                            Imprimir imprimir = new Imprimir();
                            imprimir.imprimirFactura(dtg_facturar_productos_prueba, miFactura, DateTime.Now, micliente,miFactura.v_Moneda, miDetalle);
                        bitacora.usuario_Responsable = t_Usuario.Text;
                        bitacora.accion = "Facturar";
                        bitacora.ventana_Mantenimiento = "Facturación Productos";
                        bitacora.descripcion = "Realizó una factura con el código: " + txt_codigo_factura.Content + ", al cliente: " + cmb_Cliente_Productos_Prueba.SelectedItem.ToString() + ", con un monto de " + txb_total_factura_prueba.Text + " " + miFactura.v_Moneda;
                        datos.AgregarBitacora(bitacora);
                        limpiar_Facturaprodutcos();
                        }
                }
            }
            catch (Exception m)
            {
                MessageBox.Show(m.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(m.ToString());
            }
        }

        private void RadioButton_Colon1_Checked(object sender, RoutedEventArgs e)
        {
            cambioColon();
            subtotalFactura();
            CalculaMontos();
        }

        private void RadioButton_Dolar1_Checked(object sender, RoutedEventArgs e)
        {
            cambioDolar();
            subtotalFactura();
            CalculaMontos();
        }

        private void Rb_Contado_Checked(object sender, RoutedEventArgs e)
        {
            DateTime dia = DateTime.Now;
            tipoPago = "Contado";
            diasCredito = "0";
            estadoFactura = "Cancelado";
            fechaPago = dia.ToString("dd/MM/yyyy");
            if (txb_diasCredito != null)
            {
                txb_diasCredito.Visibility = Visibility.Hidden;
                btn_agregarCredito.Visibility = Visibility.Hidden;
                txt_Credito.Visibility = Visibility.Hidden;
            }
        }

        private void Rb_Credito_Checked(object sender, RoutedEventArgs e)
        {
            txb_diasCredito.Visibility = Visibility.Visible;
            btn_agregarCredito.Visibility = Visibility.Visible;
            txt_Credito.Visibility = Visibility.Visible;
        }

        private void btn_agregarCredito_Click(object sender, RoutedEventArgs e)
        {
            if(txb_diasCredito.Text != "" )
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
                int diasContado = int.Parse(txb_diasCredito.Text);
                DateTime dia = DateTime.Now.AddDays(diasContado);
                tipoPago = "Credito";
                diasCredito = txb_diasCredito.Text;
                estadoFactura = "Pendiente";
                fechaPago = dia.ToString("dd/MM/yyyy");

                MessageBox.Show("Tiene credito hasta el "+fechaPago, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                txb_diasCredito.Visibility = Visibility.Hidden;
                btn_agregarCredito.Visibility = Visibility.Hidden;
                txt_Credito.Visibility = Visibility.Hidden;
            }

        }


        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////
        /// </summary>
        /////////////////////////////////////////////////FACTURACION DE SERVICIOS //////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //iniciamos la facturacion de servicios
        private void TabItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                txt_codigo_factura_servicios.Content = (datos.MaximaFactura() + 1).ToString();
            }
            catch (Exception m)
            {
                txt_codigo_factura_servicios.Content = "1";
            }
            if( txb_descripcion.Text == "")
            {
                txb_Cantidad.Text = "0";
                txb_Precio.Text = "0";
                txb_subtotal_factura_servicios.Text = "0";
                txb_descuento_servicios.Text = "0";
                txb_total_factura_servicios.Text = "0";
                txb_impuesto_Servicios.Text = "0";
                txb_subneto_servicios.Text = "0";
                txb_porcentaje_impuesto.Text = "0";
            }
        }

        //funcion para limpiar los datos de la facturas de servicios
        public void LimpiarServicio()
        {
            cmb_Cliente_servicios.SelectedItem = null;
            txt_codigo_factura_servicios.Content = (datos.MaximaFactura() + 1).ToString();
            txb_impuesto_Servicios.Text = "0";
            txb_subneto_servicios.Text = "0";
            txb_descripcion.Text = "";
            txb_descuento_servicios.Text = "0";
            txb_subtotal_factura_servicios.Text = "0";
            txb_total_factura_servicios.Text = "0";
            txb_Cantidad.Text = "0";
            txb_Precio.Text = "0";
            txb_porcentaje_impuesto.Text = "0";
            txb_diasCredito_Servicio.Text = "0";
            montos = 0;
            tipoPago = "";
            diasCredito = "";
            estadoFactura = "";
            fechaPago = "";
            Rb_Contado_Servicio.IsChecked = true;
            Rb_Si_Servicio.IsChecked = true;
            Rb_Colon_Servicio.IsChecked = true;

        }

        //FUNCION PARA HACER LOS CALCULOS NECESARIOS DE LA FACTURACION DE SRVICIOS
        private void CalculaMontosServicios()
        {
            if (txb_Cantidad.Text!="" && txb_Precio.Text!="" && txb_subtotal_factura_servicios.Text!="" && txb_descuento_servicios.Text!="" && txb_total_factura_servicios.Text!="" && txb_impuesto_Servicios.Text!="" && txb_subneto_servicios.Text!="")
            {
                txb_subtotal_factura_servicios.Text = (float.Parse(txb_Cantidad.Text) * float.Parse(txb_Precio.Text)).ToString("F");
                txb_subneto_servicios.Text = (float.Parse(txb_subtotal_factura_servicios.Text) - (float.Parse(txb_subtotal_factura_servicios.Text) * (float.Parse(txb_descuento_servicios.Text) / 100))).ToString("F");
                if(Rb_Si_Servicio.IsChecked == true)
                {
                    if (txb_porcentaje_impuesto.Text != "")
                        txb_impuesto_Servicios.Text = (float.Parse(txb_subneto_servicios.Text) * (float.Parse(txb_porcentaje_impuesto.Text) / 100)).ToString("F");
                    else
                        txb_impuesto_Servicios.Text = "0";
                }
                if (Rb_No_Servicio.IsChecked == true)
                    txb_impuesto_Servicios.Text = "0";

                txb_total_factura_servicios.Text = (float.Parse(txb_subneto_servicios.Text) + (float.Parse(txb_impuesto_Servicios.Text))).ToString("F");
            }
        }

        //SI EL USUARIO DESEA HACER LA FACTURA A CREDITO
        private void Rb_Credito_Servicio_Checked(object sender, RoutedEventArgs e)
        {
            txb_diasCredito_Servicio.Visibility = Visibility.Visible;
            btn_agregarCredito_Servicio.Visibility = Visibility.Visible;
            txt_Credito_Servicio.Visibility = Visibility.Visible;
        }

        //SI EL USUARIO DESEA HACER LA FACTURA A CONTADO
        private void Rb_Contado_Servicio_Checked(object sender, RoutedEventArgs e)
        {
            //System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            DateTime dia = DateTime.Now;
            tipoPago = "Contado";
            diasCredito = "0";
            estadoFactura = "Cancelado";
            fechaPago = dia.ToString("dd/MM/yyyy");
            if (txb_diasCredito != null)
            {
                txb_diasCredito_Servicio.Visibility = Visibility.Hidden;
                btn_agregarCredito_Servicio.Visibility = Visibility.Hidden;
                txt_Credito_Servicio.Visibility = Visibility.Hidden;
            }
        }
        //funcion para poder dar salto de linea en el textbox de la descripcion del servicio
        private void txb_descripcion_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;

            // your event handler here
            e.Handled = true;
            var caretIndex = txb_descripcion.CaretIndex;
            txb_descripcion.Text = txb_descripcion.Text.Insert(caretIndex, "\r");
            txb_descripcion.CaretIndex = caretIndex + 1;
        }
        //iniciamos el combobox de clientes , con los clientes activos en el sistema
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
        //VALIDACIONES FACTURACION DE SERVICIOS
        public void SoloNumeros(TextBox txb,Label lbl,TextCompositionEventArgs e)
        {
            //se convierte a Ascci del la tecla presionada 
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            //verificamos que se encuentre en ese rango que son entre el 0 y el 9 
            if (ascci >= 48 && ascci <= 57)
            {
                e.Handled = false;
                lbl.Visibility = Visibility.Collapsed;
                if (ValidarCaracteresEspeciales(txb.Text) == true)
                {
                    lbl.Content = "No se permiten caracteres especiales";
                    lbl.Visibility = Visibility.Visible;
                }
            }
            else
            {
                e.Handled = true;
                lbl.Content = "No se permite ingresar letras";
                lbl.Visibility = Visibility.Visible;
            }
        }
        private void Txb_diasCredito_Servicio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(txb_diasCredito_Servicio,lbl_error_diasCredito,e);
        }
        private void Txb_porcentaje_impuesto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(txb_porcentaje_impuesto,lbl_error_impuesto, e);
        }

        private void Txb_Cantidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(txb_Cantidad, lbl_error_horas, e);
        }

        private void Txb_Precio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(txb_Precio, lbl_error_precio, e);
        }

        private void Txb_descuento_servicios_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(txb_descuento_servicios, lbl_error_descuento, e);
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


        //funcion para poder agregar los dias de credito a la facturacion de servicios
        private void btn_agregarCredito_Servicio_Click(object sender, RoutedEventArgs e)
        {
            if (txb_diasCredito_Servicio.Text != "")
            {
                //System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
                int diasContado = int.Parse(txb_diasCredito_Servicio.Text);
                DateTime dia = DateTime.Now.AddDays(diasContado);
                tipoPago = "Credito";
                diasCredito = txb_diasCredito_Servicio.Text;
                estadoFactura = "Pendiente";
                fechaPago = dia.ToString("dd/MM/yyyy");

                MessageBox.Show("Tiene credito hasta el " + fechaPago, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                txb_diasCredito_Servicio.Visibility = Visibility.Hidden;
                btn_agregarCredito_Servicio.Visibility = Visibility.Hidden;
                txt_Credito_Servicio.Visibility = Visibility.Hidden;
            }
        }
        //si desea hacer la facturas en dolares
        private void Rb_Dolar_Servicio_Checked(object sender, RoutedEventArgs e)
        {
            if (txb_Precio.Text != "")
            {
                txb_Precio.Text = (float.Parse(txb_Precio.Text) / datos.ObtenerValorDolar()).ToString();
                CalculaMontosServicios();
            }
        }
        //si desea usar la factura en coloes
        private void Rb_Colon_Servicio_Checked(object sender, RoutedEventArgs e)
        {
            if(txb_Precio.Text!="")
            {
                txb_Precio.Text = (float.Parse(txb_Precio.Text) * datos.ObtenerValorDolar()).ToString();
                CalculaMontosServicios();
            }
        }
        //En caso que el usuario necesite aplicar impuesto a la factura de servicios
        private void Rb_Si_Servicio_Checked(object sender, RoutedEventArgs e)
        {
            if (txb_impuesto_Servicios != null && txb_porcentaje_impuesto != null)
            {
                txb_porcentaje_impuesto.Visibility = Visibility.Visible;
                txt_porcentaje_impuesto.Visibility = Visibility.Visible;
                txt_impuesto.Visibility = Visibility.Visible;
                CalculaMontosServicios();
            }
        }
        //Si el usuario no sea aplicar impuesto a la factura
        private void Rb_No_Servicio_Checked(object sender, RoutedEventArgs e)
        {
            if (txb_impuesto_Servicios != null && (txb_impuesto_Servicios.Text == "0" || txb_impuesto_Servicios.Text == "0.00") )
            {
                txb_porcentaje_impuesto.Visibility = Visibility.Hidden;
                txt_porcentaje_impuesto.Visibility = Visibility.Hidden;
                txt_impuesto.Visibility = Visibility.Hidden;
                CalculaMontosServicios();
            }
        }
        //funcion para imprimir las facturas de servicios
        private void Button_imprimir_factura_servicio_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //verificamos que todos los campos tengan sus valores solicitados
                if (txb_descuento_servicios.Text == "" || txb_descripcion.Text == "" || txb_Cantidad.Text == "" || txb_Precio.Text == "" || cmb_Cliente_servicios.SelectedItem == null)
                {
                    MessageBox.Show("No se puede imprimir\nHacen falta campos por rellenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    //agregamos los diferentes valores a la entidad de la factura
                    miFactura.v_Codigo = int.Parse(txt_codigo_factura_servicios.Content.ToString());
                    miFactura.v_Usuario = datos.id_Usuario(t_Usuario.Text).ToString();


                    EntidadClientes micliente = new EntidadClientes();
                    micliente = datos.id_Cliente(cmb_Cliente_servicios.SelectedItem.ToString());
                    miFactura.v_Cliente = micliente.v_Codigo.ToString();


                    miFactura.v_Total = txb_total_factura_servicios.Text;
                    miFactura.v_Descuento = txb_descuento_servicios.Text;
                    if (Rb_Colon_Servicio.IsChecked == true)
                    {
                        miFactura.v_Moneda = "Colones";
                    }
                    else if (Rb_Dolar_Servicio.IsChecked == true)
                    {
                        miFactura.v_Moneda = "Dolares";
                    }
                    if (Rb_Si_Servicio.IsChecked == true)
                    {
                        miFactura.v_Impuesto = txb_impuesto_Servicios.Text;
                    }
                    else if (Rb_No_Servicio.IsChecked == true)
                    {
                        miFactura.v_Impuesto = "0";
                    }
                    miFactura.v_tipoFactura = "Servicios";
                    miFactura.v_Subtotal = txb_subtotal_factura_servicios.Text;
                    miFactura.v_SubtotalNeto = txb_subneto_servicios.Text;
                    miFactura.v_tipoPago = tipoPago;
                    miFactura.v_diasCredito = diasCredito;
                    miFactura.v_estadoFactura = estadoFactura;
                    miFactura.v_fechaPago = fechaPago;
                    miFactura.v_fechaCancelacion = fechaPago;
                    miFactura.v_tipoCambio = datos.ObtenerValorDolar().ToString("F");
                    //verificamos si se puede agregar la factura o hay errores 
                    int v_Resultado = datos.AgregarFacturas(miFactura);
                    if (v_Resultado == -1)
                    {
                        try
                        {
                            miDetalle.cantidad = int.Parse(txb_Cantidad.Text);
                            miDetalle.precioProducto = txb_Precio.Text;
                            miDetalle.descripcion_servicio = txb_descripcion.Text;
                            miDetalle.id_factura = int.Parse(txt_codigo_factura_servicios.Content.ToString());
                            int v_ResultadoD = datos.AgregarDetalle(miDetalle);
                            if (v_Resultado == -1)
                            {
                                Imprimir imprimir = new Imprimir();
                                imprimir.imprimirFactura(dtg_facturar_productos_prueba, miFactura, DateTime.Now, micliente, miFactura.v_Moneda,miDetalle);
                                bitacora.usuario_Responsable = t_Usuario.Text;
                                bitacora.accion = "Facturar";
                                bitacora.ventana_Mantenimiento = "Facturación Servicios";
                                bitacora.descripcion = "Realizó una factura con el código: " + txt_codigo_factura_servicios.Content + ", al cliente: " + cmb_Cliente_servicios.SelectedItem.ToString() + ", con un monto de "+ txb_total_factura_servicios.Text+" "+miFactura.v_Moneda;
                                datos.AgregarBitacora(bitacora);
                                LimpiarServicio();
                            }else
                            {
                                MessageBox.Show("Error al ingresar la factura", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        catch(Exception m)
                        {
                            Console.WriteLine(m);
                        }
                    }
                }
            }
            catch (Exception m)
            {
                MessageBox.Show(m.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(m.ToString());
            }
        }

         //Calculos facturacion de servicios, en los cuatro textbox que pueden ser editados
        //hace calculos en la facturacion de servicios cuando el impuesto cambia
        private void txb_porcentaje_impuesto_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarErroresTxb(txb_porcentaje_impuesto, lbl_error_impuesto);
            if (txb_porcentaje_impuesto.Text == "")
            {
                lbl_error_impuesto.Content = "No se permiten caracteres especiales";
                lbl_error_impuesto.Visibility = Visibility.Collapsed;
            }
            else
            {
                CalculaMontosServicios();
            }
        }

        //hace calculos en la facturacion de servicios cuando la cantidad cambia
        private void txb_Cantidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculaMontosServicios();
        }
        //hace calculos en la facturacion de servicios cuando el precio cambia
        private void txb_Precio_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculaMontosServicios();
        }

        //Hace calculos en la facturacion de servicios cuando el descuento cambia
        private void txb_descuento_servicios_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculaMontosServicios();
        }


    }
}
