using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Entidades;

namespace Manejador
{
    public class ManejadorProductos
    {
        private AccesoDatosProductos _AccesoDatosProductos;

        public ManejadorProductos()
        {
            _AccesoDatosProductos = new AccesoDatosProductos();
        }

        public List<EntidadesProducto> ObtenerProducto(string valor)
        {
            return _AccesoDatosProductos.ObtenerDatos(valor);
        }

        public void EliminarProducto(int IdProducto)
        {
            _AccesoDatosProductos.EliminarProducto(IdProducto);
        }

        public void GuardarProducto(EntidadesProducto datosProducto)
        {
            _AccesoDatosProductos.GuardarProducto(datosProducto);
        }

        public void ActualizarProducto(EntidadesProducto datosProducto)
        {
            _AccesoDatosProductos.ActualizarProducto(datosProducto);
        }

        public Tuple<bool, string> ValidarDatosProducto(EntidadesProducto nuevosDatos)
        {
            //Se declara una variable mensaje como una cadena vacía y una variable valida como true
            //para almacenar el mensaje de error y el estado de validación, respectivamente.
            string mensaje = "";
            bool esValido = true;

            if (nuevosDatos.Nombre == "")
            {
                mensaje = "El campo Nombre es requerido";
                esValido = false;
            }
            if (nuevosDatos.Descripcion == "")
            {
                mensaje = "El campo Descripcion es requerido";
                esValido = false;
            }
            if (nuevosDatos.Precio == 0)
            {
                mensaje = "El campo Precio es requerido";
                esValido = false;
            }

            var resultado = new Tuple<bool, string>(esValido, mensaje);
            return resultado;
        }
    }
}
