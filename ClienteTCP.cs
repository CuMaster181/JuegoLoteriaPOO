using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JuegoLoteriaPOO
{
    internal class ClienteTCP
    {
        private List<TcpClient> clientes = new();

        public bool Conectar(
            string ip,
            int puerto)
        {
            try
            {
                TcpClient cliente = new TcpClient();

                cliente.Connect(ip, puerto);

                clientes.Add(cliente);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
