using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless
{

    public partial class Categoria : Form
    {
        CategoriaDAO categoriaDAO = new CategoriaDAO();
        public Categoria()
        {
            InitializeComponent();
        }

        private void Categoria_Load(object sender, EventArgs e)
        {
            ConfigurarColumnas();

            CargarCategorias();

            cmbBuscarPor.Items.Clear();

            cmbBuscarPor.Items.Add("Nombre");
            cmbBuscarPor.Items.Add("Descripción");
            cmbBuscarPor.Items.Add("Estado");

            cmbBuscarPor.SelectedIndex = 0;
        }

        private void CargarCategorias()
        {
            dgvCategorias.DataSource =
       categoriaDAO.MostrarCategorias();
        }

        private void ConfigurarColumnas()
        {
            dgvCategorias.AutoGenerateColumns = false;

            dgvCategorias.Columns["colId"].DataPropertyName = "id_categoria";
            dgvCategorias.Columns["colNombre"].DataPropertyName = "nombre_categoria";
            dgvCategorias.Columns["colDescripcion"].DataPropertyName = "descripcion";
            dgvCategorias.Columns["colEstado"].DataPropertyName = "estado";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            SubCategoriaAgregar ventana = new SubCategoriaAgregar();
            ventana.Show();
            this.Hide();

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            SubCategoriaEditar ventana = new SubCategoriaEditar();
            ventana.Show();
            this.Hide();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string campo = "";

            switch (cmbBuscarPor.Text)
            {
                case "Nombre":
                    campo = "nombre_categoria";
                    break;

                case "Descripción":
                    campo = "descripcion";
                    break;

                case "Estado":
                    campo = "estado";
                    break;
            }

            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                CargarCategorias();
                return;
            }

            dgvCategorias.DataSource =
                categoriaDAO.Buscar(campo, txtBuscar.Text);

        } 
    }
}
