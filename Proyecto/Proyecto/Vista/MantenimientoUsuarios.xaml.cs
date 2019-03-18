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
        public MantenimientoUsuarios()
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
            txb_email.Text = "";
            txb_telefono.Text = "";
            txb_telefonoOpcional.Text = "";
            txb_contrasenna.Text = "";
            txb_puesto.Text = "";
            cmb_rol.Text = "";
            txb_usuario.Text = "";
            rb_activo.IsChecked = false;
            rb_inactivo.IsChecked = false;
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

        private void btn_volver_Click(object sender, RoutedEventArgs e)
        {
            MostrarUsuariosExistentes();
        }

        private void btn_agregarUsuario_Click(object sender, RoutedEventArgs e)
        {
            MostrarFormulario();
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
            OcultarUsuariosExistentes();
            grd_formularioUsuario.Visibility = Visibility.Visible;
        }

        private void rb_activo_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rb_inactivo_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_guardarUsuario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txb_busqueda_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}


