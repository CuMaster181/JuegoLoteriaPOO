namespace JuegoLoteriaPOO
{
    partial class FormPartida
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            cbVelocidad = new ComboBox();
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
            pbCartaActual.Location = new Point(12, 27);
            pbCartaActual.Name = "pbCartaActual";
            pbCartaActual.Size = new Size(80, 109);
            pbCartaActual.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCartaActual.TabIndex = 1;
            pbCartaActual.TabStop = false;
            // 
            // bttnPausa
            // 
            bttnPausa.Location = new Point(115, 75);
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
            // cbVelocidad
            // 
            cbVelocidad.FormattingEnabled = true;
            cbVelocidad.Location = new Point(96, 46);
            cbVelocidad.Name = "cbVelocidad";
            cbVelocidad.Size = new Size(121, 23);
            cbVelocidad.TabIndex = 6;
            cbVelocidad.SelectedIndexChanged += cbVelocidad_SelectedIndexChanged;
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
            tlpTablaPartida.Size = new Size(382, 292);
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
            pnlGanador.Location = new Point(230, 9);
            pnlGanador.Name = "pnlGanador";
            pnlGanador.Size = new Size(334, 489);
            pnlGanador.TabIndex = 0;
            // 
            // gbChat
            // 
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
            txtMensaje.Location = new Point(6, 314);
            txtMensaje.Name = "txtMensaje";
            txtMensaje.Size = new Size(178, 23);
            txtMensaje.TabIndex = 1;
            // 
            // lstChat
            // 
            lstChat.Dock = DockStyle.Top;
            lstChat.FormattingEnabled = true;
            lstChat.ItemHeight = 15;
            lstChat.Location = new Point(3, 19);
            lstChat.MaximumSize = new Size(0, 300);
            lstChat.Name = "lstChat";
            lstChat.Size = new Size(244, 289);
            lstChat.TabIndex = 0;
            // 
            // FormPartida
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 504);
            Controls.Add(pnlGanador);
            Controls.Add(gbChat);
            Controls.Add(flpHistorial);
            Controls.Add(lblContador);
            Controls.Add(ntnLoteria);
            Controls.Add(pbFicha);
            Controls.Add(tlpTablaPartida);
            Controls.Add(cbVelocidad);
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
        private ComboBox cbVelocidad;
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
    }
}
