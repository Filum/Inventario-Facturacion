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
            //if (date_inicio_roles.SelectedDate != null && date_final_roles.SelectedDate != null)
            //{
            //    DateTime v_FechaInicio = DateTime.Parse(date_inicio_roles.SelectedDate.Value.Date.ToShortDateString());
            //    DateTime v_FechaFinal = DateTime.Parse(date_final_roles.SelectedDate.Value.Date.ToShortDateString());
            //    String v_Fecha1;
            //    v_Fecha1 = date_inicio_roles.SelectedDate.Value.Date.ToShortDateString();
            //    String v_Fecha2;
            //    v_Fecha2 = date_final_roles.SelectedDate.Value.Date.ToShortDateString();
            //    dtg_lista.ItemsSource = null;

            //    if (v_FechaInicio > v_FechaFinal)
            //    {
            //        MessageBox.Show("El rango de fechas es incorrecto\nLa fecha inicial no puede ser mayor a la final", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //    else
            //    {
            //        if (v_Model.MostrarListaRoles(v_Fecha1, v_Fecha2).Rows.Count == 0)
            //        {
            //            MessageBox.Show("No hay datos registrados en el rango de fechas seleccionado", "Búsqueda", MessageBoxButton.OK, MessageBoxImage.Warning);
            //        }
            //        else
            //        {
            //            dtg_lista.ItemsSource = v_Model.MostrarListaRoles(v_Fecha1, v_Fecha2).DefaultView;
            //            dtg_lista.Columns[0].Header = "Nombre";
            //            dtg_lista.Columns[1].Header = "Mantenimientos";
            //            dtg_lista.Columns[2].Header = "Estado";
            //        }
            //    }
            //}
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

        /*En esta caja de texto se implementa la búsqueda del rol que se desea, en caso de ser encontrado este despliega los datos en el DataGrid,
        en caso de que no se encuentre ningún rol se podrá agregar uno nuevo, esto en el tab de gestión de roles*/
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

        /*Método el cual cumple con la funcionalidad de desplegar los datos en los campos correspondientes de un rol seleccionado 
 en el DataGrid con la finalidad de ser modificado, esto en el tab de gestión de roles*/
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {/*
            DataGridRow row = sender as DataGridRow;

            v_Clt.v_IdProveedor = Convert.ToInt64((dtg_proveedores.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text);

            txb_cedJur.Text = (dtg_proveedores.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text;
            txb_nombre.Text = (dtg_proveedores.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;
            txb_correo.Text = (dtg_proveedores.SelectedCells[3].Column.GetCellContent(row) as TextBlock).Text;
            txb_correoOpcional.Text = (dtg_proveedores.SelectedCells[4].Column.GetCellContent(row) as TextBlock).Text;
            txb_telefono.Text = (dtg_proveedores.SelectedCells[6].Column.GetCellContent(row) as TextBlock).Text;
            txb_telefonoOpcional.Text = (dtg_proveedores.SelectedCells[7].Column.GetCellContent(row) as TextBlock).Text;
            txb_descripcion.Text = (dtg_proveedores.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text;

            lbl_actividad.Content = "Modificar proveedor";
            lbl_actividad.Visibility = Visibility.Visible;
            v_Actividad_btnModificar = true;
            v_Actividad_btnAgregar = false;*/
        }

        //Método el cual habilita el botón modificar
        private void HabilitarBtnModificar()
        {
            if (v_Actividad_btnModificar == true)
            {
                btn_modificar_roles.Visibility = Visibility.Visible;
            }
        }

        //Inicializa las opciones de agregar en la ventana 
        private void inicializarAgregacion()
        {
            v_Actividad_btnAgregar = true;
            lbl_actividad.Content = "Agregar Rol";
            lbl_actividad.Visibility = Visibility.Visible;
        }

        //Método el cual habilita el botón agregar siempre y cuando los espacios correspondientes para esta actividad no estén vacíos  situados en el tab de gestión de proveedores
        private void HabilitarBtnAgregar()
        {
            if (v_Actividad_btnAgregar == true)
            {
                if (txb_nomrol.Text != "" && checkbox_mant_proveedores.IsChecked != false || checkbox_mant_productos.IsChecked != false || checkbox_mant_usuarios.IsChecked != false || checkbox_mant_roles.IsChecked != false)
                {
                    btn_agregar_rol.Visibility = Visibility.Visible;
                }
                else
                {
                    btn_agregar_rol.Visibility = Visibility.Collapsed;
                }
            }
        }


        //Método el cual habilita componentes siempre y cuando no hayan errores en la caja de texto de buscar
        private void txb_busqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarErroresTxb(txb_busqueda_rol, lbl_errorBusqueda_rol, "");
        }

        /*//Método el cual valida si la cédula jurídica es existente o no, en caso de ser existente, mostrar un error
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
        }*/

        //Validaciones en caja de texto de nombre
        private void txb_nomrol_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_nomrol, lbl_errorNomrol, "nombre");
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
        private void ValidarErroresTxb(TextBox txb_proveedor, Label lbl_error, string tipo)
        {/*
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
            else if (txb_busqueda_rol == txb_nomrol)
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
            }*/
        }
    }//fin de la clase
}//fin proyecto

