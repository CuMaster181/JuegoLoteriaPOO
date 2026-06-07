using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    internal class VerificadorDeVictoria
    {

        private Jugador jugador;
        private TablaJugador tabla;
        private List<Carta> historial;
        public bool VerificarGanador(TablaJugador tabla, List<Carta> historial, TipoVictoria tipo)
        {
            switch (tipo)
            {
                case TipoVictoria.LineaHorizontal:
                    return VerificarLineaHorizontal(tabla, historial);

                case TipoVictoria.TablaCompleta:
                    return VerificarTablaCompleta(tabla, historial);

                default:
                    return false;
            }
        }

        private bool VerificarLineaHorizontal(TablaJugador tabla, List<Carta> historial)
        {
            for (int fila = 0; fila < 5; fila++)
            {
                bool completa = true;

                for (int columna = 0; columna < 5; columna++)
                {
                    CasillaTabla casilla = tabla.Casillas[fila, columna];

                    if (!casilla.Marcada)
                    {
                        completa = false;
                        break;
                    }

                    bool yaSalio = historial.Any(c => c.Id == casilla.Carta.Id);

                    if (!yaSalio)
                    {
                        completa = false;
                        break;
                    }
                }

                if (completa)
                    return true;
            }

            return false;
        }

        private bool VerificarTablaCompleta(TablaJugador tabla, List<Carta> historial)
        {
            for (int fila = 0; fila < 5; fila++)
            {
                for (int columna = 0; columna < 5; columna++)
                {
                    CasillaTabla casilla = tabla.Casillas[fila, columna];

                    if (!casilla.Marcada)
                        return false;

                    bool yaSalio = historial.Any(c => c.Id == casilla.Carta.Id);

                    if (!yaSalio)
                        return false;
                }
            }

            return true;
        }
    }
}
