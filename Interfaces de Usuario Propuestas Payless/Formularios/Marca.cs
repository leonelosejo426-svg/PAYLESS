using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless.Formularios
{
    public partial class Marca : Form
    {
        MarcaDAO marcaDAO = new MarcaDAO();

        public Marca()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            SubMarcaAgregar ventana = new SubMarcaAgregar();
            ventana.Show();
            this.Hide();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            SubMarcaEditar ventana = new SubMarcaEditar();
            ventana.Show();
            this.Hide();
        }

        private void Marca_Load(object sender, EventArgs e)
        {
            ConfigurarColumnas();

            CargarMarcas();

            cmbBuscarPor.Items.Clear();

            cmbBuscarPor.Items.Add("Nombre");
            cmbBuscarPor.Items.Add("Descripción");
            cmbBuscarPor.Items.Add("Estado");

            cmbBuscarPor.SelectedIndex = 0;
        }

        private void CargarMarcas()
        {
            dgvMarcas.DataSource =
                marcaDAO.MostrarMarcas();
        }

        private void ConfigurarColumnas()
        {
            dgvMarcas.AutoGenerateColumns = false;

            dgvMarcas.Columns["colId"].DataPropertyName =
                "id_marca";

            dgvMarcas.Columns["colNombre"].DataPropertyName =
                "nombre_marca";

            dgvMarcas.Columns["colDescripcion"].DataPropertyName =
                "descripcion";

            dgvMarcas.Columns["colEstado"].DataPropertyName =
                "estado";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string campo = "";

            switch (cmbBuscarPor.Text)
            {
                case "Nombre":
                    campo = "nombre_marca";
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
                CargarMarcas();
                return;
            }

            dgvMarcas.DataSource =
                marcaDAO.Buscar(campo, txtBuscar.Text);
        }
    }
}
