using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    public class Conexion
    {
        public string IP { get; set; }
        public int Puerto { get; set; }
        public bool EsHost { get; set; }
        public HashSet<string> JugadoresConectados { get; } = new(StringComparer.OrdinalIgnoreCase);
        internal GestorMultijugador? Red { get; set; }
    }
}
