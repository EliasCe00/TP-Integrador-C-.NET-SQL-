using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;
using dominio;

namespace presentacion
{
    public partial class frmPrincipal : Form
    {
        private List<Articulo> listaArticulo;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            cargar();
            cboCampo.Items.Add("Marca");
            cboCampo.Items.Add("Categoria");
            pbxArticulo.Load(listaArticulo[0].ImagenUrl);
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboCampo.SelectedItem.ToString() == "Marca")
            {
                MarcaNegocio negocio = new MarcaNegocio();
                cboCriterio.DataSource = null;
                cboCriterio.DataSource = negocio.listar();
                cboCriterio.ValueMember = "Id";
                cboCriterio.DisplayMember = "Descripcion";
            }

            if(cboCampo.SelectedItem.ToString() == "Categoria")
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                cboCriterio.DataSource = null;
                cboCriterio.DataSource = negocio.listar();
                cboCriterio.ValueMember = "Id";
                cboCriterio.DisplayMember = "Descripcion";
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                string campo = cboCampo.SelectedItem.ToString();
                int id = (int)cboCriterio.SelectedValue; 
                dgvArticulos.DataSource = negocio.filtrar(campo, id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulo = negocio.listar();
            dgvArticulos.DataSource = listaArticulo;
            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
            dgvArticulos.Columns["Precio"].DefaultCellStyle.Format = "C2";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo alta = new frmAltaArticulo();
            alta.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmAltaArticulo modificar = new frmAltaArticulo(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            DialogResult respuesta = MessageBox.Show("Confirmar eliminacion", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(respuesta == DialogResult.OK)
            {
                negocio.eliminar(seleccionado);
                cargar();
            }
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.ImagenUrl);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);

            }
            catch (Exception ex)
            {
                pbxArticulo.Load("https://imgs.search.brave.com/GTWN0t5sy1WPy7Ggov6t87keFnK_9ffTrXfWong5CiI/rs:fit:500:0:1:0/g:ce/aHR0cHM6Ly93d3cu/c2h1dHRlcnN0b2Nr/LmNvbS9pbWFnZS12/ZWN0b3Ivc2VhcmNo/aW5nLW5vdC1mb3Vu/ZC1uby1pbWFnZS0y/NjBudy0yNTQ4Njg0/MTYzLmpwZw");
            }
        }
    }
}
