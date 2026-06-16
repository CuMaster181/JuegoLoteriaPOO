using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
    public partial class UcCartaMayor : UserControl
    {
        public event Action? Continuar;

        public UcCartaMayor(List<ResultadoDesempate> resultados, ResultadoDesempate ganador, bool esHost = true)
        {
            InitializeComponent();

            dgvResultados.Rows.Clear();

            // Agregar columnas si no existen
            if (dgvResultados.Columns.Count == 0)
            {
                dgvResultados.Columns.Add("Jugador", "Jugador");
                dgvResultados.Columns.Add("Carta", "Carta");
                dgvResultados.Columns.Add("Id", "Id");
            }

            foreach (ResultadoDesempate r in resultados)
            {
                dgvResultados.Rows.Add(
                    r.Jugador,
                    r.Carta.Nombre,
                    r.Carta.Id);
            }

            lblGanador.Text = ganador.Jugador;

            pbCartaGanadora.Image = ganador.Carta.RutaImagen;
            pbCartaGanadora.SizeMode = PictureBoxSizeMode.Zoom;
            btnContinuar.Enabled = esHost;
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            Continuar?.Invoke();
        }
    }
}
