using Entidades;
using Logica;
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



namespace Proyecto
{
    /// <summary>
    /// Lógica de interacción para Usuarios.xaml
    /// </summary>
    public partial class MantenimientoUsuarios : Window
    {
        //Creación de variables e instancias
        EntidadUsuarios v_Clt = new EntidadUsuarios();
        EntidadBitacora bitacora = new EntidadBitacora();
        Model v_Model = new Model();
        bool v_Actividad_btnModificar = false;
        bool v_Actividad_btnAgregar = true;
        String v_EstadoSistema = "";
        public string nombreUsuario = "";

        //Constructor de la Clase. Se cargan los usuarios en el datagrid del tab "Listar" y se crea el hilo que se usará para mostrar la hora al usuario.
        public MantenimientoUsuarios()
        {
            InitializeComponent();
            //Formato para la hora.
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            MostrarUsuariosExistentes();
            
            //Cargar usuarios existentes.
            cmb_tipoBusqueda.SelectedIndex = 2;
            dtg_lista.ItemsSource = v_Model.MostrarListaUsuarios("LISTAUSUARIOS").DefaultView;
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
        private void btn_regresarMenu_Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.cargarMenu(nombreUsuario);
            ventana.nombreUser = usuario_usuarios.Text;
            ventana.Show();
            this.Close();
        }

        //Botón el cual brinda con información necesaria para la utilización de la ventana en la que se encuentra el usuario.
        private void btn_ayuda_Click(object sender, RoutedEventArgs e)
        {
            Vista.Ayuda ventana = new Vista.Ayuda();
            ventana.nombreUsuario = nombreUsuario;
            ventana.Show();
            ventana.Pantalla = "Usuarios";
        }

        //Vuelve al panel de búsqueda en el tab de configuración de usuarios y oculta el formulario.
        private void btn_volver_Click(object sender, RoutedEventArgs e)
        { 
            MostrarUsuariosExistentes();
            btn_limpiar_Click(sender, e);
        }

        //Presenta formulario para agregar un nuevo usuario y oculta el panel de búsqueda, esto en el tab de configuración de usuarios.
        private void btn_agregarUsuario_Click(object sender, RoutedEventArgs e)
        {
            MostrarFormulario();
            v_Actividad_btnAgregar = true;
            btn_limpiar.Visibility = Visibility.Visible;
            lbl_actividad.Content = "Agregar usuario";
            rb_activo.IsChecked = true;
        }

