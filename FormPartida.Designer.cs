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
            ((System.ComponentModel.ISupportInitialize)pbCartaActual).BeginInit();
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
            pbCartaActual.Size = new Size(64, 101);
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
            // 
            // FormPartida
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(bttnSiguiente);
            Controls.Add(pbCartaActual);
            Controls.Add(lblNombreCarta);
            Name = "FormPartida";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pbCartaActual).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNombreCarta;
        private PictureBox pbCartaActual;
        private Button bttnSiguiente;
    }
}
