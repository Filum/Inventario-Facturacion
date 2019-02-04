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

        public int AgregarClientes(EntidadClientes clt)
        {
            return v_Data.AgregarClientes(clt);
        }

        public int ModificarClientes(EntidadClientes clt)
        {
            return v_Data.ModificarClientes(clt);
        }

        //Recibe como referencia una entidad proveedores la cual va a ser enviada a la clase Data para proceder con la modificación del proveedor
        public int ModificarProveedores(EntidadProveedores clt)
        {
            return v_Data.ModificarProveedores(clt);
        }

        //Recibe como referencia una entidad proveedores la cual va a ser enviada a la clase Data para proceder con la agregación del proveedor
        public int AgregarProveedores(EntidadProveedores clt)
        {
            return v_Data.AgregarProveedores(clt);
        }
        public List<EntidadClientes> BuscarClientes(String nombre)
        {
            return v_Data.BuscarClientes(nombre);
        }

        //Recibe como referencia dos fechas las cuales van a ser enviadas a la clase Data para proceder con la actividad de listar proveedores
        public DataTable MostrarListaProveedores(String v_Fecha1, String v_Fecha2)
        {
            return v_Data.MostarListaProveedores(v_Fecha1, v_Fecha2);
        }

        //Recibe como referencia dos fechas las cuales van a ser enviadas a la clase Data para proceder con la actividad de listar productos
        public DataTable MostrarListaProductos(String v_Fecha1, String v_Fecha2)
        {
            return v_Data.MostarListaProductos(v_Fecha1, v_Fecha2);
        }

        public DataTable MostrarListaClientes(String fecha1, String fecha2)
        {
            return v_Data.MostrarListaClientes(fecha1,fecha2);
        }

        //Recibe como referencia un string necesario para proceder con la existencia de proveedores
        public List<EntidadProveedores> ValidarBusquedaProveedores(String v_Busqueda)
        {
            return v_Data.ValidarBusquedaProveedores(v_Busqueda);
        }

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
    }
}
