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
            comando.Parameters.Add(new OracleParameter("INACT", clt.v_Inactividad));
            comando.Parameters.Add(new OracleParameter("OBVS", clt.v_Observaciones));
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }

        public int ModificarClientes(EntidadClientes clt)
        {
            OracleConnection conn = new OracleConnection();
            conn.Open();
            OracleCommand comando = new OracleCommand("act_CLIENTES", conn as OracleConnection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new OracleParameter("NOM", clt.v_NombreCompleto));
            comando.Parameters.Add(new OracleParameter("TELOFI", clt.v_Teleoficina));
            comando.Parameters.Add(new OracleParameter("TELMOVIL", clt.v_Telemovil));
            comando.Parameters.Add(new OracleParameter("ID_CLIENTE", 0));
            comando.Parameters.Add(new OracleParameter("CORRE", clt.v_Correo));
            comando.Parameters.Add(new OracleParameter("CORREOPC", clt.v_CorreoOpc));
            comando.Parameters.Add(new OracleParameter("OBVS", clt.v_Observaciones));
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }

        public int EliminarClientes(EntidadClientes clt)
        {
            OracleConnection conn = new OracleConnection();
            conn.Open();
            OracleCommand comando = new OracleCommand("del_CLIENTES", conn as OracleConnection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new OracleParameter("ID_CLIENTE", clt.v_Codigo));
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
            comando.Parameters.Add(new OracleParameter("DESCRI", clt.v_descripccion));
            comando.Parameters.Add(new OracleParameter("TELEFO", clt.v_telefono));
           
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }

        public int ListarProveedores(EntidadProveedores clt)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("", conn as OracleConnection);
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }

        
    }
}
