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
        private Carta cartaSeleccionada;
        public FormCrearTabla()
        {
            InitializeComponent();
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

            Carta carta =
                (Carta)e.Data.GetData(typeof(Carta));

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

        private void Carta_MouseDown(object sender,MouseEventArgs e)
        {
            PictureBox pbCarta = (PictureBox)sender;

            Carta carta = (Carta)pbCarta.Tag;

            pbCarta.DoDragDrop(carta, DragDropEffects.Move);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

    }
}
