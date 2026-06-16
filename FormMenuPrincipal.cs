using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuegoLoteriaPOO
{
    public partial class FormMenuPrincipal : Form
    {
        private TipoPartida tipoPartida;
        private Conexion conexion;
        private ConfiguracionPartida configuracion;

        public FormMenuPrincipal()
        {
            InitializeComponent();
        }

        private void MostrarUC(UserControl uc)
        {
            pnlMostrarUc.Visible = true;
            pnlMostrarUc.Controls.Clear();
            pnlMostrarUc.Controls.Add(uc);
            uc.Location = new Point(
                (pnlMostrarUc.Width - uc.Width) / 2,
                (pnlMostrarUc.Height - uc.Height) / 2
            );
            uc.BringToFront();
        }

        private void bttnSolitario_Click(object sender, EventArgs e)
        {
            UcPerfil uc = new UcPerfil(TipoPartida.Solo);
            uc.PerfilCompletado += PerfilCompletado;
            MostrarUC(uc);
        }

        private void bttnMultijugador_Click(object sender, EventArgs e)
        {
            UcPerfil uc = new UcPerfil(TipoPartida.Multijugador);
            uc.PerfilCompletado += PerfilCompletado;
            MostrarUC(uc);
        }

        private void PerfilCompletado(Jugador jugador, TipoPartida tipo)
        {
            tipoPartida = tipo;
            if (tipo == TipoPartida.Solo)
            {
                // En modo solitario preguntar cuántas tablas quiere
                var cfg = MostrarDialogoCantidadTablasSolo();

                FormCrearTabla frm = new FormCrearTabla(jugador, tipoPartida, conexion, cfg);
                frm.Show();
                this.Hide();
            }
            else
            {
                UcConexion uc = new UcConexion(jugador, tipo);
                uc.ConexionCompletada += ConexionCompletada;
                MostrarUC(uc);
            }
        }

        /// <summary>
        /// Muestra un pequeño diálogo para que el jugador solitario elija
        /// cuántas tablas quiere usar (1-4).
        /// </summary>
        private ConfiguracionPartida MostrarDialogoCantidadTablasSolo()
        {
            Form dlg = new Form();
            dlg.Text = "Tablas";
            dlg.Size = new Size(260, 130);
            dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
            dlg.StartPosition = FormStartPosition.CenterParent;
            dlg.MaximizeBox = false;
            dlg.MinimizeBox = false;

            Label lbl = new Label { Text = "¿Cuántas tablas quieres usar? (1-4)", AutoSize = true, Location = new Point(12, 15) };
            NumericUpDown nud = new NumericUpDown { Minimum = 1, Maximum = 4, Value = 1, Location = new Point(12, 40), Width = 60 };
            Button btn = new Button { Text = "Aceptar", Location = new Point(150, 38), DialogResult = DialogResult.OK };

            dlg.Controls.AddRange(new Control[] { lbl, nud, btn });
            dlg.AcceptButton = btn;

            dlg.ShowDialog(this);

            return new ConfiguracionPartida
            {
                NumeroTablas = (int)nud.Value,
                TablasDobles = false,
                FigurasHabilitadas = ConfiguracionPartida.ObtenerTodasLasFiguras()
            };
        }

        private void ConexionCompletada(Jugador jugador, Conexion conexion, TipoPartida tipoPartida, ConfiguracionPartida configuracion)
        {
            FormCrearTabla frm = new FormCrearTabla(jugador, tipoPartida, conexion, configuracion);
            frm.Show();
            Hide();
        }

        public void CerrarUC()
        {
            pnlMostrarUc.Controls.Clear();
            pnlMostrarUc.Visible = false;
        }

        private void FormMenuPrincipal_Load(object sender, EventArgs e)
        {
            pnlMostrarUc.Visible = false;
        }
    }
}