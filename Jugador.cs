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

        /// <summary>Primera tabla (para compatibilidad con código existente).</summary>
        public TablaJugador? Tabla
        {
            get => Tablas.Count > 0 ? Tablas[0] : null;
            set
            {
                if (value == null) return;
                if (Tablas.Count == 0) Tablas.Add(value);
                else Tablas[0] = value;
            }
        }

        /// <summary>Lista de tablas del jugador (1-4).</summary>
        public List<TablaJugador> Tablas { get; set; } = new List<TablaJugador>();

        public bool EsHost { get; set; }

        public Jugador(string nombre)
        {
            Nombre = nombre;
            Puntaje = 0;
        }
    }
}
