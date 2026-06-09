using System.Media;

namespace JuegoLoteriaPOO
{
    public partial class FormPartida : Form
    {
        private Dictionary<CasillaTabla, PictureBox> casillasVisuales = new Dictionary<CasillaTabla, PictureBox>();
        private VerificadorDeVictoria verificador;
        private GestorPartida gestor;
        private Jugador jugador;
        private GestorChat gestorChat;
        private GestorMultijugador red;
        private Conexion conexion;
        private TipoPartida tipoPartida;

        public FormPartida(Jugador jugador, TipoPartida tipoPartida, Conexion conexion)
        {
            InitializeComponent();

            gestor = new GestorPartida();
            this.jugador = jugador;
            this.tipoPartida = tipoPartida;
            this.conexion = conexion;

            verificador = new VerificadorDeVictoria();
            gestorChat = new GestorChat();
            red = new GestorMultijugador();

            red.MensajeRecibido += ProcesarMensajeRed;
            gestorChat.MensajeRecibido += MostrarMensaje;
            timerCartas.Tick -= timerCartas_Tick;
            timerCartas.Tick += timerCartas_Tick;
            pbFicha.MouseDown += pbFicha_MouseDown;
        }

        private async void FormPartida_Load(object sender, EventArgs e)
        {
            CargarTabla();

            if (tipoPartida ==
        TipoPartida.Multijugador)
            {
                try
                {
                    if (conexion.EsHost)
                    {
                        MessageBox.Show(
                            "Esperando jugadores...");

                        _ = red.IniciarServidor(
                            conexion.Puerto);
                    }
                    else
                    {
                        await red.Conectar(
                            conexion.IP,
                            conexion.Puerto);

                        MessageBox.Show(
                            "Conectado al host.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message);
                }
            }


            timerCartas.Start();
        }

        private void ReproducirAudio(Carta carta)
        {
            try
            {
                SoundPlayer sonido = new SoundPlayer(carta.audio);
                sonido.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            if (tipoPartida == TipoPartida.Multijugador && !conexion.EsHost)
            {
                return;
            }

            Carta carta = gestor.SiguienteCarta();

            if (carta == null)
            {
                timerCartas.Stop();
                return;
            }

            MostrarCarta(carta);

            AgregarCartaHistorial(carta);

            lblContador.Text =
                $"{gestor.Historial.Count}/54";
            gestor.Historial.Add(carta);

            if (tipoPartida == TipoPartida.Multijugador)
            {
                red.Enviar($"CARTA|{carta.Id}");
            }
        }

        private void ProcesarMensajeRed(string mensaje)
        {
            if (InvokeRequired)
            {
                Invoke(() =>
                    ProcesarMensajeRed(mensaje));

                return;
            }

            string[] partes =
                mensaje.Split('|');

            switch (partes[0])
            {
                case "CHAT":

                    string usuario = partes[1];

                    string texto = partes[2];

                    MostrarMensaje(new MensajeChat(usuario, texto));

                    break;

                case "CARTA":

                    int id = int.Parse(partes[1]);

                    Carta carta = Carta.cartas[id];
                    gestor.Historial.Add(carta);
                    MostrarCarta(carta);

                    AgregarCartaHistorial(carta);

                    lblContador.Text = $"{gestor.Historial.Count}/54";

                    break;

                case "GANADOR":
                    MostrarGanador(partes[1]);
                    break;

                case "REINICIAR":

                    ReiniciarPartida();

                    break;
            }
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
            pb.Image = casilla.Carta.RutaImagen;
        }

        private void CargarTabla()
        {
            tlpTablaPartida.Controls.Clear();

            casillasVisuales.Clear();

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

                    casillasVisuales.Add(casilla, pb);
                }
            }
        }

        private void btnLoteria_Click(object sender, EventArgs e)
        {
            TipoVictoria? victoria = verificador.VerificarGanador(jugador.Tabla, gestor.Historial);

            if (victoria != null)
            {
                timerCartas.Stop();

                GestorPuntaje.RegistrarVictoria(
                    jugador.Nombre);

                if (tipoPartida == TipoPartida.Multijugador)
                {
                    red.Enviar($"GANADOR|{jugador.Nombre}");
                }

                MostrarGanador(
                    jugador.Nombre);
            }
            else
            {
                MessageBox.Show(
                    "No tienes ninguna figura válida.");
            }
        }

        private void MostrarGanador(string nombreGanador)
        {
            UcGanador ganador = new UcGanador(nombreGanador);

            ganador.Dock = DockStyle.Fill;

            ganador.NuevaPartidaSolicitada += ReiniciarDesdeGanador;

            Controls.Add(ganador);

            ganador.BringToFront();
        }

        private void pbFicha_MouseDown(object sender, MouseEventArgs e)
        {
            pbFicha.DoDragDrop("FICHA", DragDropEffects.Copy);
        }

        private void AgregarCartaHistorial(Carta carta)
        {
            PictureBox pb = new PictureBox();

            pb.Width = 60;
            pb.Height = 90;
            pb.Image = carta.RutaImagen;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.BorderStyle = BorderStyle.FixedSingle;
            ToolTip tip = new ToolTip();
            tip.SetToolTip(pb, carta.Nombre);
            flpHistorial.Controls.Add(pb);
        }

        private void MostrarMensaje(MensajeChat mensaje)
        {
            lstChat.Items.Add(
                $"[{mensaje.Fecha:HH:mm}] " +
                $"{mensaje.Usuario}: " +
                $"{mensaje.Texto}");

            lstChat.TopIndex = lstChat.Items.Count - 1;
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMensaje.Text))
            {
                return;
            }

            string texto = txtMensaje.Text;

            MostrarMensaje(
                new MensajeChat(
                    jugador.Nombre,
                    texto));

            if (tipoPartida == TipoPartida.Multijugador)
            {
                red.Enviar(
                    $"CHAT|{jugador.Nombre}|{texto}");
            }

            txtMensaje.Clear();
        }

        private void txtMensaje_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEnviar.PerformClick();

                e.SuppressKeyPress = true;
            }
        }

        private void ReiniciarTabla()
        {
            foreach (var item in casillasVisuales)
            {
                CasillaTabla casilla = item.Key;
                PictureBox pb = item.Value;

                casilla.Marcada = false;

                pb.Image = casilla.Carta.RutaImagen;
            }
        }

        private void LimpiarHistorial()
        {
            flpHistorial.Controls.Clear();

            lblContador.Text = "0/54";

            pbCartaActual.Image = null;

            lblNombreCarta.Text = "";
        }

        private void ReiniciarPartida()
        {
            timerCartas.Stop();

            gestor = new GestorPartida();

            flpHistorial.Controls.Clear();

            lblContador.Text = "0/54";

            pbCartaActual.Image = null;

            lblNombreCarta.Text = "";

            foreach (CasillaTabla casilla in jugador.Tabla.Casillas)
            {
                if (casilla != null)
                {
                    casilla.Marcada = false;
                }
            }

            foreach (var item in casillasVisuales)
            {
                item.Value.Image =
                    item.Key.Carta.RutaImagen;
            }

            timerCartas.Start();
        }

        private void ReiniciarDesdeGanador()
        {
            foreach (Control c in Controls.OfType<UcGanador>().ToList())
            {
                Controls.Remove(c);
                c.Dispose();
            }

            if (tipoPartida == TipoPartida.Multijugador)
            {
                red.Enviar("REINICIAR");
            }
            else
            {
                ReiniciarPartida();
            }
        }
    }
}
