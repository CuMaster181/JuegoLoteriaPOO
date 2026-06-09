namespace JuegoLoteriaPOO
{
    partial class UcGanador
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            pnlPrincipal = new Panel();
            btnContinuar = new Button();
            dgvRanking = new DataGridView();
            lblGanador = new Label();
            pbTrofeo = new PictureBox();
            pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRanking).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbTrofeo).BeginInit();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.WhiteSmoke;
            pnlPrincipal.Controls.Add(btnContinuar);
            pnlPrincipal.Controls.Add(dgvRanking);
            pnlPrincipal.Controls.Add(lblGanador);
            pnlPrincipal.Controls.Add(pbTrofeo);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(334, 489);
            pnlPrincipal.TabIndex = 0;
            // 
            // btnContinuar
            // 
            btnContinuar.Location = new Point(236, 454);
            btnContinuar.Name = "btnContinuar";
            btnContinuar.Size = new Size(75, 23);
            btnContinuar.TabIndex = 3;
            btnContinuar.Text = "Continuar";
            btnContinuar.UseVisualStyleBackColor = true;
            btnContinuar.Click += btnContinuar_Click;
            // 
            // dgvRanking
            // 
            dgvRanking.AllowUserToAddRows = false;
            dgvRanking.AllowUserToDeleteRows = false;
            dgvRanking.AllowUserToResizeColumns = false;
            dgvRanking.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRanking.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRanking.Location = new Point(20, 134);
            dgvRanking.MultiSelect = false;
            dgvRanking.Name = "dgvRanking";
            dgvRanking.ReadOnly = true;
            dgvRanking.RowHeadersVisible = false;
            dgvRanking.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRanking.Size = new Size(291, 314);
            dgvRanking.TabIndex = 2;
            // 
            // lblGanador
            // 
            lblGanador.Font = new Font("Segoe UI", 15F, FontStyle.Italic);
            lblGanador.Location = new Point(90, 65);
            lblGanador.Name = "lblGanador";
            lblGanador.Size = new Size(198, 23);
            lblGanador.TabIndex = 1;
            lblGanador.Text = "¡Tenemos Ganador!";
            lblGanador.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pbTrofeo
            // 
            pbTrofeo.Image = Properties.Resources.Trofeo;
            pbTrofeo.Location = new Point(20, 37);
            pbTrofeo.Name = "pbTrofeo";
            pbTrofeo.Size = new Size(64, 68);
            pbTrofeo.SizeMode = PictureBoxSizeMode.Zoom;
            pbTrofeo.TabIndex = 0;
            pbTrofeo.TabStop = false;
            // 
            // UcGanador
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "UcGanador";
            Size = new Size(334, 489);
            pnlPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRanking).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbTrofeo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private PictureBox pbTrofeo;
        private Label lblGanador;
        private DataGridView dgvRanking;
        private Button btnContinuar;
    }
}
