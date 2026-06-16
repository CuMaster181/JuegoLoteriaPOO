using System;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuegoLoteriaPOO
{
    public partial class UcConexion : UserControl
    {

        public event Action<Jugador, Conexion, TipoPartida, ConfiguracionPartida>? ConexionCompletada;
        private Jugador jugador;
        private ConfiguracionPartida configuracion = new ConfiguracionPartida();
        private TipoPartida tipoPartida;

        private static readonly Dictionary<TipoVictoria, string> NombresFiguras = new()
        {
            { TipoVictoria.Horizontal, "Horizontal" },
            { TipoVictoria.Vertical, "Vertical" },
            { TipoVictoria.DiagonalPrincipal, "Diagonal Principal" },
            { TipoVictoria.DiagonalSecundaria, "Diagonal Secundaria" },
            { TipoVictoria.Cruzita, "Cruzita" },
            { TipoVictoria.T, "T" },
            { TipoVictoria.Pollita, "Pollita" },
            { TipoVictoria.L, "L" },
            { TipoVictoria.J, "J" },
            { TipoVictoria.TablaCompleta, "Tabla Completa" },
        };

        public UcConexion(Jugador jugador, TipoPartida tipoPartida)
        {
            InitializeComponent();
            this.jugador = jugador;
            this.tipoPartida = tipoPartida;

            txtPuerto.Text = "5000";

            CargarFigurasDisponibles();

            rbHost.CheckedChanged += RbModo_CheckedChanged;
            rbCliente.CheckedChanged += RbModo_CheckedChanged;

            ActualizarEstadoOpcionesHost();
        }

        private void CargarFigurasDisponibles()
        {
            clbFiguras.Items.Clear();

            foreach (TipoVictoria tipo in Enum.GetValues(typeof(TipoVictoria)))
            {
                string texto = NombresFiguras.TryGetValue(tipo, out string? nombre) ? nombre : tipo.ToString();
                clbFiguras.Items.Add(texto);
            }

            for (int i = 0; i < clbFiguras.Items.Count; i++)
                clbFiguras.SetItemChecked(i, true);
        }

        private void RbModo_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarEstadoOpcionesHost();
        }

        private void ActualizarEstadoOpcionesHost()
        {
            bool esHost = rbHost.Checked;

            // Número de tablas — solo el Host
            lblNumeroTablas.Enabled = esHost;
            nudNumeroTablas.Enabled = esHost;

            // Tablas dobles — solo el Host
            chkTablasDobles.Enabled = esHost;

            // Formas de ganar — solo el Host
            lblFiguras.Enabled = esHost;
            clbFiguras.Enabled = esHost;

            if (!esHost)
            {
                nudNumeroTablas.Value = 1;
                chkTablasDobles.Checked = false;

                for (int i = 0; i < clbFiguras.Items.Count; i++)
                    clbFiguras.SetItemChecked(i, true);
            }
        }

        private async void bttnConectar_Click(object sender, EventArgs e)
        {
            if (!rbHost.Checked && !rbCliente.Checked)
            {
                MessageBox.Show("Selecciona si eres Host o Cliente.");
                return;
            }

            bttnConectar.Enabled = false;

            if (rbHost.Checked)
            {
                configuracion.NumeroTablas = (int)nudNumeroTablas.Value;
                configuracion.TablasDobles = chkTablasDobles.Checked;

                HashSet<TipoVictoria> figurasSeleccionadas = new HashSet<TipoVictoria>();
                TipoVictoria[] tipos = (TipoVictoria[])Enum.GetValues(typeof(TipoVictoria));

                for (int i = 0; i < clbFiguras.Items.Count && i < tipos.Length; i++)
                {
                    if (clbFiguras.GetItemChecked(i))
                        figurasSeleccionadas.Add(tipos[i]);
                }

                if (figurasSeleccionadas.Count == 0)
                {
                    MessageBox.Show("Selecciona al menos una forma de ganar.");
                    bttnConectar.Enabled = true;
                    return;
                }

                configuracion.FigurasHabilitadas = figurasSeleccionadas;
            }

            Conexion conexion = new Conexion
            {
                IP = txtIP.Text.Trim(),
                Puerto = int.Parse(txtPuerto.Text),
                EsHost = rbHost.Checked
            };

            GestorMultijugador red = new GestorMultijugador();
            conexion.JugadoresConectados.Add(jugador.Nombre);
            conexion.Red = red;

            try
            {
                if (conexion.EsHost)
                {
                    red.MensajeRecibido += mensaje =>
                    {
                        if (mensaje.StartsWith("CONFIG_REQUEST|"))
                        {
                            string[] partes = mensaje.Split('|');
                            if (partes.Length > 1)
                                conexion.JugadoresConectados.Add(partes[1]);
                            red.Enviar(CrearMensajeConfiguracion());
                        }
                    };

                    _ = red.IniciarServidor(conexion.Puerto);
                    ConexionCompletada?.Invoke(jugador, conexion, tipoPartida, configuracion);
                }
                else
                {
                    TaskCompletionSource<ConfiguracionPartida> configRecibida = new TaskCompletionSource<ConfiguracionPartida>();

                    red.MensajeRecibido += mensaje =>
                    {
                        if (mensaje.StartsWith("CONFIG|"))
                        {
                            ConfiguracionPartida cfg = LeerConfiguracionDesdeMensaje(mensaje);
                            configRecibida.TrySetResult(cfg);
                        }
                    };

                    await red.Conectar(conexion.IP, conexion.Puerto);
                    red.Enviar($"CONFIG_REQUEST|{jugador.Nombre}");

                    Task espera = await Task.WhenAny(configRecibida.Task, Task.Delay(5000));
                    if (espera != configRecibida.Task)
                    {
                        red.Desconectar();
                        MessageBox.Show("No se recibió la configuración del Host.");
                        bttnConectar.Enabled = true;
                        return;
                    }

                    configuracion = await configRecibida.Task;
                    ConexionCompletada?.Invoke(jugador, conexion, tipoPartida, configuracion);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bttnConectar.Enabled = true;
            }
        }

        private string CrearMensajeConfiguracion()
        {
            string valDobles = configuracion.TablasDobles ? "true" : "false";
            string valNumTablas = configuracion.NumeroTablas.ToString();
            string valFiguras = configuracion.SerializarFiguras();
            return $"CONFIG|{valDobles}|{valNumTablas}|{valFiguras}";
        }

        private ConfiguracionPartida LeerConfiguracionDesdeMensaje(string mensaje)
        {
            string[] partes = mensaje.Split('|');
            ConfiguracionPartida cfg = new ConfiguracionPartida();

            if (partes.Length > 1)
                cfg.TablasDobles = bool.Parse(partes[1]);

            if (partes.Length > 2 && int.TryParse(partes[2], out int numTablas))
                cfg.NumeroTablas = Math.Max(1, Math.Min(4, numTablas));

            if (partes.Length > 3)
            {
                HashSet<TipoVictoria> figuras = ConfiguracionPartida.DeserializarFiguras(partes[3]);
                cfg.FigurasHabilitadas = figuras.Count > 0
                    ? figuras
                    : ConfiguracionPartida.ObtenerTodasLasFiguras();
            }

            return cfg;
        }

        private void bttncancelar_Click(object sender, EventArgs e)
        {
            if (FindForm() is FormMenuPrincipal menu)
                menu.CerrarUC();
        }
    }
}
