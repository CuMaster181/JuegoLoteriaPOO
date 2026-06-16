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
    public partial class UcGanador : UserControl
    {

        public event Action? NuevaPartidaSolicitada;
        public event Action? Continuar;
        public UcGanador(string ganador)
        {
            InitializeComponent();
            lblGanador.Text = $"{ganador} ganó la partida";
            ActualizarRanking();
        }

        private void ActualizarRanking()
        {
            dgvRanking.Rows.Clear();

            foreach (PuntajeJugador jugador in GestorPuntaje.Ranking)
            {
                dgvRanking.Rows.Add(jugador.Nombre, jugador.Puntos);
            }
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            btnContinuar.Enabled = false;
            btnContinuar.Text = "Listo";
            Continuar?.Invoke();
            NuevaPartidaSolicitada?.Invoke();
        }

        public void ActualizarEstadoListos(IEnumerable<string> jugadoresListos, IEnumerable<string> jugadoresEsperados)
        {
            int listos = jugadoresListos.Distinct(StringComparer.OrdinalIgnoreCase).Count();
            int esperados = jugadoresEsperados.Distinct(StringComparer.OrdinalIgnoreCase).Count();
            lblEstadoListos.Text = $"Listos: {listos}/{esperados}";
        }
    }
}
