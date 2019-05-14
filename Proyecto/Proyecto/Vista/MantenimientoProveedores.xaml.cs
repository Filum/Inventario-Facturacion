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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

using Entidades;
using Logica;


namespace Proyecto
{
    /// <summary>
    /// Lógica de interacción para MantenimientoProveedores.xaml
    /// </summary>
    public partial class MantenimientoProveedores : Window
    {
        EntidadProveedores v_Clt = new EntidadProveedores();
        Model v_Model = new Model();
        bool v_Actividad_btnModificar = false;
        bool v_Actividad_btnAgregar = true;
        String v_EstadoSistema = "";
        public string nombreUsuario;

        public MantenimientoProveedores()
        {
            InitializeComponent();
            //Formato para la hora
            System.Windows.Threading.DispatcherTimer v_DispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            v_DispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            v_DispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            v_DispatcherTimer.Start();
            MostrarProveedoresExistentes();

            //Cargar proveedores existentes
            cmb_tipoBusqueda.SelectedIndex = 2;
            dtg_lista.ItemsSource = v_Model.MostrarListaProveedores("LISTAPROVEEDORES").DefaultView;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            lbl_fecha.Content = DateTime.Now.ToString();
        }

        //Minimiza ventana
        private void btn_minimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        //Cierra ventana
        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Maximiza ventana
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

        //Barra en el área superior de la ventana, la cual permite deslizarla de un lugar a otro
        private void barra_movil__MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                this.WindowStyle = WindowStyle.None;
                WindowState = WindowState.Normal;
            }

