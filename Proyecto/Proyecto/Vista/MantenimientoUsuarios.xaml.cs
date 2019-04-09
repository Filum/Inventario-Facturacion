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
        EntidadUsuarios v_Clt = new EntidadUsuarios();
        Model v_Model = new Model();
        bool v_Actividad_btnModificar = false;
        bool v_Actividad_btnAgregar = true;
        String v_EstadoSistema = "";

        public MantenimientoUsuarios()
        {
            InitializeComponent();
            MostrarUsuariosExistentes();
            //Formato para la hora
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            
            //Cargar usuarios existentes
            cmb_tipoBusqueda.SelectedIndex = 2;
            dtg_lista.ItemsSource = v_Model.MostrarListaUsuarios("LISTAUSUARIOS").DefaultView;
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            txt_fecha.Content = DateTime.Now.ToString();
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

        private void btn_regresarMenu_Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.Show();
            this.Close();
        }

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
            rb_activo.IsChecked = false;
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

        private void btn_ayuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Reglas: \n" +
                "1. Listar: Seleccione el botón Listar para desplegar los datos. \n" +
                "2. Buscar: Ingrese el elemento a buscar y seleccione el botón Buscar para ver los resultados. \n" +
                "3. Ingresar: \n" +
                "   Complete todos los campos.\n" +
                "   Las cajas de texto de los teléfonos solo permiten números.\n" +
                "   Formato de correo: usuario@dominio.extension \n" +
                "   Seleccionar el rol que se le desea asignar al usuario. \n" +
                "   Formato de contraseña: Tamaño entre 8 y 16 caracteres alfanuméricos.\n" +
                "4. Deshabilitar: \n" +
                "   Ingrese el elemento a deshabilitar y seleccione el botón Buscar para ver los resultados.\n" +
                "   Seleccione el elemento e ingrese el motivo por el cual se desea deshabilitar\n" +
                "5. Actualizar: \n" +
                "   Ingrese el elemento a actualizar y seleccione el botón Buscar para ver los resultados.\n" +
                "   Seleccione el elemento y proceda a editar el/los campos que desee.\n" +
                "   Se utiliza el mismo formato que ingresar. No deje campos vacíos.\n" +
                "6. Historial: Seleccione el botón Historial para desplegar los datos.", "Ayuda",
                MessageBoxButton.OK);
        }

        /*Botón el cual permite listar los usuarios existentes en el sistema según su estado, envía el estado en el 
        * sistema(v_EstadoSistema) que se obtiene del combobox "cmb_tipoBusqueda" con el cual se realizará la consulta.*/
        private void btn_listar_Click(object sender, RoutedEventArgs e)
        {
            if (v_Model.MostrarListaUsuarios(v_EstadoSistema).Rows.Count == 0)
            {
                MessageBox.Show("No hay datos registrados", "Búsqueda", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                dtg_lista.ItemsSource = v_Model.MostrarListaUsuarios(v_EstadoSistema).DefaultView;
                dtg_lista.Columns[0].Header = "Número de Cédula";
                dtg_lista.Columns[1].Header = "Nombre del Usuario";
                dtg_lista.Columns[2].Header = "Apellidos";
                dtg_lista.Columns[3].Header = "Teléfono";
                dtg_lista.Columns[4].Header = "Tel. Opcional";
                dtg_lista.Columns[5].Header = "Correo";
                dtg_lista.Columns[6].Header = "Puesto";
                dtg_lista.Columns[7].Header = "Rol";
                dtg_lista.Columns[8].Header = "Usuario";
                dtg_lista.Columns[9].Header = "Contraseña";
                dtg_lista.Columns[10].Header = "Fecha de Ingreso";
                dtg_lista.Columns[11].Header = "Estado en el Sistema";
            }
        }

        private void btn_volver_Click(object sender, RoutedEventArgs e)
        { 
            MostrarUsuariosExistentes();
            btn_limpiar_Click(sender, e);
        }

        //Metodo el cual sirve para abrir el formulario de usuarios inicializando la agregacion
        private void btn_agregarUsuario_Click(object sender, RoutedEventArgs e)
        {
            MostrarFormulario();
            v_Actividad_btnAgregar = true;
            lbl_actividad.Content = "Agregar usuario";
        }

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
                        btn_limpiar_Click(sender, e);
                        v_Actividad_btnAgregar = true;
                        DeshabilitarComponentes();
                    }
                }
                catch (Exception m)
                {
                    Console.WriteLine(m.ToString());
                    MessageBox.Show("Error al modificar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbl_errorNumCed.Visibility == Visibility.Visible || lbl_errorNombre.Visibility == Visibility.Visible || lbl_errorApellidos.Visibility == Visibility.Visible || lbl_errorTelefono.Visibility == Visibility.Visible || lbl_errorTelefonoOpcional.Visibility == Visibility.Visible || lbl_errorCorreo.Visibility == Visibility.Visible || lbl_errorPuesto.Visibility == Visibility.Visible || lbl_errorRol.Visibility == Visibility.Visible || lbl_errorUsuario.Visibility == Visibility.Visible || lbl_errorContrasenna.Visibility == Visibility.Visible || lbl_errorRb.Visibility == Visibility.Visible)
                {
                    MessageBox.Show("No se puede agregar\nHay errores por corregir.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
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
                    Int64 idRol = ComboItem.v_IdRol;
                    v_Clt.v_IdRol = idRol;

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
                        btn_limpiar_Click(sender, e);
                    }
                }
            }
            catch (Exception m)
            {
                Console.WriteLine(m.ToString());
                MessageBox.Show("Error al agregar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LlenarComboboxRol()
        {
            //Llenar combobox de rol
            var v_RolesExistentes = new List<EntidadRoles>();
            v_RolesExistentes = v_Model.RolesExistentes();
            cmb_rol.ItemsSource = v_RolesExistentes;
            cmb_rol.DisplayMemberPath = "v_Nombre";
            cmb_rol.SelectedValuePath = "v_IdRol";
        }

        //Muestra el panel de búsqueda en el tab de configuración de usuarios
        private void MostrarUsuariosExistentes()
        {
            OcultarFormulario();
            lbl_actividad.Content = "Usuarios existentes";
            grd_usuariosExistentes.Visibility = Visibility.Visible;
        }

        //Oculta el panel de búsqueda en el tab de configuración de usuarios
        private void OcultarUsuariosExistentes()
        {
            grd_usuariosExistentes.Visibility = Visibility.Collapsed;
        }

        //Oculta el panel del formulario en el tab de configuración de usuarios
        private void OcultarFormulario()
        {
            grd_formularioUsuario.Visibility = Visibility.Collapsed;
        }

        //Muestra el panel del formulario en el tab de configuración de usuarios
        private void MostrarFormulario()
        {
            HabilitarComponentes();
            ValidarComponentes();
            OcultarUsuariosExistentes();
            grd_formularioUsuario.Visibility = Visibility.Visible;
            LlenarComboboxRol();
        }

        //Deshabilita los componentes en el tap de "Gestión de Usuarios"
        private void DeshabilitarComponentes()
        {
            txb_numCed.IsEnabled = false;
            txb_nombre.IsEnabled = false;
            txb_apellidos.IsEnabled = false;
            txb_telefono.IsEnabled = false;
            txb_telefonoOpcional.IsEnabled = false;
            txb_correo.IsEnabled = false;
            txb_puesto.IsEnabled = false;
            cmb_rol.IsEnabled = false;
            txb_usuario.IsEnabled = false;
            txb_contrasenna.IsEnabled = false;
            rb_activo.IsEnabled = false;
            rb_inactivo.IsEnabled = false;
        }

        //Habilita los componentes en el tap de "Gestión de Usuarios"
        private void HabilitarComponentes()
        {
            txb_numCed.IsEnabled = true;
            txb_nombre.IsEnabled = true;
            txb_apellidos.IsEnabled = true;
            txb_telefono.IsEnabled = true;
            txb_telefonoOpcional.IsEnabled = true;
            txb_correo.IsEnabled = true;
            txb_puesto.IsEnabled = true;
            cmb_rol.IsEnabled = true;
            txb_usuario.IsEnabled = true;
            txb_contrasenna.IsEnabled = true;
            rb_activo.IsEnabled = true;
            rb_inactivo.IsEnabled = true;
        }

        //Método el cual habilita el botón modificar
        private void HabilitarBtnModificar()
        {
            if (v_Actividad_btnModificar == true)
            {
                btn_modificar.Visibility = Visibility.Visible;
            }
        }

        //Método el cual habilita el botón agregar siempre y cuando los espacios correspondientes para esta actividad no estén vacíos  situados en el tab de gestión de proveedores
        private void HabilitarBtnAgregar()
        {
            if (v_Actividad_btnAgregar == true)
            {
                if (txb_numCed.Text == "" || txb_nombre.Text == "" || txb_apellidos.Text == "" || txb_telefono.Text == "" || txb_correo.Text == "" || txb_puesto.Text == "" || cmb_rol.Text == "" || txb_usuario.Text == "" || txb_contrasenna.Text == "" || (rb_activo.IsChecked == false && rb_inactivo.IsChecked == false))
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

        //Método el cual recibe parametros necesarios para la validacion y la muestra de mensajes de erroes en las cajas de texto
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

        //Método el cual valida si en las cajas de texto recibidos contiene caracteres especiales
        private Boolean ValidarCaracteresEspeciales(String v_Txb, String v_Identificador)
        {
            if (v_Identificador == "numeros")
            {
                //caracteres que no permite si la cadena es de int
                String v_Caracteres = "[a-zA-Z !@#$%^&*())+=.,<>{}¬º´/\"':;|ñÑ~¡?`¿-]";
                if (Regex.IsMatch(v_Txb, v_Caracteres))
                {
                    return true;
                }
            }
            else if (v_Identificador == "palabras")
            {
                //caracteres que no permite si la cadena es de string
                String v_Caracteres = "[!@#$%^*())+=.,<>{}¬º´/\"':;|~¡?`¿-]";
                if (Regex.IsMatch(v_Txb, v_Caracteres))
                {
                    return true;
                }
            }
            else if (v_Identificador == "contrasenna")
            {
                //caracteres que no permite si la cadena es de string e int
                String v_Caracteres = "[!@#$%^*())+=.,<>{}¬º´/\"':;|~¡?`¿-]";
                if (Regex.IsMatch(v_Txb, v_Caracteres))
                {
                    return true;
                }
            }
            return false;
        }

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

        //Validaciones en caja de texto de teléfono
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

        //Validaciones en caja de texto de teléfono opcional
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

        //Validaciones en caja de texto de cédula jurídica
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

        //Método el cual valida si en la caja de texto recibida posee los parametros correctos de un correo electronico
        private Boolean correoCorrecto(String v_correo)
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

        //Método el cual valida si los parametros del correo electrónico son correctos, en caso de que no los sean muestra un error al usuario
        private void Validarcorreo(object sender, EventArgs e)
        {
            Console.WriteLine("Correo: " + txb_correo.Text);
            Console.WriteLine(correoCorrecto(txb_correo.Text));
            if (correoCorrecto(txb_correo.Text) == false)
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

        //Valida si se selecciona el estado del usuario (Activo - Inactivo) en el sistema
        private void ValidarComponentes()
        {
            if (rb_inactivo.IsChecked == false && rb_activo.IsChecked == false)
            {
                lbl_errorRb.Visibility = Visibility.Visible;
                lbl_errorRb.Content = "Debe seleccionar una opción";
            }
            else
            {
                lbl_errorRb.Visibility = Visibility.Hidden;
            }

            if (cmb_rol.SelectedItem == null)
            {
                lbl_errorRol.Visibility = Visibility.Visible;
                lbl_errorRol.Content = "Debe seleccionar una opción";
            }
            else
            {
                lbl_errorRol.Visibility = Visibility.Hidden;
            }
        }
    
        private void Button_guardarUsuario_Click(object sender, RoutedEventArgs e)
        {

        }

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
                //usuarios existentes
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

        /*Método el cual cumple con la funcionalidad de desplegar los datos en los campos correspondientes de un usuario seleccionado 
        en el DataGrid con la finalidad de ser modificado, esto en el panel del formulario en el tab de configuración de usuarios*/
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

            //se guarda el id del rol que se obtiene del datagrid en la variable: v_Clt.v_IdRol
            v_Clt.v_IdRol = Convert.ToInt64((dtg_usuarios.SelectedCells[8].Column.GetCellContent(row) as TextBlock).Text);
            //convertir de tipo long(v_Clt.v_IdRol) a tipo int(v_Indice) 
            int v_Indice = unchecked((int)v_Clt.v_IdRol);
            //indica al combobox cual opción debe estar seleccionada (asigna el id del rol por defecto según el índice que se obtiene del usuario seleccionado en el datagrid)
            //se hace la resta v_Indice-1 porque los elementos del combobox inician en 0
            cmb_rol.SelectedIndex = v_Indice-1;

            if ((dtg_usuarios.SelectedCells[13].Column.GetCellContent(row) as TextBlock).Text == "ACTIVO")
            {
                rb_activo.IsChecked = true;
            }
            else if ((dtg_usuarios.SelectedCells[13].Column.GetCellContent(row) as TextBlock).Text == "INACTIVO")
            {
                rb_inactivo.IsChecked = true;
            }

            HabilitarComponentes();
            lbl_actividad.Content = "Modificar usuario";
            btn_limpiar.Visibility = Visibility.Collapsed;
            v_Actividad_btnModificar = true;
        }

        //Método el cual valida si la cédula jurídica es existente o no, en caso de ser existente, mostrar un error
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

        private void txb_nombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_nombre, lbl_errorNombre, "palabras");
        }

        private void txb_apellidos_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_apellidos, lbl_errorApellidos, "palabras");
        }

        private void txb_Correo_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_correo, lbl_errorCorreo, "");
        }

        private void txb_telefono_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_telefono, lbl_errorTelefono, "numeros");
        }

        private void txb_telefonoOpcional_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
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

        private void txb_puesto_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_puesto, lbl_errorPuesto, "palabras");
        }

        private void cmb_rol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
        }

        private void txb_usuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_usuario, lbl_errorUsuario, "palabras");
        }

        private void txb_contrasenna_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_contrasenna, lbl_errorContrasenna, "contrasenna");
            ValidarContrasenna(sender, e);
        }

        private void rb_activo_Checked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
        }

        private void rb_inactivo_Checked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
        }

        //Recibe el filtro por el cual se desea realizar el listado de usuario y se lo asigna a la variable "v_EstadoSistema" 
        //para que se envie a data y realice la consulta respectiva.
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
        }
                
    }
}


