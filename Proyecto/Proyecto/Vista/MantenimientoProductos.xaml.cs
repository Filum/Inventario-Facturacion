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
        //Creación de variables e instancias
        Model v_Model = new Model();
        EntidadProductos v_Clt = new EntidadProductos();
        EntidadBitacora bitacora = new EntidadBitacora();
        bool v_Actividad_btnModificar = false;
        bool v_Actividad_btnAgregar = true;
        String v_EstadoSistema = "";
        public string v_NombreUsuario;
        long v_NuevaCantidad, v_CantActual, v_CantModificar;

        //Constructor de la Clase. Se cargan los productos en el datagrid del tab "Listar" y se crea el hilo que se usará para mostrar la hora al usuario.
        public MantenimientoProductos()
        {
            InitializeComponent();
            //Formato para la hora.
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            MostrarProductosExistentes();

            //Cargar productos existentes.
            cmb_tipoBusqueda.SelectedIndex = 2;
            dtg_lista.ItemsSource = v_Model.MostrarListaProductos("LISTAPRODUCTOS").DefaultView;
        }

        //Se asigna la hora al label de la ventana por medio del hilo creado.
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            txt_fecha.Content = DateTime.Now.ToString();
        }

        //Barra en el área superior de la ventana, la cual permite deslizarla de un lugar a otro.
        private void barra_movil__MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                this.WindowStyle = WindowStyle.None;
                WindowState = WindowState.Normal;
            }

            this.DragMove();
        }

        //EVENTOS DE LOS BOTONES.

        //Minimiza ventana.
        private void btn_minimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        //Cierra ventana.
        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Maximiza ventana.
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

        //Botón el cual permite devolverse a la ventana "Menú".
        private void btn_menu_Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.cargarMenu(v_NombreUsuario);
            ventana.nombreUser = v_NombreUsuario;
            this.Close();
        }

        //Botón el cual brinda con información necesaria para la utilización de la ventana en la que se encuentra el usuario.
        private void btn_productos_ayuda_Click(object sender, RoutedEventArgs e)
        {
            Vista.Ayuda ventana = new Vista.Ayuda();
            ventana.Show();
            ventana.Pantalla = "Productos";
        }

        //Presenta formulario para agregar un nuevo producto y oculta el panel de búsqueda, esto en el tab de configuración de productos.
        private void btn_agregarProducto_Click(object sender, RoutedEventArgs e)
        {
            lbl_actividad.Content = "Agregar producto";
            btn_limpiar.Visibility = Visibility.Visible;
            btn_limpiar_Click(sender, e);

            rb_disminuir.Visibility = Visibility.Collapsed;
            rb_aumentar.Visibility = Visibility.Collapsed;
            txb_cantModificar.Visibility = Visibility.Collapsed;
            lbl_cantModificar.Visibility = Visibility.Collapsed;

            MostrarFormulario();
            rb_activo.IsChecked = true;
            txb_cantActual.IsEnabled = true;
        }

        //Vuelve al panel de búsqueda en el tab de configuración de productos y oculta el formulario.
        private void btn_volver_Click(object sender, RoutedEventArgs e)
        {
            MostrarProductosExistentes();
            btn_limpiar_Click(sender, e);
        }

        //Botón el cual permite agregar un nuevo producto, este botón posee las validaciones necesarias para la ejecución de su funcionalidad.
        private void btn_agregar_Click(object sender, RoutedEventArgs e)
        {
            if ((lbl_errorCodProd.Visibility == Visibility.Visible) || (lbl_errorNombre.Visibility == Visibility.Visible) || (lbl_errorCantActual.Visibility == Visibility.Visible) ||
                    (lbl_errorCantMinima.Visibility == Visibility.Visible) || (lbl_errorDescripcion.Visibility == Visibility.Visible) || lbl_errorestadoProd.Visibility == Visibility.Visible || lbl_errorFabricante.Visibility == Visibility.Visible ||
                    lbl_errorMarca.Visibility == Visibility.Visible || lbl_errorPrecio.Visibility == Visibility.Visible || lbl_errorProveedor.Visibility == Visibility.Visible ||
                    lbl_errorRB.Visibility == Visibility.Visible)
            {
                MessageBox.Show("Error al agregar\nHacen falta campos por rellenar o errores que corregir", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    //Obtiene los datos del formulario y los asigna a la Entidad Producto.
                    v_Clt.v_CodigoProducto = txb_codProd.Text;
                    v_Clt.v_NombreProducto = txb_nombre.Text;
                    v_Clt.v_MarcaProducto = txb_marca.Text;

                    if (txb_cantActual.Text == "")
                    {
                        v_Clt.v_CantidadExistencia = 1;
                    }
                    else
                    {
                        v_Clt.v_CantidadExistencia = Convert.ToInt64(txb_cantActual.Text);
                    }

                    if (txb_cantMinima.Text == "")
                    {
                        v_Clt.v_CantidadMinima = 1;
                    }
                    else
                    {
                        v_Clt.v_CantidadMinima = Convert.ToInt64(txb_cantMinima.Text);
                    }

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

                   
                    if (v_Resultado == -1)
                    {
                        MessageBox.Show("Datos ingresados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Se agrega a la bitácora la información respectiva acerca de la agregación del producto.
                        bitacora.accion = "Agregar";
                        bitacora.usuario_Responsable = usuario_productos.Text;
                        bitacora.ventana_Mantenimiento = "Mantenimiento Productos";
                        bitacora.descripcion = "Agregó el producto con el código: " + txb_codProd.Text + ", con el nombre: " + txb_nombre.Text;
                        v_Model.AgregarBitacora(bitacora);

                        btn_limpiar_Click(sender, e);
                        MostrarProductosExistentes();

                        //Se actualiza el tap "Listar".
                        v_EstadoSistema = "LISTAPRODUCTOS";
                        ListarProductos();
                    }
                }
                catch (Exception m)
                {
                    Console.WriteLine(m.ToString());
                    MessageBox.Show("Error al agregar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }

        }

        //Botón el cual permite modificar un producto seleccionado, este botón posee las validaciones necesarias para la ejecución de su funcionalidad.
        private void btn_modificar_Click(object sender, RoutedEventArgs e)
        {
            if ((lbl_errorNombre.Visibility == Visibility.Visible) || (lbl_errorCantActual.Visibility == Visibility.Visible) ||
                    (lbl_errorCantMinima.Visibility == Visibility.Visible)|| (lbl_errorDescripcion.Visibility == Visibility.Visible) || lbl_errorestadoProd.Visibility == Visibility.Visible || lbl_errorFabricante.Visibility == Visibility.Visible ||
                    lbl_errorMarca.Visibility == Visibility.Visible || lbl_errorPrecio.Visibility == Visibility.Visible || lbl_errorProveedor.Visibility == Visibility.Visible ||
                    lbl_errorRB.Visibility == Visibility.Visible || lbl_errorCodProd.Visibility == Visibility.Visible)
            {
                MessageBox.Show("Error al modificar\nHacen falta campos por rellenar o errores que corregir", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    //Obtiene los datos del formulario y los asigna a la Entidad Producto.
                    v_Clt.v_CodigoProducto = txb_codProd.Text;
                    v_Clt.v_NombreProducto = txb_nombre.Text;
                    v_Clt.v_MarcaProducto = txb_marca.Text;
                    v_CantActual = Convert.ToInt64(txb_cantActual.Text);

                    if (txb_cantModificar.Text != "")
                    {
                        v_CantModificar = Convert.ToInt64(txb_cantModificar.Text);

                        if (rb_aumentar.IsChecked == true)
                        {
                            v_NuevaCantidad = v_CantActual + v_CantModificar;
                            v_Clt.v_CantidadExistencia = v_NuevaCantidad;
                            lbl_errorCantModificar.Visibility = Visibility.Hidden;
                        }
                        else if (rb_disminuir.IsChecked == true)
                        {
                            if (v_CantActual < v_CantModificar)
                            {
                                lbl_errorCantModificar.Visibility = Visibility.Visible;
                                lbl_errorCantModificar.Content = "La cant. actual es inferior a la cant. a disminuir";
                            }
                            else
                            {
                                v_NuevaCantidad = v_CantActual - v_CantModificar;
                                v_Clt.v_CantidadExistencia = v_NuevaCantidad;
                            }
                        }
                        else
                        {
                            v_Clt.v_CantidadExistencia = v_CantActual;
                        }
                    }

                    v_Clt.v_CantidadMinima = Convert.ToInt64(txb_cantMinima.Text);
                    EntidadProveedores ComboItem = (EntidadProveedores)cmb_proveedor.SelectedItem;
                    Int64 idProveedor = ComboItem.v_IdProveedor;
                    v_Clt.v_IdProveedor = idProveedor;
                    v_Clt.v_PrecioUnitario = Convert.ToInt64(txb_precio.Text);
                    v_Clt.v_Descripcion = txb_descripcion.Text;
                    v_Clt.v_Fabricante = txb_fabricante.Text;
                    if (cmb_estadoprod.SelectedIndex == 0){
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

                    if (lbl_errorCantModificar.Visibility == Visibility.Visible)
                    {
                        MessageBox.Show("Error al modificar\nHacen falta campos por rellenar o errores que corregir", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (v_Model.ModificarProductos(v_Clt) == -1)
                    {
                        MessageBox.Show("Datos modificados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Se agrega a la bitácora la información respectiva acerca de la modificación del producto.
                        bitacora.accion = "Modificar";
                        bitacora.usuario_Responsable = usuario_productos.Text;
                        bitacora.ventana_Mantenimiento = "Mantenimiento Productos";
                        bitacora.descripcion = "Modificó el producto con el código: " + txb_codProd.Text + ", con el nombre: " + txb_nombre.Text;
                        v_Model.AgregarBitacora(bitacora);

                        btn_limpiar_Click(sender, e);
                        v_Actividad_btnAgregar = true;
                        MostrarProductosExistentes();

                        //Se actualiza el tap "Listar".
                        v_EstadoSistema = "LISTAPRODUCTOS";
                        ListarProductos();
                    }
                }
                catch (Exception m)
                {
                    Console.WriteLine(m.ToString());
                    MessageBox.Show("Error al modificar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }

        //Botón el cual cumple con la funcionalidad de eliminar todos los datos existentes en los componentes situadas en el tab de configuración de productos.
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
            rb_inactivo.IsChecked = false;
            dtg_productos.ItemsSource = null;
            lbl_errorCodProd.Visibility = Visibility.Hidden;
            lbl_errorBusqueda.Visibility = Visibility.Hidden;
            lbl_errorNombre.Visibility = Visibility.Hidden;
            lbl_errorMarca.Visibility = Visibility.Hidden;
            lbl_errorCantModificar.Visibility = Visibility.Hidden;
            lbl_errorCantMinima.Visibility = Visibility.Hidden;
            lbl_errorCantActual.Visibility = Visibility.Hidden;
            lbl_errorDescripcion.Visibility = Visibility.Hidden;
            lbl_errorFabricante.Visibility = Visibility.Hidden;
            lbl_errorPrecio.Visibility = Visibility.Hidden;
            btn_agregar.Visibility = Visibility.Collapsed;
            btn_modificar.Visibility = Visibility.Collapsed;
            v_Actividad_btnModificar = false;
            v_Actividad_btnAgregar = true;
            rb_aumentar.IsChecked = false;
            rb_disminuir.IsChecked = false;
        }

        //Botón el cual permite al usuario cerrar la sesión en la que se encuentra, y lo redirecciona a la ventana de Login.
        private void btn_usuarios_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult v_Result = MessageBox.Show("¿Desea cerrar sesión?", "Cerrar Sesión", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (v_Result == MessageBoxResult.Yes)
            {
                Login v_Ventana = new Login();
                this.Close();
                v_Ventana.Show();
            }
        }

        //Botón que permite llamar a la clase para imprimir, tomando la tabla con información respectiva.
        private void btn_imprimir_Click(object sender, RoutedEventArgs e)
        {
            Imprimir print = new Imprimir();
            print.imprimir(dtg_lista, "Productos en Inventario");

        }

        //MÉTODOS FUNCIONALIDAD.

        /*Llena el combobox con los proveedores existentes obtenidos por medio de una consulta a la base de datos.
        Se obtiene el id y el nombre del proveedor y se asigna a la "EntidadProveedores" para así establecerlo en las propiedades del combobox. */
        private void LlenarComboboxProveedores()
        {
            var v_ProveedoresExistentes = new List<EntidadProveedores>();
            v_ProveedoresExistentes = v_Model.ProveedoresExistentes();
            cmb_proveedor.ItemsSource = v_ProveedoresExistentes;
            cmb_proveedor.DisplayMemberPath = "v_Nombre";
            cmb_proveedor.SelectedValuePath = "v_IdProveedor";
        }

        /*Lista los productos existentes en el sistema según su estado, envía el estado en el sistema(v_EstadoSistema) que se 
         * obtiene del combobox "cmb_tipoBusqueda" con el cual se realizará la consulta.*/
        private void ListarProductos()
        {
            if (v_Model.MostrarListaProductos(v_EstadoSistema).Rows.Count == 0)
            {
                MessageBox.Show("No hay datos registrados", "Búsqueda", MessageBoxButton.OK, MessageBoxImage.Warning);
                v_EstadoSistema = "LISTAPRODUCTOS";
                cmb_tipoBusqueda.SelectedIndex = 2;
            }
            else
            {
                dtg_lista.ItemsSource = v_Model.MostrarListaProductos(v_EstadoSistema).DefaultView;
            }
        }

        /*Método el cual habilita el botón agregar, siempre y cuando los espacios correspondientes para esta actividad no estén vacíos,  
        esto se permite solamente si la bandera "v_Actividad_btnAgregar" está encendida "true".*/
        private void HabilitarBtnAgregar()
        {
            if (v_Actividad_btnAgregar == true)
            {
                if (txb_codProd.Text == "" || txb_nombre.Text == "" || txb_marca.Text == "" || cmb_estadoprod.Text == "" || txb_precio.Text == "" || cmb_proveedor.Text == "" || txb_descripcion.Text == "" || txb_fabricante.Text == "")
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

        //Método el cual habilita el botón modificar, esto se permite solamente si la bandera "v_Actividad_btnModificar" está encendida "true".
        private void HabilitarBtnModificar()
        {
            if (v_Actividad_btnModificar == true)
            {
                btn_modificar.Visibility = Visibility.Visible;
            }
        }

        /*Método el cual cumple con la funcionalidad de desplegar los datos en los campos correspondientes de un producto seleccionado 
        en el DataGrid con la finalidad de ser modificado, esto en el panel del formulario en el tab de configuración de productos.*/
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Hace visible los componentes respectivos a la modificación de cantidades de productos.
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

            //Se guarda el id del proveedor que se obtiene del datagrid en la variable: v_Clt.v_IdProveedor y se asigna al combobox mediante la propiedad "SelectedValue".
            v_Clt.v_IdProveedor = Convert.ToInt64((dtg_productos.SelectedCells[6].Column.GetCellContent(row) as TextBlock).Text);
            cmb_proveedor.SelectedValue = v_Clt.v_IdProveedor;

            txb_precio.Text = (dtg_productos.SelectedCells[8].Column.GetCellContent(row) as TextBlock).Text;
            txb_descripcion.Text = (dtg_productos.SelectedCells[9].Column.GetCellContent(row) as TextBlock).Text;
            txb_fabricante.Text = (dtg_productos.SelectedCells[10].Column.GetCellContent(row) as TextBlock).Text;

            //Se compara el_Estado del Producto que se obtiene de la celda con los items contenidos en el combobox(cmb_estadoprod). 
            if ((dtg_productos.SelectedCells[11].Column.GetCellContent(row) as TextBlock).Text == "Nuevo")
            {
                //Asigna el item del combobox "Nuevo".
                cmb_estadoprod.SelectedIndex = 0; 
            } else if ((dtg_productos.SelectedCells[11].Column.GetCellContent(row) as TextBlock).Text == "Usado")
            {
                //Asigna el item del combobox "Usado".
                cmb_estadoprod.SelectedIndex = 1;
            } else if ((dtg_productos.SelectedCells[11].Column.GetCellContent(row) as TextBlock).Text == "Dañado")
            {
                //Asigna el item del combobox "Dañado".
                cmb_estadoprod.SelectedIndex = 2;
            }

            //Se compara el_Estado en el Sistema que se obtiene de la celda para saber si el producto está activo o inactivo.
            if ((dtg_productos.SelectedCells[13].Column.GetCellContent(row) as TextBlock).Text == "ACTIVO")
            {
                //Asigna el radiobutton en "Activo".
                rb_activo.IsChecked = true;
            }
            else if ((dtg_productos.SelectedCells[13].Column.GetCellContent(row) as TextBlock).Text == "INACTIVO")
            {
                //Asigna el radiobutton en "Inactivo".
                rb_inactivo.IsChecked = true;
            }
            
            lbl_actividad.Content = "Modificar producto";
            lbl_actividad.Visibility = Visibility.Visible;
            v_Actividad_btnModificar = true;
            v_Actividad_btnAgregar = false;
            btn_agregar.Visibility = Visibility.Collapsed;
        }

        //Muestra el panel de búsqueda en el tab de configuración de productos.
        private void MostrarProductosExistentes()
        {
            lbl_actividad.Content = "Productos existentes";
            lbl_actividad.Visibility = Visibility.Visible;
            grd_productosExistentes.Visibility = Visibility.Visible;
            OcultarFormulario();
        }

        //Oculta el panel de búsqueda en el tab de configuración de productos.
        private void OcultarProductosExistentes()
        {
            grd_productosExistentes.Visibility = Visibility.Collapsed;
        }

        //Oculta el panel del formulario en el tab de configuración de productos.
        private void OcultarFormulario()
        {
            grd_formularioProductos.Visibility = Visibility.Collapsed;
        }

        //Muestra el panel del formulario en el tab de configuración de productos.
        private void MostrarFormulario()
        {
            grd_formularioProductos.Visibility = Visibility.Visible;
            rb_activo.IsChecked = true;
            OcultarProductosExistentes();
            LlenarComboboxProveedores();
        }

        //EVENTOS DE LOS COMPONENTES.

        /*Recibe el filtro por el cual el usuario desea realizar el listado de productos y se lo asigna a la variable "v_EstadoSistema" 
        para que se envie al Data mediante el método ListarProductos() y se realice la consulta respectiva.*/
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

            ListarProductos();
        }

        /*Se implementa la búsqueda del producto que se desea, en caso de ser encontrado se despliega los datos en el DataGrid.
        Valida el contenido del txb_busqueda para que no se envie con errores al momento de realizar la consulta en la BD. */
        private void txb_busqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarErroresTxb(txb_busqueda, lbl_errorBusqueda, "");
            if (txb_busqueda.Text.Contains("'"))
            {
                lbl_errorBusqueda.Content = "No se permiten caracteres especiales";
                lbl_errorBusqueda.Visibility = Visibility.Visible;
            }
            else
            {
                //Productos existentes.
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

        /*Validaciones en caja de texto de código de producto.
        Valida que el contenido ingresado sea de la manera correcta, en caso contrario se muestran mensajes indicandole al usuario los errores.*/
        private void txb_codProd_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_codProd, lbl_errorCodProd, "nombre");
        }

        /*Validaciones en caja de texto de nombre.
        Valida que el contenido ingresado sea de la manera correcta, en caso contrario se muestran mensajes indicandole al usuario los errores.*/
        private void txb_nombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_nombre, lbl_errorNombre, "nombre");
        }

        /*Validaciones en caja de texto de marca.
        Valida que el contenido ingresado sea de la manera correcta, en caso contrario se muestran mensajes indicandole al usuario los errores.*/
        private void txb_marca_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_marca, lbl_errorMarca, "nombre");
        }

        //Validaciones en combobox de proveedor.
        private void cmb_proveedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
        }

        //Validaciones en combobox de estado del proucto.
        private void cmb_estadoprod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
        }

        /*Validaciones en caja de texto de fabricante.
        Valida que el contenido ingresado sea de la manera correcta, en caso contrario se muestran mensajes indicandole al usuario los errores.*/
        private void txb_fabricante_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_fabricante, lbl_errorFabricante, "nombre");
        }

        /*Validaciones en caja de texto de cantidad actual.
        Valida que el contenido ingresado sea de la manera correcta, en caso contrario se muestran mensajes indicandole al usuario los errores.*/
        private void txb_cantActual_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_cantActual, lbl_errorCantActual, "numeros");
            if (txb_cantActual.Text == "")
            {
                lbl_errorCantActual.Visibility = Visibility.Hidden;
            }
        }

        /*Validaciones en caja de texto de cantidad mínima.
        Valida que el contenido ingresado sea de la manera correcta, en caso contrario se muestran mensajes indicandole al usuario los errores.*/
        private void txb_cantMinima_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_cantMinima, lbl_errorCantMinima, "numeros");
            if (txb_cantMinima.Text == "")
            {
                lbl_errorCantMinima.Visibility = Visibility.Hidden;
            }
        }

        /*Validaciones en caja de texto de cantidad a modificar.
        Valida que el contenido ingresado sea de la manera correcta, en caso contrario se muestran mensajes indicandole al usuario los errores.*/
        private void txb_cantModificar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            ValidarErroresTxb(txb_cantMinima, lbl_errorCantMinima, "numeros");
        }

        /*Validaciones en caja de texto de precio.
        Valida que el contenido ingresado sea de la manera correcta, en caso contrario se muestran mensajes indicandole al usuario los errores.*/
        private void txb_precio_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_precio, lbl_errorPrecio, "numeros");
        }

        /*Validaciones en caja de texto de descripción.
        Valida que el contenido ingresado sea de la manera correcta, en caso contrario se muestran mensajes indicandole al usuario los errores.*/
        private void txb_descripcion_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_descripcion, lbl_errorDescripcion, "descripcion");
        }

        //Validación en la actividad del radiobutton Activo. Al seleccionarse se llaman los metodos respectivos para las actividades de los botones. 
        private void rb_activo_Checked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
        }

        //Validación en la actividad del radiobutton Inactivo. Al seleccionarse se llaman los metodos respectivos para las actividades de los botones. 
        private void rb_inactivo_Checked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
        }

        //VALIDACIONES.

        //Validación en caja de texto de cantidad actual. Bloquea el textbox para que no se pueda ingresar letras.
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

        //Validación en caja de texto de cantidad a modificar. Bloquea el textbox para que no se pueda ingresar letras.
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

        //Validación en caja de texto de cantidad mínima. Bloquea el textbox para que no se pueda ingresar letras.
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

        //Validación en caja de texto de precio. Bloquea el textbox para que no se pueda ingresar letras.
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

        //Valida que se seleccione una opción en los combobox de estado de producto y proveedores.
        private void ValidarComponentes()
        {
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

        /*Método el cual valida si la caja de texto recibida contiene caracteres especiales.
         *Recibe como parámetro el textbox y un identificador con el cual se realizará la validación, este puede ser: numeros, nombre o descripcion.*/
        private Boolean ValidarCaracteresEspeciales(String v_Txb, String v_Identificador)
        {
            if (v_Identificador == "numeros")
            {
                //Caracteres que no permite si la cadena es de int.
                String v_Caracteres = "[a-zA-Z !@#$%^&*())+=.,<>{}¬º´/\"':;|ñÑ~¡?`¿-]";
                if (Regex.IsMatch(v_Txb, v_Caracteres))
                {
                    return true;
                }
            }
            else if (v_Identificador == "nombre")
            {
                //Caracteres que no permite si la cadena es de string.
                String v_Caracteres = "[!@#$%^*())+=.,<>{}¬º´/\"':;|~¡?`¿-]";
                if (Regex.IsMatch(v_Txb, v_Caracteres))
                {
                    return true;
                }
            }
            else if (v_Identificador == "descripcion")
            {
                //Caracteres que no permite si la cadena es de string.
                String v_Caracteres = "[!@#$%^*())+=.<>{}¬º´/\"':;|~¡?`¿]";
                if (Regex.IsMatch(v_Txb, v_Caracteres))
                {
                    return true;
                }
            }
            return false;
        }

        /*Método el cual recibe 3 parametros para llevar a cabo las validaciones necesarias. 
         * Recibe el textbox a validar, el label de error respectivo y la cadena "tipo" con la que se llamará al método 
         * ValidarCaracteresEspeciales(tipo) para verificar si el contenido posee caracteres no permitidos.*/
        private void ValidarErroresTxb(TextBox txb_producto, Label lbl_error, string tipo)
        {
            if (txb_producto.Text == "")
            {
                lbl_error.Content = "Espacio vacío";
                lbl_error.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_error.Visibility = Visibility.Hidden;
            }
            if (txb_producto.Text == " ")
            {
                txb_producto.Text = "";
            }
            else if (txb_producto.Text.Contains("  "))
            {
                lbl_error.Content = "Parámetros incorrectos (espacios seguidos)";
                lbl_error.Visibility = Visibility.Visible;
            }
            else if (ValidarCaracteresEspeciales(txb_producto.Text, tipo) == true)
            {
                lbl_error.Content = "No se permiten caracteres especiales";
                lbl_error.Visibility = Visibility.Visible;
            }
        }

    }//Fin de la clase.
}//Fin del proyecto.
