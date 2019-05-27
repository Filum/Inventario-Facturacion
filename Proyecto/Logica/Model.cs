using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
using System.Data;
using System.Collections.ObjectModel;

namespace Logica
{
    public class Model
    {
        //////////          ATRIBUTOS           //////////
        Data v_Data = new Data();

        //////////          AGREGAR         //////////

        public int AgregarClientes(EntidadClientes clt)
        {
            return v_Data.AgregarClientes(clt);
        }

        //Recibe como referencia una "EntidadProveedores" la cual va a ser enviada a la clase Data para proceder con la agregación del proveedor.
        public int AgregarProveedores(EntidadProveedores clt)
        {
            return v_Data.AgregarProveedores(clt);
        }

        //Recibe como referencia una "EntidadUsuarios" la cual va a ser enviada a la clase Data para proceder con la agregación del usuario.
        public int AgregarUsuarios(EntidadUsuarios clt)
        {
            return v_Data.AgregarUsuarios(clt);
        }

        //Recibe como referencia una "EntidadRoles" la cual va a ser enviada a la clase Data para proceder con la agregación del rol.
        public int AgregarRoles(EntidadRoles clt)
        {
            return v_Data.AgregarRoles(clt);
        }

        //Recibe como referencia una "EntidadRoles" la cual va a ser enviada a la clase Data para proceder con la agregación del producto.
        public int AgregarProductos(EntidadProductos clt)
        {
            return v_Data.AgregarProductos(clt);
        }

        public int AgregarFacturas(EntidadFacturas fact)
        {
            return v_Data.AgregarFacturas(fact);
        }
        public int AgregarDetalle(EntidadDetalleFactura fact)
        {
            return v_Data.AgregarDetalle(fact);
        }


        //////////          MODIFICAR           //////////

        public int ModificarClientes(EntidadClientes clt)
        {
            return v_Data.ModificarClientes(clt);
        }

        //Recibe como referencia una "EntidadProveedores" la cual va a ser enviada a la clase Data para proceder con la modificación del proveedor.
        public int ModificarProveedores(EntidadProveedores clt)
        {
            return v_Data.ModificarProveedores(clt);
        }

        //Recibe como referencia una "EntidadUsuarios" la cual va a ser enviada a la clase Data para proceder con la modificación del usuario.
        public int ModificarUsuarios(EntidadUsuarios clt)
        {
            return v_Data.ModificarUsuarios(clt);
        }

        //Recibe como referencia una "EntidadRoles" la cual va a ser enviada a la clase Data para proceder con la modificación del rol.
        public int ModificarRoles(EntidadRoles clt)
        {
            return v_Data.ModificarRoles(clt);
        }

        //Recibe como referencia una "EntidadProductos" la cual va a ser enviada a la clase Data para proceder con la modificación del producto.
        public int ModificarProductos(EntidadProductos clt)
        {
            return v_Data.ModificarProductos(clt);
        }

        //Recibe como referencia una "EntidadProveedor" que contiene el id y la cédula jurídica del proveedor con el fin de validar si la cédula jurídica está asociada a dicho id.
        public bool ValidarModificacionProveedores(EntidadProveedores clt)
        {
            return v_Data.ValidarModificacionProveedores(clt);
        }

        //Recibe como referencia una "EntidadUsuario" que contiene el id y el número de cédula del usuario con el fin de validar si el número de cédula está asociada a dicho id.
        public bool ValidarModificacionUsuario(EntidadUsuarios clt)
        {
            return v_Data.ValidarModificacionUsuario(clt);
        }

        //////////          MOSTRAR         //////////

        //Recibe como referencia un estado en el sistema para realizar la consulta: ACTIVO, INACTIVO o LISTAPROVEEDORES.
        public DataTable MostrarListaProveedores(String v_EstadoSistema)
        {
            return v_Data.MostarListaProveedores(v_EstadoSistema);
        }

        //Recibe como referencia un estado en el sistema para realizar la consulta: ACTIVO, INACTIVO o LISTAUSUARIOS.
        public DataTable MostrarListaUsuarios(String v_EstadoSistema)
        {
            return v_Data.MostarListaUsuarios(v_EstadoSistema);
        }

