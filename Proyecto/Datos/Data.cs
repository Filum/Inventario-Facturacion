using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data;
using Oracle.DataAccess.Client;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Datos
{
    public class Data
    {
        //Conexion con web service de la pagina del banco, para consultar tipo de cambio
        cr.fi.bccr.gee.wsIndicadoresEconomicos cliente = new cr.fi.bccr.gee.wsIndicadoresEconomicos();

        //metodo para obtener  
        public float ObtenerValorDolar()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
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
            comando.Parameters.Add(new OracleParameter("CED", clt.v_Cedula));
            comando.Parameters.Add(new OracleParameter("REP", clt.v_Representante));
            comando.Parameters.Add(new OracleParameter("DIREC", clt.v_Direccion));
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
            comando.Parameters.Add(new OracleParameter("ESTSIS", clt.v_EstadoSistema));

            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }

        /*Se llama a un procedimiento almacenado(store procedure) que se encuentra en la base de datos el cual permite ingresar un nuevo usuario.
        Si se modifica correctamente retorna un "-1" */
        public int AgregarUsuarios(EntidadUsuarios clt)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("ADD_USUARIOS", conn as OracleConnection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new OracleParameter("NUMCED", clt.v_CedIdentificacion));
            comando.Parameters.Add(new OracleParameter("NOM", clt.v_NombreUsuario));
            comando.Parameters.Add(new OracleParameter("APE", clt.v_Apellidos));
            comando.Parameters.Add(new OracleParameter("TEL", clt.v_Telefono));
            comando.Parameters.Add(new OracleParameter("TELOPC", clt.v_TelefonoOpcional));
            comando.Parameters.Add(new OracleParameter("CORR", clt.v_Correo));
            comando.Parameters.Add(new OracleParameter("PUES", clt.v_Puesto));
            comando.Parameters.Add(new OracleParameter("ID_ROL", clt.v_IdRol));
            comando.Parameters.Add(new OracleParameter("USUA", clt.v_UsuarioSistema));
            comando.Parameters.Add(new OracleParameter("CONTRA", clt.v_Contrasena));
            comando.Parameters.Add(new OracleParameter("ESTSIS", clt.v_EstadoSistema));

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
            comando.Parameters.Add(new OracleParameter("NOM", clt.v_Nombre));
            comando.Parameters.Add(new OracleParameter("MANTCLIENT", clt.v_Mantenimiento_Clientes));
            comando.Parameters.Add(new OracleParameter("MANTPROV", clt.v_Mantenimiento_Proveedores));
            comando.Parameters.Add(new OracleParameter("MANTPROD", clt.v_Mantenimiento_Productos));
            comando.Parameters.Add(new OracleParameter("MANTUSER", clt.v_Mantenimiento_Usuarios));
            comando.Parameters.Add(new OracleParameter("MANTROL", clt.v_Mantenimiento_Roles));
            comando.Parameters.Add(new OracleParameter("FACTU", clt.v_facturacion));
            comando.Parameters.Add(new OracleParameter("BITAC", clt.v_bitacora));
            comando.Parameters.Add(new OracleParameter("ESTSIS", clt.v_Estado));


            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }

        public int AgregarFacturas(EntidadFacturas fact)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("ADD_FACTURAS", conn as OracleConnection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new OracleParameter("ID_FACTURA", fact.v_Codigo));
            comando.Parameters.Add(new OracleParameter("ID_USU", fact.v_Usuario));
            comando.Parameters.Add(new OracleParameter("ID_CLIE", fact.v_Cliente));
            comando.Parameters.Add(new OracleParameter("V_TOTAL", fact.v_Total));
            comando.Parameters.Add(new OracleParameter("V_DESCUENTO", fact.v_Descuento));
            comando.Parameters.Add(new OracleParameter("V_MONEDA", fact.v_Moneda));
            comando.Parameters.Add(new OracleParameter("V_IMP", fact.v_Impuesto));
            comando.Parameters.Add(new OracleParameter("V_TIPO", fact.v_tipoFactura));
            comando.Parameters.Add(new OracleParameter("V_SUBTOTAL", fact.v_Subtotal));
            comando.Parameters.Add(new OracleParameter("V_SUBNETO", fact.v_SubtotalNeto));
            comando.Parameters.Add(new OracleParameter("V_TPAGO", fact.v_tipoPago));
            comando.Parameters.Add(new OracleParameter("V_DCREDITO", fact.v_diasCredito));
            comando.Parameters.Add(new OracleParameter("V_EFACTURA", fact.v_estadoFactura));
            comando.Parameters.Add(new OracleParameter("V_FPAGO", fact.v_fechaPago));
            comando.Parameters.Add(new OracleParameter("V_FCANCELAR", fact.v_fechaCancelacion));
            comando.Parameters.Add(new OracleParameter("V_TIPOCAM", fact.v_tipoCambio));
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }

        /*Se llama a un procedimiento almacenado(store procedure) que se encuentra en la base de datos el cual permite ingresar un nuevo producto.
       Si se modifica correctamente retorna un "-1" */
        public int AgregarProductos(EntidadProductos clt)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("add_Productos", conn as OracleConnection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new OracleParameter("COD_PRO", clt.v_CodigoProducto));
            comando.Parameters.Add(new OracleParameter("NOM_PRODUCTO", clt.v_NombreProducto));
            comando.Parameters.Add(new OracleParameter("MARCA", clt.v_MarcaProducto));
            comando.Parameters.Add(new OracleParameter("CANT_EXI", clt.v_CantidadExistencia));
            comando.Parameters.Add(new OracleParameter("CANT_MIN", clt.v_CantidadMinima));
            comando.Parameters.Add(new OracleParameter("ID_PROVEEDOR", clt.v_IdProveedor));
            comando.Parameters.Add(new OracleParameter("PRECIO", clt.v_PrecioUnitario));
            comando.Parameters.Add(new OracleParameter("DESCRIP", clt.v_Descripcion));
            comando.Parameters.Add(new OracleParameter("FABRI", clt.v_Fabricante));
            comando.Parameters.Add(new OracleParameter("ESTPRO", clt.v_EstadoProducto));
            comando.Parameters.Add(new OracleParameter("ESTSIS", clt.v_EstadoSistema));


            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }
        public int AgregarBitacora(EntidadBitacora clt)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("add_Bitacora", conn as OracleConnection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new OracleParameter("ACCI", clt.accion));
            comando.Parameters.Add(new OracleParameter("USU_RES", clt.usuario_Responsable));
            comando.Parameters.Add(new OracleParameter("VENT_MANT", clt.ventana_Mantenimiento));
            comando.Parameters.Add(new OracleParameter("DESCRIP", clt.descripcion));


            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }
        public int AgregarDetalle(EntidadDetalleFactura fact)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("ADD_DETALLES", conn as OracleConnection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new OracleParameter("TOTAL", fact.subtotal));
            comando.Parameters.Add(new OracleParameter("DESCRIPCION_SERVICIO", fact.descripcion_servicio));
            comando.Parameters.Add(new OracleParameter("FK_IDPRODUCTO", fact.id_producto));
            comando.Parameters.Add(new OracleParameter("FK_IDFACTURA", fact.id_factura));
            comando.Parameters.Add(new OracleParameter("CANTIDAD_PRODUCTO", fact.cantidad));
            comando.Parameters.Add(new OracleParameter("IMPUESTO", fact.impuesto));
            comando.Parameters.Add(new OracleParameter("DESCUENTO", fact.descuento));
            comando.Parameters.Add(new OracleParameter("PRECIOPRODUCT", fact.precioProducto));
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
            comando.Parameters.Add(new OracleParameter("CED", clt.v_Cedula));
            comando.Parameters.Add(new OracleParameter("REP", clt.v_Representante));
            comando.Parameters.Add(new OracleParameter("DIREC", clt.v_Direccion));
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
            comando.Parameters.Add(new OracleParameter("ESTSIS", clt.v_EstadoSistema));
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }

        /*Se llama a un procedimiento almacenado(store procedure) que se encuentra en la base de datos el cual permite modificar un nuevo proveedor.
        Si se modifica correctamente retorna un "-1" */
        public int ModificarUsuarios(EntidadUsuarios clt)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("act_Usuarios", conn as OracleConnection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new OracleParameter("IDUSUARIO", clt.v_IdUsuario));
            comando.Parameters.Add(new OracleParameter("NUMCED", clt.v_CedIdentificacion));
            comando.Parameters.Add(new OracleParameter("NOM", clt.v_NombreUsuario));
            comando.Parameters.Add(new OracleParameter("APE", clt.v_Apellidos));
            comando.Parameters.Add(new OracleParameter("TEL", clt.v_Telefono));
            comando.Parameters.Add(new OracleParameter("TELOPC", clt.v_TelefonoOpcional));
            comando.Parameters.Add(new OracleParameter("CORR", clt.v_Correo));
            comando.Parameters.Add(new OracleParameter("PUES", clt.v_Puesto));
            comando.Parameters.Add(new OracleParameter("ID_ROL", clt.v_IdRol));
            comando.Parameters.Add(new OracleParameter("USUA", clt.v_UsuarioSistema));
            comando.Parameters.Add(new OracleParameter("CONTRA", clt.v_Contrasena));
            comando.Parameters.Add(new OracleParameter("ESTSIS", clt.v_EstadoSistema));

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
            comando.Parameters.Add(new OracleParameter("MANTCLIENTES", clt.v_Mantenimiento_Clientes));
            comando.Parameters.Add(new OracleParameter("MANTPROV", clt.v_Mantenimiento_Proveedores));
            comando.Parameters.Add(new OracleParameter("MANTPROD", clt.v_Mantenimiento_Productos));
            comando.Parameters.Add(new OracleParameter("MANTUSER", clt.v_Mantenimiento_Usuarios));
            comando.Parameters.Add(new OracleParameter("MANTROL", clt.v_Mantenimiento_Roles));
            comando.Parameters.Add(new OracleParameter("FACTU", clt.v_facturacion));
            comando.Parameters.Add(new OracleParameter("BITACORA", clt.v_bitacora));
            comando.Parameters.Add(new OracleParameter("ESTADO", clt.v_Estado));
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }

        /*Se llama a un procedimiento almacenado(store procedure) que se encuentra en la base de datos el cual permite modificar un producto.
      Si se modifica correctamente retorna un "-1" */
        public int ModificarProductos(EntidadProductos clt)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("act_Productos", conn as OracleConnection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new OracleParameter("IDPRO", clt.v_IdProducto));
            comando.Parameters.Add(new OracleParameter("COD_PRO", clt.v_CodigoProducto));
            comando.Parameters.Add(new OracleParameter("NOM_PRODUCTO", clt.v_NombreProducto));
            comando.Parameters.Add(new OracleParameter("MARCA", clt.v_MarcaProducto));
            comando.Parameters.Add(new OracleParameter("CANT_EXI", clt.v_CantidadExistencia));
            comando.Parameters.Add(new OracleParameter("CANT_MIN", clt.v_CantidadMinima));
            comando.Parameters.Add(new OracleParameter("ID_PROVEEDOR", clt.v_IdProveedor));
            comando.Parameters.Add(new OracleParameter("PRECIO", clt.v_PrecioUnitario));
            comando.Parameters.Add(new OracleParameter("DESCR", clt.v_Descripcion));
            comando.Parameters.Add(new OracleParameter("FABRI", clt.v_Fabricante));
            comando.Parameters.Add(new OracleParameter("ESTPRO", clt.v_EstadoProducto));
            comando.Parameters.Add(new OracleParameter("ESTSIS", clt.v_EstadoSistema));

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
            comando.CommandText = "select PK_IDCLIENTE,NOMBRE,TELEFONOOFICINA,TELEFONOMOVIL,CORREO,ESTADO,CEDULA,REPRESENTANTE,DIRECCION  from tbl_Clientes where trunc(fecha) BETWEEN '" + fecha1 + "' AND '" + fecha2 + "'";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }

        /*Este método recibe un estado en el sistema(ACTIVO, INACTIVO, LISTAPROVEEDORES) para realizar la consulta. 
         * En caso de encontrar proveedores estos serán retornados mediante una tabla*/
        public DataTable MostarListaProveedores(String v_EstadoSistema)
        {  
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            if (v_EstadoSistema == "LISTAPROVEEDORES")
            {
                comando.CommandText = "select cedulaJuridica, nombre, correo, correoOpcional, telefono, telefonoOpcional, descripcion, fecha, estadoSistema from tbl_Proveedores";
            }
            else
            {
                comando.CommandText = "select cedulaJuridica,nombre,correo,correoOpcional,telefono,telefonoOpcional,descripcion,fecha,estadoSistema from tbl_Proveedores where estadoSistema = '" + v_EstadoSistema + "'";
            }

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }

        /*Este métodorecibe un estado en el sistema(ACTIVO, INACTIVO, LISTAUSUARIOS) para realizar la consulta. 
         * En caso de encontrar usuarios estos serán retornados mediante una tabla*/
        public DataTable MostarListaUsuarios(String v_EstadoSistema)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            if (v_EstadoSistema == "LISTAUSUARIOS")
            {
                comando.CommandText = "select USU.CEDULAIDENTIFICACION,USU.NOMBREUSUARIO,USU.APELLIDOS,USU.TELEFONO,USU.TELEFONOOPCIONAL,USU.CORREO,USU.PUESTO,ROL.NOMBRE,USU.USUARIOSISTEMA,USU.CONTRASENA,USU.FECHA,USU.ESTADOSISTEMA from tbl_Usuarios USU INNER JOIN tbl_Roles ROL ON(ROL.PK_IDROL = USU.FK_IDROL)";
            }
            else
            {
                comando.CommandText = "select USU.CEDULAIDENTIFICACION,USU.NOMBREUSUARIO,USU.APELLIDOS,USU.TELEFONO,USU.TELEFONOOPCIONAL,USU.CORREO,USU.PUESTO,ROL.NOMBRE,USU.USUARIOSISTEMA,USU.CONTRASENA,USU.FECHA,USU.ESTADOSISTEMA from tbl_Usuarios USU INNER JOIN tbl_Roles ROL ON(ROL.PK_IDROL = USU.FK_IDROL) WHERE USU.ESTADOSISTEMA = '" + v_EstadoSistema + "'";
            }

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }

        /*Este método recibe un rango de fechas por parámetros para consultarlo con la base de datos y obtener los proveedores ingresados en este rango.
         Además, en caso de encontrar proveedores estos serán retornados mediante una tabla*/
        public DataTable MostarListaRoles(String v_EstadoSistema)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;

            if (v_EstadoSistema == "LISTAROLES")
            {
                comando.CommandText = "select nombre,mantenimiento_clientes,mantenimiento_proveedores,mantenimiento_productos,mantenimiento_usuarios,mantenimiento_roles,facturacion,bitacora,estadosistema from tbl_Roles";
            }
            else
            {
                comando.CommandText = "select nombre,mantenimiento_clientes,mantenimiento_proveedores,mantenimiento_productos,mantenimiento_usuarios,mantenimiento_roles,facturacion,bitacora,estadosistema from tbl_Roles WHERE estadosistema  = '" + v_EstadoSistema + "'";
            }


            

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }

        public DataTable MostarListaProductos(String v_EstadoSistema)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            if (v_EstadoSistema == "LISTAPRODUCTOS")
            {
                comando.CommandText = "select PROD.codigoProducto,PROD.nombreProducto,PROD.marcaProducto,PROD.cantidadExistencia,PROV.NOMBRE,PROD.precioUnitario,PROD.descripcion,PROD.fabricante,PROD.estadoProducto,PROD.fecha,PROD.ESTADOSISTEMA from tbl_Productos PROD INNER JOIN TBL_PROVEEDORES PROV ON(PROV.PK_IDPROVEEDOR = PROD.FK_IDPROVEEDOR)";
            }
            else
            {
                comando.CommandText = "select PROD.codigoProducto,PROD.nombreProducto,PROD.marcaProducto,PROD.cantidadExistencia,PROV.NOMBRE,PROD.precioUnitario,PROD.descripcion,PROD.fabricante,PROD.estadoProducto,PROD.fecha,PROD.ESTADOSISTEMA from tbl_Productos PROD INNER JOIN TBL_PROVEEDORES PROV ON(PROV.PK_IDPROVEEDOR = PROD.FK_IDPROVEEDOR) where prod.estadosistema='" + v_EstadoSistema + "'";
            }
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
                    cliente.v_Cedula = dr.GetString(9);
                    cliente.v_Representante = dr.GetString(10);
                    cliente.v_Direccion = dr.GetString(11);
                    Lista.Add(cliente);
                }
            }
            conn.Close();
            return Lista;
        }
        public DataTable BuscarClienteNombre(string nombre)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select PK_IDCLIENTE,NOMBRE,TELEFONOOFICINA,TELEFONOMOVIL,CORREO,ESTADO,CEDULA,REPRESENTANTE,DIRECCION  FROM TBL_CLIENTES WHERE translate(UPPER(NOMBRE),'ÁÉÍÓÚ', 'AEIOU') LIKE translate(UPPER('%"+nombre+"%'),'ÁÉÍÓÚ', 'AEIOU')";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }
        public DataTable BuscarClienteEstado(string nombre)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select PK_IDCLIENTE,NOMBRE,TELEFONOOFICINA,TELEFONOMOVIL,CORREO,ESTADO,CEDULA,REPRESENTANTE,DIRECCION  from tbl_Clientes WHERE ESTADO LIKE '%" + nombre + "%'";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }
        public DataTable BuscarClienteEstadoyNombre(string nombre,string estado)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select PK_IDCLIENTE,NOMBRE,TELEFONOOFICINA,TELEFONOMOVIL,CORREO,ESTADO,CEDULA,REPRESENTANTE,DIRECCION  from tbl_Clientes WHERE translate(UPPER(NOMBRE),'ÁÉÍÓÚ', 'AEIOU') LIKE translate(UPPER('%"+nombre+"%'),'ÁÉÍÓÚ', 'AEIOU') AND ESTADO = '"+estado+"'";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }

        /*Este método recibe un parámetro tipo string con el cual buscara en la base de datos la existencia del proveedor mediante el nombre o la cédula jurídica.
         Además, en caso de encontrar proveedores estos serán retornados mediante una lista*/
        public List<EntidadProveedores> ValidarBusquedaProveedores(String v_busqueda)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT * FROM TBL_PROVEEDORES WHERE translate(UPPER(NOMBRE),'ÁÉÍÓÚ', 'AEIOU') LIKE translate(UPPER('%" + v_busqueda + "%'),'ÁÉÍÓÚ', 'AEIOU') OR CEDULAJURIDICA LIKE '%" + v_busqueda + "%'";
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
                    proveedor.v_EstadoSistema = dr.GetString(9);
                    Lista.Add(proveedor);
                }
            }
            conn.Close();
            return Lista;
        }

        /*Este método recibe un parámetro tipo string con el cual buscara en la base de datos la existencia del usuario mediante el nombre o número de cédula.
         Además, en caso de encontrar proveedores estos serán retornados mediante una lista*/
        public List<EntidadUsuarios> ValidarBusquedaUsuarios(String v_busqueda)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select USU.PK_IDUSUARIO,USU.CEDULAIDENTIFICACION,USU.NOMBREUSUARIO,USU.APELLIDOS,USU.TELEFONO,USU.TELEFONOOPCIONAL,USU.CORREO,USU.PUESTO,USU.FK_IDROL,ROL.NOMBRE,USU.USUARIOSISTEMA,USU.CONTRASENA,USU.FECHA,USU.ESTADOSISTEMA " +
                "from TBL_USUARIOS USU INNER JOIN TBL_ROLES ROL ON(ROL.PK_IDROL = USU.FK_IDROL)" +
                "WHERE translate(UPPER(NOMBREUSUARIO),'ÁÉÍÓÚ', 'AEIOU') LIKE translate(UPPER('%" + v_busqueda + "%'),'ÁÉÍÓÚ', 'AEIOU')" +
                "OR translate(UPPER(APELLIDOS),'ÁÉÍÓÚ', 'AEIOU') LIKE translate(UPPER('%" + v_busqueda + "%'),'ÁÉÍÓÚ', 'AEIOU')" +
                "OR translate(UPPER(CEDULAIDENTIFICACION),'ÁÉÍÓÚ', 'AEIOU') LIKE translate(UPPER('%" + v_busqueda + "%'),'ÁÉÍÓÚ', 'AEIOU')";
            OracleDataReader dr = comando.ExecuteReader();
            List<EntidadUsuarios> Lista = new List<EntidadUsuarios>();

            if (v_busqueda != "")
            {
                while (dr.Read())
                {
                    EntidadUsuarios usuario = new EntidadUsuarios();
                    usuario.v_IdUsuario = dr.GetInt64(0);
                    usuario.v_CedIdentificacion = Convert.ToInt64(dr.GetValue(1));
                    usuario.v_NombreUsuario = dr.GetString(2);
                    usuario.v_Apellidos = dr.GetString(3);
                    usuario.v_Telefono = Convert.ToInt64(dr.GetValue(4));
                    usuario.v_TelefonoOpcional = Convert.ToInt64(dr.GetValue(5));
                    usuario.v_Correo = dr.GetString(6);
                    usuario.v_Puesto = dr.GetString(7);
                    usuario.v_IdRol = Convert.ToInt64(dr.GetValue(8));
                    usuario.v_NombreRol = dr.GetString(9);
                    usuario.v_UsuarioSistema = dr.GetString(10);
                    usuario.v_Contrasena = dr.GetString(11);
                    usuario.v_Fecha = Convert.ToDateTime(dr.GetValue(12));
                    usuario.v_EstadoSistema = dr.GetString(13);
                    Lista.Add(usuario);
                }
            }
            conn.Close();
            return Lista;
        }

        /*Este método recibe un parámetro tipo string con el cual buscara en la base de datos la existencia de la cédula de identificación.
         Además, en caso de encontrar dicho número de cédula retornara un true, en caso contrario retornara un false*/
        public bool ValidarNumCedUsuarios(String v_NumCed)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT CEDULAIDENTIFICACION FROM TBL_USUARIOS WHERE CEDULAIDENTIFICACION = " + v_NumCed;
            OracleDataReader dr = comando.ExecuteReader();

            if (v_NumCed != "")
            {
                while (dr.Read())
                {
                    return true;
                }
            }
            conn.Close();
            return false;
        }

        /*Este método recibe un parámetro tipo string con el cual buscara en la base de datos la existencia del rol mediante el nombre 
         Además, en caso de encontrar proveedores estos serán retornados mediante una lista*/
        public List<EntidadRoles> ValidarBusquedaRoles(String v_busqueda)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT pk_idrol,nombre,estadosistema,mantenimiento_clientes,mantenimiento_proveedores,mantenimiento_productos,mantenimiento_usuarios,mantenimiento_roles,facturacion,bitacora from tbl_Roles WHERE translate(UPPER(NOMBRE),'ÁÉÍÓÚ', 'AEIOU') LIKE translate(UPPER('%" + v_busqueda + "%'),'ÁÉÍÓÚ', 'AEIOU')";
            OracleDataReader dr = comando.ExecuteReader();
            List<EntidadRoles> Lista = new List<EntidadRoles>();

            if (v_busqueda != "")
            {
                while (dr.Read())
                {
                    EntidadRoles rol = new EntidadRoles();
                    rol.v_IdRol = dr.GetInt64(0);
                    rol.v_Nombre = dr.GetString(1);
                    rol.v_Estado = dr.GetString(2);
                    rol.v_Mantenimiento_Clientes = dr.GetString(3);
                    rol.v_Mantenimiento_Proveedores = dr.GetString(4);
                    rol.v_Mantenimiento_Productos = dr.GetString(5);
                    rol.v_Mantenimiento_Usuarios = dr.GetString(6);
                    rol.v_Mantenimiento_Roles = dr.GetString(7);
                    rol.v_facturacion = dr.GetString(8);
                    rol.v_bitacora= dr.GetString(9);
                    Lista.Add(rol);
                }
            }
            conn.Close();
            return Lista;
        }

        public List<EntidadRoles> RolesExistentes()
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT PK_IDROL, NOMBRE FROM TBL_ROLES";
            OracleDataReader dr = comando.ExecuteReader();
            List<EntidadRoles> Lista = new List<EntidadRoles>();


            while (dr.Read())
            {
                EntidadRoles rol = new EntidadRoles();
                rol.v_IdRol = dr.GetInt64(0);
                rol.v_Nombre = dr.GetString(1);
                Lista.Add(rol);
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
            comando.CommandText = "select PROD.pk_idProducto,PROD.codigoProducto,PROD.nombreProducto,PROD.marcaProducto,PROD.cantidadExistencia,PROD.cantidadMinima,PROD.fk_idProveedor,PROV.NOMBRE,PROD.precioUnitario,PROD.descripcion,PROD.fabricante,PROD.estadoProducto,PROD.fecha,PROD.estadoSistema " +
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
                    producto.v_CantidadMinima = Convert.ToInt64(dr.GetValue(5));
                    producto.v_IdProveedor = Convert.ToInt64(dr.GetValue(6));
                    producto.v_NombreProveedor = dr.GetString(7);
                    producto.v_PrecioUnitario = Convert.ToInt64(dr.GetValue(8));
                    producto.v_Descripcion = dr.GetString(9);
                    producto.v_Fabricante = dr.GetString(10);
                    producto.v_EstadoProducto = dr.GetString(11);
                    producto.v_Fecha = Convert.ToDateTime(dr.GetValue(12));
                    producto.v_EstadoSistema = dr.GetString(13);
                    Lista.Add(producto);
                }
            }
            conn.Close();
            return Lista;
        }
        public DataTable CargarProveedores()
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select cedulaJuridica, nombre, correo, correoOpcional, telefono, telefonoOpcional, descripcion, fecha, estadoSistema from tbl_Proveedores";
            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
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

        /*Recibe como referencia una entidad usuario que contiene el id y la cédula del usuario con el fin de validar si la
        cédula está asociada a dicho id.
        Además, en caso de estar asociados retornara un true, en caso contrario retornara un false, esto es con el fin de que al momento de modificar si el
        usuario ingresa o deja la misma cédula que tenia,no le de error*/
        public Boolean ValidarModificacionUsuario(EntidadUsuarios clt)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT PK_IDUSUARIO,CEDULAIDENTIFICACION from TBL_USUARIOS where PK_IDUSUARIO = " + clt.v_IdUsuario + " AND CEDULAIDENTIFICACION = " + clt.v_CedIdentificacion;

            OracleDataReader dr = comando.ExecuteReader();

            if (clt.v_IdUsuario != 0 && clt.v_CedIdentificacion != 0)
            {
                while (dr.Read())
                {
                    return true;
                }
            }
            conn.Close();
            return false;
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

        

        /*Este método recibe un parámetro tipo string con el cual buscara en la base de datos la existencia del usuario mediante el nombre 
        Además, en caso de encontrar el usuario este será retornado mediante una lista*/
        public List<EntidadUsuarios> ValidarUsuario(String nombreUsuario)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select USUARIOSISTEMA,CONTRASENA,FK_IDROL from tbl_usuarios where  usuariosistema = '"+nombreUsuario+"'";
            OracleDataReader dr = comando.ExecuteReader();
            List<EntidadUsuarios> Lista = new List<EntidadUsuarios>();

                while (dr.Read())
                {
                    EntidadUsuarios usuario = new EntidadUsuarios();
                    usuario.v_UsuarioSistema = dr.GetString(0);
                    usuario.v_Contrasena = dr.GetString(1);
                    usuario.v_IdRol = Convert.ToInt64(dr.GetValue(2));
                    Lista.Add(usuario);
                }
            conn.Close();
            return Lista;
        }

        //--------------------------------PERFIL -----------------------------------------------------------------------
        /*Este método recibe un parámetro tipo string con el cual buscara en la base de datos la existencia del usuario mediante el nombre 
    Además, en caso de encontrar el usuario este será retornado mediante una lista*/
        public List<string> consultarUsuario(string nombreUsuario)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT usuariosistema,contrasena,mantenimiento_clientes,mantenimiento_proveedores,mantenimiento_productos,mantenimiento_usuarios,mantenimiento_roles,facturacion,bitacora,nombreusuario,apellidos,puesto,nombre,tbl_usuarios.estadosistema,TBL_ROLES.ESTADOSISTEMA FROM TBL_USUARIOS INNER JOIN TBL_ROLES ON TBL_USUARIOS.FK_IDROL = TBL_ROLES.PK_IDROL AND TBL_USUARIOS.USUARIOSISTEMA = '" + nombreUsuario+"'";
            OracleDataReader dr = comando.ExecuteReader();
            List<string> Lista = new List<string>();

            while (dr.Read())
            {
                Lista.Add(dr.GetString(0));
                Lista.Add(dr.GetString(1));
                Lista.Add(dr.GetString(2));
                Lista.Add(dr.GetString(3));
                Lista.Add(dr.GetString(4));
                Lista.Add(dr.GetString(5));
                Lista.Add(dr.GetString(6));
                Lista.Add(dr.GetString(7));
                Lista.Add(dr.GetString(8));
                Lista.Add(dr.GetString((9)));
                Lista.Add(dr.GetString(10));
                Lista.Add(dr.GetString(11));
                Lista.Add(dr.GetString(12));
                Lista.Add(dr.GetString(13));
                Lista.Add(dr.GetString(14));
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
        public DataTable Productos()
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT DESCRIPCION FROM TBL_PRODUCTOS WHERE ESTADOSISTEMA='ACTIVO' AND CANTIDADEXISTENCIA > 0";

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
            comando.CommandText = "SELECT TBL_FACTURAS.TIPOFACTURA,TBL_FACTURAS.PK_CODIGOFACTURA,TBL_FACTURAS.FECHA,TBL_USUARIOS.NOMBREUSUARIO,TBL_CLIENTES.NOMBRE,TBL_FACTURAS.TOTAL,MONEDA,IMPUESTO,TBL_FACTURAS.DESCUENTO,TBL_FACTURAS.ESTADOFACTURA FROM TBL_FACTURAS INNER JOIN TBL_USUARIOS ON TBL_FACTURAS.FK_IDUSUARIO=TBL_USUARIOS.PK_IDUSUARIO INNER JOIN TBL_CLIENTES ON TBL_FACTURAS.FK_IDCLIENTE=TBL_CLIENTES.PK_IDCLIENTE where trunc(TBL_FACTURAS.FECHA) BETWEEN '" + fecha1 + "' AND '" + fecha2 + "'";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            try
            {
                adaptador.Fill(tabla);
            }catch(Exception m)
            {
                Console.WriteLine(m);
            }
            conn.Close();
            return tabla;
        }

        public DataTable MostrarDetalleFactura(int codigoFactura)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT TBL_PRODUCTOS.SERVICIO,TBL_PRODUCTOS.CODIGOPRODUCTO,TBL_PRODUCTOS.DESCRIPCION,TBL_DETALLES.CANTIDAD_PRODUCTO,TBL_DETALLES.PRECIOPRODUCTO,TBL_DETALLES.DESCUENTO,TBL_DETALLES.IMPUESTO,TBL_DETALLES.TOTAL,TBL_FACTURAS.MONEDA FROM TBL_DETALLES INNER JOIN TBL_FACTURAS ON TBL_DETALLES.FK_IDFACTURA = TBL_FACTURAS.PK_CODIGOFACTURA AND TBL_FACTURAS.PK_CODIGOFACTURA = '" + codigoFactura + "' INNER JOIN TBL_PRODUCTOS ON TBL_DETALLES.FK_IDPRODUCTO = TBL_PRODUCTOS.PK_IDPRODUCTO";

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
            comando.CommandText = "SELECT TBL_FACTURAS.TIPOFACTURA,TBL_FACTURAS.PK_CODIGOFACTURA,TBL_FACTURAS.FECHA,TBL_FACTURAS.SUBTOTAL,TBL_FACTURAS.DESCUENTO,TBL_FACTURAS.SUBTOTALNETO,TBL_FACTURAS.IMPUESTO,TBL_FACTURAS.TOTAL,TBL_FACTURAS.TIPOPAGO,TBL_FACTURAS.DIASCREDITO,TBL_FACTURAS.ESTADOFACTURA,TBL_FACTURAS.FECHAPAGO,TBL_CLIENTES.PK_IDCLIENTE,TBL_CLIENTES.CEDULA,TBL_CLIENTES.NOMBRE,TBL_CLIENTES.REPRESENTANTE,TBL_CLIENTES.DIRECCION,TBL_CLIENTES.TELEFONOMOVIL,TBL_CLIENTES.TELEFONOOFICINA,TBL_CLIENTES.CORREO,TBL_FACTURAS.FECHACANCELACION,TBL_FACTURAS.TIPOCAMBIO,TBL_FACTURAS.MONEDA FROM TBL_FACTURAS INNER JOIN TBL_USUARIOS ON TBL_FACTURAS.FK_IDUSUARIO=TBL_USUARIOS.PK_IDUSUARIO INNER JOIN TBL_CLIENTES ON TBL_FACTURAS.FK_IDCLIENTE=TBL_CLIENTES.PK_IDCLIENTE WHERE TBL_FACTURAS.PK_CODIGOFACTURA = '" + codigoFactura + "'";
            OracleDataReader dr = comando.ExecuteReader();
            var Lista = new List<string>();


            while (dr.Read())
            {
                Lista.Add(dr.GetString(0));
                Lista.Add(dr.GetInt64(1).ToString());
                Lista.Add(dr.GetDateTime(2).ToString());
                Lista.Add(dr.GetString(3));
                Lista.Add(dr.GetString(4));
                Lista.Add(dr.GetString(5));
                Lista.Add(dr.GetString(6));
                Lista.Add(dr.GetString(7));
                Lista.Add(dr.GetString(8));
                Lista.Add(dr.GetString(9));
                Lista.Add(dr.GetString(10));
                Lista.Add(dr.GetDateTime(11).ToShortDateString());
                Lista.Add(dr.GetInt64(12).ToString());
                Lista.Add(dr.GetString(13));
                Lista.Add(dr.GetString(14));
                Lista.Add(dr.GetString(15));
                Lista.Add(dr.GetString(16));
                Lista.Add(dr.GetInt64(17).ToString());
                Lista.Add(dr.GetInt64(18).ToString());
                Lista.Add(dr.GetString(19));
                Lista.Add(dr.GetDateTime(20).ToShortDateString());
                Lista.Add(dr.GetString(21));
                Lista.Add(dr.GetString(22));
            }
            conn.Close();
            return Lista;
        }
        public ObservableCollection<string> ListaProductos()
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT DESCRIPCION FROM TBL_PRODUCTOS";
            OracleDataReader dr = comando.ExecuteReader();
            var Lista = new ObservableCollection<string>();


            while (dr.Read())
            {
                Lista.Add(dr.GetString(0));
            }
            conn.Close();
            return Lista;
        }
        public List<string> DetalleProducto(string descripcion)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT CODIGOPRODUCTO,PRECIOUNITARIO,CANTIDADEXISTENCIA FROM TBL_PRODUCTOS WHERE DESCRIPCION = '" + descripcion + "'";
            OracleDataReader dr = comando.ExecuteReader();
            var Lista = new List<string>();


            while (dr.Read())
            {
                Lista.Add(dr.GetString(0));
                Lista.Add(dr.GetInt32(1).ToString());
                Lista.Add(dr.GetInt32(2).ToString());

            }
            conn.Close();
            return Lista;
        }
        
        public DataTable BuscarFacturaEstadoyCliente(string v_Nombre,string estado)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT TBL_FACTURAS.TIPOFACTURA,TBL_FACTURAS.PK_CODIGOFACTURA,TBL_FACTURAS.FECHA,TBL_USUARIOS.NOMBREUSUARIO,TBL_CLIENTES.NOMBRE,TBL_FACTURAS.TOTAL,MONEDA,IMPUESTO,TBL_FACTURAS.DESCUENTO,TBL_FACTURAS.ESTADOFACTURA FROM TBL_FACTURAS INNER JOIN TBL_USUARIOS ON TBL_FACTURAS.FK_IDUSUARIO = TBL_USUARIOS.PK_IDUSUARIO INNER JOIN TBL_CLIENTES ON TBL_FACTURAS.FK_IDCLIENTE = TBL_CLIENTES.PK_IDCLIENTE WHERE translate(UPPER(NOMBRE),'ÁÉÍÓÚ', 'AEIOU') LIKE translate(UPPER('%" + v_Nombre + "%'),'ÁÉÍÓÚ', 'AEIOU') AND TBL_FACTURAS.ESTADOFACTURA = '" + estado+"'";
            OracleDataReader dr = comando.ExecuteReader();

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }
        public DataTable BuscarFacturaEstado(String v_Nombre)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT TBL_FACTURAS.TIPOFACTURA,TBL_FACTURAS.PK_CODIGOFACTURA,TBL_FACTURAS.FECHA,TBL_USUARIOS.NOMBREUSUARIO,TBL_CLIENTES.NOMBRE,TBL_FACTURAS.TOTAL,MONEDA,IMPUESTO,TBL_FACTURAS.DESCUENTO,TBL_FACTURAS.ESTADOFACTURA FROM TBL_FACTURAS INNER JOIN TBL_USUARIOS ON TBL_FACTURAS.FK_IDUSUARIO = TBL_USUARIOS.PK_IDUSUARIO INNER JOIN TBL_CLIENTES ON TBL_FACTURAS.FK_IDCLIENTE = TBL_CLIENTES.PK_IDCLIENTE WHERE ESTADOFACTURA LIKE '%" + v_Nombre + "%'";
            OracleDataReader dr = comando.ExecuteReader();

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }
        public Int64 MaximaFactura()
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select max(Pk_codigofactura) from TBL_FACTURAS";
            OracleDataReader dr = comando.ExecuteReader();

            Int64 valor = 0;
            while (dr.Read())
            {
                valor = Convert.ToInt64(dr.GetValue(0));
            }
            conn.Close();
            return valor;
        }
        public string tipoFactura(string codigo)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select tbl_facturas.tipofactura from tbl_facturas where pk_codigofactura = '"+codigo+"'";
            OracleDataReader dr = comando.ExecuteReader();

            string valor = "";
            while (dr.Read())
            {
                valor = dr.GetString(0);
            }
            conn.Close();
            return valor;
        }
        public string Descripcion_servicio(string codigo)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select tbl_detalles.DESCRIPCION_SERVICIO from TBL_DETALLES where FK_IDFACTURA = '" + codigo + "'";
            OracleDataReader dr = comando.ExecuteReader();

            string valor = "";
            while (dr.Read())
            {
                valor = dr.GetString(0);
            }
            conn.Close();
            return valor;
        }
        public Int64 MaximoDetalle()
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select max(PK_IDDETALLE) from TBL_DETALLES";
            OracleDataReader dr = comando.ExecuteReader();

            Int64 valor = 0;
            while (dr.Read())
            {
                valor = Convert.ToInt64(dr.GetValue(0));
            }
            conn.Close();
            return valor;
        }
        public Int64 id_Usuario(string nombre)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT PK_IDUSUARIO,USUARIOSISTEMA from tbl_usuarios where usuariosistema = '"+nombre+"'";
            OracleDataReader dr = comando.ExecuteReader();

            Int64 valor = 0;
            while (dr.Read())
            {
                valor = Convert.ToInt64(dr.GetValue(0));
            }
            conn.Close();
            return valor;
        }
        public EntidadClientes IdCliente(string nombre)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT PK_IDCLIENTE, NOMBRE, TELEFONOOFICINA, TELEFONOMOVIL, CORREO, CEDULA, REPRESENTANTE, DIRECCION FROM TBL_CLIENTES WHERE NOMBRE = '"+nombre+"'";
            OracleDataReader dr = comando.ExecuteReader();

            EntidadClientes cliente = new EntidadClientes();
            
            while (dr.Read())
            {
                cliente.v_Codigo = Convert.ToInt32(dr.GetValue(0));
                cliente.v_NombreCompleto = Convert.ToString(dr.GetValue(1));
                cliente.v_Teleoficina = Convert.ToInt32(dr.GetValue(2));
                cliente.v_Telemovil = Convert.ToInt32(dr.GetValue(3));
                cliente.v_Correo = Convert.ToString(dr.GetValue(4));
                cliente.v_Cedula = Convert.ToString(dr.GetValue(5));
                cliente.v_Representante = Convert.ToString(dr.GetValue(6));
                cliente.v_Direccion = Convert.ToString(dr.GetValue(7));
            }
            conn.Close();
            return cliente;
        }

        /*Recibe como referencia una entidad proveedor que contiene el id y la cédula jurídica del proveedor con el fin de validar si la
       cédula jurídica está asociada a dicho id.
       Además, en caso de estar asociados retornara un true, en caso contrario retornara un false, esto es con el fin de que al momento de modificar si el
       usuario ingresa o deja la misma cédula que tenia,no le de error*/
        public Boolean ValidarModificacionProducto(EntidadProductos clt)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT PK_IDPRODUCTO,CODIGOPRODUCTO from TBL_PRODUCTOS where CODIGOPRODUCTO = '" + clt.v_CodigoProducto + "' AND PK_IDPRODUCTO = "+clt.v_IdProducto;

            OracleDataReader dr = comando.ExecuteReader();

            if (clt.v_IdProducto != 0 && clt.v_CodigoProducto != "")
            {
                while (dr.Read())
                {
                    return true;
                }
            }
            conn.Close();
            return false;
        }
        public Int64 VerificarNombre(string nombre)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT NOMBRE FROM TBL_CLIENTES WHERE translate(UPPER(NOMBRE), 'ÁÉÍÓÚ', 'AEIOU') LIKE translate(UPPER('%" + nombre + "%'),'ÁÉÍÓÚ', 'AEIOU')";
            OracleDataReader dr = comando.ExecuteReader();

            string valor = "";
            while (dr.Read())
            {
                valor = dr.GetString(0);
            }
            conn.Close();
            if (valor != "")
                return 1;
            else
                return 0;
        }
        public void DescuentoInventario (string cantidad, string nombre)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "update TBL_PRODUCTOS SET CANTIDADEXISTENCIA = '"+cantidad+"' WHERE DESCRIPCION = '"+nombre+"'";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }
        public void CambiarestadoFactura(string estado, string codigo)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "update TBL_FACTURAS SET ESTADOFACTURA = '" + estado + "', FECHACANCELACION = SYSDATE WHERE PK_CODIGOFACTURA = '" + codigo + "'";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }
        public void Verificarestadofactura()
        {
            //OracleConnection conn = DataBase.Conexion();
            //conn.Open();
            //OracleCommand comando = new OracleCommand();
            //comando.Connection = conn;
            //comando.CommandText = "select PK_CODIGOFACTURA,FECHAPAGO from TBL_FACTURAS WHERE ESTADOFACTURA = 'Pendiente'";
            //OracleDataReader dr = comando.ExecuteReader();
            //System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            //DateTime fechaPago = new DateTime();
            //DateTime diaActual = DateTime.Parse(DateTime.Now.ToShortDateString());
            //string codigo = "";
            //while (dr.Read())
            //{
            //    codigo=dr.GetInt64(0).ToString();
            //    fechaPago = DateTime.Parse(dr.GetDateTime(1).ToShortDateString());
            //    if(fechaPago < diaActual)
            //    {
            //        EstadoVencidaFactura(codigo);
            //    }
            //}
            //conn.Close();
        }
        public void EstadoVencidaFactura(string codigo)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "update TBL_FACTURAS SET ESTADOFACTURA = 'Vencida' WHERE PK_CODIGOFACTURA = '" + codigo + "'";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }
        public List<string> DetalleFacturaServicios(int codigoFactura)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT DESCRIPCION_SERVICIO,CANTIDAD_PRODUCTO,PRECIOPRODUCTO FROM TBL_DETALLES WHERE FK_IDFACTURA = '"+codigoFactura+"'";
            OracleDataReader dr = comando.ExecuteReader();
            var Lista = new List<string>();


            while (dr.Read())
            {
                Lista.Add(dr.GetString(0));
                Lista.Add(dr.GetInt64(1).ToString());
                Lista.Add(dr.GetString(2));
            }
            conn.Close();
            return Lista;
        }


        public bool ValidarCodProductos(String v_Codigo)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT CODIGOPRODUCTO FROM TBL_PRODUCTOS WHERE codigoProducto = '" + v_Codigo + "'";
            OracleDataReader dr = comando.ExecuteReader();

            if (v_Codigo != "")
            {
                while (dr.Read())
                {
                    return true;
                }
            }
            conn.Close();
            return false;
        }


        public Int64 IdProducto(string nombre)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT PK_IDPRODUCTO,DESCRIPCION from TBL_PRODUCTOS where DESCRIPCION = '" + nombre + "'";
            OracleDataReader dr = comando.ExecuteReader();

            Int64 valor = 0;
            while (dr.Read())
            {
                valor = Convert.ToInt64(dr.GetValue(0));
            }
            conn.Close();
            return valor;
        }

        public DataTable BuscarFactura(String v_Nombre)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT TBL_FACTURAS.TIPOFACTURA,TBL_FACTURAS.PK_CODIGOFACTURA,TBL_FACTURAS.FECHA,TBL_USUARIOS.NOMBREUSUARIO,TBL_CLIENTES.NOMBRE,TBL_FACTURAS.TOTAL,MONEDA,IMPUESTO,TBL_FACTURAS.DESCUENTO,TBL_FACTURAS.ESTADOFACTURA FROM TBL_FACTURAS INNER JOIN TBL_USUARIOS ON TBL_FACTURAS.FK_IDUSUARIO = TBL_USUARIOS.PK_IDUSUARIO INNER JOIN TBL_CLIENTES ON TBL_FACTURAS.FK_IDCLIENTE = TBL_CLIENTES.PK_IDCLIENTE WHERE translate(UPPER(NOMBRE),'ÁÉÍÓÚ', 'AEIOU') LIKE translate(UPPER('%" + v_Nombre + "%'),'ÁÉÍÓÚ', 'AEIOU')";
            OracleDataReader dr = comando.ExecuteReader();

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }

        /////////////////////////////////////////////Bitacora////////////////////////////////////////////////////////
        ///
        public DataTable MostrarBitacoraPorFecha(String fecha1, String fecha2)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT FECHA,ACCION,USUARIO_RESPONSABLE,VENTANA_MANTENIMIENTO,DESCRIPCION  from TBL_BITACORA where trunc(TBL_BITACORA.FECHA) BETWEEN '" + fecha1 + "' AND '" + fecha2 + "'";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            try
            {
                adaptador.Fill(tabla);
            }
            catch (Exception m)
            {
                Console.WriteLine(m);
            }
            conn.Close();
            return tabla;
        }
        public DataTable BitacoraMantenimieto(string mantenimiento)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT FECHA,ACCION,USUARIO_RESPONSABLE,VENTANA_MANTENIMIENTO,DESCRIPCION  from TBL_BITACORA where VENTANA_MANTENIMIENTO = '"+mantenimiento+"'";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            try
            {
                adaptador.Fill(tabla);
            }
            catch (Exception m)
            {
                Console.WriteLine(m);
            }
            conn.Close();
            return tabla;
        }
        public DataTable BitacoraAccion(string accion)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT FECHA,ACCION,USUARIO_RESPONSABLE,VENTANA_MANTENIMIENTO,DESCRIPCION  from TBL_BITACORA where ACCION = '"+accion+"'";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            try
            {
                adaptador.Fill(tabla);
            }
            catch (Exception m)
            {
                Console.WriteLine(m);
            }
            conn.Close();
            return tabla;
        }
        public DataTable BitacoraMantenimietoyAccion(string mantenimiento,string accion)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT FECHA,ACCION,USUARIO_RESPONSABLE,VENTANA_MANTENIMIENTO,DESCRIPCION  from TBL_BITACORA where VENTANA_MANTENIMIENTO = '"+mantenimiento+"' AND ACCION = '"+accion+"'";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            try
            {
                adaptador.Fill(tabla);
            }
            catch (Exception m)
            {
                Console.WriteLine(m);
            }
            conn.Close();
            return tabla;
        }


    }
}