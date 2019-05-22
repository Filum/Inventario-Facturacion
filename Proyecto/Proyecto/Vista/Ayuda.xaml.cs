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
using System.Windows.Forms;

namespace Proyecto.Vista
{
    /// <summary>
    /// Interaction logic for Ayuda.xaml
    /// </summary>
    public partial class Ayuda : Window
    {
        public string Pantalla;
        public Ayuda()
        {
            InitializeComponent(); System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        //Funciones basicas de la pantalla de facturacion
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            txt_fecha.Content = DateTime.Now.ToString();
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

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            Media.Play();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            Media.Pause();
        }

        private void Cmb_ayuda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(cmb_ayuda.SelectedItem.ToString())
            {
                case "Buscar y Listar":
                    Media.Source = new Uri("C:/SIF/Ayuda/prueba2.wmv");
                    Media.Play();
                    break;

                case "Facturar Productos":
                    Media.Source = new Uri("C:/SIF/Ayuda/facturarproductos.mp4");
                    Media.Play();
                    break;

                case "Facturar Servicios":
                    Media.Source = new Uri("C:/SIF/Ayuda/facturarservicios.mp4");
                    Media.Play();
                    break;

                case "Imprimir":
                    Media.Source = new Uri("C:/SIF/Ayuda/imprimir.mp4");
                    Media.Play();
                    break;

                case "Agregar":
                    Media.Source = new Uri("C:/SIF/Ayuda/agregar.mp4");
                    Media.Play();
                    break;
                case "Modificar":
                    Media.Source = new Uri("C:/SIF/Ayuda/modificar.mp4");
                    Media.Play();
                    break;

                case "Bitácora":
                    Media.Source = new Uri("C:/SIF/Ayuda/bitacora.mp4");
                    Media.Play();
                    break;
                case "Navegación":
                    Media.Source = new Uri("C:/SIF/Ayuda/navegacion.mp4");
                    Media.Play();
                    break;
        }
        }

        private void Cmb_ayuda_Initialized(object sender, EventArgs e)
        {
            cmb_ayuda.Items.Add("Buscar y Listar");
            cmb_ayuda.Items.Add("Facturar Productos");
            cmb_ayuda.Items.Add("Facturar Servicios");
            cmb_ayuda.Items.Add("Imprimir");
            cmb_ayuda.Items.Add("Agregar");
            cmb_ayuda.Items.Add("Modificar");
            cmb_ayuda.Items.Add("Bitácora");
            cmb_ayuda.Items.Add("Navegación");
        }
    }
}
