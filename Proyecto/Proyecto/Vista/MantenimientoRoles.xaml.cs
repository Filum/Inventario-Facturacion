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
using System.Text.RegularExpressions;
using Entidades;
using Logica;

namespace Proyecto
{
    /// <summary>
    /// Lógica de interacción para MantenimientoRoles.xaml
    /// </summary>
    public partial class MantenimientoRoles : Window
    {
        //Creación de variables e instancias
        EntidadRoles v_Ctl = new EntidadRoles();
        Model v_Model = new Model();
        EntidadBitacora bitacora = new EntidadBitacora();
        String v_EstadoSistema = "";
        bool v_Actividad_btnModificar = false;
        bool v_Actividad_btnAgregar = true;

        public string nombreUsuario;

        //Constructor de la Clase. Se cargan los roles en el datagrid del tab "Listar" y se crea el hilo que se usará para mostrar la hora al usuario.
        public MantenimientoRoles()
        {
            InitializeComponent();
            //Formato para la hora.
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            MostrarRolesExistentes();

            //Cargar roles existentes.
            cmb_tipoBusqueda.SelectedIndex = 2;
            dtg_lista.ItemsSource = v_Model.MostrarListaRoles("LISTAROLES").DefaultView;
        }

        //Se asigna la hora al label de la ventana por medio del hilo creado.
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            lbl_fecha.Content = DateTime.Now.ToString();
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

        //Cierra Ventana.
        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Maximiza Ventana.
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
            Menu Ventana = new Menu();
            Ventana.cargarMenu(nombreUsuario);
            Ventana.nombreUser = nombreUsuario;
            this.Close();
        }

        //Botón el cual brinda con información necesaria para la utilización de la ventana en la que se encuentra el usuario.
        private void btn_ayuda_Click(object sender, RoutedEventArgs e)
        {
            Vista.Ayuda ventana = new Vista.Ayuda();
            ventana.Show();
            ventana.Pantalla = "Roles";
        }

        //Presenta formulario para agregar un nuevo rol y oculta el panel de búsqueda, esto en el tab de configuración de roles.
        private void btn_agregarRol_Click(object sender, RoutedEventArgs e)
        {
            lbl_errorNombre.Visibility = Visibility.Collapsed;
            lbl_errorCB.Visibility = Visibility.Collapsed;
            btn_limpiar.Visibility = Visibility.Visible;
            lbl_actividad2.Content = "Agregar Rol";
            rb_activo.IsChecked = true;
            btn_limpiar_Click(sender, e);
            MostrarFormulario();
        }

