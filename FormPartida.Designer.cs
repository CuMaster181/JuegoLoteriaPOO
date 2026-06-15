namespace JuegoLoteriaPOO
{
    partial class FormPartida
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblNombreCarta = new Label();
            pbCartaActual = new PictureBox();
            bttnPausa = new Button();
            fileSystemWatcher1 = new FileSystemWatcher();
            label1 = new Label();
            timerCartas = new System.Windows.Forms.Timer(components);
            tlpTablaPartida = new TableLayoutPanel();
            pbFicha = new PictureBox();
            ntnLoteria = new Button();
            lblContador = new Label();
            flpHistorial = new FlowLayoutPanel();
            pnlGanador = new Panel();
            gbChat = new GroupBox();
            btnEnviar = new Button();
            txtMensaje = new TextBox();
            lstChat = new ListBox();
            btnAumento = new Button();
            btnResta = new Button();
            btnSiguiente = new Button();
            lblProximaCarta = new Label();
            lblFormadeGanar = new Label();
            ((System.ComponentModel.ISupportInitialize)pbCartaActual).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbFicha).BeginInit();
            gbChat.SuspendLayout();
            SuspendLayout();
            // 
            // lblNombreCarta
            // 
            lblNombreCarta.AutoSize = true;
            lblNombreCarta.Location = new Point(12, 9);
            lblNombreCarta.Name = "lblNombreCarta";
            lblNombreCarta.Size = new Size(16, 15);
            lblNombreCarta.TabIndex = 0;
            lblNombreCarta.Text = "...";
            // 
            // pbCartaActual
            // 
            pbCartaActual.Image = Properties.Resources.CartaPosterior;
            pbCartaActual.Location = new Point(12, 27);
            pbCartaActual.Name = "pbCartaActual";
            pbCartaActual.Size = new Size(80, 109);
            pbCartaActual.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCartaActual.TabIndex = 1;
            pbCartaActual.TabStop = false;
            // 
            // bttnPausa
            // 
            bttnPausa.Location = new Point(116, 104);
            bttnPausa.Name = "bttnPausa";
            bttnPausa.Size = new Size(75, 23);
            bttnPausa.TabIndex = 2;
            bttnPausa.Text = "iniciar";
            bttnPausa.UseVisualStyleBackColor = true;
            bttnPausa.Click += bttnPausa_Click;
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(223, 9);
            label1.Name = "label1";
            label1.Size = new Size(103, 15);
            label1.TabIndex = 4;
            label1.Text = "Historial de Cartas";
            // 
            // timerCartas
            // 
            timerCartas.Interval = 3000;
            timerCartas.Tick += timerCartas_Tick;
            // 
            // tlpTablaPartida
            // 
            tlpTablaPartida.ColumnCount = 5;
            tlpTablaPartida.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpTablaPartida.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpTablaPartida.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpTablaPartida.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpTablaPartida.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpTablaPartida.Location = new Point(12, 146);
            tlpTablaPartida.Name = "tlpTablaPartida";
            tlpTablaPartida.RowCount = 5;
            tlpTablaPartida.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpTablaPartida.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpTablaPartida.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpTablaPartida.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpTablaPartida.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpTablaPartida.Size = new Size(382, 352);
            tlpTablaPartida.TabIndex = 7;
            // 
            // pbFicha
            // 
            pbFicha.Image = Properties.Resources.Ficha;
            pbFicha.Location = new Point(411, 365);
            pbFicha.Name = "pbFicha";
            pbFicha.Size = new Size(100, 73);
            pbFicha.SizeMode = PictureBoxSizeMode.StretchImage;
            pbFicha.TabIndex = 8;
            pbFicha.TabStop = false;
            pbFicha.Click += pbFicha_Click;
            pbFicha.MouseDown += pbFicha_MouseDown;
            // 
            // ntnLoteria
            // 
            ntnLoteria.Location = new Point(570, 383);
            ntnLoteria.Name = "ntnLoteria";
            ntnLoteria.Size = new Size(164, 59);
            ntnLoteria.TabIndex = 9;
            ntnLoteria.Text = "BUENAS!!!";
            ntnLoteria.UseVisualStyleBackColor = true;
            ntnLoteria.Click += btnLoteria_Click;
            // 
            // lblContador
            // 
            lblContador.AutoSize = true;
            lblContador.Location = new Point(459, 9);
            lblContador.Name = "lblContador";
            lblContador.Size = new Size(30, 15);
            lblContador.TabIndex = 10;
            lblContador.Text = "0/54";
            // 
            // flpHistorial
            // 
            flpHistorial.AutoScroll = true;
            flpHistorial.FlowDirection = FlowDirection.BottomUp;
            flpHistorial.Location = new Point(223, 27);
            flpHistorial.Name = "flpHistorial";
            flpHistorial.Size = new Size(266, 100);
            flpHistorial.TabIndex = 11;
            // 
            // pnlGanador
            // 
            pnlGanador.Enabled = false;
            pnlGanador.Location = new Point(230, 12);
            pnlGanador.Name = "pnlGanador";
            pnlGanador.Size = new Size(334, 489);
            pnlGanador.TabIndex = 0;
            // 
            // gbChat
            // 
            gbChat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            gbChat.Controls.Add(btnEnviar);
            gbChat.Controls.Add(txtMensaje);
            gbChat.Controls.Add(lstChat);
            gbChat.Location = new Point(532, 27);
            gbChat.Name = "gbChat";
            gbChat.Size = new Size(250, 350);
            gbChat.TabIndex = 12;
            gbChat.TabStop = false;
            gbChat.Text = "Chat";
            // 
            // btnEnviar
            // 
            btnEnviar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEnviar.Location = new Point(190, 314);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(49, 23);
            btnEnviar.TabIndex = 2;
            btnEnviar.Text = "enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // txtMensaje
            // 
            txtMensaje.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtMensaje.Location = new Point(6, 314);
            txtMensaje.Name = "txtMensaje";
            txtMensaje.Size = new Size(178, 23);
            txtMensaje.TabIndex = 1;
            // 
            // lstChat
            // 
            lstChat.Dock = DockStyle.Fill;
            lstChat.FormattingEnabled = true;
            lstChat.ItemHeight = 15;
            lstChat.Location = new Point(3, 19);
            lstChat.Name = "lstChat";
            lstChat.Size = new Size(244, 328);
            lstChat.TabIndex = 0;
            // 
            // btnAumento
            // 
            btnAumento.Location = new Point(98, 57);
            btnAumento.Name = "btnAumento";
            btnAumento.Size = new Size(33, 23);
            btnAumento.TabIndex = 13;
            btnAumento.Text = "+";
            btnAumento.UseVisualStyleBackColor = true;
            btnAumento.Click += btnAumento_Click;
            // 
            // btnResta
            // 
            btnResta.Location = new Point(184, 57);
            btnResta.Name = "btnResta";
            btnResta.Size = new Size(33, 23);
            btnResta.TabIndex = 14;
            btnResta.Text = "-";
            btnResta.UseVisualStyleBackColor = true;
            btnResta.Click += btnResta_Click;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Location = new Point(137, 57);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(33, 23);
            btnSiguiente.TabIndex = 15;
            btnSiguiente.Text = "Sig.";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // lblProximaCarta
            // 
            lblProximaCarta.AutoSize = true;
            lblProximaCarta.Location = new Point(98, 27);
            lblProximaCarta.Name = "lblProximaCarta";
            lblProximaCarta.Size = new Size(16, 15);
            lblProximaCarta.TabIndex = 16;
            lblProximaCarta.Text = "...";
            lblProximaCarta.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblFormadeGanar
            // 
            lblFormadeGanar.AutoSize = true;
            lblFormadeGanar.Location = new Point(575, 447);
            lblFormadeGanar.Name = "lblFormadeGanar";
            lblFormadeGanar.Size = new Size(16, 15);
            lblFormadeGanar.TabIndex = 17;
            lblFormadeGanar.Text = "...";
            // 
            // FormPartida
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 504);
            Controls.Add(lblFormadeGanar);
            Controls.Add(lblProximaCarta);
            Controls.Add(btnSiguiente);
            Controls.Add(btnResta);
            Controls.Add(btnAumento);
            Controls.Add(pnlGanador);
            Controls.Add(gbChat);
            Controls.Add(flpHistorial);
            Controls.Add(lblContador);
            Controls.Add(ntnLoteria);
            Controls.Add(pbFicha);
            Controls.Add(tlpTablaPartida);
            Controls.Add(label1);
            Controls.Add(bttnPausa);
            Controls.Add(pbCartaActual);
            Controls.Add(lblNombreCarta);
            Name = "FormPartida";
            Text = "Form1";
            Load += FormPartida_Load;
            ((System.ComponentModel.ISupportInitialize)pbCartaActual).EndInit();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbFicha).EndInit();
            gbChat.ResumeLayout(false);
            gbChat.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNombreCarta;
        private PictureBox pbCartaActual;
        private Button bttnPausa;
        private FileSystemWatcher fileSystemWatcher1;
        private Label label1;
        private System.Windows.Forms.Timer timerCartas;
        private TableLayoutPanel tlpTablaPartida;
        private PictureBox pbFicha;
        private Button ntnLoteria;
        private Label lblContador;
        private FlowLayoutPanel flpHistorial;
        private GroupBox gbChat;
        private ListBox lstChat;
        private Button btnEnviar;
        private TextBox txtMensaje;
        private Panel pnlGanador;
        private Button btnResta;
        private Button btnAumento;
        private Button btnSiguiente;
        private Label lblProximaCarta;
        private Label lblFormadeGanar;
    }
}
