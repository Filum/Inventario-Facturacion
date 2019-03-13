using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EntidadRoles
    {

        public EntidadRoles()
        {

        }

        public Int64 v_IdRol { set; get; }
        public DateTime v_Fecha { set; get; }
        public string v_Nombre { set; get; }
        public string v_Mantenimiento_Clientes { set; get; }
        public string v_Mantenimiento_Proveedores { set; get; }
        public string v_Mantenimiento_Productos { set; get; }
        public string v_Mantenimiento_Usuarios { set; get; }
        public string v_Mantenimiento_Roles { set; get; }
        public string v_facturacion { set; get; }
        public string v_bitacora { set; get; }
        public string v_Estado { set; get; }

        public EntidadRoles(Int64 v_IdRol, DateTime v_Fecha, string v_Nombre,string v_Estado, string v_Mant_clientes, string v_Mant_proveedores, string v_Mant_productos, string v_Mant_usuarios, string v_Mant_roles, string v_factu, string v_bitacora)
        {
            this.v_IdRol = v_IdRol;
            this.v_Fecha = v_Fecha;
            this.v_Nombre = v_Nombre;
            this.v_Mantenimiento_Clientes = v_Mant_clientes;
            this.v_Mantenimiento_Proveedores = v_Mant_proveedores;
            this.v_Mantenimiento_Productos = v_Mantenimiento_Productos;
            this.v_Mantenimiento_Usuarios = v_Mant_usuarios;
            this.v_Mantenimiento_Roles = v_Mant_roles;
            this.v_facturacion = v_factu;
            this.v_bitacora = v_bitacora;
            this.v_Estado = v_Estado;
        }

    }
}
