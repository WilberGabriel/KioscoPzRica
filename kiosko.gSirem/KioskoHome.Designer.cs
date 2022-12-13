namespace kiosko.gSirem
{
    partial class KioskoHome
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
            this.txtMonto = new Telerik.WinControls.UI.RadTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLeyenda = new System.Windows.Forms.Label();
            this.btnCobrar = new Telerik.WinControls.UI.RadButton();
            this.telerikMetroTouchTheme1 = new Telerik.WinControls.Themes.TelerikMetroTouchTheme();
            this.aquaTheme1 = new Telerik.WinControls.Themes.AquaTheme();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCobrar)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMonto
            // 
            this.txtMonto.Font = new System.Drawing.Font("Roboto Medium", 15F);
            this.txtMonto.Location = new System.Drawing.Point(125, 76);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(357, 30);
            this.txtMonto.TabIndex = 0;
            this.txtMonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMonto.ThemeName = "ControlDefault";
            this.txtMonto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMonto_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 36);
            this.label1.TabIndex = 1;
            this.label1.Text = "Valor";
            // 
            // lblLeyenda
            // 
            this.lblLeyenda.AutoSize = true;
            this.lblLeyenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeyenda.ForeColor = System.Drawing.Color.Maroon;
            this.lblLeyenda.Location = new System.Drawing.Point(121, 129);
            this.lblLeyenda.Name = "lblLeyenda";
            this.lblLeyenda.Size = new System.Drawing.Size(174, 36);
            this.lblLeyenda.TabIndex = 2;
            this.lblLeyenda.Text = "Resultado...";
            // 
            // btnCobrar
            // 
            this.btnCobrar.Font = new System.Drawing.Font("Roboto Medium", 16F, System.Drawing.FontStyle.Bold);
            this.btnCobrar.Location = new System.Drawing.Point(497, 76);
            this.btnCobrar.Name = "btnCobrar";
            this.btnCobrar.Size = new System.Drawing.Size(199, 45);
            this.btnCobrar.TabIndex = 3;
            this.btnCobrar.Text = "Cobrar";
            this.btnCobrar.ThemeName = "ControlDefault";
            this.btnCobrar.Click += new System.EventHandler(this.btnCobrar_Click);
            // 
            // KioskoHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 801);
            this.Controls.Add(this.btnCobrar);
            this.Controls.Add(this.lblLeyenda);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMonto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "KioskoHome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KioskoHome";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.KioskoHome_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCobrar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox txtMonto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLeyenda;
        private Telerik.WinControls.UI.RadButton btnCobrar;
        private Telerik.WinControls.Themes.TelerikMetroTouchTheme telerikMetroTouchTheme1;
        private Telerik.WinControls.Themes.AquaTheme aquaTheme1;
    }
}