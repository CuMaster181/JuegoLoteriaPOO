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
                FormCrearTabla frm =
                    new FormCrearTabla(jugador, tipoPartida, conexion, configuracion);

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
