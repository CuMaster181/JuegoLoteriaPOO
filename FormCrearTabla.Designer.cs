namespace JuegoLoteriaPOO
{
    partial class FormCrearTabla
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            flpCartasDisponibles = new FlowLayoutPanel();
            tabControlTablas = new TabControl();
            bttnRandom = new Button();
            bttnInicio = new Button();
            btnCargarTabla = new Button();
            btnGuardarTabla = new Button();
            lblTablas = new Label();
            SuspendLayout();
            // 
            // flpCartasDisponibles
            // 
            flpCartasDisponibles.AutoScroll = true;
            flpCartasDisponibles.Location = new Point(12, 12);
            flpCartasDisponibles.Name = "flpCartasDisponibles";
            flpCartasDisponibles.Size = new Size(210, 580);
            flpCartasDisponibles.TabIndex = 0;
            // 
            // tabControlTablas
            // 
            tabControlTablas.Location = new Point(230, 12);
            tabControlTablas.Name = "tabControlTablas";
            tabControlTablas.Size = new Size(490, 530);
            tabControlTablas.TabIndex = 1;
            // 
            // lblTablas
            // 
            lblTablas.AutoSize = true;
            lblTablas.Location = new Point(230, 550);
            lblTablas.Name = "lblTablas";
            lblTablas.Size = new Size(160, 15);
            lblTablas.TabIndex = 6;
            lblTablas.Text = "Arrastra cartas a la tabla activa";
            // 
            // bttnRandom
            // 
            bttnRandom.Location = new Point(230, 570);
            bttnRandom.Name = "bttnRandom";
            bttnRandom.Size = new Size(115, 23);
            bttnRandom.TabIndex = 2;
            bttnRandom.Text = "Aleatorio (todas)";
            bttnRandom.UseVisualStyleBackColor = true;
            bttnRandom.Click += bttnRandom_Click;
            // 
            // btnGuardarTabla
            // 
            btnGuardarTabla.Location = new Point(355, 570);
            btnGuardarTabla.Name = "btnGuardarTabla";
            btnGuardarTabla.Size = new Size(110, 23);
            btnGuardarTabla.TabIndex = 3;
            btnGuardarTabla.Text = "Guardar tabla";
            btnGuardarTabla.UseVisualStyleBackColor = true;
            btnGuardarTabla.Click += btnGuardarTabla_Click;
            // 
            // btnCargarTabla
            // 
            btnCargarTabla.Location = new Point(475, 570);
            btnCargarTabla.Name = "btnCargarTabla";
            btnCargarTabla.Size = new Size(110, 23);
            btnCargarTabla.TabIndex = 4;
            btnCargarTabla.Text = "Cargar tabla";
            btnCargarTabla.UseVisualStyleBackColor = true;
            btnCargarTabla.Click += btnCargarTabla_Click;
            // 
            // bttnInicio
            // 
            bttnInicio.Location = new Point(605, 570);
            bttnInicio.Name = "bttnInicio";
            bttnInicio.Size = new Size(110, 23);
            bttnInicio.TabIndex = 5;
            bttnInicio.Text = "¡Jugar!";
            bttnInicio.UseVisualStyleBackColor = true;
            bttnInicio.Click += bttnInicio_Click;
            // 
            // FormCrearTabla
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(730, 605);
            Controls.Add(btnGuardarTabla);
            Controls.Add(btnCargarTabla);
            Controls.Add(bttnInicio);
            Controls.Add(bttnRandom);
            Controls.Add(lblTablas);
            Controls.Add(tabControlTablas);
            Controls.Add(flpCartasDisponibles);
            Name = "FormCrearTabla";
            Text = "Crear Tabla";
            Load += FormCrearTabla_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flpCartasDisponibles;
        private TabControl tabControlTablas;
        private Button bttnRandom;
        private Button bttnInicio;
        private Button btnCargarTabla;
        private Button btnGuardarTabla;
        private Label lblTablas;
    }
}
