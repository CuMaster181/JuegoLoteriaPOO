namespace JuegoLoteriaPOO
{
    partial class UcConexion
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
            txtIP = new TextBox();
            txtPuerto = new TextBox();
            bttnConectar = new Button();
            bttncancelar = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // txtIP
            // 
            txtIP.Location = new Point(69, 98);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(170, 23);
            txtIP.TabIndex = 0;
            // 
            // txtPuerto
            // 
            txtPuerto.Location = new Point(69, 152);
            txtPuerto.Name = "txtPuerto";
            txtPuerto.Size = new Size(170, 23);
            txtPuerto.TabIndex = 1;
            // 
            // bttnConectar
            // 
            bttnConectar.Location = new Point(69, 236);
            bttnConectar.Name = "bttnConectar";
            bttnConectar.Size = new Size(170, 23);
            bttnConectar.TabIndex = 2;
            bttnConectar.Text = "Conectar";
            bttnConectar.UseVisualStyleBackColor = true;
            bttnConectar.Click += bttnConectar_Click;
            // 
            // bttncancelar
            // 
            bttncancelar.Location = new Point(69, 288);
            bttncancelar.Name = "bttncancelar";
            bttncancelar.Size = new Size(170, 23);
            bttncancelar.TabIndex = 3;
            bttncancelar.Text = "Cancelar";
            bttncancelar.UseVisualStyleBackColor = true;
            bttncancelar.Click += bttncancelar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(69, 80);
            label1.Name = "label1";
            label1.Size = new Size(20, 15);
            label1.TabIndex = 4;
            label1.Text = "IP:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(69, 134);
            label2.Name = "label2";
            label2.Size = new Size(52, 15);
            label2.TabIndex = 5;
            label2.Text = "PUERTO:";
            // 
            // UcConexion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(bttncancelar);
            Controls.Add(bttnConectar);
            Controls.Add(txtPuerto);
            Controls.Add(txtIP);
            Name = "UcConexion";
            Size = new Size(334, 489);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtIP;
        private TextBox txtPuerto;
        private Button bttnConectar;
        private Button bttncancelar;
        private Label label1;
        private Label label2;
    }
}