        //Vuelve al panel de búsqueda en el tab de configuración de roles y oculta el formulario.
        private void btn_volver(object sender, RoutedEventArgs e)
        {
            btn_limpiar_Click(sender, e);
            MostrarRolesExistentes();
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

        //Botón el cual permite agregar un nuevo rol, este botón posee las validaciones necesarias para la ejecución de su funcionalidad.
        private void btn_agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidarComponentes();
                if (lbl_errorNombre.Visibility == Visibility.Visible || lbl_errorCB.Visibility == Visibility.Visible)
                {
                    MessageBox.Show("No se puede agregar un rol nuevo \nHay errores por corregir.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    //Obtiene los datos del formulario y los asigna a la Entidad Rol.
                    v_Ctl.v_Nombre = txb_nombre.Text;
                    if (checkbox_mant_clientes.IsChecked == false)
                    {
                        v_Ctl.v_Mantenimiento_Clientes = "X";
                    }
                    else
                    {
                        v_Ctl.v_Mantenimiento_Clientes = "✓";
                    }
                    if (checkbox_mant_proveedores.IsChecked == false)
                    {
                        v_Ctl.v_Mantenimiento_Proveedores = "X";
                    }
                    else
                    {
                        v_Ctl.v_Mantenimiento_Proveedores = "✓";
                    }
                    if (checkbox_mant_productos.IsChecked == false)
                    {
                        v_Ctl.v_Mantenimiento_Productos = "X";
                    }
                    else
                    {
                        v_Ctl.v_Mantenimiento_Productos = "✓";
                    }
                    if (checkbox_mant_usuarios.IsChecked == false)
                    {
                        v_Ctl.v_Mantenimiento_Usuarios = "X";
                    }
                    else
                    {
                        v_Ctl.v_Mantenimiento_Usuarios = "✓";
                    }
                    if (checkbox_mant_roles.IsChecked == false)
                    {
                        v_Ctl.v_Mantenimiento_Roles = "X";
                    }
                    else
                    {
                        v_Ctl.v_Mantenimiento_Roles = "✓";
                    }
                    if (checkbox_facturacion.IsChecked == false)
                    {
                        v_Ctl.v_facturacion = "X";
                    }
                    else
                    {
                        v_Ctl.v_facturacion = "✓";
                    }
                    if (checkbox_bitacora.IsChecked == false)
                    {
                        v_Ctl.v_bitacora = "X";
                    }
                    else
                    {
                        v_Ctl.v_bitacora = "✓";
                    }
                    if (rb_inactivo.IsChecked == true)
                    {
                        v_Ctl.v_Estado = "INACTIVO";
                    }
                    else
                    {
                        v_Ctl.v_Estado = "ACTIVO";
                    }

                    int v_Resultado = v_Model.AgregarRoles(v_Ctl);
                    if (v_Resultado == -1)
                    {
                        MessageBox.Show("Datos ingresados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Se agrega a la bitácora la información respectiva acerca de la agregación del rol.
                        bitacora.usuario_Responsable = usuario_roles.Text;
                        bitacora.accion = "Agregar";
                        bitacora.ventana_Mantenimiento = "Mantenimiento Roles";
                        bitacora.descripcion = "Agregó el rol con el nombre: " + txb_nombre.Text;
                        v_Model.AgregarBitacora(bitacora);

                        lbl_errorNombre.Visibility = Visibility.Collapsed;
                        lbl_errorCB.Visibility = Visibility.Collapsed;
                        MostrarRolesExistentes();
                        txb_busqueda.Text = "";
                        btn_limpiar_Click(sender, e);

                        //Se actualiza el tap "Listar".
                        v_EstadoSistema = "LISTAROLES";
                        ListarRoles();
                    }
                }
                
            }
            catch (Exception m)
            {
                Console.WriteLine(m.ToString());
                MessageBox.Show("Error al agregar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Botón el cual permite modificar un rol seleccionado, este botón posee las validaciones necesarias para la ejecución de su funcionalidad.
        private void btn_modificar_Click(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();

            if (lbl_errorNombre.Visibility == Visibility.Visible || lbl_errorCB.Visibility == Visibility.Visible)
            {
                MessageBox.Show("No se puede agregar un rol nuevo \nHay errores por corregir.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    //Se agrega a la bitácora la información respectiva acerca de la agregación del rol.
                    v_Ctl.v_Nombre = txb_nombre.Text;
                    if (checkbox_mant_clientes.IsChecked == false)
                    {
                        v_Ctl.v_Mantenimiento_Clientes = "X";
                    }
                    else
                    {
                        v_Ctl.v_Mantenimiento_Clientes = "✓";
                    }
                    if (checkbox_mant_proveedores.IsChecked == false)
                    {
                        v_Ctl.v_Mantenimiento_Proveedores = "X";
                    }
                    else
                    {
                        v_Ctl.v_Mantenimiento_Proveedores = "✓";
                    }
                    if (checkbox_mant_productos.IsChecked == false)
                    {
                        v_Ctl.v_Mantenimiento_Productos = "X";
                    }
                    else
                    {
                        v_Ctl.v_Mantenimiento_Productos = "✓";
                    }
                    if (checkbox_mant_usuarios.IsChecked == false)
                    {
                        v_Ctl.v_Mantenimiento_Usuarios = "X";
                    }
                    else
                    {
                        v_Ctl.v_Mantenimiento_Usuarios = "✓";
                    }
                    if (checkbox_mant_roles.IsChecked == false)
                    {
                        v_Ctl.v_Mantenimiento_Roles = "X";
                    }
                    else
                    {
                        v_Ctl.v_Mantenimiento_Roles = "✓";
                    }
                    if (checkbox_facturacion.IsChecked == false)
                    {
                        v_Ctl.v_facturacion = "X";
                    }
                    else
                    {
                        v_Ctl.v_facturacion = "✓";
                    }
                    if (checkbox_bitacora.IsChecked == false)
                    {
                        v_Ctl.v_bitacora = "X";
                    }
                    else
                    {
                        v_Ctl.v_bitacora = "✓";
                    }
                    if (rb_inactivo.IsChecked == true)
                    {
                        v_Ctl.v_Estado = "INACTIVO";
                    }
                    else
                    {
                        v_Ctl.v_Estado = "ACTIVO";
                    }

                    if (v_Model.ModificarRoles(v_Ctl) == -1)
                    {
                        MessageBox.Show("Datos modificados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        
                        //Se agrega a la bitácora la información respectiva acerca de la modificación del rol.
                        bitacora.usuario_Responsable = usuario_roles.Text;
                        bitacora.accion = "Modificar";
                        bitacora.ventana_Mantenimiento = "Mantenimiento Roles";
                        bitacora.descripcion = "Modificó el rol con el nombre: " + txb_nombre.Text;
                        v_Model.AgregarBitacora(bitacora);
                        
                        btn_limpiar_Click(sender, e);
                        v_Actividad_btnAgregar = true;
                        MostrarRolesExistentes();
                        txb_busqueda.Text = "";

                        //Se actualiza el tap "Listar".
                        v_EstadoSistema = "LISTAROLES";
                        ListarRoles();
                    }
                }
                catch (Exception m)
                {
                    Console.WriteLine(m.ToString());
                    MessageBox.Show("Error al modificar\nHacen falta campos por rellenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }

        //Botón el cual cumple con la funcionalidad de eliminar todos los datos existentes en los componentes situadas en el tab de configuración de roles.
        private void btn_limpiar_Click(object sender, RoutedEventArgs e)
        {
            checkbox_bitacora.IsChecked = false;
            checkbox_facturacion.IsChecked = false;
            checkbox_mant_proveedores.IsChecked = false;
            checkbox_mant_productos.IsChecked = false;
            checkbox_mant_clientes.IsChecked = false;
            checkbox_mant_roles.IsChecked = false;
            checkbox_mant_usuarios.IsChecked = false;
            rb_activo.IsChecked = true;
            rb_inactivo.IsChecked = false;
            txb_busqueda.Text = "";
            txb_nombre.Text = "";
            lbl_errorNombre.Content = "";
            lbl_errorCB.Content = "";
            lbl_errorNombre.Visibility = Visibility.Collapsed;
            lbl_errorCB.Visibility = Visibility.Collapsed;
            dtg_roles.ItemsSource = null;
            btn_agregar.Visibility = Visibility.Collapsed;
            btn_modificar.Visibility = Visibility.Collapsed;
            v_Actividad_btnModificar = false;
            v_Actividad_btnAgregar = true;
        }

        //MÉTODOS FUNCIONALIDAD

        /*Lista los roles existentes en el sistema según su estado, envía el estado en el sistema (v_EstadoSistema) que se 
         * obtiene del combobox "cmb_tipoBusqueda" con el cual se realizará la consulta.*/
        private void ListarRoles()
        {
            if (v_Model.MostrarListaRoles(v_EstadoSistema).Rows.Count == 0)
            {
                MessageBox.Show("No hay datos registrados", "Búsqueda", MessageBoxButton.OK, MessageBoxImage.Warning);
                v_EstadoSistema = "LISTAROLES";
                cmb_tipoBusqueda.SelectedIndex = 2;
            }
            else
            {
                dtg_lista.ItemsSource = v_Model.MostrarListaRoles(v_EstadoSistema).DefaultView;
            }
        }

        /*Método el cual cumple con la funcionalidad de desplegar los datos en los campos correspondientes de un rol seleccionado 
        en el DataGrid con la finalidad de ser modificado, esto en el panel del formulario en el tab de configuración de roles. */
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;

            v_Ctl.v_IdRol = Convert.ToInt64((dtg_roles.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text);

            txb_nombre.Text = (dtg_roles.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text;
            if ((dtg_roles.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text == "ACTIVO")
            {
                rb_activo.IsChecked = true;
            }
            else if ((dtg_roles.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text == "INACTIVO")
            {
                rb_inactivo.IsChecked = true;
            }

            if ((dtg_roles.SelectedCells[3].Column.GetCellContent(row) as TextBlock).Text == "✓")
            {
                checkbox_mant_clientes.IsChecked = true;
            }
            if ((dtg_roles.SelectedCells[4].Column.GetCellContent(row) as TextBlock).Text == "✓")
            {
                checkbox_mant_proveedores.IsChecked = true;
            }
            if ((dtg_roles.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text == "✓")
            {
                checkbox_mant_productos.IsChecked = true;
            }
            if ((dtg_roles.SelectedCells[6].Column.GetCellContent(row) as TextBlock).Text == "✓")
            {
                checkbox_mant_usuarios.IsChecked = true;
            }
            if ((dtg_roles.SelectedCells[7].Column.GetCellContent(row) as TextBlock).Text == "✓")
            {
                checkbox_mant_roles.IsChecked = true;
            }
            if ((dtg_roles.SelectedCells[8].Column.GetCellContent(row) as TextBlock).Text == "✓")
            {
                checkbox_facturacion.IsChecked = true;
            }
            if ((dtg_roles.SelectedCells[9].Column.GetCellContent(row) as TextBlock).Text == "✓")
            {
                checkbox_bitacora.IsChecked = true;
            }

            btn_limpiar.Visibility = Visibility.Collapsed;
            v_Actividad_btnAgregar = false;
            v_Actividad_btnModificar = true;
            lbl_actividad2.Content = "Modificar Rol";
            MostrarFormulario();
        }

        /*Método el cual habilita el botón agregar, siempre y cuando los espacios correspondientes para esta actividad no estén vacíos,  
        esto se permite solamente si la bandera "v_Actividad_btnAgregar" está encendida "true".*/
        private void HabilitarBtnAgregar()
        {
            if (v_Actividad_btnAgregar == true)
            {
                if (txb_nombre.Text == "" || (checkbox_mant_usuarios.IsChecked == false && checkbox_mant_productos.IsChecked == false && checkbox_mant_roles.IsChecked == false && checkbox_mant_proveedores.IsChecked == false && checkbox_facturacion.IsChecked == false && checkbox_mant_clientes.IsChecked == false && checkbox_bitacora.IsChecked == false))
                {
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

        //Muestra el panel de búsqueda en el tab de configuración de roles.
        private void MostrarRolesExistentes()
        {
            lbl_actividad.Content = "Roles existentes";
            grd_rolesExistentes.Visibility = Visibility.Visible;
            OcultarFormulario();
        }

        //Oculta el panel de búsqueda en el tab de configuración de roles.
        private void OcultarRolesExistentes()
        {
            grd_rolesExistentes.Visibility = Visibility.Collapsed;
        }

        //Oculta el panel del formulario en el tab de configuración de roles.
        private void OcultarFormulario()
        {
            grd_formularioRoles.Visibility = Visibility.Collapsed;
        }

        //Muestra el panel del formulario en el tab de configuración de roles.
        private void MostrarFormulario()
        {
            grd_formularioRoles.Visibility = Visibility.Visible;
            OcultarRolesExistentes();
        }


        //EVENTOS DE LOS COMPONENTES.

        /*Recibe el filtro por el cual el usuario desea realizar el listado de roles y se lo asigna a la variable "v_EstadoSistema" 
        para que se envie al Data mediante el método ListarRoles() y se realice la consulta respectiva.*/
        private void cmb_tipoBusqueda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_tipoBusqueda.SelectedValue == rolesActivos)
            {
                v_EstadoSistema = "ACTIVO";
            }
            else if (cmb_tipoBusqueda.SelectedValue == rolesInactivos)
            {
                v_EstadoSistema = "INACTIVO";
            }
            else if (cmb_tipoBusqueda.SelectedValue == listaroles)
            {
                v_EstadoSistema = "LISTAROLES";
            }

            ListarRoles();
        }

        //Se implementa la búsqueda del rol que se desea, en caso de ser encontrado se despliega los datos en el DataGrid.
        private void txb_busqueda_rol_KeyUp(object sender, KeyEventArgs e)
        {
            if (txb_busqueda.Text.Contains("'"))
            {
               lbl_errorNomrol.Content = "No se permiten caracteres especiales";
               lbl_errorNomrol.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_errorNomrol.Visibility = Visibility.Collapsed;
                dtg_roles.ItemsSource = v_Model.ValidarBusquedaRoles(txb_busqueda.Text);
                dtg_roles.Columns[0].Header = "Código";
                dtg_roles.Columns[1].Header = "Nombre";
                dtg_roles.Columns[2].Header = "Estado";
                dtg_roles.Columns[3].Header = "Mantenimiento Clientes";
                dtg_roles.Columns[4].Header = "Mantenimiento Proveedores";
                dtg_roles.Columns[5].Header = "Mantenimiento Productos";
                dtg_roles.Columns[6].Header = "Mantenimiento Usuarios";
                dtg_roles.Columns[7].Header = "Mantenimiento Roles";
                dtg_roles.Columns[8].Header = "Facturación";
                dtg_roles.Columns[9].Header = "Bitácora";

                //Roles existentes
                v_Actividad_btnAgregar = false;
                btn_agregar.Visibility = Visibility.Collapsed;
                btn_modificar.Visibility = Visibility.Collapsed;
                lbl_actividad.Content = "Roles existentes";
                lbl_actividad.Visibility = Visibility.Visible;
            }
        }

        //Valida el contenido del txb_busqueda para que no se envie con errores al momento de realizar la consulta en la BD. */
        private void txb_busqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarErroresTxb(txb_busqueda, lbl_errorNomrol, "");
        }

        /*Validaciones en caja de texto de nombre.
        Valida que el contenido ingresado sea de la manera correcta, en caso contrario se muestran mensajes indicandole al usuario los errores.*/
        private void txb_nomrol_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnAgregar();
            HabilitarBtnModificar();
            ValidarErroresTxb(txb_nombre, lbl_errorNombre, "nombre");
        }

        //Valida el checkbox cuando está checkeado para saber la actividad de los botones y verificar que esté selecionado al menos un ckeckbox.
        private void checkbox_mant_usuarios_Checked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnAgregar();
            HabilitarBtnModificar();
        }

        //Valida el checkbox cuando está no checkeado para saber la actividad de los botones y verificar que esté selecionado al menos un ckeckbox.
        private void checkbox_mant_usuarios_Unchecked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnAgregar();
            HabilitarBtnModificar();
        }

        //Valida el checkbox cuando está checkeado para saber la actividad de los botones y verificar que esté selecionado al menos un ckeckbox.
        private void checkbox_mant_productos_Checked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnAgregar();
            HabilitarBtnModificar();
        }

        //Valida el checkbox cuando no está checkeado para saber la actividad de los botones y verificar que esté selecionado al menos un ckeckbox.
        private void checkbox_mant_productos_Unchecked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnAgregar();
            HabilitarBtnModificar();
        }

        //Valida el checkbox cuando está checkeado para saber la actividad de los botones y verificar que esté selecionado al menos un ckeckbox.
        private void checkbox_mant_proveedores_Checked(object sender, RoutedEventArgs e)
        {
           ValidarComponentes();
            HabilitarBtnAgregar();
            HabilitarBtnModificar();
        }

        //Valida el checkbox cuando no está checkeado para saber la actividad de los botones y verificar que esté selecionado al menos un ckeckbox.
        private void checkbox_mant_proveedores_Unchecked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnAgregar();
            HabilitarBtnModificar();
        }

        //Valida el checkbox cuando está checkeado para saber la actividad de los botones y verificar que esté selecionado al menos un ckeckbox.
        private void checkbox_mant_roles_Checked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnAgregar();
            HabilitarBtnModificar();
        }

        //Valida el checkbox cuando no está checkeado para saber la actividad de los botones y verificar que esté selecionado al menos un ckeckbox.
        private void checkbox_mant_roles_Unchecked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnAgregar();
            HabilitarBtnModificar();
        }

        //Valida el checkbox cuando está checkeado para saber la actividad de los botones y verificar que esté selecionado al menos un ckeckbox.
        private void checkbox_facturacion_Checked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnAgregar();
            HabilitarBtnModificar();
        }

        //Valida el checkbox cuando no está checkeado para saber la actividad de los botones y verificar que esté selecionado al menos un ckeckbox.
        private void checkbox_facturacion_Unchecked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnAgregar();
            HabilitarBtnModificar();
        }

        //Valida el checkbox cuando está checkeado para saber la actividad de los botones y verificar que esté selecionado al menos un ckeckbox.
        private void checkbox_mant_clientes_Checked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnAgregar();
            HabilitarBtnModificar();
        }

        //Valida el checkbox cuando no está checkeado para saber la actividad de los botones y verificar que esté selecionado al menos un ckeckbox.
        private void checkbox_mant_clientes_Unchecked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnAgregar();
            HabilitarBtnModificar();
        }

        //Valida el checkbox cuando está checkeado para saber la actividad de los botones y verificar que esté selecionado al menos un ckeckbox.
        private void checkbox_bitacora_Checked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnAgregar();
            HabilitarBtnModificar();
        }

