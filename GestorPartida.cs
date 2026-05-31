using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    internal class GestorPartida
    {
        private readonly Partida partida;
        private readonly GeneradorCartas generador;

        public GestorPartida()
        {
            partida = new Partida();
            generador = new GeneradorCartas();
        }

        public Carta SiguienteCarta()
        {
            Carta carta = generador.ObtenerCartaAleatoria();

            if (carta != null)
                partida.CantarCarta(carta);

            return carta;
        }

        public bool YaSalio(int idCarta)
        {
            return partida.CartaYaSalio(idCarta);
        }

        public Carta CartaActual
        {
            get { return partida.CartaActual; }
        }
    }
}
