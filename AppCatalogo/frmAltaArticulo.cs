using AppCatalogo.Clases;
using AppCatalogo.Servicios;
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
    

    public partial class frmAltaArticulo : Form
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
                if (string.IsNullOrWhiteSpace(txtCodigo.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Debe completar Código y Nombre.");
                    return;
                }

                if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
                {
                    MessageBox.Show("El precio debe ser un número válido.");
                    return;
                }

                if (txtDescripcion.Text.Length > 100)
                {
                    MessageBox.Show("La descripción debe tener como máximo 100 caracteres.");
                    return;
                }

                ArticuloServicio servicio = new ArticuloServicio();

                if (articulo == null)
                    articulo = new Articulo(); // Alta

                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Precio = precio;
                articulo.ImagenUrl = txtImagenUrl.Text;
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;

                if (articulo.Id != 0)
                {
                    servicio.Modificar(articulo);
                    MessageBox.Show("Artículo modificado exitosamente");
                }
                else
                {
                    servicio.Agregar(articulo);
                    MessageBox.Show("Artículo agregado exitosamente");
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el artículo: " + ex.Message);
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


        private void txtImagenUrl_Leave(object sender, EventArgs e)
        {
            try
            {
                pbxImagen.Load(txtImagenUrl.Text);
            }
            catch
            {
                pbxImagen.Image = null;
            }
           
        }

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
