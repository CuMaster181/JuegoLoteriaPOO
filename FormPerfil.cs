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
    public partial class FormPerfil : Form
    {
        public FormPerfil()
        {
            InitializeComponent();
        }

        private void bttnContinuar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show(
                    "Ingresa un nombre.");
                return;
            }

            Jugador jugador = new Jugador(txtNombre.Text);

            FormCrearTabla frm = new FormCrearTabla();

            frm.Show();

            this.Hide();
        }
    }
}
