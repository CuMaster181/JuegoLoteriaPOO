using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    internal class TablaJugador
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

        private bool CartaYaEstaEnTabla(int idCarta)
        {
            foreach (Control control in tlpTabla.Controls)
            {
                PictureBox pb = control as PictureBox;

                if (pb != null && pb.Tag != null)
                {
                    Carta carta = (Carta)pb.Tag;

                    if (carta.Id == idCarta)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
