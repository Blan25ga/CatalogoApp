using AppCatalogo.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCatalogo.Servicios
{
    internal class ArticuloServicio
    {
        public List<Articulo> Listar()
        {
            // 1. Creo la lista donde voy a guardar los artículos
            List<Articulo> lista = new List<Articulo>();

            // 2. Defino la conexión a la base de datos
            SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS; database=CATALOGO_DB; integrated security=true");


            // 3. Armo el comando SQL que trae los datos
            SqlCommand comando = new SqlCommand(
                "SELECT A.Id, Codigo, Nombre, A.Descripcion, ImagenUrl, Precio, " +
                "M.Descripcion Marca, C.Descripcion Categoria, A.IdMarca, A.IdCategoria " +
                "FROM ARTICULOS A, MARCAS M, CATEGORIAS C " +
                "WHERE A.IdMarca = M.Id AND A.IdCategoria = C.Id", conexion);

            // 4. Abro la conexión
            conexion.Open();

            // 5. Ejecuto el comando y obtengo un lector de datos
            SqlDataReader lector = comando.ExecuteReader();

            // 6. Recorro cada fila que devuelve la consulta
            while (lector.Read())
            {
                Articulo aux = new Articulo();

                // Cargo las propiedades básicas
                aux.Id = (int)lector["Id"];
                aux.Codigo = (string)lector["Codigo"];
                aux.Nombre = (string)lector["Nombre"];
                aux.Descripcion = (string)lector["Descripcion"];
                aux.ImagenUrl = (string)lector["ImagenUrl"];
                aux.Precio = (decimal)lector["Precio"];

                // Cargo la marca
                aux.Marca = new Marca();
                aux.Marca.Id = (int)lector["IdMarca"];
                aux.Marca.Descripcion = (string)lector["Marca"];

                // Cargo la categoría
                aux.Categoria = new Categoria();
                aux.Categoria.Id = (int)lector["IdCategoria"];
                aux.Categoria.Descripcion = (string)lector["Categoria"];

                // Agrego el artículo a la lista
                lista.Add(aux);
            }

            // 7. Cierro la conexión
            conexion.Close();

            // 8. Devuelvo la lista completa
            return lista;
        }
    }
}
