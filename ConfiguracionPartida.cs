using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    public class ConfiguracionPartida
    {
        public bool TablasDobles { get; set; }

        /// <summary>
        /// Número de tablas que usará cada jugador (1 a 4).
        /// </summary>
        public int NumeroTablas { get; set; } = 1;

        /// <summary>
        /// Conjunto de figuras (formas de ganar) habilitadas para la partida.
        /// Por defecto incluye todas las figuras existentes.
        /// Esta configuracion la define unicamente el Host; los clientes
        /// la heredan al unirse a la sala.
        /// </summary>
        public HashSet<TipoVictoria> FigurasHabilitadas { get; set; } = ObtenerTodasLasFiguras();

        public static HashSet<TipoVictoria> ObtenerTodasLasFiguras()
        {
            return Enum.GetValues(typeof(TipoVictoria))
                .Cast<TipoVictoria>()
                .ToHashSet();
        }

        /// <summary>
        /// Serializa las figuras habilitadas a una cadena separada por comas
        /// (valores enteros del enum TipoVictoria) para enviarla por red.
        /// </summary>
        public string SerializarFiguras()
        {
            return string.Join(",", FigurasHabilitadas.Select(f => (int)f));
        }

        /// <summary>Serializa configuración completa. Formato: "numTablas;fig1,fig2,..."</summary>
        public string SerializarCompleto()
        {
            return $"{NumeroTablas};{SerializarFiguras()}";
        }

        /// <summary>Reconstruye ConfiguracionPartida desde SerializarCompleto.</summary>
        public static ConfiguracionPartida DeserializarCompleto(string datos, bool tablasDobles)
        {
            var cfg = new ConfiguracionPartida { TablasDobles = tablasDobles };
            if (string.IsNullOrWhiteSpace(datos)) return cfg;
            var partes = datos.Split(';');
            if (partes.Length >= 1 && int.TryParse(partes[0], out int n))
                cfg.NumeroTablas = Math.Max(1, Math.Min(4, n));
            if (partes.Length >= 2)
                cfg.FigurasHabilitadas = DeserializarFiguras(partes[1]);
            return cfg;
        }

        /// <summary>
        /// Reconstruye el conjunto de figuras habilitadas a partir de una
        /// cadena generada por SerializarFiguras. Si la cadena esta vacia
        /// o es invalida, devuelve el conjunto vacio (sin figuras habilitadas).
        /// </summary>
        public static HashSet<TipoVictoria> DeserializarFiguras(string datos)
        {
            HashSet<TipoVictoria> figuras = new HashSet<TipoVictoria>();

            if (string.IsNullOrWhiteSpace(datos))
            {
                return figuras;
            }

            foreach (string parte in datos.Split(','))
            {
                if (int.TryParse(parte, out int valor) && Enum.IsDefined(typeof(TipoVictoria), valor))
                {
                    figuras.Add((TipoVictoria)valor);
                }
            }

            return figuras;
        }
    }
}
