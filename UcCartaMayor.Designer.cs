namespace JuegoLoteriaPOO
{
    partial class UcCartaMayor
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
            dgvResultados = new DataGridView();
            lblTitulo = new Label();
            lblGanador = new Label();
            pbCartaGanadora = new PictureBox();
            btnContinuar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbCartaGanadora).BeginInit();
            SuspendLayout();
            // 
            // dgvResultados
            // 
            dgvResultados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResultados.Location = new Point(28, 83);
            dgvResultados.Name = "dgvResultados";
            dgvResultados.Size = new Size(273, 195);
            dgvResultados.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Agency FB", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(28, 16);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(273, 35);
            lblTitulo.TabIndex = 3;
            lblTitulo.Text = "Desempate por carta Mayor";
            // 
            // lblGanador
            // 
            lblGanador.AutoSize = true;
            lblGanador.Font = new Font("Agency FB", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblGanador.Location = new Point(28, 405);
            lblGanador.Name = "lblGanador";
            lblGanador.Size = new Size(30, 35);
            lblGanador.TabIndex = 4;
            lblGanador.Text = "...";
            // 
            // pbCartaGanadora
            // 
            pbCartaGanadora.Location = new Point(112, 284);
            pbCartaGanadora.Name = "pbCartaGanadora";
            pbCartaGanadora.Size = new Size(90, 119);
            pbCartaGanadora.TabIndex = 5;
            pbCartaGanadora.TabStop = false;
            // 
            // btnContinuar
            // 
            btnContinuar.Location = new Point(210, 454);
            btnContinuar.Name = "btnContinuar";
            btnContinuar.Size = new Size(121, 32);
            btnContinuar.TabIndex = 6;
            btnContinuar.Text = "Continuar";
            btnContinuar.UseVisualStyleBackColor = true;
            btnContinuar.Click += btnContinuar_Click;
            // 
            // UcCartaMayor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnContinuar);
            Controls.Add(pbCartaGanadora);
            Controls.Add(lblGanador);
            Controls.Add(lblTitulo);
            Controls.Add(dgvResultados);
            Name = "UcCartaMayor";
            Size = new Size(334, 489);
            ((System.ComponentModel.ISupportInitialize)dgvResultados).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbCartaGanadora).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvResultados;
        private Label label2;
        private Label lblTitulo;
        private Label lblGanador;
        private PictureBox pbCartaGanadora;
        private Button btnContinuar;
    }
}
