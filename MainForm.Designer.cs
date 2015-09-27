namespace hexer
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.navigateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goToSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMarkerWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openMarkersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMarkersAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMarkersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.hoverAddressInspector = new hexer.AddressInspector();
            this.selectedAddressInspector = new hexer.AddressInspector();
            this.hexView = new hexer.HexView();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 732);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1111, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.navigateToolStripMenuItem,
            this.markersToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1111, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // navigateToolStripMenuItem
            // 
            this.navigateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToSelectedToolStripMenuItem,
            this.goToToolStripMenuItem,
            this.searchToolStripMenuItem});
            this.navigateToolStripMenuItem.Name = "navigateToolStripMenuItem";
            this.navigateToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.navigateToolStripMenuItem.Text = "Navigate";
            // 
            // goToSelectedToolStripMenuItem
            // 
            this.goToSelectedToolStripMenuItem.Name = "goToSelectedToolStripMenuItem";
            this.goToSelectedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.G)));
            this.goToSelectedToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.goToSelectedToolStripMenuItem.Text = "Go To Selected";
            this.goToSelectedToolStripMenuItem.Click += new System.EventHandler(this.goToSelectedToolStripMenuItem_Click);
            // 
            // goToToolStripMenuItem
            // 
            this.goToToolStripMenuItem.Name = "goToToolStripMenuItem";
            this.goToToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.goToToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.goToToolStripMenuItem.Text = "Go To ...";
            this.goToToolStripMenuItem.Click += new System.EventHandler(this.goToToolStripMenuItem_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.searchToolStripMenuItem.Text = "Search";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // markersToolStripMenuItem
            // 
            this.markersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMarkerWindowToolStripMenuItem,
            this.toolStripSeparator1,
            this.openMarkersToolStripMenuItem,
            this.saveMarkersAsToolStripMenuItem,
            this.saveMarkersToolStripMenuItem});
            this.markersToolStripMenuItem.Name = "markersToolStripMenuItem";
            this.markersToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.markersToolStripMenuItem.Text = "Markers";
            // 
            // showMarkerWindowToolStripMenuItem
            // 
            this.showMarkerWindowToolStripMenuItem.Name = "showMarkerWindowToolStripMenuItem";
            this.showMarkerWindowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.showMarkerWindowToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.showMarkerWindowToolStripMenuItem.Text = "Show Marker Window";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(260, 6);
            // 
            // openMarkersToolStripMenuItem
            // 
            this.openMarkersToolStripMenuItem.Name = "openMarkersToolStripMenuItem";
            this.openMarkersToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.O)));
            this.openMarkersToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.openMarkersToolStripMenuItem.Text = "Load Markers";
            this.openMarkersToolStripMenuItem.Click += new System.EventHandler(this.openMarkersToolStripMenuItem_Click);
            // 
            // saveMarkersAsToolStripMenuItem
            // 
            this.saveMarkersAsToolStripMenuItem.Name = "saveMarkersAsToolStripMenuItem";
            this.saveMarkersAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveMarkersAsToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.saveMarkersAsToolStripMenuItem.Text = "Save Markers As...";
            this.saveMarkersAsToolStripMenuItem.Click += new System.EventHandler(this.saveMarkersAsToolStripMenuItem_Click);
            // 
            // saveMarkersToolStripMenuItem
            // 
            this.saveMarkersToolStripMenuItem.Name = "saveMarkersToolStripMenuItem";
            this.saveMarkersToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.saveMarkersToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.saveMarkersToolStripMenuItem.Text = "Save Markers";
            this.saveMarkersToolStripMenuItem.Click += new System.EventHandler(this.saveMarkersToolStripMenuItem_Click);
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 24);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.hoverAddressInspector);
            this.mainSplitContainer.Panel1.Controls.Add(this.selectedAddressInspector);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.hexView);
            this.mainSplitContainer.Size = new System.Drawing.Size(1111, 708);
            this.mainSplitContainer.SplitterDistance = 220;
            this.mainSplitContainer.TabIndex = 3;
            // 
            // hoverAddressInspector
            // 
            this.hoverAddressInspector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hoverAddressInspector.Caption = "Hover";
            this.hoverAddressInspector.Editable = false;
            this.hoverAddressInspector.Location = new System.Drawing.Point(0, 355);
            this.hoverAddressInspector.Name = "hoverAddressInspector";
            this.hoverAddressInspector.Size = new System.Drawing.Size(218, 361);
            this.hoverAddressInspector.TabIndex = 1;
            this.hoverAddressInspector.Target = null;
            // 
            // selectedAddressInspector
            // 
            this.selectedAddressInspector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedAddressInspector.Caption = "Selected";
            this.selectedAddressInspector.Editable = true;
            this.selectedAddressInspector.Location = new System.Drawing.Point(0, 0);
            this.selectedAddressInspector.Name = "selectedAddressInspector";
            this.selectedAddressInspector.Size = new System.Drawing.Size(218, 349);
            this.selectedAddressInspector.TabIndex = 0;
            this.selectedAddressInspector.Target = null;
            // 
            // hexView
            // 
            this.hexView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexView.FileName = null;
            this.hexView.HoverAddress = -1;
            this.hexView.Location = new System.Drawing.Point(0, 0);
            this.hexView.Name = "hexView";
            this.hexView.NumBytesInLine = 32;
            this.hexView.SelectedAddress = -1;
            this.hexView.Size = new System.Drawing.Size(887, 708);
            this.hexView.TabIndex = 0;
            this.hexView.HoverAddressChanged += new System.EventHandler(this.hexView_HoverAddressChanged);
            this.hexView.SelectedAddressChanged += new System.EventHandler(this.hexView_SelectedAddressChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 754);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Hexer";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private HexView hexView;
        private AddressInspector hoverAddressInspector;
        private AddressInspector selectedAddressInspector;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem navigateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goToSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMarkersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMarkersAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMarkersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showMarkerWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

