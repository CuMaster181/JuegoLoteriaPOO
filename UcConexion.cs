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

            txtIP.Text = "127.0.0.1";
            txtPuerto.Text = "5000";
        }

        private void bttnConectar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIP.Text))
            {
                MessageBox.Show("Ingresa una IP.");
                return;
            }

            if (!int.TryParse(
                    txtPuerto.Text,
                    out int puerto))
            {
                MessageBox.Show(
                    "Puerto inválido.");
                return;
            }

            Conexion conexion = new Conexion
                {
                    IP = txtIP.Text.Trim(),
                    Puerto = puerto
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
