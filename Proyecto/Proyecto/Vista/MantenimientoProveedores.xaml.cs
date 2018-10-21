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

        private void btn_buscar_proveedor_Click(object sender, RoutedEventArgs e)
        {
            if (txb_buscar_proveedores.Text == "")
            {
                MessageBox.Show("No hay datos que buscar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Buscando...");
            }
        }

        private void btn_ingresar_guardar_Click(object sender, RoutedEventArgs e)
        {
           
                if ((lbl_ingresar_errorCedJur.Visibility == Visibility.Visible) || (lbl_ingresar_errorNombre.Visibility == Visibility.Visible) ||
                    (lbl_ingresar_errorTelefono.Visibility == Visibility.Visible || (lbl_ingresar_errorEmail.Visibility == Visibility.Visible)) ||
                    (lbl_ingresar_errorDesc.Visibility == Visibility.Visible))
                {
                    MessageBox.Show("Error al agregar\nHacen falta campos por rellenar o errores que corregir", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    try
                    {
                        clt.v_cedulaJuridica = Convert.ToInt64(txb_ingresar_cedJuridica.Text);
                        clt.v_nombre = txb_ingresar_nombre.Text;
                        clt.v_telefono = Convert.ToInt64(txb_ingresar_telefono.Text);
                        clt.v_correo = txb_ingresar_email.Text;
                        clt.v_descripccion = txb_ingresar_descripcion.Text;
                        int v_Resultado = model.AgregarProveedores(clt);

                        if (v_Resultado == -1)
                        {
                            MessageBox.Show("Datos guardados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                            btn_ingresar_limpiar_Click(sender, e);
                        }

                        //esconder mensajes de error
                        lbl_ingresar_errorCedJur.Visibility = Visibility.Collapsed;
                        lbl_ingresar_errorNombre.Visibility = Visibility.Collapsed;
                        lbl_ingresar_errorTelefono.Visibility = Visibility.Collapsed;
                        lbl_ingresar_errorEmail.Visibility = Visibility.Collapsed;
                        lbl_ingresar_errorDesc.Visibility = Visibility.Collapsed;
                    }
                    catch (Exception m)
                    {
                        Console.WriteLine(m.ToString());
                        MessageBox.Show("Error al agregar\nHacen falta campos por rellenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }    
        }

        private void btn_ingresar_limpiar_Click(object sender, RoutedEventArgs e)
        {
            txb_ingresar_telefono.Text = "";
            txb_ingresar_cedJuridica.Text = "";
            txb_ingresar_nombre.Text = "";
            txb_ingresar_email.Text = "";
            txb_ingresar_descripcion.Text = "";
            txb_ingresar_email.Background = Brushes.White;
            txb_ingresar_email.BorderBrush = Brushes.White;
            lbl_ingresar_errorCedJur.Visibility = Visibility.Collapsed;
            lbl_ingresar_errorNombre.Visibility = Visibility.Collapsed;
            lbl_ingresar_errorTelefono.Visibility = Visibility.Collapsed;
            lbl_ingresar_errorEmail.Visibility = Visibility.Collapsed;
            lbl_ingresar_errorDesc.Visibility = Visibility.Collapsed;
            
        }

        private void telefono_ingresar_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
            { 
                e.Handled = false;
                lbl_ingresar_errorTelefono.Visibility = Visibility.Collapsed;
            }
            else
            {
                e.Handled = true;
                lbl_ingresar_errorTelefono.Content = "No se permite ingresar letras";
                lbl_ingresar_errorTelefono.Visibility = Visibility.Visible;
            }  
        }

        private void cedJuridica_ingresar_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
            {
                e.Handled = false;
                lbl_ingresar_errorCedJur.Visibility = Visibility.Collapsed;
            }
            else
            {
                e.Handled = true;
                lbl_ingresar_errorCedJur.Content = "No se permite ingresar letras";
                lbl_ingresar_errorCedJur.Visibility = Visibility.Visible;
            }
        }
        private void validar_telefono(object sender, KeyboardFocusChangedEventArgs e)
        {
            string v_Telefono = txb_ingresar_telefono.Text;
            if (txb_ingresar_telefono.Text == "")
            {
                lbl_ingresar_errorTelefono.Content = "Espacio vacío";
                lbl_ingresar_errorTelefono.Visibility = Visibility.Visible;
            } else if(v_Telefono.Length < 8)
            {
                lbl_ingresar_errorTelefono.Content = "Los números telefónicos deben tener al menos 8 dígitos";
                lbl_ingresar_errorTelefono.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_ingresar_errorTelefono.Visibility = Visibility.Collapsed;
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
            Console.WriteLine("Correo: " + txb_ingresar_email.Text);
            Console.WriteLine(email_bien(txb_ingresar_email.Text));

            if (txb_ingresar_email.Text == "")
            {
                lbl_ingresar_errorEmail.Content = "Espacio vacío";
                lbl_ingresar_errorEmail.Visibility = Visibility.Visible;
            }
            else if (email_bien(txb_ingresar_email.Text) == false)
            {
                lbl_ingresar_errorEmail.Content = "Error de formato (usuario@dominio.extension)";
                lbl_ingresar_errorEmail.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_ingresar_errorEmail.Visibility = Visibility.Collapsed;
            }
        }

        private void btn_buscar_deshabilitar_proveedores_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_cedJuridica.Text == "")
            {
                MessageBox.Show("No hay datos que buscar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Buscando...");
            }
        }

        private void btn_limpiar_deshabilitar_Click(object sender, RoutedEventArgs e)
        {
            textbox_motivo_proveedores.Text = " ";
            
        }

        private void btn_buscar_actualizar_proveedores_Click(object sender, RoutedEventArgs e)
        {
            if (txb_codProd.Text == "")
            {
                MessageBox.Show("No hay datos que buscar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Buscando...");
            }
        }

        private void btn_guardar_deshabilitar_proveedores_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_motivo_proveedores.Text == "")
            {
                MessageBox.Show("Faltan elementos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                MessageBox.Show("Datos Ingresados", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btn_guardar_actualizar_proveedores_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_nombre_actualizar.Text == "" && textbox_telefono_proveedores.Text == "" && txb_ingresar_email.Text == "" && textbox_descripcion_actualizar.Text == "")
            {
                MessageBox.Show("Faltan elementos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                MessageBox.Show("Datos Ingresados", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void validar_email_proveedores(object sender, EventArgs e)
        {

            Console.WriteLine("Correo: " + txb_ingresar_email.Text);

            Console.WriteLine(email_bien(txb_ingresar_email.Text));

            if (email_bien(txb_ingresar_email.Text) == false)
            {
                MessageBox.Show("Error en el formato del correo\n" + "Formato correcto: usuario@dominio.extension", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txb_ingresar_email.BorderBrush = Brushes.Red;
                txb_ingresar_email.Background = Brushes.Tomato;
            }
            else
            {
                txb_ingresar_email.BorderBrush = Brushes.White;
                txb_ingresar_email.Background = Brushes.White;
            }
        }


        private void btn_limpiar_actualizar_Click(object sender, RoutedEventArgs e)
        {
            textbox_nombre_actualizar.Text = " ";
            textbox_telefono_proveedores.Text = " ";
            txb_ingresar_email.Text = " ";
            textbox_descripcion_actualizar.Text = " ";
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
            if (date_listar_inicio.SelectedDate != null && date_listar_final.SelectedDate != null)
            {
                String v_Fecha1;
                v_Fecha1 = date_listar_inicio.SelectedDate.Value.Date.ToShortDateString(); 
                String v_Fecha2;
                v_Fecha2 = date_listar_final.SelectedDate.Value.Date.ToShortDateString();
                dtg_listar_Proveedores.ItemsSource = model.mostrarListaProveedores(v_Fecha1, v_Fecha2).DefaultView;
                dtg_listar_Proveedores.Columns[0].Header = "Código";
                dtg_listar_Proveedores.Columns[1].Header = "Fecha";
                dtg_listar_Proveedores.Columns[2].Header = "Cédula Jurídica";
                dtg_listar_Proveedores.Columns[3].Header = "Nombre";
                dtg_listar_Proveedores.Columns[4].Header = "Correo";
                dtg_listar_Proveedores.Columns[5].Header = "Descripción";
                dtg_listar_Proveedores.Columns[6].Header = "Teléfono";

            }
            else
            {
                MessageBox.Show("Seleccione un rango de fechas","Error",MessageBoxButton.OK,MessageBoxImage.Error );
            }
            
        }

        private void txb_ingresar_cedJuridica_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txb_ingresar_cedJuridica.Text == "")
            {
                lbl_ingresar_errorCedJur.Content = "Espacio vacío";
                lbl_ingresar_errorCedJur.Visibility = Visibility.Visible;
                txb_ingresar_nombre.IsEnabled = false;
                txb_ingresar_telefono.IsEnabled = false;
                txb_ingresar_email.IsEnabled = false;
                txb_ingresar_descripcion.IsEnabled = false;
            }
            else if (txb_ingresar_cedJuridica.Text.Contains(" "))
            {
                lbl_ingresar_errorCedJur.Content = "No se permite el ingreso de espacios";
                lbl_ingresar_errorCedJur.Visibility = Visibility.Visible;
                txb_ingresar_nombre.IsEnabled = false;
                txb_ingresar_telefono.IsEnabled = false;
                txb_ingresar_email.IsEnabled = false;
                txb_ingresar_descripcion.IsEnabled = false;                
            }
            else
            {
                txb_ingresar_nombre.IsEnabled = true;
                txb_ingresar_telefono.IsEnabled = true;
                txb_ingresar_email.IsEnabled = true;
                txb_ingresar_descripcion.IsEnabled = true;
                lbl_ingresar_errorCedJur.Visibility = Visibility.Collapsed;
            }
        }

        private void txb_ingresar_nombre_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (txb_ingresar_nombre.Text == "")
            {
                lbl_ingresar_errorNombre.Content = "Espacio vacío";
                lbl_ingresar_errorNombre.Visibility = Visibility.Visible;
            }
            else if (txb_ingresar_nombre.Text.Contains("  "))
            {
                lbl_ingresar_errorNombre.Content = "Parámetros incorrectos (espacios seguidos).";
                lbl_ingresar_errorNombre.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_ingresar_errorNombre.Visibility = Visibility.Collapsed;
            }
        }

        private void txb_ingresar_telefono_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txb_ingresar_telefono.Text == "")
            {
                lbl_ingresar_errorTelefono.Content = "Espacio vacío";
                lbl_ingresar_errorTelefono.Visibility = Visibility.Visible;
            }
            else if(txb_ingresar_telefono.Text.Contains(" "))
            {
                lbl_ingresar_errorTelefono.Content = "No se permite el ingreso de espacios";
                lbl_ingresar_errorTelefono.Visibility = Visibility.Visible;

            }
            else
            {
                lbl_ingresar_errorTelefono.Visibility = Visibility.Collapsed;
            }
        }

        private void textbox_ingresar_email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txb_ingresar_email.Text == "")
            {
                lbl_ingresar_errorEmail.Content = "Espacio vacío";
                lbl_ingresar_errorEmail.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_ingresar_errorEmail.Visibility = Visibility.Collapsed;
            }
        }

        private void txb_ingresar_descripcion_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txb_ingresar_descripcion.Text == "")
            {
                lbl_ingresar_errorDesc.Content = "Espacio vacío";
                lbl_ingresar_errorDesc.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_ingresar_errorDesc.Visibility = Visibility.Collapsed;
            }
        }


        private void validar_cedJur(object sender, EventArgs e)
        {
            string v_CedJuridica = txb_ingresar_cedJuridica.Text;
            if(v_CedJuridica.Length < 9)
            {
                lbl_ingresar_errorCedJur.Content = "La cédula jurídica debe tener al menos 9 dígitos";
                lbl_ingresar_errorCedJur.Visibility = Visibility.Visible;
            }
            else if (txb_ingresar_cedJuridica.Text != "")
            {
                lbl_ingresar_errorCedJur.Visibility = Visibility.Collapsed;
                long v_CedJur = Convert.ToInt64(txb_ingresar_cedJuridica.Text);
                long v_Resultado = model.validar_cedJur_proveedores(v_CedJur);
                if (v_Resultado == 1)
                {
                    lbl_ingresar_errorCedJur.Content = "La cédula jurídica ya existe.";
                    lbl_ingresar_errorCedJur.Visibility = Visibility.Visible;
                    txb_ingresar_nombre.IsEnabled = false;
                    txb_ingresar_telefono.IsEnabled = false;
                    txb_ingresar_email.IsEnabled = false;
                    txb_ingresar_descripcion.IsEnabled = false;
                }
            }
        }

    }
}
