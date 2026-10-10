using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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

            cmbBuscar.SelectedIndexChanged += cmbBuscar_SelectedIndexChanged;
        }

        private void Usuario_Load(object sender, EventArgs e)
        {
            lblCaja.Enabled = false;
            lblProveedores.Enabled = false;
            lblProductos.Enabled = false;
            lblVenta.Enabled = false;
            lblCompras.Enabled = false;
            lblUsuarios.Enabled = false;


            lblCliente.Enabled = false;
            lblVenta.Enabled = false;
            lblInventario.Enabled = false;
            lblMantenimiento.Enabled = false;
            lblReportes.Enabled = false;


            switch (ClaseSesion.RolActual)
            {
                case "Administrador":

                    lblCaja.Enabled = true;
                    lblCompras.Enabled = true;
                    lblVenta.Enabled = true;
                    lblUsuarios.Enabled = true;
                    lblMantenimiento.Enabled = true;
                    lblCliente.Enabled = true;
                    lblVenta.Enabled = true;
                    lblInventario.Enabled = true;
                    lblProveedores.Enabled = true;
                    lblProductos.Enabled = true;
                    lblReportes.Enabled = true;


                    break;

                case "Gerente":

                    lblCaja.Enabled = true;
                    lblCompras.Enabled = true;
                    lblVenta.Enabled = true;

                    break;

                case "Cajero":

                    lblCaja.Enabled = true;
                    lblVenta.Enabled = true;

                    break;
            }
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

            string opcion = cmbBuscar.Text.Trim();
            string valor = txtBuscar.Text.Trim();

            try
            {
                // ============================
                // BUSCAR POR USUARIO
                // ============================

                if (opcion == "Usuario")
                {
                    if (string.IsNullOrWhiteSpace(valor))
                    {
                        MessageBox.Show(
                            "Escriba el usuario que desea buscar.",
                            "Búsqueda",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        txtBuscar.Focus();
                        return;
                    }

                    DataTable tabla = usuarioDAO.BuscarUsuarios(
                        "nombre_usuario",
                        valor);

                    MostrarResultadoBusqueda(tabla);
                }

                // ============================
                // BUSCAR POR NOMBRE
                // ============================

                else if (opcion == "Nombre")
                {
                    if (string.IsNullOrWhiteSpace(valor))
                    {
                        MessageBox.Show(
                            "Escriba el nombre que desea buscar.",
                            "Búsqueda",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        txtBuscar.Focus();
                        return;
                    }

                    DataTable tabla = usuarioDAO.BuscarUsuarios(
                        "nombre_completo",
                        valor);

                    MostrarResultadoBusqueda(tabla);
                }

                // ============================
                // BUSCAR POR ROL
                // ============================

                else if (opcion == "Rol")
                {
                    if (string.IsNullOrWhiteSpace(valor))
                    {
                        MessageBox.Show(
                            "Escriba el rol que desea buscar.",
                            "Búsqueda",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        txtBuscar.Focus();
                        return;
                    }

                    DataTable tabla = usuarioDAO.BuscarUsuarios(
                        "nombre_rol",
                        valor);

                    MostrarResultadoBusqueda(tabla);
                }

                // ============================
                // MOSTRAR ACTIVOS
                // ============================

                else if (opcion == "Activo")
                {
                    txtBuscar.Clear();

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

                    DataView vista = new DataView(tabla);

                    vista.RowFilter = "estado = true";

                    dgvUsuarios.DataSource = null;
                    dgvUsuarios.DataSource = vista;
                }

                // ============================
                // MOSTRAR INACTIVOS
                // ============================

                else if (opcion == "Inactivo")
                {
                    txtBuscar.Clear();

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

                    DataView vista = new DataView(tabla);

                    vista.RowFilter = "estado = false";

                    dgvUsuarios.DataSource = null;
                    dgvUsuarios.DataSource = vista;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al realizar la búsqueda:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }

        }

        private void MostrarResultadoBusqueda(DataTable tabla)
        {
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

            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = tabla;
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

            cmbBuscar.SelectedIndex = 0;
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

        private void cmbBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBuscar.SelectedIndex == -1)
                return;

            if (cmbBuscar.Text == "Activo" || cmbBuscar.Text == "Inactivo")
            {
                txtBuscar.Enabled = false;
                txtBuscar.Clear();
            }
            else
            {
                txtBuscar.Enabled = true;
                txtBuscar.Focus();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Pusuario ventana = new Pusuario();
            ventana.ShowDialog();
            this.Show();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar_usuarios ventana = new Editar_usuarios();
            ventana.ShowDialog();
            this.Show();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            try
            {
                string rutaPDF = @"C:\Users\Lenovo\Desktop\PAYLESS\Interfaces de Usuario Propuestas Payless\Ayuda\Manual_Usuario.pdf";

                if (!File.Exists(rutaPDF))
                {
                    MessageBox.Show($"No se encontró el manual en la ruta:\n{rutaPDF}",
                                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Convertir la ruta del disco a formato URI web (maneja espacios y caracteres especiales)
                string uriPDF = new Uri(rutaPDF).AbsoluteUri;

                // 2. Cambia el número 2 por el número exacto de la página de Caja
                int numeroPaginaCaja = 7;

                // 3. Crear el argumento en formato file:///C:/...#page=2
                string argumentos = $"\"{uriPDF}#page={numeroPaginaCaja}\"";

                ProcessStartInfo edgeInfo = new ProcessStartInfo
                {
                    FileName = "msedge.exe",
                    Arguments = argumentos,
                    UseShellExecute = true
                };

                Process.Start(edgeInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el manual: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblReportes_Click(object sender, EventArgs e)
        {
            Reportes ventana = new Reportes();
            ventana.Show();
            this.Hide();
        }
    }
}
