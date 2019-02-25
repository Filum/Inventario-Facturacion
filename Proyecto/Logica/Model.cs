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
        //Recibe como referencia una entidad roles la cual va a ser enviada a la clase Data para proceder con la agregación del rol
        public int AgregarRoles(EntidadRoles clt)
        {
            return v_Data.AgregarRoles(clt);
        }

        public int AgregarFacturas(EntidadFacturas fact)
        {
            return v_Data.AgregarFacturas(fact);
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

        //Recibe como referencia una entidad ROLES la cual va a ser enviada a la clase Data para proceder con la modificación del ROL
        public int ModificarRoles(EntidadRoles clt)
        {
            return v_Data.ModificarRoles(clt);
        }


        //--------------- M O S T R A R -------------------
        //Recibe como referencia dos fechas las cuales van a ser enviadas a la clase Data para proceder con la actividad de listar proveedores
        public DataTable MostrarListaProveedores(String v_Fecha1, String v_Fecha2)
        {
            return v_Data.MostarListaProveedores(v_Fecha1, v_Fecha2);
        }

        public List<EntidadProveedores> ProveedoresExistentes()
        {
            return v_Data.ProveedoresExistentes();
        }

        //Recibe como referencia dos fechas las cuales van a ser enviadas a la clase Data para proceder con la actividad de listar productos
        public DataTable MostrarListaProductos(String v_Fecha1, String v_Fecha2)
        {
            return v_Data.MostarListaProductos(v_Fecha1, v_Fecha2);
        }

        public DataTable MostrarListaClientes(String fecha1, String fecha2)
        {
            return v_Data.MostrarListaClientes(fecha1, fecha2);
        }

        public DataTable MostrarListaRoles(String fecha1, String fecha2)
        {
            return v_Data.MostarListaRoles(fecha1, fecha2);
        }

        //Recibe como referencia un string necesario para proceder con la existencia de proveedores
        public List<EntidadProveedores> ValidarBusquedaProveedores(String v_Busqueda)
        {
            return v_Data.ValidarBusquedaProveedores(v_Busqueda);
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

        /*Recibe como referencia una entidad proveedor que contiene el id y la cédula jurídica del proveedor con el fin de validar si la
        cédula jurídica está asociada a dicho id*/
        public bool ValidarModificacionProveedores(EntidadProveedores clt)
        {
            return v_Data.ValidarModificacionProveedores(clt);
        }
       public DataTable Clientes()
        {
            return  v_Data.Clientes();
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
    }
}
