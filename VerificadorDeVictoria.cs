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
        public TipoVictoria? VerificarGanador(TablaJugador tabla, List<Carta> historial)
        {
            if (VerificarHorizontal(tabla, historial))
                return TipoVictoria.Horizontal;

            if (VerificarVertical(tabla, historial))
                return TipoVictoria.Vertical;

            if (VerificarDiagonalPrincipal(tabla, historial))
                return TipoVictoria.DiagonalPrincipal;

            if (VerificarDiagonalSecundaria(tabla, historial))
                return TipoVictoria.DiagonalSecundaria;

            if (VerificarPatron(tabla, historial, PatronVictoria.Patrones[TipoVictoria.Cruzita]))
                return TipoVictoria.Cruzita;

            if (VerificarPatron(tabla, historial, PatronVictoria.Patrones[TipoVictoria.T]))
                return TipoVictoria.T;

            if (VerificarPatron(tabla, historial, PatronVictoria.Patrones[TipoVictoria.Pollita]))
                return TipoVictoria.Pollita;

            if (VerificarPatron(tabla, historial, PatronVictoria.Patrones[TipoVictoria.L]))
                return TipoVictoria.L;

            if (VerificarPatron(tabla, historial, PatronVictoria.Patrones[TipoVictoria.J]))
                return TipoVictoria.J;

            if (VerificarTablaCompleta(tabla, historial))
                return TipoVictoria.TablaCompleta;

            return null;
        }

        private bool VerificarHorizontal(TablaJugador tabla, List<Carta> historial)
        {
            for (int fila = 0; fila < 5; fila++)
            {
                bool completa = true;

                for (int columna = 0; columna < 5; columna++)
                {
                    if (!CasillaValida(tabla.Casillas[fila, columna], historial))
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

        private bool VerificarVertical(TablaJugador tabla, List<Carta> historial)
        {
            for (int columna = 0; columna < 5; columna++)
            {
                bool completa = true;

                for (int fila = 0; fila < 5; fila++)
                {
                    if (!CasillaValida(tabla.Casillas[fila, columna], historial))
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

        private bool VerificarDiagonalPrincipal(TablaJugador tabla, List<Carta> historial)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!CasillaValida(tabla.Casillas[i, i], historial))
                {
                    return false;
                }
            }

            return true;
        }

        private bool VerificarDiagonalSecundaria(TablaJugador tabla, List<Carta> historial)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!CasillaValida(tabla.Casillas[i, 4 - i], historial))
                {
                    return false;
                }
            }

            return true;
        }

        private bool VerificarTablaCompleta(TablaJugador tabla, List<Carta> historial)
        {
            foreach (CasillaTabla casilla in tabla.Casillas)
            {
                if (!CasillaValida(casilla, historial))
                    return false;
            }

            return true;
        }

        private bool VerificarPatron(TablaJugador tabla, List<Carta> historial, int[,] patron)
        {
            for (int filaInicio = 0; filaInicio <= 2; filaInicio++)
            {
                for (int columnaInicio = 0; columnaInicio <= 2; columnaInicio++)
                {
                    bool coincide = true;

                    for (int i = 0; i < patron.GetLength(0); i++)
                    {
                        int fila = filaInicio + patron[i, 0];
                        int columna = columnaInicio + patron[i, 1];

                        if (!CasillaValida(tabla.Casillas[fila, columna], historial))
                        {
                            coincide = false;
                            break;
                        }
                    }

                    if (coincide)
                        return true;
                }
            }

            return false;
        }

        private bool CasillaValida(CasillaTabla casilla, List<Carta> historial)
        {
            return casilla.Marcada && historial.Any(c => c.Id == casilla.Carta.Id);
        }
    }
}
