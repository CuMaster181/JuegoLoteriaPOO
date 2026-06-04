namespace JuegoLoteriaPOO
{
    partial class FormMenuPrincipal
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
            bttnSolitario = new Button();
            bttnMultijugador = new Button();
            pnlMostrarUc = new Panel();
            SuspendLayout();
            // 
            // bttnSolitario
            // 
            bttnSolitario.Location = new Point(84, 142);
            bttnSolitario.Name = "bttnSolitario";
            bttnSolitario.Size = new Size(132, 23);
            bttnSolitario.TabIndex = 0;
            bttnSolitario.Text = "Partida En Solitario";
            bttnSolitario.UseVisualStyleBackColor = true;
            bttnSolitario.Click += bttnSolitario_Click;
            // 
            // bttnMultijugador
            // 
            bttnMultijugador.Location = new Point(84, 183);
            bttnMultijugador.Name = "bttnMultijugador";
            bttnMultijugador.Size = new Size(132, 23);
            bttnMultijugador.TabIndex = 1;
            bttnMultijugador.Text = "Multijugador";
            bttnMultijugador.UseVisualStyleBackColor = true;
            bttnMultijugador.Click += bttnMultijugador_Click;
            // 
            // pnlMostrarUc
            // 
            pnlMostrarUc.Dock = DockStyle.Fill;
            pnlMostrarUc.Location = new Point(0, 0);
            pnlMostrarUc.Name = "pnlMostrarUc";
            pnlMostrarUc.Size = new Size(318, 450);
            pnlMostrarUc.TabIndex = 2;
            pnlMostrarUc.Visible = false;
            // 
            // FormMenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(318, 450);
            Controls.Add(pnlMostrarUc);
            Controls.Add(bttnMultijugador);
            Controls.Add(bttnSolitario);
            Name = "FormMenuPrincipal";
            Text = "FormMenuPrincipal";
            Load += FormMenuPrincipal_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button bttnSolitario;
        private Button bttnMultijugador;
        private Panel pnlMostrarUc;
    }
}