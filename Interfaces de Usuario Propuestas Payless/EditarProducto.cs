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
    public partial class EditarProducto : Form
    {
        ProductoDAO dao = new ProductoDAO();
        int idProducto = 0;
        int idProductoTalla = 0;

        public EditarProducto()
        {
            InitializeComponent();
        }
        //Cargar formulario
        private void EditarProducto_Load(object sender, EventArgs e)
        {
            txtCodigoP.ReadOnly = true;
            //Cargar productos
            CBProducto.DataSource = dao.CargarProductos();
            CBProducto.DisplayMember = "nombre";
            CBProducto.ValueMember = "id_producto";
            CBProducto.SelectedIndex = -1;

           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
