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

        // Eliminado el operador implícito inválido que causaba CS0555
        // public static implicit operator TablaGuardada(TablaGuardada v)
        // {
        //     throw new NotImplementedException();
        // }
    }
}
