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
        private TablaJugador tablaJugador;
        private Carta? cartaSeleccionada;
        private Conexion conexion;
        private TipoPartida tipoPartida;
        public FormCrearTabla(Jugador jugador, TipoPartida tipoPartida, Conexion conexion)
        {
            InitializeComponent();
            generador = new GeneradorTablas();
            tablaJugador = new TablaJugador();
            this.jugador = jugador;
            this.tipoPartida = tipoPartida;
            this.conexion = conexion;
        }

        private void FormCrearTabla_Load(object sender, EventArgs e)
        {
            CrearCasillasTabla();

            CargarCartasDisponibles();
        }

        private void Casilla_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Carta)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void Casilla_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox pbCasilla = (PictureBox)sender;

            if (pbCasilla.Image != null)
            {
                MessageBox.Show("Esta casilla ya tiene una carta.");
                return;
            }

            Carta carta = (Carta)e.Data.GetData(typeof(Carta));

            if (CartaYaExiste(carta.Id))
            {
                MessageBox.Show("Esa carta ya está en la tabla.");
                return;
            }

            pbCasilla.Image = carta.RutaImagen;

            pbCasilla.SizeMode = PictureBoxSizeMode.StretchImage;

            pbCasilla.Tag = carta;

            int indice = tlpTabla.Controls.GetChildIndex(pbCasilla);

            int fila = indice / 5;

            int columna = indice % 5;

            if ((fila == 1 && columna == 1) || (fila == 3 && columna == 3))
            {
                // Corregido: TablaJugador almacena CasillaTabla, no Carta.
                CasillaTabla casillaExistente1 = tablaJugador.Casillas[1, 1];
                CasillaTabla casillaExistente2 = tablaJugador.Casillas[3, 3];

                if (casillaExistente1 != null || casillaExistente2 != null)
                {
                    MessageBox.Show("La carta duplicada ya fue asignada.");

                    return;
                }
            }

            tablaJugador.AsignarCasilla(fila, columna, carta);

            if (fila == 1 && columna == 1)
            {
                DuplicarCarta(3, 3, carta);
            }
            else if (fila == 3 && columna == 3)
            {
                DuplicarCarta(1, 1, carta);
            }
        }

        private void DuplicarCarta(int filaDestino, int columnaDestino, Carta carta)
        {
            foreach (Control control in tlpTabla.Controls)
            {
                if (tlpTabla.GetRow(control) == filaDestino &&
                    tlpTabla.GetColumn(control) == columnaDestino)
                {
                    PictureBox pb =
                        (PictureBox)control;

                    pb.Image = carta.RutaImagen;

                    pb.Tag = carta;

                    tablaJugador.AsignarCasilla(
                        filaDestino,
                        columnaDestino,
                        carta);

                    break;
                }
            }
        }

        private void CrearCasillasTabla()
        {
            tlpTabla.Controls.Clear();

            for (int fila = 0; fila < 5; fila++)
            {
                for (int columna = 0; columna < 5; columna++)
                {
                    PictureBox pb = new PictureBox();

                    pb.Dock = DockStyle.Fill;
                    pb.Width = 90;
                    pb.Height = 130;
                    pb.Margin = new Padding(5);
                    pb.BorderStyle = BorderStyle.FixedSingle;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.AllowDrop = true;

                    pb.DragEnter += Casilla_DragEnter;
                    pb.DragDrop += Casilla_DragDrop;
                    pb.MouseDoubleClick += Casilla_MouseDoubleClick;

                    tlpTabla.Controls.Add(pb, columna, fila);
                }
            }
        }

        private void CargarCartasDisponibles()
        {
            flpCartasDisponibles.Controls.Clear();

            foreach (Carta carta in Carta.cartas.Values)
            {
                PictureBox pbCarta = new PictureBox();

                pbCarta.Width = 90;
                pbCarta.Height = 130;
                pbCarta.Margin = new Padding(5);
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

        private void Casilla_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            if (pb.Tag is Carta)
            {
                int indice = tlpTabla.Controls.GetChildIndex(pb);

                int fila = indice / 5;

                int columna = indice % 5;

                tablaJugador.Casillas[fila, columna] = null;

                pb.Image = null;

                pb.Tag = null;
            }
        }

        private bool CartaYaExiste(int idCarta)
        {
            foreach (Control control in tlpTabla.Controls)
            {
                if (control is PictureBox pb && pb.Tag is Carta carta && carta.Id == idCarta)
                {
                    return true;
                }
            }

            return false;
        }

        private void LimpiarTabla()
        {
            foreach (Control control in tlpTabla.Controls)
            {
                PictureBox pb = control as PictureBox;

                if (pb != null)
                {
                    pb.Image = null;
                    pb.Tag = null;
                }
            }
        }

        private void bttnRandom_Click(object sender, EventArgs e)
        {
            tablaJugador = new TablaJugador();

            List<Carta> cartasAleatorias =
                generador.GenerarCartasAleatorias(24);

            int indiceCarta = 0;

            foreach (Control control in tlpTabla.Controls)
            {
                if (control is PictureBox pb)
                {
                    int indice = tlpTabla.Controls.GetChildIndex(pb);

                    int fila = indice / 5;

                    int columna = indice % 5;

                    if (fila == 3 && columna == 3)
                    {
                        continue;
                    }

                    Carta carta = cartasAleatorias[indiceCarta];

                    pb.Image = carta.RutaImagen;

                    pb.SizeMode = PictureBoxSizeMode.StretchImage;

                    pb.Tag = carta;

                    tablaJugador.AsignarCasilla(fila, columna, carta);

                    if (fila == 1 && columna == 1)
                    {
                        DuplicarCarta(3, 3, carta);
                    }

                    indiceCarta++;
                }
            }
        }

        private void bttnInicio_Click(object sender, EventArgs e)
        {
            if (!tablaJugador.EstaCompleta())
            {
                MessageBox.Show($"Faltan {25 - tablaJugador.ContarCartas()} cartas para completar la tabla.");

                return;
            }

            jugador.Tabla = tablaJugador;

            FormPartida frm = new FormPartida(jugador, tipoPartida, conexion);

            frm.Show();

            this.Hide();
        }

        private void btnGuardarTabla_Click(object sender, EventArgs e)
        {
            if (!tablaJugador.EstaCompleta())
            {
                MessageBox.Show(
                    "Completa la tabla primero.");

                return;
            }

            TablaGuardada tabla = new TablaGuardada();

            tabla.NombreJugador = jugador.Nombre;

            for (int fila = 0; fila < 5; fila++)
            {
                for (int columna = 0; columna < 5; columna++)
                {
                    tabla.Cartas.Add(
                        tablaJugador
                        .Casillas[fila, columna]
                        .Carta.Id);
                }
            }

            Directory.CreateDirectory(
                "TablasGuardadas");

            SaveFileDialog guardar =
                new SaveFileDialog();

            guardar.InitialDirectory =
                Path.Combine(
                    Application.StartupPath,
                    "TablasGuardadas");

            guardar.Filter =
                "Tabla (*.json)|*.json";

            guardar.FileName =
                jugador.Nombre + "_Tabla";

            if (guardar.ShowDialog()
                != DialogResult.OK)
            {
                return;
            }

            string json =
                JsonSerializer.Serialize(
                    tabla,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

            File.WriteAllText(
                guardar.FileName,
                json);

            MessageBox.Show(
                "Tabla guardada.");
        }

        private void btnCargarTabla_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir =
        new OpenFileDialog();

            abrir.InitialDirectory =
                Path.Combine(
                    Application.StartupPath,
                    "TablasGuardadas");

            abrir.Filter =
                "Tabla (*.json)|*.json";

            if (abrir.ShowDialog()
                != DialogResult.OK)
            {
                return;
            }

            string json =
                File.ReadAllText(
                    abrir.FileName);

            TablaGuardada tabla = JsonSerializer.Deserialize<TablaGuardada>(json);

            CargarTablaGuardada(tabla);
        }

        private void CargarTablaGuardada(TablaGuardada tabla)
        {
            tablaJugador =
                new TablaJugador();

            int indiceCarta = 0;

            foreach (Control control in tlpTabla.Controls)
            {
                if (control is PictureBox pb)
                {
                    int indice =
                        tlpTabla.Controls.GetChildIndex(pb);

                    int fila =
                        indice / 5;

                    int columna =
                        indice % 5;

                    int idCarta =
                        tabla.Cartas[indiceCarta];

                    Carta carta =
                        Carta.cartas[idCarta];

                    pb.Image =
                        carta.RutaImagen;

                    pb.Tag =
                        carta;

                    tablaJugador
                        .AsignarCasilla(
                            fila,
                            columna,
                            carta);

                    indiceCarta++;
                }
            }

            MessageBox.Show(
                "Tabla cargada.");
        }
    }
}