        //Valida el checkbox cuando no está checkeado para saber la actividad de los botones y verificar que esté selecionado al menos un ckeckbox.
        private void checkbox_bitacora_Unchecked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnAgregar();
            HabilitarBtnModificar();
        }

        //Validación en la actividad del radiobutton Activo. Al seleccionarse se llaman los metodos respectivos para las actividades de los botones. 
        private void Rb_activo_Checked(object sender, RoutedEventArgs e)
        {
            HabilitarBtnAgregar();
            HabilitarBtnModificar();
        }

        //Validación en la actividad del radiobutton Inactivo. Al seleccionarse se llaman los metodos respectivos para las actividades de los botones. 
        private void Rb_inactivo_Checked(object sender, RoutedEventArgs e)
        {
            HabilitarBtnAgregar();
            HabilitarBtnModificar();
        }

        //VALIDACIONES.

        /*Método el cual valida si la caja de texto recibida contiene caracteres especiales.
         *Recibe como parámetro el textbox y un identificador con el cual se realizará la validación, en este caso: nombre.*/
        private Boolean ValidarCaracteresEspeciales(String v_Txb, String v_Identificador)
        {
            if (v_Identificador == "nombre")
            {
                //caracteres que permite si la cadena es de string
                String v_Caracteres = "[!@#$%^*())+=.,<>{}¬º´/\"':;|~¡?`¿-]";
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
        private void ValidarErroresTxb(TextBox txb_rol, Label lbl_error, string tipo)
        {
            if (txb_rol.Text == "")
            {
                lbl_error.Content = "Espacio vacío";
                lbl_error.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_error.Visibility = Visibility.Hidden;
            }
            if (txb_rol.Text == " ")
            {
                txb_nombre.Text = "";
            }
            else if (txb_rol.Text.Contains("  "))
            {
                lbl_error.Content = "Parámetros incorrectos (espacios seguidos)";
                lbl_error.Visibility = Visibility.Visible;
            }
            else if (ValidarCaracteresEspeciales(txb_rol.Text, tipo) == true)
            {
                lbl_error.Content = "No se permiten caracteres especiales";
                lbl_error.Visibility = Visibility.Visible;
            }
        }

        //Se validan los checkbox para saber si al menos uno está checkeado.
        private void ValidarComponentes()
        {
            ValidarErroresTxb(txb_nombre, lbl_errorNombre, "numeros");

            if (checkbox_mant_clientes.IsChecked == false && checkbox_facturacion.IsChecked == false && checkbox_bitacora.IsChecked == false && checkbox_mant_productos.IsChecked == false && checkbox_mant_proveedores.IsChecked == false && checkbox_mant_roles.IsChecked == false && checkbox_mant_usuarios.IsChecked == false)
            {
                lbl_errorCB.Content = "Seleccione al menos un mantenimiento";
                lbl_errorCB.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_errorCB.Visibility = Visibility.Collapsed;
            }
        }

    }//Fin de la clase.
}//Fin proyecto.

