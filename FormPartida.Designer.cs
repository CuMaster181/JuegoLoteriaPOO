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
            lblNombreCarta = new Label();
            pbCartaActual = new PictureBox();
            bttnSiguiente = new Button();
            fileSystemWatcher1 = new FileSystemWatcher();
            label1 = new Label();
            listBox1 = new ListBox();
            ((System.ComponentModel.ISupportInitialize)pbCartaActual).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
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
            // bttnSiguiente
            // 
            bttnSiguiente.Location = new Point(98, 101);
            bttnSiguiente.Name = "bttnSiguiente";
            bttnSiguiente.Size = new Size(75, 23);
            bttnSiguiente.TabIndex = 2;
            bttnSiguiente.Text = "Siguiente";
            bttnSiguiente.UseVisualStyleBackColor = true;
            bttnSiguiente.Click += bttnSiguiente_Click;
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
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(223, 27);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(274, 109);
            listBox1.TabIndex = 5;
            // 
            // FormPartida
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(listBox1);
            Controls.Add(label1);
            Controls.Add(bttnSiguiente);
            Controls.Add(pbCartaActual);
            Controls.Add(lblNombreCarta);
            Name = "FormPartida";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pbCartaActual).EndInit();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNombreCarta;
        private PictureBox pbCartaActual;
        private Button bttnSiguiente;
        private FileSystemWatcher fileSystemWatcher1;
        private Label label1;
        private ListBox listBox1;
    }
}
