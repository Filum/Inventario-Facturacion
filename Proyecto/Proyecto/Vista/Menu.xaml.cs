using Entidades;
using Logica;
using Proyecto.Vista;
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
        public string nombreUser;
        Model datos = new Model();
        public Menu()
        {
            InitializeComponent();

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
            ventana.nombreUsuario = nombreUser;
            ventana.Show();
            this.Close();
        }

        private void btn_Mantenimiento_Selected(object sender, RoutedEventArgs e)
        {
            MantenimientoUsuarios ventana = new MantenimientoUsuarios();
            ventana.nombreUsuario = nombreUser;
            ventana.Show();
            this.Close();
        }

        private void Mantenimiento_Selected_1(object sender, RoutedEventArgs e)
        {
                if(btn_Clientes.IsEnabled == true)
                    btn_Clientes.Visibility = Visibility.Visible;

                if (btn_Productos.IsEnabled == true)
                    btn_Productos.Visibility = Visibility.Visible;

                if (btn_Proveedores.IsEnabled == true)
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
                btn_Facturar.Visibility = Visibility.Collapsed;

                if (btn_Mantenimiento.IsEnabled == true)
                    btn_Mantenimiento.Visibility = Visibility.Visible;

                if (btn_Roles.IsEnabled == true)
                    btn_Roles.Visibility = Visibility.Visible; 
            
        }

        private void Facturas_Selected(object sender, RoutedEventArgs e)
        {

            btn_Clientes.Visibility = Visibility.Collapsed;
            btn_Productos.Visibility = Visibility.Collapsed;
            btn_Proveedores.Visibility = Visibility.Collapsed;
            btn_Mantenimiento.Visibility = Visibility.Collapsed;
            btn_Roles.Visibility = Visibility.Collapsed;

            if (btn_Facturar.IsEnabled == true)
                btn_Facturar.Visibility = Visibility.Visible;
        }

        private void btn_Facturar_Selected_1(object sender, RoutedEventArgs e)
        {
            Facturacion ventana = new Facturacion();
            ventana.nombreUsuario = nombreUser;
            ventana.Show();
            this.Close();
        }

        private void btn_Roles_Selected(object sender, RoutedEventArgs e)
        {
            MantenimientoRoles ventana = new MantenimientoRoles();
            ventana.nombreUsuario = nombreUser;
            ventana.Show();
            this.Close();
            
        }

        private void btn_Clientes_Selected(object sender, RoutedEventArgs e)
        {
            Clientes ventana = new Clientes();
            ventana.nombreUsuario = nombreUser;
            ventana.Show();
            this.Close();
        }

        private void btn_Productos_Selected(object sender, RoutedEventArgs e)
        {
            MantenimientoProductos ventana = new MantenimientoProductos();
            ventana.nombreUsuario = nombreUser;
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
            ventana.nombreUsuario = nombreUser;
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

        private void Button_Click(object sender, RoutedEventArgs e)
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

        private void btn_Ayuda_Click(object sender, RoutedEventArgs e)
        {
            AcercaDeSIF ventana = new AcercaDeSIF();
            ventana.Show();
        }

        public void cargarMenu(string valor)
        {
            var detalleUsuario = new List<string>();
            detalleUsuario = datos.consultarUsuario(valor);
            Roles(detalleUsuario[2], detalleUsuario[3], detalleUsuario[4], detalleUsuario[5], detalleUsuario[6], detalleUsuario[7], detalleUsuario[8], detalleUsuario[9], detalleUsuario[10], detalleUsuario[11], detalleUsuario[12]);
        }
        private void Roles(string m_clientes, string m_proveedores, string m_productos, string m_usuarios, string m_roles, string facturacion, string bitacora, string nombreUsuario, string apellidos, string puesto, string nombreRol)
        {

            if (m_clientes == "✓")
                btn_Clientes.IsEnabled = true;
            else if (m_clientes == "X")
                btn_Clientes.IsEnabled = false;

            if (m_proveedores == "✓")
                btn_Proveedores.IsEnabled = true;
            else if (m_proveedores == "X")
                btn_Proveedores.IsEnabled = false;

            if (m_productos == "✓")
                btn_Productos.IsEnabled = true;
            else if (m_productos == "X")
                btn_Productos.IsEnabled = false;

            if (m_usuarios == "✓")
                btn_Mantenimiento.IsEnabled = true;
            else if (m_usuarios == "X")
                btn_Mantenimiento.IsEnabled = false;

            if (m_roles == "✓")
                btn_Roles.IsEnabled = true;
            else if (m_roles == "X")
                btn_Roles.IsEnabled = false;

            if (facturacion == "✓")
                btn_Facturar.IsEnabled = true;
            else if (facturacion == "X")
                btn_Facturar.IsEnabled = false;

            if (btn_Clientes.IsEnabled == false && btn_Proveedores.IsEnabled == false && btn_Productos.IsEnabled == false)
                Mantenimiento.Visibility = Visibility.Collapsed;
            else
                Mantenimiento.Visibility = Visibility.Visible;

            if (btn_Mantenimiento.IsEnabled == false && btn_Roles.IsEnabled == false)
                Usuarios.Visibility = Visibility.Collapsed;
            else
                Usuarios.Visibility = Visibility.Visible;

            if (btn_Facturar.IsEnabled == false)
                Facturas.Visibility = Visibility.Collapsed;
            else
                Facturas.Visibility = Visibility.Visible;

            if (bitacora == "✓")
                Bitacora.Visibility = Visibility.Visible;
            else if (bitacora == "X")
                Bitacora.Visibility = Visibility.Collapsed;

            txt_nombreUsuario.Text = nombreUsuario;
            txt_apellidoUsuario.Text = apellidos;
            txt_Puesto.Text = puesto;
            txt_Rol.Text = nombreRol;

            this.Show();
        }
    }
}
