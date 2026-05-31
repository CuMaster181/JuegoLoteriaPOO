using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    internal class GeneradorCartas
    {
        private List<int> cartasDisponibles;

        public GeneradorCartas()
        {
            cartasDisponibles = Carta.cartas.Keys.ToList();
        }

        public Carta ObtenerCartaAleatoria()
        {
            if (cartasDisponibles.Count == 0)
                return null;

            Random rnd = new Random();

            int indice = rnd.Next(cartasDisponibles.Count);

            int idCarta = cartasDisponibles[indice];

            cartasDisponibles.RemoveAt(indice);

            return Carta.cartas[idCarta];
        }
    }
}
