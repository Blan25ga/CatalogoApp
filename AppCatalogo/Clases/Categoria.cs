using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCatalogo.Clases
{
    public class Categoria
    {
        public int Id { get; set; }

        // Nombre de la categoría (ej: Smartphones, Laptops)
        public string Descripcion { get; set; } = string.Empty; // Inicialización para evitar valores nulos
    }
}
