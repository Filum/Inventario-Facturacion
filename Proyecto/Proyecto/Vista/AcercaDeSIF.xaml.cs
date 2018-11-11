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
    /// Lógica de interacción para AcercaDeSIF.xaml
    /// </summary>
    public partial class AcercaDeSIF : Window
    {
        public AcercaDeSIF()
        {
            InitializeComponent();
        }

        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void barra_movil__MouseDown(object sender, MouseButtonEventArgs e)

        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }

            this.DragMove();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
