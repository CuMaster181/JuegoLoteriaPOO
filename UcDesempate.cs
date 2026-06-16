using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuegoLoteriaPOO
{
    public partial class UcDesempate : UserControl
    {
        public event Action? ContinuarPartida;

        public event Action? CartaMayor;
        public UcDesempate(List<string> empatados, bool esHost = true)
        {
            InitializeComponent();
            lstEmpatados.Items.Clear();

            foreach (string nombre in empatados)
            {
                lstEmpatados.Items.Add(nombre);
            }

            if (!esHost)
            {
                btnCartaMayor.Enabled = false;
                btnContinuar.Enabled = false;
                lblEmpate.Text = "Esperando decisión del Host...";
            }
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            ContinuarPartida?.Invoke();
        }

        private void btnCartaMayor_Click(object sender, EventArgs e)
        {
            CartaMayor?.Invoke();
        }
    }
}
