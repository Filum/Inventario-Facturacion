using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
using System.Data;

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
        public int AgregarProductos(EntidadProductos clt)
        {
            return v_Data.AgregarProductos(clt);
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

        public int ModificarProductos(EntidadProductos clt)
        {
            return v_Data.ModificarProductos(clt);
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
        public DataTable MostrarListaProductos(String v_EstadoSistema)
        {
            return v_Data.MostarListaProductos(v_EstadoSistema);
        }

        public DataTable MostrarListaClientes(String fecha1, String fecha2)
        {
            return v_Data.MostrarListaClientes(fecha1,fecha2);
        }

        public DataTable MostrarListaRoles(String v_EstadoSistema)
        {
            return v_Data.MostarListaRoles(v_EstadoSistema);
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

        //Recibe como referencia un string necesario para proceder con la existencia de cédulas de identificacion de usuarios
        public bool ValidarCodProductos(String v_CodProductos)
        {
            return v_Data.ValidarCodProductos(v_CodProductos);
        }

        /*Recibe como referencia una entidad proveedor que contiene el id y la cédula jurídica del proveedor con el fin de validar si la
        cédula jurídica está asociada a dicho id*/
        public bool ValidarModificacionProveedores(EntidadProveedores clt)
        {
            return v_Data.ValidarModificacionProveedores(clt);
        }

        /*Recibe como referencia una entidad usuario que contiene el id y la cédula del usuario con el fin de validar si la
        cédula está asociada a dicho id*/
        public bool ValidarModificacionUsuario(EntidadUsuarios clt)
        {
            return v_Data.ValidarModificacionUsuario(clt);
        }

        public bool ValidarModificacionProducto(EntidadProductos clt)
        {
            return v_Data.ValidarModificacionProducto(clt);
        }

        public List<EntidadClientes> BuscarClientes(String nombre)
        {
            return v_Data.BuscarClientes(nombre);
        }
        public List<EntidadUsuarios> ValidarUsuario(String nombreUsuario)
        {
            return v_Data.ValidarUsuario(nombreUsuario);
        }

   }
}
