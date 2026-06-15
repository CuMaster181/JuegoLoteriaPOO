using Microsoft.VisualBasic.Devices;
using System.Media;

namespace JuegoLoteriaPOO
{
    public partial class FormPartida : Form
    {
        private int segundosRestantes = 0;
        private bool esperandoDesempate = false;
        private bool fichaSeleccionada = false;
        private bool jugadorActivo = true;
        private bool tablasDobles;
        private bool debeReproducirSonidoInicio = true;
        private System.Windows.Forms.Timer timerDesempate = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timerCuentaAtras = new System.Windows.Forms.Timer();
        private Dictionary<CasillaTabla, PictureBox> casillasVisuales = new Dictionary<CasillaTabla, PictureBox>();
        private List<string> jugadoresDesempate = new();
        private Dictionary<string, int> cartasGanadorasJugadores = new();
        private VerificadorDeVictoria verificador;
        private GestorPartida gestor;
        private List<ResultadoDesempate> resultados = new List<ResultadoDesempate>();
        private ResultadoDesempate? ganador;
        private Jugador jugador;
        private GestorChat gestorChat;
        private GestorMultijugador red;
        private Conexion conexion;
        private ConfiguracionPartida configuracion;
        private UcDesempate? pantallaDesempateActual;
        private TipoPartida tipoPartida;

        public FormPartida(Jugador jugador, TipoPartida tipoPartida, Conexion conexion, ConfiguracionPartida configuracion)
        {
            InitializeComponent();

            gestor = new GestorPartida();
            this.jugador = jugador;
            this.tipoPartida = tipoPartida;
            this.conexion = conexion;
            this.configuracion = configuracion;

            verificador = new VerificadorDeVictoria();
            gestorChat = new GestorChat();
            red = new GestorMultijugador();

            timerDesempate.Interval = 5000;
            timerDesempate.Tick += TimerDesempate_Tick;

            // En el constructor, después de timerDesempate initialization
            timerCuentaAtras.Interval = 1000;
            timerCuentaAtras.Tick += TimerCuentaAtras_Tick;

            red.MensajeRecibido += ProcesarMensajeRed;
            gestorChat.MensajeRecibido += MostrarMensaje;
            timerCartas.Tick -= timerCartas_Tick;
            timerCartas.Tick += timerCartas_Tick;
            pbFicha.MouseDown += pbFicha_MouseDown;
            pbFicha.Click += pbFicha_Click;

            this.FormClosed += (s, e) =>
            {
                try
                {
                    red.Desconectar();
                }
                catch { }

                foreach (Form frm in Application.OpenForms.Cast<Form>().ToList())
                {
                    if (frm is FormMenuPrincipal menu)
                    {
                        menu.Show();
                    }
                    else if (frm != this)
                    {
                        frm.Close();
                    }
                }
            };
        }

        private async void FormPartida_Load(object sender, EventArgs e)
        {
            CargarTabla();

            // Mostrar la carta posterior (placeholder) al cargar la partida
            if (Properties.Resources.CartaPosterior != null)
            {
                pbCartaActual.Image = Properties.Resources.CartaPosterior;
                pbCartaActual.SizeMode = PictureBoxSizeMode.Zoom;
            }

            // Mostrar el panel de ganador oculto inicialmente
            pnlGanador.Visible = false;

            // Mostrar el chat solo en modo multijugador
            bool esMultijugador = tipoPartida == TipoPartida.Multijugador;
            gbChat.Visible = esMultijugador;
            gbChat.Enabled = esMultijugador;

            if (esMultijugador)
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

                        red.Enviar($"CONNECT_CONFIG|{jugador.Nombre}|{(configuracion.TablasDobles ? "true" : "false")}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message);
                }
            }

            // NO arrancar automáticamente: el jugador debe presionar el botón para empezar
            timerCartas.Stop();
            timerCuentaAtras.Stop();
            bttnPausa.Text = "Iniciar";
            bttnPausa.Enabled = true;

            // Al cargar una partida nueva, aseguramos que la bandera esté en true para reproducir sonido en el primer inicio
            debeReproducirSonidoInicio = true;
        }

