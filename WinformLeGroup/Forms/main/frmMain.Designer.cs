namespace WinformGoshi.Forms.main
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mnuMainMenu = new System.Windows.Forms.MenuStrip();
            this.dashboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dashboardToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dashboardTỷLệThựcTếToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dashboardBảoTrìBảoDưỡngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dashboardOEEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dashboardMachineEfficiencyOEEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dashboardDowntimeAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.mnuMainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMainMenu
            // 
            this.mnuMainMenu.AutoSize = false;
            this.mnuMainMenu.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.mnuMainMenu.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dashboardToolStripMenuItem});
            this.mnuMainMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMainMenu.Name = "mnuMainMenu";
            this.mnuMainMenu.Padding = new System.Windows.Forms.Padding(100, 2, 0, 2);
            this.mnuMainMenu.Size = new System.Drawing.Size(749, 40);
            this.mnuMainMenu.TabIndex = 0;
            this.mnuMainMenu.Text = "menuStrip1";
            // 
            // dashboardToolStripMenuItem
            // 
            this.dashboardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dashboardToolStripMenuItem1,
            this.dashboardTỷLệThựcTếToolStripMenuItem,
            this.dashboardBảoTrìBảoDưỡngToolStripMenuItem,
            this.dashboardOEEToolStripMenuItem,
            this.dashboardMachineEfficiencyOEEToolStripMenuItem,
            this.dashboardDowntimeAnalysisToolStripMenuItem});
            this.dashboardToolStripMenuItem.Name = "dashboardToolStripMenuItem";
            this.dashboardToolStripMenuItem.Size = new System.Drawing.Size(94, 36);
            this.dashboardToolStripMenuItem.Text = "Dashboard";
            // 
            // dashboardToolStripMenuItem1
            // 
            this.dashboardToolStripMenuItem1.Name = "dashboardToolStripMenuItem1";
            this.dashboardToolStripMenuItem1.Size = new System.Drawing.Size(319, 24);
            this.dashboardToolStripMenuItem1.Text = "Dashboard Sản lượng";
            this.dashboardToolStripMenuItem1.Click += new System.EventHandler(this.dashboardToolStripMenuItem1_Click);
            // 
            // dashboardTỷLệThựcTếToolStripMenuItem
            // 
            this.dashboardTỷLệThựcTếToolStripMenuItem.Name = "dashboardTỷLệThựcTếToolStripMenuItem";
            this.dashboardTỷLệThựcTếToolStripMenuItem.Size = new System.Drawing.Size(319, 24);
            this.dashboardTỷLệThựcTếToolStripMenuItem.Text = "Dashboard Tỷ lệ thực tế";
            this.dashboardTỷLệThựcTếToolStripMenuItem.Click += new System.EventHandler(this.dashboardTỷLệThựcTếToolStripMenuItem_Click);
            // 
            // dashboardBảoTrìBảoDưỡngToolStripMenuItem
            // 
            this.dashboardBảoTrìBảoDưỡngToolStripMenuItem.Name = "dashboardBảoTrìBảoDưỡngToolStripMenuItem";
            this.dashboardBảoTrìBảoDưỡngToolStripMenuItem.Size = new System.Drawing.Size(319, 24);
            this.dashboardBảoTrìBảoDưỡngToolStripMenuItem.Text = "Dashboard Bảo trì bảo dưỡng";
            this.dashboardBảoTrìBảoDưỡngToolStripMenuItem.Click += new System.EventHandler(this.dashboardBảoTrìBảoDưỡngToolStripMenuItem_Click);
            // 
            // dashboardOEEToolStripMenuItem
            // 
            this.dashboardOEEToolStripMenuItem.Name = "dashboardOEEToolStripMenuItem";
            this.dashboardOEEToolStripMenuItem.Size = new System.Drawing.Size(319, 24);
            this.dashboardOEEToolStripMenuItem.Text = "Dashboard Production Orverview";
            this.dashboardOEEToolStripMenuItem.Click += new System.EventHandler(this.dashboardOEEToolStripMenuItem_Click);
            // 
            // dashboardMachineEfficiencyOEEToolStripMenuItem
            // 
            this.dashboardMachineEfficiencyOEEToolStripMenuItem.Name = "dashboardMachineEfficiencyOEEToolStripMenuItem";
            this.dashboardMachineEfficiencyOEEToolStripMenuItem.Size = new System.Drawing.Size(319, 24);
            this.dashboardMachineEfficiencyOEEToolStripMenuItem.Text = "Dashboard Machine Efficiency (OEE)";
            this.dashboardMachineEfficiencyOEEToolStripMenuItem.Click += new System.EventHandler(this.dashboardMachineEfficiencyOEEToolStripMenuItem_Click);
            // 
            // dashboardDowntimeAnalysisToolStripMenuItem
            // 
            this.dashboardDowntimeAnalysisToolStripMenuItem.Name = "dashboardDowntimeAnalysisToolStripMenuItem";
            this.dashboardDowntimeAnalysisToolStripMenuItem.Size = new System.Drawing.Size(319, 24);
            this.dashboardDowntimeAnalysisToolStripMenuItem.Text = "Dashboard Downtime Analysis";
            this.dashboardDowntimeAnalysisToolStripMenuItem.Click += new System.EventHandler(this.dashboardDowntimeAnalysisToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox1.Image = global::WinformLeGroup.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(9, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(749, 455);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.mnuMainMenu);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuMainMenu;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmMain";
            this.Text = "LEGROUP";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.mnuMainMenu.ResumeLayout(false);
            this.mnuMainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMainMenu;
        private System.Windows.Forms.ToolStripMenuItem dashboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dashboardToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem dashboardTỷLệThựcTếToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem dashboardBảoTrìBảoDưỡngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dashboardOEEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dashboardMachineEfficiencyOEEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dashboardDowntimeAnalysisToolStripMenuItem;
    }
}