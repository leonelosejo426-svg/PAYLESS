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
    public partial class SubMarcaAgregar : Form
    {
        CategoriaDAO categoriaDAO = new CategoriaDAO();
        public SubMarcaAgregar()
        {
            InitializeComponent();
        }

        private void SubMarcaAgregar_Load(object sender, EventArgs e)
        {
            cmbEstado.Items.Clear();

            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Inactivo");

            cmbEstado.SelectedIndex = 0;
        }
    }
}