        //Botón el cual permite agregar un nuevo usuario, este botón posee las validaciones necesarias para la ejecución de su funcionalidad.
        private void btn_agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbl_errorNumCed.Visibility == Visibility.Visible || lbl_errorNombre.Visibility == Visibility.Visible || lbl_errorApellidos.Visibility == Visibility.Visible || lbl_errorTelefono.Visibility == Visibility.Visible || lbl_errorTelefonoOpcional.Visibility == Visibility.Visible || lbl_errorCorreo.Visibility == Visibility.Visible || lbl_errorPuesto.Visibility == Visibility.Visible || lbl_errorRol.Visibility == Visibility.Visible || lbl_errorUsuario.Visibility == Visibility.Visible || lbl_errorContrasenna.Visibility == Visibility.Visible || lbl_errorRb.Visibility == Visibility.Visible)
                {
                    MessageBox.Show("No se puede agregar\nHay errores por corregir.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    //Obtiene los datos del formulario y los asigna a la Entidad Usuario.
                    v_Clt.v_CedIdentificacion = Convert.ToInt64(txb_numCed.Text);
                    v_Clt.v_NombreUsuario = txb_nombre.Text;
                    v_Clt.v_Apellidos = txb_apellidos.Text;
                    v_Clt.v_Correo = txb_correo.Text;
                    v_Clt.v_Telefono = Convert.ToInt64(txb_telefono.Text);

                    if (txb_telefonoOpcional.Text == "")
                    {
                        v_Clt.v_TelefonoOpcional = 0;
                    }
                    else
                    {
                        v_Clt.v_TelefonoOpcional = Convert.ToInt64(txb_telefonoOpcional.Text);
                    }

                    v_Clt.v_Correo = txb_correo.Text;

                    v_Clt.v_Puesto = txb_puesto.Text;

                    EntidadRoles ComboItem = (EntidadRoles)cmb_rol.SelectedItem;
                    Int64 v_IdRol = ComboItem.v_IdRol;
                    v_Clt.v_IdRol = v_IdRol;

                    v_Clt.v_UsuarioSistema = txb_usuario.Text;
                    v_Clt.v_Contrasena = txb_contrasenna.Text;

                    if (rb_inactivo.IsChecked == true)
                    {
                        v_Clt.v_EstadoSistema = "INACTIVO";
                    }
                    else
                    {
                        v_Clt.v_EstadoSistema = "ACTIVO";
                    }

                    int v_Resultado = v_Model.AgregarUsuarios(v_Clt);
                    if (v_Resultado == -1)
                    {
                        MessageBox.Show("Datos ingresados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Se agrega a la bitácora la información respectiva acerca de la agregación del usuario.
                        bitacora.accion = "Agregar";
                        bitacora.usuario_Responsable = usuario_usuarios.Text;
                        bitacora.ventana_Mantenimiento = "Mantenimiento Usuarios";
                        bitacora.descripcion = "Agregó el usuario con el número de cédula: " + txb_numCed.Text + ", con el nombre: " + txb_nombre.Text;
                        v_Model.AgregarBitacora(bitacora);

                        btn_limpiar_Click(sender, e);
                        MostrarUsuariosExistentes();

                        //Se actualiza el tap "Listar".
                        v_EstadoSistema = "LISTAUSUARIOS";
                        ListarUsuarios();
                    }
                }
            }
            catch (Exception m)
            {
                Console.WriteLine(m.ToString());
                MessageBox.Show("Error al agregar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Botón el cual permite modificar un usuario seleccionado, este botón posee las validaciones necesarias para la ejecución de su funcionalidad.
        private void btn_modificar_Click(object sender, RoutedEventArgs e)
        {
            if ((lbl_errorNombre.Visibility == Visibility.Visible) ||
                (lbl_errorApellidos.Visibility == Visibility.Visible || lbl_errorCorreo.Visibility == Visibility.Visible) ||
                (lbl_errorTelefono.Visibility == Visibility.Visible || lbl_errorTelefonoOpcional.Visibility == Visibility.Visible) ||
                (lbl_errorPuesto.Visibility == Visibility.Visible || lbl_errorRol.Visibility == Visibility.Visible) ||
                (lbl_errorUsuario.Visibility == Visibility.Visible || lbl_errorContrasenna.Visibility == Visibility.Visible) ||
                (lbl_errorRb.Visibility == Visibility.Visible))
            {
                MessageBox.Show("Error al modificar\nHacen falta campos por rellenar o errores que corregir", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    //Obtiene los datos del formulario y los asigna a la Entidad Usuario.
                    v_Clt.v_CedIdentificacion = Convert.ToInt64(txb_numCed.Text);
                    v_Clt.v_NombreUsuario = txb_nombre.Text;
                    v_Clt.v_Correo = txb_correo.Text;
                    v_Clt.v_Apellidos = txb_apellidos.Text;
                    v_Clt.v_Correo = txb_correo.Text;
                    v_Clt.v_Telefono = Convert.ToInt64(txb_telefono.Text);

                    if (txb_telefonoOpcional.Text == "")
                    {
                        v_Clt.v_TelefonoOpcional = 0;
                    }
                    else
                    {
                        v_Clt.v_TelefonoOpcional = Convert.ToInt64(txb_telefonoOpcional.Text);
                    }
                    v_Clt.v_Puesto = txb_puesto.Text;

                    EntidadRoles ComboItem = (EntidadRoles)cmb_rol.SelectedItem;
                    Int64 v_IdRol = ComboItem.v_IdRol;
                    v_Clt.v_IdRol = v_IdRol;

                    v_Clt.v_UsuarioSistema = txb_usuario.Text;
                    v_Clt.v_Contrasena = txb_contrasenna.Text;

                    if (rb_inactivo.IsChecked == true)
                    {
                        v_Clt.v_EstadoSistema = "INACTIVO";
                    }
                    else
                    {
                        v_Clt.v_EstadoSistema = "ACTIVO";
                    }
                    if (v_Model.ValidarModificacionUsuario(v_Clt) == true)
                    {
                        lbl_errorNumCed.Visibility = Visibility.Hidden;
                    }
                    if (lbl_errorNumCed.Visibility == Visibility.Visible)
                    {
                        MessageBox.Show("Error al modificar\nHacen falta campos por rellenar o errores que corregir", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    else if (v_Model.ModificarUsuarios(v_Clt) == -1)
                    {
                        MessageBox.Show("Datos modificados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Se agrega a la bitácora la información respectiva acerca de la modificación del usuario.
                        bitacora.accion = "Modificar";
                        bitacora.usuario_Responsable = usuario_usuarios.Text;
                        bitacora.ventana_Mantenimiento = "Mantenimiento Usuarios";
                        bitacora.descripcion = "Modificó el usuario con el número de cédula: " + txb_numCed.Text + ", con el nombre: " + txb_nombre.Text;
                        v_Model.AgregarBitacora(bitacora);

                        btn_limpiar_Click(sender, e);
                        v_Actividad_btnAgregar = true;
                        MostrarUsuariosExistentes();

                        //Se actualiza el tap "Listar".
                        v_EstadoSistema = "LISTAUSUARIOS";
                        ListarUsuarios();
                    }
                }
                catch (Exception m)
                {
                    Console.WriteLine(m.ToString());
                    MessageBox.Show("Error al modificar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //Botón el cual cumple con la funcionalidad de eliminar todos los datos existentes en los componentes situadas en el tab de configuración de usuarios.
        private void btn_limpiar_Click(object sender, RoutedEventArgs e)
        {
            txb_busqueda.Text = "";
            txb_numCed.Text = "";
            txb_nombre.Text = "";
            txb_apellidos.Text = "";
            txb_correo.Text = "";
            txb_telefono.Text = "";
            txb_telefonoOpcional.Text = "";
            txb_contrasenna.Text = "";
            txb_puesto.Text = "";
            cmb_rol.Text = "";
            txb_usuario.Text = "";
            rb_activo.IsChecked = true;
            rb_inactivo.IsChecked = false;
            lbl_errorNumCed.Visibility = Visibility.Hidden;
            lbl_errorBusqueda.Visibility = Visibility.Hidden;
            lbl_errorNombre.Visibility = Visibility.Hidden;
            lbl_errorApellidos.Visibility = Visibility.Hidden;
            lbl_errorTelefono.Visibility = Visibility.Hidden;
            lbl_errorTelefonoOpcional.Visibility = Visibility.Hidden;
            lbl_errorCorreo.Visibility = Visibility.Hidden;
            lbl_errorPuesto.Visibility = Visibility.Hidden;
            lbl_errorRol.Visibility = Visibility.Hidden;
            lbl_errorContrasenna.Visibility = Visibility.Hidden;
            lbl_errorUsuario.Visibility = Visibility.Hidden;
            lbl_errorRb.Visibility = Visibility.Hidden;
            btn_agregar.Visibility = Visibility.Collapsed;
            btn_modificar.Visibility = Visibility.Collapsed;
            v_Actividad_btnModificar = false;
            v_Actividad_btnAgregar = true;
        }

        //MÉTODOS FUNCIONALIDAD

        /*Lista los usuarios existentes en el sistema según su estado, envía el estado en el sistema (v_EstadoSistema) que se 
         * obtiene del combobox "cmb_tipoBusqueda" con el cual se realizará la consulta.*/
        private void ListarUsuarios()
        {
            if (v_Model.MostrarListaUsuarios(v_EstadoSistema).Rows.Count == 0)
            {
                MessageBox.Show("No hay datos registrados", "Búsqueda", MessageBoxButton.OK, MessageBoxImage.Warning);
                v_EstadoSistema = "LISTAUSUARIOS";
                cmb_tipoBusqueda.SelectedIndex = 2;
            }
            else
            {
                dtg_lista.ItemsSource = v_Model.MostrarListaUsuarios(v_EstadoSistema).DefaultView;
            }
        }

        /*Llena el combobox con los roles existentes obtenidos por medio de una consulta a la base de datos.
        Se obtiene el id y el nombre del rol y se asigna a la "EntidadRoles" para así establecerlo en las propiedades del combobox. */
        private void LlenarComboboxRol()
        {
            var v_RolesExistentes = new List<EntidadRoles>();
            v_RolesExistentes = v_Model.RolesExistentes();
            cmb_rol.ItemsSource = v_RolesExistentes;
            cmb_rol.DisplayMemberPath = "v_Nombre";
            cmb_rol.SelectedValuePath = "v_IdRol";
        }

        //Muestra el panel de búsqueda en el tab de configuración de usuarios.
        private void MostrarUsuariosExistentes()
        {
            OcultarFormulario();
            lbl_actividad.Content = "Usuarios existentes";
            grd_usuariosExistentes.Visibility = Visibility.Visible;
        }

        //Oculta el panel de búsqueda en el tab de configuración de usuarios.
        private void OcultarUsuariosExistentes()
        {
            grd_usuariosExistentes.Visibility = Visibility.Collapsed;
        }

        //Oculta el panel del formulario en el tab de configuración de usuarios.
        private void OcultarFormulario()
        {
            grd_formularioUsuario.Visibility = Visibility.Collapsed;
        }

        //Muestra el panel del formulario en el tab de configuración de usuarios.
        private void MostrarFormulario()
        {
            OcultarUsuariosExistentes();
            grd_formularioUsuario.Visibility = Visibility.Visible;
            LlenarComboboxRol();
        }

        /*Método el cual habilita el botón agregar, siempre y cuando los espacios correspondientes para esta actividad no estén vacíos,  
        esto se permite solamente si la bandera "v_Actividad_btnAgregar" está encendida "true".*/
        private void HabilitarBtnAgregar()
        {
            if (v_Actividad_btnAgregar == true)
            {
                if (txb_numCed.Text == "" || txb_nombre.Text == "" || txb_apellidos.Text == "" || txb_telefono.Text == "" || txb_correo.Text == "" || txb_puesto.Text == "" || cmb_rol.Text == "" || txb_usuario.Text == "" || txb_contrasenna.Text == "")
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

        /*Método el cual cumple con la funcionalidad de desplegar los datos en los campos correspondientes de un usuario seleccionado 
        en el DataGrid con la finalidad de ser modificado, esto en el panel del formulario en el tab de configuración de usuarios. */
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            MostrarFormulario();
            DataGridRow row = sender as DataGridRow;

            v_Clt.v_IdUsuario = Convert.ToInt64((dtg_usuarios.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text);

            txb_numCed.Text = (dtg_usuarios.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text;
            txb_nombre.Text = (dtg_usuarios.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;
            txb_apellidos.Text = (dtg_usuarios.SelectedCells[3].Column.GetCellContent(row) as TextBlock).Text;
            txb_telefono.Text = (dtg_usuarios.SelectedCells[4].Column.GetCellContent(row) as TextBlock).Text;
            txb_telefonoOpcional.Text = (dtg_usuarios.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text;
            txb_correo.Text = (dtg_usuarios.SelectedCells[6].Column.GetCellContent(row) as TextBlock).Text;
            txb_puesto.Text = (dtg_usuarios.SelectedCells[7].Column.GetCellContent(row) as TextBlock).Text;
            txb_usuario.Text = (dtg_usuarios.SelectedCells[10].Column.GetCellContent(row) as TextBlock).Text;
            txb_contrasenna.Text = (dtg_usuarios.SelectedCells[11].Column.GetCellContent(row) as TextBlock).Text;

            /*Se guarda el id del rol que se obtiene del datagrid en la variable: v_Clt.v_IdRol y se asigna al combobox mediante
            la propiedad SelectedValue. Automáticamente se agigna el nombre del rol por medio del id.*/
            v_Clt.v_IdRol = Convert.ToInt64((dtg_usuarios.SelectedCells[8].Column.GetCellContent(row) as TextBlock).Text);
            cmb_rol.SelectedValue = v_Clt.v_IdRol; 

            if ((dtg_usuarios.SelectedCells[13].Column.GetCellContent(row) as TextBlock).Text == "ACTIVO")
            {
                rb_activo.IsChecked = true;
            }
            else if ((dtg_usuarios.SelectedCells[13].Column.GetCellContent(row) as TextBlock).Text == "INACTIVO")
            {
                rb_inactivo.IsChecked = true;
            }
            lbl_actividad.Content = "Modificar usuario";
            btn_limpiar.Visibility = Visibility.Collapsed;
            v_Actividad_btnModificar = true;
        }

        //EVENTOS DE LOS COMPONENTES.

        /*Se implementa la búsqueda del usuario que se desea, en caso de ser encontrado se despliega los datos en el DataGrid.
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
                //Usuarios existentes.  
                dtg_usuarios.ItemsSource = v_Model.ValidarBusquedaUsuarios(txb_busqueda.Text);
                dtg_usuarios.Columns[0].Header = "Código";
                dtg_usuarios.Columns[1].Header = "Número de Cédula";
                dtg_usuarios.Columns[2].Header = "Nombre del Usuario";
                dtg_usuarios.Columns[3].Header = "Apellidos";
                dtg_usuarios.Columns[4].Header = "Teléfono";
                dtg_usuarios.Columns[5].Header = "Tel. Opcional";
                dtg_usuarios.Columns[6].Header = "Correo";
                dtg_usuarios.Columns[7].Header = "Puesto";
                dtg_usuarios.Columns[8].Header = "Id Rol";
                dtg_usuarios.Columns[9].Header = "Nombre Rol";
                dtg_usuarios.Columns[10].Header = "Usuario";
                dtg_usuarios.Columns[11].Header = "Contraseña";
                dtg_usuarios.Columns[12].Header = "Fecha";
                dtg_usuarios.Columns[13].Header = "Estado en el Sistema";

                v_Actividad_btnAgregar = false;
            }
        }

        /*Método el cual valida si la cédula jurídica es existente o no, en caso de ser existente muestra un mensaje de error.
        * Además valida que el contenido ingresado sea de la manera correcta, en caso contrario se muestran mensajes indicandole al usuario los errores.*/
        private void ValidarTxbNumCed(object sender, EventArgs e)
        {
            string v_NumCed = txb_numCed.Text;
            HabilitarBtnAgregar();
            HabilitarBtnModificar();
            ValidarErroresTxb(txb_numCed, lbl_errorNumCed, "numeros");
            if (lbl_errorNumCed.Visibility == Visibility.Hidden)
            {
                bool v_Resultado = v_Model.ValidarNumCedUsuarios(v_NumCed);
                if (v_Resultado == true)
                {
                    lbl_errorNumCed.Content = "La cédula jurídica ya existe";
                    lbl_errorNumCed.Visibility = Visibility.Visible;
                }
                else
                {
                    lbl_errorNumCed.Visibility = Visibility.Hidden;
                }
            }
        }

        /*Validaciones en caja de texto de nombre.
        Valida que el contenido ingresado sea de la manera correcta, en caso contrario se muestran mensajes indicandole al usuario los errores.*/
        private void txb_nombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_nombre, lbl_errorNombre, "palabras");
        }

        /*Validaciones en caja de texto de apellidos.
        Valida que el contenido ingresado sea de la manera correcta, en caso contrario se muestran mensajes indicandole al usuario los errores.*/
        private void txb_apellidos_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_apellidos, lbl_errorApellidos, "palabras");
        }

        /*Validaciones en caja de texto de correo.
        Valida que el contenido ingresado sea de la manera correcta, en caso contrario se muestran mensajes indicandole al usuario los errores.*/
        private void txb_Correo_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_correo, lbl_errorCorreo, "");
        }

        /*Validaciones en caja de texto de teléfono.
        Valida que el contenido ingresado sea de la manera correcta, en caso contrario se muestran mensajes indicandole al usuario los errores.*/
        private void txb_telefono_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_telefono, lbl_errorTelefono, "numeros");
        }

        /*Validaciones en caja de texto de teléfono opcional.
        Valida que el contenido ingresado sea de la manera correcta, en caso contrario se muestran mensajes indicandole al usuario los errores.*/
        private void txb_telefonoOpcional_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_telefonoOpcional, lbl_errorTelefonoOpcional, "numeros");
            if (txb_telefonoOpcional.Text == "")
            {
                lbl_errorTelefonoOpcional.Visibility = Visibility.Hidden;
            }
            else if (txb_telefonoOpcional.Text == "0")
            {
                lbl_errorTelefonoOpcional.Visibility = Visibility.Hidden;
            }
        }

        /*Validaciones en caja de texto de puesto.
        Valida que el contenido ingresado sea de la manera correcta, en caso contrario se muestran mensajes indicandole al usuario los errores.*/
        private void txb_puesto_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_puesto, lbl_errorPuesto, "palabras");
        }

        //Validaciones en combobox de rol.
        private void cmb_rol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
        }

        /*Validaciones en caja de texto de usuario.
        Valida que el contenido ingresado sea de la manera correcta, en caso contrario se muestran mensajes indicandole al usuario los errores.*/
        private void txb_usuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_usuario, lbl_errorUsuario, "palabras");
        }

