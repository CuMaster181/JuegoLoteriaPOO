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
    public partial class UcPerfil : UserControl
    {
        private TipoPartida tipoPartida;

        public event Action<Jugador, TipoPartida>? PerfilCompletado;
        public UcPerfil(TipoPartida tipoPartida)
        {
            InitializeComponent();

            this.tipoPartida = tipoPartida;
        }
        private void bttnContinuar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show(
                    "Ingresa un nombre.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            Jugador jugador =
                new Jugador(txtNombre.Text.Trim());

            PerfilCompletado?.Invoke(jugador,tipoPartida);
        }

        private void bttnCancelar_Click(object sender, EventArgs e)
        {
            if (FindForm() is FormMenuPrincipal menu)
            {
                menu.CerrarUC();
            }
        }
    }
}
