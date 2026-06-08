using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    internal class GestorChat
    {
        public event Action<MensajeChat> MensajeRecibido;

        public void EnviarMensaje(string usuario, string texto)
        {
            MensajeChat mensaje = new MensajeChat(usuario, texto);
            MensajeRecibido?.Invoke(mensaje);
        }
    }
}
