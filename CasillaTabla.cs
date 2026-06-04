using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    public class CasillaTabla
    {
        public Carta Carta { get; set; }

        public bool Marcada { get; set; }

        public CasillaTabla(Carta carta)
        {
            Carta = carta;
            Marcada = false;
        }
    }
}
