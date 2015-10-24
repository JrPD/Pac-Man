namespace Pac_man
{
	partial class Game
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
			this.GroupBox = new System.Windows.Forms.GroupBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectMapMenuItem = new System.Windows.Forms.ToolStripComboBox();
			this.SelectLevelCb = new System.Windows.Forms.ToolStripComboBox();
			this.fToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PointsLabel = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// GroupBox
			// 
			this.GroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GroupBox.Location = new System.Drawing.Point(5, 32);
			this.GroupBox.Margin = new System.Windows.Forms.Padding(5);
			this.GroupBox.Name = "GroupBox";
			this.GroupBox.Padding = new System.Windows.Forms.Padding(5);
			this.GroupBox.Size = new System.Drawing.Size(600, 613);
			this.GroupBox.TabIndex = 0;
			this.GroupBox.TabStop = false;
			this.GroupBox.Text = "Play field";
			// 
			// menuStrip1
			// 
			this.menuStrip1.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.selectMapMenuItem,
            this.SelectLevelCb,
            this.fToolStripMenuItem,
            this.PointsLabel});
			this.menuStrip1.Location = new System.Drawing.Point(5, 5);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(600, 27);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// startToolStripMenuItem
			// 
			this.startToolStripMenuItem.Name = "startToolStripMenuItem";
			this.startToolStripMenuItem.Size = new System.Drawing.Size(43, 23);
			this.startToolStripMenuItem.Text = "Start";
			this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 23);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// selectMapMenuItem
			// 
			this.selectMapMenuItem.Name = "selectMapMenuItem";
			this.selectMapMenuItem.Size = new System.Drawing.Size(80, 23);
			this.selectMapMenuItem.Text = "Map";
			// 
			// SelectLevelCb
			// 
			this.SelectLevelCb.Name = "SelectLevelCb";
			this.SelectLevelCb.Size = new System.Drawing.Size(80, 23);
			this.SelectLevelCb.Text = "Level";
			// 
			// fToolStripMenuItem
			// 
			this.fToolStripMenuItem.Name = "fToolStripMenuItem";
			this.fToolStripMenuItem.Size = new System.Drawing.Size(55, 23);
			this.fToolStripMenuItem.Text = "Points:";
			// 
			// PointsLabel
			// 
			this.PointsLabel.Name = "PointsLabel";
			this.PointsLabel.Size = new System.Drawing.Size(25, 23);
			this.PointsLabel.Text = "1";
			// 
			// Game
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(610, 650);
			this.Controls.Add(this.GroupBox);
			this.Controls.Add(this.menuStrip1);
			this.Name = "Game";
			this.Padding = new System.Windows.Forms.Padding(5);
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox GroupBox;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripComboBox selectMapMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem PointsLabel;
		private System.Windows.Forms.ToolStripComboBox SelectLevelCb;
	}
}

