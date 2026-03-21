using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WinformLeGroup.Models.CommonModel;
using WinformLeGroup.Models.Dashboard;
using WinformLeGroup.Sevices.DashboardSevices;

namespace WinformLeGroup.Forms.Dashboard
{
    public partial class frmDashboard : Form
    {
        private List<Data_BoPhan> lsbp = new List<Data_BoPhan>();
        private List<DMWorktation> lsmm = new List<DMWorktation>();
        private System.Windows.Forms.Timer tRefreshData;

        public frmDashboard()
        {
            InitializeComponent();
            dataOpenForm();
            tRefreshData = new System.Windows.Forms.Timer();
            tRefreshData.Tick += new EventHandler(cbbBoPhan_SelectionChangeCommitted);
            tRefreshData.Interval = 15000;
            tRefreshData.Enabled = true;

            cbbBoPhan_SelectionChangeCommitted(null, null);
            tRefreshData.Start();
        }

        private void dataOpenForm()
        {
            dtpTuNgay.Value = DateTime.Now;
            dtpDenNgay.Value = DateTime.Now;
             
            lsbp = DashboardSevices.getDivision(dtpTuNgay.Value, dtpDenNgay.Value);
            cbbBoPhan.ValueMember = "id";
            cbbBoPhan.DisplayMember = "name";
            cbbBoPhan.DataSource = lsbp;
            cbbBoPhan.SelectedIndex = 0;

            lbSLKeHoach.Text = "0";
            lbSLThucTe.Text = "0";
            lbTyLe.Text = "0" + " %";
        }

        private void cbbBoPhan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbMMTB.SelectedValue == null || cbbBoPhan.SelectedValue == null)
                return;
            List<searchdb> dssearch = DashboardSevices.getSearch(dtpTuNgay.Value, dtpDenNgay.Value,
                int.Parse(cbbBoPhan.SelectedValue.ToString()), int.Parse(cbbMMTB.SelectedValue.ToString()));

            //chart

            Series kehoach = chart.Series["Kế hoạch"];
            Series thucte = chart.Series["Thực tế"];
            kehoach.Points.Clear();
            thucte.Points.Clear();

            foreach (var item in dssearch)
            {
                kehoach.Points.AddXY(item.masp, item.soKeHoach);
                thucte.Points.AddXY(item.masp, item.soThucTe);
            }

            chart.ChartAreas[0].AxisX.Title = "Mã sản phẩm";
            chart.ChartAreas[0].AxisY.Title = "Số lượng";

            kehoach.IsValueShownAsLabel = true;
            thucte.IsValueShownAsLabel = true;

            var axisX = chart.ChartAreas[0].AxisX;
            axisX.Interval = 1; // Hiển thị nhãn mỗi 1 điểm
            axisX.LabelStyle.IsStaggered = true;
            //axisX.LabelStyle.Angle = -45;

            if (dssearch.Select(x => x.masp).Distinct().Count() > 7)
            {
                axisX.LabelStyle.Angle = -45;
            }
            else
            {

            }

            //tong sl & tyle
            double sumkehoach = dssearch.Sum(sp => sp.soKeHoach);
            double sumthucte = dssearch.Sum(sp => sp.soThucTe);
            lbSLKeHoach.Text = sumkehoach.ToString();
            lbSLThucTe.Text = sumthucte.ToString();
            lbTyLe.Text = Math.Round((sumthucte / sumkehoach) * 100, 1).ToString() + " %";

            //listview
            ListViewItem lv;
            LVThongTin.Items.Clear();
            foreach (var item in dssearch)
            {
                lv = new ListViewItem(item.date.ToString("dd/MM/yyyy"));
                lv.SubItems.Add(item.calv);
                lv.SubItems.Add(item.daychuyen);
                lv.SubItems.Add(item.masp);
                lv.SubItems.Add(item.soKeHoach.ToString());
                lv.SubItems.Add(item.soThucTe.ToString());
                lv.SubItems.Add(item.tyle.ToString());
                lv.SubItems.Add(item.tinhtrang);
                LVThongTin.Items.Add(lv);
            }
            LVThongTin.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void cbbMMTB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbMMTB.SelectedValue == null || cbbBoPhan.SelectedValue == null)
                return;
            List<searchdb> dssearch = DashboardSevices.getSearch(dtpTuNgay.Value, dtpDenNgay.Value,
                int.Parse(cbbBoPhan.SelectedValue.ToString()), int.Parse(cbbMMTB.SelectedValue.ToString()));

            //chart

            Series kehoach = chart.Series["Kế hoạch"];
            Series thucte = chart.Series["Thực tế"];
            kehoach.Points.Clear();
            thucte.Points.Clear();

            foreach (var item in dssearch)
            {
                kehoach.Points.AddXY(item.masp, item.soKeHoach);
                thucte.Points.AddXY(item.masp, item.soThucTe);
            }

            chart.ChartAreas[0].AxisX.Title = "Mã sản phẩm";
            chart.ChartAreas[0].AxisY.Title = "Số lượng";

            kehoach.IsValueShownAsLabel = true;
            thucte.IsValueShownAsLabel = true;

            var axisX = chart.ChartAreas[0].AxisX;
            axisX.Interval = 1; // Hiển thị nhãn mỗi 1 điểm
            axisX.LabelStyle.IsStaggered = true;
            

            if (dssearch.Select(x => x.masp).Distinct().Count() > 7)
            {
                axisX.LabelStyle.Angle = -45;
            }
            else
            {

            }

            //tong sl & tyle
            double sumkehoach = dssearch.Sum(sp => sp.soKeHoach);
            double sumthucte = dssearch.Sum(sp => sp.soThucTe);
            lbSLKeHoach.Text = sumkehoach.ToString();
            lbSLThucTe.Text = sumthucte.ToString();
            lbTyLe.Text = Math.Round((sumthucte / sumkehoach) * 100, 1).ToString() + " %";

            //listview
            ListViewItem lv;
            LVThongTin.Items.Clear();
            foreach (var item in dssearch)
            {
                lv = new ListViewItem(item.date.ToString("dd/MM/yyyy"));
                lv.SubItems.Add(item.calv);
                lv.SubItems.Add(item.daychuyen);
                lv.SubItems.Add(item.masp);
                lv.SubItems.Add(item.soKeHoach.ToString());
                lv.SubItems.Add(item.soThucTe.ToString());
                lv.SubItems.Add(item.tyle.ToString());
                lv.SubItems.Add(item.tinhtrang);
                LVThongTin.Items.Add(lv);
            }
        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            lsbp = DashboardSevices.getDivision(dtpTuNgay.Value, dtpDenNgay.Value);
            cbbBoPhan.ValueMember = "id";
            cbbBoPhan.DisplayMember = "name";
            cbbBoPhan.DataSource = lsbp;
            cbbBoPhan.SelectedIndex = 0;
        }

        private void cbbBoPhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsmm = DashboardSevices.getWorktation(int.Parse(cbbBoPhan.SelectedValue.ToString()));
            cbbMMTB.ValueMember = "id";
            cbbMMTB.DisplayMember = "name";
            cbbMMTB.DataSource = lsmm;
            cbbMMTB.SelectedIndex = 0;
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            
        }

        private void LVThongTin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
