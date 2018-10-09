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

namespace Proyecto
{
    /// <summary>
    /// Lógica de interacción para MantenimientoProveedores.xaml
    /// </summary>
    public partial class MantenimientoProveedores : Window
    {
        public MantenimientoProveedores()
        {
            InitializeComponent();
            txt_fecha.Content = DateTime.Now.ToShortDateString();
            txt_hora.Content = DateTime.Now.ToShortTimeString();
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

        private void txb_nombre_ingresar_TextChanged(object sender, TextChangedEventArgs e)
        {

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
            Menu ventana = new Menu();
            ventana.Show();
            this.Close();
        }

        private void btn_buscar_proveedor_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_buscar_proveedores.Text == "")
            {
                MessageBox.Show("No hay datos que buscar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Buscando...");
            }
        }

        private void btn_guardar_proveedor_Click(object sender, RoutedEventArgs e)
        {
                if (textbox_cedJuridica_ingresar.Text == "" && textbox_nombre_ingresar.Text =="" && textbox_telefono_ingresar.Text == ""&& textbox_email_ingresar.Text == ""&& textbox_descripcion_ingresar.Text == "")
                {
                    MessageBox.Show("Faltan elementos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                {
                    MessageBox.Show("Datos Ingresados", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
        }

        private void btn_limpiar_ingresar_Click(object sender, RoutedEventArgs e)
        {
            textbox_nombre_ingresar.Text = "";
            textbox_cedJuridica_ingresar.Text = "";
            textbox_telefono_ingresar.Text = "";
            textbox_email_ingresar.Text = "";
            textbox_descripcion_ingresar.Text = "";
        }

        private void telefono_ingresar_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textbox_email_ingresar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private Boolean email_bien(String email)
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
        private void validar_email(object sender, EventArgs e)
        {

            Console.WriteLine("Correo: " + textbox_email_ingresar.Text);

            Console.WriteLine(email_bien(textbox_email_ingresar.Text));

            if (email_bien(textbox_email_ingresar.Text) == false)
            {
                MessageBox.Show("Error en el formato del correo\n" + "Formato correcto: usuario@dominio.extension", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textbox_email_ingresar.BorderBrush = Brushes.Red;
                textbox_email_ingresar.Background = Brushes.Tomato;
            }
            else
            {
                textbox_email_ingresar.BorderBrush = Brushes.White;
                textbox_email_ingresar.Background = Brushes.White;
            }
        }

        private void textbox_cedJuridica_TextChanged(object sender, TextChangedEventArgs e)
        {

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
            if (textbox_cedJuridica.Text == "")
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
            if (textbox_nombre_actualizar.Text == "" && textbox_telefono_proveedores.Text == "" && textbox_email_proveedores.Text == "" && textbox_descripcion_actualizar.Text == "")
            {
                MessageBox.Show("Faltan elementos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                MessageBox.Show("Datos Ingresados", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void textbox_telefono_proveedores_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void telefono_proveedores_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void validar_email_proveedores(object sender, EventArgs e)
        {

            Console.WriteLine("Correo: " + textbox_email_proveedores.Text);

            Console.WriteLine(email_bien(textbox_email_proveedores.Text));

            if (email_bien(textbox_email_proveedores.Text) == false)
            {
                MessageBox.Show("Error en el formato del correo\n" + "Formato correcto: usuario@dominio.extension", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textbox_email_proveedores.BorderBrush = Brushes.Red;
                textbox_email_proveedores.Background = Brushes.Tomato;
            }
            else
            {
                textbox_email_proveedores.BorderBrush = Brushes.White;
                textbox_email_proveedores.Background = Brushes.White;
            }
        }


        private void btn_limpiar_actualizar_Click(object sender, RoutedEventArgs e)
        {
            textbox_nombre_actualizar.Text = " ";
            textbox_telefono_proveedores.Text = " ";
            textbox_email_proveedores.Text = " ";
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
    }
}
