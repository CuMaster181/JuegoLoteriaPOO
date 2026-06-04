using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    public class Jugador
    {
        public string Nombre { get; set; }

        public int Puntaje { get; set; }

        public TablaJugador? Tabla { get; set; }
        public bool EsHost { get; set; }

        public Jugador(string nombre)
        {
            Nombre = nombre;
            Puntaje = 0;
        }
    }
}
