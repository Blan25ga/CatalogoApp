using AppCatalogo.Clases;
using AppCatalogo.Servicios;// Importo el espacio de nombres donde está la clase ArticuloServicio
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppCatalogo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArticuloServicio servicio = new ArticuloServicio();
            dgvArticulos.DataSource = servicio.Listar();

            // Oculto la columna de URL porque no es necesaria en la grilla
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                try
                {
                    if (!string.IsNullOrEmpty(seleccionado.ImagenUrl))
                    {
                        pbxArticulo.Load(seleccionado.ImagenUrl);
                    }
                    else
                    {
                        pbxArticulo.Load("https://via.placeholder.com/150");
                    }
                }
                catch (Exception)
                {
                    // Si falla la carga, muestro una imagen por defecto
                    pbxArticulo.Load("https://images.icon-icons.com/3001/PNG/512/default_filetype_file_empty_document_icon_187718.png");
                }
            }
        }

    }
}
