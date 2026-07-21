using AppCatalogo.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCatalogo.Servicios
{
    internal class CategoriaServicio
    {
        public List<Categoria> Listar()
        {
            // Crear una lista de categorías y llenarla con los datos obtenidos de la base de datos
            List<Categoria> lista = new List<Categoria>();
            using (SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS; database=CATALOGO_DB; integrated security=true"))
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("SELECT Id, Descripcion FROM CATEGORIAS", conexion);
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())// Lee cada fila del resultado de la consulta y crear un objeto Categoria con los datos obtenidos
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)lector["Id"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    lista.Add(aux);
                }
            }
            return lista;
        }
    }
}
