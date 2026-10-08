using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
using Interfaces_de_Usuario_Propuestas_Payless.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Compras_nuevo : Form
    {
        private CompraDAO compraDAO;
        private ArbolCompras arbolCompras;

        public Compras_nuevo()
        {
            InitializeComponent();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();
        }

        private void label28_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }

        private void label24_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();
            this.Hide();
        }

        private void label23_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Hide();
        }

        private void label25_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo();
            ventana.Show();
            this.Hide();
        }

        private void label26_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();

        }

        private void label29_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito();
            ventana.Show();
            this.Hide();
        }

        private void label30_Click(object sender, EventArgs e)
        {
            inventario ventana = new inventario();
            ventana.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento();
            ventana.Show();
            this.Hide();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reporte_Compra ventana = new Reporte_Compra();
            ventana.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }



        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void cmbTalla_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        private void btnAgregarProductos_Click(object sender, EventArgs e)
        {
        }

        // ============================================================
        // CALCULAR TOTALES
        // ============================================================



        // ============================================================
        // ELIMINAR
        // ============================================================

        private void btnEliminar_Click(
            object sender,
            EventArgs e)
        {

        }




        private void btnEditar_Click(object sender, EventArgs e)
        {

        }




        private void txtNoCompra_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void Compras_nuevo_Load(object sender, EventArgs e)
        {
            compraDAO = new CompraDAO();
            arbolCompras = new ArbolCompras();

            ConfigurarFormulario();
            CargarCompras();
        }

        // ==========================================
        // CONFIGURAR FORMULARIO
        // ==========================================
        private void ConfigurarFormulario()
        {
            cbBuscarPor.Items.Clear();

            cbBuscarPor.Items.Add("N.º Compra");

            cbBuscarPor.SelectedIndex = 0;

            txtBuscar.Clear();

            ConfigurarDataGridView();
        }

        // ==========================================
        // CONFIGURAR DATAGRIDVIEW
        // ==========================================
        private void ConfigurarDataGridView()
        {
            dgvCompras.AutoGenerateColumns = false;

            colIdCompra.DataPropertyName = "id_compra";
            colFecha.DataPropertyName = "fecha";
            colProveedor.DataPropertyName = "proveedor";
            colTotal.DataPropertyName = "total";
            colEstado.DataPropertyName = "estado";

            colFecha.DefaultCellStyle.Format =
                "dd/MM/yyyy HH:mm";

            colTotal.DefaultCellStyle.Format =
                "C2";
        }

        // ==========================================
        // CARGAR COMPRAS
        // ==========================================
        private void CargarCompras()
        {
            try
            {
                DataTable tabla =
                    compraDAO.MostrarCompras();

                dgvCompras.DataSource = tabla;

                CargarArbol(tabla);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las compras:\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // CARGAR ÁRBOL
        // ==========================================
        private void CargarArbol(DataTable tabla)
        {
            arbolCompras.Limpiar();

            foreach (DataRow fila in tabla.Rows)
            {
                int idCompra =
                    Convert.ToInt32(fila["id_compra"]);

                DateTime fecha =
                    Convert.ToDateTime(fila["fecha"]);

                string proveedor =
                    fila["proveedor"].ToString();

                decimal total =
                    Convert.ToDecimal(fila["total"]);

                bool estado =
                    Convert.ToBoolean(fila["estado"]);

                NodoCompra nodo =
                    new NodoCompra(
                        idCompra,
                        fecha,
                        proveedor,
                        total,
                        estado);

                arbolCompras.Insertar(nodo);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            NuevaCompra formulario =
        new NuevaCompra();

            formulario.ShowDialog();

            CargarCompras();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarCompra();
        }

        private void BuscarCompra()
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                MessageBox.Show(
                    "Ingrese el número de compra.",
                    "Buscar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtBuscar.Focus();

                return;
            }

            int idCompra;

            if (!int.TryParse(
                txtBuscar.Text.Trim(),
                out idCompra))
            {
                MessageBox.Show(
                    "El número de compra debe ser numérico.",
                    "Buscar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtBuscar.Focus();

                return;
            }

            // Buscar primero en el árbol
            NodoCompra compra =
                arbolCompras.Buscar(idCompra);

            if (compra == null)
            {
                MessageBox.Show(
                    "No se encontró la compra N.º " +
                    idCompra + ".",
                    "Buscar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                dgvCompras.ClearSelection();

                return;
            }

            // Obtener información actualizada desde BD
            DataTable resultado =
                compraDAO.BuscarCompraPorId(idCompra);

            if (resultado.Rows.Count == 0)
            {
                MessageBox.Show(
                    "La compra no se encuentra disponible.",
                    "Buscar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            dgvCompras.DataSource = resultado;

            dgvCompras.ClearSelection();

            if (dgvCompras.Rows.Count > 0)
            {
                dgvCompras.Rows[0].Selected = true;
            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (dgvCompras.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione una compra.",
                    "Eliminar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            int idCompra =
                Convert.ToInt32(
                    dgvCompras.SelectedRows[0]
                    .Cells["colIdCompra"]
                    .Value);

            DialogResult respuesta =
                MessageBox.Show(
                    "¿Está seguro de eliminar la compra N.º " +
                    idCompra + "?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            try
            {
                bool eliminado =
                    compraDAO.EliminarCompra(idCompra);

                if (eliminado)
                {
                    // Eliminar también del árbol
                    arbolCompras.Eliminar(idCompra);

                    MessageBox.Show(
                        "Compra eliminada correctamente.",
                        "Eliminar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CargarCompras();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo eliminar la compra.",
                        "Eliminar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al eliminar la compra:\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        // ==========================================
        // LIMPIAR BÚSQUEDA
        // ==========================================
        private void txtBuscar_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BuscarCompra();
                e.SuppressKeyPress = true;
            }
        }
    }
}