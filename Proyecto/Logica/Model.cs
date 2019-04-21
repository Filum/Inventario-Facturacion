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
        Data v_Data = new Data();

        //---------------- A G R E G A R ---------------
        public int AgregarClientes(EntidadClientes clt)
        {
            return v_Data.AgregarClientes(clt);
        }

        //Recibe como referencia una entidad proveedores la cual va a ser enviada a la clase Data para proceder con la agregación del proveedor
        public int AgregarProveedores(EntidadProveedores clt)
        {
            return v_Data.AgregarProveedores(clt);
        }

        //Recibe como referencia una entidad usuarios la cual va a ser enviada a la clase Data para proceder con la agregación del usuario
        public int AgregarUsuarios(EntidadUsuarios clt)
        {
            return v_Data.AgregarUsuarios(clt);
        }

        //Recibe como referencia una entidad roles la cual va a ser enviada a la clase Data para proceder con la agregación del rol
        public int AgregarRoles(EntidadRoles clt)
        {
            return v_Data.AgregarRoles(clt);
        }

        public int AgregarFacturas(EntidadFacturas fact)
        {
            return v_Data.AgregarFacturas(fact);
        }
        public int AgregarDetalle(EntidadDetalleFactura fact)
        {
            return v_Data.AgregarDetalle(fact);
        }


            //------------------- M O D I F I C A R ---------------

            public int ModificarClientes(EntidadClientes clt)
        {
            return v_Data.ModificarClientes(clt);
        }

        //Recibe como referencia una entidad proveedores la cual va a ser enviada a la clase Data para proceder con la modificación del proveedor
        public int ModificarProveedores(EntidadProveedores clt)
        {
            return v_Data.ModificarProveedores(clt);
        }

        //Recibe como referencia una entidad proveedores la cual va a ser enviada a la clase Data para proceder con la modificación del proveedor
        public int ModificarUsuarios(EntidadUsuarios clt)
        {
            return v_Data.ModificarUsuarios(clt);
        }

        //Recibe como referencia una entidad ROLES la cual va a ser enviada a la clase Data para proceder con la modificación del ROL
        public int ModificarRoles(EntidadRoles clt)
        {
            return v_Data.ModificarRoles(clt);
        }


        //--------------- M O S T R A R -------------------
        //Recibe como referencia un estado en el sistema(ACTIVO, INACTIVO, LISTAPROVEEDORES) para realizar la consulta.
        public DataTable MostrarListaProveedores(String v_EstadoSistema)
        {
            return v_Data.MostarListaProveedores(v_EstadoSistema);
        }

        //Recibe como referencia un estado en el sistema(ACTIVO, INACTIVO, LISTAUSUARIOS) para realizar la consulta.
        public DataTable MostrarListaUsuarios(String v_EstadoSistema)
        {
            return v_Data.MostarListaUsuarios(v_EstadoSistema);
        }

        public List<EntidadProveedores> ProveedoresExistentes()
        {
            return v_Data.ProveedoresExistentes();
        }

        public DataTable CargarProveedores()
        {
            return v_Data.CargarProveedores();
        }

        //Recibe como referencia dos fechas las cuales van a ser enviadas a la clase Data para proceder con la actividad de listar productos
        public DataTable MostrarListaProductos()
        {
            return v_Data.MostarListaProductos();
        }

        public DataTable MostrarListaClientes(String fecha1, String fecha2)
        {
            return v_Data.MostrarListaClientes(fecha1, fecha2);
        }

        public DataTable MostrarListaRoles()
        {
            return v_Data.MostarListaRoles();
        }

        public List<EntidadRoles> RolesExistentes()
        {
            return v_Data.RolesExistentes();
        }

        //Recibe como referencia un string necesario para proceder con la existencia de proveedores
        public List<EntidadProveedores> ValidarBusquedaProveedores(String v_Busqueda)
        {
            return v_Data.ValidarBusquedaProveedores(v_Busqueda);
        }

        //Recibe como referencia un string necesario para proceder con la existencia de usuarios
        public List<EntidadUsuarios> ValidarBusquedaUsuarios(String v_Busqueda)
        {
            return v_Data.ValidarBusquedaUsuarios(v_Busqueda);
        }

        //Recibe como referencia un string necesario para proceder con la busqueda de productos
        public List<EntidadProductos> ValidarBusquedaProductos(String v_Busqueda)
        {
            return v_Data.ValidarBusquedaProductos(v_Busqueda);
        }

        public List<EntidadRoles> ValidarBusquedaRoles(String v_Busqueda)
        {
            return v_Data.ValidarBusquedaRoles(v_Busqueda);
        }

        //-------------------------- V A L I D A R C E D U L A -------------------
        //Recibe como referencia un string necesario para proceder con la existencia de cédulas jurídicas
        public bool ValidarCedJurProveedores(String v_CedJur)
        {
            return v_Data.ValidarCedJurProveedores(v_CedJur);
        }

        //Recibe como referencia un string necesario para proceder con la existencia de cédulas de identificacion de usuarios
        public bool ValidarNumCedUsuarios(String v_NumCed)
        {
            return v_Data.ValidarNumCedUsuarios(v_NumCed);
        }

        /*Recibe como referencia una entidad proveedor que contiene el id y la cédula jurídica del proveedor con el fin de validar si la
        cédula jurídica está asociada a dicho id*/
        public bool ValidarModificacionProveedores(EntidadProveedores clt)
        {
            return v_Data.ValidarModificacionProveedores(clt);
        }
        public DataTable Clientes()
        {
            return v_Data.Clientes();
        }

        /*Recibe como referencia una entidad usuario que contiene el id y la cédula del usuario con el fin de validar si la
        cédula está asociada a dicho id*/
        public bool ValidarModificacionUsuario(EntidadUsuarios clt)
        {
            return v_Data.ValidarModificacionUsuario(clt);
        }

        public List<EntidadClientes> BuscarClientes(String nombre)
        {
            return v_Data.BuscarClientes(nombre);
        }
        public DataTable MostrarListaFacturas(String fecha1, String fecha2)
        {
            return v_Data.MostrarListaFacturas(fecha1, fecha2);
        }
        public DataTable MostrarDetalleFactura(int codigoFactura)
        {
            return v_Data.MostrarDetalleFactura(codigoFactura);
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
        public DataTable BuscarFactura(String v_Nombre)
        {
            return v_Data.BuscarFactura(v_Nombre);
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
        public DataTable BuscarFacturaEstado(String estado)
        {
            return v_Data.BuscarFacturaEstado(estado);
        }
        public DataTable BuscarFacturaEstadoyCliente(string v_Nombre, string estado)
        {
            return v_Data.BuscarFacturaEstadoyCliente(v_Nombre,estado);
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


        }
}