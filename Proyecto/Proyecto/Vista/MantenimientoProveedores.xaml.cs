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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using Entidades;
using Logica;


namespace Proyecto
{
    /// <summary>
    /// Lógica de interacción para MantenimientoProveedores.xaml
    /// </summary>
    public partial class MantenimientoProveedores : Window
    {
        EntidadProveedores clt = new EntidadProveedores();
        Model model = new Model();
        public MantenimientoProveedores()
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
            lbl_fecha.Content = DateTime.Now.ToString();

        }
        private void btn_minimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txb_codProd_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_salir_proveedores__Click(object sender, RoutedEventArgs e)
        {
            Menu v_Ventana = new Menu();
            v_Ventana.Show();
            this.Close();
        }

        private void btn_modificar_Click(object sender, RoutedEventArgs e)
        {
           
                if ((lbl_errorBusqueda.Visibility == Visibility.Visible) || (lbl_errorNombre.Visibility == Visibility.Visible) ||
                    (lbl_errorTelefono.Visibility == Visibility.Visible || (lbl_errorEmail.Visibility == Visibility.Visible)) ||
                    (lbl_errorDesc.Visibility == Visibility.Visible || lbl_errorCedJur.Visibility == Visibility.Visible))
                {
                    MessageBox.Show("Error al agregar\nHacen falta campos por rellenar o errores que corregir", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    try
                    {
                        clt.v_cedulaJuridica = Convert.ToInt64(txb_cedJur.Text);
                        clt.v_nombre = txb_nombre.Text;
                        clt.v_telefono = Convert.ToInt64(txb_telefono.Text);
                        clt.v_correo = txb_email.Text;
                        clt.v_descripcion = txb_descripcion.Text;
                        int v_Resultado = model.ModificarProveedores(clt);

                        if (v_Resultado == -1)
                        {
                            MessageBox.Show("Datos guardados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                            btn_limpiar_Click(sender, e);
                        }

                        //esconder mensajes de error
                        lbl_errorBusqueda.Visibility = Visibility.Collapsed;
                        lbl_errorNombre.Visibility = Visibility.Collapsed;
                        lbl_errorTelefono.Visibility = Visibility.Collapsed;
                        lbl_errorEmail.Visibility = Visibility.Collapsed;
                        lbl_errorDesc.Visibility = Visibility.Collapsed;
                    }
                    catch (Exception m)
                    {
                        Console.WriteLine(m.ToString());
                        MessageBox.Show("Error al agregar\nHacen falta campos por rellenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }    
        }

        private void btn_limpiar_Click(object sender, RoutedEventArgs e)
        {
            txb_cedJur.Text = "";
            txb_telefono.Text = "";
            txb_busqueda.Text = "";
            txb_nombre.Text = "";
            txb_email.Text = "";
            txb_descripcion.Text = "";
            dtg_proveedores.ItemsSource = null;
            lbl_errorCedJur.Visibility = Visibility.Collapsed;
            lbl_errorBusqueda.Visibility = Visibility.Collapsed;
            lbl_errorNombre.Visibility = Visibility.Collapsed;
            lbl_errorTelefono.Visibility = Visibility.Collapsed;
            lbl_errorEmail.Visibility = Visibility.Collapsed;
            lbl_errorDesc.Visibility = Visibility.Collapsed;   
        }

        private void telefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
            { 
                e.Handled = false;
                lbl_errorTelefono.Visibility = Visibility.Collapsed;
            }
            else
            {
                e.Handled = true;
                lbl_errorTelefono.Content = "No se permite ingresar letras";
                lbl_errorTelefono.Visibility = Visibility.Visible;
            }  
        }

        private void cedJur_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
            {
                e.Handled = false;
                lbl_errorCedJur.Visibility = Visibility.Collapsed;
            }
            else
            {
                e.Handled = true;
                lbl_errorCedJur.Content = "No se permite ingresar letras";
                lbl_errorCedJur.Visibility = Visibility.Visible;
            }
        }

        private void validar_telefono(object sender, KeyboardFocusChangedEventArgs e)
        {
            string v_Telefono = txb_telefono.Text;
            if (txb_telefono.Text == "")
            {
                lbl_errorTelefono.Content = "Espacio vacío";
                lbl_errorTelefono.Visibility = Visibility.Visible;
            }
            else if (txb_telefono.Text.Contains(" "))
            {
                lbl_errorTelefono.Content = "No se permiten espacios";
                lbl_errorTelefono.Visibility = Visibility.Visible;
                txb_telefono.Text = "";
            }
            else if(v_Telefono.Length < 8)
            {
                lbl_errorTelefono.Content = "Los números telefónicos deben tener al menos 8 dígitos";
                lbl_errorTelefono.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_errorTelefono.Visibility = Visibility.Collapsed;
            }
        }
        private void validar_cedulaJuridica(object sender, KeyboardFocusChangedEventArgs e)
        {
            string v_CedJur = txb_cedJur.Text;
            if (txb_cedJur.Text == "")
            {
                lbl_errorCedJur.Content = "Espacio vacío";
                lbl_errorCedJur.Visibility = Visibility.Visible;
            }
            else if (txb_cedJur.Text.Contains(" "))
            {
                lbl_errorCedJur.Content = "No se permiten espacios";
                lbl_errorCedJur.Visibility = Visibility.Visible;
                txb_cedJur.Text = "";
            }
            else if (v_CedJur.Length < 9)
            {
                lbl_errorCedJur.Content = "La cédula jurídica debe tener al menos 9 dígitos";
                lbl_errorCedJur.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_errorCedJur.Visibility = Visibility.Collapsed;
            }
        }

        private Boolean email_bien(String v_Email)
        {
            String v_Expresion;
            v_Expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(v_Email, v_Expresion))
            {
                if (Regex.Replace(v_Email, v_Expresion, String.Empty).Length == 0)
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

        private void validar_email(object sender, EventArgs e)
        {
            Console.WriteLine("Correo: " + txb_email.Text);
            Console.WriteLine(email_bien(txb_email.Text));

            if (txb_email.Text == "")
            {
                lbl_errorEmail.Content = "Espacio vacío";
                lbl_errorEmail.Visibility = Visibility.Visible;
            }
            else if (email_bien(txb_email.Text) == false)
            {
                lbl_errorEmail.Content = "Error de formato (usuario@dominio.extension)";
                lbl_errorEmail.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_errorEmail.Visibility = Visibility.Collapsed;
            }
        }

        private void btn_ayuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Secciones Mantenimiento de Proveedores:\n" +
                           "Listar: usted podra observar la lista completa de proveedores.\n" +
                           "Actualizar: esta sección le permite modificar datos de un proveedor en especifico.\n" +
                           "Ingresar: esta sección le permite ingresar proveedores al sistema.\n" +
                           "Deshabilitar: esta sección le permite deshabilitar proveedores.\n" +
                           "Historial: esta ventana le mostrara todos los cambios realizados en esta sección.\n" +
                           
                           "Actualizar Proveedores: \n" +
                             "1 - Si desea actualiar un proveedor primero debe buscarlo por nombre o por la cedula juridica de la empresa.\n" +
                             "2 - Una ves que lo encontró lo puede seleccionar, luego edita los datos que desee y guarda los cambios.\n\n" +

                             "Ingresar Proveedores\n" +

                             "1 - Debera completar cada uno de los espacios requeridos para la creación del nuevo proveedor.\n" +
                             "2 - En caso de que cometa algún error el sistema le notificará.\n" +
                             "3 - En caso de que todos los datos esten corectos, proceda crearlo.\n\n" +


                             "Deshabilitar proveedores:\n" +
                             "1 - Busca el proveedor que desea deshabilitar, luego de que lo muestre, lo selecciona \n\n" +
                             "2 - Ingresa el motivo por el que desea deshabilitar el rol \n" +
                             "3 - Guarda los cambios con los datos necesarios llenos\n\n"

                          , "Ayuda");
        }

        private void btn_listar_listar_Click(object sender, RoutedEventArgs e)
        {
            if (date_inicio.SelectedDate != null && date_final.SelectedDate != null)
            {
                String v_Fecha1;
                v_Fecha1 = date_inicio.SelectedDate.Value.Date.ToShortDateString(); 
                String v_Fecha2;
                v_Fecha2 = date_final.SelectedDate.Value.Date.ToShortDateString();
                dtg_lista.ItemsSource = model.mostrarListaProveedores(v_Fecha1, v_Fecha2).DefaultView;
                dtg_lista.Columns[0].Header = "Código";
                dtg_lista.Columns[1].Header = "Fecha";
                dtg_lista.Columns[2].Header = "Cédula Jurídica";
                dtg_lista.Columns[3].Header = "Nombre";
                dtg_lista.Columns[4].Header = "Correo";
                dtg_lista.Columns[5].Header = "Descripción";
                dtg_lista.Columns[6].Header = "Teléfono";

            }
            else
            {
                MessageBox.Show("Seleccione un rango de fechas","Error",MessageBoxButton.OK,MessageBoxImage.Error );
            }
            
        }

        private void txb_busqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txb_busqueda.Text == "")
            {
                lbl_errorBusqueda.Content = "Espacio vacío";
                lbl_errorBusqueda.Visibility = Visibility.Visible;
                txb_cedJur.IsEnabled = false;
                txb_nombre.IsEnabled = false;
                txb_telefono.IsEnabled = false;
                txb_email.IsEnabled = false;
                txb_descripcion.IsEnabled = false;
            }
            else if (txb_busqueda.Text.Contains("  "))
            {
                lbl_errorBusqueda.Content = "Parámetros incorrectos (espacios seguidos).";
                lbl_errorBusqueda.Visibility = Visibility.Visible;
                txb_cedJur.IsEnabled = false;
                txb_nombre.IsEnabled = false;
                txb_telefono.IsEnabled = false;
                txb_email.IsEnabled = false;
                txb_descripcion.IsEnabled = false;                
            }
            else
            {
                txb_cedJur.IsEnabled = true;
                txb_nombre.IsEnabled = true;
                txb_telefono.IsEnabled = true;
                txb_email.IsEnabled = true;
                txb_descripcion.IsEnabled = true;
                lbl_errorBusqueda.Visibility = Visibility.Collapsed;
            }
        }

