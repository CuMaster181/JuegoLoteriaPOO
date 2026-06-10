using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    internal class GestorPartida
    {
        private Queue<Carta> mazo;
        public List<Carta> Historial { get; } = new();
        public Carta CartaActual { get; private set; }
        public bool Pausado { get; private set; }
        private TipoVictoria tipoVictoria;
        public TipoVictoria TipoVictoriaActual {get { return tipoVictoria; }}

        public GestorPartida()
        {
            InicializarMazo();
        }

        public Carta SiguienteCarta()
        {
            if (mazo.Count == 0)
                return null;

            CartaActual = mazo.Dequeue();
            Historial.Add(CartaActual);
            return CartaActual;
        }

        public void Pausar()
        {
            Pausado = true;
        }

        public void Reanudar()
        {
            Pausado = false;
        }

        private void InicializarMazo()
        {
            Random rnd = new Random();

            mazo = new Queue<Carta>(Carta.cartas.Values.OrderBy(x => rnd.Next()));
        }

        public void Reiniciar()
        {
            Historial.Clear();

            CartaActual = null;

            InicializarMazo();
        }
    }
}
