using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        Model v_Model = new Model();
        EntidadProductos v_Clt = new EntidadProductos();
        bool v_Actividad_btnModificar = false;
        bool v_Actividad_btnAgregar = true;

        public MantenimientoProductos()
        {
            InitializeComponent();
            //Formato para la hora
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            date_inicio.SelectedDate = DateTime.Now.Date;
            date_final.SelectedDate = DateTime.Now.Date;

            LlenarComboboxProveedores();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            txt_fecha.Content = DateTime.Now.ToString();
        }

        private void LlenarComboboxProveedores()
        {
            //Llenar combobox de proveedor
            var v_ProveedoresExistentes = new List<EntidadProveedores>();
            v_ProveedoresExistentes = v_Model.ProveedoresExistentes();
            cmb_proveedor.ItemsSource = v_ProveedoresExistentes;
            cmb_proveedor.DisplayMemberPath = "v_Nombre";
            cmb_proveedor.SelectedValuePath = "v_IdProveedor";
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
            txb_busqueda.Text = "";
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
            dtg_productos.ItemsSource = null;
            lbl_errorCodProd.Visibility = Visibility.Hidden;
            lbl_errorBusqueda.Visibility = Visibility.Collapsed;
            lbl_errorNombre.Visibility = Visibility.Collapsed;
            lbl_errorCantModificar.Visibility = Visibility.Collapsed;
            lbl_errorCantMinima.Visibility = Visibility.Collapsed;
            lbl_errorCantActual.Visibility = Visibility.Collapsed;
            lbl_errorEstado.Visibility = Visibility.Hidden;
            lbl_errorDescripcion.Visibility = Visibility.Collapsed;
            lbl_errorFabricante.Visibility = Visibility.Collapsed;
            lbl_errorPrecio.Visibility = Visibility.Collapsed;
            lbl_errorProveedor.Visibility = Visibility.Collapsed;
            lbl_actividad.Visibility = Visibility.Collapsed;
            btn_agregar.Visibility = Visibility.Collapsed;
            btn_modificar.Visibility = Visibility.Collapsed;
            v_Actividad_btnModificar = false;
            inicializarAgregacion();
        }

        private void btn_productos_ayuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Mantenimiento de Productos: \n\n" +
               "1. Listar: Seleccione el rango de fechas y oprima el botón 'Listar' para desplegar los datos. \n\n" +
               "2. Buscar: Ingrese el elemento a buscar, puede ser por código o por nombre del producto. \n" +
               "   Si existe el producto se le deslegará los datos y podrá seleccionarlo para modificarlo o deshabilitarlo.\n" +
               "   Si no existe el producto se le permitirá agregarlo al sistema.\n\n" +
               "   Agregar: \n" +
               "   Complete todos los campos, excepto los opcionales.\n" +
               "   Las cajas de texto de las cantidades y del precio solo permiten números.\n" +
               "   No ingrese caracteres especiales. \n" +
               "   Al agregar un nuevo producto la cantidad a modificar se debe dejar en blanco, siempre y cuando no tenga errores. \n\n" +
               "   Deshabilitar: \n" +
               "   Seleccione el elemento del datagrid e ingrese el motivo por el cual se desea deshabilitar. \n\n" +
               "   Actualizar: \n" +
               "   Seleccione el elemento y proceda a editar los campos que desee.\n" +
               "   Se utiliza el mismo formato de validaciones de ingresar.", "Ayuda",
               MessageBoxButton.OK);
        }

        private void btn_agregar_Click(object sender, RoutedEventArgs e)
        {
         
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
                        dtg_lista.Columns[9].Header = "Estado del Producto";
                        dtg_lista.Columns[10].Header = "Fecha";
                        dtg_lista.Columns[11].Header = "Estado en el Sistema";
                    }
                }
            }
        }

        private void txb_busqueda_KeyUp(object sender, KeyEventArgs e)
        {
           // LimpiarTextboxAgregar();

            if (txb_busqueda.Text == "")
            {
                txb_nombre.Text = "";
                btn_limpiar_Click(sender, e);
                btn_agregar.Visibility = Visibility.Collapsed;
                btn_modificar.Visibility = Visibility.Collapsed;
            }
            else if (txb_busqueda.Text.Contains("'"))
            {
                lbl_errorBusqueda.Content = "No se permiten caracteres especiales";
                lbl_errorBusqueda.Visibility = Visibility.Visible;
            }
            else
            {
                dtg_productos.ItemsSource = v_Model.ValidarBusquedaProductos(txb_busqueda.Text);
                dtg_productos.Columns[0].Header = "#";
                dtg_productos.Columns[1].Header = "Código del Producto";
                dtg_productos.Columns[2].Header = "Nombre del Producto";
                dtg_productos.Columns[3].Header = "Marca";
                dtg_productos.Columns[4].Header = "Cantidad en Existencia";
                dtg_productos.Columns[5].Header = "Cantidad Mínima";
                dtg_productos.Columns[6].Header = "Proveedor";
                dtg_productos.Columns[7].Header = "Precio Unitario";
                dtg_productos.Columns[8].Header = "Descripción";
                dtg_productos.Columns[9].Header = "Fabricante";
                dtg_productos.Columns[10].Header = "Estado del Producto";
                dtg_productos.Columns[11].Header = "Fecha de Ingreso";
                dtg_productos.Columns[12].Header = "Estado en el Sistema";

                if (dtg_productos.Items.Count == 0)//El producto no existe
                {
                    btn_modificar.Visibility = Visibility.Collapsed;
                    inicializarAgregacion();

                    if (Regex.IsMatch(this.txb_busqueda.Text, "[a-zA-Z]"))
                    {
                        if (ValidarCaracteresEspeciales(txb_busqueda.Text, "nombre") == true)
                        {
                            lbl_errorBusqueda.Content = "No se permiten caracteres especiales";
                            lbl_errorBusqueda.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            //HACER*****************
                            txb_busqueda.MaxLength = 35;
                            lbl_errorBusqueda.Visibility = Visibility.Collapsed;
                        }
                    }
                }
                else//Productos existentes
                {
                    v_Actividad_btnAgregar = false;
                    btn_agregar.Visibility = Visibility.Collapsed;
                    btn_modificar.Visibility = Visibility.Collapsed;
                    lbl_actividad.Content = "Productos existentes";
                    lbl_actividad.Visibility = Visibility.Visible;
                }
            }
        }

        /*Método el cual cumple con la funcionalidad de desplegar los datos en los campos correspondientes de un producto seleccionado 
        en el DataGrid con la finalidad de ser modificado, esto en el tab de gestión de productos*/
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;

            v_Clt.v_IdProducto = Convert.ToInt64((dtg_productos.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text);

            txb_codProd.Text = (dtg_productos.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text;
            txb_nombre.Text = (dtg_productos.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;
            //marca
            txb_cantActual.Text = (dtg_productos.SelectedCells[4].Column.GetCellContent(row) as TextBlock).Text;
            txb_cantMinima.Text = (dtg_productos.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text;
            cmb_proveedor.Text = (dtg_productos.SelectedCells[6].Column.GetCellContent(row) as TextBlock).Text;
            txb_precio.Text = (dtg_productos.SelectedCells[7].Column.GetCellContent(row) as TextBlock).Text;
            txb_descripcion.Text = (dtg_productos.SelectedCells[8].Column.GetCellContent(row) as TextBlock).Text;
            txb_fabricante.Text = (dtg_productos.SelectedCells[9].Column.GetCellContent(row) as TextBlock).Text;
            cmb_estado.Text = (dtg_productos.SelectedCells[10].Column.GetCellContent(row) as TextBlock).Text;

            lbl_actividad.Content = "Modificar producto";
            lbl_actividad.Visibility = Visibility.Visible;
            v_Actividad_btnModificar = true;
            v_Actividad_btnAgregar = false;
        }

        //Método el cual habilita el botón modificar
        private void HabilitarBtnModificar()
        {
            if (v_Actividad_btnModificar == true)
            {
                btn_modificar.Visibility = Visibility.Visible;
            }
        }

        //Inicializa las opciones de agregar en la ventana 
        private void inicializarAgregacion()
        {
            v_Actividad_btnAgregar = true;
            lbl_actividad.Content = "Agregar producto";
            lbl_actividad.Visibility = Visibility.Visible;
        }

        //Método el cual habilita el botón agregar siempre y cuando los espacios correspondientes para esta actividad no estén vacíos  situados en el tab de gestión de productos
        private void HabilitarBtnAgregar()
        {
            if (v_Actividad_btnAgregar == true)
            {
                if (txb_codProd.Text != "" && txb_nombre.Text != "" && txb_cantMinima.Text != "" && cmb_estado.Text != "" && txb_precio.Text != "" && cmb_proveedor.Text != "" && txb_fabricante.Text != "" && txb_descripcion.Text != "")
                {
                    btn_agregar.Visibility = Visibility.Visible;
                }
                else
                {
                    btn_agregar.Visibility = Visibility.Collapsed;
                }
            }
        }

        //Método el cual valida si en las cajas de texto recibidos contiene caracteres especiales
        private Boolean ValidarCaracteresEspeciales(String v_Txb, String v_Identificador)
        {
            if (v_Identificador == "numeros")
            {
                //caracteres que permite si la cadena es de int
                String v_Caracteres = "[a-zA-Z !@#$%^&*())+=.,<>{}¬º´/\"':;|ñÑ~¡?`¿-]";
                if (Regex.IsMatch(v_Txb, v_Caracteres))
                {
                    return true;
                }
            }
            else if (v_Identificador == "nombre")
            {
                //caracteres que permite si la cadena es de string
                String v_Caracteres = "[!@#$%^*())+=.,<>{}¬º´/\"':;|~¡?`¿-]";
                if (Regex.IsMatch(v_Txb, v_Caracteres))
                {
                    return true;
                }
            }
            else if (v_Identificador == "descripcion")
            {
                //caracteres que permite si la cadena es de string
                String v_Caracteres = "[!@#$%^*())+=.<>{}¬º´/\"':;|~¡?`¿]";
                if (Regex.IsMatch(v_Txb, v_Caracteres))
                {
                    return true;
                }
            }
            return false;
        }
        //botón que permite llamar a la clase para imprimir, tomando la tabla con información
        private void btn_imprimir_Click(object sender, RoutedEventArgs e)
        {
            Imprimir print = new Imprimir();
            print.imprimir(dtg_lista, "Productos en Inventario");

        }




    }//Fin de la clase
}
