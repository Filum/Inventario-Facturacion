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
        bool v_Actividad_btnModificar = false;
        bool v_Actividad_btnAgregar = true;

        public MantenimientoRoles()
        {
            InitializeComponent();

            //Formato para la hora
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            date_inicio_roles.SelectedDate = DateTime.Now.Date;
            date_final_roles.SelectedDate = DateTime.Now.Date;
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
            Ventana.Show();
            this.Close();
        }

        //Botón el cual permite agregar un nuevo rol, este botón posee las validaciones necesarias para la ejecución de su funcionalidad         
        private void btn_agregar_rol_Click(object sender, RoutedEventArgs e)
        {

        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListViewItem_Selected_2(object sender, RoutedEventArgs e)
        {
            /* try
             {
                 if (lbl_errorCedJur.Visibility == Visibility.Visible || lbl_errorNombre.Visibility == Visibility.Visible || lbl_errorTelefono.Visibility == Visibility.Visible || lbl_errorTelefonoOpcional.Visibility == Visibility.Visible || lbl_errorcorreo.Visibility == Visibility.Visible || lbl_errorcorreoOpcional.Visibility == Visibility.Visible || lbl_errorDesc.Visibility == Visibility.Visible)
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

                     int v_Resultado = v_Model.AgregarProveedores(v_Clt);
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
             }*/
        }

        //Botón el cual permite modificar un rol seleccionado, este botón posee validaciones según funcionalidad 
        private void btn_modificar_roles_Click(object sender, RoutedEventArgs e)
        {
            /*  if ((lbl_errorBusqueda.Visibility == Visibility.Visible) || (lbl_errorNombre.Visibility == Visibility.Visible) ||
                 (lbl_errorTelefono.Visibility == Visibility.Visible || (lbl_errorcorreo.Visibility == Visibility.Visible)) ||
                 (lbl_errorDesc.Visibility == Visibility.Visible || lbl_errorTelefonoOpcional.Visibility == Visibility.Visible || lbl_errorcorreoOpcional.Visibility == Visibility.Visible))
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
                          inicializarAgregacion();
                      }
                  }
                  catch (Exception m)
                  {
                      Console.WriteLine(m.ToString());
                      MessageBox.Show("Error al modificar\nHacen falta campos por rellenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                  }
              }*/
        }

        //Botón encargado de limpiar toda la información ingresada y las opciones seleccionadas
        private void btn_limpiar_rol_Click(object sender, RoutedEventArgs e)
        {
            txb_nomrol.Text = " ";
            checkbox_mant_productos.IsChecked = false;
            checkbox_mant_usuarios.IsChecked = false;
            checkbox_mant_proveedores.IsChecked = false;
            checkbox_mant_roles.IsChecked = false;
        }

        /*Botón el cual permite listar los proveedores existentes en el sistema según un rango de fechas establecidas siempre 
        cumpla con las validaciones necesarias para la ejecución de su funcionalidad situadas en el tab de listar*/
        private void btn_listar_roles_Click(object sender, RoutedEventArgs e)
        {
            if (date_inicio_roles.SelectedDate != null && date_final_roles.SelectedDate != null)
            {
                DateTime v_FechaInicio = DateTime.Parse(date_inicio_roles.SelectedDate.Value.Date.ToShortDateString());
                DateTime v_FechaFinal = DateTime.Parse(date_final_roles.SelectedDate.Value.Date.ToShortDateString());
                String v_Fecha1;
                v_Fecha1 = date_inicio_roles.SelectedDate.Value.Date.ToShortDateString();
                String v_Fecha2;
                v_Fecha2 = date_final_roles.SelectedDate.Value.Date.ToShortDateString();
                dtg_lista.ItemsSource = null;

                if (v_FechaInicio > v_FechaFinal)
                {
                    MessageBox.Show("El rango de fechas es incorrecto\nLa fecha inicial no puede ser mayor a la final", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (v_Model.MostrarListaRoles(v_Fecha1, v_Fecha2).Rows.Count == 0)
                    {
                        MessageBox.Show("No hay datos registrados en el rango de fechas seleccionado", "Búsqueda", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        dtg_lista.ItemsSource = v_Model.MostrarListaRoles(v_Fecha1, v_Fecha2).DefaultView;
                        dtg_lista.Columns[0].Header = "Nombre";
                        dtg_lista.Columns[1].Header = "Mantenimientos";
                        dtg_lista.Columns[2].Header = "Estado";
                    }
                }
            }
        }

        //Botón de ayuda 
        private void btn_ayuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Mantenimiento de Roles: \n\n" +
                "1. Listar: Seleccione el rango de fechas y oprima el botón 'Listar' para desplegar los datos. \n\n" +
                "2. Buscar: Ingrese el elemento a buscar por nombre del rol. \n" +
                "   Si existe el rol se le deslegará los datos y podrá seleccionarlo para modificarlo o deshabilitarlo.\n" +
                "   Si no existe el rol se le permitirá agregarlo al sistema.\n\n" +
                "   Agregar: \n" +
                "   Complete todos los campos y marque los valores permitidos.\n" +
                "   No está permitido ingresar caracteres especiales \n\n" +
                "   Deshabilitar: \n" +
                "   Seleccione el elemento del datagrid e ingrese el motivo por el cual se desea deshabilitar. \n\n" +
                "   Actualizar: \n" +
                "   Seleccione el elemento y proceda a editar los campos que desee.\n" +
                "   Se utiliza el mismo formato de validaciones de ingresar. \n", "Ayuda",
                MessageBoxButton.OK);
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
        private void LimpiarTextboxAgregar()
        {
            txb_nomrol.Text = "";
            checkbox_mant_productos.IsChecked = false;
            checkbox_mant_usuarios.IsChecked = false;
            checkbox_mant_proveedores.IsChecked = false;
            checkbox_mant_roles.IsChecked = false;
            //lbl_errorCedJur.Visibility = Visibility.Collapsed;
            //lbl_errorNombre.Visibility = Visibility.Collapsed;
            v_Actividad_btnModificar = false;
            //inicializarAgregacion();
        }

        /*En esta caja de texto se implementa la búsqueda del proveedor que se desea, en caso de ser encontrado este despliega los datos en el DataGrid,
        en caso de que no se encuentre ningún proveedor se podrá agregar uno nuevo, esto en el tab de gestión de proveedores*/
        /*private void txb_busqueda_KeyUp(object sender, KeyEventArgs e)
        {
            LimpiarTextboxAgregar();

            if (txb_busqueda_rol.Text == "")
            {
                txb_nomrol.Text = "";
                btn_limpiar_rol_Click(sender, e);
                btn_agregar_rol.Visibility = Visibility.Collapsed;
                btn_modificar_roles.Visibility = Visibility.Collapsed;
            }
            else if (txb_busqueda_rol.Text.Contains("'"))
            {
                //lbl_errorBusqueda_rol.Content = "No se permiten caracteres especiales";
                //lbl_errorBusqueda.Visibility = Visibility.Visible;
            }
            else
            {
                dtg_roles.ItemsSource = v_Model.ValidarBusquedaProveedores(txb_busqueda_rol.Text);
                dtg_roles.Columns[0].Header = "Nombre";
                dtg_roles.Columns[1].Header = "Mantenimientos";
                dtg_roles.Columns[2].Header = "Estado";
                if (dtg_roles.Items.Count == 0)//El proveedor no existe
                {
                    btn_modificar_roles.Visibility = Visibility.Collapsed;
                    //inicializarAgregacion();

                    if (Regex.IsMatch(this.txb_busqueda_rol.Text, "[a-zA-Z]"))
                    {
                       /* if (ValidarCaracteresEspeciales(txb_busqueda.Text, "nombre") == true)
                        {
                            lbl_errorBusqueda.Content = "No se permiten caracteres especiales";
                            lbl_errorBusqueda.Visibility = Visibility.Visible;
                            txb_nombre.Text = "";
                            lbl_errorNombre.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            txb_nombre.Text = txb_busqueda.Text;
                            txb_cedJur.Text = "";
                            txb_busqueda.MaxLength = 35;
                            lbl_errorCedJur.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        if (ValidarCaracteresEspeciales(txb_busqueda.Text, "numeros") == true)
                        {
                            lbl_errorBusqueda.Content = "No se permiten caracteres especiales";
                            lbl_errorBusqueda.Visibility = Visibility.Visible;
                            txb_cedJur.Text = "";
                            lbl_errorCedJur.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            txb_cedJur.Text = txb_busqueda.Text;
                            txb_nombre.Text = "";
                            txb_busqueda.MaxLength = 12;
                        }
                    }
                }
                else//Proveedores existentes
                {
                    v_Actividad_btnAgregar = false;
                    btn_agregar.Visibility = Visibility.Collapsed;
                    btn_modificar.Visibility = Visibility.Collapsed;
                    lbl_actividad.Content = "Proveedores existentes";
                    lbl_actividad.Visibility = Visibility.Visible;
                }
            }
        }
        */
        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            Menu Ventana = new Menu();
            Ventana.Show();
            this.Close();
        }

        private void btn_buscar_rol_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {

        }

        private void btn_deshabilitar_Click(object sender, RoutedEventArgs e)
        {

        }

       /* private void btn_guardar_deshabilitar_rol_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_motivo_deshabilitar.Text == "")
            {
                MessageBox.Show("Ingrese el motivo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                MessageBox.Show("Rol Deshabilitado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        */
        

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txb_motivo_deshabilitar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void dtg_actualizar_roles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_editar_rol_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_guardar_rol_Click(object sender, EventArgs e)
        {


        }

        private void btn_limpiar_actualizar_rol_Click(object sender, RoutedEventArgs e)
        {
            txb_nomrol.Text = " ";
            checkbox_mant_productos.IsChecked = false;
            checkbox_mant_usuarios.IsChecked = false;
            checkbox_mant_proveedores.IsChecked = false;
            checkbox_mant_roles.IsChecked = false;
        }

        private void textbox_nombre_rol_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



      /*  private void btn_buscar_deshabilitar_rol_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_rol_modificar.Text == "")
            {
                MessageBox.Show("No hay datos que buscar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Buscando...");
            }
        }*/


       

 

        /*private void btn_limpiar_deshabilitar_rol_Click(object sender, RoutedEventArgs e)
        {
            textbox_motivo_deshabilitar.Text = " ";
        }*/

        private void textbox_motivo_deshabilitar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textbox_rol_deshabilitar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

 
    }
}
