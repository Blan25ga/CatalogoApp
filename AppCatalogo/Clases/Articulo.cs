using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCatalogo.Clases
{
    internal class Articulo
    {
        // Identificador único en la DB
        public int Id { get; set; }

        // Código interno del artículo (ej: S01, A23)
        public string Codigo { get; set; }

        // Nombre comercial del artículo (ej: Galaxy S10)
        public string Nombre { get; set; }

        // Descripción detallada del artículo
        public string Descripcion { get; set; }

        // Relación con la tabla MARCAS
        public Marca Marca { get; set; }

        // Relación con la tabla CATEGORIAS
        public Categoria Categoria { get; set; }

        // URL de la imagen del artículo
        public string ImagenUrl { get; set; }

        // Precio del artículo
        public decimal Precio { get; set; }
        
    }
}