        private void txb_busqueda_KeyUp(object sender, KeyEventArgs e)
        {
            dtg_proveedores.ItemsSource = model.validar_busqueda_proveedores(txb_busqueda.Text);
            dtg_proveedores.Columns[0].Header = "Código";
            dtg_proveedores.Columns[1].Header = "Cédula jurídica";
            dtg_proveedores.Columns[2].Header = "Nombre";
            dtg_proveedores.Columns[5].Header = "Teléfono";
            dtg_proveedores.Columns[4].Header = "Correo";
            dtg_proveedores.Columns[3].Header = "Descripción";
            dtg_proveedores.Columns[6].Header = "Fecha";

            if (txb_busqueda.Text == "")
            {
                txb_nombre.Text = "";
                deshabilitar_componentes();
                btn_limpiar_Click(sender,e);
                btn_agregar.Visibility = Visibility.Collapsed;
                btn_modificar.Visibility = Visibility.Collapsed;
            }
            else// el usuario no existe
            {
                if (dtg_proveedores.Items.Count == 0)
                {
                    habilitar_componentes();
                    btn_agregar.Visibility = Visibility.Visible;
                    btn_modificar.Visibility = Visibility.Collapsed;

                    if (Regex.IsMatch(this.txb_busqueda.Text, @"[\p{L}\s]"))
                    {
                        txb_nombre.Text = txb_busqueda.Text;    
                    }
                    else
                    {    
                        txb_cedJur.Text = txb_busqueda.Text;
                        txb_busqueda.MaxLength = 12;
                    }
                }
                else
                {
                    btn_agregar.Visibility = Visibility.Collapsed;
                    btn_modificar.Visibility = Visibility.Visible;
                    deshabilitar_componentes();
                }
            }
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;

            clt.v_idProveedor = Convert.ToInt64((dtg_proveedores.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text);

            txb_cedJur.Text = (dtg_proveedores.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text;
            txb_nombre.Text = (dtg_proveedores.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;
            txb_telefono.Text = (dtg_proveedores.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text;
            txb_email.Text = (dtg_proveedores.SelectedCells[3].Column.GetCellContent(row) as TextBlock).Text;
            txb_descripcion.Text = (dtg_proveedores.SelectedCells[4].Column.GetCellContent(row) as TextBlock).Text;
            habilitar_componentes();
        }

        public void deshabilitar_componentes()
        {
            txb_cedJur.IsEnabled = false;
            txb_nombre.IsEnabled = false;
            txb_telefono.IsEnabled = false;
            txb_email.IsEnabled = false;
            txb_descripcion.IsEnabled = false;
        }

        public void habilitar_componentes()
        {
            txb_cedJur.IsEnabled = true;
            txb_nombre.IsEnabled = true;
            txb_telefono.IsEnabled = true;
            txb_email.IsEnabled = true;
            txb_descripcion.IsEnabled = true;
        }

        private void txb_cedJur_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txb_cedJur.Text == "")
            {
                lbl_errorCedJur.Content = "Espacio vacío";
                lbl_errorCedJur.Visibility = Visibility.Visible;
            }
            else if (txb_cedJur.Text.Contains(" "))
            {
                lbl_errorCedJur.Content = "No se permite el ingreso de espacios";
                lbl_errorCedJur.Visibility = Visibility.Visible;

            }
            else
            {
                lbl_errorCedJur.Visibility = Visibility.Collapsed;
            }
        }

        private void txb_nombre_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (txb_nombre.Text == "")
            {
                lbl_errorNombre.Content = "Espacio vacío";
                lbl_errorNombre.Visibility = Visibility.Visible;
            }
            else if (txb_nombre.Text == " ")
            {
                lbl_errorNombre.Content = "No deje espacios";
                lbl_errorNombre.Visibility = Visibility.Visible;
            }
            else if (txb_nombre.Text.Contains("  "))
            {
                lbl_errorNombre.Content = "Parámetros incorrectos (espacios seguidos).";
                lbl_errorNombre.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_errorNombre.Visibility = Visibility.Collapsed;
            }
        }

        private void txb_telefono_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txb_telefono.Text == "")
            {
                lbl_errorTelefono.Content = "Espacio vacío";
                lbl_errorTelefono.Visibility = Visibility.Visible;
            }
            else if(txb_telefono.Text.Contains(" "))
            {
                lbl_errorTelefono.Content = "No se permite el ingreso de espacios";
                lbl_errorTelefono.Visibility = Visibility.Visible;

            }
            else
            {
                lbl_errorTelefono.Visibility = Visibility.Collapsed;
            }
        }

        private void textbox_ingresar_email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txb_email.Text == "")
            {
                lbl_errorEmail.Content = "Espacio vacío";
                lbl_errorEmail.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_errorEmail.Visibility = Visibility.Collapsed;
            }
        }

        private void txb_descripcion_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txb_descripcion.Text == "")
            {
                lbl_errorDesc.Content = "Espacio vacío";
                lbl_errorDesc.Visibility = Visibility.Visible;
            }
            else if (txb_descripcion.Text == " ")
            {
                lbl_errorDesc.Content = "No deje espacios";
                lbl_errorDesc.Visibility = Visibility.Visible;
            }
            else if (txb_descripcion.Text.Contains("  "))
            {
                lbl_errorDesc.Content = "Parámetros incorrectos (espacios seguidos).";
                lbl_errorDesc.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_errorDesc.Visibility = Visibility.Collapsed;
            }
        }

        private void btn_guardar_actualizar_proveedores_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txb_cedJur.Text == "" || txb_email.Text == "" || txb_nombre.Text == "" || txb_telefono.Text == "" || txb_busqueda.Text == "" || lbl_errorCedJur.Visibility == Visibility.Visible || lbl_errorNombre.Visibility == Visibility.Visible || lbl_errorTelefono.Visibility == Visibility.Visible || lbl_errorEmail.Visibility == Visibility.Visible || lbl_errorDesc.Visibility == Visibility.Visible)
                {
                    MessageBox.Show("No se puede agregar\nHacen falta campos por rellenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    clt.v_cedulaJuridica = Convert.ToInt64(txb_cedJur.Text);
                    clt.v_nombre = txb_nombre.Text;
                    clt.v_telefono = Convert.ToInt64(txb_telefono.Text);
                    clt.v_correo = txb_email.Text;
                    clt.v_descripcion = txb_descripcion.Text;

                    int v_Resultado = model.AgregarProveedores(clt);
                    if (v_Resultado == -1)
                    {
                        MessageBox.Show("Datos ingresados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        btn_limpiar_Click(sender,e);
                    }
                }
            }
            catch (Exception m)
            {
                Console.WriteLine(m.ToString());
                MessageBox.Show("Error al agregar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dtg_proveedores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
