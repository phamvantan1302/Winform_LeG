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
using WinformLeGroup.Models.DashboardIOT;
using WinformLeGroup.Sevices.DashboardSevices;

namespace WinformLeGroup.Forms.Dashboard
{
    public partial class frmDashboardDowntimeAnalysis : Form
    {
        private System.Windows.Forms.Timer tRefreshData;
        private bool isFormClosing = false;

        public frmDashboardDowntimeAnalysis()
        {
            InitializeComponent();
            dataOpenForm();

            dtpDay.Value = DateTime.Now;

            loadform(null, null);
            tRefreshData = new System.Windows.Forms.Timer();
            tRefreshData.Tick += new EventHandler(loadform);
            tRefreshData.Interval = 15000;
            tRefreshData.Enabled = true;
        }

        private void dataOpenForm()
        {
            List<DMLine> lsW = frmProductionOverviewServices.getLineIOT();
            cbbWorkstation.ValueMember = "number";
            cbbWorkstation.DisplayMember = "number";
            cbbWorkstation.DataSource = lsW;
            if (lsW.Count > 0)
                cbbWorkstation.SelectedIndex = 0;
        }

        private void loadform(object sender, EventArgs e)
        {
            productInformation();
            chartInformation1();
        }

        private void productInformation()
        {
            DateTime date = dtpDay.Value;
            string numberWorkstation = "", nameStatus = "";
            if (string.IsNullOrEmpty(txtShowMay.Text))
                numberWorkstation = "RBH21";
            //double valueStatus = frmIoTMonitoringServices.getAllStatusW(numberWorkstation, date);
            //lbNameMay1.Text = cbbWorkstation.SelectedValue.ToString();
            lbNameMay1.Text = "RBH21";
            lbNameMay2.Text = "RBH53";
            double valueStatus = frmIoTMonitoringServices.getAllStatusW(lbNameMay1.Text, date);
            double valueStatus1 = frmIoTMonitoringServices.getAllStatusW(lbNameMay2.Text, date);
            if (valueStatus < 5)
            {
                lbStatusMay1.Text = "RUNNING";
                txtMay1.FillColor = Color.MediumSeaGreen;
                lbNameMay1.BackColor = Color.MediumSeaGreen;
                lbStatusMay1.BackColor = Color.MediumSeaGreen;
            }
            else
            {
                lbStatusMay1.Text = "DOWN";
                txtMay1.FillColor = Color.Red;
                lbNameMay1.BackColor = Color.Red;
                lbStatusMay1.BackColor = Color.Red;
            }

            if (valueStatus1 < 5)
            {
                lbStatusMay2.Text = "RUNNING";
                txtMay2.FillColor = Color.MediumSeaGreen;
                lbNameMay2.BackColor = Color.MediumSeaGreen;
                lbStatusMay2.BackColor = Color.MediumSeaGreen;
            }
            else
            {
                lbStatusMay2.Text = "DOWN";
                txtMay2.FillColor = Color.Red;
                lbNameMay2.BackColor = Color.Red;
                lbStatusMay2.BackColor = Color.Red;
            }

            LBTimeStop.Text = frmDashboardDowntimeAnalysisServices.getLineIOT(numberWorkstation, date);
        }

        private void chartInformation1()
        {
            Series kehoach = chartWeekProd.Series["Target"];
            Series thucte = chartWeekProd.Series["Actual Producttion"];
            kehoach.Points.Clear();
            thucte.Points.Clear();
            chartWeekProd.ChartAreas[0].AxisX.Interval = 1;
            List<DMWeekProduction> lskehoach = datakehoach();

            foreach (var item in lskehoach)
            {
                // ngat line
                double yKH = item.slsxKH == 0 ? double.NaN : item.slsxKH;
                double yTT = item.slsxTT == 0 ? double.NaN : item.slsxTT;

                int indexKH = kehoach.Points.AddXY(item.day.ToString("dd"), 0);
                int indexTT = thucte.Points.AddXY(item.day.ToString("dd"), yTT);

                // lien line
                //kehoach.Points.AddXY(item.day.ToString("dd"), item.slsxKH); 
                //thucte.Points.AddXY(item.day.ToString("dd"), item.slsxTT);

                // show TT tại điểm thay đổi
                //kehoach.Points[indexKH].ToolTip = $"{item.day.ToString("dd/MM/yyyy")}\n Target: {yKH}";
                thucte.Points[indexTT].ToolTip = $"{item.day.ToString("dd/MM/yyyy")}\n Actual Producttion: {yTT}";
            }
        }

        private List<DMWeekProduction> datakehoach()
        {
            DateTime date = dtpDay.Value;
            string numberWorkstation = "";
            if (string.IsNullOrEmpty(txtShowMay.Text))
                numberWorkstation = "RBH21";
            List<DMWeekProduction> ls = frmProductionOverviewServices.getChartOneMonLine(numberWorkstation, dtpDay.Value);
            List<DMWeekProduction> lschart = new List<DMWeekProduction>();
            DMWeekProduction it = null;
            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            for (int i = 1; i <= daysInMonth; i++)
            {
                DateTime cvdate = new DateTime(date.Year, date.Month, i);
                DMWeekProduction dmchart = ls.FirstOrDefault(x => x.day.ToString("yyyy-MM-dd") == cvdate.ToString("yyyy-MM-dd"));
                if (dmchart != null)
                {
                    it = new DMWeekProduction()
                    {
                        id = i,
                        day = cvdate,
                        slsxKH = 0,
                        slsxTT = dmchart.slsxTT
                    };
                    lschart.Add(it);
                }
                else
                {
                    it = new DMWeekProduction()
                    {
                        id = i,
                        day = cvdate,
                        slsxKH = 0,
                        slsxTT = 0
                    };
                    lschart.Add(it);
                }
            }

            return lschart;
        }

        private void frmDashboardDowntimeAnalysis_FormClosing(object sender, FormClosingEventArgs e)
        {
            isFormClosing = true;

            if (tRefreshData != null)
            {
                tRefreshData.Stop();
                tRefreshData.Dispose();
                tRefreshData = null;
            }
        }

        private void txtMay1_MouseClick(object sender, MouseEventArgs e)
        {
            txtShowMay.Text = lbNameMay1.Text;
        }

        private void txtMay2_MouseClick(object sender, MouseEventArgs e)
        {
            txtShowMay.Text = lbNameMay2.Text;
        }

        private void txtShowMay_TextChanged(object sender, EventArgs e)
        {
            loadform(null, null);
        }
    }
}
