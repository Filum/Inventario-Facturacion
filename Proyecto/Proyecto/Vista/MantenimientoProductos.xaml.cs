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
using Entidades;
using Logica;

namespace Proyecto
{
    /// <summary>
    /// Interaction logic for MantenimientoProductos.xaml
    /// </summary>
    public partial class MantenimientoProductos : Window
    {
        EntidadProveedores v_Clt = new EntidadProveedores();
        Model v_Model = new Model();

        public MantenimientoProductos()
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

        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.Show();
            this.Close();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.Show();
            this.Close();
        }

        private void btn_salir_roles__Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.Show();
            this.Close();
        }

        private void btn_limpiar_Click(object sender, RoutedEventArgs e)
        {
            txt_busqueda.Text = "";
            txb_codProd.Text = "";
            txb_nombre.Text = "";
            txb_cantMinima.Text = "";
            txb_cantModificar.Text = "";
            txb_cantActual.Text = "";
            cmb_estado.Text = "";
            txb_precio.Text = "";
            cmb_proveedor.Text = "";
            txb_fabricante.Text = "";
            txb_descripcion.Text = "";
        }

        private void btn_productos_ayuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Reglas: \n" +
                "1. Listar: Seleccione el botón 'Listar' para desplegar los datos. \n" +
                "2. Buscar: Ingrese el elemento a buscar y seleccione el botón Buscar para ver los resultados. \n" +
                "3. Ingresar: \n" +
                "   Complete todos los campos.\n" +
                "   Las cajas de texto del precio y las cantidades solo permiten números.\n" +
                "   Seleccionar el estado del producto en el 'combo box'.\n" +
                "4. Deshabilitar: \n" +
                "   Ingrese el elemento a deshabilitar y seleccione el botón 'Buscar' para ver los resultados.\n" +
                "   Seleccione el elemento e ingrese el motivo por el cual se desea deshabilitar\n" +
                "5. Actualizar: \n" +
                "   Ingrese el elemento a actualizar y seleccione el botón 'Buscar' para ver los resultados.\n" +
                "   Seleccione el elemento y proceda a editar el/los campos que desee.\n" +
                "   Se utiliza el mismo formato que ingresar. No deje campos vacíos.\n" +
                "6. Historial: Seleccione el rango de fecha a buscar y oprima el botón 'Historial' para desplegar los datos.", "Ayuda",
                MessageBoxButton.OK);
        }

        private void btn_agregar_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void soloNumeros_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Sólo se permite ingresar números en este espacio.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            }
        }

        private void btn_modificar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_usuarios_usuario_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult v_Result = MessageBox.Show("¿Desea cerrar sesión?", "Cerrar Sesión", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (v_Result == MessageBoxResult.Yes)
            {
                Login v_Ventana = new Login();
                this.Close();
                v_Ventana.Show();
            }
        }

        private void btn_listar_Click(object sender, RoutedEventArgs e)
        {
            if (date_inicio.SelectedDate != null && date_final.SelectedDate != null)
            {
                DateTime v_FechaInicio = DateTime.Parse(date_inicio.SelectedDate.Value.Date.ToShortDateString());
                DateTime v_FechaFinal = DateTime.Parse(date_final.SelectedDate.Value.Date.ToShortDateString());
                String v_Fecha1;
                v_Fecha1 = date_inicio.SelectedDate.Value.Date.ToShortDateString();
                String v_Fecha2;
                v_Fecha2 = date_final.SelectedDate.Value.Date.ToShortDateString();
                dtg_lista.ItemsSource = null;

                if (v_FechaInicio > v_FechaFinal)
                {
                    MessageBox.Show("El rango de fechas es incorrecto\nLa fecha inicial no puede ser mayor a la final", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (v_Model.MostrarListaProveedores(v_Fecha1, v_Fecha2).Rows.Count == 0)
                    {
                        MessageBox.Show("No hay datos registrados en el rango de fechas seleccionado", "Búsqueda", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        dtg_lista.ItemsSource = v_Model.MostrarListaProductos(v_Fecha1, v_Fecha2).DefaultView;
                        dtg_lista.Columns[0].Header = "Código Producto";
                        dtg_lista.Columns[1].Header = "Nombre del Producto";
                        dtg_lista.Columns[2].Header = "Marca";
                        dtg_lista.Columns[3].Header = "Cant en existencia";
                        dtg_lista.Columns[4].Header = "Cant mínima";
                        dtg_lista.Columns[5].Header = "Proveedor";
                        dtg_lista.Columns[6].Header = "Precio";
                        dtg_lista.Columns[7].Header = "Descripción";
                        dtg_lista.Columns[8].Header = "Fabricante";
                        dtg_lista.Columns[9].Header = "Estado";
                        dtg_lista.Columns[10].Header = "Fecha";
                    }
                }
            }
        }
    }
}
