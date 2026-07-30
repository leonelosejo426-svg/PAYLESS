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
    public partial class Recuperacion_de_Cuenta : Form
    {
        public Recuperacion_de_Cuenta()
        {
            InitializeComponent();


        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void Recuperacion_de_Cuenta_Load(object sender, EventArgs e)
        {
            textBox2.TabIndex = 0;
            button1.TabIndex = 1;
            button2.TabIndex = 2;
            button3.TabIndex = 3;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            Login ventana = new Login();
            ventana.Show();

            this.Hide();
        }
    }
}
