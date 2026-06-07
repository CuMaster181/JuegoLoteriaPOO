using System.Media;

namespace JuegoLoteriaPOO
{
    public partial class FormPartida : Form
    {
        private VerificadorDeVictoria verificador;
        private GestorPartida gestor;
        private Jugador jugador;
        public FormPartida(Jugador jugador)
        {
            InitializeComponent();
            gestor = new GestorPartida();
            this.jugador = jugador;
            verificador = new VerificadorDeVictoria();
            timerCartas.Tick += timerCartas_Tick;
            pbFicha.MouseDown += pbFicha_MouseDown;
        }

        private void FormPartida_Load(object sender, EventArgs e)
        {
            CargarTabla();
            timerCartas.Start();
        }

        private void ReproducirAudio(Carta carta)
        {
            try
            {
                SoundPlayer sonido = new SoundPlayer(carta.audio);
                sonido.Play();
            }
            catch
            {
            }
        }

        private void MostrarCarta(Carta carta)
        {
            pbCartaActual.Image = carta.RutaImagen;

            lblNombreCarta.Text = carta.Nombre;

            ReproducirAudio(carta);
        }


        private void bttnPausa_Click(object sender, EventArgs e)
        {
            if (timerCartas.Enabled)
            {
                timerCartas.Stop();

                bttnPausa.Text = "Reanudar";
            }
            else
            {
                timerCartas.Start();

                bttnPausa.Text = "Pausar";
            }
        }

        private void timerCartas_Tick(object sender, EventArgs e)
        {
            Carta carta = gestor.SiguienteCarta();

            if (carta == null)
            {
                timerCartas.Stop();
                MessageBox.Show("No quedan cartas.");
                return;
            }

            MostrarCarta(carta);
        }

        private void cbVelocidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbVelocidad.SelectedIndex)
            {
                case 0:
                    timerCartas.Interval = 1500;
                    break;

                case 1:
                    timerCartas.Interval = 3000;
                    break;

                case 2:
                    timerCartas.Interval = 5000;
                    break;
            }
        }

        private void Casilla_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string)))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Casilla_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            CasillaTabla casilla = (CasillaTabla)pb.Tag;
            casilla.Marcada = true;
            pb.Image = Properties.Resources.Ficha;
        }

        private void Casilla_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            CasillaTabla casilla = (CasillaTabla)pb.Tag;

            casilla.Marcada = false;

            pb.BackColor = Color.Transparent;
        }

        private void CargarTabla()
        {
            tlpTablaPartida.Controls.Clear();

            for (int fila = 0; fila < 5; fila++)
            {
                for (int columna = 0; columna < 5; columna++)
                {
                    CasillaTabla casilla = jugador.Tabla.ObtenerCasilla(fila, columna);

                    PictureBox pb = new PictureBox();

                    pb.Dock = DockStyle.Fill;

                    pb.BorderStyle = BorderStyle.FixedSingle;

                    pb.SizeMode = PictureBoxSizeMode.StretchImage;

                    pb.Image = casilla.Carta.RutaImagen;

                    pb.Tag = casilla;

                    pb.AllowDrop = true;

                    pb.DragEnter += Casilla_DragEnter;

                    pb.DragDrop += Casilla_DragDrop;

                    pb.MouseDoubleClick += Casilla_MouseDoubleClick;

                    tlpTablaPartida.Controls.Add(pb, columna, fila);
                }
            }
        }

        private void ntnLoteria_Click(object sender, EventArgs e)
        {
            bool gano = verificador.VerificarGanador(jugador.Tabla, gestor.Historial, TipoVictoria.TablaCompleta);

            if (gano)
            {
                MessageBox.Show("¡LOTERÍA!");

                timerCartas.Stop();
            }
            else
            {
                MessageBox.Show("No cumples las condiciones.");
            }
        }

        private void pbFicha_MouseDown(object sender, MouseEventArgs e)
        {

            pbFicha.DoDragDrop("FICHA", DragDropEffects.Copy);
        }
    }
}
