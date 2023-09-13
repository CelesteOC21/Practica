using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;

namespace AccesoDatos
{
    public class Conexion
    {
        private MySqlConnection _connection;

        public Conexion(string servidor, string usuario, string database, uint puerto)
        {
            MySqlConnectionStringBuilder CadenaConexion = new MySqlConnectionStringBuilder();
            CadenaConexion.Server = servidor;
            CadenaConexion.UserID = usuario;
            CadenaConexion.Database = database;
            CadenaConexion.Port = puerto;

            _connection = new MySqlConnection(CadenaConexion.ToString());
        }

        public void EjecutarConsulta(string consulta)
        {
            try
            {
                _connection.Open();
                using (MySqlCommand comando = new MySqlCommand(consulta, _connection))
                {
                    comando.ExecuteNonQuery();
                    Console.WriteLine("Consulta ejecutada correctamente");
                }
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al ejecutar la consulta: ", ex.Message);
            }
        }

        //método utilizado para obtener datos de una base de datos y devolverlos en forma de un objeto DataTable
        public DataTable ObtenerDatos(string consulta)
        {
            //Se crea un nuevo objeto DataTable llamado dataTable, que se utilizará para almacenar los datos recuperados de la bd
            DataTable dataTable = new DataTable();
            try
            {
                _connection.Open();
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(consulta, _connection))
                {
                    //Se llama al método Fill del adaptador, que ejecuta la consulta y llena el dataTable con los resultados obtenidos
                    //de la base de datos.
                    adaptador.Fill(dataTable);
                    Console.WriteLine("Consulta ejecutada correctamente");
                }
                _connection.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error al ejecutar la consulta: ", ex.Message);
            }
            return dataTable;
        }
    }
}

