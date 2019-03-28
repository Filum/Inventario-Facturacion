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
using Entidades;

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
            datos.Verificarestadofactura();
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
            Menu ventana = new Menu();
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
                    if (detalleUsuario[13] == "ACTIVO")
                    {
                        if (detalleUsuario[1] == txb_contrasena.Password.ToString())
                        {
                            MessageBox.Show("Bienvenido " + detalleUsuario[0], "Bienvenido", MessageBoxButton.OK, MessageBoxImage.Information);
                            ventana.cargarMenu(txb_usuario.Text);
                            ventana.nombreUser = txb_usuario.Text;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("El usuario o la contraseña son incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Su usuario está inactivo ,comunicarse con el personal necesario para la activación del mismo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void txb_contrasena_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;

            // your event handler here
            e.Handled = true;
            verificar();
        }
        private void Validar_Usuario ()
        {

        }
    }
}
