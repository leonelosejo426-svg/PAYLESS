using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
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
    public partial class AperturaCaja : Form
    {
        private AperturaCajaDAO aperturaCajaDAO;

        // Usuario que inició sesión
        private int idUsuario;
        private string nombreUsuario;

        public AperturaCaja(int idUsuario, string nombreUsuario)
        {
            InitializeComponent();
          
            aperturaCajaDAO = new AperturaCajaDAO();

            dtpFecha.Value = DateTime.Now;
            dtpHora.Value = DateTime.Now;


        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }

        private void AperturaCaja_Load(object sender, EventArgs e)
        {
            // 1. Tus asignaciones automáticas de interfaz
            dtpFecha.Value = DateTime.Now;
            dtpHora.Value = DateTime.Now;
            lblUsuario.Text = ClaseSesion.UsuarioActual;

            // 2. CANDADO DE PROTECCIÓN: Si ya hay una caja abierta en el sistema...
            if (ClaseSesion.TieneCajaActiva)
            {
                // Deshabilitamos los controles para que no puedan modificar nada
                txtMontoInicial.Enabled = false;
                txtCambioDolar.Enabled = false;
                btnAperturarCaja.Enabled = false; // El botón se vuelve gris y no es clickeable
                btnAperturarCaja.Text = "Caja Ya Abierta";

                // Mostramos el aviso de advertencia en pantalla
                MessageBox.Show("Atención: Ya existe una caja activa en el sistema.\n" +
                                "No se permiten nuevas aperturas ni modificaciones hasta que se realice el cierre correspondiente.",
                                "Módulo Protegido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Si está libre, aseguramos que los campos estén disponibles para rellenar
                txtMontoInicial.Enabled = true;
                txtCambioDolar.Enabled = true;
                btnAperturarCaja.Enabled = true;
                btnAperturarCaja.Text = "Aperturar Caja";
            }
        }   

        private void btnAperturarCaja_Click(object sender, EventArgs e)
        {
            try
            {
                decimal montoInicial;
                decimal cambioDolar;

                // 1. Validaciones de montos
                if (!decimal.TryParse(txtMontoInicial.Text.Trim(), out montoInicial))
                {
                    MessageBox.Show("Ingrese un monto inicial válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMontoInicial.Focus();
                    return;
                }

                if (montoInicial < 0)
                {
                    MessageBox.Show("El monto inicial no puede ser negativo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMontoInicial.Focus();
                    return;
                }

                if (!decimal.TryParse(txtCambioDolar.Text.Trim(), out cambioDolar))
                {
                    MessageBox.Show("Ingrese un cambio de dólar válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCambioDolar.Focus();
                    return;
                }

                if (cambioDolar <= 0)
                {
                    MessageBox.Show("El cambio de dólar debe ser mayor que cero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCambioDolar.Focus();
                    return;
                }

                // 2. Candado en base de datos (Tu método del DAO)
                if (aperturaCajaDAO.ExisteCajaAbierta())
                {
                    MessageBox.Show("Ya existe una caja abierta en el sistema.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // USUARIO AUTOMÁTICO DEL LOGIN
                int idUsuario = ClaseSesion.IdUsuario;

                // 3. Ejecución del registro en PostgreSQL
                bool resultado = aperturaCajaDAO.GuardarApertura(montoInicial, cambioDolar, idUsuario);

                if (resultado)
                {
                    // ¡IMPORTANTE!: Actualizamos la variable de sesión en caliente
                    ClaseSesion.TieneCajaActiva = true;

                    MessageBox.Show("La caja se aperturó correctamente.", "Apertura de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Redirección y enlace de ciclo de vida a la pantalla operativa de Caja (Ventas)
                    Caja frmCaja = new Caja();
                    frmCaja.FormClosed += (s, args) => this.Close(); // Evita que la app se quede colgada en segundo plano
                    frmCaja.Show();

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("No se pudo aperturar la caja.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al aperturar la caja:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtMontoInicial.Clear();
            txtCambioDolar.Clear();

            dtpFecha.Value = DateTime.Now;
            dtpHora.Value = DateTime.Now;

            txtMontoInicial.Focus();
        }
    }
}
