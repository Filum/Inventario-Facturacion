using System;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Clientes : Window
    {
        //Declaramos dos objetos, uno de la entidad de clientes y otro del model.
        EntidadClientes clt = new EntidadClientes();
        EntidadBitacora bitacora = new EntidadBitacora();
        Model model = new Model();
        public string nombreUsuario;
        public Clientes()
        {
            InitializeComponent();
            //Formato para la hora, se ejecuta por medio de un hilo.
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(Hora_Fecha);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            date_historial_clientes_inicio.SelectedDate = DateTime.Now.Date;
            date_historial_clientes_final.SelectedDate = DateTime.Now.Date;
        }
        //Se toma el txt_fecha para establecer la hra del sistema.
        private void Hora_Fecha(object sender, EventArgs e)
        {
            txt_fecha.Content = DateTime.Now.ToString();

        }

        //funcion para mostar mensajes de ayuda para el usuario.
        private void btn_Ayuda_Click(object sender, RoutedEventArgs e)
        {
            Vista.Ayuda ventana = new Vista.Ayuda();
            ventana.nombreUsuario = nombreUsuario;
            ventana.Show();
            ventana.Pantalla = "Clientes";
        }

        //Función para limpiar campos en el modulo de clientes. 
        private void Limpiar_Actualizar_Cliente()
        {
            txb_correo.Text = "";
            txb_correo_o.Text = "";
            txb_nombre.Text = "";
            txb_observaciones.Text = "";
            txb_TelMov.Text = "";
            txb_TelOf.Text = "";
            txb_buscar_cliente.Text = "";
            txb_cedula.Text = "";
            txb_representante.Text = "";
            txb_direccion.Text = "";
            txb_cedula.Text = "";
            txb_representante.Text = "";
            txb_direccion.Text = "";
            rb_activo.IsChecked = false;
            rb_inactivo.IsChecked = false;
            txt_error_telM.Visibility = Visibility.Hidden;
            txt_error_TelO.Visibility = Visibility.Hidden;
            txt_error_correo.Visibility = Visibility.Hidden;
            txt_error_correo_o.Visibility = Visibility.Hidden;
            btn_agregar_Cliente.Visibility = Visibility.Visible;
            btn_guardar_cliente_actualizado.Visibility = Visibility.Hidden;
            txt_actividad.Visibility = Visibility.Hidden;
            dtg_clientes.ItemsSource = null;
            txb_buscar_cliente.IsEnabled = true;
        }

        //Botón para regresar al menú principal. 
        private void btn_Regresar_Click_1(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.Show();//funcion para mostrar otra ventana.
            this.Close();//cierra la ventana en la que se esta en ese momento.
        }

        //Botón para salir del sistema. 
        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.cargarMenu(nombreUsuario);
            ventana.nombreUser = nombreUsuario;
            this.Close();
        }

        //Botón para cerrar sesión. 
        private void btn_usuario_clientes_Click(object sender, RoutedEventArgs e)
        {
            Login ventana = new Login();
            ventana.Show();
            this.Close();
        }
        public void SoloNumeros(TextBox txb, Label lbl, TextCompositionEventArgs e)
        {
            //se convierte a Ascci del la tecla presionada 
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            //verificamos que se encuentre en ese rango que son entre el 0 y el 9 
            if (ascci >= 48 && ascci <= 57)
            {
                e.Handled = false;
                lbl.Visibility = Visibility.Collapsed;
                if (ValidarCaracteresEspeciales(txb.Text) == true)
                {
                    lbl.Content = "No se permiten caracteres especiales";
                    lbl.Visibility = Visibility.Visible;
                }
            }
            else
            {
                e.Handled = true;
                lbl.Content = "No se permite ingresar letras";
                lbl.Visibility = Visibility.Visible;
            }
        }
        private void btn_editar_cliente_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("No se ha seleccionado ningun cliente.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        //Botón que guarda los cambios que se le realizan a los clientes.
        private void btn_guardar_cliente_actualizado_Click(object sender, RoutedEventArgs e)
        {
            try//Comprobamos que se rellenen los espacios obligatorios en la pantlla de actualizar clientes.
            {
                if (txb_correo.Text == "" || txb_nombre.Text == "" || txb_TelOf.Text == "" || txb_TelMov.Text == "" || txb_cedula.Text == "" || txb_representante.Text == "" || txb_direccion.Text == "" || txb_TelOf.Text.Length < 8 || txb_TelMov.Text.Length < 8 )
                {
                    MessageBox.Show("No se puede modificar\nHacen falta campos por rellenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (rb_activo.IsChecked == true)
                    {
                        clt.v_Inactividad = "Activo";
                    }
                    else if (rb_inactivo.IsChecked == true)
                    {
                        clt.v_Inactividad = "Inactivo";
                    }

                    //Extraemos los datos de la pantlla de actualizar clientes y los ingresamos al objeto de clientes.
                    clt.v_NombreCompleto = txb_nombre.Text;
                    clt.v_Teleoficina = Convert.ToInt32(txb_TelOf.Text);
                    clt.v_Telemovil = Convert.ToInt32(txb_TelMov.Text);
                    clt.v_Correo = txb_correo.Text;
                    clt.v_Cedula = txb_cedula.Text;
                    clt.v_Representante = txb_representante.Text;
                    clt.v_Direccion = txb_direccion.Text;
                    //A los espacios vacios se rellenan con un "N/A"
                    if (txb_correo_o.Text == "")
                        clt.v_CorreoOpc = "N/A";
                    else
                        clt.v_CorreoOpc = txb_correo_o.Text;


                    if (txb_correo_o.Text == "")
                        clt.v_Observaciones = "N/A";
                    else
                        clt.v_Observaciones = txb_observaciones.Text;

                    //Obetenemos el resultado de modificar los datos del cliente
                    int v_Resultado = model.ModificarClientes(clt);
                    if (v_Resultado == -1)//Si no surge ningun error ,se modifica correctamente.
                    {
                        MessageBox.Show("Datos modificados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        bitacora.accion = "Modificar";
                        bitacora.usuario_Responsable = Usuario_clientes.Text;
                        bitacora.ventana_Mantenimiento = "Mantenimiento Clientes";
                        bitacora.descripcion = "Modificó el cliente con la cédula: " + txb_cedula.Text+", con el nombre: "+txb_nombre.Text;
                        model.AgregarBitacora(bitacora);
                        Limpiar_Actualizar_Cliente();
                        grd_formularioCliente.Visibility = Visibility.Hidden;
                        grd_ClientesExistentes.Visibility = Visibility.Visible;
                        txb_buscar_cliente.IsEnabled = true;
                    }
                }
            }
            catch (Exception m)//En caso de que surgan error, se muestra un mensaje de error.
            {
                Console.WriteLine(m.ToString());
                MessageBox.Show("Error al modificar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Botón para limpiar el modulo de clientes.
        private void btn_limpiar_actualizar_cliente_Click(object sender, RoutedEventArgs e)
        {
            Limpiar_Actualizar_Cliente();
        }


        //Botón para salir del modulo de clientes.
        private void btn_salir_clientes_Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.Show();
            this.Close();
        }

        //Botón para minimizar la pantalla.
        private void btn_minimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        //Botón para maximizar la pantalla.
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

        //Botón cerrar el programa.
        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Botón para listar los clientes existentes en el sistema.
        private void btn_Listar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (date_historial_clientes_inicio.SelectedDate != null && date_historial_clientes_final.SelectedDate != null)
                {
                    DateTime v_Date1 = DateTime.Parse(date_historial_clientes_inicio.SelectedDate.Value.Date.ToShortDateString());
                    DateTime v_Date2 = DateTime.Parse(date_historial_clientes_final.SelectedDate.Value.Date.ToShortDateString());
                    String v_Fecha1;
                    v_Fecha1 = date_historial_clientes_inicio.SelectedDate.Value.Date.ToShortDateString();
                    String v_Fecha2;
                    v_Fecha2 = date_historial_clientes_final.SelectedDate.Value.Date.ToShortDateString();
                    if (v_Date1 > v_Date2)
                    {
                        MessageBox.Show("El rango de fechas es incorrecto\nLa fecha inicial no puede ser mayor a la final", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        if (model.MostrarListaClientes(v_Fecha1, v_Fecha2).Rows.Count == 0)
                        {
                            MessageBox.Show("No hay datos registrados en el rango de fechas seleccionado", "Búsqueda", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            dtg_listar_clientes.ItemsSource = model.MostrarListaClientes(v_Fecha1, v_Fecha2).DefaultView;
                            txb_buscar_cliente_Lista.Text = "";
                            cmb_estado_Cliente.SelectedItem = null;
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Seleccione un rango de fechas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }catch(Exception m)
            {
                Console.Write(m);
                MessageBox.Show("Error al listar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        //Métodos para controlar los errores que se pueden cometer a la hora de rellenar el formulario.
        private void Txb_TelOf_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(txb_TelOf, txt_error_TelO, e);
        }

        private void Txb_TelMov_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(txb_TelMov, txt_error_telM, e);
        }

        private void Txb_cedula_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(txb_cedula, txt_error_cedula, e);
        }

        private Boolean email_correcto(String email)
        {
            String v_Expresion;
            v_Expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, v_Expresion))
            {
                if (Regex.Replace(email, v_Expresion, String.Empty).Length == 0)
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


        private void validar_Actualizar_Correo(object sender, EventArgs e)
        {
            if (email_correcto(txb_correo.Text) == false)
            {
                txt_error_correo.Content = "Error de formato(usuario@dominio.extensión)";
                txt_error_correo.Visibility = Visibility.Visible;
            }
            else
            {
                txt_error_correo.Visibility = Visibility.Hidden;
            }



        }


        private void txb_cliente_modificar_KeyUp(object sender, KeyEventArgs e)
        {
            string v_Texto;
            dtg_clientes.ItemsSource = model.BuscarClientes(txb_buscar_cliente.Text);
            if (txb_buscar_cliente.Text == "")
            {
                txb_nombre.Text = "";
                Inabilitar_Campos();
                Limpiar_Actualizar_Cliente();
            }
            else
            {
                if (dtg_clientes.Items.Count == 0)
                {
                    txt_actividad.Content = "Agregar Clientes";
                    rb_inactivo.Visibility = Visibility.Hidden;
                    rb_activo.IsChecked = true;
                    rb_activo.IsEnabled = false;
                    txt_actividad.Visibility = Visibility.Visible;
                    v_Texto = txb_buscar_cliente.Text;
                    Agregar(v_Texto);
                    btn_agregar_Cliente.Visibility = Visibility.Visible;
                    btn_guardar_cliente_actualizado.Visibility = Visibility.Hidden;
                }
                else
                {
                    txt_actividad.Content = "Clientes Existentes";
                    txt_actividad.Visibility = Visibility.Visible;
                    btn_agregar_Cliente.Visibility = Visibility.Hidden;
                    btn_guardar_cliente_actualizado.Visibility = Visibility.Visible;
                    Inabilitar_Campos();
                }
            }

        }

        private void Agregar(string v_Texto)
        {
            Habilitar_Campos();
            txb_nombre.Text = v_Texto;
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            txt_actividad.Content = "Modificar Clientes";
            txt_actividad.Visibility = Visibility.Visible;
            rb_inactivo.Visibility = Visibility.Visible;
            txb_buscar_cliente.IsEnabled = true;


            clt.v_Codigo = Convert.ToInt32((dtg_clientes.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text);

            txb_nombre.Text = (dtg_clientes.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text;
            txb_correo.Text = (dtg_clientes.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;
            txb_correo_o.Text = (dtg_clientes.SelectedCells[3].Column.GetCellContent(row) as TextBlock).Text;
            txb_TelOf.Text = (dtg_clientes.SelectedCells[4].Column.GetCellContent(row) as TextBlock).Text;
            txb_TelMov.Text = (dtg_clientes.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text;
            String v_Inacto = (dtg_clientes.SelectedCells[6].Column.GetCellContent(row) as TextBlock).Text; ;
            txb_observaciones.Text = (dtg_clientes.SelectedCells[7].Column.GetCellContent(row) as TextBlock).Text;
            txb_cedula.Text = (dtg_clientes.SelectedCells[8].Column.GetCellContent(row) as TextBlock).Text;
            txb_representante.Text = (dtg_clientes.SelectedCells[9].Column.GetCellContent(row) as TextBlock).Text;
            txb_direccion.Text = (dtg_clientes.SelectedCells[10].Column.GetCellContent(row) as TextBlock).Text;

            if (v_Inacto == "Inactivo")
            {
                rb_inactivo.IsChecked = true;
            }
            else if (v_Inacto == "Activo")
            {
                rb_activo.IsChecked = true;
            }

            Habilitar_Campos();
            
            btn_guardar_cliente_actualizado.Visibility = Visibility.Visible;
            btn_agregar_Cliente.Visibility = Visibility.Collapsed;
            MostrarFormulario();

        }


        private void validar_Correo_Opc_Act(object sender, RoutedEventArgs e)
        {
            if (email_correcto(txb_correo_o.Text) == false)
            {
                txt_error_correo_o.Content = "Error de formato(usuario@dominio.extensión)";
                txt_error_correo_o.Visibility = Visibility.Visible;
            }
            else
            {
                txt_error_correo_o.Visibility = Visibility.Hidden;
            }
        }




        private void validar_actualizar_oficina(object sender, KeyboardFocusChangedEventArgs e)
        {
            string tele1 = txb_TelOf.Text;
            if (tele1.Length < 8)
            {
                txt_error_TelO.Content = "Los números telefónicos deben de tener "+ String.Format(Environment.NewLine)+" un formato valido de 8 dígitos.";
                txt_error_TelO.Visibility = Visibility.Visible;
            }
            else
            {
                txt_error_TelO.Visibility = Visibility.Hidden;
            }
        }

        private void validar_actualizar_movil(object sender, KeyboardFocusChangedEventArgs e)
        {
            string tele = txb_TelMov.Text;
            if (tele.Length < 8)
            {
                txt_error_telM.Content = "Los números telefónicos deben de " + String.Format(Environment.NewLine) + "tener un formato " + String.Format(Environment.NewLine) + "valido de 8 dígitos.";
                txt_error_telM.Visibility = Visibility.Visible;
            }
            else
            {
                txt_error_telM.Visibility = Visibility.Hidden;
            }
        }
        //Funciones para habilitar o inhabilitar campos en el modulo de clientes .
        public void Habilitar_Campos()
        {
            txb_nombre.IsEnabled = true;
            txb_correo.IsEnabled = true;
            txb_correo_o.IsEnabled = true;
            txb_TelOf.IsEnabled = true;
            txb_TelMov.IsEnabled = true;
            txb_observaciones.IsEnabled = true;
            rb_inactivo.IsEnabled = true;
            rb_activo.IsEnabled = true;
        }
        public void Inabilitar_Campos()
        {
            txb_nombre.IsEnabled = false;
            txb_correo.IsEnabled = false;
            txb_correo_o.IsEnabled = false;
            txb_TelOf.IsEnabled = false;
            txb_TelMov.IsEnabled = false;
            txb_observaciones.IsEnabled = false;
            rb_inactivo.IsEnabled = false;
            rb_activo.IsEnabled = false;
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            txt_actividad.Visibility = Visibility.Visible;
            txt_actividad.Content = "Buscar Clientes";
            btn_agregar_Cliente.Visibility = Visibility.Visible;
        }

        //Método el cual valida si en las cajas de texto recibidos contiene caracteres especiales
        private Boolean ValidarCaracteresEspeciales(String v_Txb)
        {
            //caracteres que permite si la cadena es de int
            String v_Caracteres = "[a-zA-Z !@#$%^&*())+=.,<>{}¬º´/\"':;|ñÑ~¡?`¿-]";
            if (Regex.IsMatch(v_Txb, v_Caracteres))
            {
                return true;
            }
            
            return false;
        }

        //Botón que se utiliza para agregar clientes inexistentes al sistema 
        private void btn_agregar_Cliente_Click(object sender, RoutedEventArgs e)
        {
            string v_Inactivo;
            try
            {
                if (txb_correo.Text == "" || txb_nombre.Text == "" || txb_TelOf.Text == "" || txb_TelMov.Text == "" || txb_cedula.Text == "" || txb_representante.Text == "" || txb_direccion.Text == "" || txt_error_TelO.Visibility == Visibility.Visible || txt_error_telM.Visibility == Visibility.Visible || txt_error_correo.Visibility == Visibility.Visible)
                {
                    MessageBox.Show("No se puede agregar\nHacen falta campos por rellenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (rb_activo.IsChecked == true)
                    {
                        clt.v_Inactividad = "Activo";
                    }
                    else if (rb_inactivo.IsChecked == true)
                    {
                        clt.v_Inactividad = "Inactivo";
                    }
                    clt.v_NombreCompleto = txb_nombre.Text;
                    clt.v_Teleoficina = Convert.ToInt32(txb_TelOf.Text);
                    clt.v_Telemovil = Convert.ToInt32(txb_TelMov.Text);

                    clt.v_Correo = txb_correo.Text;
                    clt.v_Cedula = txb_cedula.Text;
                    clt.v_Representante = txb_representante.Text;
                    clt.v_Direccion = txb_direccion.Text;


                    if (txb_correo_o.Text == "")
                        clt.v_CorreoOpc = "N/A";
                    else
                        clt.v_CorreoOpc = txb_correo_o.Text;

                  //  clt.v_Inactividad = v_Inactivo;

                    if (txb_observaciones.Text == "")
                        clt.v_Observaciones = "N/A";
                    else
                        clt.v_Observaciones = txb_observaciones.Text;

                    int v_Resultado = model.AgregarClientes(clt);
                    if (v_Resultado == -1)
                    {
                        MessageBox.Show("Datos ingresados correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        bitacora.accion = "Agregar";
                        bitacora.usuario_Responsable = Usuario_clientes.Text;
                        bitacora.ventana_Mantenimiento = "Mantenimiento Clientes";
                        bitacora.descripcion = "Agregó el cliente con la cédula: " + txb_cedula.Text + ", con el nombre: " + txb_nombre.Text;
                        model.AgregarBitacora(bitacora);
                        Limpiar_Actualizar_Cliente();
                    }

                }
            }
            catch (Exception m)
            {
                Console.WriteLine(m.ToString());
                MessageBox.Show("Error al agregar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_usuario_roles_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Desea cerrar sesión?",
                                          "Cerrar Sesión",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Login ventana = new Login();
                this.Close();
                ventana.Show();
            }
        }



        private void txb_buscar_cliente_TextChanged(object sender, TextChangedEventArgs e)
        {
            dtg_clientes.ItemsSource = model.BuscarClientes(txb_buscar_cliente.Text);
            txt_actividad.Content = "Clientes existentes";
            txt_actividad.Visibility = Visibility.Visible;
        }

        //Método el cual recibe parametros necesarios para la validacion y la muestra de mensajes de erroes en las cajas de texto
        private void ValidarErroresTxb(TextBox txb_clientes, Label lbl_error)
        {
            string v_TamanoTxb = txb_clientes.Text;
            if (txb_clientes.Text == "")
            {
                lbl_error.Content = "Espacio vacío";
                lbl_error.Visibility = Visibility.Visible;
            }
            else if (txb_clientes.Text == " ")
            {
                txb_clientes.Text = "";
            }
            else if (txb_clientes.Text.Contains("  "))
            {
                lbl_error.Content = "Parámetros incorrectos (espacios seguidos)";
                lbl_error.Visibility = Visibility.Visible;
            }
            else if (ValidarCaracteresEspeciales(txb_clientes.Text) == true)
            {
                lbl_error.Content = "No se permiten caracteres especiales";
                lbl_error.Visibility = Visibility.Visible;
            }
            else if (txb_clientes == txb_TelOf || txb_clientes == txb_TelMov)
            {
                if (v_TamanoTxb.Length < 8)
                {
                    lbl_error.Content = "Deben tener al menos 8 dígitos";
                    lbl_error.Visibility = Visibility.Visible;
                }
            }
            else
            {
                lbl_error.Visibility = Visibility.Collapsed;
            }
        }

        private void txb_TelOf_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarErroresTxb(txb_TelOf, txt_error_TelO);
            if (txb_TelOf.Text == "")
            {
                txt_error_TelO.Visibility = Visibility.Collapsed;
            }
        }

        private void txb_TelMov_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarErroresTxb(txb_TelMov, txt_error_telM);
            if (txb_TelMov.Text == "")
            {
                txt_error_telM.Visibility = Visibility.Collapsed;
            }
        }

        private void btn_imprimirClientes_Click(object sender, RoutedEventArgs e)
        {
            Imprimir print = new Imprimir();
            print.imprimir(dtg_listar_clientes, "REPORTE CLIENTES");
        }

        private void btn_volver_Click(object sender, RoutedEventArgs e)
        {
            OcultarFormulario();
            txt_actividad.Content = "Buscar Clientes";
        }

        private void OcultarFormulario()
        {
            grd_formularioCliente.Visibility = Visibility.Collapsed;
            MostrarClientesExistentes();
        }

        //Muestra el panel de búsqueda en el tab de configuración de proveedores
        private void MostrarClientesExistentes()
        {
            txt_actividad.Content = "Clientes existentes";
            grd_ClientesExistentes.Visibility = Visibility.Visible;
        }

        private void OcultarClientesExistentes()
        {
            grd_ClientesExistentes.Visibility = Visibility.Collapsed;
        }

        private void btn_agregarCliente_Click(object sender, RoutedEventArgs e)
        {
            Habilitar_Campos();
            btn_limpiar_actualizar_cliente_Click(sender, e);
            txt_actividad.Visibility = Visibility.Visible;
            txt_actividad.Content = "Agregar Nuevo Cliente";
            btn_limpiar_actualizar_cliente.Visibility = Visibility.Visible;
            btn_guardar_cliente_actualizado.Visibility = Visibility.Collapsed;
            btn_agregar_Cliente.Visibility = Visibility.Visible;
            MostrarFormulario();
        }

        //Muestra el panel del formulario en el tab de configuración de proveedores
        private void MostrarFormulario()
        {
            grd_formularioCliente.Visibility = Visibility.Visible;
            OcultarClientesExistentes();
        }
       

        private void txb_cedula_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarErroresTxb(txb_cedula, txt_error_cedula);
            if (txb_cedula.Text == "")
            {
                txt_error_cedula.Visibility = Visibility.Collapsed;
            }
        }

        private void cmb_estado_Cliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (txb_buscar_cliente_Lista.Text == "")
                {
                    if (cmb_estado_Cliente.SelectedItem.ToString() == "Todos")
                        dtg_listar_clientes.ItemsSource = model.BuscarTodoslosClientes().DefaultView;
                    else
                        dtg_listar_clientes.ItemsSource = model.BuscarClienteEstado(cmb_estado_Cliente.SelectedItem.ToString()).DefaultView;
                }
                else
                {
                    if (cmb_estado_Cliente.SelectedItem.ToString() == "Todos")
                        dtg_listar_clientes.ItemsSource = model.BuscarClienteNombre(txb_buscar_cliente_Lista.Text).DefaultView;
                    else
                        dtg_listar_clientes.ItemsSource = model.BuscarClienteEstadoyNombre(txb_buscar_cliente_Lista.Text, cmb_estado_Cliente.SelectedItem.ToString()).DefaultView;
                }
            }
            catch (Exception m)
            {
                Console.WriteLine(m);
            }
        }

        private void cmb_estado_Cliente_Initialized(object sender, EventArgs e)
        {
            cmb_estado_Cliente.Items.Add("Todos");
            cmb_estado_Cliente.Items.Add("Activo");
            cmb_estado_Cliente.Items.Add("Inactivo");
        }

        private void txb_buscar_cliente_Lista_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txb_buscar_cliente_Lista.Text == "")
                {
                    dtg_listar_clientes.ItemsSource = null;
                    dtg_listar_clientes.ItemsSource = model.BuscarClienteEstado(cmb_estado_Cliente.SelectedItem.ToString()).DefaultView;
                }
                else
                {
                    if (cmb_estado_Cliente.SelectedItem == null)
                    {
                        dtg_listar_clientes.ItemsSource = model.BuscarClienteNombre(txb_buscar_cliente_Lista.Text).DefaultView;
                    }
                    else
                    {
                        if(cmb_estado_Cliente.SelectedItem.ToString() == "Todos")
                            dtg_listar_clientes.ItemsSource = model.BuscarClienteNombre(txb_buscar_cliente_Lista.Text).DefaultView;
                        else
                            dtg_listar_clientes.ItemsSource = model.BuscarClienteEstadoyNombre(txb_buscar_cliente_Lista.Text, cmb_estado_Cliente.SelectedItem.ToString()).DefaultView;
                    }
                }
            }catch (Exception m)
            {
                Console.Write(m);
            }
        }

        private void validar_Cedula(object sender, KeyboardFocusChangedEventArgs e)
        {
            string cedula = txb_cedula.Text;
            if (cedula.Length < 9)
            {
                txt_error_cedula.Content = "La cédula debe tener un formato valido valido de 9 dígitos.";
                txt_error_cedula.Visibility = Visibility.Visible;
            }
            else
            {
                txt_error_cedula.Visibility = Visibility.Hidden;
            }
        }

        private void Txb_nombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(model.VerificarNombre(txb_nombre.Text)==1)
            {
                txt_error_nombre.Content = "Hay similitudes con cliente existe.";
                txt_error_nombre.Visibility = Visibility.Visible;
            }
            else
            {
                txt_error_nombre.Visibility = Visibility.Hidden;
            }
        }
    }
}