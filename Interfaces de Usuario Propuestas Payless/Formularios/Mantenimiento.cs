using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
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
    public partial class Mantenimiento : Form
    {
        private RespaldoBD respaldoBD = new RespaldoBD();

        private string rutaArchivoSeleccionado = "";
        public Mantenimiento()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo();
            ventana.Show();
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();
        }

        private void label24_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito();
            ventana.Show();
            this.Hide();
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

        private void Mantenimiento_Load(object sender, EventArgs e)
        {
            ConfigurarPermisos();

            try
            {
                CargarRespaldos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }


        private void ConfigurarPermisos()
        {
            lblCaja.Enabled = false;
            lblProveedores.Enabled = false;
            lblProductos.Enabled = false;
            lblVenta.Enabled = false;
            lblCompras.Enabled = false;
            lblUsuarios.Enabled = false;

            lblCliente.Enabled = false;
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

        private void CargarRespaldos()
        {
            try
            {
                dgvRespaldos.DataSource =
                    respaldoBD.MostrarRespaldos();

                if (dgvRespaldos.Columns.Contains(
                    "Nombre"))
                {
                    dgvRespaldos.Columns[
                        "Nombre"].Width = 200;
                }

                if (dgvRespaldos.Columns.Contains(
                    "Tipo"))
                {
                    dgvRespaldos.Columns[
                        "Tipo"].Width = 120;
                }

                if (dgvRespaldos.Columns.Contains(
                    "Ruta"))
                {
                    dgvRespaldos.Columns[
                        "Ruta"].Width = 450;
                }

                if (dgvRespaldos.Columns.Contains(
                    "Fecha"))
                {
                    dgvRespaldos.Columns[
                        "Fecha"].Width = 150;
                }

                if (dgvRespaldos.Columns.Contains(
                    "Tamaño"))
                {
                    dgvRespaldos.Columns[
                        "Tamaño"].Width = 100;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error al cargar respaldos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta;

                bool resultado =
                    respaldoBD.CrearRespaldoCompleto(
                        "Respaldo_Completo",
                        out ruta);

                if (resultado)
                {
                    MessageBox.Show(
                        "Respaldo completo creado correctamente.\n\n" +
                        "El archivo fue cifrado para proteger " +
                        "la información de la base de datos.\n\n" +
                        "Ubicación:\n" +
                        ruta,
                        "Respaldo completo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CargarRespaldos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error al crear respaldo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dialogo =
                    new OpenFileDialog())
                {
                    dialogo.Title =
                        "Seleccionar respaldo completo";

                    dialogo.Filter =
                        "Respaldos SQL (*.sql)|*.sql";

                    dialogo.Multiselect = false;

                    if (dialogo.ShowDialog() ==
                        DialogResult.OK)
                    {
                        rutaArchivoSeleccionado =
                            dialogo.FileName;

                        MessageBox.Show(
                            "Respaldo seleccionado:\n\n" +
                            rutaArchivoSeleccionado,
                            "Seleccionar respaldo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvRespaldos.CurrentRow == null)
                {
                    MessageBox.Show(
                        "Seleccione un respaldo de la lista.",
                        "Restaurar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Obtener tipo
                string tipo =
                    dgvRespaldos.CurrentRow
                    .Cells["Tipo"]
                    .Value?
                    .ToString();

                // Obtener ruta
                string ruta =
                    dgvRespaldos.CurrentRow
                    .Cells["Ruta"]
                    .Value?
                    .ToString();

                if (string.IsNullOrWhiteSpace(tipo))
                {
                    MessageBox.Show(
                        "No se pudo determinar el tipo " +
                        "de respaldo.",
                        "Restaurar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // ======================================================
                // SOLAMENTE COMPLETO
                // ======================================================

                if (!tipo.Equals(
                    "Completo",
                    StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show(
                        "Solo se pueden restaurar " +
                        "los respaldos completos.\n\n" +
                        "El respaldo seleccionado es de tipo: " +
                        tipo,
                        "Restaurar respaldo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                if (string.IsNullOrWhiteSpace(ruta))
                {
                    MessageBox.Show(
                        "No se encontró la ruta del respaldo.",
                        "Restaurar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // ======================================================
                // CONFIRMACIÓN
                // ======================================================

                DialogResult respuesta =
                    MessageBox.Show(
                        "¿Está seguro de restaurar este " +
                        "respaldo completo?\n\n" +
                        "La información actual de la base " +
                        "de datos será reemplazada.",
                        "Confirmar restauración",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                if (respuesta != DialogResult.Yes)
                    return;

                // ======================================================
                // RESTAURAR
                // ======================================================

                bool resultado =
                    respaldoBD.RestaurarRespaldo(
                        ruta);

                if (resultado)
                {
                    MessageBox.Show(
                        "El respaldo completo fue " +
                        "descifrado y restaurado correctamente.",
                        "Restauración",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CargarRespaldos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error al restaurar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string ObtenerRutaSeleccionada()
        {
            if (dgvRespaldos.CurrentRow == null)
                return "";

            if (dgvRespaldos.Columns.Contains("Ruta"))
            {
                object valor =
                    dgvRespaldos.CurrentRow
                    .Cells["Ruta"]
                    .Value;

                if (valor != null)
                    return valor.ToString();
            }

            return "";
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta =
                    ObtenerRutaSeleccionada();

                if (string.IsNullOrWhiteSpace(ruta))
                {
                    MessageBox.Show(
                        "Seleccione un respaldo.",
                        "Descargar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                using (SaveFileDialog dialogo =
                    new SaveFileDialog())
                {
                    dialogo.FileName =
                        Path.GetFileName(ruta);

                    dialogo.Filter =
                        "Archivo de respaldo (*.sql)|*.sql";

                    if (dialogo.ShowDialog() ==
                        DialogResult.OK)
                    {
                        respaldoBD.CopiarRespaldo(
                            ruta,
                            dialogo.FileName);

                        MessageBox.Show(
                            "Respaldo descargado correctamente.",
                            "Descargar",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error al descargar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta =
                    ObtenerRutaSeleccionada();

                if (string.IsNullOrWhiteSpace(ruta))
                {
                    MessageBox.Show(
                        "Seleccione un respaldo.",
                        "Eliminar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DialogResult respuesta =
                    MessageBox.Show(
                        "¿Está seguro de eliminar " +
                        "el respaldo seleccionado?",
                        "Confirmar eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                if (respuesta != DialogResult.Yes)
                    return;

                respaldoBD.EliminarRespaldo(
                    ruta);

                MessageBox.Show(
                    "Respaldo eliminado correctamente.",
                    "Eliminar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarRespaldos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error al eliminar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                CargarRespaldos();

                MessageBox.Show(
                    "Lista de respaldos actualizada.",
                    "Actualizar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void label13_Click(object sender, EventArgs e)
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

                // 2. Cambia el número 6 por el número exacto de la página de Caja
                int numeroPaginaCaja = 6;

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

        private void btnIncremental_Click(object sender, EventArgs e)
        {
            try
            {
                string carpeta;

                bool resultado =
                    respaldoBD.CrearRespaldoIncremental(
                        out carpeta);

                if (resultado)
                {
                    MessageBox.Show(
                        "Respaldo incremental creado correctamente.\n\n" +
                        "Ubicación:\n" +
                        carpeta,
                        "Respaldo incremental",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CargarRespaldos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error al crear respaldo incremental",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnDiferencial_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta;

                bool resultado =
                    respaldoBD.CrearRespaldoDiferencial(
                        out ruta);

                if (resultado)
                {
                    MessageBox.Show(
                        "Respaldo diferencial creado correctamente.\n\n" +
                        "El archivo fue cifrado para proteger " +
                        "la información de la base de datos.\n\n" +
                        "Ubicación:\n" +
                        ruta,
                        "Respaldo diferencial",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CargarRespaldos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error al crear respaldo diferencial",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string ObtenerTamaño(
            long bytes)
        {
            if (bytes < 1024)
                return bytes + " B";

            if (bytes <
                1024 * 1024)
            {
                return
                    $"{bytes / 1024.0:F2} KB";
            }

            if (bytes <
                1024L * 1024L * 1024L)
            {
                return
                    $"{bytes / 1024.0 / 1024.0:F2} MB";
            }

            return
                $"{bytes / 1024.0 / 1024.0 / 1024.0:F2} GB";
        }

        private void lblReportes_Click(object sender, EventArgs e)
        {
            Reportes ventana = new Reportes();
            ventana.Show();
            this.Hide();
        }
    }
}
