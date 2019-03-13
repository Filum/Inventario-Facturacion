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
            llenardtg();
            //Formato para la hora
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();


        }

        public void llenardtg()
        {

            dtg_lista.ItemsSource = v_Model.MostrarListaRoles().DefaultView;
          
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
            GridBuscar.Visibility = Visibility.Hidden;
            GridAgregar.Visibility = Visibility.Visible;

        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListViewItem_Selected_2(object sender, RoutedEventArgs e)
        {
            
        }

        //Botón el cual permite modificar un rol seleccionado, este botón posee validaciones según funcionalidad 
        private void btn_volver(object sender, RoutedEventArgs e)
        {
            GridAgregar.Visibility = Visibility.Hidden;
            GridBuscar.Visibility = Visibility.Visible;
            
        }

        //Botón encargado de limpiar toda la información ingresada y las opciones seleccionadas
      

        /*Botón el cual permite listar los proveedores existentes en el sistema según un rango de fechas establecidas siempre 
        cumpla con las validaciones necesarias para la ejecución de su funcionalidad situadas en el tab de listar*/
        private void btn_listar_roles_Click(object sender, RoutedEventArgs e)
        {
                   
                        dtg_lista.ItemsSource = v_Model.MostrarListaRoles().DefaultView;
                        dtg_lista.Columns[0].Header = "Nombre";
                        dtg_lista.Columns[1].Header = "Mantenimiento Clientes";
                        dtg_lista.Columns[2].Header = "Mantenimiento Proveedores";
                        dtg_lista.Columns[3].Header = "Mantenimiento Productos";
                        dtg_lista.Columns[4].Header = "Mantenimiento Usuarios";
                        dtg_lista.Columns[5].Header = "Mantenimiento Roles";
                        dtg_lista.Columns[6].Header = "Facturacion";
                        dtg_lista.Columns[7].Header = "Bitacora";
                    
             
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
       

        /*Método el cual cumple con la funcionalidad de desplegar los datos en los campos correspondientes de un rol seleccionado 
 en el DataGrid con la finalidad de ser modificado, esto en el tab de gestión de roles*/
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
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
        {
        }
        //Boton para garegar un rol
        private void btn_agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbl_error.Visibility == Visibility.Visible)
                {
                    MessageBox.Show("No se permiten caracteres especiales en el nombre");
                }
                else
                {
                    v_ER.v_Nombre = txb_nomrol.Text;

                    if (checkbox_mant_clientes.IsChecked == false)
                    {
                        v_ER.v_Mantenimiento_Clientes = "No";
                    }
                    else
                    {
                        v_ER.v_Mantenimiento_Clientes = "Si";
                    }
                    if (checkbox_mant_proveedores.IsChecked == false)
                    {
                        v_ER.v_Mantenimiento_Proveedores = "No";
                    }
                    else
                    {
                        v_ER.v_Mantenimiento_Proveedores = "Si";
                    }
                    if (checkbox_mant_productos.IsChecked == false)
                    {
                        v_ER.v_Mantenimiento_Productos = "No";
                    }
                    else
                    {
                        v_ER.v_Mantenimiento_Productos = "Si";
                    }
                    if (checkbox_mant_usuarios.IsChecked == false)
                    {
                        v_ER.v_Mantenimiento_Usuarios = "No";
                    }
                    else
                    {
                        v_ER.v_Mantenimiento_Usuarios = "Si";
                    }
                    if (checkbox_mant_roles.IsChecked == false)
                    {
                        v_ER.v_Mantenimiento_Roles = "No";
                    }
                    else
                    {
                        v_ER.v_Mantenimiento_Roles = "Si";
                    }
                    if (checkbox_facturacion.IsChecked == false)
                    {
                        v_ER.v_facturacion = "No";
                    }
                    else
                    {
                        v_ER.v_facturacion = "Si";
                    }
                    if (checkbox_bitacora.IsChecked == false)
                    {
                        v_ER.v_bitacora = "No";
                    }
                    else
                    {
                        v_ER.v_bitacora = "Si";
                    }
                    /*if (rb_inactivo.IsChecked == true)
                    {
                        v_Clt.v_EstadoSistema = "INACTIVO";
                    }
                    else
                    {
                        v_Clt.v_EstadoSistema = "ACTIVO";
                    }*/

                    int v_Resultado = v_Model.AgregarRoles(v_ER);
                    if (v_Resultado == -1)
                    {
                        MessageBox.Show("Datos ingresados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        limpiar();
                    }
                }
            }
            catch (Exception m)
            {
                Console.WriteLine(m.ToString());
                MessageBox.Show("Error al agregar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void limpiar()
        {
            checkbox_bitacora.IsChecked = false;
            checkbox_mant_proveedores.IsChecked = false;
            checkbox_mant_productos.IsChecked = false;
            checkbox_mant_clientes.IsChecked = false;
            checkbox_mant_roles.IsChecked = false;
            checkbox_mant_usuarios.IsChecked = false;
            txb_nomrol.Text = "";
        }

        private void Rb_inactivo_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Rb_activo_Checked(object sender, RoutedEventArgs e)
        {

        }
    }//fin de la clase
}//fin proyecto

