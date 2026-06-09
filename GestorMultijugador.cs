using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace JuegoLoteriaPOO
{
    internal class GestorMultijugador
    {
        public event Action<string>? MensajeRecibido;
        private List<TcpClient> clientes = new();
        private TcpClient miCliente;
        private TcpListener servidor;

        public bool EsHost { get; private set; }

        public async Task IniciarServidor(int puerto)
        {
            EsHost = true;

            servidor = new TcpListener(
                IPAddress.Any,
                puerto);

            servidor.Start();

            while (true)
            {
                TcpClient cliente =
                    await servidor.AcceptTcpClientAsync();

                clientes.Add(cliente);

                Escuchar(cliente);
            }
        }

        public async Task Conectar(
            string ip,
            int puerto)
        {
            EsHost = false;

            miCliente = new TcpClient();

            await miCliente.ConnectAsync(
                ip,
                puerto);

            Escuchar(miCliente);
        }

        public void Enviar(string mensaje)
        {
            byte[] datos = Encoding.UTF8.GetBytes(mensaje);

            if (EsHost)
            {
                foreach (TcpClient c in clientes)
                {
                    try
                    {
                        c.GetStream().Write(datos, 0, datos.Length);
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                try
                {
                    miCliente.GetStream().Write(datos, 0, datos.Length);
                }
                catch
                {
                }
            }
        }

        private async void Escuchar(TcpClient cliente)
        {
            byte[] buffer = new byte[1024];

            while (cliente != null)
            {
                int bytes =
                    await cliente.GetStream()
                    .ReadAsync(
                        buffer,
                        0,
                        buffer.Length);

                if (bytes <= 0)
                    break;

                string mensaje =
                    Encoding.UTF8.GetString(
                        buffer,
                        0,
                        bytes);

                MensajeRecibido?.Invoke(mensaje);

                if (EsHost)
                {
                    ReenviarATodos(
                        mensaje,
                        cliente);
                }
            }
        }

        private void ReenviarATodos(
    string mensaje,
    TcpClient remitente)
        {
            byte[] datos =
                Encoding.UTF8.GetBytes(mensaje);

            foreach (TcpClient c in clientes)
            {
                if (c == remitente)
                    continue;

                try
                {
                    c.GetStream().Write(
                        datos,
                        0,
                        datos.Length);
                }
                catch
                {
                }
            }
        }
    }
}