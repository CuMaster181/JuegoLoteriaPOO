using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    internal class Partida
    {
        public List<int> CartasCantadas { get; private set; }

        public Carta CartaActual { get; private set; }

        public bool PartidaTerminada { get; private set; }

        public Partida()
        {
            CartasCantadas = new List<int>();
            PartidaTerminada = false;
        }

        public bool CartaYaSalio(int idCarta)
        {
            return CartasCantadas.Contains(idCarta);
        }

        public void CantarCarta(Carta carta)
        {
            if (!CartasCantadas.Contains(carta.Id))
            {
                CartasCantadas.Add(carta.Id);
                CartaActual = carta;
            }
        }

        public int TotalCartasCantadas()
        {
            return CartasCantadas.Count;
        }
    }
}
