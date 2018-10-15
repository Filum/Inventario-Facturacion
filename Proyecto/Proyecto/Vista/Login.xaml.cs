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

namespace Proyecto
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
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

        private void txt_usuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_usuario.Text == "USUARIO")
            {
                txt_usuario.Text = "USUARIO";

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
            txt_usuario.Text = "";
            textbox_contraseña.Password = "";
        }

        private void click_Ingresar(object sender, RoutedEventArgs e)
        {
            /* try
             {
                 OracleConnection oracle = new OracleConnection("DATA SOURCE = XE ; PASSWORD = root ; USER ID = DELRAM");
                 oracle.Open();
                 MessageBox.Show("Conectado");
                 oracle.Close();

             }
             catch (Exception)
             {
                 MessageBox.Show("No se ha podido conectar con la base de datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                 //CREAR BOTON CONECTAR
             }*/

            Menu ventana = new Menu();
            ventana.Show();
            this.Close();

        }

        private void btn_Ayuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("El proyecto SIF(Sistema de Inventario y Facturación), es un sistema creado por la necesidad de la empresa DELRAM" +
            "de manejar el inventario que se moviliza por parte de la empresa y al mismo tiempo optimizar procesos que se generan " +
            "por parte de la empresa. Este sistema tambien presentara un modulo de facturacion donde se busca un manejo óptimo " +
            "de las facturas de la empresa. El proyecto tiene una elaboracion en conjunto de dos grupos de desarrollo, uno que " +
            "implementa el inventario y otro que implementa facturacion para lograr en el lapso de 3 semestres de trabajo " +
            "conjunto culminar con el sistema que ayudará a los procesos de DELRAM.", "Acerca", MessageBoxButton.OK, MessageBoxImage.Information);     
        }
    }
}
