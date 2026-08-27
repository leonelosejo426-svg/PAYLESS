using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Usuario : Form
    {
        UsuarioDAO usuarioDAO = new UsuarioDAO();


        public Usuario()
        {
            InitializeComponent();

            ConfigurarDataGridView();
            ConfigurarComboBuscar();
            MostrarUsuarios();
        }

        private void Usuario_Load(object sender, EventArgs e)
        {
         
        }

        private void MostrarUsuarios()
        {

            try
            {
                DataTable tabla = usuarioDAO.MostrarUsuarios();

                if (tabla == null)
                {
                    MessageBox.Show(
                        "No se pudieron cargar los usuarios.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los usuarios:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            dgvUsuarios.AutoGenerateColumns = false;
            dgvUsuarios.ReadOnly = true;

            dgvUsuarios.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.AllowUserToAddRows = false;

            dgvUsuarios.Columns.Clear();


            // ==========================
            // ID
            // ==========================

            DataGridViewTextBoxColumn columnaID =
                new DataGridViewTextBoxColumn();

            columnaID.Name = "colID";
            columnaID.HeaderText = "ID";
            columnaID.DataPropertyName = "id_usuario";
            columnaID.Width = 70;

            dgvUsuarios.Columns.Add(columnaID);


            // ==========================
            // NOMBRE
            // ==========================

            DataGridViewTextBoxColumn columnaNombre =
                new DataGridViewTextBoxColumn();

            columnaNombre.Name = "colNombre";
            columnaNombre.HeaderText = "Nombre";
            columnaNombre.DataPropertyName = "nombre_completo";
            columnaNombre.Width = 200;

            dgvUsuarios.Columns.Add(columnaNombre);


            // ==========================
            // USUARIO
            // ==========================

            DataGridViewTextBoxColumn columnaUsuario =
                new DataGridViewTextBoxColumn();

            columnaUsuario.Name = "colUsuario";
            columnaUsuario.HeaderText = "Usuario";
            columnaUsuario.DataPropertyName = "nombre_usuario";
            columnaUsuario.Width = 150;

            dgvUsuarios.Columns.Add(columnaUsuario);


            // ==========================
            // ROL
            // ==========================

            DataGridViewTextBoxColumn columnaRol =
                new DataGridViewTextBoxColumn();

            columnaRol.Name = "colRol";
            columnaRol.HeaderText = "Rol";
            columnaRol.DataPropertyName = "nombre_rol";
            columnaRol.Width = 120;

            dgvUsuarios.Columns.Add(columnaRol);


            // ==========================
            // ESTADO
            // ==========================

            DataGridViewTextBoxColumn columnaEstado =
                new DataGridViewTextBoxColumn();

            columnaEstado.Name = "colEstado";
            columnaEstado.HeaderText = "Estado";
            columnaEstado.DataPropertyName = "estado";
            columnaEstado.Width = 100;

            dgvUsuarios.Columns.Add(columnaEstado);
        }





        private void label18_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
        private void label17_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo();
            ventana.Show();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show(); 
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            inventario ventana = new inventario();
            ventana.Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito();
            ventana.Show();
            this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cmbBuscar.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione qué desea buscar.",
                    "Búsqueda",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string campo = "";

            switch (cmbBuscar.Text)
            {
                case "Usuario":
                    campo = "nombre_usuario";
                    break;

                case "Nombre":
                    campo = "nombre_completo";
                    break;

                case "Rol":
                    campo = "nombre_rol";
                    break;

                default:
                    MessageBox.Show(
                        "Seleccione Usuario, Nombre o Rol.",
                        "Búsqueda",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
            }

            string valor = "";

            using (Form formularioBuscar = new Form())
            {
                formularioBuscar.Text = "Buscar usuario";
                formularioBuscar.StartPosition = FormStartPosition.CenterParent;
                formularioBuscar.FormBorderStyle = FormBorderStyle.FixedDialog;
                formularioBuscar.MaximizeBox = false;
                formularioBuscar.MinimizeBox = false;
                formularioBuscar.ClientSize = new Size(350, 130);

                Label etiqueta = new Label();
                etiqueta.Text = "Ingrese el valor que desea buscar:";
                etiqueta.AutoSize = true;
                etiqueta.Location = new Point(15, 15);

                TextBox txtValor = new TextBox();
                txtValor.Width = 310;
                txtValor.Location = new Point(15, 40);

                Button botonAceptar = new Button();
                botonAceptar.Text = "Buscar";
                botonAceptar.Width = 90;
                botonAceptar.Location = new Point(145, 75);
                botonAceptar.DialogResult = DialogResult.OK;

                Button botonCancelar = new Button();
                botonCancelar.Text = "Cancelar";
                botonCancelar.Width = 90;
                botonCancelar.Location = new Point(240, 75);
                botonCancelar.DialogResult = DialogResult.Cancel;

                formularioBuscar.Controls.Add(etiqueta);
                formularioBuscar.Controls.Add(txtValor);
                formularioBuscar.Controls.Add(botonAceptar);
                formularioBuscar.Controls.Add(botonCancelar);

                formularioBuscar.AcceptButton = botonAceptar;
                formularioBuscar.CancelButton = botonCancelar;

                if (formularioBuscar.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                valor = txtValor.Text.Trim();
            }

            if (string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show(
                    "Ingrese un valor para buscar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            try
            {
                DataTable tabla = usuarioDAO.BuscarUsuarios(campo, valor);

                if (tabla == null || tabla.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontraron usuarios.",
                        "Búsqueda",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    MostrarUsuarios();
                    return;
                }

                // IMPORTANTE:
                // No cambiar las columnas del DataGridView.
                // Solamente cambiar los datos.
                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)

            {

                MessageBox.Show(

                    "Seleccione un usuario.",

                    "Aviso",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Warning

                );

                return;

            }

            int idUsuario = Convert.ToInt32(

                dgvUsuarios.CurrentRow.Cells["colID"].Value

            );

            DialogResult respuesta = MessageBox.Show(

                "¿Está seguro de eliminar este usuario?",

                "Confirmar eliminación",

                MessageBoxButtons.YesNo,

                MessageBoxIcon.Question

            );

            if (respuesta == DialogResult.Yes)

            {

                bool eliminado = usuarioDAO.EliminarUsuario(idUsuario);

                if (eliminado)

                {

                    MessageBox.Show(

                        "Usuario eliminado correctamente.",

                        "Éxito",

                        MessageBoxButtons.OK,

                        MessageBoxIcon.Information

                    );

                    MostrarUsuarios();

                }

                else

                {

                    MessageBox.Show(

                        "No se pudo eliminar el usuario.",

                        "Error",

                        MessageBoxButtons.OK,

                        MessageBoxIcon.Error

                    );

                }

            }

        }
        private void ConfigurarComboBuscar()
        {
            cmbBuscar.Items.Clear();

            cmbBuscar.Items.Add("Usuario");
            cmbBuscar.Items.Add("Nombre");
            cmbBuscar.Items.Add("Rol");
            cmbBuscar.Items.Add("Activo");
            cmbBuscar.Items.Add("Inactivo");

            cmbBuscar.SelectedIndex = -1;
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento();
            ventana.Show();
            this.Hide();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
