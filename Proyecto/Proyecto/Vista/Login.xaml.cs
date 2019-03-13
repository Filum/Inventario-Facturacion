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
using System.Data.OracleClient;
using System.Windows.Threading;
using Proyecto.Vista;
using Logica;

namespace Proyecto
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        Model datos = new Model();
        public Login()
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

        private void txt_usuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txb_usuario.Text == "USUARIO")
            {
                txb_usuario.Text = "USUARIO";

            }
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

        private void click_Limpiar_Datos(object sender, RoutedEventArgs e)
        {
            txb_usuario.Text = "";
            txb_contrasena.Password = "";
        }

        private void click_Ingresar(object sender, RoutedEventArgs e)
        {
             verificar();

        }

        private void btn_Ayuda_Click(object sender, RoutedEventArgs e)
        {
            AcercaDeSIF ventana = new AcercaDeSIF();
            ventana.Show();
        }
        private void verificar()
        {
            var detalleUsuario = new List<string>();
            if (txb_usuario.Text == "" || txb_contrasena.Password == "")
            {
                MessageBox.Show("El usuario o la contraseña son incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                detalleUsuario = datos.consultarUsuario(txb_usuario.Text);
                if (detalleUsuario.Count() == 0)
                {
                    MessageBox.Show("El usuario o la contraseña son incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (detalleUsuario[1] == txb_contrasena.Password.ToString())
                    {
                        MessageBox.Show("Bienvenido " + detalleUsuario[0], "Bienvenido", MessageBoxButton.OK, MessageBoxImage.Information);
                        Roles(detalleUsuario[2], detalleUsuario[3], detalleUsuario[4], detalleUsuario[5], detalleUsuario[6], detalleUsuario[7], detalleUsuario[8]);
                    }
                    else
                    {
                        MessageBox.Show("El usuario o la contraseña son incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void Roles(string m_clientes,string m_proveedores,string m_productos,string m_usuarios,string m_roles,string facturacion ,string bitacora)
        {
            Menu ventana = new Menu();

            if (m_clientes == "✓")
                ventana.btn_Clientes.IsEnabled = true; 
            else if(m_clientes == "X")
                ventana.btn_Clientes.IsEnabled = false;

            if (m_proveedores == "✓")
                ventana.btn_Proveedores.IsEnabled = true;
            else if (m_proveedores == "X")
                ventana.btn_Proveedores.IsEnabled = false;

            if (m_productos == "✓")
                ventana.btn_Productos.IsEnabled = true;
            else if (m_productos == "X")
                ventana.btn_Productos.IsEnabled = false;

            if (m_usuarios == "✓")
                ventana.btn_Mantenimiento.IsEnabled = true;
            else if (m_usuarios == "X")
                ventana.btn_Mantenimiento.IsEnabled = false;

            if (m_roles == "✓")
                ventana.btn_Roles.IsEnabled = true;
            else if (m_roles == "X")
                ventana.btn_Roles.IsEnabled = false;

            if (facturacion == "✓")
                ventana.btn_Facturar.IsEnabled = true;
            else if (facturacion == "X")
                ventana.btn_Facturar.IsEnabled = false;

            if (ventana.btn_Clientes.IsEnabled == false && ventana.btn_Proveedores.IsEnabled == false && ventana.btn_Productos.IsEnabled == false)
                ventana.Mantenimiento.Visibility = Visibility.Collapsed;
            else 
                ventana.Mantenimiento.Visibility = Visibility.Visible;

            if (ventana.btn_Mantenimiento.IsEnabled == false && ventana.btn_Roles.IsEnabled == false)
                ventana.Usuarios.Visibility = Visibility.Collapsed;
            else 
                ventana.Usuarios.Visibility = Visibility.Visible;

            if (ventana.btn_Facturar.IsEnabled == false)
                ventana.Facturas.Visibility = Visibility.Collapsed;
            else 
                ventana.Facturas.Visibility = Visibility.Visible;

            if (bitacora == "✓")
                ventana.Bitacora.Visibility = Visibility.Visible;
            else if (bitacora == "X")
                ventana.Bitacora.Visibility = Visibility.Collapsed;

            ventana.Show();
            this.Close();
        }
    }
}
