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
    public partial class FormCrearTabla : Form
    {
        private GeneradorTablas generador;
        private TablaJugador tablaJugador;
        private Carta? cartaSeleccionada;
        public FormCrearTabla(Jugador jugador)
        {
            InitializeComponent();
            generador = new GeneradorTablas();
            tablaJugador = new TablaJugador();
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

            pbCasilla.Image = carta.RutaImagen;

            pbCasilla.Tag = carta;
        }

        private void CrearCasillasTabla()
        {
            tlpTabla.Controls.Clear();

            for (int fila = 0; fila < 5; fila++)
            {
                for (int columna = 0; columna < 5; columna++)
                {
                    PictureBox pbCasilla = new PictureBox();

                    pbCasilla.Dock = DockStyle.Fill;

                    pbCasilla.BorderStyle = BorderStyle.FixedSingle;

                    pbCasilla.SizeMode = PictureBoxSizeMode.Zoom;

                    pbCasilla.AllowDrop = true;

                    pbCasilla.BackColor = Color.White;

                    pbCasilla.DragEnter += Casilla_DragEnter;

                    pbCasilla.DragDrop += Casilla_DragDrop;

                    tlpTabla.Controls.Add(pbCasilla, columna, fila);
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

                pbCarta.SizeMode = PictureBoxSizeMode.Zoom;

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

        private bool CartaYaEstaEnTabla(int idCarta)
        {
            foreach (Control control in tlpTabla.Controls)
            {
                PictureBox pb = control as PictureBox;

                if (pb != null && pb.Tag != null)
                {
                    Carta carta = (Carta)pb.Tag;

                    if (carta.Id == idCarta)
                    {
                        return true;
                    }
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
            LimpiarTabla();

            List<Carta> cartas =
                generador.GenerarCartasAleatorias(25);

            int indice = 0;

            foreach (Control control in tlpTabla.Controls)
            {
                PictureBox pb = control as PictureBox;

                Carta carta = cartas[indice];

                pb.Image = carta.RutaImagen;

                pb.Tag = carta;

                indice++;
            }
        }

        private void bttnInicio_Click(object sender, EventArgs e)
        {
            if (!tablaJugador.EstaCompleta())
            {
                MessageBox.Show(
                    "Debes completar las 25 cartas.");
                return;
            }

            MessageBox.Show(
                "Tabla guardada correctamente.");
        }
    }
}
