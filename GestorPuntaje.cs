using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    internal class GestorPuntaje
    {
        public static List<PuntajeJugador> Ranking =
            new List<PuntajeJugador>();

        public static void AgregarVictoria(string nombre)
        {
            PuntajeJugador jugador = Ranking.FirstOrDefault(x => x.Nombre == nombre);

            if (jugador == null)
            {
                jugador =
                    new PuntajeJugador
                    {
                        Nombre = nombre,
                        Puntos = 0
                    };

                Ranking.Add(jugador);
            }
            
            jugador.Puntos++;
        }

        public static void RegistrarVictoria(string nombre)
        {
            PuntajeJugador jugador = Ranking.FirstOrDefault(x => x.Nombre == nombre);

            if (jugador == null)
            {
                jugador = new PuntajeJugador
                {
                    Nombre = nombre,
                    Puntos = 1
                };

                Ranking.Add(jugador);
            }
            else
            {
                jugador.Puntos++;
            }

            Ranking = Ranking.OrderByDescending(x => x.Puntos).ToList();
        }
    }
}
