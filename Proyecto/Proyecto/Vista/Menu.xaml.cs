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

namespace Proyecto
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
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
            txt_fecha.Content = DateTime.Now.ToString();

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
                WindowState = WindowState.Normal;
            }
        }

        private void barra_movil__MouseDown(object sender, MouseButtonEventArgs e)

        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }

            this.DragMove();

        }
        private void btn_Usuario_Copy_Click(object sender, RoutedEventArgs e)
        {

        }


        private void btn_Facturar_Selected(object sender, MouseButtonEventArgs e)
        {
            Facturacion ventana = new Facturacion();
            ventana.Show();
            this.Close();
        }

        private void btn_Mantenimiento_Selected(object sender, RoutedEventArgs e)
        {
            MantenimientoUsuarios ventana = new MantenimientoUsuarios();
            ventana.Show();
            this.Close();
        }

        private void Mantenimiento_Selected_1(object sender, RoutedEventArgs e)
        {
            btn_Clientes.Visibility = Visibility.Visible;
            btn_Productos.Visibility = Visibility.Visible;
            btn_Proveedores.Visibility = Visibility.Visible;
            btn_Mantenimiento.Visibility = Visibility.Collapsed;
            btn_Roles.Visibility = Visibility.Collapsed;
            btn_Facturar.Visibility = Visibility.Collapsed;
            
        }

        private void Usuarios_Selected_1(object sender, RoutedEventArgs e)
        {
   
                btn_Clientes.Visibility = Visibility.Collapsed;
                btn_Productos.Visibility = Visibility.Collapsed;
                btn_Proveedores.Visibility = Visibility.Collapsed;
                btn_Mantenimiento.Visibility = Visibility.Visible;
                btn_Roles.Visibility = Visibility.Visible;
                btn_Facturar.Visibility = Visibility.Collapsed;       
        }

        private void Facturas_Selected(object sender, RoutedEventArgs e)
        {

            btn_Clientes.Visibility = Visibility.Collapsed;
            btn_Productos.Visibility = Visibility.Collapsed;
            btn_Proveedores.Visibility = Visibility.Collapsed;
            btn_Mantenimiento.Visibility = Visibility.Collapsed;
            btn_Roles.Visibility = Visibility.Collapsed;
            btn_Facturar.Visibility = Visibility.Visible;
        }

        private void btn_Facturar_Selected_1(object sender, RoutedEventArgs e)
        {
            Facturacion ventana = new Facturacion();
            ventana.Show();
            this.Close();
        }

        private void btn_Roles_Selected(object sender, RoutedEventArgs e)
        {
            MantenimientoRoles ventana = new MantenimientoRoles();
            ventana.Show();
            this.Close();
        }

        private void btn_Clientes_Selected(object sender, RoutedEventArgs e)
        {
            Clientes ventana = new Clientes();
            ventana.Show();
            this.Close();
        }

        private void btn_Productos_Selected(object sender, RoutedEventArgs e)
        {
            MantenimientoProductos ventana = new MantenimientoProductos();
            ventana.Show();
            this.Close();
        }

        private void btn_Usuario_Click(object sender, RoutedEventArgs e)
        {
            Login ventana = new Login();
            ventana.Show();
            this.Close();

        }

        private void btn_Proveedores_Selected(object sender, RoutedEventArgs e)
        {
            MantenimientoProveedores ventana = new MantenimientoProveedores();
            ventana.Show();
            this.Close();
        }

        private void Bitacora_Selected(object sender, RoutedEventArgs e)
        {
            btn_Clientes.Visibility = Visibility.Collapsed;
            btn_Productos.Visibility = Visibility.Collapsed;
            btn_Proveedores.Visibility = Visibility.Collapsed;
            btn_Mantenimiento.Visibility = Visibility.Collapsed;
            btn_Roles.Visibility = Visibility.Collapsed;
            btn_Facturar.Visibility = Visibility.Collapsed;
        }
    }
}
