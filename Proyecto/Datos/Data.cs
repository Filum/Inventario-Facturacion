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

        public int AgregarProveedores(EntidadProveedores clt)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("ADD_PROVEEDORES", conn as OracleConnection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new OracleParameter("CEDJUR", clt.v_cedulaJuridica));
            comando.Parameters.Add(new OracleParameter("NOMB", clt.v_nombre));
            comando.Parameters.Add(new OracleParameter("EMAIL", clt.v_correo));
            comando.Parameters.Add(new OracleParameter("DESCRI", clt.v_descripcion));
            comando.Parameters.Add(new OracleParameter("TELEFO", clt.v_telefono));

            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }

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

        public int ModificarProveedores(EntidadProveedores clt)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("act_Proveedores", conn as OracleConnection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new OracleParameter("IDPROVEEDOR", clt.v_idProveedor));
            comando.Parameters.Add(new OracleParameter("CEDJUR", clt.v_cedulaJuridica));
            comando.Parameters.Add(new OracleParameter("NOMB", clt.v_nombre));
            comando.Parameters.Add(new OracleParameter("EMAIL", clt.v_correo));
            comando.Parameters.Add(new OracleParameter("DESCRI", clt.v_descripcion));
            comando.Parameters.Add(new OracleParameter("TELEFO", clt.v_telefono));
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }

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

        public DataTable MostarListaProveedores(String v_Fecha1, String v_Fecha2)
        {
            
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select * from tbl_Proveedores where trunc(fecha) BETWEEN '" +v_Fecha1+ "' AND '" + v_Fecha2 + "'";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }

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

        public List<EntidadProveedores> validar_busqueda_proveedores(String v_busqueda)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT * FROM TBL_PROVEEDORES WHERE NOMBRE LIKE '%" + v_busqueda + "%' OR CEDULAJURIDICA LIKE '%" + v_busqueda + "%'";
            OracleDataReader dr = comando.ExecuteReader();
            List<EntidadProveedores> Lista = new List<EntidadProveedores>();

            if (v_busqueda != "")
            {
                while (dr.Read())
                {
                    EntidadProveedores proveedor = new EntidadProveedores();
                    proveedor.v_idProveedor = dr.GetInt64(0);
                    proveedor.v_cedulaJuridica = Convert.ToInt64(dr.GetValue(2));
                    proveedor.v_nombre = dr.GetString(3);
                    proveedor.v_telefono = Convert.ToInt64(dr.GetValue(6));
                    proveedor.v_correo = dr.GetString(4);
                    proveedor.v_descripcion = dr.GetString(5);
                    proveedor.v_fecha = Convert.ToDateTime(dr.GetValue(1));
                    Lista.Add(proveedor);
                }
            }
            conn.Close();
            return Lista;
        }
    }
}