        //Recibe como referencia un estado en el sistema para realizar la consulta: ACTIVO, INACTIVO o LISTAPRODUCTOS.
        public DataTable MostrarListaProductos(String v_EstadoSistema)
        {
            return v_Data.MostarListaProductos(v_EstadoSistema);
        }

        //Recibe como referencia un estado en el sistema para realizar la consulta: ACTIVO, INACTIVO o LISTAROLES.
        public DataTable MostrarListaRoles(String v_EstadoSistema)
        {
            return v_Data.MostarListaRoles(v_EstadoSistema);
        }

        public DataTable MostrarListaClientes(String fecha1, String fecha2)
        {
            return v_Data.MostrarListaClientes(fecha1, fecha2);
        }

        public DataTable MostrarListaFacturas(String fecha1, String fecha2)
        {
            return v_Data.MostrarListaFacturas(fecha1, fecha2);
        }

        public DataTable MostrarDetalleFactura(int codigoFactura)
        {
            return v_Data.MostrarDetalleFactura(codigoFactura);
        }

        //////////          CARGAR DATOS         //////////
        /*Invoca al método que se utiliza para llenar el combobox de proveedores en el mantenimiento de productos. 
        Se consulta al Data los proveedores existentes obtiendo solo su id y nombre para ser retornados a la vista y así llenar el combobox. */
        public List<EntidadProveedores> ProveedoresExistentes()
        {
            return v_Data.ProveedoresExistentes();
        }

        /*Invoca al método que se utiliza para llenar el combobox de roles en el mantenimiento de usuarios. 
        Se consulta al Data los roles existentes obtiendo solo su id y nombre para ser retornados a la vista y así llenar el combobox. */
        public List<EntidadRoles> RolesExistentes()
        {
            return v_Data.RolesExistentes();
        }

        //////////          BUSCAR         //////////

        //Recibe como referencia un string necesario para proceder con la búsqueda de la existencia de proveedores en el tab de "Configuración de Proveedores".
        public List<EntidadProveedores> ValidarBusquedaProveedores(String v_Busqueda)
        {
            return v_Data.ValidarBusquedaProveedores(v_Busqueda);
        }

        //Recibe como referencia un string necesario para proceder con la búsqueda de la existencia de usuarios en el tab de "Configuración de Usuarios".
        public List<EntidadUsuarios> ValidarBusquedaUsuarios(String v_Busqueda)
        {
            return v_Data.ValidarBusquedaUsuarios(v_Busqueda);
        }

        //Recibe como referencia un string necesario para proceder con la búsqueda de la existencia de productos en el tab de "Configuración de Productos".
        public List<EntidadProductos> ValidarBusquedaProductos(String v_Busqueda)
        {
            return v_Data.ValidarBusquedaProductos(v_Busqueda);
        }

        //Recibe como referencia un string necesario para proceder con la búsqueda de la existencia de roles en el tab de "Configuración de Roles".
        public List<EntidadRoles> ValidarBusquedaRoles(String v_Busqueda)
        {
            return v_Data.ValidarBusquedaRoles(v_Busqueda);
        }

        public DataTable BuscarFacturaEstadoClienteFechas(string v_Nombre, string estado, string fecha1, string fecha2)
        {
            return v_Data.BuscarFacturaEstadoClienteFechas(v_Nombre, estado, fecha1, fecha2);
        }

        public DataTable BuscarFacturas()
        {
            return v_Data.BuscarFacturas();
        }

        public DataTable BuscarTodoslosClientes()
        {
            return v_Data.BuscarTodoslosClientes();
        }

        public List<EntidadClientes> BuscarClientes(String nombre)
        {
            return v_Data.BuscarClientes(nombre);
        }

        public DataTable BuscarFactura(String v_Nombre)
        {
            return v_Data.BuscarFactura(v_Nombre);
        }

        public DataTable BuscarFacturaEstado(String estado)
        {
            return v_Data.BuscarFacturaEstado(estado);
        }

        public DataTable BuscarFacturaEstadoyCliente(string v_Nombre, string estado)
        {
            return v_Data.BuscarFacturaEstadoyCliente(v_Nombre, estado);
        }

        public DataTable BuscarClienteNombre(string nombre)
        {
            return v_Data.BuscarClienteNombre(nombre);
        }

