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
    public partial class Credito : Form
    {
        public Credito()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Credito
            // 
            this.ClientSize = new System.Drawing.Size(278, 244);
            this.Name = "Credito";
            this.ResumeLayout(false);

        }
    }
}
