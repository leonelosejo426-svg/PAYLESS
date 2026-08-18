using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
using Interfaces_de_Usuario_Propuestas_Payless.Datos;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Productos : Form
    {
        ProductoDAO productoDAO = new ProductoDAO();

        ClaseUsuario usuarioActual;

        private DataTable tablaProductos;

        public Productos()
        {
            InitializeComponent();

            ConfigurarDataGridView();
            ConfigurarComboBuscar();
        }


        // =========================================================
        // CONFIGURACIÓN DEL DATAGRIDVIEW
        // =========================================================
        private void ConfigurarDataGridView()
        {
            // IMPORTANTE:
            // Evita que el DataGridView cree automáticamente
            // las columnas que vienen desde PostgreSQL.
            dgvProductos.AutoGenerateColumns = false;

            // No permitir editar ninguna celda directamente.
            dgvProductos.ReadOnly = true;

            // Seleccionar la fila completa.
            dgvProductos.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvProductos.MultiSelect = false;

            // No permitir agregar filas desde el DataGridView.
            dgvProductos.AllowUserToAddRows = false;

            // Fuente Times New Roman tamaño 12.
            dgvProductos.Font =
                new Font("Times New Roman", 12);

            dgvProductos.ColumnHeadersDefaultCellStyle.Font =
                new Font("Times New Roman", 12);

            // Elimina las columnas que tengas creadas actualmente.
            // Esto evita las columnas duplicadas.
            dgvProductos.Columns.Clear();


            // =====================================================
            // ID
            // =====================================================
            DataGridViewTextBoxColumn columnaID =
                new DataGridViewTextBoxColumn();

            columnaID.Name = "colID";
            columnaID.HeaderText = "ID";
            columnaID.DataPropertyName = "id_producto";
            columnaID.ReadOnly = true;
            columnaID.Width = 80;

            dgvProductos.Columns.Add(columnaID);


            // =====================================================
            // NOMBRE
            // =====================================================
            DataGridViewTextBoxColumn columnaNombre =
                new DataGridViewTextBoxColumn();

            columnaNombre.Name = "colNombre";
            columnaNombre.HeaderText = "Nombre";
            columnaNombre.DataPropertyName = "nombre";
            columnaNombre.ReadOnly = true;
            columnaNombre.Width = 180;

            dgvProductos.Columns.Add(columnaNombre);


            // =====================================================
            // ESTADO
            // =====================================================
            DataGridViewCheckBoxColumn columnaEstado =
                new DataGridViewCheckBoxColumn();

            columnaEstado.Name = "colEstado";
            columnaEstado.HeaderText = "Estado";
            columnaEstado.DataPropertyName = "estado_producto";
            columnaEstado.ReadOnly = true;
            columnaEstado.Width = 100;

            dgvProductos.Columns.Add(columnaEstado);


            // =====================================================
            // CATEGORÍA
            // =====================================================
            DataGridViewTextBoxColumn columnaCategoria =
                new DataGridViewTextBoxColumn();

            columnaCategoria.Name = "colCategoria";
            columnaCategoria.HeaderText = "Categoría";
            columnaCategoria.DataPropertyName = "categoria";
            columnaCategoria.ReadOnly = true;
            columnaCategoria.Width = 150;

            dgvProductos.Columns.Add(columnaCategoria);


            // =====================================================
            // MARCA
            // =====================================================
            DataGridViewTextBoxColumn columnaMarca =
                new DataGridViewTextBoxColumn();

            columnaMarca.Name = "colMarca";
            columnaMarca.HeaderText = "Marca";
            columnaMarca.DataPropertyName = "marca";
            columnaMarca.ReadOnly = true;
            columnaMarca.Width = 150;

            dgvProductos.Columns.Add(columnaMarca);


            // =====================================================
            // PROVEEDOR
            // =====================================================
            DataGridViewTextBoxColumn columnaProveedor =
                new DataGridViewTextBoxColumn();

            columnaProveedor.Name = "colProveedor";
            columnaProveedor.HeaderText = "Proveedor";
            columnaProveedor.DataPropertyName = "proveedor";
            columnaProveedor.ReadOnly = true;
            columnaProveedor.Width = 150;

            dgvProductos.Columns.Add(columnaProveedor);
        }


        // =========================================================
        // CONFIGURAR COMBOBOX
        // =========================================================
        private void ConfigurarComboBuscar()
        {
            cmbBuscarPor.Items.Clear();

            cmbBuscarPor.Items.Add("ID");
            cmbBuscarPor.Items.Add("Nombre");
            cmbBuscarPor.Items.Add("Categoría");
            cmbBuscarPor.Items.Add("Marca");
            cmbBuscarPor.Items.Add("Proveedor");
            cmbBuscarPor.Items.Add("Estado");

            cmbBuscarPor.SelectedIndex = -1;
        }


        // =========================================================
        // MOSTRAR PRODUCTOS
        // =========================================================
        private void MostrarProductos()
        {
            try
            {
                tablaProductos = productoDAO.MostrarProductos();

                dgvProductos.DataSource = null;
                dgvProductos.DataSource = tablaProductos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los productos: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void Productos_Load(object sender, EventArgs e)
        {
            if (ClaseSesion.RolActual != "ADMIN")
            {
                MessageBox.Show(
                    "No tienes acceso",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                this.Hide();
                return;
            }

            MostrarProductos();
        }



        private void button3_Click(object sender, EventArgs e)
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

        private void label20_Click(object sender, EventArgs e)
        {
            new Cliente().Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos(); ventana.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal(); ventana.Show();
            this.Hide();
        }

        private void label25_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja(); ventana.Show();
            this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito(); ventana.Show(); 
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
         
        }

        private void label23_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas(); ventana.Show();
            this.Hide();
        }

        private void label24_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo(); ventana.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            inventario ventana = new inventario(); ventana.Show(); this.Hide();
        }

        private void label9_Click_1(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento(); ventana.Show(); this.Hide();
        }

        private void cmbBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            SubProductoAgregar formulario = new SubProductoAgregar();

            formulario.ShowDialog();

            MostrarProductos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
           /* string json = JsonConvert.SerializeObject(
        listaProductos,
        Formatting.Indented);

            File.WriteAllText("productos.json", json);

          MessageBox.Show("Productos guardados correctamente."); */
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbBuscarPor.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione una opción en 'Buscar por'.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (tablaProductos == null)
            {
                MessageBox.Show(
                    "No hay productos cargados.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            string criterio =
                cmbBuscarPor.SelectedItem.ToString();

            string texto = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese el valor que desea buscar:",
                "Buscar producto",
                "");

            if (string.IsNullOrWhiteSpace(texto))
                return;

            try
            {
                DataView vista =
                    new DataView(tablaProductos);

                string textoSeguro =
                    texto.Replace("'", "''");

                switch (criterio)
                {
                    case "ID":

                        if (!int.TryParse(texto, out int id))
                        {
                            MessageBox.Show(
                                "El ID debe ser un número.",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return;
                        }

                        vista.RowFilter =
                            $"id_producto = {id}";

                        break;


                    case "Nombre":

                        vista.RowFilter =
                            $"CONVERT(nombre, 'System.String') LIKE '%{textoSeguro}%'";

                        break;


                    case "Categoría":

                        vista.RowFilter =
                            $"CONVERT(categoria, 'System.String') LIKE '%{textoSeguro}%'";

                        break;


                    case "Marca":

                        vista.RowFilter =
                            $"CONVERT(marca, 'System.String') LIKE '%{textoSeguro}%'";

                        break;


                    case "Proveedor":

                        vista.RowFilter =
                            $"CONVERT(proveedor, 'System.String') LIKE '%{textoSeguro}%'";

                        break;


                    case "Estado":

                        if (texto.ToLower() == "activo" ||
                            texto.ToLower() == "true" ||
                            texto == "1")
                        {
                            vista.RowFilter =
                                "estado_producto = TRUE";
                        }
                        else if (texto.ToLower() == "inactivo" ||
                                 texto.ToLower() == "false" ||
                                 texto == "0")
                        {
                            vista.RowFilter =
                                "estado_producto = FALSE";
                        }
                        else
                        {
                            MessageBox.Show(
                                "Escriba Activo o Inactivo.",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            return;
                        }

                        break;
                }

                dgvProductos.DataSource = vista;

                if (vista.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontraron productos.",
                        "Resultado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarProducto ventana = new EditarProducto();
            ventana.Show();
            this.Hide();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione un producto.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            int idProducto = Convert.ToInt32(
                dgvProductos.CurrentRow.Cells["colID"].Value);

            DialogResult respuesta =
                MessageBox.Show(
                    "¿Desea eliminar este producto?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            bool eliminado =
                productoDAO.EliminarProducto(idProducto);

            if (eliminado)
            {
                MessageBox.Show(
                    "Producto eliminado correctamente.",
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                MostrarProductos();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo eliminar el producto.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Reporte_Productos ventana = new Reporte_Productos();
            ventana.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Categoria ventana = new Categoria();
            ventana.Show();
            this.Hide();
        }

        private void btnMarca_Click(object sender, EventArgs e)
        {
          //  Marca ventana = new Marca();
          //  ventana.Show();
            //this.Hide();
        }
    }
}
