using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class AccesoDatosProductos
    {
        Conexion conexion;
        public AccesoDatosProductos()
        {
            conexion = new Conexion("localhost", "root", "Tienda1", 3306);
        }

        public List<EntidadesProducto> ObtenerDatos(string valor)
        {
            var ListaDatosProducto = new List<EntidadesProducto>();
            
            var dataTable = new DataTable();

            var consulta = string.Format("select * from producto where nombre like '%{0}%'", valor);
            dataTable = conexion.ObtenerDatos(consulta);

            foreach (DataRow renglon in dataTable.Rows)
            {
                var DatosProducto = new EntidadesProducto
                {
                    Id = Convert.ToInt32(renglon["id"]),
                    Nombre = renglon["nombre"].ToString(),
                    Descripcion = renglon["descripcion"].ToString(),
                    Precio = double.Parse(renglon["precio"].ToString())
                };
                ListaDatosProducto.Add(DatosProducto);
            }
            return ListaDatosProducto;
        }


        public void GuardarProducto(EntidadesProducto nuevosProductos)
        {
            string consulta = string.Format("insert into producto values(null, '{0}', '{1}', {2})",
                nuevosProductos.Nombre, nuevosProductos.Descripcion, nuevosProductos.Precio);
            conexion.EjecutarConsulta(consulta);
        }

        public void ActualizarProducto(EntidadesProducto nuevosDatos)
        {
            string consulta = string.Format("update producto set nombre = '{0}', descripcion = '{1}', precio = {2}", 
                nuevosDatos.Nombre, nuevosDatos.Descripcion, nuevosDatos.Precio);
            conexion.EjecutarConsulta(consulta);
        }

        public void EliminarProducto(int IdUsuario)
        {
            string consulta = string.Format("delete from producto where id = {0}", IdUsuario);
            conexion.EjecutarConsulta(consulta);
        }
    }
}
