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
using System.Windows.Shapes;

namespace Proyecto
{
    /// <summary>
    /// Interaction logic for MantenimientoProductos.xaml
    /// </summary>
    public partial class MantenimientoProductos : Window
    {
        public MantenimientoProductos()
        {
            InitializeComponent();
            txt_fecha.Content = DateTime.Now.ToShortDateString();
            txt_hora.Content = DateTime.Now.ToShortTimeString();
        }

        private void btn_guardarIng(object sender, RoutedEventArgs e)
        {

        }

        private void btn_limpiarIng(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.Show();
            this.Close();
        }

        private void btn_ayuda_Click(object sender, RoutedEventArgs e)
        {

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.Show();
            this.Close();
        }

        private void btn_salir_roles__Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.Show();
            this.Close();
        }

        private void textbox_ingresar_cantIngresar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_ingresar_limpiar_Click(object sender, RoutedEventArgs e)
        {
            textbox_ingresar_codProd.Text = "";
            textbox_ingresar_nombre.Text = "";
            textbox_ingresar_proveedor.Text = "";
            date_ingresar_fechaIngreso.Text = "";
            textbox_ingresar_precio.Text = "";
            textbox_ingresar_cantIngresar.Text = "";
            textbox_ingresar_cantMinima.Text = "";
            textbox_ingresar_fabricante.Text = "";
            cmb_ingresar_estado.Text = "";
            textbox_ingresar_descripcion.Text = "";
        }

        private void btn_deshabilitar_limpiar_Click(object sender, RoutedEventArgs e)
        {
            textbox_deshabilitar_cantDeshabilitar.Text = "";
            textbox_deshabilitar_motivo.Text = "";
        }

        private void btn_actualizar_limpiar_Click(object sender, RoutedEventArgs e)
        {
            textbox_actualizar_nombre.Text = "";
            textbox_actualizar_cantActual.Text = "";
            textbox_actualizar_cantModificar.Text = "";
            textbox_actualizar_proveedor.Text = "";
            cmb_actualizar_estado.Text = "";
            textbox_actualizar_precio.Text = "";
            textbox_actualizar_cantMinima.Text = "";
            date_actualizar_fechaIngreso.Text = "";
            textbox_actualizar_fabricante.Text = "";
            textbox_actualizar_descripcion.Text = "";
        }

        private void btn_buscar_buscar_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_buscar_buscar.Text == "")
            {
                MessageBox.Show("Ingrese elemento a buscar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_deshabilitar_buscar_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_deshabilitar_buscar.Text == "")
            {
                MessageBox.Show("Ingrese elemento a buscar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_productos_ayuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Reglas: \n" +
                "1. Listar: Seleccione el botón 'Listar' para desplegar los datos. \n" +
                "2. Buscar: Ingrese el elemento a buscar y seleccione el botón Buscar para ver los resultados. \n" +
                "3. Ingresar: \n" +
                "   Complete todos los campos.\n" +
                "   Las cajas de texto del precio y las cantidades solo permiten números.\n" +
                "   Seleccionar el estado del producto en el 'combo box'.\n" +
                "4. Deshabilitar: \n" +
                "   Ingrese el elemento a deshabilitar y seleccione el botón 'Buscar' para ver los resultados.\n" +
                "   Seleccione el elemento e ingrese el motivo por el cual se desea deshabilitar\n" +
                "5. Actualizar: \n" +
                "   Ingrese el elemento a actualizar y seleccione el botón 'Buscar' para ver los resultados.\n" +
                "   Seleccione el elemento y proceda a editar el/los campos que desee.\n" +
                "   Se utiliza el mismo formato que ingresar. No deje campos vacíos.\n" +
                "6. Historial: Seleccione el rango de fecha a buscar y oprima el botón 'Historial' para desplegar los datos.", "Ayuda",
                MessageBoxButton.OK);
        }

        private void btn_actualizar_buscar_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_actualizar_buscar.Text == "")
            {
                MessageBox.Show("Ingrese el elemento a buscar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_ingresar_guardar_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_ingresar_codProd.Text == "" && textbox_ingresar_nombre.Text == "" && textbox_ingresar_proveedor.Text == ""
               && date_ingresar_fechaIngreso.Text == "" && textbox_ingresar_precio.Text == "" && textbox_ingresar_cantIngresar.Text == ""
               && textbox_ingresar_cantMinima.Text == "" && textbox_ingresar_fabricante.Text == "" && cmb_ingresar_estado.Text == ""
               && textbox_ingresar_descripcion.Text == "")
            {
                MessageBox.Show("Ingrese elementos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (textbox_ingresar_codProd.Text == "" || textbox_ingresar_nombre.Text == "" || textbox_ingresar_proveedor.Text == ""
               || date_ingresar_fechaIngreso.Text == "" || textbox_ingresar_precio.Text == "" || textbox_ingresar_cantIngresar.Text == ""
               || textbox_ingresar_cantMinima.Text == "" || textbox_ingresar_fabricante.Text == "" || cmb_ingresar_estado.Text == ""
               || textbox_ingresar_descripcion.Text == "")
            {
                MessageBox.Show("Faltan elementos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                MessageBox.Show("Datos Ingresados", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                btn_ingresar_limpiar_Click(sender, e);
            }
        }

        private void btn_deshabilitar_guardar_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_deshabilitar_cantDeshabilitar.Text == "" && textbox_deshabilitar_motivo.Text == "")
            {
                MessageBox.Show("Ingrese elementos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (textbox_deshabilitar_cantDeshabilitar.Text == "" || textbox_deshabilitar_motivo.Text == "")
            {
                MessageBox.Show("Faltan elementos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                MessageBox.Show("Datos Ingresados", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                btn_deshabilitar_limpiar_Click(sender, e);
            }
        }

        private void btn_actualizar_guardar_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_actualizar_nombre.Text == "" && textbox_actualizar_cantActual.Text == "" && textbox_actualizar_cantModificar.Text == ""
               && textbox_actualizar_proveedor.Text == "" && cmb_actualizar_estado.Text == "" && textbox_actualizar_precio.Text == ""
               && textbox_actualizar_cantMinima.Text == "" && date_actualizar_fechaIngreso.Text == "" && textbox_actualizar_fabricante.Text == ""
               && textbox_actualizar_descripcion.Text == "")
            {
                MessageBox.Show("Ingrese elementos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (textbox_actualizar_nombre.Text == "" || textbox_actualizar_cantActual.Text == "" || textbox_actualizar_cantModificar.Text == ""
               || textbox_actualizar_proveedor.Text == "" || cmb_actualizar_estado.Text == "" || textbox_actualizar_precio.Text == ""
               || textbox_actualizar_cantMinima.Text == "" || date_actualizar_fechaIngreso.Text == "" || textbox_actualizar_fabricante.Text == ""
               || textbox_actualizar_descripcion.Text == "")
            {
                MessageBox.Show("Faltan elementos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                MessageBox.Show("Datos Ingresados", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                btn_actualizar_limpiar_Click(sender, e);
            }
        }

        private void soloNumeros_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Sólo se permite ingresar números en este espacio.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            }
        }

        private void textbox_ingresar_cantMinima_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textbox_actualizar_precio_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textbox_buscar_buscar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_historial_buscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void textbox_ingresar_fechaIngreso_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
