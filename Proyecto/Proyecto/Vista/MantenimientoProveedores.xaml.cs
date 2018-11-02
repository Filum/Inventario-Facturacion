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
        bool v_Actividad_btnAgregar = false;
        public MantenimientoProveedores()
        {
            InitializeComponent();
            //Formato para la hora
            System.Windows.Threading.DispatcherTimer v_DispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            v_DispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            v_DispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            v_DispatcherTimer.Start();

            date_inicio.SelectedDate = DateTime.Now.Date;
            date_final.SelectedDate = DateTime.Now.Date;
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            lbl_fecha.Content = DateTime.Now.ToString();

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

        private void btn_salir_proveedores__Click(object sender, RoutedEventArgs e)
        {
            Menu v_Ventana = new Menu();
            v_Ventana.Show();
            this.Close();
        }

        private void btn_agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbl_errorCedJur.Visibility == Visibility.Visible || lbl_errorNombre.Visibility == Visibility.Visible || lbl_errorTelefono.Visibility == Visibility.Visible || lbl_errorTelefonoOpcional.Visibility == Visibility.Visible || lbl_errorEmail.Visibility == Visibility.Visible || lbl_errorEmailOpcional.Visibility == Visibility.Visible || lbl_errorDesc.Visibility == Visibility.Visible)
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

                    v_Clt.v_Correo = txb_email.Text;

                    if (txb_emailOpcional.Text == "")
                    {
                        v_Clt.v_CorreoOpcional = "N/A";
                    }
                    else
                    {
                        v_Clt.v_CorreoOpcional = txb_emailOpcional.Text;
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
            }
        }

        private void btn_modificar_Click(object sender, RoutedEventArgs e)
        {
            if ((lbl_errorBusqueda.Visibility == Visibility.Visible) || (lbl_errorNombre.Visibility == Visibility.Visible) ||
                (lbl_errorTelefono.Visibility == Visibility.Visible || (lbl_errorEmail.Visibility == Visibility.Visible)) ||
                (lbl_errorDesc.Visibility == Visibility.Visible || lbl_errorTelefonoOpcional.Visibility == Visibility.Visible || lbl_errorEmailOpcional.Visibility == Visibility.Visible))
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

                    v_Clt.v_Correo = txb_email.Text;

                    if (txb_emailOpcional.Text == "")
                    {
                        v_Clt.v_CorreoOpcional = "N/A";
                    }
                    else
                    {
                        v_Clt.v_CorreoOpcional = txb_emailOpcional.Text;
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
                    }
                }
                catch (Exception m)
                {
                    Console.WriteLine(m.ToString());
                    MessageBox.Show("Error al modificar\nHacen falta campos por rellenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }    
        }

        private void btn_limpiar_Click(object sender, RoutedEventArgs e)
        {
            txb_cedJur.Text = "";
            txb_telefono.Text = "";
            txb_telefonoOpcional.Text = "";
            txb_busqueda.Text = "";
            txb_nombre.Text = "";
            txb_email.Text = "";
            txb_emailOpcional.Text = "";
            txb_descripcion.Text = "";
            dtg_proveedores.ItemsSource = null;
            lbl_errorCedJur.Visibility = Visibility.Collapsed;
            lbl_errorBusqueda.Visibility = Visibility.Collapsed;
            lbl_errorNombre.Visibility = Visibility.Collapsed;
            lbl_errorTelefono.Visibility = Visibility.Collapsed;
            lbl_errorTelefonoOpcional.Visibility = Visibility.Collapsed;
            lbl_errorEmail.Visibility = Visibility.Collapsed;
            lbl_errorEmailOpcional.Visibility = Visibility.Collapsed;
            lbl_errorDesc.Visibility = Visibility.Collapsed;
            lbl_actividad.Visibility = Visibility.Collapsed;
            btn_agregar.Visibility = Visibility.Collapsed;
            btn_modificar.Visibility = Visibility.Collapsed;
            v_Actividad_btnModificar = false;
            v_Actividad_btnAgregar = false;
            DeshabilitarComponentes();
        }

        private void btn_listar_Click(object sender, RoutedEventArgs e)
        {
            if (date_inicio.SelectedDate != null && date_final.SelectedDate != null)
            {
                DateTime v_FechaInicio = DateTime.Parse(date_inicio.SelectedDate.Value.Date.ToShortDateString());
                DateTime v_FechaFinal = DateTime.Parse(date_final.SelectedDate.Value.Date.ToShortDateString());
                String v_Fecha1;
                v_Fecha1 = date_inicio.SelectedDate.Value.Date.ToShortDateString();
                String v_Fecha2;
                v_Fecha2 = date_final.SelectedDate.Value.Date.ToShortDateString();
                dtg_lista.ItemsSource = null;

                if (v_FechaInicio > v_FechaFinal)
                {
                    MessageBox.Show("El rango de fechas es incorrecto\nLa fecha inicial no puede ser mayor a la final", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    date_inicio.Text = "";
                    date_final.Text = "";
                }
                else
                {
                    if(v_Model.MostrarListaProveedores(v_Fecha1, v_Fecha2).Rows.Count == 0)
                    {
                        MessageBox.Show("No hay datos registrados en el rango de fechas seleccionado", "Búsqueda", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        dtg_lista.ItemsSource = v_Model.MostrarListaProveedores(v_Fecha1, v_Fecha2).DefaultView; 
                        dtg_lista.Columns[0].Header = "Cédula Jurídica";
                        dtg_lista.Columns[1].Header = "Nombre del Proveedor";
                        dtg_lista.Columns[2].Header = "Correo";
                        dtg_lista.Columns[3].Header = "Correo Opcional";
                        dtg_lista.Columns[4].Header = "Teléfono";
                        dtg_lista.Columns[5].Header = "Tel. Opcional";
                        dtg_lista.Columns[6].Header = "Descripción";
                        dtg_lista.Columns[7].Header = "Fecha de Ingreso";
                    }
                }
            }            
        }

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

        private void LimpiarTextbox()
        {
            txb_cedJur.Text = "";
            txb_telefono.Text = "";
            txb_telefonoOpcional.Text = "";
            txb_nombre.Text = "";
            txb_email.Text = "";
            txb_emailOpcional.Text = "";
            txb_descripcion.Text = "";
            lbl_errorCedJur.Visibility = Visibility.Collapsed;
            lbl_errorNombre.Visibility = Visibility.Collapsed;
            lbl_errorTelefono.Visibility = Visibility.Collapsed;
            lbl_errorTelefonoOpcional.Visibility = Visibility.Collapsed;
            lbl_errorEmail.Visibility = Visibility.Collapsed;
            lbl_errorEmailOpcional.Visibility = Visibility.Collapsed;
            lbl_errorDesc.Visibility = Visibility.Collapsed;
            lbl_actividad.Visibility = Visibility.Collapsed;
            v_Actividad_btnAgregar = false;
        }
     
        private void txb_busqueda_KeyUp(object sender, KeyEventArgs e)
        {
            LimpiarTextbox();
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

            if (txb_busqueda.Text == "")
            {
                txb_nombre.Text = "";
                DeshabilitarComponentes();
                btn_limpiar_Click(sender,e);
                btn_agregar.Visibility = Visibility.Collapsed;
                btn_modificar.Visibility = Visibility.Collapsed;
                lbl_actividad.Visibility = Visibility.Collapsed;

            }
            else// el usuario no existe
            {
                if (dtg_proveedores.Items.Count == 0)
                {
                    HabilitarComponentes();
                    v_Actividad_btnAgregar = true;
                    btn_modificar.Visibility = Visibility.Collapsed;
                    lbl_actividad.Content = "Agregar proveedor";
                    lbl_actividad.Visibility = Visibility.Visible;

                    
                    if (Regex.IsMatch(this.txb_busqueda.Text, "[a-zA-Z]"))
                    {
                        if (ValidarCaracteresEspeciales(txb_busqueda.Text, "nombre") == true)
                        { 
                            lbl_errorBusqueda.Content = "No se permiten caracteres especiales";
                            lbl_errorBusqueda.Visibility = Visibility.Visible;
                            txb_nombre.Text = "";
                            lbl_errorNombre.Visibility = Visibility.Collapsed;
                            DeshabilitarComponentes();
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
                            DeshabilitarComponentes();
                        }
                        else
                        {
                            txb_cedJur.Text = txb_busqueda.Text;
                            txb_nombre.Text = "";
                            txb_busqueda.MaxLength = 12;
                        }
                    }
                }
                else
                {
                    btn_agregar.Visibility = Visibility.Collapsed;
                    btn_modificar.Visibility = Visibility.Collapsed;
                    lbl_actividad.Content = "Proveedores existentes";
                    lbl_actividad.Visibility = Visibility.Visible;
                    DeshabilitarComponentes();                    
                }
            }
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;

            v_Clt.v_IdProveedor = Convert.ToInt64((dtg_proveedores.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text);

            txb_cedJur.Text = (dtg_proveedores.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text;
            txb_nombre.Text = (dtg_proveedores.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;
            txb_email.Text = (dtg_proveedores.SelectedCells[3].Column.GetCellContent(row) as TextBlock).Text;
            txb_emailOpcional.Text = (dtg_proveedores.SelectedCells[4].Column.GetCellContent(row) as TextBlock).Text;
            txb_telefono.Text = (dtg_proveedores.SelectedCells[6].Column.GetCellContent(row) as TextBlock).Text;
            txb_telefonoOpcional.Text = (dtg_proveedores.SelectedCells[7].Column.GetCellContent(row) as TextBlock).Text;
            txb_descripcion.Text = (dtg_proveedores.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text;

            lbl_actividad.Content = "Modificar proveedor";
            lbl_actividad.Visibility = Visibility.Visible;
            v_Actividad_btnModificar = true;
            HabilitarComponentes();
        }

        public void DeshabilitarComponentes()
        {
            txb_cedJur.IsEnabled = false;
            txb_nombre.IsEnabled = false;
            txb_telefono.IsEnabled = false;
            txb_telefonoOpcional.IsEnabled = false;
            txb_email.IsEnabled = false;
            txb_emailOpcional.IsEnabled = false;
            txb_descripcion.IsEnabled = false;
            btn_agregar.Visibility = Visibility.Collapsed;
            btn_modificar.Visibility = Visibility.Collapsed;
        }

        public void HabilitarComponentes()
        {
            txb_cedJur.IsEnabled = true;
            txb_nombre.IsEnabled = true;
            txb_telefono.IsEnabled = true;
            txb_telefonoOpcional.IsEnabled = true;
            txb_email.IsEnabled = true;
            txb_emailOpcional.IsEnabled = true;
            txb_descripcion.IsEnabled = true;
        }

        private void HabilitarBtnModificar()
        {
            if (v_Actividad_btnModificar == true)
            {
                btn_modificar.Visibility = Visibility.Visible;
            }
        }

        private void HabilitarBtnAgregar()
        {
            if (v_Actividad_btnAgregar == true)
            {
                if (txb_cedJur.Text != "" && txb_email.Text != "" && txb_nombre.Text != "" && txb_telefono.Text != "" && txb_descripcion.Text != "")
                {
                    btn_agregar.Visibility = Visibility.Visible;
                }
                else
                {
                    btn_agregar.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void txb_busqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarErroresTxb(txb_busqueda, lbl_errorBusqueda, "");
            if (lbl_errorBusqueda.Visibility == Visibility.Collapsed)
            {
                HabilitarComponentes();
            }      
        }

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

        private void txb_nombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_nombre, lbl_errorNombre, "nombre");
        }

        private void txb_telefono_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_telefono, lbl_errorTelefono, "numeros");     
        }

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

        private void txb_email_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_email, lbl_errorEmail, "");
        }

        private void txb_emailOpcional_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_emailOpcional, lbl_errorEmailOpcional, "");
            if (txb_emailOpcional.Text == "")
            {
                lbl_errorEmailOpcional.Visibility = Visibility.Collapsed;
            }
        }

        private void txb_descripcion_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitarBtnModificar();
            HabilitarBtnAgregar();
            ValidarErroresTxb(txb_descripcion,lbl_errorDesc,"descripcion"); 
        }

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

        private Boolean EmailCorrecto(String v_Email)
        {
            String v_Expresion;
            v_Expresion = @"^([a-zA-Z0-9_\-\.ñÑ]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,3}|[0-9]{1,3})(\]?)$";
            if (Regex.IsMatch(v_Email, v_Expresion))
            {
                if (Regex.Replace(v_Email, v_Expresion, String.Empty).Length == 0)
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

        private void ValidarEmail(object sender, EventArgs e)
        {
            Console.WriteLine("Correo: " + txb_email.Text);
            Console.WriteLine(EmailCorrecto(txb_email.Text));
            if (EmailCorrecto(txb_email.Text) == false)
            {
                if (txb_email.Text == "")
                {
                    lbl_errorEmail.Content = "Espacio vacío";
                    lbl_errorEmail.Visibility = Visibility.Visible;
                }
                else
                {
                    lbl_errorEmail.Content = "Formato: usuario@dominio.extension(Máx 3 caracteres)";
                    lbl_errorEmail.Visibility = Visibility.Visible;
                }
            }
        }

        private void ValidarEmailOpcional(object sender, EventArgs e)
        {
            Console.WriteLine("Correo: " + txb_emailOpcional.Text);
            Console.WriteLine(EmailCorrecto(txb_emailOpcional.Text));
            if (txb_emailOpcional.Text == "N/A")
            {
                lbl_errorEmailOpcional.Visibility = Visibility.Collapsed;
            }
            else if (EmailCorrecto(txb_emailOpcional.Text) == false)
            {
                if (txb_emailOpcional.Text == "")
                {
                    lbl_errorEmailOpcional.Visibility = Visibility.Collapsed;
                }
                else
                {
                    lbl_errorEmailOpcional.Content = "Formato: usuario@dominio.extension(Máx 3 caracteres)";
                    lbl_errorEmailOpcional.Visibility = Visibility.Visible;
                }
            }
        }
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

    }//fin de la clase
}//fin proyecto
