using System;
using System.Collections.Generic;
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



namespace Proyecto
{
    /// <summary>
    /// Lógica de interacción para Usuarios.xaml
    /// </summary>
    public partial class MantenimientoUsuarios : Window
    {
        public MantenimientoUsuarios()
        {
            InitializeComponent();
            lbl_fecha.Content = DateTime.Now.ToShortDateString();
            lbl_hora.Content = DateTime.Now.ToShortTimeString();
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

        private void btn_salir_usuarios__Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txb_buscar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void txb_usuario_ingresar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_ingresar_limpiar_Click(object sender, RoutedEventArgs e)
        {
            //limpiar
            textbox_ingresar_numCed.Text = "";
            textbox_ingresar_nombre.Text = "";
            textbox_ingresar_email.Text = "";
            textbox_ingresar_telHabitacion.Text = "";
            textbox_ingresar_telMovil.Text = "";
            textbox_ingresar_usuario.Text = "";
            textbox_ingresar_contrasenna.Text = "";
            textbox_ingresar_puesto.Text = "";
            cmb_ingresar_rol.Text = "";
            textbox_ingresar_email.BorderBrush = Brushes.White;
            textbox_ingresar_email.Background = Brushes.White;
        }

        private void btn_actualizar_limpiar_Click(object sender, RoutedEventArgs e)
        {
            textbox_actualizar_numCed.Text = "";
            textbox_actualizar_nombre.Text = "";
            textbox_actualizar_email.Text = "";
            textbox_actualizar_telHabitacion.Text = "";
            textbox_actualizar_telMovil.Text = "";
            textbox_actualizar_usuario.Text = "";
            textbox_actualizar_contrasenna.Text = "";
            textbox_actualizar_puesto.Text = "";
            cmb_actualizar_rol.Text = "";
            textbox_actualizar_email.BorderBrush = Brushes.White;
            textbox_actualizar_email.Background = Brushes.White;
        }

        private void btn_deshabilitar_limpiar_Click(object sender, RoutedEventArgs e)
        {
            textbox_deshabilitar_motivo.Text = "";
            textbox_deshabilitar_numCed.Text = "";
        }

        private void Button_ingresar_guardar(object sender, RoutedEventArgs e)
        {
            if (textbox_ingresar_numCed.Text == "" && textbox_ingresar_nombre.Text == "" && textbox_ingresar_email.Text == ""
                && textbox_ingresar_telHabitacion.Text == "" && textbox_ingresar_telMovil.Text == "" && textbox_ingresar_puesto.Text == ""
                && textbox_ingresar_usuario.Text == "" && textbox_ingresar_contrasenna.Text == "" && cmb_ingresar_rol.Text == "")
            {
                MessageBox.Show("Ingrese elementos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (textbox_ingresar_numCed.Text == "" || textbox_ingresar_nombre.Text == "" || textbox_ingresar_email.Text == ""
                || textbox_ingresar_telHabitacion.Text == "" || textbox_ingresar_telMovil.Text == "" || textbox_ingresar_puesto.Text == ""
                || textbox_ingresar_usuario.Text == "" || textbox_ingresar_contrasenna.Text == "" || cmb_ingresar_rol.Text == "")
            {
                MessageBox.Show("Faltan elementos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                MessageBox.Show("Datos Ingresados", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_deshabilitar_guardar(object sender, RoutedEventArgs e)
        {
            if (textbox_deshabilitar_numCed.Text == "" && textbox_deshabilitar_motivo.Text == "")
            {
                MessageBox.Show("Ingrese elementos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (textbox_deshabilitar_numCed.Text == "" || textbox_deshabilitar_motivo.Text == "")
            {
                MessageBox.Show("Faltan elementos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                MessageBox.Show("Datos Ingresados", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                btn_deshabilitar_limpiar_Click(sender, e);
            }
        }

        private void Button_actualizar_guardar_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_actualizar_numCed.Text == "" && textbox_actualizar_nombre.Text == "" && textbox_actualizar_email.Text == ""
                && textbox_actualizar_telHabitacion.Text == "" && textbox_actualizar_telMovil.Text == "" && textbox_actualizar_puesto.Text == ""
                && textbox_actualizar_usuario.Text == "" && textbox_actualizar_contrasenna.Text == "" && cmb_actualizar_rol.Text == "")
            {
                MessageBox.Show("Ingrese elementos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (textbox_actualizar_numCed.Text == "" || textbox_actualizar_nombre.Text == "" || textbox_actualizar_email.Text == ""
                || textbox_actualizar_telHabitacion.Text == "" || textbox_actualizar_telMovil.Text == "" || textbox_actualizar_puesto.Text == ""
                || textbox_actualizar_usuario.Text == "" || textbox_actualizar_contrasenna.Text == "" || cmb_actualizar_rol.Text == "")
            {
                MessageBox.Show("Faltan elementos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Datos Ingresados", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                btn_actualizar_limpiar_Click(sender, e);
            }
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
            Console.WriteLine("Correo: " + textbox_ingresar_email.Text);

            Console.WriteLine(email_correcto(textbox_ingresar_email.Text));

            if (email_correcto(textbox_ingresar_email.Text) == false)
            {
                MessageBox.Show("Error en el formato del correo\n" + "Formato correcto: usuario@dominio.extension", "Error");
                textbox_ingresar_email.BorderBrush = Brushes.Red;
                textbox_ingresar_email.Background = Brushes.Tomato;
            }
            else
            {
                textbox_ingresar_email.BorderBrush = Brushes.White;
                textbox_ingresar_email.Background = Brushes.White;
            }
        }

        private void validar_Actualizar_Correo(object sender, EventArgs e)
        {
            Console.WriteLine("Correo: " + textbox_actualizar_email.Text);

            Console.WriteLine(email_correcto(textbox_actualizar_email.Text));

            if (email_correcto(textbox_actualizar_email.Text) == false)
            {
                MessageBox.Show("Error en el formato del correo\n" + "Formato correcto: usuario@dominio.extension", "Error");
                textbox_actualizar_email.BorderBrush = Brushes.Red;
                textbox_actualizar_email.Background = Brushes.Tomato;
            }
            else
            {
                textbox_actualizar_email.BorderBrush = Brushes.White;
                textbox_actualizar_email.Background = Brushes.White;
            }
        }

        private void telefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
                e.Handled = false;
            else
            {
                MessageBox.Show("Sólo se permite ingresar números en este espacio.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            }
            
        }

        private void textbox_ingresar_email_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_ayuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Reglas: \n" +
                "1. Listar: Seleccione el botón Listar para desplegar los datos. \n" +
                "2. Buscar: Ingrese el elemento a buscar y seleccione el botón Buscar para ver los resultados. \n" +
                "3. Ingresar: \n" +
                "   Complete todos los campos.\n" +
                "   Las cajas de texto de los teléfonos solo permiten números.\n" +
                "   Formato de correo: usuario@dominio.extension \n" +
                "   Seleccionar el rol que se le desea asignar al usuario. \n" +
                "   Formato de contraseña: Tamaño entre 8 y 16 caracteres alfanuméricos.\n" +
                "4. Deshabilitar: \n" +
                "   Ingrese el elemento a deshabilitar y seleccione el botón Buscar para ver los resultados.\n" +
                "   Seleccione el elemento e ingrese el motivo por el cual se desea deshabilitar\n" +
                "5. Actualizar: \n" +
                "   Ingrese el elemento a actualizar y seleccione el botón Buscar para ver los resultados.\n" +
                "   Seleccione el elemento y proceda a editar el/los campos que desee.\n" +
                "   Se utiliza el mismo formato que ingresar. No deje campos vacíos.\n" +
                "6. Historial: Seleccione el botón Historial para desplegar los datos.", "Ayuda",
                MessageBoxButton.OK);
        }

        private void btn_buscar_buscar_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_buscar_buscar.Text == "")
            {
                MessageBox.Show("Ingrese elemento a buscar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_deshabilitar_buscar_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_deshabilitar_numCed.Text == "")
            {
                MessageBox.Show("Ingrese elemento a buscar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_actualizar_buscar_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_actualizar_numCed.Text == "")
            {
                MessageBox.Show("Ingrese elemento a buscar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void validar_ingresar_contrasenna(object sender, EventArgs e)
        {
            if(textbox_ingresar_contrasenna.Text.Length < 8)
            {
                MessageBox.Show("Formato incorrecto, debe tener más de 8 caraceres.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textbox_ingresar_contrasenna.Text = "";
            }
        }

        private void validar_actualizar_contrasenna(object sender, EventArgs e)
        {
            if (textbox_actualizar_contrasenna.Text.Length < 8)
            {
                MessageBox.Show("Formato incorrecto, debe tener más de 8 caraceres.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textbox_actualizar_contrasenna.Text = "";
            }
        }

        private void textbox_ingresar_contrasenna_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textbox_actualizar_contrasenna_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void textbox_ingresar_numCed_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textbox_actualizar_nombre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dtg_listar_lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_historial_buscar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


