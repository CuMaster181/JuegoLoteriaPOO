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
    public partial class UcConexion : UserControl
    {
        private Jugador jugador;
        private TipoPartida tipoPartida;
        public event Action<Jugador, Conexion, TipoPartida>? ConexionCompletada;

        public UcConexion(Jugador jugador, TipoPartida tipoPartida)
        {
            InitializeComponent();
            this.jugador = jugador;
            this.tipoPartida = tipoPartida;

            txtPuerto.Text = "5000";
        }

        private void bttnConectar_Click(object sender, EventArgs e)
        {
            Conexion conexion =
        new Conexion
        {
            IP = txtIP.Text.Trim(),
            Puerto = int.Parse(
                txtPuerto.Text),

            EsHost = rbHost.Checked
        };

            ConexionCompletada?.Invoke(
                jugador,
                conexion,
                tipoPartida);
        }

        private void bttncancelar_Click(object sender, EventArgs e)
        {

            if (FindForm() is FormMenuPrincipal menu)
            {
                menu.CerrarUC();
            }
        }
    }
}
