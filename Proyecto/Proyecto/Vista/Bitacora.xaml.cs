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

namespace Proyecto.Vista
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Bitacora : Window
    {
        public string nombreUsuario;
        public Bitacora()
        {
            InitializeComponent();
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        //Funciones basicas de la pantalla de facturacion
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            txt_fecha.Content = DateTime.Now.ToString();
        }
        //funcion para volver al menu , le devolvemos el usuario con el que estamos en sesion
        private void btn_Salir_Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.cargarMenu(nombreUsuario);
            ventana.nombreUser = nombreUsuario;
            ventana.Show();
            this.Close();
        }


        //boton para cerrar sesion
        private void btn_Usuario_Click(object sender, RoutedEventArgs e)
        {
            Login ventana = new Login();
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
    }
}
