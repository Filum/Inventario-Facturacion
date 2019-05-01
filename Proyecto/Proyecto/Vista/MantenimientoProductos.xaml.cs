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
        String v_EstadoSistema = "";
        public string nombreUsuario;

        public MantenimientoProductos()
        {
            InitializeComponent();
            //Formato para la hora
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            lbl_actividad.Content = "Productos existentes";
            dispatcherTimer.Start();

            MostrarProductosExistentes();
            cmb_tipoBusqueda.SelectedIndex = 2;
            dtg_lista.ItemsSource = v_Model.MostrarListaProductos("LISTAPRODUCTOS").DefaultView;
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
            ventana.cargarMenu(nombreUsuario);
            ventana.nombreUser = nombreUsuario;
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
            ventana.cargarMenu(nombreUsuario);
            ventana.nombreUser = nombreUsuario;
            this.Close();
        }


        private void HabilitarBtnAgregar()
        {
            if (v_Actividad_btnAgregar == true)
            {
                if (txb_codProd.Text == "" || txb_nombre.Text == "" || txb_marca.Text == "" || txb_cantActual.Text == "" || txb_cantMinima.Text == "" || txb_precio.Text == "" || cmb_proveedor.Text == "" || txb_descripcion.Text == "" || txb_fabricante.Text == "" || (rb_activo.IsChecked == false && rb_inactivo.IsChecked == false))
                {
                    ValidarComponentes();
                    btn_agregar.Visibility = Visibility.Collapsed;
                }
                else
                {
                    btn_agregar.Visibility = Visibility.Visible;
                }
            }
        }

        private void ValidarComponentes()
        {
            if (rb_inactivo.IsChecked == false && rb_activo.IsChecked == false)
            {
                lbl_errorRB.Visibility = Visibility.Visible;
                lbl_errorRB.Content = "Debe seleccionar una opción";
            }
            else
            {
                lbl_errorRB.Visibility = Visibility.Hidden;
            }


            if (cmb_estadoprod.SelectedItem == null)
            {
                lbl_errorestadoProd.Visibility = Visibility.Visible;
                lbl_errorestadoProd.Content = "Debe seleccionar una opción";
            }
            else
            {
                lbl_errorestadoProd.Visibility = Visibility.Hidden;
            }

            if (cmb_proveedor.SelectedItem == null)
            {
                lbl_errorProveedor.Visibility = Visibility.Visible;
                lbl_errorProveedor.Content = "Debe seleccionar una opción";
            }
            else
            {
                lbl_errorProveedor.Visibility = Visibility.Hidden;
            }

        }

        private void btn_limpiar_Click(object sender, RoutedEventArgs e)
        {
            txb_busqueda.Text = "";
            txb_codProd.Text = "";
            txb_nombre.Text = "";
            txb_cantMinima.Text = "";
            txb_cantModificar.Text = "";
            txb_cantActual.Text = "";
            cmb_estadoprod.Text = "";
            txb_precio.Text = "";
            cmb_proveedor.Text = "";
            txb_marca.Text = "";
            txb_fabricante.Text = "";
            txb_descripcion.Text = "";
            rb_activo.IsChecked = true;
            rb_activo.IsChecked = false;
            dtg_productos.ItemsSource = null;
            lbl_errorCodProd.Visibility = Visibility.Hidden;
            lbl_errorBusqueda.Visibility = Visibility.Hidden;
            lbl_errorNombre.Visibility = Visibility.Hidden;
            lbl_errorCantModificar.Visibility = Visibility.Hidden;
            lbl_errorCantMinima.Visibility = Visibility.Hidden;
            lbl_errorCantActual.Visibility = Visibility.Hidden;
            lbl_errorDescripcion.Visibility = Visibility.Hidden;
            lbl_errorFabricante.Visibility = Visibility.Hidden;
            lbl_errorPrecio.Visibility = Visibility.Hidden;
            lbl_errorProveedor.Visibility = Visibility.Hidden;
            lbl_actividad.Visibility = Visibility.Collapsed;
            btn_agregar.Visibility = Visibility.Collapsed;
            btn_modificar.Visibility = Visibility.Collapsed;
            v_Actividad_btnModificar = false;
            rb_aumentar.IsChecked = false;
            rb_disminuir.IsChecked = false;
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
            if ((lbl_errorCodProd.Visibility == Visibility.Visible) || (lbl_errorNombre.Visibility == Visibility.Visible) || (lbl_errorCantActual.Visibility == Visibility.Visible) ||
                    (lbl_errorCantMinima.Visibility == Visibility.Visible) || (lbl_errorCantModificar.Visibility == Visibility.Visible) || (lbl_errorDescripcion.Visibility == Visibility.Visible) || lbl_errorestadoProd.Visibility == Visibility.Visible || lbl_errorFabricante.Visibility == Visibility.Visible ||
                    lbl_errorMarca.Visibility == Visibility.Visible || lbl_errorPrecio.Visibility == Visibility.Visible || lbl_errorProveedor.Visibility == Visibility.Visible ||
                    lbl_errorRB.Visibility == Visibility.Visible)
            {
                MessageBox.Show("Error al agregar\nHacen falta campos por rellenar o errores que corregir", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    v_Clt.v_CodigoProducto = txb_codProd.Text;
                    v_Clt.v_NombreProducto = txb_nombre.Text;
                    v_Clt.v_MarcaProducto = txb_marca.Text;
                    v_Clt.v_CantidadExistencia = Convert.ToInt64(txb_cantActual.Text);
                    v_Clt.v_CantidadMinima = Convert.ToInt64(txb_cantMinima.Text);

                    EntidadProveedores ComboItem = (EntidadProveedores)cmb_proveedor.SelectedItem;
                    Int64 idProveedor = ComboItem.v_IdProveedor;
                    v_Clt.v_IdProveedor = idProveedor;

                    v_Clt.v_PrecioUnitario = Convert.ToInt64(txb_precio.Text);
                    v_Clt.v_Descripcion = txb_descripcion.Text;
                    v_Clt.v_Fabricante = txb_fabricante.Text;

                    if (cmb_estadoprod.SelectedIndex == 0)
                    {
                        v_Clt.v_EstadoProducto = "Nuevo";
                    }
                    else if (cmb_estadoprod.SelectedIndex == 1)
                    {
                        v_Clt.v_EstadoProducto = "Usado";
                    }
                    else if (cmb_estadoprod.SelectedIndex == 2)
                    {
                        v_Clt.v_EstadoProducto = "Dañado";
                    }

                    if (rb_inactivo.IsChecked == true)
                    {
                        v_Clt.v_EstadoSistema = "INACTIVO";
                    }
                    else
                    {
                        v_Clt.v_EstadoSistema = "ACTIVO";
                    }

                    int v_Resultado = v_Model.AgregarProductos(v_Clt);

                   
                    if (lbl_errorCodProd.Visibility == Visibility.Visible)
                    {
                        MessageBox.Show("Error al agregar\nHacen falta campos por rellenar o errores que corregir", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (v_Resultado == -1)
                    {
                        MessageBox.Show("Datos ingresados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        btn_limpiar_Click(sender, e);
                        MostrarProductosExistentes();
                    }
                }
                catch (Exception m)
                {
                    Console.WriteLine(m.ToString());
                    MessageBox.Show("Error al agregar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }

        }

        private void btn_modificar_Click(object sender, RoutedEventArgs e)
        {
            if ((lbl_errorCodProd.Visibility == Visibility.Visible) || (lbl_errorNombre.Visibility == Visibility.Visible) || (lbl_errorCantActual.Visibility == Visibility.Visible) ||
                    (lbl_errorCantMinima.Visibility == Visibility.Visible)|| (lbl_errorCantModificar.Visibility == Visibility.Visible) || (lbl_errorDescripcion.Visibility == Visibility.Visible) || lbl_errorestadoProd.Visibility == Visibility.Visible || lbl_errorFabricante.Visibility == Visibility.Visible ||
                    lbl_errorMarca.Visibility == Visibility.Visible || lbl_errorPrecio.Visibility == Visibility.Visible || lbl_errorProveedor.Visibility == Visibility.Visible ||
                    lbl_errorRB.Visibility == Visibility.Visible)
            {
                MessageBox.Show("Error al modificar\nHacen falta campos por rellenar o errores que corregir", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    v_Clt.v_CodigoProducto = txb_codProd.Text;
                    v_Clt.v_NombreProducto = txb_nombre.Text;
                    v_Clt.v_MarcaProducto = txb_marca.Text;
                    long v_NuevaCantidad;
                    if (rb_aumentar.IsChecked == true) {
                        v_NuevaCantidad = Convert.ToInt64(txb_cantActual.Text) + Convert.ToInt64(txb_cantModificar.Text);
                        v_Clt.v_CantidadExistencia = v_NuevaCantidad;
                    }
                    if (rb_disminuir.IsChecked == true)
                    {
                        v_NuevaCantidad = Convert.ToInt64(txb_cantActual.Text) - Convert.ToInt64(txb_cantModificar.Text);
                        v_Clt.v_CantidadExistencia = v_NuevaCantidad;
                    }
                   // v_Clt.v_CantidadExistencia = Convert.ToInt64(txb_cantActual.Text);
                    v_Clt.v_CantidadMinima = Convert.ToInt64(txb_cantMinima.Text);

                    EntidadProveedores ComboItem = (EntidadProveedores)cmb_proveedor.SelectedItem;
                    Int64 idProveedor = ComboItem.v_IdProveedor;
                    v_Clt.v_IdProveedor = idProveedor;

                    v_Clt.v_PrecioUnitario = Convert.ToInt64(txb_precio.Text);
                    v_Clt.v_Descripcion = txb_descripcion.Text;
                    v_Clt.v_Fabricante = txb_fabricante.Text;

                    if(cmb_estadoprod.SelectedIndex == 0){
                        v_Clt.v_EstadoProducto = "Nuevo";
                    }
                    else if (cmb_estadoprod.SelectedIndex == 1)
                    {
                        v_Clt.v_EstadoProducto = "Usado";
                    }
                    else if (cmb_estadoprod.SelectedIndex == 2)
                    {
                        v_Clt.v_EstadoProducto = "Dañado";
                    }

                    if (rb_inactivo.IsChecked == true)
                    {
                        v_Clt.v_EstadoSistema = "INACTIVO";
                    }
                    else
                    {
                        v_Clt.v_EstadoSistema = "ACTIVO";
                    }


                    if (v_Model.ValidarModificacionProducto(v_Clt) == true)
                    {
                        lbl_errorCodProd.Visibility = Visibility.Hidden;
                    } 
                    if (lbl_errorCodProd.Visibility == Visibility.Visible)
                    {
                        MessageBox.Show("Error al modificar\nHacen falta campos por rellenar o errores que corregir", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (v_Model.ModificarProductos(v_Clt) == -1)
                    {
                        MessageBox.Show("Datos modificados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        btn_limpiar_Click(sender, e);
                        v_Actividad_btnAgregar = true;
                        MostrarProductosExistentes();
                    }
                }
                catch (Exception m)
                {
                    Console.WriteLine(m.ToString());
                    MessageBox.Show("Error al modificar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
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
       
                dtg_lista.ItemsSource = null;
                if (v_Model.MostrarListaProductos(v_EstadoSistema).Rows.Count == 0)
                {
                    MessageBox.Show("No existen productos registrados en el sistema", "Búsqueda", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    dtg_lista.ItemsSource = v_Model.MostrarListaProductos(v_EstadoSistema).DefaultView;
                }
        }

        /*Método el cual cumple con la funcionalidad de desplegar los datos en los campos correspondientes de un producto seleccionado 
        en el DataGrid con la finalidad de ser modificado, esto en el tab de gestión de productos*/
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {

            rb_disminuir.Visibility = Visibility.Visible;
            rb_aumentar.Visibility = Visibility.Visible;
            txb_cantModificar.Visibility = Visibility.Visible;
            lbl_cantModificar.Visibility = Visibility.Visible;
            txb_cantActual.IsEnabled = false;
            btn_limpiar.Visibility = Visibility.Hidden;

            MostrarFormulario();
            DataGridRow row = sender as DataGridRow;

            v_Clt.v_IdProducto = Convert.ToInt64((dtg_productos.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text);

            txb_codProd.Text = (dtg_productos.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text;
            txb_nombre.Text = (dtg_productos.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;
            txb_marca.Text = (dtg_productos.SelectedCells[3].Column.GetCellContent(row) as TextBlock).Text;
            txb_cantActual.Text = (dtg_productos.SelectedCells[4].Column.GetCellContent(row) as TextBlock).Text;
            txb_cantMinima.Text = (dtg_productos.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text;

            //se guarda el id del proveedor que se obtiene del datagrid en la variable: v_Clt.v_IdProveedor
            v_Clt.v_IdProveedor = Convert.ToInt64((dtg_productos.SelectedCells[6].Column.GetCellContent(row) as TextBlock).Text);
            //convertir de tipo long(v_Clt.v_IdProveedor) a tipo int(v_Indice) 
            int v_Indice = unchecked((int)v_Clt.v_IdProveedor);
            //indica al combobox cual opción debe estar seleccionada (asigna el id del proveedor por defecto según el índice que se obtiene del producto seleccionado en el datagrid)
            //se hace la resta v_Indice-1 porque los elementos del combobox inician en 0
            cmb_proveedor.SelectedIndex = v_Indice - 1;


            txb_precio.Text = (dtg_productos.SelectedCells[8].Column.GetCellContent(row) as TextBlock).Text;
            txb_descripcion.Text = (dtg_productos.SelectedCells[9].Column.GetCellContent(row) as TextBlock).Text;
            txb_fabricante.Text = (dtg_productos.SelectedCells[10].Column.GetCellContent(row) as TextBlock).Text;

            //Se compara el_Estado del Producto que se obtiene de la celda con los items contenidos en el combobox(cmb_estadoprod) 
            if ((dtg_productos.SelectedCells[11].Column.GetCellContent(row) as TextBlock).Text == "Nuevo")
            {
                //asigna el item del combobox "Nuevo"
                cmb_estadoprod.SelectedIndex = 0; 
            } else if ((dtg_productos.SelectedCells[11].Column.GetCellContent(row) as TextBlock).Text == "Usado")
            {
                //asigna el item del combobox "Usado"
                cmb_estadoprod.SelectedIndex = 1;
            } else if ((dtg_productos.SelectedCells[11].Column.GetCellContent(row) as TextBlock).Text == "Dañado")
            {
                //asigna el item del combobox "Dañado"
                cmb_estadoprod.SelectedIndex = 2;
            }

            //Se compara el_Estado en el Sistema que se obtiene de la celda para saber si el producto está activo o inactivo
            if ((dtg_productos.SelectedCells[13].Column.GetCellContent(row) as TextBlock).Text == "ACTIVO")
            {
                //asigna el radiobutton en "Activo"
                rb_activo.IsChecked = true;
            }
            else if ((dtg_productos.SelectedCells[13].Column.GetCellContent(row) as TextBlock).Text == "INACTIVO")
            {
                //asigna el radiobutton en "Inactivo"
                rb_inactivo.IsChecked = true;
            }
            
            lbl_actividad.Content = "Modificar producto";
            lbl_actividad.Visibility = Visibility.Visible;
            v_Actividad_btnModificar = true;
            v_Actividad_btnAgregar = false;
            btn_agregar.Visibility = Visibility.Collapsed;

           
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

        private void btn_agregarProveedor_Click(object sender, RoutedEventArgs e)
        {
            lbl_actividad.Content = "Agregar Producto";
            btn_limpiar.Visibility = Visibility.Visible;

            rb_disminuir.Visibility = Visibility.Collapsed;
            rb_aumentar.Visibility = Visibility.Collapsed;
            txb_cantModificar.Visibility = Visibility.Collapsed;
            lbl_cantModificar.Visibility = Visibility.Collapsed;

            MostrarFormulario();
            rb_activo.IsChecked = true;
        }

        private void btn_volver_Click(object sender, RoutedEventArgs e)
        {
            MostrarProductosExistentes();
            btn_limpiar_Click(sender, e);
            txb_cantActual.IsEnabled = true;
        }

        private void MostrarProductosExistentes()
        {
            lbl_actividad.Content = "Productos existentes";
            grd_productosExistentes.Visibility = Visibility.Visible;
            OcultarFormulario();
        }

        //Oculta el panel de búsqueda en el tab de configuración de proveedores
        private void OcultarProveedoresExistentes()
        {
            grd_productosExistentes.Visibility = Visibility.Collapsed;
        }

        //Oculta el panel del formulario en el tab de configuración de proveedores
        private void OcultarFormulario()
        {
            grd_formularioProductos.Visibility = Visibility.Collapsed;
        }

        //Muestra el panel del formulario en el tab de configuración de proveedores
        private void MostrarFormulario()
        {
            grd_formularioProductos.Visibility = Visibility.Visible;
            rb_activo.IsChecked = true;
            OcultarProveedoresExistentes();
            LlenarComboboxProveedores();
            ValidarRadioButton();
        }

        private void ValidarRadioButton()
        {
            if (rb_inactivo.IsChecked == false && rb_activo.IsChecked == false)
            {
                lbl_errorRB.Visibility = Visibility.Visible;
                lbl_errorRB.Content = "Debe seleccionar una opción";
            }
            else
            {
                lbl_errorRB.Visibility = Visibility.Collapsed;
            }
        }



        private void cmb_tipoBusqueda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_tipoBusqueda.SelectedValue == productosActivos)
            {
                v_EstadoSistema = "ACTIVO";
            }
            else if (cmb_tipoBusqueda.SelectedValue == productosInactivos)
            {
                v_EstadoSistema = "INACTIVO";
            }
            else if (cmb_tipoBusqueda.SelectedValue == listaProductos)
            {
                v_EstadoSistema = "LISTAPRODUCTOS";
            }
        }

        private void txb_busqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            //ValidarErroresTxb(txb_busqueda, lbl_errorBusqueda, "");
            if (txb_busqueda.Text.Contains("'"))
            {
                lbl_errorBusqueda.Content = "No se permiten caracteres especiales";
                lbl_errorBusqueda.Visibility = Visibility.Visible;
            }
            else
            {
                //productos existentes
                dtg_productos.ItemsSource = v_Model.ValidarBusquedaProductos(txb_busqueda.Text);
                dtg_productos.Columns[0].Header = "#";
                dtg_productos.Columns[1].Header = "Código del Producto";
                dtg_productos.Columns[2].Header = "Nombre del Producto";
                dtg_productos.Columns[3].Header = "Marca";
                dtg_productos.Columns[4].Header = "Cantidad Actual";
                dtg_productos.Columns[5].Header = "Cantidad Mínima";
                dtg_productos.Columns[6].Header = "Id del Proveedor";
                dtg_productos.Columns[7].Header = "Nombre del Proveedor";
                dtg_productos.Columns[8].Header = "Precio Unitario";
                dtg_productos.Columns[9].Header = "Descripción";
                dtg_productos.Columns[10].Header = "Fabricante";
                dtg_productos.Columns[11].Header = "Estado del Producto";
                dtg_productos.Columns[12].Header = "Fecha";
                dtg_productos.Columns[13].Header = "Estado en el Sistema";

                v_Actividad_btnAgregar = false;
            }
        }

        //Método el cual recibe parametros necesarios para la validacion y la muestra de mensajes de erroes en las cajas de texto
        private void ValidarErroresTxb(TextBox txb_usuario, Label lbl_error, string tipo)
        {
            if (txb_usuario.Text == "")
            {
                lbl_error.Content = "Espacio vacío";
                lbl_error.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_error.Visibility = Visibility.Hidden;
            }
             if (txb_usuario.Text == " ")
            {
                txb_usuario.Text = "";
            }
            else if (txb_usuario.Text.Contains("  "))
            {
                lbl_error.Content = "Parámetros incorrectos (espacios seguidos)";
                lbl_error.Visibility = Visibility.Visible;
            }
            else if (ValidarCaracteresEspeciales(txb_usuario.Text, tipo) == true)
            {
                lbl_error.Content = "No se permiten caracteres especiales";
                lbl_error.Visibility = Visibility.Visible;
            }
        }

        private void txb_codProd_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            ValidaCod(txb_codProd, lbl_errorCodProd);
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_codProd, lbl_errorCodProd, "nombre");

        }

        private void rb_inactivo_Checked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
        }

        private void rb_activo_Checked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
        }

        private void cmb_proveedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
        }

        private void cmb_estadoprod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
        }

        private void txb_nombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_nombre, lbl_errorNombre, "nombre");
        }

        private void txb_marca_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_marca, lbl_errorMarca, "nombre");
        }

        private void txb_fabricante_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_fabricante, lbl_errorFabricante, "nombre");
        }

        private void txb_cantActual_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_cantActual, lbl_errorCantActual, "numeros");
        }

        private void txb_cantMinima_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_cantMinima, lbl_errorCantMinima, "numeros");
        }

        private void txb_precio_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_precio, lbl_errorPrecio, "numeros");
        }

        private void txb_descripcion_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_descripcion, lbl_errorDescripcion, "descripcion");
        }

        private void CantAct_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
            {
                e.Handled = false;
                lbl_errorCantActual.Visibility = Visibility.Hidden;
                if (ValidarCaracteresEspeciales(txb_cantActual.Text, "numeros") == true)
                {
                    lbl_errorCantActual.Content = "No se permiten caracteres especiales";
                    lbl_errorCantActual.Visibility = Visibility.Visible;
                }
            }
            else
            {
                e.Handled = true;
                lbl_errorCantActual.Content = "No se permite ingresar letras";
                lbl_errorCantActual.Visibility = Visibility.Visible;
            }
        }

        private void CantMod_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
            {
                e.Handled = false;
                lbl_errorCantModificar.Visibility = Visibility.Hidden;
                if (ValidarCaracteresEspeciales(txb_cantMinima.Text, "numeros") == true)
                {
                    lbl_errorCantModificar.Content = "No se permiten caracteres especiales";
                    lbl_errorCantModificar.Visibility = Visibility.Visible;
                }
            }
            else
            {
                e.Handled = true;
                lbl_errorCantModificar.Content = "No se permite ingresar letras";
                lbl_errorCantModificar.Visibility = Visibility.Visible;
            }
        }

        private void CantMin_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
            {
                e.Handled = false;
                lbl_errorCantMinima.Visibility = Visibility.Hidden;
                if (ValidarCaracteresEspeciales(txb_cantMinima.Text, "numeros") == true)
                {
                    lbl_errorCantMinima.Content = "No se permiten caracteres especiales";
                    lbl_errorCantMinima.Visibility = Visibility.Visible;
                }
            }
            else
            {
                e.Handled = true;
                lbl_errorCantMinima.Content = "No se permite ingresar letras";
                lbl_errorCantMinima.Visibility = Visibility.Visible;
            }
        }

        private void Precio_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
            {
                e.Handled = false;
                lbl_errorPrecio.Visibility = Visibility.Hidden;
                if (ValidarCaracteresEspeciales(txb_precio.Text, "numeros") == true)
                {
                    lbl_errorPrecio.Content = "No se permiten caracteres especiales";
                    lbl_errorPrecio.Visibility = Visibility.Visible;
                }
            }
            else
            {
                e.Handled = true;
                lbl_errorPrecio.Content = "No se permite ingresar letras";
                lbl_errorPrecio.Visibility = Visibility.Visible;
            }
        }

        public void ValidaCod(TextBox txb_usuario, Label lbl_error)
        {
            String cod = txb_codProd.Text;
            if (v_Model.ValidarCodProductos(cod) == true)
            {
                lbl_error.Content = "Este código ya existe";
                lbl_error.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_error.Visibility = Visibility.Hidden;
            }

        }

        private void txb_cantModificar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            ValidarErroresTxb(txb_cantMinima, lbl_errorCantMinima, "numeros");
        }




    }//Fin de la clase
}
