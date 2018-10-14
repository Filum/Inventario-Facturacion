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
    /// Interaction logic for MantenimientoRoles.xaml
    /// </summary>
    public partial class MantenimientoRoles : Window
    {
        public MantenimientoRoles()
        {
            InitializeComponent();
            txt_fecha.Content = DateTime.Now.ToShortDateString();
            txt_hora.Content = DateTime.Now.ToShortTimeString();
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_buscar_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_rol_modificar.Text == "")
            {
                MessageBox.Show("No hay datos que buscar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {

            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListViewItem_Selected_2(object sender, RoutedEventArgs e)
        {

        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            Menu Ventana = new Menu();
            Ventana.Show();
            this.Close();
        }

        private void btn_buscar_rol_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {

        }

        private void btn_deshabilitar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_guardar_deshabilitar_rol_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_motivo_deshabilitar.Text == "")
            {
                MessageBox.Show("Ingrese el motivo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                MessageBox.Show("Rol Deshabilitado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btn_salir_roles__Click(object sender, RoutedEventArgs e)
        {
            Menu Ventana = new Menu();
            Ventana.Show();
            this.Close();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txb_motivo_deshabilitar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_listar_roles_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void dtg_actualizar_roles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_editar_rol_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_guardar_rol_actualizado_Click(object sender, EventArgs e)
        {
            if (textbox_actualizar_nombre.Text == "" || checkbox_mant_productos_actualizar.IsChecked == false && checkbox_mant_proveedores_actualizar.IsChecked == false && checkbox_mant_roles_actualizar.IsChecked == false && checkbox_mant_usuarios_actualizar.IsChecked == false)
            {
                MessageBox.Show("Faltan elementos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                MessageBox.Show("Datos Actualizados", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void btn_limpiar_actualizar_rol_Click(object sender, RoutedEventArgs e)
        {
            textbox_actualizar_nombre.Text = " ";
            checkbox_mant_productos_actualizar.IsChecked = false;
            checkbox_mant_usuarios_actualizar.IsChecked = false;
            checkbox_mant_proveedores_actualizar.IsChecked = false;
            checkbox_mant_roles_actualizar.IsChecked = false;
        }

        private void textbox_nombre_rol_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private void btn_buscar_deshabilitar_rol_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_rol_modificar.Text == "")
            {
                MessageBox.Show("No hay datos que buscar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Buscando...");
            }
        }

        private void btn_historial_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_usuario_roles_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_ayuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Secciones Mantenimiento de Roles:\n" +
                           "Listar: usted podra imprimir la lista de roles.\n" +
                           "Actualizar: esta sección le permite modificar datos de un rol.\n" +
                           "Ingresar: esta sección le permite ingresar roles al sistema.\n" +
                           "Deshabilitar: esta sección le permite deshabilitar roles.\n" +
                           "Historial: esta ventana le mostrara todos los cambios realizados en esta sección.\n" +
                           "Actualizar Roles: \n" +
                             "1 - Si desea actualiar un rol primero debe buscarlo por nombre o por el código.\n" +
                             "2 - Una ves que lo encontró lo puede editar y guardar los cambios.\n\n" +

                             "Ingresar Roles\n" +

                             "1 - Debera completar cada uno de los espacios requeridos para la creación del nuevo rol.\n" +
                             "2 - En caso de que cometa algún error el sistema le noticará.\n" +
                             "3 - En caso de que toos los datos esten corectos, proceda crearlo.\n\n" +

                             "Recuerde:\n" +
                             "El nombre del rol debe tener relación con los permisos \n\n" +
                             "Los mantenimientos dependen del tipo de rol otorgado \n\n" +

                             "Deshabilitar roles:\n" +
                             "1 - Busca el rol que desea deshabilitar, lo selecciona \n\n" +
                             "2 - Ingresa el motivo por el que desea dehabilitar el rol \n\n"

                          , "Ayuda");
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

        private void btn_minimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
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

        private void textbox_actualizar_nombre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_limpiar_rol_Click(object sender, RoutedEventArgs e)
        {
            textbox_nombre_rol.Text = " ";
            checkbox_mant_productos.IsChecked = false;
            checkbox_mant_usuarios.IsChecked = false;
            checkbox_mant_proveedores.IsChecked = false;
            checkbox_mant_roles.IsChecked = false;
        }

        private void btn_limpiar_deshabilitar_rol_Click(object sender, RoutedEventArgs e)
        {
            textbox_motivo_deshabilitar.Text = " ";
        }

        private void textbox_motivo_deshabilitar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textbox_rol_deshabilitar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_guardar_rol_Click(object sender, EventArgs e)
        {
            if (textbox_nombre_rol.Text == "" || checkbox_mant_productos.IsChecked == false && checkbox_mant_proveedores.IsChecked == false && checkbox_mant_roles.IsChecked == false && checkbox_mant_usuarios.IsChecked == false)
            {
                MessageBox.Show("Faltan elementos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                MessageBox.Show("Datos Ingresados", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
    }
}
