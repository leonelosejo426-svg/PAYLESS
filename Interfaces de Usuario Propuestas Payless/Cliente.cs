using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Cliente : Form
    {
        ClaseUsuario usuarioActual;

        //lista cliente
        List<cliente> listacliente = new List<cliente>();


        public Cliente()
        {
            InitializeComponent();


        }
        //Clase cliente
        public class cliente
        {
            public string Nombre { get; set; }
            public string Telefono { get; set; }
            public string Codigo { get; set; }
            public string Cedula { get; set; }
            public string Estado { get; set; }
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {
            new Proveedores().Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            new Usuario().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();

            this.Hide();
        }

        private void Cliente_Load(object sender, EventArgs e)
        {
            if (ClaseSesion.RolActual != "ADMIN" && ClaseSesion.RolActual != "YUBELKIS")
            {
                MessageBox.Show("No tienes acceso");

                new Menú_Principal().Show(); 
                this.Hide();

                return;
            }

        }

        private void label15_Click(object sender, EventArgs e)
        {
            new Productos().Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void label14_Click_1(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Hide();
        }

        private void label15_Click_1(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();
            this.Hide();
        }

        private void label17_Click_1(object sender, EventArgs e)
        {
            Compras ventana = new Compras();
            ventana.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Devoluciones ventana = new Devoluciones();
            ventana.Show();
            this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {
           Credito ventana = new Credito(); 
           ventana.Show();
           this.Hide();
        }

        private void label20_Click_1(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtcedula.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese la cedula");
                txtcedula.Focus();
                return;
            }
            if (txtNombre.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese un nombre");
                txtNombre.Focus();
                return;
            }
            if (txtTelefono.Text.Trim() == "")
            {
                MessageBox.Show("Ingrece un número de Teléfono");
                txtcedula.Focus();
                return;
            }
            if (txtcodigo.Text.Trim() == "")
            {
                MessageBox.Show("Debe de asignar un codigo");
                txtcodigo.Focus();
                return;
            }
            string json = JsonConvert.SerializeObject(
                listacliente,
                Newtonsoft.Json.Formatting.Indented
                );

            File.WriteAllText("clientes.json", json);
            MessageBox.Show("Cliente guardado correctamente");

              
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DGVtabla1.CurrentRow != null)
            {
                int indice = DGVtabla1.CurrentRow.Index;

                DialogResult respuesta = MessageBox.Show(
                    "¿ Desea eliminar a este cliente ?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (respuesta == DialogResult.Yes)
                {
                    listacliente.RemoveAt(indice);

                    DGVtabla1.DataSource = null;
                    DGVtabla1.DataSource = listacliente;

                    MessageBox.Show("Cliente eliminado correctamente");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un cliente");
            }

        }
       

        private void button4_Click(object sender, EventArgs e)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<html>");
            html.Append("<head>");
            html.Append("<title>Reporte</title>");
            html.Append("</head>");
            html.Append("<body>");

            html.Append("<h1>REPORTE DE CLIENTES</h1>");
            html.Append("<table border='1' cellpadding='5'>");

            html.Append("<tr>");
            html.Append("<th>Nombre</th>");
            html.Append("<th>Telefono</th>");
            html.Append("<th>Código</th>");
            html.Append("<th>Cédula</th>");
            html.Append("<th>Estado</th>");
            html.Append("</tr>");

            foreach(cliente p in listacliente)
            {
                html.Append("<tr>");
                html.Append("<td>" + p.Nombre + "</td>");
                html.Append("<td>" + p.Telefono + "</td>");
                html.Append("<td>" + p.Codigo + "</td>");
                html.Append("<td>" + p.Cedula + "</td>");
                html.Append("<td>" + p.Estado + "</td>");
                html.Append("</tr>");
            }

            html.Append("</table>");
            html.Append("</body>");
            html.Append("</html>");

            File.WriteAllText("Reporte.html", html.ToString());
            Process.Start("Reporte.html");

            MessageBox.Show("Reporte generado");
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento();
            ventana.Show();
            this.Hide();
        }

        private void CBbusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNombre.Visible = false;
            txtcedula.Visible = false;
            txtcodigo.Visible = false;

            txtNombre.Clear();
            txtcedula.Clear();
            txtcodigo.Clear();

            switch (CBbusqueda.Text)
            {
                case "Nombre":
                    txtNombre.Visible = true;
                    LblNombre.Visible = true;
                    txtNombre.Focus();
                    break;

                case "Numero de cédula":
                    txtcedula.Visible = true;
                    LblNumero.Visible = true;
                    txtcedula.Focus();
                    break;

                case "Código":
                    txtcodigo.Visible = true;
                    Lblcodigo.Visible = true;
                    txtcodigo.Focus();
                    break;
            }
                
        }

        private void CBestado_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (File.Exists("cliente.json"))
            {
                //leer archivo json
                string json = File.ReadAllText("cliente.json");

                //Convertir Json a lista 
                listacliente = JsonConvert.DeserializeObject<List<cliente>>(json);

                // Mostrar en datagridview
                DGVtabla1.DataSource = null;
                DGVtabla1.DataSource = listacliente;

                MessageBox.Show("Datos cargados correctamente");
            }
            else
            {
                MessageBox.Show("No existe el archivo");
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            List<cliente> resultado = new List<cliente>();

            switch (CBbusqueda.Text)
            {
                case "Nombre":
                    resultado = listacliente
                        .Where(c => c.Nombre.Contains(txtNombre.Text))
                        .ToList();
                    break;

                case "Numero de cédula":
                    resultado = listacliente
                        .Where(c => c.Cedula.Contains(txtcedula.Text))
                        .ToList();
                    break;

                case "Código":
                    resultado = listacliente
                        .Where(c => c.Codigo.Contains(txtcodigo.Text))
                        .ToList();
                    break;

                default:
                    MessageBox.Show("Seleccione un criterio de busqueda");
                    return;
            }
            DGVtabla1.DataSource = null;
            DGVtabla1.DataSource = resultado;

            if (resultado.Count == 0)
            {
                MessageBox.Show("No se encontraron resultados");
            }

            
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnagregar_Click_1(object sender, EventArgs e)
        {
            
        }
    }

}
