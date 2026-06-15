namespace JuegoLoteriaPOO
{
    partial class UcDesempate
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
            pnlFondo = new Panel();
            btnCartaMayor = new Button();
            btnContinuar = new Button();
            lstEmpatados = new ListBox();
            lblEmpate = new Label();
            pnlFondo.SuspendLayout();
            SuspendLayout();
            // 
            // pnlFondo
            // 
            pnlFondo.BackColor = Color.White;
            pnlFondo.Controls.Add(btnCartaMayor);
            pnlFondo.Controls.Add(btnContinuar);
            pnlFondo.Controls.Add(lstEmpatados);
            pnlFondo.Controls.Add(lblEmpate);
            pnlFondo.Dock = DockStyle.Fill;
            pnlFondo.Location = new Point(0, 0);
            pnlFondo.Name = "pnlFondo";
            pnlFondo.Size = new Size(334, 489);
            pnlFondo.TabIndex = 0;
            // 
            // btnCartaMayor
            // 
            btnCartaMayor.Location = new Point(33, 304);
            btnCartaMayor.Name = "btnCartaMayor";
            btnCartaMayor.Size = new Size(124, 145);
            btnCartaMayor.TabIndex = 3;
            btnCartaMayor.Text = "Carta Mayor";
            btnCartaMayor.UseVisualStyleBackColor = true;
            btnCartaMayor.Click += btnCartaMayor_Click;
            // 
            // btnContinuar
            // 
            btnContinuar.Location = new Point(171, 304);
            btnContinuar.Name = "btnContinuar";
            btnContinuar.Size = new Size(124, 145);
            btnContinuar.TabIndex = 2;
            btnContinuar.Text = "Continuar Partida";
            btnContinuar.UseVisualStyleBackColor = true;
            btnContinuar.Click += btnContinuar_Click;
            // 
            // lstEmpatados
            // 
            lstEmpatados.FormattingEnabled = true;
            lstEmpatados.ItemHeight = 15;
            lstEmpatados.Location = new Point(33, 69);
            lstEmpatados.Name = "lstEmpatados";
            lstEmpatados.Size = new Size(262, 229);
            lstEmpatados.TabIndex = 1;
            // 
            // lblEmpate
            // 
            lblEmpate.AutoSize = true;
            lblEmpate.Font = new Font("Agency FB", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEmpate.Location = new Point(76, 17);
            lblEmpate.Name = "lblEmpate";
            lblEmpate.Size = new Size(176, 35);
            lblEmpate.TabIndex = 0;
            lblEmpate.Text = "Empate Detectado";
            // 
            // UcDesempate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlFondo);
            Name = "UcDesempate";
            Size = new Size(334, 489);
            pnlFondo.ResumeLayout(false);
            pnlFondo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlFondo;
        private Label lblEmpate;
        private ListBox lstEmpatados;
        private Button btnCartaMayor;
        private Button btnContinuar;
    }
}
