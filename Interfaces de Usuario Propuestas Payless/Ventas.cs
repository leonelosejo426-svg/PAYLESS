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
    public partial class Ventas : Form
    {
        public Ventas()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            Menú_Principal.Show()
            this.Hide();
        }
    }
}
