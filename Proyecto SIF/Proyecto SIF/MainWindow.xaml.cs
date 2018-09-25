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

namespace Proyecto_SIF
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
           
        }

        private void txt_usuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_usuario.Text == "USUARIO")
            {
                txt_usuario.Text = "USUARIO";

            }
        }

        private void barra_movil__MouseDown(object sender, MouseButtonEventArgs e)

        {
            if(WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;       
            }

            this.DragMove();

        }
    }
}
