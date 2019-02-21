using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data;
using Oracle.DataAccess.Client;

namespace Datos
{
    public class Data
    {
        //Conexion con web service de la pagina del banco, para consultar tipo de cambio
        cr.fi.bccr.gee.wsIndicadoresEconomicos cliente = new cr.fi.bccr.gee.wsIndicadoresEconomicos();
        
        //metodo para obtener  
       public float ObtenerValorDolar()
        {
            DateTime dia = DateTime.Now;
            string fecha = dia.ToShortDateString();

            DataSet tipoCambio = cliente.ObtenerIndicadoresEconomicos("317", fecha, fecha, "Usuario", "N");
            float valor = float.Parse(tipoCambio.Tables[0].Rows[0].ItemArray[2].ToString());
            return valor;
        }

        //----------------- A G R E G A R  -----------------------
        public int AgregarClientes(EntidadClientes clt)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("ADD_CLIENTES", conn as OracleConnection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new OracleParameter("NOM", clt.v_NombreCompleto));
            comando.Parameters.Add(new OracleParameter("TELOFI", clt.v_Teleoficina));
            comando.Parameters.Add(new OracleParameter("TELMOVIL", clt.v_Telemovil));
            comando.Parameters.Add(new OracleParameter("CORRE", clt.v_Correo));
            comando.Parameters.Add(new OracleParameter("CORREOPC", clt.v_CorreoOpc));
            comando.Parameters.Add(new OracleParameter("EST", clt.v_Inactividad));
            comando.Parameters.Add(new OracleParameter("OBVS", clt.v_Observaciones));
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }

