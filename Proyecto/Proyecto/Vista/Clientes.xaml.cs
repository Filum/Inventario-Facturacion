using System;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using Entidades;
using Logica;


namespace Proyecto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Clientes : Window
    {
        //Declaramos dos objetos, uno de la entidad de clientes y otro del model.
        EntidadClientes clt = new EntidadClientes();
        Model model = new Model();
        public Clientes()
        {
            InitializeComponent();
            //Formato para la hora, se ejecuta por medio de un hilo.
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(Hora_Fecha);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        //Se toma el txt_fecha para establecer la hra del sistema.
        private void Hora_Fecha(object sender, EventArgs e)
        {
            txt_fecha.Content = DateTime.Now.ToString();

        }

        //funcion para mostar mensajes de ayuda para el usuario.
        private void btn_Ayuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sección Mantenimiento de Clientes:\n\n" + "Listar:\n 1 - Si desea listar clientes primero debe seleccionar el rango de fecha en el cual los quiere listar. \n"+
                            "2 - Después se le da “click” en el botón de listar, este desplegara los datos de los clientes. \n"+
                            "3 - También se puede ordenar alfabéticamente o en orden descendente, dando “click” en la columna que se desea. \n"+
                             "Actualizar Clientes\n" +
                             "1 - Si desea actualiar un cliente primero debe buscarlo por nombre del mismo.\n" +
                             "2 - Una ves que lo encontró lo puede seleccionar,editar y guardar los cambios.\n\n" +

                             "Ingresar Clientes\n" +
                             "1 - Si desea ingresar un cliente primero debe buscarlo por nombre del mismo, para comprobar que este no exista previamente.\n" +
                             "2 - Debera completar cada uno de los espacios requeridos para la creación del nuevo cliente.\n" +
                             "3 - En caso de que cometa algún error el sistema le noticará.\n" +
                             "4 - En caso de que todos los datos esten corectos, proceda a crearlo.\n\n" +

                             "Recuerde:\n" +
                             "*Nombre: puede utilizar letras mayúscula y minúsculas.\n" +
                             "* Correo: debe cumplir con el formato de usuario@dominio.extensión\n" +
                             "* Teléfono: solo se aceptan números y no debe contener espacios ni separadores.\n" +
                             "* Observaciones: acá posee la libertad de escribir lo que desee, ya sean letras, números o símbolos.\n\n"

                 , "Ayuda", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Limpiar_Actualizar_Cliente()
        {
            txb_correo.Text = "";
            txb_correo_o.Text = "";
            txb_nombre.Text = "";
            txb_observaciones.Text = "";
            txb_TelMov.Text = "";
            txb_TelOf.Text = "";
            rb_activo.IsChecked = false;
            rb_inactivo.IsChecked = false;
            txb_correo_o.BorderBrush = Brushes.White;
            txb_correo_o.Background = Brushes.White;
            txb_correo.BorderBrush = Brushes.White;
            txb_correo.Background = Brushes.White;
            txb_TelMov.BorderBrush = Brushes.White;
            txb_TelMov.Background = Brushes.White;
            txb_TelOf.BorderBrush = Brushes.White;
            txb_TelOf.Background = Brushes.White;
            txt_error_telM.Visibility = Visibility.Hidden;
            txt_error_TelO.Visibility = Visibility.Hidden;
            txt_error_correo.Visibility = Visibility.Hidden;
            txt_error_correo_o.Visibility = Visibility.Hidden;
        }
        private void btn_Regresar_Click_1(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.Show();//funcion para mostrar otra ventana.
            this.Close();//cierra la ventana en la que se esta en ese momento.
        }

        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.Show();
            this.Close();
        }


        private void btn_usuario_clientes_Click(object sender, RoutedEventArgs e)
        {
            Login ventana = new Login();
            ventana.Show();
            this.Close();
        }

        private void btn_editar_cliente_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("No se ha seleccionado ningun cliente.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        private void btn_guardar_cliente_actualizado_Click(object sender, RoutedEventArgs e)
        {
            try//Comprobamos que se rellenen los espacios obligatorios en la pantlla de actualizar clientes.
            {
                string inactivo;
                if (txb_correo.Text == "" || txb_nombre.Text == "" || txb_TelOf.Text == "" || txb_TelMov.Text == "" || txt_error_TelO.Visibility == Visibility.Visible || txt_error_telM.Visibility == Visibility.Visible || txt_error_correo.Visibility == Visibility.Visible )
                {
                    MessageBox.Show("No se puede modificar\nHacen falta campos por rellenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (rb_activo.IsChecked == true)
                        inactivo = "Activo";
                    else
                        inactivo = "Inactivo";

                    //Extraemos los datos de la pantlla de actualizar clientes y los ingresamos al objeto de clientes.
                    clt.v_NombreCompleto = txb_nombre.Text;
                    clt.v_Teleoficina = Convert.ToInt32(txb_TelOf.Text);
                    clt.v_Telemovil = Convert.ToInt32(txb_TelMov.Text);
                    clt.v_Correo = txb_correo.Text;
                    //A los espacios vacios se rellenan con un "N/A"
                    if (txb_correo_o.Text == "")
                        clt.v_CorreoOpc = "N/A";
                    else
                        clt.v_CorreoOpc = txb_correo_o.Text;

                    clt.v_Inactividad = inactivo;

                    if (txb_correo_o.Text == "")
                        clt.v_Observaciones = "N/A";
                    else
                        clt.v_Observaciones = txb_observaciones.Text;

                    //Obetenemos el resultado de modificar los datos del cliente
                    int v_Resultado = model.ModificarClientes(clt);
                    if (v_Resultado == -1)//Si no surge ningun error ,se modifica correctamente.
                    {
                        MessageBox.Show("Datos modificados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        Limpiar_Actualizar_Cliente();
                        Modificar_No_Editable();
                    }
                }
            }
            catch (Exception m)//En caso de que surgan error, se muestra un mensaje de error.
            {
                Console.WriteLine(m.ToString());
                MessageBox.Show("Error al modificar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_limpiar_actualizar_cliente_Click(object sender, RoutedEventArgs e)
        {
            Limpiar_Actualizar_Cliente();
        }



        private void btn_salir_clientes_Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.Show();
            this.Close();
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


        private void btn_Listar_Click(object sender, RoutedEventArgs e)
        {
            if (date_historial_clientes_inicio.SelectedDate != null && date_historial_clientes_final.SelectedDate != null)
            {
                DateTime date1 = DateTime.Parse(date_historial_clientes_inicio.SelectedDate.Value.Date.ToShortDateString());
                DateTime date2 = DateTime.Parse(date_historial_clientes_final.SelectedDate.Value.Date.ToShortDateString());
                String fecha1;
                fecha1 = date_historial_clientes_inicio.SelectedDate.Value.Date.ToShortDateString();
                String fecha2;
                fecha2 = date_historial_clientes_final.SelectedDate.Value.Date.ToShortDateString();
                if (date1 > date2)
                {
                    MessageBox.Show("El rango de fechas es incorrecto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    dtg_listar_clientes.ItemsSource = model.MostrarListaClientes(fecha1, fecha2).DefaultView;
                    dtg_listar_clientes.Columns[0].Header = "Código";
                    dtg_listar_clientes.Columns[1].Header = "Fecha Ingreso";
                    dtg_listar_clientes.Columns[2].Header = "Nombre Completo";
                    dtg_listar_clientes.Columns[3].Header = "Tel. Oficina";
                    dtg_listar_clientes.Columns[4].Header = "Tel. Móvil";
                    dtg_listar_clientes.Columns[5].Header = "Correo";
                    dtg_listar_clientes.Columns[6].Header = "Correo Opcional";
                    dtg_listar_clientes.Columns[7].Header = "Estado";
                    dtg_listar_clientes.Columns[8].Header = "Observaciones";
                }

            }
            else
            {
                MessageBox.Show("Seleccione un rango de fechas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void telefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private Boolean email_correcto(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        private void validar_Actualizar_Correo(object sender, EventArgs e)
        {

            Console.WriteLine("Correo: " + txb_correo.Text);

            Console.WriteLine(email_correcto(txb_correo.Text));

            if (email_correcto(txb_correo.Text) == false)
            {
                txb_correo.BorderBrush = Brushes.Red;
                txb_correo.Background = Brushes.Tomato;
                txt_error_correo.Content = "Formato correcto: usuario@dominio.extensión";
                txt_error_correo.Visibility = Visibility.Visible;
            }
            else
            {
                txt_error_correo.Visibility = Visibility.Hidden;
                txb_correo.BorderBrush = Brushes.White;
                txb_correo.Background = Brushes.White;
            }



        }


        private void txb_cliente_modificar_KeyUp(object sender, KeyEventArgs e)
        {
            string texto;
            dtg_clientes.ItemsSource = model.BuscarClientes(txb_buscar_cliente.Text);
            dtg_clientes.Columns[0].Header = "Código";
            dtg_clientes.Columns[1].Header = "Nombre";
            dtg_clientes.Columns[2].Header = "Correo";
            dtg_clientes.Columns[3].Header = "Correo Opcional";
            dtg_clientes.Columns[4].Header = "Tel. Oficina";
            dtg_clientes.Columns[5].Header = "Tel. Móvil";
            dtg_clientes.Columns[6].Header = "Estado";
            dtg_clientes.Columns[7].Header = "Observaciones";
            if (txb_buscar_cliente.Text == "")
            {
                txb_nombre.Text = "";
                Modificar_No_Editable();
                Limpiar_Actualizar_Cliente();
            }
            else
            {
                if (dtg_clientes.Items.Count == 0)
                {
                    texto = txb_buscar_cliente.Text;
                    Agregar(texto);
                    btn_agregar_Cliente.Visibility = Visibility.Visible;
                    btn_guardar_cliente_actualizado.Visibility = Visibility.Hidden;
                }
                else
                {
                    btn_agregar_Cliente.Visibility = Visibility.Hidden;
                    btn_guardar_cliente_actualizado.Visibility = Visibility.Visible;
                    Modificar_No_Editable();
                }
            }

        }

        private void Agregar(string texto)
        {
            Modificar_Si_Editable();
            txb_nombre.Text = texto;
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;

            clt.v_Codigo = Convert.ToInt32((dtg_clientes.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text);

            txb_nombre.Text = (dtg_clientes.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text;
            txb_correo.Text = (dtg_clientes.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;
            txb_correo_o.Text = (dtg_clientes.SelectedCells[3].Column.GetCellContent(row) as TextBlock).Text;
            txb_TelOf.Text = (dtg_clientes.SelectedCells[4].Column.GetCellContent(row) as TextBlock).Text;
            txb_TelMov.Text = (dtg_clientes.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text;

            String vInact = (dtg_clientes.SelectedCells[6].Column.GetCellContent(row) as TextBlock).Text; ;
            if (vInact == "Inactivo")
            {
                rb_inactivo.IsChecked = true;
            }
            else if (vInact == "Activo")
            {
                rb_activo.IsChecked = true;
            }

            Modificar_Si_Editable();
            txb_observaciones.Text = (dtg_clientes.SelectedCells[7].Column.GetCellContent(row) as TextBlock).Text;


        }


        private void validar_Correo_Opc_Act(object sender, RoutedEventArgs e)
        {
            if (email_correcto(txb_correo_o.Text) == false)
            {
                txb_correo_o.BorderBrush = Brushes.Red;
                txb_correo_o.Background = Brushes.Tomato;
                txt_error_correo_o.Content = "Formato correcto: usuario@dominio.extensión";
                txt_error_correo_o.Visibility = Visibility.Visible;
            }
            else
            {
                txt_error_correo_o.Visibility = Visibility.Hidden;
                txb_correo_o.BorderBrush = Brushes.White;
                txb_correo_o.Background = Brushes.White;
            }
        }




        private void validar_actualizar_oficina(object sender, KeyboardFocusChangedEventArgs e)
        {
            string tele1 = txb_TelOf.Text;
            if (tele1.Length < 8)
            {
                txb_TelOf.BorderBrush = Brushes.Red;
                txb_TelOf.Background = Brushes.Tomato;
                txt_error_TelO.Content = "Los números telefónicos tiene que tener un formato valido de 8 dígitos.";
                txt_error_TelO.Visibility = Visibility.Visible;
            }
            else
            {
                txt_error_TelO.Visibility = Visibility.Hidden;
                txb_TelOf.BorderBrush = Brushes.White;
                txb_TelOf.Background = Brushes.White;
            }
        }

        private void validar_actualizar_movil(object sender, KeyboardFocusChangedEventArgs e)
        {
            string tele1 = txb_TelMov.Text;
            if (tele1.Length < 8)
            {
                txb_TelMov.BorderBrush = Brushes.Red;
                txb_TelMov.Background = Brushes.Tomato;
                txt_error_telM.Content = "Los números telefónicos tiene que tener un formato valido de 8 dígitos.";
                txt_error_telM.Visibility = Visibility.Visible;
            }
            else
            {
                txt_error_telM.Visibility = Visibility.Hidden;
                txb_TelMov.BorderBrush = Brushes.White;
                txb_TelMov.Background = Brushes.White;
            }
        }
        public void Modificar_Si_Editable()
        {
            txb_nombre.IsEnabled = true;
            txb_correo.IsEnabled = true;
            txb_correo_o.IsEnabled = true;
            txb_TelOf.IsEnabled = true;
            txb_TelMov.IsEnabled = true;
            txb_observaciones.IsEnabled = true;
            rb_inactivo.IsEnabled = true;
            rb_activo.IsEnabled = true;
        }
        public void Modificar_No_Editable()
        {
            txb_nombre.IsEnabled = false;
            txb_correo.IsEnabled = false;
            txb_correo_o.IsEnabled = false;
            txb_TelOf.IsEnabled = false;
            txb_TelMov.IsEnabled = false;
            txb_observaciones.IsEnabled = false;
            rb_inactivo.IsEnabled = false;
            rb_activo.IsEnabled = false;
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            Modificar_No_Editable();
            btn_guardar_cliente_actualizado.Visibility = Visibility.Hidden;
        }

        private void btn_agregar_Cliente_Click(object sender, RoutedEventArgs e)
        {
            string inactivo;
            try
            {
                if (txb_correo.Text == "" || txb_nombre.Text == "" || txb_TelOf.Text == "" || txb_TelMov.Text == "" || txt_error_TelO.Visibility == Visibility.Visible || txt_error_telM.Visibility == Visibility.Visible || txt_error_correo.Visibility == Visibility.Visible )
                {
                    MessageBox.Show("No se puede agregar\nHacen falta campos por rellenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (rb_activo.IsChecked == true)
                        inactivo = "Activo";
                    else
                        inactivo = "Inactivo";
                    clt.v_NombreCompleto = txb_nombre.Text;
                    clt.v_Teleoficina = Convert.ToInt32(txb_TelOf.Text);
                    clt.v_Telemovil = Convert.ToInt32(txb_TelMov.Text);

                    clt.v_Correo = txb_correo.Text;

                    if (txb_correo_o.Text == "")
                        clt.v_CorreoOpc = "N/A";
                    else
                        clt.v_CorreoOpc = txb_correo_o.Text;

                    clt.v_Inactividad = inactivo;

                    if (txb_observaciones.Text == "")
                        clt.v_Observaciones = "N/A";
                    else
                        clt.v_Observaciones = txb_observaciones.Text;

                    int v_Resultado = model.AgregarClientes(clt);
                    if (v_Resultado == -1)
                    {
                        MessageBox.Show("Datos ingresados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        Limpiar_Actualizar_Cliente();
                    }

                }
            }
            catch (Exception m)
            {
                Console.WriteLine(m.ToString());
                MessageBox.Show("Error al agregar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_usuario_roles_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Desea cerrar sesión?",
                                          "Cerrar Sesión",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Login ventana = new Login();
                this.Close();
                ventana.Show();
            }
        }

        private void dtg_clientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txb_buscar_cliente_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}