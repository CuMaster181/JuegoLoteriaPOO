namespace JuegoLoteriaPOO
{
    public partial class FormPartida : Form
    {
        private GestorPartida gestor;
        public FormPartida()
        {
            InitializeComponent();
            gestor = new GestorPartida();
        }
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            Carta carta = gestor.SiguienteCarta();

            if (carta == null)
            {
                MessageBox.Show("No quedan cartas.");
                return;
            }

            MostrarCarta(carta);
        }

        private void MostrarCarta(Carta carta)
        {
            lblNombreCarta.Text = carta.Nombre;
            pbCartaActual.Image = Image.FromFile(carta.RutaImagen);
        }
    }
}