            this.DragMove();
        }

        //Botón el cual permite devolverse a la ventana "Menú"
        private void btn_salir_proveedores__Click(object sender, RoutedEventArgs e)
        {
            Menu v_Ventana = new Menu();
            v_Ventana.cargarMenu(nombreUsuario);
            v_Ventana.nombreUser = nombreUsuario;
            v_Ventana.Show();
            this.Close();
        }

        //Presenta formulario para agregar un nuevo proveedor y oculta el panel de búsqueda, esto en el tab de configuración de proveedores.
        private void btn_agregarProveedor_Click(object sender, RoutedEventArgs e)
        {
            btn_limpiar_Click(sender, e);
            lbl_actividad.Content = "Agregar proveedor";
            btn_limpiar.Visibility = Visibility.Visible;
            MostrarFormulario();
            rb_activo.IsChecked = true;
        }

        //Vuelve al panel de búsqueda en el tab de configuración de proveedores y oculta el formulario
        private void btn_volver_Click(object sender, RoutedEventArgs e)
        {
            MostrarProveedoresExistentes();
        }

        //Botón el cual permite agregar un nuevo proveedor, este botón posee las validaciones necesarias para la ejecución de su funcionalidad         
        private void btn_agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbl_errorCedJur.Visibility == Visibility.Visible || lbl_errorNombre.Visibility == Visibility.Visible || lbl_errorTelefono.Visibility == Visibility.Visible || lbl_errorTelefonoOpcional.Visibility == Visibility.Visible || lbl_errorcorreo.Visibility == Visibility.Visible || lbl_errorcorreoOpcional.Visibility == Visibility.Visible || lbl_errorDesc.Visibility == Visibility.Visible || lbl_errorRb.Visibility == Visibility.Visible)
                {
                    MessageBox.Show("No se puede agregar\nHay errores por corregir.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    v_Clt.v_CedulaJuridica = Convert.ToInt64(txb_cedJur.Text);
                    v_Clt.v_Nombre = txb_nombre.Text;
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

                    if (txb_correoOpcional.Text == "")
                    {
                        v_Clt.v_CorreoOpcional = "N/A";
                    }
                    else
                    {
                        v_Clt.v_CorreoOpcional = txb_correoOpcional.Text;
                    }

                    v_Clt.v_Descripcion = txb_descripcion.Text;

                    if (rb_inactivo.IsChecked == true)
                    {
                        v_Clt.v_EstadoSistema = "INACTIVO";
                    }
                    else
                    {
                        v_Clt.v_EstadoSistema = "ACTIVO";
                    }

                    int v_Resultado = v_Model.AgregarProveedores(v_Clt);
                    if (v_Resultado == -1)
                    {
                        MessageBox.Show("Datos ingresados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        btn_limpiar_Click(sender, e);
                        MostrarProveedoresExistentes();

                        v_EstadoSistema = "LISTAPROVEEDORES";
                        ListarProveedores();
                    }
                }
            }
            catch (Exception m)
            {
                Console.WriteLine(m.ToString());
                MessageBox.Show("Error al agregar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Botón el cual permite modificar un proveedor seleccionado, este botón posee las validaciones necesarias para la ejecución de su funcionalidad   
        private void btn_modificar_Click(object sender, RoutedEventArgs e)
        {
            if ((lbl_errorBusqueda.Visibility == Visibility.Visible || lbl_errorNombre.Visibility == Visibility.Visible) ||
                (lbl_errorTelefono.Visibility == Visibility.Visible || lbl_errorcorreo.Visibility == Visibility.Visible) ||
                (lbl_errorDesc.Visibility == Visibility.Visible || lbl_errorTelefonoOpcional.Visibility == Visibility.Visible) ||
                (lbl_errorcorreoOpcional.Visibility == Visibility.Visible || lbl_errorRb.Visibility == Visibility.Visible))
            {
                MessageBox.Show("Error al modificar\nHacen falta campos por rellenar o errores que corregir", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    v_Clt.v_CedulaJuridica = Convert.ToInt64(txb_cedJur.Text);
                    v_Clt.v_Nombre = txb_nombre.Text;
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

                    if (txb_correoOpcional.Text == "")
                    {
                        v_Clt.v_CorreoOpcional = "N/A";
                    }
                    else
                    {
                        v_Clt.v_CorreoOpcional = txb_correoOpcional.Text;
                    }

                    v_Clt.v_Descripcion = txb_descripcion.Text;

                    if (rb_inactivo.IsChecked == true)
                    {
                        v_Clt.v_EstadoSistema = "INACTIVO";
                    }
                    else
                    {
                        v_Clt.v_EstadoSistema = "ACTIVO";
                    }

                    if (v_Model.ValidarModificacionProveedores(v_Clt) == true)
                    {
                        lbl_errorCedJur.Visibility = Visibility.Collapsed;
                    }
                    if (lbl_errorCedJur.Visibility == Visibility.Visible)
                    {
                        MessageBox.Show("Error al modificar\nHacen falta campos por rellenar o errores que corregir", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (v_Model.ModificarProveedores(v_Clt) == -1)
                    {
                        MessageBox.Show("Datos modificados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        btn_limpiar_Click(sender, e);
                        v_Actividad_btnAgregar = true;
                        MostrarProveedoresExistentes();

                        v_EstadoSistema = "LISTAPROVEEDORES";
                        ListarProveedores();
                    }
                }
                catch (Exception m)
                {
                    Console.WriteLine(m.ToString());
                    MessageBox.Show("Error al modificar\nHacen falta campos por rellenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //Botón el cual cumple con la funcionalidad de eliminar todos los datos existentes en las cajas de texto situadas en el tab de gestión de proveedores
        private void btn_limpiar_Click(object sender, RoutedEventArgs e)
        {
            txb_cedJur.Text = "";
            txb_telefono.Text = "";
            txb_telefonoOpcional.Text = "";
            txb_busqueda.Text = "";
            txb_nombre.Text = "";
            txb_correo.Text = "";
            txb_correoOpcional.Text = "";
            txb_descripcion.Text = "";
            rb_activo.IsChecked = true;
            rb_inactivo.IsChecked = false;
            dtg_proveedores.ItemsSource = null;
            lbl_errorCedJur.Visibility = Visibility.Collapsed;
            lbl_errorBusqueda.Visibility = Visibility.Collapsed;
            lbl_errorNombre.Visibility = Visibility.Collapsed;
            lbl_errorTelefono.Visibility = Visibility.Collapsed;
            lbl_errorTelefonoOpcional.Visibility = Visibility.Collapsed;
            lbl_errorcorreo.Visibility = Visibility.Collapsed;
            lbl_errorcorreoOpcional.Visibility = Visibility.Collapsed;
            lbl_errorDesc.Visibility = Visibility.Collapsed;
            lbl_errorRb.Visibility = Visibility.Collapsed;
            btn_agregar.Visibility = Visibility.Collapsed;
            btn_modificar.Visibility = Visibility.Collapsed;
            v_Actividad_btnModificar = false;
            v_Actividad_btnAgregar = true;
        }

        //Botón el cual brinda con información necesaria para la utilización de la ventana en la que se encuentra el usuario
        private void btn_ayuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Mantenimiento de Proveedores: \n\n" +
               "1. Listar: Seleccione el rango de fechas y oprima el botón 'Listar' para desplegar los datos. \n\n" +
               "2. Buscar: Ingrese el elemento a buscar, puede ser por cédula jurídica o por nombre del proveedor. \n" +
               "   Si existe el proveedor se le deslegará los datos y podrá seleccionarlo para modificarlo o deshabilitarlo.\n" +
               "   Si no existe el proveedor se le permitirá agregarlo al sistema.\n\n" +
               "   Agregar: \n" +
               "   Complete todos los campos, excepto los opcionales.\n" +
               "   Las cajas de texto de los teléfonos y de la cédula jurídica solo permiten números.\n" +
               "   Formato de correo: usuario@dominio.extension, la extensión debe ser como máximo 3 caracteres. \n" +
               "   No ingrese caracteres especiales. \n" +
               "   El teléfono y el correo opcional se pueden dejar en blanco, mientras no tenga errores. \n\n" +
               "   Deshabilitar: \n" +
               "   Seleccione el elemento del datagrid e ingrese el motivo por el cual se desea deshabilitar. \n\n" +
               "   Actualizar: \n" +
               "   Seleccione el elemento y proceda a editar los campos que desee.\n" +
               "   Se utiliza el mismo formato de validaciones de ingresar. \n" +
               "   No deje campos vacíos, excepto los opcionales.", "Ayuda",
               MessageBoxButton.OK);
        }

        //Botón el cual permite al usuario cerrar la sesión en la que se encuentra y ser llevado a la ventana de Login 
        private void btn_usuario_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult v_Result = MessageBox.Show("¿Desea cerrar sesión?", "Cerrar Sesión", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (v_Result == MessageBoxResult.Yes)
            {
                Login v_Ventana = new Login();
                this.Close();
                v_Ventana.Show();
            }
        }

        /*Lista los proveedores existentes en el sistema según su estado, envía el estado en el sistema(v_EstadoSistema) que se 
         * obtiene del combobox "cmb_tipoBusqueda" con el cual se realizará la consulta.*/
        private void ListarProveedores()
        {
            if (cmb_tipoBusqueda.SelectedItem == null)
            {
                MessageBox.Show("Seleccione el tipo de búsqueda", "Búsqueda", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (v_Model.MostrarListaProveedores(v_EstadoSistema).Rows.Count == 0)
            {
                MessageBox.Show("No hay datos registrados", "Búsqueda", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                dtg_lista.ItemsSource = v_Model.MostrarListaProveedores(v_EstadoSistema).DefaultView;
            }
        }

        //Muestra el panel de búsqueda en el tab de configuración de proveedores
        private void MostrarProveedoresExistentes()
        {
            lbl_actividad.Content = "Proveedores existentes";
            grd_proveedoresExistentes.Visibility = Visibility.Visible;
            OcultarFormulario();
        }

        //Oculta el panel de búsqueda en el tab de configuración de proveedores
        private void OcultarProveedoresExistentes()
        {
            grd_proveedoresExistentes.Visibility = Visibility.Collapsed;
        }

        //Oculta el panel del formulario en el tab de configuración de proveedores
        private void OcultarFormulario()
        {
            grd_formularioProveedor.Visibility = Visibility.Collapsed;
        }

        //Muestra el panel del formulario en el tab de configuración de proveedores
        private void MostrarFormulario()
        {
            grd_formularioProveedor.Visibility = Visibility.Visible;
            OcultarProveedoresExistentes();
        }
               

        /*En esta caja de texto se implementa la búsqueda del proveedor que se desea, en caso de ser encontrado este despliega los datos en el DataGrid,
        de lo contrario se podrá agregar un nuevo proveedor*/
        private void txb_busqueda_KeyUp(object sender, KeyEventArgs e)
        {
            if (txb_busqueda.Text.Contains("'"))
            {
                lbl_errorBusqueda.Content = "No se permiten caracteres especiales";
                lbl_errorBusqueda.Visibility = Visibility.Visible;
            }
            else
            {
                dtg_proveedores.ItemsSource = v_Model.ValidarBusquedaProveedores(txb_busqueda.Text);
                dtg_proveedores.Columns[0].Header = "Código";
                dtg_proveedores.Columns[1].Header = "Cédula Jurídica";
                dtg_proveedores.Columns[2].Header = "Nombre del Proveedor";
                dtg_proveedores.Columns[3].Header = "Correo";
                dtg_proveedores.Columns[4].Header = "Correo Opcional";
                dtg_proveedores.Columns[6].Header = "Teléfono";
                dtg_proveedores.Columns[7].Header = "Tel. Opcional";
                dtg_proveedores.Columns[5].Header = "Descripción";
                dtg_proveedores.Columns[8].Header = "Fecha de Ingreso";
                dtg_proveedores.Columns[9].Header = "Estado en el Sistema";

                //Proveedores existentes
                v_Actividad_btnAgregar = false;
                btn_agregar.Visibility = Visibility.Collapsed;
                btn_modificar.Visibility = Visibility.Collapsed;
                lbl_actividad.Content = "Proveedores existentes";
                lbl_actividad.Visibility = Visibility.Visible;
            }
        }

        /*Método el cual cumple con la funcionalidad de desplegar los datos en los campos correspondientes de un proveedor seleccionado 
        en el DataGrid con la finalidad de ser modificado, esto en el panel del formulario en el tab de configuración de proveedores*/
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;

            v_Clt.v_IdProveedor = Convert.ToInt64((dtg_proveedores.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text);

            txb_cedJur.Text = (dtg_proveedores.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text;
            txb_nombre.Text = (dtg_proveedores.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;
            txb_correo.Text = (dtg_proveedores.SelectedCells[3].Column.GetCellContent(row) as TextBlock).Text;
            txb_correoOpcional.Text = (dtg_proveedores.SelectedCells[4].Column.GetCellContent(row) as TextBlock).Text;
            txb_telefono.Text = (dtg_proveedores.SelectedCells[6].Column.GetCellContent(row) as TextBlock).Text;
            txb_telefonoOpcional.Text = (dtg_proveedores.SelectedCells[7].Column.GetCellContent(row) as TextBlock).Text;
            txb_descripcion.Text = (dtg_proveedores.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text;
            if ((dtg_proveedores.SelectedCells[9].Column.GetCellContent(row) as TextBlock).Text == "ACTIVO")
            {
                rb_activo.IsChecked = true;
            }
            else if ((dtg_proveedores.SelectedCells[9].Column.GetCellContent(row) as TextBlock).Text == "INACTIVO")
            {
                rb_inactivo.IsChecked = true;
            }
            lbl_actividad.Content = "Modificar proveedor";
            btn_limpiar.Visibility = Visibility.Collapsed;
            v_Actividad_btnModificar = true;
            MostrarFormulario();
        }

        //Método el cual habilita el botón modificar
        private void HabilitarBtnModificar()
        {
            if (v_Actividad_btnModificar == true)
            {
                btn_modificar.Visibility = Visibility.Visible;
            }
        }

        //Método el cual habilita el botón agregar siempre y cuando los espacios correspondientes para esta actividad no estén vacíos  situados en el tab de configuración de proveedores
        private void HabilitarBtnAgregar()
        {
            if (v_Actividad_btnAgregar == true)
            {
                if (txb_cedJur.Text == "" || txb_correo.Text == "" || txb_nombre.Text == "" || txb_telefono.Text == "" || txb_descripcion.Text == "")
                {
                    btn_agregar.Visibility = Visibility.Collapsed;
                }
                else
                {
                    btn_agregar.Visibility = Visibility.Visible;
                }
            }
        }

        //Método el cual habilita componentes siempre y cuando no hayan errores en la caja de texto de buscar
        private void txb_busqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarErroresTxb(txb_busqueda, lbl_errorBusqueda, "");
        }

        //Método el cual valida si la cédula jurídica es existente o no, en caso de ser existente, mostrar un error
        private void ValidarTxbCedJur(object sender, EventArgs e)
        {
            string v_CedJur = txb_cedJur.Text;
            HabilitarBtnAgregar();
            HabilitarBtnModificar();
            ValidarErroresTxb(txb_cedJur, lbl_errorCedJur, "numeros");
            if (lbl_errorCedJur.Visibility == Visibility.Collapsed)
            {
                bool v_Resultado = v_Model.ValidarCedJurProveedores(txb_cedJur.Text);
                if (v_Resultado == true)
                {
                    lbl_errorCedJur.Content = "La cédula jurídica ya existe";
                    lbl_errorCedJur.Visibility = Visibility.Visible;
                }
                else
                {
                    lbl_errorCedJur.Visibility = Visibility.Collapsed;
                }
            }
        }

        //Validaciones en caja de texto de nombre
        private void txb_nombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_nombre, lbl_errorNombre, "nombre");
        }

        //Validaciones en caja de texto de teléfono
        private void txb_telefono_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_telefono, lbl_errorTelefono, "numeros");
        }

        //Validaciones en caja de texto de teléfono opcional
        private void txb_telefonoOpcional_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_telefonoOpcional, lbl_errorTelefonoOpcional, "numeros");
            if (txb_telefonoOpcional.Text == "")
            {
                lbl_errorTelefonoOpcional.Visibility = Visibility.Collapsed;
            }
            else if (txb_telefonoOpcional.Text == "0")
            {
                lbl_errorTelefonoOpcional.Visibility = Visibility.Collapsed;
            }
        }

        //Validaciones en caja de texto de correo
        private void txb_correo_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_correo, lbl_errorcorreo, "");
        }

        //Validaciones en caja de texto de correo opcional
        private void txb_correoOpcional_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_correoOpcional, lbl_errorcorreoOpcional, "");
            if (txb_correoOpcional.Text == "")
            {
                lbl_errorcorreoOpcional.Visibility = Visibility.Collapsed;
            }
        }

        //Validaciones en caja de texto de descripción
        private void txb_descripcion_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_descripcion, lbl_errorDesc, "descripcion");
        }

        //Validación en la actividad de los radiobutton
        private void rb_activo_Checked(object sender, RoutedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
        }

        //Validación en la actividad de los radiobutton
        private void rb_inactivo_Checked(object sender, RoutedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
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
            else if (v_Identificador == "nombre")
            {
                //caracteres que no permite si la cadena es de string
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

        //Validaciones en caja de texto de teléfono
        private void Telefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
            {
                e.Handled = false;
                lbl_errorTelefono.Visibility = Visibility.Collapsed;
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
                lbl_errorTelefonoOpcional.Visibility = Visibility.Collapsed;
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
        private void CedJur_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(e.Key.ToString().Substring(e.Key.ToString().Length - 1)[0]))
            {
                e.Handled = false;
                lbl_errorCedJur.Visibility = Visibility.Collapsed;
            }
            else
            {
                e.Handled = true;
                lbl_errorCedJur.Content = "No se permite ingresar letras";
                lbl_errorCedJur.Visibility = Visibility.Visible;
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
                    lbl_errorcorreo.Content = "Espacio vacío";
                    lbl_errorcorreo.Visibility = Visibility.Visible;
                }
                else
                {
                    lbl_errorcorreo.Content = "Formato: usuario@dominio.extension(Máx 3 caracteres)";
                    lbl_errorcorreo.Visibility = Visibility.Visible;
                }
            }
        }

        /*Método el cual valida si los parametros del correo electrónico opcional son correctos, en caso de que no los sean muestra un error al usuario
         y en caso de que este no posea (N/A) no mostrar errores al usuario */
        private void ValidarcorreoOpcional(object sender, EventArgs e)
        {
            Console.WriteLine("Correo: " + txb_correoOpcional.Text);
            Console.WriteLine(correoCorrecto(txb_correoOpcional.Text));
            if (txb_correoOpcional.Text == "N/A")
            {
                lbl_errorcorreoOpcional.Visibility = Visibility.Collapsed;
            }
            else if (correoCorrecto(txb_correoOpcional.Text) == false)
            {
                if (txb_correoOpcional.Text == "")
                {
                    lbl_errorcorreoOpcional.Visibility = Visibility.Collapsed;
                }
                else
                {
                    lbl_errorcorreoOpcional.Content = "Formato: usuario@dominio.extension(Máx 3 caracteres)";
                    lbl_errorcorreoOpcional.Visibility = Visibility.Visible;
                }
            }
        }

        //Método el cual recibe parametros necesarios para la validacion y la muestra de mensajes de erroes en las cajas de texto
        private void ValidarErroresTxb(TextBox txb_proveedor, Label lbl_error, string tipo)
        {
            string v_TamanoTxb = txb_proveedor.Text;
            if (txb_proveedor.Text == "")
            {
                lbl_error.Content = "Espacio vacío";
                lbl_error.Visibility = Visibility.Visible;
            }
            else if (txb_proveedor == txb_telefonoOpcional && txb_proveedor.Text == "")
            {
                lbl_error.Visibility = Visibility.Collapsed;
            }
            else if (txb_proveedor.Text == " ")
            {
                txb_proveedor.Text = "";
            }
            else if (txb_proveedor.Text.Contains("  "))
            {
                lbl_error.Content = "Parámetros incorrectos (espacios seguidos)";
                lbl_error.Visibility = Visibility.Visible;
            }
            else if (ValidarCaracteresEspeciales(txb_proveedor.Text, tipo) == true)
            {
                lbl_error.Content = "No se permiten caracteres especiales";
                lbl_error.Visibility = Visibility.Visible;
            }
            else if (txb_proveedor == txb_telefono || txb_proveedor == txb_telefonoOpcional)
            {
                if (v_TamanoTxb.Length < 8)
                {
                    lbl_error.Content = "Deben tener al menos 8 dígitos";
                    lbl_error.Visibility = Visibility.Visible;
                }
                else if (v_TamanoTxb.Length <= 12)
                {
                    if (ValidarCaracteresEspeciales(txb_proveedor.Text, tipo) == false)
                    {
                        lbl_error.Visibility = Visibility.Collapsed;
                    }
                }
            }
            else if (txb_proveedor == txb_cedJur)
            {
                if (v_TamanoTxb.Length < 9)
                {
                    lbl_error.Content = "Deben tener al menos 9 dígitos";
                    lbl_error.Visibility = Visibility.Visible;
                }
                else if (v_TamanoTxb.Length <= 12)
                {
                    if (ValidarCaracteresEspeciales(txb_proveedor.Text, tipo) == false)
                    {
                        lbl_error.Visibility = Visibility.Collapsed;
                    }
                }
            }
            else
            {
                lbl_error.Visibility = Visibility.Collapsed;
            }
        }

        //Recibe el filtro por el cual el usuario desea realizar el listado de proveedores y se lo asigna a la variable "v_EstadoSistema" 
        //para que se envie a data y realice la consulta respectiva.
        private void cmb_tipoBusqueda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_tipoBusqueda.SelectedValue == proveedoresActivos)
            {
                v_EstadoSistema = "ACTIVO";
            }
            else if (cmb_tipoBusqueda.SelectedValue == proveedoresInactivos)
            {
                v_EstadoSistema = "INACTIVO";
            }
            else if (cmb_tipoBusqueda.SelectedValue == listaProveedores)
            {
                v_EstadoSistema = "LISTAPROVEEDORES";
            }

            ListarProveedores();
        }

       
    }//fin de la clase
}//fin proyecto
