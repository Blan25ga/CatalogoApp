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
                //la descripcion debe ser menos o igual a 100 caracteres
                if (txtDescripcion.Text.Length > 100)
                {
                    MessageBox.Show("La descripción debe tener como máximo 100 caracteres.");
                    return;
                }

                Articulo nuevo = new Articulo();
                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.Precio = precio;
                nuevo.ImagenUrl = txtImagenUrl.Text;
                nuevo.Marca = (Marca)cboMarca.SelectedItem;
                nuevo.Categoria = (Categoria)cboCategoria.SelectedItem;

                ArticuloServicio servicio = new ArticuloServicio();
                servicio.Agregar(nuevo);

                MessageBox.Show("Artículo agregado exitosamente");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el artículo: " + ex.Message);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar combos: " + ex.Message);
            }
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
