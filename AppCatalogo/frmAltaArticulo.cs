using AppCatalogo.Clases;
using AppCatalogo.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace AppCatalogo
{
    

    public partial class frmAltaArticulo : System.Windows.Forms.Form
    {
        public Articulo articulo = null;
        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validar campos obligatorios de texto
                if (string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    MessageBox.Show("Debe ingresar el Código de artículo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Debe ingresar el Nombre.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("Debe ingresar la Descripción.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtDescripcion.Text.Trim().Length < 10)
                {
                    MessageBox.Show("La descripción debe tener como mínimo 10 caracteres.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Validar Precio (Numérico y no negativo)
                if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
                {
                    MessageBox.Show("El precio debe ser un número válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (precio <= 0)
                {
                    MessageBox.Show("El precio debe ser un valor mayor a cero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Validar ComboBoxes
                if (cboMarca.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar una Marca.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboCategoria.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar una Categoría.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 4. Validar formato de URL de Imagen (Opcional o con fallback)
                string urlImagen = txtImagenUrl.Text.Trim();
                bool esUrlValida = Uri.TryCreate(urlImagen, UriKind.Absolute, out Uri uriResult)
                                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if (!esUrlValida && !string.IsNullOrWhiteSpace(urlImagen))
                {
                    MessageBox.Show("La URL de la imagen no es válida. Se asignará una imagen por defecto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    urlImagen = "https://via.placeholder.com/150";
                }

                // 5. Instanciar y mapear objeto
                ArticuloServicio servicio = new ArticuloServicio();

                if (articulo == null)
                    articulo = new Articulo(); // Es un Alta

                articulo.Codigo = txtCodigo.Text.Trim();
                articulo.Nombre = txtNombre.Text.Trim();
                articulo.Descripcion = txtDescripcion.Text.Trim();
                articulo.Precio = precio;
                articulo.ImagenUrl = urlImagen;
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;

                // 6. Guardar en Base de Datos
                if (articulo.Id != 0)
                {
                    servicio.Modificar(articulo);
                    MessageBox.Show("Artículo modificado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    servicio.Agregar(articulo);
                    MessageBox.Show("Artículo agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el artículo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Mensaje de que si esta seguro que quiere finalizar la carga de articulos
            DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar la carga del artículo?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Close();
            }
            
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            try
            {
                MarcaServicio marcaServicio = new MarcaServicio();
                cboMarca.DataSource = marcaServicio.Listar();
                cboMarca.DisplayMember = "Descripcion";
                cboMarca.ValueMember = "Id";

                CategoriaServicio categoriaServicio = new CategoriaServicio();
                cboCategoria.DataSource = categoriaServicio.Listar();
                cboCategoria.DisplayMember = "Descripcion";
                cboCategoria.ValueMember = "Id";

                // Si estamos modificando, rellenar los campos
                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtPrecio.Text = articulo.Precio.ToString();
                    txtImagenUrl.Text = articulo.ImagenUrl;
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar combos: " + ex.Message);
            }
        }

        internal frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Artículo";
        }

        // Evento para validar la imagen al salir del campo
        private async void txtImagenUrl_Leave(object sender, EventArgs e)
        {
            string url = txtImagenUrl.Text.Trim(); // Obtenemos la URL ingresada por el usuario

            // Si el campo está vacío, asignamos la imagen por defecto sin mostrar alertas
            if (string.IsNullOrWhiteSpace(url))
            {
                cargarImagenPorDefecto();
                return;
            }

            // Validamos que sea una URL con formato válido (http o https)
            bool esUrlValida = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (esUrlValida)
            {
                try
                {
                    // Usamos Load síncrono envuelto en try-catch para capturar si la URL no existe (404)
                    pbxImagen.Load(url);
                }
                catch
                {
                    // Si la URL no se pudo cargar o no es una imagen válida, mostramos placeholder sin alertar al usuario
                    cargarImagenPorDefecto();
                }
            }
            else
            {
                // Si no es una URL válida, simplemente ponemos el placeholder
                cargarImagenPorDefecto();
            }
        }

        // Método auxiliar para evitar repetir la URL del placeholder
        private void cargarImagenPorDefecto()
        {
            try
            {
                pbxImagen.Load("https://via.placeholder.com/150");
            }
            catch
            {
                // Si no hay internet, desvinculamos la imagen para evitar crashes
                pbxImagen.Image = null;
            }

            // Caso 2: validar formato de URL
            if (Uri.TryCreate(txtImagenUrl.Text, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                try
                {
                    pbxImagen.LoadAsync(uriResult.ToString());
                }
                catch
                {
                    MessageBox.Show("Error al cargar la imagen.");
                    pbxImagen.Load("https://via.placeholder.com/150");
                    
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar una dirección de imagen válida.");
                
            }

        }


        // Evento para validar que solo se ingresen números y coma en el campo de precio
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números, coma y control (backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

    }
}
