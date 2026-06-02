namespace JuegoLoteriaPOO
{
    partial class FormPerfil
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            bttnContinuar = new Button();
            txtNombre = new TextBox();
            SuspendLayout();
            // 
            // bttnContinuar
            // 
            bttnContinuar.Location = new Point(105, 262);
            bttnContinuar.Name = "bttnContinuar";
            bttnContinuar.Size = new Size(244, 70);
            bttnContinuar.TabIndex = 0;
            bttnContinuar.Text = "Continuar";
            bttnContinuar.UseVisualStyleBackColor = true;
            bttnContinuar.Click += bttnContinuar_Click;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(137, 143);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(173, 23);
            txtNombre.TabIndex = 1;
            // 
            // FormPerfil
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(478, 403);
            Controls.Add(txtNombre);
            Controls.Add(bttnContinuar);
            Name = "FormPerfil";
            Text = "FormPerfil";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bttnContinuar;
        private TextBox txtNombre;
    }
}