        private void ReproducirAudio(Carta carta)
        {
            if (carta == null || carta.audio == null)
            {
                return;
            }

            try
            {
                // Asegurar que el stream se lea desde el inicio
                if (carta.audio.CanSeek)
                {
                    carta.audio.Position = 0;
                }

                SoundPlayer sonido = new SoundPlayer
                {
                    Stream = carta.audio
                };

                sonido.Play();
            }
            catch (Exception ex)
            {
                // Detalle técnico para depuración
                System.Diagnostics.Debug.WriteLine($"Error al reproducir audio de '{carta?.Nombre}': {ex}");

                // Mensaje amistoso y en español para el usuario
                MessageBox.Show(
                    $"No se pudo reproducir el audio de la carta \"{carta?.Nombre}\".",
                    "Error de audio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void MostrarCarta(Carta carta)
        {
            pbCartaActual.Image = carta.RutaImagen;

            lblNombreCarta.Text = carta.Nombre;

            ReproducirAudio(carta);
        }

        // Actualizar bttnPausa_Click: al arrancar/pausar sincronizar timerCuentaAtras
        private void bttnPausa_Click(object sender, EventArgs e)
        {
            // Reproducir el sonido de inicio SOLO cuando se va a iniciar (no al pausar)
            if (!timerCartas.Enabled && debeReproducirSonidoInicio)
            {
                try
                {
                    // Properties.Resources.corre_y_se_va_con debe ser UnmanagedMemoryStream (WAV embebido)
                    SoundPlayer sp = new SoundPlayer(Properties.Resources.corre_y_se_va_con);
                    sp.Play();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"No se pudo reproducir sonido de inicio: {ex}");
                }

                // Solo reproducir una vez hasta que se reinicie la ronda
                debeReproducirSonidoInicio = false;
            }

            if (timerCartas.Enabled)
            {
                timerCartas.Stop();
                timerCuentaAtras.Stop();
                bttnPausa.Text = "Reanudar";
                lblProximaCarta.Text = "—";
            }
            else
            {
                // Si es host en multijugador y es la primera vez que inicia, enviar CONFIG
                if (tipoPartida == TipoPartida.Multijugador && conexion != null && conexion.EsHost)
                {
                    string valDobles = configuracion.TablasDobles ? "true" : "false";
                    red.Enviar($"CONFIG|{valDobles}");
                    
                }

                timerCartas.Start();

                // calcular segundos iniciales (redondeo hacia arriba)
                segundosRestantes = Math.Max(1, (timerCartas.Interval + 999) / 1000);
                lblProximaCarta.Text = $"{segundosRestantes}s";
                timerCuentaAtras.Start();

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
                timerCuentaAtras.Stop();
                lblProximaCarta.Text = "—";
                return;
            }

            MostrarCarta(carta);

            AgregarCartaHistorial(carta);

            // No volver a añadir la carta al historial: GestorPartida.SiguienteCarta() ya lo hizo.
            lblContador.Text = $"{gestor.Historial.Count}/54";

            // Reiniciar cuenta atrás para la siguiente carta
            segundosRestantes = Math.Max(1, (timerCartas.Interval + 999) / 1000);
            lblProximaCarta.Text = $"{segundosRestantes}s";
            timerCuentaAtras.Start();

            if (tipoPartida == TipoPartida.Multijugador)
            {
                red.Enviar($"CARTA|{carta.Id}");
            }
        }

        private void TimerDesempate_Tick(object sender, EventArgs e)
        {
            timerDesempate.Stop();

            esperandoDesempate = false;

            if (jugadoresDesempate.Count == 0)
            {
                return;
            }

            if (jugadoresDesempate.Count == 1)
            {
                string ganador = jugadoresDesempate[0];

                if (tipoPartida == TipoPartida.Multijugador)
                {
                    if (conexion != null && conexion.EsHost)
                    {
                        red.Enviar($"GANADOR|{ganador}");
                        GestorPuntaje.RegistrarVictoria(ganador);
                        MostrarGanador(ganador);
                    }
                }
                else
                {
                    GestorPuntaje.RegistrarVictoria(ganador);
                    MostrarGanador(ganador);
                }
            }
            else
            {
                // Hay múltiples reclamantes. Vamos a comprobar sus cartas ganadoras en el historial.
                var reclamantesConIndex = jugadoresDesempate
                    .Select(nombre =>
                    {
                        int cardId = cartasGanadorasJugadores.ContainsKey(nombre) ? cartasGanadorasJugadores[nombre] : -1;
                        int idx = gestor.Historial.FindIndex(c => c.Id == cardId);
                        return new { Nombre = nombre, Index = idx };
                    })
                    .Where(x => x.Index != -1)
                    .ToList();

                if (reclamantesConIndex.Count == 0)
                {
                    MostrarPantallaDesempate();
                    return;
                }

                // Encontrar el índice mínimo (el que ganó primero en el historial)
                int minIndex = reclamantesConIndex.Min(x => x.Index);

                // Filtrar los que empataron en ese índice mínimo
                var empatados = reclamantesConIndex.Where(x => x.Index == minIndex).Select(x => x.Nombre).ToList();

                if (empatados.Count == 1)
                {
                    string ganador = empatados[0];
                    if (tipoPartida == TipoPartida.Multijugador)
                    {
                        if (conexion != null && conexion.EsHost)
                        {
                            red.Enviar($"GANADOR|{ganador}");
                            GestorPuntaje.RegistrarVictoria(ganador);
                            MostrarGanador(ganador);
                        }
                    }
                    else
                    {
                        GestorPuntaje.RegistrarVictoria(ganador);
                        MostrarGanador(ganador);
                    }
                }
                else
                {
                    // Empate real en la misma carta (mismo índice mínimo)
                    jugadoresDesempate = empatados;
                    MostrarPantallaDesempate();
                }
            }
        }

        private void MostrarPantallaDesempate()
        {
            bool esHost = (tipoPartida != TipoPartida.Multijugador || (conexion != null && conexion.EsHost));
            UcDesempate desempate = new UcDesempate(jugadoresDesempate, esHost);

            desempate.Dock = DockStyle.Fill;

            desempate.CartaMayor += () =>
            {
                Controls.Remove(desempate);
                desempate.Dispose();
                pantallaDesempateActual = null;
                Desempate_CartaMayor();
            };

            desempate.ContinuarPartida += () =>
            {
                Controls.Remove(desempate);
                desempate.Dispose();
                pantallaDesempateActual = null;
                Desempate_ContinuarPartida();
            };

            pantallaDesempateActual = desempate;
            Controls.Add(desempate);

            desempate.BringToFront();
        }

        private void IniciarCartaMayor()
        {
            // Limpiar resultados anteriores
            resultados.Clear();

            Random rnd = new Random();

            List<Carta> cartasDisponibles = Carta.cartas.Values.ToList();

            foreach (string nombreJugador in jugadoresDesempate)
            {
                int indice = rnd.Next(cartasDisponibles.Count);
                Carta carta = cartasDisponibles[indice];
                cartasDisponibles.RemoveAt(indice);

                resultados.Add(new ResultadoDesempate
                {
                    Jugador = nombreJugador,
                    Carta = carta
                });
            }

            // Determinar el ganador: quien tenga la carta con mayor Id
            ganador = resultados.OrderByDescending(r => r.Carta.Id).First();

            // Registrar la victoria
            GestorPuntaje.RegistrarVictoria(ganador.Jugador);
        }

        private void Desempate_CartaMayor()
        {
            // Primero llenamos los datos
            IniciarCartaMayor();

            // Ahora creamos la pantalla con los datos ya listos
            UcCartaMayor pantalla = new UcCartaMayor(resultados, ganador!);

            pantalla.Dock = DockStyle.Fill;

            pantalla.Continuar += () =>
            {
                Controls.Remove(pantalla);
                pantalla.Dispose();

                if (tipoPartida == TipoPartida.Multijugador)
                {
                    red.Enviar($"GANADOR|{ganador!.Jugador}");
                }

                MostrarGanador(ganador!.Jugador);
            };

            Controls.Add(pantalla);

            pantalla.BringToFront();
        }

        private void Desempate_ContinuarPartida()
        {
            if (tipoPartida == TipoPartida.Multijugador)
            {
                string lista = string.Join(",", jugadoresDesempate);

                red.Enviar($"CONTINUAR|{lista}");
            }

            ContinuarSoloEmpatados();
        }

        private void ContinuarSoloEmpatados()
        {
            jugadorActivo =
                jugadoresDesempate.Contains(
                    jugador.Nombre);

            if (!jugadorActivo)
            {
                MessageBox.Show(
                    "Has quedado fuera de esta ronda.\nEspera a que termine el desempate.");
            }
        }

        private void ProcesarMensajeRed(string mensaje)
        {
            if (InvokeRequired)
            {
                Invoke(() => ProcesarMensajeRed(mensaje));

                return;
            }

            string[] partes = mensaje.Split('|');

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
                    string ganador = partes[1];

                    if (pantallaDesempateActual != null)
                    {
                        Controls.Remove(pantallaDesempateActual);
                        pantallaDesempateActual.Dispose();
                        pantallaDesempateActual = null;
                    }

                    GestorPuntaje.RegistrarVictoria(ganador);
                    MostrarGanador(ganador);
                    break;

                case "REINICIAR":

                    ReiniciarPartida();

                    break;

                case "LOTERIA":
                    {
                        string nombre = partes[1];
                        int idCarta = int.Parse(partes[2]);

                        if (!esperandoDesempate)
                        {
                            esperandoDesempate = true;

                            jugadoresDesempate.Clear();
                            cartasGanadorasJugadores.Clear();

                            timerDesempate.Start();
                        }

                        if (!jugadoresDesempate.Contains(nombre))
                        {
                            jugadoresDesempate.Add(nombre);
                        }

                        cartasGanadorasJugadores[nombre] = idCarta;
                    }
                    break;

                case "CONTINUAR":
                    if (pantallaDesempateActual != null)
                    {
                        Controls.Remove(pantallaDesempateActual);
                        pantallaDesempateActual.Dispose();
                        pantallaDesempateActual = null;
                    }

                    jugadoresDesempate =
                        partes[1]
                        .Split(',')
                        .ToList();

                    ContinuarSoloEmpatados();

                    break;

                case "PUNTAJE":

                    string nombreJugador = partes[1];

                    GestorPuntaje.RegistrarVictoria(nombreJugador);

                    break;

                case "NUEVARONDA":

                    ReiniciarPartida();

                    break;

                case "CONFIG":

                    bool valorDobles = bool.Parse(partes[1]);
                    tablasDobles = valorDobles;
                    configuracion.TablasDobles = valorDobles;

                    break;

                case "CONNECT_CONFIG":
                    if (conexion.EsHost)
                    {
                        string clientName = partes[1];
                        bool clientDobles = bool.Parse(partes[2]);

                        if (configuracion.TablasDobles != clientDobles)
                        {
                            red.Enviar($"KICK|{clientName}|La configuración de tablas dobles no coincide con la del Host (Host: {(configuracion.TablasDobles ? "Activado" : "Desactivado")}, Cliente: {(clientDobles ? "Activado" : "Desactivado")}). No se te permite entrar a la sala.");
                        }
                    }
                    break;

                case "KICK":
                    {
                        string targetName = partes[1];
                        string motivo = partes[2];
                        if (targetName == jugador.Nombre)
                        {
                            MessageBox.Show(motivo, "Conexión rechazada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }
                    }
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
            if (!jugadorActivo)
            {
                return;
            }
            PictureBox pb = (PictureBox)sender;
            CasillaTabla casilla = (CasillaTabla)pb.Tag;
            casilla.Marcada = true;
            MostrarFicha(pb);
        }

        private void Casilla_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            if (pb.Tag is not CasillaTabla casilla)
                return;

            casilla.Marcada = false;

            pb.Image = casilla.Carta.RutaImagen;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.BackColor = Color.Empty;
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
                    pb.Click += Casilla_Click;

                    tlpTablaPartida.Controls.Add(pb, columna, fila);

                    casillasVisuales.Add(casilla, pb);
                }
            }
        }

        private void btnLoteria_Click(object sender, EventArgs e)
        {
            TipoVictoria? victoria = verificador.VerificarGanador(jugador.Tabla, gestor.Historial);

            if (!jugadorActivo)
            {
                return;
            }

            if (victoria == null)
            {
                MessageBox.Show("No tienes ninguna figura válida.");

                return;
            }

            Carta cartaGanadora = verificador.ObtenerCartaGanadora(jugador.Tabla, gestor.Historial, victoria.Value);

            if (!esperandoDesempate)
            {
                esperandoDesempate = true;

                jugadoresDesempate.Clear();
                cartasGanadorasJugadores.Clear();

                timerDesempate.Start();
                timerCartas.Stop();
            }

            if (!jugadoresDesempate.Contains(jugador.Nombre))
            {
                jugadoresDesempate.Add(jugador.Nombre);
            }

            cartasGanadorasJugadores[jugador.Nombre] = cartaGanadora.Id;

            if (tipoPartida == TipoPartida.Multijugador)
            {
                red.Enviar($"LOTERIA|{jugador.Nombre}|{cartaGanadora.Id}");
            }

            MessageBox.Show("Lotería registrada.\nEsperando posibles empates...");
        }

        private void MostrarGanador(string nombreGanador)
        {
            UcGanador ucGanador = new UcGanador(nombreGanador);

            ucGanador.Dock = DockStyle.Fill;

            // UcGanador expone NuevaPartidaSolicitada desde el boton Continuar
            ucGanador.NuevaPartidaSolicitada += () =>
            {
                ReiniciarDesdeGanador();
            };

            pnlGanador.Controls.Clear();

            pnlGanador.Controls.Add(ucGanador);

            pnlGanador.Enabled = true;
            pnlGanador.Visible = true;

            pnlGanador.BringToFront();
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

            MostrarMensaje(new MensajeChat(jugador.Nombre, texto));

            if (tipoPartida == TipoPartida.Multijugador)
            {
                red.Enviar($"CHAT|{jugador.Nombre}|{texto}");
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

                pb.Controls.Clear();

                // Restaurar imagen de la carta y apariencia original
                pb.Image = casilla.Carta.RutaImagen;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.BackColor = Color.Empty;
            }
        }

        private void LimpiarHistorial()
        {
            flpHistorial.Controls.Clear();

            lblContador.Text = "0/54";

            // Mostrar la imagen posterior cuando se limpia historial
            if (Properties.Resources.CartaPosterior != null)
            {
                pbCartaActual.Image = Properties.Resources.CartaPosterior;
                pbCartaActual.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                pbCartaActual.Image = null;
            }

            lblNombreCarta.Text = "";
        }

        private void ReiniciarPartida()
        {
            jugadorActivo = true;

            esperandoDesempate = false;

            jugadoresDesempate.Clear();

            timerCartas.Stop();
            timerCuentaAtras.Stop();

            gestor = new GestorPartida();

            LimpiarHistorial();

            ReiniciarTabla();

            bttnPausa.Text = "Iniciar";

            bttnPausa.Enabled = true;

            fichaSeleccionada = false;

            pbFicha.BorderStyle = BorderStyle.None;

            lblProximaCarta.Text = "—";
            segundosRestantes = 0;

            // Al reiniciar la ronda, permitir que el siguiente inicio reproduzca el sonido
            debeReproducirSonidoInicio = true;

            // Asegurar que la imagen placeholder vuelva a mostrarse tras reiniciar
            if (Properties.Resources.CartaPosterior != null)
            {
                pbCartaActual.Image = Properties.Resources.CartaPosterior;
                pbCartaActual.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                pbCartaActual.Image = null;
            }
        }

        private void ReiniciarDesdeGanador()
        {
            // eliminar y liberar los UcGanador contenidos en pnlGanador
            foreach (Control c in pnlGanador.Controls.OfType<UcGanador>().ToList())
            {
                pnlGanador.Controls.Remove(c);
                c.Dispose();
            }

            // ocultar y deshabilitar el panel para restaurar el estado original
            pnlGanador.Visible = false;
            pnlGanador.Enabled = false;

            // En multijugador: enviar la señal de reinicio a los demás y reiniciar localmente.
            if (tipoPartida == TipoPartida.Multijugador)
            {
                try
                {
                    red.Enviar("REINICIAR");
                }
                catch
                {
                    // Ignorar errores de red aquí; al menos reiniciamos localmente
                }

                ReiniciarPartida();
            }
            else
            {
                // En partida local reiniciamos directamente
                ReiniciarPartida();
            }
        }

        private void pbFicha_Click(object sender, EventArgs e)
        {
            fichaSeleccionada = !fichaSeleccionada;

            pbFicha.BorderStyle =
                fichaSeleccionada
                ? BorderStyle.Fixed3D
                : BorderStyle.None;
        }

        private void Casilla_Click(object sender, EventArgs e)
        {
            if (!jugadorActivo)
            {
                return;
            }
            if (!fichaSeleccionada)
                return;

            PictureBox pb = (PictureBox)sender;

            CasillaTabla casilla = (CasillaTabla)pb.Tag;

            casilla.Marcada = true;

            MostrarFicha(pb);

            fichaSeleccionada = false;

            pbFicha.BorderStyle = BorderStyle.None;
        }

        private void MostrarFicha(PictureBox pb)
        {
            if (pb == null)
                return;

            if (pb.Tag is not CasillaTabla casillaTabla)
                return;

            // Mostrar la ficha encima de la imagen de la carta
            pb.Image = Properties.Resources.Ficha;
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.BackColor = Color.Transparent;

            casillaTabla.Marcada = true;
        }

        private void btnAumento_Click(object sender, EventArgs e)
        {
            // Reducir el intervalo para AUMENTAR la velocidad (más rápido)
            const int paso = 250;   // ms
            const int minimo = 250; // ms mínimo permitido

            int nuevo = Math.Max(minimo, timerCartas.Interval - paso);

            // Solo aplicar si cambia
            if (nuevo != timerCartas.Interval)
            {
                timerCartas.Interval = nuevo;
                // recalcular segundos restantes según la nueva velocidad
                segundosRestantes = Math.Max(1, (timerCartas.Interval + 999) / 1000);
                lblProximaCarta.Text = timerCartas.Enabled ? $"{segundosRestantes}s" : $"{segundosRestantes}s";
            }
        }

        private void btnResta_Click(object sender, EventArgs e)
        {
            // Aumentar el intervalo para DISMINUIR la velocidad (más lento)
            const int paso = 250;    // ms
            const int maximo = 10000; // ms máximo permitido

            int nuevo = Math.Min(maximo, timerCartas.Interval + paso);

            // Solo aplicar si cambia
            if (nuevo != timerCartas.Interval)
            {
                timerCartas.Interval = nuevo;
                segundosRestantes = Math.Max(1, (timerCartas.Interval + 999) / 1000);
                lblProximaCarta.Text = timerCartas.Enabled ? $"{segundosRestantes}s" : $"{segundosRestantes}s";
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            Carta carta = gestor.SiguienteCarta();
            if (carta != null)
            {
                MostrarCarta(carta);
                AgregarCartaHistorial(carta);
                lblContador.Text = $"{gestor.Historial.Count}/54";
                if (tipoPartida == TipoPartida.Multijugador)
                {
                    red.Enviar($"CARTA|{carta.Id}");
                }
            }
            segundosRestantes = Math.Max(1, (timerCartas.Interval + 999) / 1000);
            lblProximaCarta.Text = $"{segundosRestantes}s";
        }

        private void TimerCuentaAtras_Tick(object? sender, EventArgs e)
        {
            if (segundosRestantes > 0)
            {
                segundosRestantes--;
                lblProximaCarta.Text = $"{segundosRestantes}s";
            }
            else
            {
                // Mostrar 0s mientras esperamos el siguiente tick real
                lblProximaCarta.Text = "0s";
            }
        }
    }
}