        public DataTable BuscarClienteEstado(string nombre)
        {
            return v_Data.BuscarClienteEstado(nombre);
        }

        public DataTable BuscarClienteEstadoyNombre(string nombre, string estado)
        {
            return v_Data.BuscarClienteEstadoyNombre(nombre, estado);
        }

        //////////          VALIDACIONES            //////////

        //Recibe como referencia un string necesario para proceder con la verificación de existencia de la cédula jurídica.
        public bool ValidarCedJurProveedores(String v_CedJur)
        {
            return v_Data.ValidarCedJurProveedores(v_CedJur);
        }

        //Recibe como referencia un string necesario para proceder con la verificación de existencia de la cédula de identificacion del usuario.
        public bool ValidarNumCedUsuarios(String v_NumCed)
        {
            return v_Data.ValidarNumCedUsuarios(v_NumCed);
        }

        public DataTable Clientes()
        {
            return v_Data.Clientes();
        }

        public List<string> DetalleFactura(int codigoFactura)
        {
            return v_Data.DetalleFactura(codigoFactura);
        }

        public float ObtenerValorDolar()
        {
            return v_Data.ObtenerValorDolar();
        }

        public ObservableCollection<string> ListaProductos()
        {
            return v_Data.ListaProductos();
        }

        public List<string> DetalleProducto(string descripcion)
        {
            return v_Data.DetalleProducto(descripcion);
        }

        public List<string> consultarUsuario(string nombreUsuario)
        {
            return v_Data.consultarUsuario(nombreUsuario);
        }

        public DataTable Productos()
        {
            return v_Data.Productos();
        }

        public Int64 MaximaFactura()
        {
            return v_Data.MaximaFactura();
        }

        public Int64 MaximoDetalle()
        {
            return v_Data.MaximoDetalle();
        }

        public Int64 id_Usuario(string nombre)
        {
            return v_Data.id_Usuario(nombre);
        }

        public EntidadClientes id_Cliente(string nombre)
        {
            return v_Data.IdCliente(nombre);
        }

        public Int64 id_Producto(string nombre)
        {
            return v_Data.IdProducto(nombre);
        }

        public void DescuentoInventario(string cantidad, string nombre)
        {
            v_Data.DescuentoInventario(cantidad, nombre);
        }

        public void CambiarestadoFactura(string estado, string codigo)
        {
            v_Data.CambiarestadoFactura(estado, codigo);
        }

        public void Verificarestadofactura()
        {
            v_Data.Verificarestadofactura();
        }

        public List<string> DetalleFacturaServicios(int codigoFactura)
        {
            return v_Data.DetalleFacturaServicios(codigoFactura);
        }

        public Int64 VerificarNombre(string nombre)
        {
            return v_Data.VerificarNombre(nombre);
        }

        public string tipoFactura(string codigo)
        {
            return v_Data.tipoFactura(codigo);
        }

        public string Descripcion_servicio(string codigo)
        {
            return v_Data.Descripcion_servicio(codigo);
        }

        //////////          BITÁCORA            //////////
        
        //Recibe como referencia una "EntidadBitacora" la cual va a ser enviada a la clase Data para proceder con la agregación de la bitácora.
        public int AgregarBitacora(EntidadBitacora clt)
        {
            return v_Data.AgregarBitacora(clt);
        }

        public DataTable MostrarBitacoraPorFecha(String fecha1, String fecha2)
        {
            return v_Data.MostrarBitacoraPorFecha(fecha1, fecha2);
        }

        public DataTable BitacoraMantenimieto(string mantenimiento)
        {
            return v_Data.BitacoraMantenimieto(mantenimiento);
        }

        public DataTable BitacoraAccion(string accion)
        {
            return v_Data.BitacoraAccion(accion);
        }

        public DataTable BitacoraMantenimietoyAccion(string mantenimiento, string accion)
        {
            return v_Data.BitacoraMantenimietoyAccion(mantenimiento, accion);
        }

        public DataTable MostrarBitacoraPorFechaNombreAccion(string mantenimiento, string accion, String fecha1, String fecha2)
        {
            return v_Data.MostrarBitacoraPorFechaNombreAccion(mantenimiento,accion,fecha1,fecha2);
        }

    }//Fin de la clase.
}//Fin del proyecto.