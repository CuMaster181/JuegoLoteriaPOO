using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    internal class Partida
    {
        public List<int> HistorialCartas { get; private set; }

        public Carta CartaActual { get; private set; }

        public bool PartidaTerminada { get; private set; }

        public Partida()
        {
            HistorialCartas = new List<int>();
            PartidaTerminada = false;
        }

        public bool CartaYaSalio(int idCarta)
        {
            return HistorialCartas.Contains(idCarta);
        }

        public void CantarCarta(Carta carta)
        {
            if (!HistorialCartas.Contains(carta.Id))
            {
                HistorialCartas.Add(carta.Id);
                CartaActual = carta;
            }
        }

        public int TotalCartasCantadas()
        {
            return HistorialCartas.Count;
        }
    }
}
