using AppCatalogo.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCatalogo.Servicios
{
    internal class MarcaServicio
    {
        public List<Marca> Listar()
        {
            // Crear una lista de marcas y llenarla con los datos obtenidos de la base de datos
            List<Marca> lista = new List<Marca>();
            using (SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS; database=CATALOGO_DB; integrated security=true"))
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("SELECT Id, Descripcion FROM MARCAS", conexion);
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)lector["Id"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    lista.Add(aux);
                }
            }
            return lista;
        }
    }
}
