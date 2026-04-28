using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Caja : Form
    {

        public Caja()
        {
            InitializeComponent();

        }

        private void Caja_Load(object sender, EventArgs e)
        {

            

            if (ClaseSesion.RolActual != "ADMIN" && ClaseSesion.RolActual != "KELLY")
            {
                MessageBox.Show("No tienes acceso");
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
