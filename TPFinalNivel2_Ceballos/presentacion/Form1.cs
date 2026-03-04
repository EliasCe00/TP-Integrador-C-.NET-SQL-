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
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            cargar();
            cboCampo.Items.Add("Marca");
            cboCampo.Items.Add("Categoria");
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
            dgvArticulos.DataSource = negocio.listar();
            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo alta = new frmAltaArticulo();
            alta.ShowDialog();
        }
    }
}
