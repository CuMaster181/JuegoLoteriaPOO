using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    public class TablaJugador
    {
        public CasillaTabla[,] Casillas { get; private set; }

        public const int Filas = 5;
        public const int Columnas = 5;

        public TablaJugador()
        {
            Casillas = new CasillaTabla[Filas, Columnas];
        }

        public CasillaTabla ObtenerCasilla(int fila, int columna)
        {
            return Casillas[fila, columna];
        }

        public void AsignarCasilla(int fila, int columna, Carta carta)
        {
            Casillas[fila, columna] = new CasillaTabla(carta);
        }

        public void LimpiarCasilla(int fila, int columna)
        {
            Casillas[fila, columna] = null;
        }

        public int ContarCartas()
        {
            int total = 0;

            foreach (CasillaTabla casilla in Casillas)
            {
                if (casilla != null)
                {
                    total++;
                }
            }

            return total;
        }

        public bool EstaCompleta()
        {
            foreach (CasillaTabla casilla in Casillas)
            {
                if (casilla == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
