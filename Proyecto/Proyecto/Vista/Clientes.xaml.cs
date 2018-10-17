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
            MessageBox.Show("Sección Mantenimiento de Clientes:\n\n" + "Listar: usted podra imprimir la lista de clientes, aparte de ordenarlos ya sea por código o por nombre.\n" +
                             "Actualizar: aca usted podra actualizar la informacion del cliente que desee.\n" +
                             "Ingresar: esta sección le permite la creación de un nuevo cliente.\n" +
                             "Buscar: aca usted podra buscar los clientes que desee.\n" +
                             "Historial: esta ventana le mostrara todos los cambios realizados en esta sección.\n\n" +
                             "Actualizar Clientes\n" +
                             "1 - Si desea actualiar un cliente primero debe buscarlo por nombre o por el código.\n" +
                             "2 - Una ves que lo encontró lo puede seleccionar,editar y guardar los cambios.\n\n" +

                             "Ingresar Clientes\n" +

                             "1 - Debera completar cada uno de los espacios requeridos para la creación del nuevo cliente.\n" +
                             "2 - En caso de que cometa algún error el sistema le noticará.\n" +
                             "3 - En caso de que toos los datos esten corectos, proceda crearlo.\n\n" +

                             "Recuerde:\n" +
                             "*Nombre: puede utilizar letras mayúscula y minúsculas.\n" +
                             "* Correo: debe cumplir con el formato de usuario@dominio.extensión\n" +
                             "* Teléfono: solo se aceptan números y no debe contener espacios ni separadores.\n" +
                             "* Observaciones: acá posee la libertad de escribir loq que desee, ya sean letras, números o símbolos.\n\n" +

                             "Buscar Clientes\n" +

                             "1 - Deberá buscar por nombre del cliente.\n" +
                             "2 - Se le  mostrará el código, nombre completo, correo y teléfono del cliente seleccionado.\n\n" +

                             "Historial de Clientes\n" +

                             "1 - Acá podra ver los cambios realizados por los usuarios, por ejemplo el ingreso de nuevos clientes o actualización de los mismos."
                 , "Ayuda", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Funciones para limpiar los campos de texto en las pestañas de "Ingresar Clientes" y "Modificar Clientes".
        private void Limpiar_Ingresar_Cliente()
        {
            txb_ingresar_correo.Text = "";
            txb_ingresar_correo_o.Text = "";
            txb_ingresar_nombre.Text = "";
            txb_observaciones.Text = "";
            txb_ingresar_TelMov.Text = "";
            txb_ingresar_TelOf.Text = "";
            rb_no_insertar.IsChecked = false;
            rb_si_insertar.IsChecked = false;
            txb_ingresar_correo_o.BorderBrush = Brushes.White;
            txb_ingresar_correo_o.Background = Brushes.White;
        }
        private void Limpiar_Actualizar_Cliente()
        {
            txb_actualizar_correo.Text = "";
            txb_actualizar_correo_o.Text = "";
            txb_actualizar_nombre.Text = "";
            txb_actualizar_observaciones.Text = "";
            txb_actualizar_TelMov.Text = "";
            txb_actualizar_TelOf.Text = "";
            rb_si_actualizar.IsChecked = false;
            rb_no_actualizar.IsChecked = false;
            txb_actualizar_correo_o.BorderBrush = Brushes.White;
            txb_actualizar_correo_o.Background = Brushes.White;
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
                int inactivo;
                if (txb_actualizar_correo.Text == "" || txb_actualizar_nombre.Text == "" || txb_actualizar_TelOf.Text == "" || txb_actualizar_TelMov.Text == "" && rb_si_actualizar.IsChecked == false || rb_no_actualizar.IsChecked == false)
                {
                    MessageBox.Show("No se puede modificar\nHacen falta campos por rellenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (rb_si_insertar.IsChecked == true)
                        inactivo = 1;
                    else
                        inactivo = 0;
                    
                    //Extraemos los datos de la pantlla de actualizar clientes y los ingresamos al objeto de clientes.
                    clt.v_NombreCompleto = txb_actualizar_nombre.Text;
                    clt.v_Teleoficina = Convert.ToInt32(txb_actualizar_TelOf.Text);
                    clt.v_Telemovil = Convert.ToInt32(txb_actualizar_TelMov.Text);
                    clt.v_Correo = txb_actualizar_correo.Text;
                    //A los espacios vacios se rellenan con un "N/A"
                    if (txb_actualizar_correo_o.Text == "")
                        clt.v_CorreoOpc = "N/A";
                    else
                        clt.v_CorreoOpc = txb_actualizar_correo_o.Text;

                    clt.v_Inactividad = inactivo;

                    if (txb_actualizar_correo_o.Text == "")
                        clt.v_Observaciones = "N/A";
                    else
                        clt.v_Observaciones = txb_actualizar_observaciones.Text;

                    //Obetenemos el resultado de modificar los datos del cliente
                    int v_Resultado = model.ModificarClientes(clt);
                    if (v_Resultado == -1)//Si no surge ningun error ,se modifica correctamente.
                    {
                        MessageBox.Show("Datos modificados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        Limpiar_Actualizar_Cliente();
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
      
        //Metodo para ingresar clientes al sistema.
        private void Button_guardar_cliente_Click(object sender, RoutedEventArgs e)
        {
            Int32 inactivo;
            try
            {
                if (txb_ingresar_correo.Text == "" || txb_ingresar_nombre.Text == "" || txb_ingresar_TelOf.Text == "" || txb_ingresar_TelMov.Text == "" && rb_si_insertar.IsChecked == false || rb_no_insertar.IsChecked == false)
                {
                    MessageBox.Show("No se puede agregar\nHacen falta campos por rellenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (rb_si_insertar.IsChecked == true)
                        inactivo = 1;
                    else
                        inactivo = 0;
                    
                    clt.v_NombreCompleto = txb_ingresar_nombre.Text;
                    clt.v_Teleoficina = Convert.ToInt32(txb_ingresar_TelOf.Text);
                    clt.v_Telemovil = Convert.ToInt32(txb_ingresar_TelMov.Text);
                    clt.v_Correo = txb_ingresar_correo.Text;

                    if (txb_ingresar_correo_o.Text == "")
                        clt.v_CorreoOpc = "N/A";
                    else
                        clt.v_CorreoOpc = txb_ingresar_correo_o.Text;

                    clt.v_Inactividad = inactivo;

                    if (txb_observaciones.Text == "")
                        clt.v_Observaciones = "N/A";
                    else
                        clt.v_Observaciones = txb_observaciones.Text;

                    int v_Resultado = model.AgregarClientes(clt);
                if (v_Resultado == -1)
                {
                    MessageBox.Show("Datos ingresados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    Limpiar_Ingresar_Cliente();
                }
             }
            }
            catch (Exception m)
            {
                Console.WriteLine(m.ToString());
                MessageBox.Show("Error al agregar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_limpiar_cliente_Click(object sender, RoutedEventArgs e)
        {
            Limpiar_Ingresar_Cliente();
        }

        private void btn_buscar_cliente_Click(object sender, RoutedEventArgs e)
        {
            string v_Nombre;
            v_Nombre = txb_buscar_cliente.Text;
            model.BuscarClientes(v_Nombre);
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
            dtg_listar_clientes.ItemsSource = model.MostrarListaClientes().DefaultView;
            dtg_listar_clientes.Columns[0].Header = "Código";
            dtg_listar_clientes.Columns[1].Header = "Fecha Ingreso";
            dtg_listar_clientes.Columns[2].Header = "Nombre";
            dtg_listar_clientes.Columns[3].Header = "Tel. Oficina";
            dtg_listar_clientes.Columns[4].Header = "Tel. Móvil";
            dtg_listar_clientes.Columns[5].Header = "Correo";
            dtg_listar_clientes.Columns[6].Header = "Correo Opcional";
            dtg_listar_clientes.Columns[7].Header = "Inactividad";
            dtg_listar_clientes.Columns[8].Header = "Observaciones";


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

        private void validar_Correo(object sender, EventArgs e)
        {

            Console.WriteLine("Correo: " + txb_ingresar_correo.Text);

            Console.WriteLine(email_correcto(txb_ingresar_correo.Text));

            if (email_correcto(txb_ingresar_correo.Text) == false)
            {
                MessageBox.Show("Error en el formato del correo\n" + "Formato correcto: usuario@dominio.extension", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txb_ingresar_correo.BorderBrush = Brushes.Red;
                txb_ingresar_correo.Background = Brushes.Tomato;
            }
            else
            {
                txb_ingresar_correo.BorderBrush = Brushes.White;
                txb_ingresar_correo.Background = Brushes.White;
            }
        }

        private void validar_Actualizar_Correo(object sender, EventArgs e)
        {

            Console.WriteLine("Correo: " + txb_actualizar_correo.Text);

            Console.WriteLine(email_correcto(txb_actualizar_correo.Text));

            if (email_correcto(txb_actualizar_correo.Text) == false)
            {
                MessageBox.Show("Error en el formato del correo\n" + "Formato correcto: usuario@dominio.extension", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txb_actualizar_correo.BorderBrush = Brushes.Red;
                txb_actualizar_correo.Background = Brushes.Tomato;
            }
            else
            {
                txb_actualizar_correo.BorderBrush = Brushes.White;
                txb_actualizar_correo.Background = Brushes.White;
            }



        }


        private void txb_cliente_modificar_KeyUp(object sender, KeyEventArgs e)
        {
            dtg_actualizar_clientes.ItemsSource = model.BuscarClientes(txb_cliente_modificar.Text);
            dtg_actualizar_clientes.Columns[0].Header = "Código";
            dtg_actualizar_clientes.Columns[1].Header = "Nombre";
            dtg_actualizar_clientes.Columns[2].Header = "Correo";
            dtg_actualizar_clientes.Columns[3].Header = "Correo Opcional";
            dtg_actualizar_clientes.Columns[4].Header = "Tel. Oficina";
            dtg_actualizar_clientes.Columns[5].Header = "Tel. Móvil";
            dtg_actualizar_clientes.Columns[6].Header = "Inactividad";
            dtg_actualizar_clientes.Columns[7].Header = "Observaciones";
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;

            clt.v_Codigo = Convert.ToInt32((dtg_actualizar_clientes.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text);
         
            txb_actualizar_nombre.Text = (dtg_actualizar_clientes.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text;
            txb_actualizar_correo.Text = (dtg_actualizar_clientes.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;
            txb_actualizar_correo_o.Text = (dtg_actualizar_clientes.SelectedCells[3].Column.GetCellContent(row) as TextBlock).Text;
            txb_actualizar_TelOf.Text = (dtg_actualizar_clientes.SelectedCells[4].Column.GetCellContent(row) as TextBlock).Text;
            txb_actualizar_TelMov.Text = (dtg_actualizar_clientes.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text;

            String vInact = (dtg_actualizar_clientes.SelectedCells[6].Column.GetCellContent(row) as TextBlock).Text; ;
            if (vInact=="0")
            {
                rb_no_actualizar.IsChecked = true;
            } else if(vInact=="1")
            {
                rb_si_actualizar.IsChecked = true;
            }


            txb_actualizar_observaciones.Text = (dtg_actualizar_clientes.SelectedCells[7].Column.GetCellContent(row) as TextBlock).Text;


        }

        private void txb_buscar_cliente_KeyUp(object sender, KeyEventArgs e)
        {
            dtg_buscar_cliente.ItemsSource = model.BuscarClientes(txb_buscar_cliente.Text);
            dtg_buscar_cliente.Columns[0].Header = "Código";
            dtg_buscar_cliente.Columns[1].Header = "Nombre";
            dtg_buscar_cliente.Columns[2].Header = "Correo";
            dtg_buscar_cliente.Columns[3].Header = "Correo Opcional";
            dtg_buscar_cliente.Columns[4].Header = "Tel. Oficina";
            dtg_buscar_cliente.Columns[5].Header = "Tel. Móvil";
            dtg_buscar_cliente.Columns[6].Header = "Inactividad";
            dtg_buscar_cliente.Columns[7].Header = "Observaciones";
        }

        private void btn_buscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_historial_Click(object sender, RoutedEventArgs e)
        {

        }


        private void validar_Correo_Opc(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (email_correcto(txb_ingresar_correo_o.Text) == false)
            {
                MessageBox.Show("Error en el formato del correo\n" + "Formato correcto: usuario@dominio.extension", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txb_ingresar_correo_o.BorderBrush = Brushes.Red;
                txb_ingresar_correo_o.Background = Brushes.Tomato;
            }
            else
            {
                txb_ingresar_correo_o.BorderBrush = Brushes.White;
                txb_ingresar_correo_o.Background = Brushes.White;
            }
        }

        private void validar_Correo_Opc_Act(object sender, RoutedEventArgs e)
        {
            if (email_correcto(txb_actualizar_correo_o.Text) == false)
            {
                MessageBox.Show("Error en el formato del correo\n" + "Formato correcto: usuario@dominio.extension", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txb_actualizar_correo_o.BorderBrush = Brushes.Red;
                txb_actualizar_correo_o.Background = Brushes.Tomato;
            }
            else
            {
                txb_actualizar_correo_o.BorderBrush = Brushes.White;
                txb_actualizar_correo_o.Background = Brushes.White;
            }
        }
    }
}