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

        public UcConexion(Jugador jugador, TipoPartida tipoPartida)
        {
            InitializeComponent();
            this.jugador = jugador;
            this.tipoPartida = tipoPartida;

            txtPuerto.Text = "5000";

            // Suscribir el cambio de modo Host/Cliente
            rbHost.CheckedChanged += RbModo_CheckedChanged;
            rbCliente.CheckedChanged += RbModo_CheckedChanged;

            // Estado inicial segun seleccion actual
            ActualizarEstadoTablasDobles();
        }

        private void RbModo_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarEstadoTablasDobles();
        }

        private void ActualizarEstadoTablasDobles()
        {
            // Dejar habilitado para todos, para que el cliente pueda elegir si desea activar tablas dobles
            chkTablasDobles.Enabled = true;
            chkTablasDobles.Text = "Tablas Dobles";
        }

        private void bttnConectar_Click(object sender, EventArgs e)
        {
            if (!rbHost.Checked && !rbCliente.Checked)
            {
                MessageBox.Show("Selecciona si eres Host o Cliente.");
                return;
            }

            // Ambos (Host y Cliente) definen su intencion de TablasDobles localmente
            configuracion.TablasDobles = chkTablasDobles.Checked;

            Conexion conexion = new Conexion
            {
                IP = txtIP.Text.Trim(),
                Puerto = int.Parse(txtPuerto.Text),
                EsHost = rbHost.Checked
            };

            ConexionCompletada?.Invoke(jugador, conexion, tipoPartida, configuracion);
        }

        private void bttncancelar_Click(object sender, EventArgs e)
        {

            if (FindForm() is FormMenuPrincipal menu)
            {
                menu.CerrarUC();
            }
        }
    }
}