        /*Se llama a un procedimiento almacenado(store procedure) que se encuentra en la base de datos el cual permite ingresar un nuevo proveedor.
        Si se modifica correctamente retorna un "-1" */
        public int AgregarProveedores(EntidadProveedores clt)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("ADD_PROVEEDORES", conn as OracleConnection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new OracleParameter("CEDJUR", clt.v_CedulaJuridica));
            comando.Parameters.Add(new OracleParameter("NOMB", clt.v_Nombre));
            comando.Parameters.Add(new OracleParameter("EMAIL", clt.v_Correo));
            comando.Parameters.Add(new OracleParameter("DESCRI", clt.v_Descripcion));
            comando.Parameters.Add(new OracleParameter("TELEFO", clt.v_Telefono));
            comando.Parameters.Add(new OracleParameter("TELOPCIONAL", clt.v_TelefonoOpcional));
            comando.Parameters.Add(new OracleParameter("EMAILOPC", clt.v_CorreoOpcional));

            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }
        /*Se llama a un procedimiento almacenado(store procedure) que se encuentra en la base de datos el cual permite ingresar un nuevo rol.
        Si se modifica correctamente retorna un "-1" */
        public int AgregarRoles(EntidadRoles clt)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("ADD_ROLES", conn as OracleConnection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new OracleParameter("NOMBR", clt.v_Nombre));
            comando.Parameters.Add(new OracleParameter("MANTENIMIENTOS", clt.v_Mantenimientos));
            comando.Parameters.Add(new OracleParameter("ESTADO", clt.v_Estado));

            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }

        //------------ M O D I F I C A R ---------------------------------
        public int ModificarClientes(EntidadClientes clt)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("act_CLIENTES", conn as OracleConnection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new OracleParameter("NOM", clt.v_NombreCompleto));
            comando.Parameters.Add(new OracleParameter("TELOFI", clt.v_Teleoficina));
            comando.Parameters.Add(new OracleParameter("TELMOVIL", clt.v_Telemovil));
            comando.Parameters.Add(new OracleParameter("ID_CLIENTE", clt.v_Codigo));
            comando.Parameters.Add(new OracleParameter("CORRE", clt.v_Correo));
            comando.Parameters.Add(new OracleParameter("CORREOPC", clt.v_CorreoOpc));
            comando.Parameters.Add(new OracleParameter("EST", clt.v_Inactividad));
            comando.Parameters.Add(new OracleParameter("OBVS", clt.v_Observaciones));
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }

        /*Se llama a un procedimiento almacenado(store procedure) que se encuentra en la base de datos el cual permite modificar un nuevo proveedor.
        Si se modifica correctamente retorna un "-1" */
        public int ModificarProveedores(EntidadProveedores clt)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("act_Proveedores", conn as OracleConnection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new OracleParameter("IDPROVEEDOR", clt.v_IdProveedor));
            comando.Parameters.Add(new OracleParameter("CEDJUR", clt.v_CedulaJuridica));
            comando.Parameters.Add(new OracleParameter("NOMB", clt.v_Nombre));
            comando.Parameters.Add(new OracleParameter("EMAIL", clt.v_Correo));
            comando.Parameters.Add(new OracleParameter("DESCRI", clt.v_Descripcion));
            comando.Parameters.Add(new OracleParameter("TELEFO", clt.v_Telefono));
            comando.Parameters.Add(new OracleParameter("TELOPCIONAL", clt.v_TelefonoOpcional));
            comando.Parameters.Add(new OracleParameter("EMAILOPC", clt.v_CorreoOpcional));
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }

        /*Se llama a un procedimiento almacenado(store procedure) que se encuentra en la base de datos el cual permite modificar un rol.
       Si se modifica correctamente retorna un "-1" */
        public int ModificarRoles(EntidadRoles clt)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("act_Roles", conn as OracleConnection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new OracleParameter("IDROL", clt.v_IdRol));
            comando.Parameters.Add(new OracleParameter("NOMBR", clt.v_Nombre));
            comando.Parameters.Add(new OracleParameter("MANTENIMIENTOS", clt.v_Mantenimientos));
            comando.Parameters.Add(new OracleParameter("ESTADO", clt.v_Estado));
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }

        //--------------------- M O S T R A R ----------------------------- 
        public DataTable MostrarListaClientes(String fecha1, String fecha2)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select * from tbl_Clientes where trunc(fecha) BETWEEN '" + fecha1 + "' AND '" + fecha2 + "'";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }

        /*Este método recibe un rango de fechas por parámetros para consultarlo con la base de datos y obtener los proveedores ingresados en este rango.
         Además, en caso de encontrar proveedores estos serán retornados mediante una tabla*/
        public DataTable MostarListaProveedores(String v_Fecha1, String v_Fecha2)
        {  
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select cedulaJuridica,nombre,correo,correoOpcional,telefono,telefonoOpcional,descripcion,fecha from tbl_Proveedores where trunc(fecha) BETWEEN '" + v_Fecha1+ "' AND '" + v_Fecha2 + "'";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }

        /*Este método recibe un rango de fechas por parámetros para consultarlo con la base de datos y obtener los proveedores ingresados en este rango.
         Además, en caso de encontrar proveedores estos serán retornados mediante una tabla*/
        public DataTable MostarListaRoles(String v_Fecha1, String v_Fecha2)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select fecha,nombre,mantenimientos,estado from tbl_Roles where trunc(fecha) BETWEEN '" + v_Fecha1 + "' AND '" + v_Fecha2 + "'";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }

        // ----------------- B Ú S Q U E D A -------------------------
        public List<EntidadClientes> BuscarClientes(String v_Nombre)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT * FROM TBL_CLIENTES WHERE NOMBRE LIKE '%" + v_Nombre + "%'";
            OracleDataReader dr = comando.ExecuteReader();
            List<EntidadClientes> Lista = new List<EntidadClientes>();

            if (v_Nombre != "")
            {
                while (dr.Read())
                {
                    EntidadClientes cliente = new EntidadClientes();
                    cliente.v_Codigo = dr.GetInt32(0);
                    cliente.v_NombreCompleto = dr.GetString(2);
                    cliente.v_Teleoficina = Convert.ToInt32(dr.GetValue(3));
                    cliente.v_Telemovil = Convert.ToInt32(dr.GetValue(4));
                    cliente.v_Correo = dr.GetString(5);
                    cliente.v_CorreoOpc = dr.GetString(6);
                    cliente.v_Inactividad = dr.GetString(7);
                    cliente.v_Observaciones = dr.GetString(8);
                    Lista.Add(cliente);
                }
            }
            conn.Close();
            return Lista;
        }

        /*Este método recibe un parámetro tipo string con el cual buscara en la base de datos la existencia del proveedor mediante el nombre o la cédula jurídica.
         Además, en caso de encontrar proveedores estos serán retornados mediante una lista*/
        public List<EntidadProveedores> ValidarBusquedaProveedores(String v_busqueda)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT * FROM TBL_PROVEEDORES WHERE translate(UPPER(NOMBRE),'ÁÉÍÓÚ', 'AEIOU') LIKE translate(UPPER('%" + v_busqueda  + "%'),'ÁÉÍÓÚ', 'AEIOU') OR CEDULAJURIDICA LIKE '%" + v_busqueda + "%'";
            OracleDataReader dr = comando.ExecuteReader();
            List<EntidadProveedores> Lista = new List<EntidadProveedores>();

            if (v_busqueda != "")
            {
                while (dr.Read())
                {
                    EntidadProveedores proveedor = new EntidadProveedores();
                    proveedor.v_IdProveedor = dr.GetInt64(0);
                    proveedor.v_CedulaJuridica = Convert.ToInt64(dr.GetValue(2));
                    proveedor.v_Nombre = dr.GetString(3);
                    proveedor.v_Telefono = Convert.ToInt64(dr.GetValue(6));
                    proveedor.v_TelefonoOpcional = Convert.ToInt64(dr.GetValue(7));
                    proveedor.v_Correo = dr.GetString(4);
                    proveedor.v_CorreoOpcional = dr.GetString(8);
                    proveedor.v_Descripcion = dr.GetString(5);
                    proveedor.v_Fecha = Convert.ToDateTime(dr.GetValue(1));
                    Lista.Add(proveedor);
                }
            }
            conn.Close();
            return Lista;
        }

        /*Este método recibe un parámetro tipo string con el cual buscara en la base de datos la existencia del rol mediante el nombre 
         Además, en caso de encontrar proveedores estos serán retornados mediante una lista*/
        public List<EntidadRoles> ValidarBusquedaRoles(String v_busqueda)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT * FROM TBL_ROLES WHERE translate(UPPER(NOMBRE),'ÁÉÍÓÚ', 'AEIOU') LIKE translate(UPPER('%" + v_busqueda + "%'),'ÁÉÍÓÚ', 'AEIOU')";
            OracleDataReader dr = comando.ExecuteReader();
            List<EntidadRoles> Lista = new List<EntidadRoles>();

            if (v_busqueda != "")
            {
                while (dr.Read())
                {
                    EntidadRoles rol = new EntidadRoles();
                    rol.v_IdRol = dr.GetInt64(0);
                    rol.v_Fecha = Convert.ToDateTime(dr.GetValue(1));
                    rol.v_Nombre = dr.GetString(2);
                    rol.v_Mantenimientos = dr.GetString(3); ;
                    rol.v_Estado = dr.GetString(4); ;
                    Lista.Add(rol);
                }
            }
            conn.Close();
            return Lista;
        }

        /*Este método recibe un parámetro tipo string con el cual buscara en la base de datos la existencia del producto mediante el nombre o el codigo.
         Además, en caso de encontrar productos estos serán retornados mediante una lista*/
        public List<EntidadProductos> ValidarBusquedaProductos(String v_busqueda)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select PROD.pk_idProducto,PROD.codigoProducto,PROD.nombreProducto,PROD.marcaProducto,PROD.cantidadExistencia,PROD.cantidadMinima,PROV.NOMBRE,PROD.precioUnitario,PROD.descripcion,PROD.fabricante,PROD.estado,PROD.fecha " +
                "from tbl_Productos PROD INNER JOIN TBL_PROVEEDORES PROV ON (PROV.PK_IDPROVEEDOR = PROD.FK_IDPROVEEDOR) WHERE translate(UPPER(NOMBREPRODUCTO),'ÁÉÍÓÚ', 'AEIOU') LIKE translate(UPPER('%" + v_busqueda + "%'),'ÁÉÍÓÚ', 'AEIOU') " +
                "OR translate(UPPER(CODIGOPRODUCTO),'ÁÉÍÓÚ', 'AEIOU') LIKE translate(UPPER('%" + v_busqueda + "%'),'ÁÉÍÓÚ', 'AEIOU')";
            OracleDataReader dr = comando.ExecuteReader();
            List<EntidadProductos> Lista = new List<EntidadProductos>();

            if (v_busqueda != "")
            {
                while (dr.Read())
                {
                    EntidadProductos producto = new EntidadProductos();
                    producto.v_IdProducto = dr.GetInt64(0);
                    producto.v_CodigoProducto = dr.GetString(1);
                    producto.v_NombreProducto = dr.GetString(2);
                    producto.v_MarcaProducto = dr.GetString(3);
                    producto.v_CantidadExistencia = Convert.ToInt64(dr.GetValue(4));
                    producto.v_CantidadMinima = Convert.ToInt32(dr.GetValue(5));
                    producto.v_Fk_IdProveedor = dr.GetString(6);
                    producto.v_PrecioUnitario = Convert.ToInt64(dr.GetValue(7));
                    producto.v_Descripcion = dr.GetString(8);
                    producto.v_Fabricante = dr.GetString(9);
                    producto.v_Estado = dr.GetString(10);
                    producto.v_Fecha = Convert.ToDateTime(dr.GetValue(11));
                    Lista.Add(producto);
                }
            }
            conn.Close();
            return Lista;
        }

        /*Este método recibe un parámetro tipo string con el cual buscara en la base de datos la existencia de la cédula jurídica.
         Además, en caso de encontrar dicha cédula jurídica retornara un true, en caso contrario retornara un false*/
        public bool ValidarCedJurProveedores(String v_CedJur)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT CEDULAJURIDICA FROM TBL_PROVEEDORES WHERE CEDULAJURIDICA = " + v_CedJur;
            OracleDataReader dr = comando.ExecuteReader();

            if (v_CedJur != "")
            {
                while (dr.Read())
                {
                    return true;
                }
            }
            conn.Close();
            return false;
        }

        /*Recibe como referencia una entidad proveedor que contiene el id y la cédula jurídica del proveedor con el fin de validar si la
        cédula jurídica está asociada a dicho id.
        Además, en caso de estar asociados retornara un true, en caso contrario retornara un false, esto es con el fin de que al momento de modificar si el
        usuario ingresa o deja la misma cédula que tenia,no le de error*/
        public Boolean ValidarModificacionProveedores(EntidadProveedores clt)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT pk_idProveedor,cedulaJuridica from tbl_Proveedores where pk_idProveedor = " + clt.v_IdProveedor + " AND cedulaJuridica = " + clt.v_CedulaJuridica;

            OracleDataReader dr = comando.ExecuteReader();

            if (clt.v_IdProveedor != 0 && clt.v_CedulaJuridica != 0)
            {
                while (dr.Read())
                {
                    return true;
                }
            }
            conn.Close();
            return false;
        }

        public DataTable MostarListaProductos(String v_Fecha1, String v_Fecha2)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select PROD.codigoProducto,PROD.nombreProducto,PROD.marcaProducto,PROD.cantidadExistencia,PROD.cantidadMinima,PROV.NOMBRE,PROD.precioUnitario,PROD.descripcion,PROD.fabricante,PROD.estado,PROD.fecha from tbl_Productos PROD INNER JOIN TBL_PROVEEDORES PROV ON(PROV.PK_IDPROVEEDOR = PROD.FK_IDPROVEEDOR) where trunc(PROD.fecha) BETWEEN '" + v_Fecha1 + "' AND '" + v_Fecha2 + "'";
            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }

        public List<EntidadProveedores> ProveedoresExistentes()
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT PK_IDPROVEEDOR, NOMBRE FROM TBL_PROVEEDORES";
            OracleDataReader dr = comando.ExecuteReader();
            List<EntidadProveedores> Lista = new List<EntidadProveedores>();


            while (dr.Read())
            {
                EntidadProveedores proveedor = new EntidadProveedores();
                proveedor.v_IdProveedor = dr.GetInt64(0);
                proveedor.v_Nombre = dr.GetString(1);
                Lista.Add(proveedor);
            }
            conn.Close();
            return Lista;
        }


        //----------------------------------FACTURACION----------------------------------------------------------------
        //Metodo para cargar los clientes activos del sistema
        public DataTable Clientes()
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT NOMBRE FROM TBL_CLIENTES WHERE ESTADO='Activo'";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }
        public DataTable MostrarListaFacturas(String fecha1, String fecha2)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT TBL_FACTURAS.PK_CODIGOFACTURA,TBL_FACTURAS.FECHA,TBL_USUARIOS.NOMBREUSUARIO,TBL_CLIENTES.NOMBRE,TBL_FACTURAS.TOTAL,MONEDA,IMPUESTO,TBL_FACTURAS.DESCUENTO FROM TBL_FACTURAS INNER JOIN TBL_USUARIOS ON TBL_FACTURAS.FK_IDUSUARIO=TBL_USUARIOS.PK_IDUSUARIO INNER JOIN TBL_CLIENTES ON TBL_FACTURAS.FK_IDCLIENTE=TBL_CLIENTES.PK_IDCLIENTE where trunc(TBL_FACTURAS.FECHA) BETWEEN '" + fecha1 + "' AND '" + fecha2 + "'";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }

        public DataTable MostrarDetalleFactura(int codigoFactura)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT TBL_FACTURAS.PK_CODIGOFACTURA,TBL_PRODUCTOS.CODIGOPRODUCTO,TBL_PRODUCTOS.NOMBREPRODUCTO,TBL_PRODUCTOS.MARCAPRODUCTO,TBL_PRODUCTOS.PRECIOUNITARIO,TBL_DETALLES.CANTIDAD_PRODUCTO,TBL_DETALLES.TOTAL,TBL_FACTURAS.MONEDA FROM TBL_DETALLES INNER JOIN TBL_FACTURAS ON TBL_DETALLES.FK_IDFACTURA = TBL_FACTURAS.PK_CODIGOFACTURA AND TBL_FACTURAS.PK_CODIGOFACTURA = '"+codigoFactura+"' INNER JOIN TBL_PRODUCTOS ON TBL_DETALLES.FK_IDPRODUCTO = TBL_PRODUCTOS.PK_IDPRODUCTO";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }
        public List<string> DetalleFactura(int codigoFactura)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT TBL_FACTURAS.PK_CODIGOFACTURA,TBL_FACTURAS.FECHA,TBL_USUARIOS.NOMBREUSUARIO,TBL_CLIENTES.NOMBRE FROM TBL_FACTURAS INNER JOIN TBL_USUARIOS ON TBL_FACTURAS.FK_IDUSUARIO=TBL_USUARIOS.PK_IDUSUARIO INNER JOIN TBL_CLIENTES ON TBL_FACTURAS.FK_IDCLIENTE=TBL_CLIENTES.PK_IDCLIENTE WHERE TBL_FACTURAS.PK_CODIGOFACTURA = '"+codigoFactura+"'";
            OracleDataReader dr = comando.ExecuteReader();
            var Lista = new List<string>();


            while (dr.Read())
            {
                Lista.Add(dr.GetInt64(0).ToString());
                Lista.Add(dr.GetDateTime(1).ToString());
                Lista.Add(dr.GetString(2));
                Lista.Add(dr.GetString(3));

            }
            conn.Close();
            return Lista;
        }
    }
}
