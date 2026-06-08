using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    internal class MensajeChat
    {
        public string Usuario { get; set; }

        public string Texto { get; set; }

        public DateTime Fecha { get; set; }

        public MensajeChat(string usuario, string texto)
        {
            Usuario = usuario;
            Texto = texto;
            Fecha = DateTime.Now;
        }
    }
}
