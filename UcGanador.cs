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
            NuevaPartidaSolicitada?.Invoke();
        }
    }
}
