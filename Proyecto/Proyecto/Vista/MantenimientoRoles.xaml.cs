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
        EntidadRoles v_ER = new EntidadRoles();
        Model v_Model = new Model();
        EntidadBitacora bitacora = new EntidadBitacora();
        String v_EstadoSistema = "";

        public string nombreUsuario;

        public MantenimientoRoles()
        {
           
            InitializeComponent();
            //Formato para la hora
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            cmb_tipoBusqueda.SelectedIndex = 2;
            dtg_lista.ItemsSource = v_Model.MostrarListaRoles("LISTAROLES").DefaultView;
            //dtg_roles.ItemsSource = v_Model.MostrarListaRoles("LISTAROLES").DefaultView;
        }

        public void llenardtg()
        {

            dtg_lista.ItemsSource = v_Model.MostrarListaRoles(v_EstadoSistema).DefaultView;
            
        }

        
       
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            lbl_fecha.Content = DateTime.Now.ToString();

        }

        //Minimiza ventana
        private void btn_minimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        //Cierra Ventana
        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Maximiza Ventana
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
        private void btn_salir_roles__Click(object sender, RoutedEventArgs e)
        {
            Menu Ventana = new Menu();
            Ventana.cargarMenu(nombreUsuario);
            Ventana.nombreUser = nombreUsuario;
            this.Close();
        }

         /*Botón el cual permite listar los proveedores existentes en el sistema según un rango de fechas establecidas siempre 
        cumpla con las validaciones necesarias para la ejecución de su funcionalidad situadas en el tab de listar*/
        private void btn_listar_roles_Click(object sender, RoutedEventArgs e)
        {
                   
                        dtg_lista.ItemsSource = v_Model.MostrarListaRoles(v_EstadoSistema).DefaultView;
                        dtg_lista.Columns[0].Header = "Nombre";
                        dtg_lista.Columns[1].Header = "Mantenimiento Clientes";
                        dtg_lista.Columns[2].Header = "Mantenimiento Proveedores";
                        dtg_lista.Columns[3].Header = "Mantenimiento Productos";
                        dtg_lista.Columns[4].Header = "Mantenimiento Usuarios";
                        dtg_lista.Columns[5].Header = "Mantenimiento Roles";
                        dtg_lista.Columns[6].Header = "Facturación";
                        dtg_lista.Columns[7].Header = "Bitácora";
                        dtg_lista.Columns[8].Header = "Estado en el Sistema";


        }

        //Botón de ayuda 
        private void btn_ayuda_Click(object sender, RoutedEventArgs e)
        {
            Vista.Ayuda ventana = new Vista.Ayuda();
            ventana.Show();
            ventana.Pantalla = "Roles";
            this.Close();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //Botón el cual permite agregar un nuevo rol, este botón posee las validaciones necesarias para la ejecución de su funcionalidad         
        private void btn_agregar_rol_Click(object sender, RoutedEventArgs e)
        {
            btn_agregar.Visibility = Visibility.Visible;
            btn_modificar_roles.Visibility = Visibility.Collapsed;
            lbl_actividad2.Content = "Agregar Rol";
            GridBuscar.Visibility = Visibility.Hidden;
            GridAgregar.Visibility = Visibility.Visible;
            lbl_error.Visibility = Visibility.Collapsed;
            lbl_errorCB.Visibility = Visibility.Collapsed;
            lbl_errorRB.Visibility = Visibility.Collapsed;
            rb_activo.IsChecked = true;
        }

        //Botón el cual permite modificar un rol seleccionado, este botón posee validaciones según funcionalidad 
        private void btn_volver(object sender, RoutedEventArgs e)
        {
            GridAgregar.Visibility = Visibility.Hidden;
            GridBuscar.Visibility = Visibility.Visible;
            limpiar();
        }

        // Botón para cerrar sesión
        private void btn_usuario_roles_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult v_Result = MessageBox.Show("¿Desea cerrar sesión?", "Cerrar Sesión", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (v_Result == MessageBoxResult.Yes)
            {
                Login v_Ventana = new Login();
                this.Close();
                v_Ventana.Show();
            }
        }
        /*Botón el cual cumple con la funcionalidad de eliminar todos los datos existentes en las cajas de texto a la hora de agregar proveedores
       situadas en el tab de gestión de roles*/
       
        /*Método el cual cumple con la funcionalidad de desplegar los datos en los campos correspondientes de un rol seleccionado 
 en el DataGrid con la finalidad de ser modificado, esto en el tab de gestión de roles*/
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            GridBuscar.Visibility = Visibility.Collapsed;
            GridAgregar.Visibility = Visibility.Visible;

            btn_modificar_roles.Visibility = Visibility.Visible;
            
            DataGridRow row = sender as DataGridRow;

            v_ER.v_IdRol = Convert.ToInt64((dtg_roles.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text);
            
            txb_nomrol.Text = (dtg_roles.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text;
            if ((dtg_roles.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text == "ACTIVO")
            {
                rb_activo .IsChecked = true;
            }
            else if ((dtg_roles.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text == "INACTIVO")
            {
                rb_inactivo.IsChecked = true;
            }




            if((dtg_roles.SelectedCells[3].Column.GetCellContent(row) as TextBlock).Text == "✓")
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
                checkbox_mant_usuarios.IsChecked=true;
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

            HabilitarComponentes();
            lbl_actividad2.Content = "Modificar Rol";
            
            
            
        }
        

        //Método el cual habilita componentes siempre y cuando no hayan errores en la caja de texto de buscar
        private void txb_busqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarErroresTxb(txb_busqueda_rol, lbl_errorNomrol, "");
        }

        //Validaciones en caja de texto de nombre
        private void txb_nomrol_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarErroresTxb(txb_busqueda_rol, lbl_error, "");  
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




        //Método el cual recibe parametros necesarios para la validacion y la muestra de mensajes de erroes en las cajas de texto
        private void ValidarErroresTxb(TextBox txb_nombreRol, Label lbl_error, string tipo)
        {
            if (txb_nomrol.Text == "")
            {
                lbl_error.Content = "Espacio vacío";
                lbl_error.Visibility = Visibility.Visible;
            }else if(txb_nomrol.Text != "") 
            {
                lbl_error.Visibility = Visibility.Collapsed;
            }
            else if (txb_nomrol.Text == " ")
            {
                txb_nombreRol.Text = "";
            }
            else if (txb_nomrol.Text.Contains("  "))
            {
                lbl_error.Content = "Parámetros incorrectos (espacios seguidos)";
                lbl_error.Visibility = Visibility.Visible;
            }
            else if (ValidarCaracteresEspeciales(txb_nomrol.Text, tipo) == true)
            {
                lbl_error.Content = "No se permiten caracteres especiales";
                lbl_error.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_error.Visibility = Visibility.Collapsed;
            }
        }




        //Se validan los checkbox
        private void ValidarComponentes()
        {
            ValidarErroresTxb(txb_nomrol, lbl_error, "numeros");

            if (rb_inactivo.IsChecked == false && rb_activo.IsChecked == false)
            {
                rb_activo.IsChecked = true;
            }
            
            if (checkbox_mant_clientes.IsChecked == false && checkbox_facturacion.IsChecked == false && checkbox_bitacora.IsChecked == false && checkbox_mant_productos.IsChecked == false && checkbox_mant_proveedores.IsChecked == false && checkbox_mant_roles.IsChecked == false && checkbox_mant_usuarios.IsChecked == false)
            {
                lbl_errorCB.Content = "De seleccionar al menos un mantenimiento";
                lbl_errorCB.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_errorCB.Visibility = Visibility.Collapsed;
            }
        }
        
        //Boton para garegar un rol
        private void btn_agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidarComponentes();
               

                if (lbl_error.Visibility == Visibility.Visible || lbl_errorCB.Visibility == Visibility.Visible || lbl_errorRB.Visibility == Visibility.Visible)
                {
                    MessageBox.Show("No se puede agregar un rol nuevo \nHay errores por corregir.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {

                    v_ER.v_Nombre = txb_nomrol.Text;



                    if (checkbox_mant_clientes.IsChecked == false)
                    {
                        v_ER.v_Mantenimiento_Clientes = "X";
                    }
                    else
                    {
                        v_ER.v_Mantenimiento_Clientes = "✓";
                    }
                    if (checkbox_mant_proveedores.IsChecked == false)
                    {
                        v_ER.v_Mantenimiento_Proveedores = "X";
                    }
                    else
                    {
                        v_ER.v_Mantenimiento_Proveedores = "✓";
                    }
                    if (checkbox_mant_productos.IsChecked == false)
                    {
                        v_ER.v_Mantenimiento_Productos = "X";
                    }
                    else
                    {
                        v_ER.v_Mantenimiento_Productos = "✓";
                    }
                    if (checkbox_mant_usuarios.IsChecked == false)
                    {
                        v_ER.v_Mantenimiento_Usuarios = "X";
                    }
                    else
                    {
                        v_ER.v_Mantenimiento_Usuarios = "✓";
                    }
                    if (checkbox_mant_roles.IsChecked == false)
                    {
                        v_ER.v_Mantenimiento_Roles = "X";
                    }
                    else
                    {
                        v_ER.v_Mantenimiento_Roles = "✓";
                    }
                    if (checkbox_facturacion.IsChecked == false)
                    {
                        v_ER.v_facturacion = "X";
                    }
                    else
                    {
                        v_ER.v_facturacion = "✓";
                    }
                    if (checkbox_bitacora.IsChecked == false)
                    {
                        v_ER.v_bitacora = "X";
                    }
                    else
                    {
                        v_ER.v_bitacora = "✓";
                    }
                    if (rb_inactivo.IsChecked == true)
                    {
                        v_ER.v_Estado = "INACTIVO";
                    }
                    else
                    {
                        v_ER.v_Estado = "ACTIVO";
                    }


                    int v_Resultado = v_Model.AgregarRoles(v_ER);
                    if (v_Resultado == -1)
                    {
                        MessageBox.Show("Datos ingresados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        bitacora.usuario_Responsable = t_Usuario.Text;
                        bitacora.accion = "Agregó";
                        bitacora.ventana_Mantenimiento = "Mantenimieto Roles";
                        bitacora.descripcion = "Agregó el Rol con el nombre: " + txb_nomrol.Text;
                        v_Model.AgregarBitacora(bitacora);


                        limpiar();
                        lbl_errorRB.Visibility = Visibility.Collapsed;
                        lbl_error.Visibility = Visibility.Collapsed;
                        lbl_errorCB.Visibility = Visibility.Collapsed;
                        GridAgregar.Visibility = Visibility.Collapsed;
                        GridBuscar.Visibility = Visibility.Visible;
                        txb_busqueda_rol.Text = "";
                    }
                }
                
            }
            catch (Exception m)
            {
                Console.WriteLine(m.ToString());
                MessageBox.Show("Error al agregar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        public void limpiar()
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
            txb_nomrol.Text = "";
            lbl_error.Content = "";
            lbl_errorCB.Content = "";
            lbl_errorRB.Content = "";
            lbl_error.Visibility = Visibility.Collapsed;
            lbl_errorCB.Visibility = Visibility.Collapsed;
            lbl_errorRB.Visibility = Visibility.Collapsed;
        }

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
        }

        private void btn_modificar_roles_Click(object sender, RoutedEventArgs e)
        {/*
            if ((lbl_errorBusqueda.Visibility == Visibility.Visible || lbl_errorNombre.Visibility == Visibility.Visible) ||
               (lbl_errorTelefono.Visibility == Visibility.Visible || lbl_errorcorreo.Visibility == Visibility.Visible) ||
               (lbl_errorDesc.Visibility == Visibility.Visible || lbl_errorTelefonoOpcional.Visibility == Visibility.Visible) ||
               (lbl_errorcorreoOpcional.Visibility == Visibility.Visible || lbl_errorRb.Visibility == Visibility.Visible))
            {
                MessageBox.Show("Error al modificar\nHacen falta campos por rellenar o errores que corregir", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {*/


            ValidarComponentes();

            if (lbl_error.Visibility == Visibility.Visible || lbl_errorCB.Visibility == Visibility.Visible || lbl_errorRB.Visibility == Visibility.Visible)
            {
                MessageBox.Show("No se puede agregar un rol nuevo \nHay errores por corregir.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {


                    v_ER.v_Nombre = txb_nomrol.Text;
                    if (checkbox_mant_clientes.IsChecked == false)
                    {
                        v_ER.v_Mantenimiento_Clientes = "X";
                    }
                    else
                    {
                        v_ER.v_Mantenimiento_Clientes = "✓";
                    }
                    if (checkbox_mant_proveedores.IsChecked == false)
                    {
                        v_ER.v_Mantenimiento_Proveedores = "X";
                    }
                    else
                    {
                        v_ER.v_Mantenimiento_Proveedores = "✓";
                    }
                    if (checkbox_mant_productos.IsChecked == false)
                    {
                        v_ER.v_Mantenimiento_Productos = "X";
                    }
                    else
                    {
                        v_ER.v_Mantenimiento_Productos = "✓";
                    }
                    if (checkbox_mant_usuarios.IsChecked == false)
                    {
                        v_ER.v_Mantenimiento_Usuarios = "X";
                    }
                    else
                    {
                        v_ER.v_Mantenimiento_Usuarios = "✓";
                    }
                    if (checkbox_mant_roles.IsChecked == false)
                    {
                        v_ER.v_Mantenimiento_Roles = "X";
                    }
                    else
                    {
                        v_ER.v_Mantenimiento_Roles = "✓";
                    }
                    if (checkbox_facturacion.IsChecked == false)
                    {
                        v_ER.v_facturacion = "X";
                    }
                    else
                    {
                        v_ER.v_facturacion = "✓";
                    }
                    if (checkbox_bitacora.IsChecked == false)
                    {
                        v_ER.v_bitacora = "X";
                    }
                    else
                    {
                        v_ER.v_bitacora = "✓";
                    }
                    if (rb_inactivo.IsChecked == true)
                    {
                        v_ER.v_Estado = "INACTIVO";
                    }
                    else
                    {
                        v_ER.v_Estado = "ACTIVO";
                    }

                    if (v_Model.ModificarRoles(v_ER) == -1)
                    {
                        MessageBox.Show("Datos modificados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        bitacora.usuario_Responsable = t_Usuario.Text;
                        bitacora.accion = "Modificó";
                        bitacora.ventana_Mantenimiento = "Mantenimieto Roles";
                        bitacora.descripcion = "Modificó el Rol con el nombre: " + txb_nomrol.Text;
                        v_Model.AgregarBitacora(bitacora);

                        GridBuscar.Visibility = Visibility.Visible;
                        GridAgregar.Visibility = Visibility.Collapsed;
                        btn_agregar.Visibility = Visibility.Visible;
                        btn_modificar_roles.Visibility = Visibility.Collapsed;
                        limpiar();
                        txb_busqueda_rol.Text = "";
                    }
                }
                catch (Exception m)
                {
                    Console.WriteLine(m.ToString());
                    MessageBox.Show("Error al modificar\nHacen falta campos por rellenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }
        

        private void checkbox_mant_usuarios_Checked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
        }

        private void txb_busqueda_rol_KeyUp(object sender, KeyEventArgs e)
        {
            if (txb_busqueda_rol.Text.Contains("'"))
            {
               lbl_errorNomrol.Content = "No se permiten caracteres especiales";
               lbl_errorNomrol.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_errorNomrol.Visibility = Visibility.Collapsed;
                dtg_roles.ItemsSource = v_Model.ValidarBusquedaRoles(txb_busqueda_rol.Text);
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

                //Proveedores existentes
                btn_agregar.Visibility = Visibility.Collapsed;
                lbl_actividad.Visibility = Visibility.Visible;
            }
        }

        private void checkbox_mant_productos_Checked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
        }

        private void checkbox_mant_proveedores_Checked(object sender, RoutedEventArgs e)
        {
           ValidarComponentes();
        }

        private void checkbox_mant_roles_Checked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
        }

        private void checkbox_facturacion_Checked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
        }

        private void checkbox_mant_clientes_Checked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
        }

        private void checkbox_bitacora_Checked(object sender, RoutedEventArgs e)
        {
            ValidarComponentes();
        }

        private void DeshabilitarComponentes()
        {
            txb_nomrol.IsEnabled = false;
            checkbox_mant_clientes.IsEnabled = false;
            checkbox_mant_productos.IsEnabled = false;
            checkbox_mant_proveedores.IsEnabled = false;
            checkbox_mant_roles.IsEnabled = false;
            checkbox_mant_usuarios.IsEnabled = false;
            checkbox_facturacion.IsEnabled = false;
            checkbox_bitacora.IsEnabled = false;
            rb_activo.IsEnabled = false;
            rb_inactivo.IsEnabled = false;
        }

        private void HabilitarComponentes()
        {
            txb_nomrol.IsEnabled = true;
            checkbox_mant_clientes.IsEnabled = true;
            checkbox_mant_productos.IsEnabled = true;
            checkbox_mant_proveedores.IsEnabled = true;
            checkbox_mant_roles.IsEnabled = true;
            checkbox_mant_usuarios.IsEnabled = true;
            checkbox_facturacion.IsEnabled = true;
            checkbox_bitacora.IsEnabled = true;
            rb_activo.IsEnabled = true;
            rb_inactivo.IsEnabled = true;
        }

        private void Btn_limpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }
    }//fin de la clase
}//fin proyecto

