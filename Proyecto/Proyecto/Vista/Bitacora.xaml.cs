using Logica;
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
        Model datos = new Model();
        public Bitacora()
        {
            InitializeComponent();
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            date_historial_datte3.SelectedDate = DateTime.Now.Date;
            date_historial_datte4.SelectedDate = DateTime.Now.Date;
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

        private void Cmb_Mantenimiento_Initialized(object sender, EventArgs e)
        {
            cmb_Mantenimiento.Items.Add("Mantenimiento Clientes");
            cmb_Mantenimiento.Items.Add("Mantenimiento Proveedores");
            cmb_Mantenimiento.Items.Add("Mantenimiento Productos");
            cmb_Mantenimiento.Items.Add("Mantenimiento Roles");
            cmb_Mantenimiento.Items.Add("Mantenimiento Usuarios");
            cmb_Mantenimiento.Items.Add("Facturación Servicios");
            cmb_Mantenimiento.Items.Add("Facturación Productos");

        }

        private void Cmb_accion_Initialized(object sender, EventArgs e)
        {
            cmb_accion.Items.Add("Agregar");
            cmb_accion.Items.Add("Modificar");
            cmb_accion.Items.Add("Facturar");
        }

        private void btn_Listar_Click(object sender, RoutedEventArgs e)
        {
            if (date_historial_datte3.SelectedDate != null && date_historial_datte4.SelectedDate != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
                DateTime v_Date1 = DateTime.Parse(date_historial_datte3.SelectedDate.Value.Date.ToShortDateString());
                DateTime v_Date2 = DateTime.Parse(date_historial_datte4.SelectedDate.Value.Date.ToShortDateString());
                String v_Fecha1;
                v_Fecha1 = date_historial_datte3.SelectedDate.Value.Date.ToShortDateString();
                String v_Fecha2;
                v_Fecha2 = date_historial_datte4.SelectedDate.Value.Date.ToShortDateString();
                if (v_Date1 > v_Date2)
                {
                    MessageBox.Show("El rango de fechas es incorrecto\nLa fecha inicial no puede ser mayor a la final", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (datos.MostrarBitacoraPorFecha(v_Fecha1, v_Fecha2).Rows.Count == 0)
                    {
                        dtg_bitacora.ItemsSource = null;
                        MessageBox.Show("No hay datos registrados en el rango de fechas seleccionado", "Búsqueda", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        if (cmb_accion.SelectedItem == null && cmb_Mantenimiento.SelectedItem == null)
                            dtg_bitacora.ItemsSource = datos.MostrarBitacoraPorFecha(v_Fecha1, v_Fecha2).DefaultView;
                        else
                        {
                            try
                            {
                              dtg_bitacora.ItemsSource = datos.MostrarBitacoraPorFechaNombreAccion(cmb_Mantenimiento.SelectedItem.ToString(), cmb_accion.SelectedItem.ToString(),v_Fecha1, v_Fecha2).DefaultView;
                            }
                            catch (Exception m)
                            {
                                Console.WriteLine(m);
                            }
                        }
                        
                    }
                }

            }
            else
            {
                MessageBox.Show("Seleccione un rango de fechas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_imprimir_Click(object sender, RoutedEventArgs e)
        {
            Imprimir print = new Imprimir();
            print.imprimir(dtg_bitacora, "Bitácora");
        }

        private void Cmb_Mantenimiento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmb_accion.SelectedItem == null)
                {
                    dtg_bitacora.ItemsSource = datos.BitacoraMantenimieto(cmb_Mantenimiento.SelectedItem.ToString()).DefaultView;
                }
                else
                {
                    dtg_bitacora.ItemsSource = datos.BitacoraMantenimietoyAccion(cmb_Mantenimiento.SelectedItem.ToString(), cmb_accion.SelectedItem.ToString()).DefaultView;
                }
            }
            catch (Exception m)
            {
                Console.WriteLine(m);
            }
        }

        private void Cmb_accion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmb_Mantenimiento.SelectedItem == null)
                {
                    dtg_bitacora.ItemsSource = datos.BitacoraAccion(cmb_accion.SelectedItem.ToString()).DefaultView;
                }
                else
                {
                    dtg_bitacora.ItemsSource = datos.BitacoraMantenimietoyAccion(cmb_Mantenimiento.SelectedItem.ToString(), cmb_accion.SelectedItem.ToString()).DefaultView;
                }
            }
            catch (Exception m)
            {
                Console.WriteLine(m);
            }
        }

        private void Btn_ayuda_Click(object sender, RoutedEventArgs e)
        {
            Vista.Ayuda ventana = new Vista.Ayuda();
            ventana.Show();
            ventana.Pantalla = "Bitacora";
        }
    }
}
