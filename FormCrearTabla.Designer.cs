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
            flpCartasDisponibles = new FlowLayoutPanel();
            tlpTabla = new TableLayoutPanel();
            bttnRandom = new Button();
            bttnInicio = new Button();
            SuspendLayout();
            // 
            // flpCartasDisponibles
            // 
            flpCartasDisponibles.AutoScroll = true;
            flpCartasDisponibles.Location = new Point(12, 12);
            flpCartasDisponibles.Name = "flpCartasDisponibles";
            flpCartasDisponibles.Size = new Size(200, 426);
            flpCartasDisponibles.TabIndex = 0;
            // 
            // tlpTabla
            // 
            tlpTabla.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tlpTabla.ColumnCount = 5;
            tlpTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpTabla.Location = new Point(316, 12);
            tlpTabla.Name = "tlpTabla";
            tlpTabla.RowCount = 5;
            tlpTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpTabla.Size = new Size(316, 426);
            tlpTabla.TabIndex = 1;
            // 
            // bttnRandom
            // 
            bttnRandom.Location = new Point(218, 12);
            bttnRandom.Name = "bttnRandom";
            bttnRandom.Size = new Size(34, 23);
            bttnRandom.TabIndex = 2;
            bttnRandom.Text = "rnd";
            bttnRandom.UseVisualStyleBackColor = true;
            bttnRandom.Click += bttnRandom_Click;
            // 
            // bttnInicio
            // 
            bttnInicio.Location = new Point(679, 397);
            bttnInicio.Name = "bttnInicio";
            bttnInicio.Size = new Size(75, 23);
            bttnInicio.TabIndex = 3;
            bttnInicio.Text = "Inicio";
            bttnInicio.UseVisualStyleBackColor = true;
            bttnInicio.Click += bttnInicio_Click;
            // 
            // FormCrearTabla
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(bttnInicio);
            Controls.Add(bttnRandom);
            Controls.Add(tlpTabla);
            Controls.Add(flpCartasDisponibles);
            Name = "FormCrearTabla";
            Text = "FormCrearTabla";
            Load += FormCrearTabla_Load;
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flpCartasDisponibles;
        private TableLayoutPanel tlpTabla;
        private Button bttnRandom;
        private Button bttnInicio;
    }
}