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
        EntidadClientes clt = new EntidadClientes();
        Model model = new Model();
        public Clientes()
        {
            InitializeComponent();
            txt_fecha.Content = DateTime.Now.ToShortDateString();
            txt_hora.Content = DateTime.Now.ToShortTimeString();
        }

        private void btn_Ayuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sección Mantenimiento de Clientes:\n\n" + "Listar: usted podra imprimir la lista de clientes, aparte de ordenarlos ya sea por código o por nombre.\n" +
                             "Actualizar: aca usted podra actualizar la informacion del cliente que desee.\n" +
                             "Ingresar: esta sección le permite la creación de un nuevo cliente.\n" +
                             "Buscar: aca usted podra buscar los clientes que desee.\n" +
                             "Historial: esta ventana le mostrara todos los cambios realizados en esta sección.\n\n" +
                             "Actualizar Clientes\n" +
                             "1 - Si desea actualiar un cliente primero debe buscarlo por nombre o por el código.\n" +
                             "2 - Una ves que lo encontró lo puede editar y guardar los cambios.\n\n" +

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


        private void btn_Regresar_Click_1(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.Show();
            this.Close();
        }

        private void btn_Lista_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_guardarIng(object sender, RoutedEventArgs e)
        {

        }

        private void btn_limpiarIng(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {

        }

        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.Show();
            this.Close();
        }


        private void btn_historial_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_listar_clientes_nombre_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_usuario_clientes_Click(object sender, RoutedEventArgs e)
        {
            Login ventana = new Login();
            ventana.Show();
            this.Close();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          //  _en = dtg_listar_clientes.SelectedItem as Ventana; 
        }

        private void textbox_rol_modificar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_editar_cliente_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("No se ha seleccionado ningun cliente.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void dtg_actualizar_clientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_buscar_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btn_guardar_cliente_actualizado_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_actualizar_correo.Text == "" || textbox_actualizar_nombre.Text == "" || textbox_actualizar_TelMov.Text == "" || textbox_actualizar_TelOf.Text == "")
            {
                MessageBox.Show("No se puede agregar\nHacen falta campos por rellenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Datos guardados", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btn_limpiar_actualizar_cliente_Click(object sender, RoutedEventArgs e)
        {
            textbox_actualizar_correo.Text = "";
            textbox_actualizar_correo_o.Text = "";
            textbox_actualizar_nombre.Text = "";
            textbox_actualizar_observaciones.Text = "";
            textbox_actualizar_TelMov.Text = "";
            textbox_actualizar_TelOf.Text = "";
        }

        private void Button_guardar_cliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /*if (textbox_actualizar_correo.Text == "" || textbox_actualizar_nombre.Text == "" || textbox_actualizar_TelMov.Text == "" || textbox_actualizar_TelOf.Text == "")
                {
                    MessageBox.Show("No se puede agregar\nHacen falta campos por rellenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {*/
                    clt.v_NombreCompleto = textbox_ingresar_nombre.Text;
                    clt.v_Teleoficina = Convert.ToInt32(textbox_TelOf.Text);
                    clt.v_Telemovil = Convert.ToInt32(textbox_TelMov.Text);
                    clt.v_Correo = textbox_ingresar_correo.Text;
                    clt.v_CorreoOpc = textbox_ingresar_correo_o.Text;     
                    clt.v_Observaciones = textbox_observaciones.Text;
                    int v_Resultado = model.AgregarClientes(clt);
                    if (v_Resultado == -1)
                    {
                        MessageBox.Show("Datos guardados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                //}
            }
            catch(Exception m)
            {
                Console.WriteLine(m.ToString());
                MessageBox.Show("Error al agregar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_limpiar_cliente_Click(object sender, RoutedEventArgs e)
        {
            textbox_ingresar_correo.Text = "";
            textbox_ingresar_correo_o.Text = "";
            textbox_ingresar_nombre.Text = "";
            textbox_observaciones.Text = "";
            textbox_TelMov.Text = "";
            textbox_TelOf.Text = "";
        }

        private void textbox_ingresar_nombre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textbox_ingresar_correo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textbox_ingresar_correo_o_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textbox_ingresar_telOf_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textbox_ingresar_telMov_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RadioButton_no_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void textbox_observaciones_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_buscar_cliente_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

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

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_imprimir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_ordenarporcodigo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_ordenarpornombre_Click(object sender, RoutedEventArgs e)
        {

        }

        private void textbox_cliente_modificar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textbox_actualizar_nombre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textbox_actualizar_correo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textbox_actualizar_correo_o_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textbox_actualizar_TelOf_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textbox_actualizar_TelMov_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textbox_actualizar_observaciones_TextChanged(object sender, TextChangedEventArgs e)
        {

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

            Console.WriteLine("Correo: " + textbox_ingresar_correo.Text);

            Console.WriteLine(email_correcto(textbox_ingresar_correo.Text));

            if (email_correcto(textbox_ingresar_correo.Text) == false)
            {
                MessageBox.Show("Error en el formato del correo\n" + "Formato correcto: usuario@dominio.extension", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textbox_ingresar_correo.BorderBrush = Brushes.Red;
                textbox_ingresar_correo.Background = Brushes.Tomato;
            }
            else
            {
                textbox_ingresar_correo.BorderBrush = Brushes.White;
                textbox_ingresar_correo.Background = Brushes.White;
            }
        }

        private void validar_Actualizar_Correo(object sender, EventArgs e)
        {

            Console.WriteLine("Correo: " + textbox_actualizar_correo.Text);

            Console.WriteLine(email_correcto(textbox_actualizar_correo.Text));

            if (email_correcto(textbox_actualizar_correo.Text) == false)
            {
                MessageBox.Show("Error en el formato del correo\n" + "Formato correcto: usuario@dominio.extension", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textbox_actualizar_correo.BorderBrush = Brushes.Red;
                textbox_actualizar_correo.Background = Brushes.Tomato;
            }
            else
            {
                textbox_actualizar_correo.BorderBrush = Brushes.White;
                textbox_actualizar_correo.Background = Brushes.White;
            }



        }


    }
}