        /*Validaciones en caja de texto de contraseña.
        Valida que el contenido ingresado sea de la manera correcta, en caso contrario se muestran mensajes indicandole al usuario los errores.*/
        private void txb_contrasenna_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_contrasenna, lbl_errorContrasenna, "contrasenna");
            ValidarContrasenna(sender, e);
        }

        //Validación en la actividad del radiobutton Activo. Al seleccionarse se llaman los metodos respectivos para las actividades de los botones. 
        private void rb_activo_Checked(object sender, RoutedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
        }

        //Validación en la actividad del radiobutton Inactivo. Al seleccionarse se llaman los metodos respectivos para las actividades de los botones. 
        private void rb_inactivo_Checked(object sender, RoutedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
        }

        /*Recibe el filtro por el cual el usuario desea realizar el listado de usuarios y se lo asigna a la variable "v_EstadoSistema" 
        para que se envie al Data mediante el método ListarUsuarios() y se realice la consulta respectiva.*/
        private void cmb_tipoBusqueda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_tipoBusqueda.SelectedValue == usuariosActivos)
            {
                v_EstadoSistema = "ACTIVO";
            }
            else if (cmb_tipoBusqueda.SelectedValue == usuariosInactivos)
            {
                v_EstadoSistema = "INACTIVO";
            }
            else if (cmb_tipoBusqueda.SelectedValue == listaUsuarios)
            {
                v_EstadoSistema = "LISTAUSUARIOS";
            }

            ListarUsuarios();
        }

        //VALIDACIONES.

        /*Método el cual recibe 3 parametros para llevar a cabo las validaciones necesarias. 
         * Recibe el textbox a validar, el label de error respectivo y la cadena "tipo" con la que se llamará al método 
         * ValidarCaracteresEspeciales(tipo) para verificar si el contenido posee caracteres no permitidos.*/
        private void ValidarErroresTxb(TextBox txb_usuario, Label lbl_error, string tipo)
        {
            string v_TamanoTxb = txb_usuario.Text;
            if (txb_usuario.Text == "")
            {
                lbl_error.Content = "Espacio vacío";
                lbl_error.Visibility = Visibility.Visible;
            }
            else if (txb_usuario == txb_telefonoOpcional && txb_usuario.Text == "")
            {
                lbl_error.Visibility = Visibility.Hidden;
            }
            else if (txb_usuario.Text == " ")
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
            else if (txb_usuario == txb_telefono || txb_usuario == txb_telefonoOpcional)
            {
                if (v_TamanoTxb.Length < 8)
                {
                    lbl_error.Content = "Deben tener al menos 8 dígitos";
                    lbl_error.Visibility = Visibility.Visible;
                }
                else if (v_TamanoTxb.Length <= 12)
                {
                    if (ValidarCaracteresEspeciales(txb_usuario.Text, tipo) == false)
                    {
                        lbl_error.Visibility = Visibility.Hidden;
                    }
                }
            }
            else if (txb_usuario == txb_numCed)
            {
                if (v_TamanoTxb.Length < 9)
                {
                    lbl_error.Content = "Deben tener al menos 9 dígitos";
                    lbl_error.Visibility = Visibility.Visible;
                }
                else if (v_TamanoTxb.Length <= 12)
                {
                    if (ValidarCaracteresEspeciales(txb_usuario.Text, tipo) == false)
                    {
                        lbl_error.Visibility = Visibility.Hidden;
                    }
                }
            }
            else if (txb_usuario == txb_contrasenna)
            {
                if (v_TamanoTxb.Length < 8 || v_TamanoTxb.Length > 16)
                {
                    lbl_error.Content = "Debe tener entre 8 y 16 caracteres alfanuméricos";
                    lbl_error.Visibility = Visibility.Visible;
                }
                else if (v_TamanoTxb.Length <= 16)
                {
                    if (ValidarCaracteresEspeciales(txb_usuario.Text, tipo) == false)
                    {
                        lbl_error.Visibility = Visibility.Hidden;
                    }
                }
            }
            else
            {
                lbl_error.Visibility = Visibility.Hidden;
            }
        }

        /*Método el cual valida si la caja de texto recibida contiene caracteres especiales.
         *Recibe como parámetro el textbox y un identificador con el cual se realizará la validación, este puede ser: numeros, palabras o contrasenna.*/
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
            else if (v_Identificador == "palabras")
            {
                //Caracteres que no permite si la cadena es de string.
                String v_Caracteres = "[!@#$%^*())+=.,<>{}¬º´/\"':;|~¡?`¿-]";
                if (Regex.IsMatch(v_Txb, v_Caracteres))
                {
                    return true;
                }
            }
            else if (v_Identificador == "contrasenna")
            {
                //Caracteres que no permite si la cadena es de string e int.
                String v_Caracteres = "[!@#$%^*())+=.,<>{}¬º´/\"':;|~¡?`¿-]";
                if (Regex.IsMatch(v_Txb, v_Caracteres))
                {
                    return true;
                }
            }
            return false;
        }

        //Valida que la contraseña sea alfanumérica, en caso contrario muestra mensajes de error.
        private void ValidarContrasenna(object sender, EventArgs e)
        {
            if (txb_contrasenna.Text == "")
            {
                lbl_errorContrasenna.Visibility = Visibility.Visible;
                lbl_errorContrasenna.Content = "Espacio Vacío";
            }
            else if (!Regex.IsMatch((string)txb_contrasenna.Text, "[a-zA-Z]"))
            {
                //Si no escriben letras
                lbl_errorContrasenna.Visibility = Visibility.Visible;
                lbl_errorContrasenna.Content = "Debe mezclar números y letras";
            }
            else if (!Regex.IsMatch((string)txb_contrasenna.Text, "[0-9]"))
            {
                //Si no escriben números
                lbl_errorContrasenna.Visibility = Visibility.Visible;
                lbl_errorContrasenna.Content = "Debe mezclar números y letras";
            }
            else if (!Regex.IsMatch((string)txb_contrasenna.Text, "[0-9a-zA-Z]"))
            {
                //Si escriben letras y números
                lbl_errorContrasenna.Visibility = Visibility.Hidden;
            }
        }

        //Validación en caja de texto de teléfono. Bloquea el textbox para que no se pueda ingresar letras.
        private void Telefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
            {
                e.Handled = false;
                lbl_errorTelefono.Visibility = Visibility.Hidden;
                if (ValidarCaracteresEspeciales(txb_telefono.Text, "numeros") == true)
                {
                    lbl_errorTelefono.Content = "No se permiten caracteres especiales";
                    lbl_errorTelefono.Visibility = Visibility.Visible;
                }
            }
            else
            {
                e.Handled = true;
                lbl_errorTelefono.Content = "No se permite ingresar letras";
                lbl_errorTelefono.Visibility = Visibility.Visible;
            }
        }

        //Validación en caja de texto de teléfono opcional. Bloquea el textbox para que no se pueda ingresar letras.
        private void TelefonoOpcional_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
            {
                e.Handled = false;
                lbl_errorTelefonoOpcional.Visibility = Visibility.Hidden;
                if (ValidarCaracteresEspeciales(txb_telefonoOpcional.Text, "numeros") == true)
                {
                    lbl_errorTelefonoOpcional.Content = "No se permiten caracteres especiales";
                    lbl_errorTelefonoOpcional.Visibility = Visibility.Visible;
                }
            }
            else
            {
                e.Handled = true;
                lbl_errorTelefonoOpcional.Content = "No se permite ingresar letras";
                lbl_errorTelefonoOpcional.Visibility = Visibility.Visible;
            }
        }

        //Validación en caja de texto de cédula de identificación. Bloquea el textbox para que no se pueda ingresar letras.
        private void NumCed_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
            {
                e.Handled = false;
                lbl_errorNumCed.Visibility = Visibility.Hidden;
            }
            else
            {
                e.Handled = true;
                lbl_errorNumCed.Content = "No se permite ingresar letras";
                lbl_errorNumCed.Visibility = Visibility.Visible;
            }
        }

        //Método el cual valida si la cadena recibida posee los parametros correctos de un correo electrónico.
        private Boolean CorreoCorrecto(String v_correo)
        {
            String v_Expresion;
            v_Expresion = @"^([a-zA-Z0-9_\-\.ñÑ]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,3}|[0-9]{1,3})(\]?)$";
            if (Regex.IsMatch(v_correo, v_Expresion))
            {
                if (Regex.Replace(v_correo, v_Expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //Método el cual valida el contenido del correo electrónico, en caso de que esté incorrecto se muestra un mensaje de error al usuario.
        private void Validarcorreo(object sender, EventArgs e)
        {
            Console.WriteLine("Correo: " + txb_correo.Text);
            Console.WriteLine(CorreoCorrecto(txb_correo.Text));
            if (CorreoCorrecto(txb_correo.Text) == false)
            {
                if (txb_correo.Text == "")
                {
                    lbl_errorCorreo.Content = "Espacio vacío";
                    lbl_errorCorreo.Visibility = Visibility.Visible;
                }
                else
                {
                    lbl_errorCorreo.Content = "Formato: usuario@dominio.extension(Máx 3 caracteres)";
                    lbl_errorCorreo.Visibility = Visibility.Visible;
                }
            }
        }

    }//Fin de la clase.
}//Fin del proyecto


