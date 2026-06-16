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
        /// <summary>
        /// Verifica si el jugador tiene una figura ganadora.
        /// Si se proporciona <paramref name="figurasHabilitadas"/>, solo se
        /// consideran validas las figuras incluidas en ese conjunto. Si es
        /// null, se consideran todas las figuras (comportamiento por defecto).
        /// </summary>
        public TipoVictoria? VerificarGanador(TablaJugador tabla, List<Carta> historial, HashSet<TipoVictoria>? figurasHabilitadas = null)
        {
            bool Habilitada(TipoVictoria tipo) => figurasHabilitadas == null || figurasHabilitadas.Contains(tipo);

            if (Habilitada(TipoVictoria.Horizontal) && VerificarHorizontal(tabla, historial))
                return TipoVictoria.Horizontal;

            if (Habilitada(TipoVictoria.Vertical) && VerificarVertical(tabla, historial))
                return TipoVictoria.Vertical;

            if (Habilitada(TipoVictoria.DiagonalPrincipal) && VerificarDiagonalPrincipal(tabla, historial))
                return TipoVictoria.DiagonalPrincipal;

            if (Habilitada(TipoVictoria.DiagonalSecundaria) && VerificarDiagonalSecundaria(tabla, historial))
                return TipoVictoria.DiagonalSecundaria;

            if (Habilitada(TipoVictoria.Cruzita) && VerificarPatron(tabla, historial, PatronVictoria.Patrones[TipoVictoria.Cruzita]))
                return TipoVictoria.Cruzita;

            if (Habilitada(TipoVictoria.T) && VerificarPatron(tabla, historial, PatronVictoria.Patrones[TipoVictoria.T]))
                return TipoVictoria.T;

            if (Habilitada(TipoVictoria.Pollita) && VerificarPatron(tabla, historial, PatronVictoria.Patrones[TipoVictoria.Pollita]))
                return TipoVictoria.Pollita;

            if (Habilitada(TipoVictoria.L) && VerificarPatron(tabla, historial, PatronVictoria.Patrones[TipoVictoria.L]))
                return TipoVictoria.L;

            if (Habilitada(TipoVictoria.J) && VerificarPatron(tabla, historial, PatronVictoria.Patrones[TipoVictoria.J]))
                return TipoVictoria.J;

            if (Habilitada(TipoVictoria.TablaCompleta) && VerificarTablaCompleta(tabla, historial))
                return TipoVictoria.TablaCompleta;

            return null;
        }

        private bool VerificarHorizontalFila(TablaJugador tabla, List<Carta> historial, int fila)
        {
            for (int columna = 0; columna < 5; columna++)
            {
                if (!CasillaValida(tabla.Casillas[fila, columna], historial))
                    return false;
            }
            return true;
        }

        private bool VerificarVerticalColumna(TablaJugador tabla, List<Carta> historial, int col)
        {
            for (int fila = 0; fila < 5; fila++)
            {
                if (!CasillaValida(tabla.Casillas[fila, col], historial))
                    return false;
            }
            return true;
        }

        private bool VerificarPatronEnPosicion(TablaJugador tabla, List<Carta> historial, int[,] patron, int filaInicio, int columnaInicio)
        {
            for (int i = 0; i < patron.GetLength(0); i++)
            {
                int fila = filaInicio + patron[i, 0];
                int columna = columnaInicio + patron[i, 1];

                if (!CasillaValida(tabla.Casillas[fila, columna], historial))
                    return false;
            }
            return true;
        }

        private bool VerificarHorizontal(TablaJugador tabla, List<Carta> historial)
        {
            for (int fila = 0; fila < 5; fila++)
            {
                if (VerificarHorizontalFila(tabla, historial, fila))
                    return true;
            }
            return false;
        }

        private bool VerificarVertical(TablaJugador tabla, List<Carta> historial)
        {
            for (int columna = 0; columna < 5; columna++)
            {
                if (VerificarVerticalColumna(tabla, historial, columna))
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
                    if (VerificarPatronEnPosicion(tabla, historial, patron, filaInicio, columnaInicio))
                        return true;
                }
            }

            return false;
        }

        public Carta ObtenerCartaGanadora(TablaJugador tabla, List<Carta> historial, TipoVictoria tipo)
        {
            List<Carta> cartasFigura = new List<Carta>();

            if (tipo == TipoVictoria.Horizontal)
            {
                for (int fila = 0; fila < 5; fila++)
                {
                    if (VerificarHorizontalFila(tabla, historial, fila))
                    {
                        for (int col = 0; col < 5; col++)
                            cartasFigura.Add(tabla.Casillas[fila, col].Carta);
                    }
                }
            }
            else if (tipo == TipoVictoria.Vertical)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (VerificarVerticalColumna(tabla, historial, col))
                    {
                        for (int fila = 0; fila < 5; fila++)
                            cartasFigura.Add(tabla.Casillas[fila, col].Carta);
                    }
                }
            }
            else if (tipo == TipoVictoria.DiagonalPrincipal)
            {
                for (int i = 0; i < 5; i++)
                    cartasFigura.Add(tabla.Casillas[i, i].Carta);
            }
            else if (tipo == TipoVictoria.DiagonalSecundaria)
            {
                for (int i = 0; i < 5; i++)
                    cartasFigura.Add(tabla.Casillas[i, 4 - i].Carta);
            }
            else if (tipo == TipoVictoria.TablaCompleta)
            {
                foreach (CasillaTabla casilla in tabla.Casillas)
                    cartasFigura.Add(casilla.Carta);
            }
            else // Patrones (Cruzita, T, Pollita, L, J)
            {
                if (PatronVictoria.Patrones.ContainsKey(tipo))
                {
                    int[,] patron = PatronVictoria.Patrones[tipo];
                    for (int filaInicio = 0; filaInicio <= 2; filaInicio++)
                    {
                        for (int columnaInicio = 0; columnaInicio <= 2; columnaInicio++)
                        {
                            if (VerificarPatronEnPosicion(tabla, historial, patron, filaInicio, columnaInicio))
                            {
                                for (int i = 0; i < patron.GetLength(0); i++)
                                {
                                    int fila = filaInicio + patron[i, 0];
                                    int col = columnaInicio + patron[i, 1];
                                    cartasFigura.Add(tabla.Casillas[fila, col].Carta);
                                }
                            }
                        }
                    }
                }
            }

            // Encontrar la carta que aparezca de última en el historial
            Carta cartaGanadora = null;
            int maxIndex = -1;

            foreach (Carta c in cartasFigura)
            {
                int idx = historial.FindLastIndex(x => x.Id == c.Id);
                if (idx > maxIndex)
                {
                    maxIndex = idx;
                    cartaGanadora = c;
                }
            }

            return cartaGanadora ?? cartasFigura.FirstOrDefault();
        }

        private bool CasillaValida(CasillaTabla casilla, List<Carta> historial)
        {
            return casilla.Marcada && historial.Any(c => c.Id == casilla.Carta.Id);
        }
    }
}