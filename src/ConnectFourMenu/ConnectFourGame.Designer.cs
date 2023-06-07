namespace EVAL.ConnectFour.View
{
    partial class ConnectFourGame
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonPause = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorStatus = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelStatus = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabelP2Timer = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparatorTimer = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelP1Timer = new System.Windows.Forms.ToolStripLabel();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(458, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.saveExitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "Fájl";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.saveToolStripMenuItem.Text = "Mentés";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_ClickAsync);
            // 
            // saveExitToolStripMenuItem
            // 
            this.saveExitToolStripMenuItem.Name = "saveExitToolStripMenuItem";
            this.saveExitToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.saveExitToolStripMenuItem.Text = "Mentés és kilépés";
            this.saveExitToolStripMenuItem.Click += new System.EventHandler(this.saveExitToolStripMenuItem_ClickAsync);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonPause,
            this.toolStripSeparatorStatus,
            this.toolStripLabelStatus,
            this.toolStripLabelP2Timer,
            this.toolStripSeparatorTimer,
            this.toolStripLabelP1Timer});
            this.toolStrip1.Location = new System.Drawing.Point(0, 373);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(458, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip";
            // 
            // toolStripButtonPause
            // 
            this.toolStripButtonPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPause.Name = "toolStripButtonPause";
            this.toolStripButtonPause.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPause.Text = "I I";
            this.toolStripButtonPause.ToolTipText = "Pause";
            this.toolStripButtonPause.Click += new System.EventHandler(this.toolStripButtonPause_Click);
            // 
            // toolStripSeparatorStatus
            // 
            this.toolStripSeparatorStatus.Name = "toolStripSeparatorStatus";
            this.toolStripSeparatorStatus.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabelStatus
            // 
            this.toolStripLabelStatus.Name = "toolStripLabelStatus";
            this.toolStripLabelStatus.Size = new System.Drawing.Size(112, 22);
            this.toolStripLabelStatus.Text = "Játék folyamatban...";
            // 
            // toolStripLabelP2Timer
            // 
            this.toolStripLabelP2Timer.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabelP2Timer.Name = "toolStripLabelP2Timer";
            this.toolStripLabelP2Timer.Size = new System.Drawing.Size(43, 22);
            this.toolStripLabelP2Timer.Text = "O: 0:00";
            // 
            // toolStripSeparatorTimer
            // 
            this.toolStripSeparatorTimer.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparatorTimer.Name = "toolStripSeparatorTimer";
            this.toolStripSeparatorTimer.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabelP1Timer
            // 
            this.toolStripLabelP1Timer.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabelP1Timer.Name = "toolStripLabelP1Timer";
            this.toolStripLabelP1Timer.Size = new System.Drawing.Size(41, 22);
            this.toolStripLabelP1Timer.Text = "X: 0:00";
            // 
            // ConnectFourGame
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(458, 398);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.Name = "ConnectFourGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Potoygós Amőba";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveExitToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButtonPause;
        private ToolStripSeparator toolStripSeparatorTimer;
        private ToolStripLabel toolStripLabelStatus;
        private ToolStripLabel toolStripLabelP2Timer;
        private ToolStripLabel toolStripLabelP1Timer;
        private ToolStripSeparator toolStripSeparatorStatus;
    }
}