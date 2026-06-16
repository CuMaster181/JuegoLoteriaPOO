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

        // Buffer acumulador para manejar TCP coalescing
        private string bufferPendiente = "";

        public bool EsHost { get; private set; }
        public bool EstaActivo { get; private set; }

        public async Task IniciarServidor(int puerto)
        {
            EsHost = true;

            servidor = new TcpListener(
                IPAddress.Any,
                puerto);

            servidor.Start();
            EstaActivo = true;

            while (EstaActivo)
            {
                TcpClient cliente;
                try
                {
                    cliente = await servidor.AcceptTcpClientAsync();
                }
                catch
                {
                    break;
                }

                clientes.Add(cliente);

                Escuchar(cliente, new StringBuilder());
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

            EstaActivo = true;
            Escuchar(miCliente, new StringBuilder());
        }

        public void Enviar(string mensaje)
        {
            // Agregar separador de mensaje para evitar TCP coalescing
            byte[] datos = Encoding.UTF8.GetBytes(mensaje + "\n");

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

        private async void Escuchar(TcpClient cliente, StringBuilder acumulador)
        {
            byte[] buffer = new byte[4096];

            while (cliente != null)
            {
                int bytes;
                try
                {
                    bytes = await cliente.GetStream()
                        .ReadAsync(buffer, 0, buffer.Length);
                }
                catch
                {
                    break;
                }

                if (bytes <= 0)
                    break;

                string recibido = Encoding.UTF8.GetString(buffer, 0, bytes);
                acumulador.Append(recibido);

                // Procesar todos los mensajes completos (separados por \n)
                string acumulado = acumulador.ToString();
                int pos;
                while ((pos = acumulado.IndexOf('\n')) >= 0)
                {
                    string mensaje = acumulado[..pos].Trim();
                    acumulado = acumulado[(pos + 1)..];

                    if (!string.IsNullOrWhiteSpace(mensaje))
                    {
                        MensajeRecibido?.Invoke(mensaje);

                        if (EsHost && EsMensajeClientePermitido(mensaje))
                            ReenviarATodos(mensaje + "\n", cliente);
                    }
                }

                acumulador.Clear();
                acumulador.Append(acumulado);
            }
        }

        private void ReenviarATodos(string mensaje, TcpClient remitente)
        {
            byte[] datos = Encoding.UTF8.GetBytes(mensaje);

            foreach (TcpClient c in clientes)
            {
                if (c == remitente)
                    continue;

                try
                {
                    c.GetStream().Write(datos, 0, datos.Length);
                }
                catch
                {
                }
            }
        }

        private bool EsMensajeClientePermitido(string mensaje)
        {
            string tipo = mensaje.Split('|')[0];
            return tipo is "CHAT"
                or "LOTERIA"
                or "LISTO_GANADOR"
                or "CONFIG_REQUEST";
        }

        public void Desconectar()
        {
            EstaActivo = false;

            try
            {
                if (servidor != null)
                    servidor.Stop();
            }
            catch { }

            foreach (TcpClient c in clientes)
            {
                try { c.Close(); } catch { }
            }
            clientes.Clear();

            try
            {
                if (miCliente != null)
                    miCliente.Close();
            }
            catch { }
        }
    }
}
