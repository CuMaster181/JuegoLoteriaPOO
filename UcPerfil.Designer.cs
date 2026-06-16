namespace JuegoLoteriaPOO
{
    partial class UcPerfil
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
            bttnContinuar = new Button();
            txtNombre = new TextBox();
            bttnCancelar = new Button();
            lblUsuario = new Label();
            SuspendLayout();
            // 
            // bttnContinuar
            // 
            bttnContinuar.Location = new Point(46, 239);
            bttnContinuar.Name = "bttnContinuar";
            bttnContinuar.Size = new Size(232, 62);
            bttnContinuar.TabIndex = 0;
            bttnContinuar.Text = "Continuar";
            bttnContinuar.UseVisualStyleBackColor = true;
            bttnContinuar.Click += bttnContinuar_Click;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(46, 145);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(232, 23);
            txtNombre.TabIndex = 1;
            // 
            // bttnCancelar
            // 
            bttnCancelar.Location = new Point(46, 307);
            bttnCancelar.Name = "bttnCancelar";
            bttnCancelar.Size = new Size(232, 60);
            bttnCancelar.TabIndex = 2;
            bttnCancelar.Text = "Cancelar";
            bttnCancelar.UseVisualStyleBackColor = true;
            bttnCancelar.Click += bttnCancelar_Click;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(46, 127);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(155, 15);
            lblUsuario.TabIndex = 3;
            lblUsuario.Text = "Ingresa Nombre De Usuario:";
            // 
            // UcPerfil
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblUsuario);
            Controls.Add(bttnCancelar);
            Controls.Add(txtNombre);
            Controls.Add(bttnContinuar);
            Name = "UcPerfil";
            Size = new Size(482, 574);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bttnContinuar;
        private TextBox txtNombre;
        private Button bttnCancelar;
        private Label lblUsuario;
    }
}
