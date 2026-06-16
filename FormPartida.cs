using Microsoft.VisualBasic.Devices;
using System.Media;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

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

        // casillasVisuales ahora agrupa todas las tablas: CasillaTabla -> PictureBox
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
                try { red.Desconectar(); } catch { }

                foreach (Form frm in Application.OpenForms.Cast<Form>().ToList())
                {
                    if (frm is FormMenuPrincipal menu)
                        menu.Show();
                    else if (frm != this)
                        frm.Close();
                }
            };
        }

        private async void FormPartida_Load(object sender, EventArgs e)
        {
            CargarTodasLasTablas();

            if (Properties.Resources.CartaPosterior != null)
            {
                pbCartaActual.Image = Properties.Resources.CartaPosterior;
                pbCartaActual.SizeMode = PictureBoxSizeMode.Zoom;
            }

            pnlGanador.Visible = false;

            bool esMultijugador = tipoPartida == TipoPartida.Multijugador;
            gbChat.Visible = esMultijugador;
            gbChat.Enabled = esMultijugador;

            if (esMultijugador)
            {
                try
                {
                    if (conexion.EsHost)
                    {
                        MessageBox.Show("Esperando jugadores...");
                        _ = red.IniciarServidor(conexion.Puerto);
                    }
                    else
                    {
                        await red.Conectar(conexion.IP, conexion.Puerto);
                        MessageBox.Show("Conectado al host.");
                        red.Enviar($"CONNECT_CONFIG|{jugador.Nombre}|{(configuracion.TablasDobles ? "true" : "false")}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            timerCartas.Stop();
            timerCuentaAtras.Stop();
            bttnPausa.Text = "Iniciar";
            bttnPausa.Enabled = true;
            debeReproducirSonidoInicio = true;
        }

        // ─── Carga de múltiples tablas ────────────────────────────────────────

        private void CargarTodasLasTablas()
        {
            flpTablas.Controls.Clear();
            casillasVisuales.Clear();

            var tablas = jugador.Tablas;
            if (tablas == null || tablas.Count == 0)
            {
                // fallback compatibilidad
                if (jugador.Tabla != null)
                    tablas = new List<TablaJugador> { jugador.Tabla };
                else
                    return;
            }

            int numTablas = tablas.Count;

            // Tamaño de cada celda según cantidad de tablas
            int celdaAncho = numTablas == 1 ? 80 : numTablas == 2 ? 60 : 48;
            int celdaAlto = numTablas == 1 ? 100 : numTablas == 2 ? 75 : 60;

            foreach (var tabla in tablas)
            {
                Panel pnlTabla = new Panel();
                pnlTabla.BorderStyle = BorderStyle.FixedSingle;
                pnlTabla.Margin = new Padding(6);
                pnlTabla.AutoSize = true;
                pnlTabla.AutoSizeMode = AutoSizeMode.GrowAndShrink;

                TableLayoutPanel tlp = new TableLayoutPanel();
                tlp.ColumnCount = 5;
                tlp.RowCount = 5;
                tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                tlp.AutoSize = true;

                for (int i = 0; i < 5; i++)
                {
                    tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, celdaAncho));
                    tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, celdaAlto));
                }

                for (int fila = 0; fila < 5; fila++)
                {
                    for (int columna = 0; columna < 5; columna++)
                    {
                        CasillaTabla casilla = tabla.ObtenerCasilla(fila, columna);

                        PictureBox pb = new PictureBox();
                        pb.Width = celdaAncho;
                        pb.Height = celdaAlto;
                        pb.BorderStyle = BorderStyle.FixedSingle;
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                        pb.Image = casilla.Carta.RutaImagen;
                        pb.Tag = casilla;
                        pb.AllowDrop = true;

                        pb.DragEnter += Casilla_DragEnter;
                        pb.DragDrop += Casilla_DragDrop;
                        pb.MouseDoubleClick += Casilla_MouseDoubleClick;
                        pb.Click += Casilla_Click;

                        tlp.Controls.Add(pb, columna, fila);
                        casillasVisuales[casilla] = pb;
                    }
                }

                pnlTabla.Controls.Add(tlp);
                flpTablas.Controls.Add(pnlTabla);
            }
        }

        // ─── Audio ────────────────────────────────────────────────────────────

        private void ReproducirAudio(Carta carta)
        {
            if (carta == null || carta.audio == null) return;
            try
            {
                if (carta.audio.CanSeek) carta.audio.Position = 0;
                SoundPlayer sonido = new SoundPlayer { Stream = carta.audio };
                sonido.Play();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al reproducir audio de '{carta?.Nombre}': {ex}");
                MessageBox.Show($"No se pudo reproducir el audio de la carta \"{carta?.Nombre}\".",
                    "Error de audio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MostrarCarta(Carta carta)
        {
            pbCartaActual.Image = carta.RutaImagen;
            lblNombreCarta.Text = carta.Nombre;
            ReproducirAudio(carta);
        }

        // ─── Pausa / Inicio ───────────────────────────────────────────────────

        private void bttnPausa_Click(object sender, EventArgs e)
        {
            if (!timerCartas.Enabled && debeReproducirSonidoInicio)
            {
                try
                {
                    SoundPlayer sp = new SoundPlayer(Properties.Resources.corre_y_se_va_con);
                    sp.Play();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"No se pudo reproducir sonido de inicio: {ex}");
                }
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
                if (tipoPartida == TipoPartida.Multijugador && conexion != null && conexion.EsHost)
                {
                    string valDobles = configuracion.TablasDobles ? "true" : "false";
                    string valNumTablas = configuracion.NumeroTablas.ToString();
                    string valFiguras = configuracion.SerializarFiguras();
                    red.Enviar($"CONFIG|{valDobles}|{valNumTablas}|{valFiguras}");
                }

                timerCartas.Start();
                segundosRestantes = Math.Max(1, (timerCartas.Interval + 999) / 1000);
                lblProximaCarta.Text = $"{segundosRestantes}s";
                timerCuentaAtras.Start();
                bttnPausa.Text = "Pausar";
            }
        }

        private void timerCartas_Tick(object sender, EventArgs e)
        {
            if (tipoPartida == TipoPartida.Multijugador && !conexion.EsHost) return;

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
            lblContador.Text = $"{gestor.Historial.Count}/54";

            segundosRestantes = Math.Max(1, (timerCartas.Interval + 999) / 1000);
            lblProximaCarta.Text = $"{segundosRestantes}s";
            timerCuentaAtras.Start();

            if (tipoPartida == TipoPartida.Multijugador)
                red.Enviar($"CARTA|{carta.Id}");
        }

        // ─── Desempate ────────────────────────────────────────────────────────

        private void TimerDesempate_Tick(object sender, EventArgs e)
        {
            timerDesempate.Stop();
            esperandoDesempate = false;

            if (jugadoresDesempate.Count == 0) return;

            if (jugadoresDesempate.Count == 1)
            {
                string ganadorNombre = jugadoresDesempate[0];
                if (conexion != null && conexion.EsHost)
                {
                    red.Enviar($"GANADOR|{ganadorNombre}");
                    GestorPuntaje.RegistrarVictoria(ganadorNombre);
                    MostrarGanador(ganadorNombre);
                }
            }
            else
            {
                var reclamantesConIndex = jugadoresDesempate
                    .Select(nombre =>
                    {
                        int cardId = cartasGanadorasJugadores.ContainsKey(nombre) ? cartasGanadorasJugadores[nombre] : -1;
                        int idx = gestor.Historial.FindIndex(c => c.Id == cardId);
                        return new { Nombre = nombre, Index = idx };
                    })
                    .Where(x => x.Index != -1)
                    .ToList();

                if (reclamantesConIndex.Count == 0) { MostrarPantallaDesempate(); return; }

                int minIndex = reclamantesConIndex.Min(x => x.Index);
                var empatados = reclamantesConIndex.Where(x => x.Index == minIndex).Select(x => x.Nombre).ToList();

                if (empatados.Count == 1)
                {
                    string ganadorNombre = empatados[0];
                    if (conexion != null && conexion.EsHost)
                    {
                        red.Enviar($"GANADOR|{ganadorNombre}");
                        GestorPuntaje.RegistrarVictoria(ganadorNombre);
                        MostrarGanador(ganadorNombre);
                    }
                }
                else
                {
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

            if (pantallaDesempateActual != null)
            {
                Controls.Remove(pantallaDesempateActual);
                pantallaDesempateActual.Dispose();
            }

            pantallaDesempateActual = desempate;
            Controls.Add(desempate);
            desempate.BringToFront();
        }

        private void IniciarCartaMayor()
        {
            resultados.Clear();
            Random rnd = new Random();
            List<Carta> cartasDisponibles = Carta.cartas.Values.ToList();

            foreach (string nombreJugador in jugadoresDesempate)
            {
                int indice = rnd.Next(cartasDisponibles.Count);
                Carta carta = cartasDisponibles[indice];
                cartasDisponibles.RemoveAt(indice);
                resultados.Add(new ResultadoDesempate { Jugador = nombreJugador, Carta = carta });
            }

            ganador = resultados.OrderByDescending(r => r.Carta.Id).First();
            GestorPuntaje.RegistrarVictoria(ganador.Jugador);
        }

        private void Desempate_CartaMayor()
        {
            IniciarCartaMayor();

            UcCartaMayor pantalla = new UcCartaMayor(resultados, ganador!);
            pantalla.Dock = DockStyle.Fill;

            pantalla.Continuar += () =>
            {
                Controls.Remove(pantalla);
                pantalla.Dispose();

                if (tipoPartida == TipoPartida.Multijugador)
                    red.Enviar($"GANADOR|{ganador!.Jugador}");

                MostrarGanador(ganador!.Jugador);
            };

            Controls.Add(pantalla);
            pantalla.BringToFront();
        }

        private void Desempate_ContinuarPartida()
        {
            if (tipoPartida == TipoPartida.Multijugador)
                red.Enviar($"CONTINUAR|{string.Join(",", jugadoresDesempate)}");

            ContinuarSoloEmpatados();
        }

        private void ContinuarSoloEmpatados()
        {
            jugadorActivo = jugadoresDesempate.Contains(jugador.Nombre);
            if (!jugadorActivo)
                MessageBox.Show("Has quedado fuera de esta ronda.\nEspera a que termine el desempate.");
        }

        // ─── Mensajes de red ──────────────────────────────────────────────────

        private void ProcesarMensajeRed(string mensaje)
        {
            if (InvokeRequired) { Invoke(() => ProcesarMensajeRed(mensaje)); return; }

            string[] partes = mensaje.Split('|');

            switch (partes[0])
            {
                case "CHAT":
                    MostrarMensaje(new MensajeChat(partes[1], partes[2]));
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
                    string ganadorMensaje = partes[1];
                    if (pantallaDesempateActual != null)
                    {
                        Controls.Remove(pantallaDesempateActual);
                        pantallaDesempateActual.Dispose();
                        pantallaDesempateActual = null;
                    }
                    GestorPuntaje.RegistrarVictoria(ganadorMensaje);
                    MostrarGanador(ganadorMensaje);
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
                            // Solo el Host maneja el timer de desempate
                            if (conexion != null && conexion.EsHost)
                                timerDesempate.Start();
                        }
                        if (!jugadoresDesempate.Contains(nombre))
                            jugadoresDesempate.Add(nombre);
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
                    jugadoresDesempate = partes[1].Split(',').ToList();
                    ContinuarSoloEmpatados();
                    break;

                case "PUNTAJE":
                    GestorPuntaje.RegistrarVictoria(partes[1]);
                    break;

                case "NUEVARONDA":
                    ReiniciarPartida();
                    break;

                case "CONFIG":
                    bool valorDobles = bool.Parse(partes[1]);
                    tablasDobles = valorDobles;
                    configuracion.TablasDobles = valorDobles;
                    if (partes.Length > 2)
                    {
                        // formato: "dobles|numTablas|figuras"
                        if (int.TryParse(partes[2], out int numTablas))
                        {
                            configuracion.NumeroTablas = Math.Max(1, Math.Min(4, numTablas));
                        }
                    }
                    if (partes.Length > 3)
                    {
                        HashSet<TipoVictoria> figurasRecibidas = ConfiguracionPartida.DeserializarFiguras(partes[3]);
                        if (figurasRecibidas.Count > 0)
                            configuracion.FigurasHabilitadas = figurasRecibidas;
                    }
                    break;

                case "CONNECT_CONFIG":
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

        // ─── Drag & Drop de ficha ─────────────────────────────────────────────

        private void Casilla_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string)))
                e.Effect = DragDropEffects.Copy;
        }

        private void Casilla_DragDrop(object sender, DragEventArgs e)
        {
            if (!jugadorActivo) return;
            PictureBox pb = (PictureBox)sender;
            CasillaTabla casilla = (CasillaTabla)pb.Tag;
            if (casilla.Marcada) return;
            MostrarFicha(pb);
        }

        private void Casilla_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!jugadorActivo) return;

            PictureBox pb = (PictureBox)sender;

            if (pb.Tag is not CasillaTabla casilla) return;

            if (!casilla.Marcada) return;

            // Liberar imagen combinada anterior
            Image imagenAnterior = pb.Image;

            casilla.Marcada = false;
            pb.Image = casilla.Carta.RutaImagen;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.BackColor = Color.Empty;
            pb.Controls.Clear();

            if (imagenAnterior != null && imagenAnterior != casilla.Carta.RutaImagen)
                imagenAnterior.Dispose();
        }

        // ─── Lotería ─────────────────────────────────────────────────────────

        private void btnLoteria_Click(object sender, EventArgs e)
        {
            if (!jugadorActivo) return;

            // Verificar victoria en CUALQUIERA de las tablas del jugador
            TipoVictoria? victoriaEncontrada = null;
            TablaJugador? tablaGanadora = null;

            var tablas = jugador.Tablas.Count > 0 ? jugador.Tablas : (jugador.Tabla != null ? new List<TablaJugador> { jugador.Tabla } : new List<TablaJugador>());

            foreach (var tabla in tablas)
            {
                TipoVictoria? v = verificador.VerificarGanador(tabla, gestor.Historial, configuracion?.FigurasHabilitadas);
                if (v != null)
                {
                    victoriaEncontrada = v;
                    tablaGanadora = tabla;
                    break;
                }
            }

            if (victoriaEncontrada == null)
            {
                MessageBox.Show("No tienes ninguna figura válida.");
                return;
            }

            Carta cartaGanadora = verificador.ObtenerCartaGanadora(tablaGanadora!, gestor.Historial, victoriaEncontrada.Value);

            if (tipoPartida != TipoPartida.Multijugador)
            {
                GestorPuntaje.RegistrarVictoria(jugador.Nombre);
                timerCartas.Stop();
                timerCuentaAtras.Stop();
                ReiniciarPartida();
                return;
            }

            if (!esperandoDesempate)
            {
                esperandoDesempate = true;
                jugadoresDesempate.Clear();
                cartasGanadorasJugadores.Clear();
                timerDesempate.Start();
                timerCartas.Stop();
            }

            if (!jugadoresDesempate.Contains(jugador.Nombre))
                jugadoresDesempate.Add(jugador.Nombre);

            cartasGanadorasJugadores[jugador.Nombre] = cartaGanadora.Id;
            red.Enviar($"LOTERIA|{jugador.Nombre}|{cartaGanadora.Id}");
            MessageBox.Show("Lotería registrada.\nEsperando posibles empates...");
        }

        // ─── Mostrar ganador / reiniciar ──────────────────────────────────────

        private void MostrarGanador(string nombreGanador)
        {
            UcGanador ucGanador = new UcGanador(nombreGanador);
            ucGanador.Dock = DockStyle.Fill;
            ucGanador.NuevaPartidaSolicitada += () => ReiniciarDesdeGanador();

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
            lstChat.Items.Add($"[{mensaje.Fecha:HH:mm}] {mensaje.Usuario}: {mensaje.Texto}");
            lstChat.TopIndex = lstChat.Items.Count - 1;
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMensaje.Text)) return;
            string texto = txtMensaje.Text;
            MostrarMensaje(new MensajeChat(jugador.Nombre, texto));
            if (tipoPartida == TipoPartida.Multijugador)
                red.Enviar($"CHAT|{jugador.Nombre}|{texto}");
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
                Image imagenAnterior = pb.Image;
                pb.Image = casilla.Carta.RutaImagen;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.BackColor = Color.Empty;
                if (imagenAnterior != null && imagenAnterior != casilla.Carta.RutaImagen)
                    imagenAnterior.Dispose();
            }
        }

        private void LimpiarHistorial()
        {
            flpHistorial.Controls.Clear();
            lblContador.Text = "0/54";
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
            debeReproducirSonidoInicio = true;
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
            foreach (Control c in pnlGanador.Controls.OfType<UcGanador>().ToList())
            {
                pnlGanador.Controls.Remove(c);
                c.Dispose();
            }
            pnlGanador.Visible = false;
            pnlGanador.Enabled = false;

            if (tipoPartida == TipoPartida.Multijugador)
            {
                try { red.Enviar("REINICIAR"); } catch { }
                ReiniciarPartida();
            }
            else
            {
                ReiniciarPartida();
            }
        }

        // ─── Ficha ────────────────────────────────────────────────────────────

        private void pbFicha_Click(object sender, EventArgs e)
        {
            fichaSeleccionada = !fichaSeleccionada;
            pbFicha.BorderStyle = fichaSeleccionada ? BorderStyle.Fixed3D : BorderStyle.None;
        }

        private void Casilla_Click(object sender, EventArgs e)
        {
            if (!jugadorActivo) return;
            if (!fichaSeleccionada) return;

            PictureBox pb = (PictureBox)sender;
            CasillaTabla casilla = (CasillaTabla)pb.Tag;
            if (casilla.Marcada) return;

            MostrarFicha(pb);
            fichaSeleccionada = false;
            pbFicha.BorderStyle = BorderStyle.None;
        }

        private void MostrarFicha(PictureBox pb)
        {
            if (pb == null) return;
            if (pb.Tag is not CasillaTabla casillaTabla) return;

            Image imagenCombinada = CrearImagenConFichaSuperpuesta(casillaTabla.Carta.RutaImagen);
            Image imagenAnterior = pb.Image;
            pb.Image = imagenCombinada;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.BackColor = Color.Empty;

            if (imagenAnterior != null && imagenAnterior != casillaTabla.Carta.RutaImagen)
                imagenAnterior.Dispose();

            casillaTabla.Marcada = true;
        }

        private Image CrearImagenConFichaSuperpuesta(Image imagenCarta)
        {
            int ancho = imagenCarta.Width;
            int alto = imagenCarta.Height;
            Bitmap resultado = new Bitmap(ancho, alto);

            using (Graphics g = Graphics.FromImage(resultado))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(imagenCarta, 0, 0, ancho, alto);

                Bitmap ficha = Properties.Resources.Ficha;
                if (ficha != null)
                {
                    using (ImageAttributes atributos = new ImageAttributes())
                    {
                        atributos.SetColorKey(
                            Color.FromArgb(200, 200, 200),
                            Color.FromArgb(255, 255, 255),
                            ColorAdjustType.Bitmap);
                        g.DrawImage(ficha, new Rectangle(0, 0, ancho, alto),
                            0, 0, ficha.Width, ficha.Height,
                            GraphicsUnit.Pixel, atributos);
                    }
                }
            }

            return resultado;
        }

        // ─── Velocidad ────────────────────────────────────────────────────────

        private void btnAumento_Click(object sender, EventArgs e)
        {
            const int paso = 250;
            const int minimo = 250;
            int nuevo = Math.Max(minimo, timerCartas.Interval - paso);
            if (nuevo != timerCartas.Interval)
            {
                timerCartas.Interval = nuevo;
                segundosRestantes = Math.Max(1, (timerCartas.Interval + 999) / 1000);
                lblProximaCarta.Text = $"{segundosRestantes}s";
            }
        }

        private void btnResta_Click(object sender, EventArgs e)
        {
            const int paso = 250;
            const int maximo = 10000;
            int nuevo = Math.Min(maximo, timerCartas.Interval + paso);
            if (nuevo != timerCartas.Interval)
            {
                timerCartas.Interval = nuevo;
                segundosRestantes = Math.Max(1, (timerCartas.Interval + 999) / 1000);
                lblProximaCarta.Text = $"{segundosRestantes}s";
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
                    red.Enviar($"CARTA|{carta.Id}");
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
                lblProximaCarta.Text = "0s";
            }
        }
    }
}

