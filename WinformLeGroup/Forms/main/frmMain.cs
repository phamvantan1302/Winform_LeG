using HKDashboard.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformLeGroup.Forms.Dashboard;

namespace WinformGoshi.Forms.main
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            //mnuMainMenu.Renderer = new HoverOnlyMenuRenderer();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Lấy không gian có thể sử dụng (không tính Taskbar)
            Rectangle workingArea = SystemInformation.WorkingArea;

            // Lấy chiều cao của Taskbar
            int taskbarHeight = Screen.PrimaryScreen.Bounds.Height - workingArea.Height;

            // Tính toán chiều cao cửa sổ bằng chiều cao màn hình trừ đi chiều cao của Taskbar
            int windowHeight = Screen.PrimaryScreen.Bounds.Height - taskbarHeight;
            int windowWidth = Screen.PrimaryScreen.Bounds.Width;

            // Cập nhật kích thước của cửa sổ để không bị Taskbar che khuất
            this.Bounds = new Rectangle(workingArea.Left, workingArea.Top, windowWidth, windowHeight);

            // Đặt cửa sổ ở trạng thái tối đa (maximize)
            this.WindowState = FormWindowState.Normal;  // Không tối đa hóa

        }

        //public class HoverOnlyMenuRenderer : ToolStripProfessionalRenderer
        //{
        //    public HoverOnlyMenuRenderer() : base(new HoverOnlyColorTable()) { }

        //    protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        //    {
        //        if (e.Item.Selected || e.Item.Pressed)
        //        {
        //            e.Graphics.FillRectangle(Brushes.Navy, new Rectangle(Point.Empty, e.Item.Size));
        //            e.Item.ForeColor = Color.White;
        //        }
        //        else
        //        {
        //            e.Graphics.FillRectangle(new SolidBrush(Color.Navy), new Rectangle(Point.Empty, e.Item.Size));
        //            e.Item.ForeColor = SystemColors.ControlLightLight;
        //        }
        //    }
        //}

        //public class HoverOnlyColorTable : ProfessionalColorTable
        //{
        //    public override Color MenuItemSelected => Color.Navy;
        //    public override Color MenuItemBorder => Color.Navy;
        //    public override Color MenuItemSelectedGradientBegin => Color.Navy;
        //    public override Color MenuItemSelectedGradientEnd => Color.Navy;

        //    public override Color MenuItemPressedGradientBegin => Color.Navy;
        //    public override Color MenuItemPressedGradientEnd => Color.Navy;
        //    public override Color MenuItemPressedGradientMiddle => Color.Navy;
        //}

        private void dashboardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDashboardSLSX frm = new frmDashboardSLSX();
            frm.MdiParent = this;
            frm.Show();
        }

        private void dashboardTỷLệThựcTếToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDashboard frm = new frmDashboard();
            frm.MdiParent = this;
            frm.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            //this.WindowState = FormWindowState.Maximized;
        }

        private void dashboardBảoTrìBảoDưỡngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDashboardBTBD frm = new frmDashboardBTBD();
            frm.MdiParent = this;
            frm.Show();
        }

        private void dashboardOEEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProductionOverview frm = new frmProductionOverview();
            frm.MdiParent = this;
            frm.Show();
        }

        private void dashboardMachineEfficiencyOEEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDashboardMachineEfficiency frm = new frmDashboardMachineEfficiency();
            frm.MdiParent = this;
            frm.Show();
        }

        private void dashboardDowntimeAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDashboardDowntimeAnalysis frm = new frmDashboardDowntimeAnalysis();
            frm.MdiParent = this;
            frm.Show();
        }

        private void dashoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIoTMonitoring frm = new frmIoTMonitoring();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
