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
using WinformLeGroup.Models.Dashboard;
using WinformLeGroup.Sevices.DashboardSevices;

namespace WinformLeGroup.Forms.Dashboard
{
    public partial class frmDashboardBTBD : Form
    {
        public frmDashboardBTBD()
        {
            InitializeComponent();
            loadData();
        }

        private void loadData()
        {
            List<DMByTT> dsTT = frmDashboardBTBDService.getTrangThai();
            List<DMByBoPhan> dsBP = frmDashboardBTBDService.GetBoPhan();
            List<DMBTBD> dsBTBD = frmDashboardBTBDService.GetBTBD();

            Series pieSeriesTT = chartTT.Series["SeriesTT"];
            Series pieSeriesBP = chartBP.Series["SeriesBP"];

            //chart 1
            foreach (var item in dsTT)
            {
                pieSeriesTT.Points.AddXY(item.trangthai, Math.Round(double.Parse(item.sl.ToString()) / double.Parse(dsBTBD.Count.ToString()) * 100, 1));
            }
            pieSeriesTT.IsValueShownAsLabel = true;
            //chartTT.Series.Add(pieSeriesTT);

            pieSeriesTT.Points[0].Color = Color.Green;
            pieSeriesTT.Points[1].Color = Color.Blue;
            pieSeriesTT.Points[2].Color = Color.Orange;

            //chart 2
            foreach (var item in dsBP)
            {
                pieSeriesBP.Points.AddXY(item.bophan, Math.Round(double.Parse(item.sl.ToString()) / double.Parse(dsBTBD.Count.ToString()) * 100, 1));
            }
            pieSeriesBP.IsValueShownAsLabel = true;
            //chartTT.Series.Add(pieSeriesBP);

            //listview
            ListViewItem lv;
            LVTT.Items.Clear();
            foreach (var item in dsBTBD)
            {
                lv = new ListViewItem(item.malenh);
                lv.SubItems.Add(item.mamay);
                lv.SubItems.Add(item.tenmay);
                lv.SubItems.Add(item.makhuon);
                lv.SubItems.Add(item.tenkhuon);
                lv.SubItems.Add(item.bophan);
                lv.SubItems.Add(item.ngayTT);
                lv.SubItems.Add(item.nguoiTT);
                lv.SubItems.Add(item.trangthai);
                LVTT.Items.Add(lv);
            }

        }
    }
}
