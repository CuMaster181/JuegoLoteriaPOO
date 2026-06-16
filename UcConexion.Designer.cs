namespace JuegoLoteriaPOO
{
    partial class UcConexion
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        private void InitializeComponent()
        {
            txtIP = new TextBox();
            txtPuerto = new TextBox();
            bttnConectar = new Button();
            bttncancelar = new Button();
            label1 = new Label();
            label2 = new Label();
            rbHost = new RadioButton();
            rbCliente = new RadioButton();
            lblNumeroTablas = new Label();
            nudNumeroTablas = new NumericUpDown();
            chkTablasDobles = new CheckBox();
            lblFiguras = new Label();
            clbFiguras = new CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)nudNumeroTablas).BeginInit();
            SuspendLayout();
            // 
            // txtIP
            // 
            txtIP.Location = new Point(154, 60);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(170, 23);
            txtIP.TabIndex = 0;
            // 
            // txtPuerto
            // 
            txtPuerto.Location = new Point(154, 114);
            txtPuerto.Name = "txtPuerto";
            txtPuerto.Size = new Size(170, 23);
            txtPuerto.TabIndex = 1;
            // 
            // bttnConectar
            // 
            bttnConectar.Location = new Point(154, 497);
            bttnConectar.Name = "bttnConectar";
            bttnConectar.Size = new Size(170, 23);
            bttnConectar.TabIndex = 2;
            bttnConectar.Text = "Conectar";
            bttnConectar.UseVisualStyleBackColor = true;
            bttnConectar.Click += bttnConectar_Click;
            // 
            // bttncancelar
            // 
            bttncancelar.Location = new Point(154, 532);
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
            label1.Location = new Point(154, 42);
            label1.Name = "label1";
            label1.Size = new Size(20, 15);
            label1.TabIndex = 4;
            label1.Text = "IP:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(154, 96);
            label2.Name = "label2";
            label2.Size = new Size(52, 15);
            label2.TabIndex = 5;
            label2.Text = "PUERTO:";
            // 
            // rbHost
            // 
            rbHost.AutoSize = true;
            rbHost.Location = new Point(22, 250);
            rbHost.Name = "rbHost";
            rbHost.Size = new Size(77, 19);
            rbHost.TabIndex = 6;
            rbHost.TabStop = true;
            rbHost.Text = "Crear Sala";
            rbHost.UseVisualStyleBackColor = true;
            // 
            // rbCliente
            // 
            rbCliente.AutoSize = true;
            rbCliente.Location = new Point(22, 275);
            rbCliente.Name = "rbCliente";
            rbCliente.Size = new Size(91, 19);
            rbCliente.TabIndex = 7;
            rbCliente.TabStop = true;
            rbCliente.Text = "Unirse a Sala";
            rbCliente.UseVisualStyleBackColor = true;
            // 
            // lblNumeroTablas
            // 
            lblNumeroTablas.AutoSize = true;
            lblNumeroTablas.Location = new Point(154, 149);
            lblNumeroTablas.Name = "lblNumeroTablas";
            lblNumeroTablas.Size = new Size(104, 15);
            lblNumeroTablas.TabIndex = 8;
            lblNumeroTablas.Text = "Número de tablas:";
            // 
            // nudNumeroTablas
            // 
            nudNumeroTablas.Location = new Point(274, 147);
            nudNumeroTablas.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            nudNumeroTablas.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudNumeroTablas.Name = "nudNumeroTablas";
            nudNumeroTablas.Size = new Size(50, 23);
            nudNumeroTablas.TabIndex = 9;
            nudNumeroTablas.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // chkTablasDobles
            // 
            chkTablasDobles.AutoSize = true;
            chkTablasDobles.Location = new Point(154, 179);
            chkTablasDobles.Name = "chkTablasDobles";
            chkTablasDobles.Size = new Size(98, 19);
            chkTablasDobles.TabIndex = 10;
            chkTablasDobles.Text = "Tablas Dobles";
            chkTablasDobles.UseVisualStyleBackColor = true;
            // 
            // lblFiguras
            // 
            lblFiguras.AutoSize = true;
            lblFiguras.Location = new Point(154, 209);
            lblFiguras.Name = "lblFiguras";
            lblFiguras.Size = new Size(98, 15);
            lblFiguras.TabIndex = 11;
            lblFiguras.Text = "Formas de ganar:";
            // 
            // clbFiguras
            // 
            clbFiguras.CheckOnClick = true;
            clbFiguras.FormattingEnabled = true;
            clbFiguras.Location = new Point(154, 227);
            clbFiguras.Name = "clbFiguras";
            clbFiguras.Size = new Size(196, 238);
            clbFiguras.TabIndex = 12;
            // 
            // UcConexion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(clbFiguras);
            Controls.Add(lblFiguras);
            Controls.Add(chkTablasDobles);
            Controls.Add(nudNumeroTablas);
            Controls.Add(lblNumeroTablas);
            Controls.Add(rbCliente);
            Controls.Add(rbHost);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(bttncancelar);
            Controls.Add(bttnConectar);
            Controls.Add(txtPuerto);
            Controls.Add(txtIP);
            Name = "UcConexion";
            Size = new Size(482, 590);
            ((System.ComponentModel.ISupportInitialize)nudNumeroTablas).EndInit();
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
        private RadioButton rbHost;
        private RadioButton rbCliente;
        private Label lblNumeroTablas;
        private NumericUpDown nudNumeroTablas;
        private CheckBox chkTablasDobles;
        private Label lblFiguras;
        private CheckedListBox clbFiguras;
    }
}
