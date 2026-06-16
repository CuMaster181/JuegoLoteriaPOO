using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuegoLoteriaPOO
{
    public partial class FormCrearTabla : Form
    {
        private Jugador jugador;
        private GeneradorTablas generador;
        private List<TablaJugador> tablasJugador = new List<TablaJugador>();
        private Carta? cartaSeleccionada;
        private Conexion conexion;
        private ConfiguracionPartida configuracion;
        private TipoPartida tipoPartida;
        private int numeroTablas;

        public FormCrearTabla(Jugador jugador, TipoPartida tipoPartida, Conexion conexion, ConfiguracionPartida configuracion)
        {
            InitializeComponent();
            generador = new GeneradorTablas();
            this.jugador = jugador;
            this.tipoPartida = tipoPartida;
            this.conexion = conexion;

            // Asegurar que configuracion no sea nula
            this.configuracion = configuracion ?? new ConfiguracionPartida();

            // En modo Solo siempre al menos 1 tabla
            if (this.tipoPartida == TipoPartida.Solo && this.configuracion.NumeroTablas < 1)
                this.configuracion.NumeroTablas = 1;

            numeroTablas = Math.Max(1, Math.Min(4, this.configuracion.NumeroTablas));

            base.FormClosed += (s, e) =>
            {
                var formMenu = Application.OpenForms.OfType<FormMenuPrincipal>().FirstOrDefault();
                if (formMenu != null)
                    formMenu.Show();
                else
                    Application.Exit();
            };
        }

        private void FormCrearTabla_Load(object sender, EventArgs e)
        {
            InicializarTablas();
            CargarCartasDisponibles();
        }

        // ─── Inicialización de múltiples tablas ───────────────────────────────

        private void InicializarTablas()
        {
            tablasJugador.Clear();
            tabControlTablas.TabPages.Clear();

            for (int t = 0; t < numeroTablas; t++)
            {
                var tabla = new TablaJugador();
                tabla.EsDoble = configuracion.TablasDobles;
                tablasJugador.Add(tabla);

                TabPage page = new TabPage($"Tabla {t + 1}");
                page.Tag = t; // índice

                TableLayoutPanel tlp = CrearTlpTabla(t);
                page.Controls.Add(tlp);

                tabControlTablas.TabPages.Add(page);
            }
        }

        private TableLayoutPanel CrearTlpTabla(int indiceTabla)
        {
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Name = $"tlpTabla_{indiceTabla}";
            tlp.Tag = indiceTabla;
            tlp.ColumnCount = 5;
            tlp.RowCount = 5;
            tlp.Dock = DockStyle.Fill;
            tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            for (int i = 0; i < 5; i++)
            {
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            }

            for (int fila = 0; fila < 5; fila++)
            {
                for (int columna = 0; columna < 5; columna++)
                {
                    PictureBox pb = new PictureBox();
                    pb.Dock = DockStyle.Fill;
                    pb.Margin = new Padding(3);
                    pb.BorderStyle = BorderStyle.FixedSingle;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.AllowDrop = true;
                    pb.Tag = null; // Carta irá aquí

                    pb.DragEnter += Casilla_DragEnter;
                    pb.DragDrop += (s, e) => Casilla_DragDrop(s, e, indiceTabla);
                    pb.MouseDoubleClick += (s, e) => Casilla_MouseDoubleClick(s, e, indiceTabla);

                    tlp.Controls.Add(pb, columna, fila);
                }
            }

            return tlp;
        }

        private TableLayoutPanel? ObtenerTlpActual()
        {
            if (tabControlTablas.SelectedTab == null) return null;
            return tabControlTablas.SelectedTab.Controls.OfType<TableLayoutPanel>().FirstOrDefault();
        }

        private TableLayoutPanel? ObtenerTlpPorIndice(int idx)
        {
            if (idx < 0 || idx >= tabControlTablas.TabPages.Count) return null;
            return tabControlTablas.TabPages[idx].Controls.OfType<TableLayoutPanel>().FirstOrDefault();
        }

        private int IndiceTablaActual()
        {
            return tabControlTablas.SelectedIndex >= 0 ? tabControlTablas.SelectedIndex : 0;
        }

        // ─── Drag & Drop ──────────────────────────────────────────────────────

        private void Casilla_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Carta)))
                e.Effect = DragDropEffects.Move;
        }

        private void Casilla_DragDrop(object sender, DragEventArgs e, int indiceTabla)
        {
            PictureBox pbCasilla = (PictureBox)sender;

            if (pbCasilla.Tag is Carta)
            {
                MessageBox.Show("Esta casilla ya tiene una carta.");
                return;
            }

            Carta carta = (Carta)e.Data.GetData(typeof(Carta));
            TablaJugador tabla = tablasJugador[indiceTabla];
            TableLayoutPanel tlp = ObtenerTlpPorIndice(indiceTabla)!;

            if (CartaYaExisteEnTlp(tlp, carta.Id))
            {
                MessageBox.Show("Esa carta ya está en esta tabla.");
                return;
            }

            int indice = tlp.Controls.GetChildIndex(pbCasilla);
            int fila = indice / 5;
            int columna = indice % 5;

            pbCasilla.Image = carta.RutaImagen;
            pbCasilla.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCasilla.Tag = carta;
            tabla.AsignarCasilla(fila, columna, carta);

            if (configuracion.TablasDobles)
            {
                if (fila == 1 && columna == 1)
                    DuplicarCartaEnTlp(tlp, tabla, 3, 3, carta);
                else if (fila == 3 && columna == 3)
                    DuplicarCartaEnTlp(tlp, tabla, 1, 1, carta);
            }
        }

        private void DuplicarCartaEnTlp(TableLayoutPanel tlp, TablaJugador tabla, int filaDestino, int columnaDestino, Carta carta)
        {
            foreach (Control control in tlp.Controls)
            {
                if (tlp.GetRow(control) == filaDestino && tlp.GetColumn(control) == columnaDestino)
                {
                    PictureBox pb = (PictureBox)control;
                    if (pb.Tag is Carta) break; // ya tiene carta
                    pb.Image = carta.RutaImagen;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.Tag = carta;
                    tabla.AsignarCasilla(filaDestino, columnaDestino, carta);
                    break;
                }
            }
        }

        private void Casilla_MouseDoubleClick(object sender, MouseEventArgs e, int indiceTabla)
        {
            PictureBox pb = (PictureBox)sender;
            if (pb.Tag is Carta carta)
            {
                TableLayoutPanel tlp = ObtenerTlpPorIndice(indiceTabla)!;
                int indice = tlp.Controls.GetChildIndex(pb);
                int fila = indice / 5;
                int columna = indice % 5;
                tablasJugador[indiceTabla].LimpiarCasilla(fila, columna);
                pb.Image = null;
                pb.Tag = null;
            }
        }

        // ─── Cargar cartas disponibles ────────────────────────────────────────

        private void CargarCartasDisponibles()
        {
            flpCartasDisponibles.Controls.Clear();

            foreach (Carta carta in Carta.cartas.Values)
            {
                PictureBox pbCarta = new PictureBox();
                pbCarta.Width = 75;
                pbCarta.Height = 110;
                pbCarta.Margin = new Padding(4);
                pbCarta.SizeMode = PictureBoxSizeMode.StretchImage;
                pbCarta.BorderStyle = BorderStyle.FixedSingle;
                pbCarta.Image = carta.RutaImagen;
                pbCarta.Tag = carta;
                pbCarta.MouseDown += Carta_MouseDown;
                flpCartasDisponibles.Controls.Add(pbCarta);
            }
        }

        private void Carta_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pbCarta = (PictureBox)sender;
            Carta carta = (Carta)pbCarta.Tag;
            pbCarta.DoDragDrop(carta, DragDropEffects.Move);
        }

        // ─── Random ───────────────────────────────────────────────────────────

        private void bttnRandom_Click(object sender, EventArgs e)
        {
            // Aleatorizar TODAS las tablas
            for (int t = 0; t < numeroTablas; t++)
            {
                AleatoriarTabla(t);
            }
        }

        private void AleatoriarTabla(int indiceTabla)
        {
            TablaJugador tabla = tablasJugador[indiceTabla];
            TableLayoutPanel tlp = ObtenerTlpPorIndice(indiceTabla)!;

            // Limpiar tabla
            tabla = new TablaJugador();
            tabla.EsDoble = configuracion.TablasDobles;
            tablasJugador[indiceTabla] = tabla;

            foreach (Control control in tlp.Controls)
            {
                if (control is PictureBox pb)
                {
                    pb.Image = null;
                    pb.Tag = null;
                }
            }

            if (configuracion.TablasDobles)
            {
                List<Carta> cartasAleatorias = generador.GenerarCartasAleatorias(24);
                int indiceCarta = 0;

                foreach (Control control in tlp.Controls)
                {
                    if (control is PictureBox pb)
                    {
                        int indice = tlp.Controls.GetChildIndex(pb);
                        int fila = indice / 5;
                        int columna = indice % 5;

                        if (fila == 3 && columna == 3) continue;

                        Carta carta = cartasAleatorias[indiceCarta++];
                        pb.Image = carta.RutaImagen;
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                        pb.Tag = carta;
                        tabla.AsignarCasilla(fila, columna, carta);

                        if (fila == 1 && columna == 1)
                            DuplicarCartaEnTlp(tlp, tabla, 3, 3, carta);
                    }
                }
            }
            else
            {
                List<Carta> cartasAleatorias = generador.GenerarCartasAleatorias(25);
                int indiceCarta = 0;

                foreach (Control control in tlp.Controls)
                {
                    if (control is PictureBox pb)
                    {
                        int indice = tlp.Controls.GetChildIndex(pb);
                        int fila = indice / 5;
                        int columna = indice % 5;

                        Carta carta = cartasAleatorias[indiceCarta++];
                        pb.Image = carta.RutaImagen;
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                        pb.Tag = carta;
                        tabla.AsignarCasilla(fila, columna, carta);
                    }
                }
            }
        }

        // ─── Inicio ───────────────────────────────────────────────────────────

        private void bttnInicio_Click(object sender, EventArgs e)
        {
            for (int t = 0; t < numeroTablas; t++)
            {
                if (!tablasJugador[t].EstaCompleta())
                {
                    MessageBox.Show($"La tabla {t + 1} le faltan {25 - tablasJugador[t].ContarCartas()} cartas.");
                    tabControlTablas.SelectedIndex = t;
                    return;
                }
            }

            jugador.Tablas = new List<TablaJugador>(tablasJugador);

            FormPartida frm = new FormPartida(jugador, tipoPartida, conexion, configuracion);
            frm.Show();
            this.Hide();
        }

        // ─── Guardar / Cargar tabla ───────────────────────────────────────────

        private void btnGuardarTabla_Click(object sender, EventArgs e)
        {
            int t = IndiceTablaActual();
            if (!tablasJugador[t].EstaCompleta())
            {
                MessageBox.Show("Completa la tabla primero.");
                return;
            }

            TablaGuardada tabla = new TablaGuardada();
            tabla.NombreJugador = jugador.Nombre;
            tabla.EsDoble = tablasJugador[t].EsDoble;

            for (int fila = 0; fila < 5; fila++)
                for (int columna = 0; columna < 5; columna++)
                    tabla.Cartas.Add(tablasJugador[t].Casillas[fila, columna].Carta.Id);

            Directory.CreateDirectory("TablasGuardadas");

            SaveFileDialog guardar = new SaveFileDialog();
            guardar.InitialDirectory = Path.Combine(Application.StartupPath, "TablasGuardadas");
            guardar.Filter = "Tabla (*.json)|*.json";
            guardar.FileName = $"{jugador.Nombre}_Tabla{t + 1}";

            if (guardar.ShowDialog() != DialogResult.OK) return;

            string json = JsonSerializer.Serialize(tabla, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(guardar.FileName, json);
            MessageBox.Show("Tabla guardada.");
        }

        private void btnCargarTabla_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.InitialDirectory = Path.Combine(Application.StartupPath, "TablasGuardadas");
            abrir.Filter = "Tabla (*.json)|*.json";

            if (abrir.ShowDialog() != DialogResult.OK) return;

            string json = File.ReadAllText(abrir.FileName);
            TablaGuardada tabla = JsonSerializer.Deserialize<TablaGuardada>(json);

            int t = IndiceTablaActual();
            CargarTablaGuardadaEnIndice(t, tabla);
        }

        private void CargarTablaGuardadaEnIndice(int indiceTabla, TablaGuardada tabla)
        {
            if (tabla.EsDoble != configuracion.TablasDobles)
            {
                MessageBox.Show("La tabla guardada no coincide con el tipo de tablas configurado por el Host.");
                return;
            }

            tablasJugador[indiceTabla] = new TablaJugador();
            tablasJugador[indiceTabla].EsDoble = configuracion.TablasDobles;
            TableLayoutPanel tlp = ObtenerTlpPorIndice(indiceTabla)!;

            int indiceCarta = 0;
            foreach (Control control in tlp.Controls)
            {
                if (control is PictureBox pb)
                {
                    int indice = tlp.Controls.GetChildIndex(pb);
                    int fila = indice / 5;
                    int columna = indice % 5;

                    int idCarta = tabla.Cartas[indiceCarta++];
                    Carta carta = Carta.cartas[idCarta];

                    pb.Image = carta.RutaImagen;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.Tag = carta;
                    tablasJugador[indiceTabla].AsignarCasilla(fila, columna, carta);
                }
            }

            MessageBox.Show("Tabla cargada.");
        }

        // ─── Helpers ─────────────────────────────────────────────────────────

        private bool CartaYaExisteEnTlp(TableLayoutPanel tlp, int idCarta)
        {
            foreach (Control control in tlp.Controls)
                if (control is PictureBox pb && pb.Tag is Carta c && c.Id == idCarta)
                    return true;
            return false;
        }
    }
}