using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    internal class ServidorTCP
    {
        private TcpListener listener;

        public void Iniciar(int puerto)
        {
            listener = new TcpListener(
                IPAddress.Any,
                puerto);

            listener.Start();
        }

        public TcpClient EsperarCliente()
        {
            return listener.AcceptTcpClient();
        }
    }
}
