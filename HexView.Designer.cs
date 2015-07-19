using System;

namespace hexer
{
    partial class HexView
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
        
        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.markAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markerMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editMarkerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMarkerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.markerMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // vScrollBar
            // 
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar.Location = new System.Drawing.Point(567, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 539);
            this.vScrollBar.TabIndex = 0;
            this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.markAsToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(118, 26);
            // 
            // markAsToolStripMenuItem
            // 
            this.markAsToolStripMenuItem.Name = "markAsToolStripMenuItem";
            this.markAsToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.markAsToolStripMenuItem.Text = "Mark As";
            // 
            // markerMenuStrip
            // 
            this.markerMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editMarkerToolStripMenuItem,
            this.deleteMarkerToolStripMenuItem});
            this.markerMenuStrip.Name = "markerMenuStrip";
            this.markerMenuStrip.Size = new System.Drawing.Size(153, 70);
            // 
            // editMarkerToolStripMenuItem
            // 
            this.editMarkerToolStripMenuItem.Name = "editMarkerToolStripMenuItem";
            this.editMarkerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editMarkerToolStripMenuItem.Text = "Edit Marker";
            this.editMarkerToolStripMenuItem.Click += new System.EventHandler(this.editMarkerToolStripMenuItem_Click);
            // 
            // deleteMarkerToolStripMenuItem
            // 
            this.deleteMarkerToolStripMenuItem.Name = "deleteMarkerToolStripMenuItem";
            this.deleteMarkerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteMarkerToolStripMenuItem.Text = "Delete Marker";
            this.deleteMarkerToolStripMenuItem.Click += new System.EventHandler(this.deleteMarkerToolStripMenuItem_Click);
            // 
            // HexView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vScrollBar);
            this.Name = "HexView";
            this.Size = new System.Drawing.Size(584, 539);
            this.contextMenuStrip.ResumeLayout(false);
            this.markerMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem markAsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip markerMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editMarkerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteMarkerToolStripMenuItem;
    }
}
