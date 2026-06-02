using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    internal class GeneradorTablas
    {
        public List<Carta> GenerarCartasAleatorias(int cantidad)
        {
            Random rnd = new Random();

            List<Carta> disponibles =
                Carta.cartas.Values.ToList();

            List<Carta> seleccionadas =
                new List<Carta>();

            while (seleccionadas.Count < cantidad)
            {
                int indice =
                    rnd.Next(disponibles.Count);

                seleccionadas.Add(
                    disponibles[indice]);

                disponibles.RemoveAt(indice);
            }

            return seleccionadas;
        }
    }
}
