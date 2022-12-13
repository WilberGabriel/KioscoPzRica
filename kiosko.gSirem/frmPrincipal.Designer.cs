namespace kiosko.gSirem
{
    partial class frmPrincipal
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
            this.materialTheme1 = new Telerik.WinControls.Themes.MaterialTheme();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnSalir = new Telerik.WinControls.UI.RadButton();
            this.radPanorama1 = new Telerik.WinControls.UI.RadPanorama();
            this.tileGroupElement1 = new Telerik.WinControls.UI.TileGroupElement();
            this.radTileElement1 = new Telerik.WinControls.UI.RadTileElement();
            this.tileGroupElement2 = new Telerik.WinControls.UI.TileGroupElement();
            this.radTileElement2 = new Telerik.WinControls.UI.RadTileElement();
            this.mainpanel = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSalir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanorama1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.Silver;
            this.panelHeader.Controls.Add(this.btnSalir);
            this.panelHeader.Controls.Add(this.radPanorama1);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1242, 413);
            this.panelHeader.TabIndex = 1;
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.btnSalir.Location = new System.Drawing.Point(12, 365);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(67, 38);
            this.btnSalir.TabIndex = 0;
            this.btnSalir.Text = "X";
            this.btnSalir.ThemeName = "Material";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // radPanorama1
            // 
            this.radPanorama1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanorama1.Groups.AddRange(new Telerik.WinControls.RadItem[] {
            this.tileGroupElement1,
            this.tileGroupElement2});
            this.radPanorama1.Location = new System.Drawing.Point(0, 0);
            this.radPanorama1.Name = "radPanorama1";
            this.radPanorama1.RowsCount = 3;
            this.radPanorama1.ShowGroups = true;
            this.radPanorama1.Size = new System.Drawing.Size(1242, 359);
            this.radPanorama1.TabIndex = 0;
            this.radPanorama1.Text = "radPanorama1";
            // 
            // tileGroupElement1
            // 
            this.tileGroupElement1.Alignment = System.Drawing.ContentAlignment.TopLeft;
            this.tileGroupElement1.AngleTransform = 0F;
            this.tileGroupElement1.FlipText = false;
            this.tileGroupElement1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radTileElement1});
            this.tileGroupElement1.Margin = new System.Windows.Forms.Padding(0);
            this.tileGroupElement1.Name = "tileGroupElement1";
            this.tileGroupElement1.Padding = new System.Windows.Forms.Padding(0);
            this.tileGroupElement1.RightToLeft = false;
            this.tileGroupElement1.RowsCount = 3;
            this.tileGroupElement1.Text = "PREDIAL";
            this.tileGroupElement1.TextOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.tileGroupElement1.TextWrap = false;
            // 
            // radTileElement1
            // 
            this.radTileElement1.Alignment = System.Drawing.ContentAlignment.TopLeft;
            this.radTileElement1.AngleTransform = 0F;
            this.radTileElement1.ColSpan = 4;
            this.radTileElement1.FlipText = false;
            this.radTileElement1.Image = global::kiosko.gSirem.Properties.Resources.cobrodepredial;
            this.radTileElement1.Margin = new System.Windows.Forms.Padding(0);
            this.radTileElement1.Name = "radTileElement1";
            this.radTileElement1.Padding = new System.Windows.Forms.Padding(0);
            this.radTileElement1.RightToLeft = false;
            this.radTileElement1.RowSpan = 3;
            this.radTileElement1.Text = "IMPUESTO PREDIAL";
            this.radTileElement1.TextOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.radTileElement1.TextWrap = false;
            this.radTileElement1.Click += new System.EventHandler(this.radTileElement1_Click);
            // 
            // tileGroupElement2
            // 
            this.tileGroupElement2.Alignment = System.Drawing.ContentAlignment.TopLeft;
            this.tileGroupElement2.AngleTransform = 0F;
            this.tileGroupElement2.FlipText = false;
            this.tileGroupElement2.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radTileElement2});
            this.tileGroupElement2.Margin = new System.Windows.Forms.Padding(0);
            this.tileGroupElement2.Name = "tileGroupElement2";
            this.tileGroupElement2.Padding = new System.Windows.Forms.Padding(0);
            this.tileGroupElement2.RightToLeft = false;
            this.tileGroupElement2.RowsCount = 2;
            this.tileGroupElement2.Text = "REGISTRO CIVIL";
            this.tileGroupElement2.TextOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.tileGroupElement2.TextWrap = false;
            // 
            // radTileElement2
            // 
            this.radTileElement2.Alignment = System.Drawing.ContentAlignment.TopLeft;
            this.radTileElement2.AngleTransform = 0F;
            this.radTileElement2.ColSpan = 5;
            this.radTileElement2.FlipText = false;
            this.radTileElement2.Image = global::kiosko.gSirem.Properties.Resources.cortedecaja;
            this.radTileElement2.Margin = new System.Windows.Forms.Padding(0);
            this.radTileElement2.Name = "radTileElement2";
            this.radTileElement2.Padding = new System.Windows.Forms.Padding(0);
            this.radTileElement2.RightToLeft = false;
            this.radTileElement2.RowSpan = 2;
            this.radTileElement2.Text = "ACTA NACIMIENTO";
            this.radTileElement2.TextOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.radTileElement2.TextWrap = false;
            this.radTileElement2.Click += new System.EventHandler(this.radTileElement2_Click);
            // 
            // mainpanel
            // 
            this.mainpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainpanel.Location = new System.Drawing.Point(0, 413);
            this.mainpanel.Name = "mainpanel";
            this.mainpanel.Size = new System.Drawing.Size(1242, 629);
            this.mainpanel.TabIndex = 2;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1242, 1042);
            this.Controls.Add(this.mainpanel);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmPrincipal";
            this.Text = "Sistema de Control de Pagos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnSalir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanorama1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanorama radPanorama1;
        private Telerik.WinControls.Themes.MaterialTheme materialTheme1;
        private Telerik.WinControls.UI.TileGroupElement tileGroupElement1;
        private Telerik.WinControls.UI.RadTileElement radTileElement1;
        private Telerik.WinControls.UI.TileGroupElement tileGroupElement2;
        private Telerik.WinControls.UI.RadTileElement radTileElement2;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel mainpanel;
        private Telerik.WinControls.UI.RadButton btnSalir;
    }
}