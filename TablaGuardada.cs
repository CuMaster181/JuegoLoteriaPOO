using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    public class TablaGuardada
    {
        public string NombreJugador { get; set; }

        public List<int> Cartas { get; set; } = new List<int>();
    }
}
