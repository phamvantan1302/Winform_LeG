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
using System.Windows.Forms.DataVisualization.Charting;
using WinformLeGroup.Models.DashboardIOT;
using WinformLeGroup.Sevices.DashboardSevices;

namespace WinformLeGroup.Forms.Dashboard
{
    public partial class frmDashboardMachineEfficiency : Form
    {
        private System.Windows.Forms.Timer tRefreshData;
        private bool isFormClosing = false;
        public frmDashboardMachineEfficiency()
        {
            InitializeComponent();

            dataOpenForm();
            loadform(null, null);

            tRefreshData = new System.Windows.Forms.Timer();
            tRefreshData.Tick += new EventHandler(loadform);
            tRefreshData.Interval = 15000;
            tRefreshData.Enabled = true;
        }

        private void dataOpenForm()
        {
            dtpDay.Value = DateTime.Now;

            List<DMWorkstation> lsW = frmProductionOverviewServices.getWorkstationIOT();
            cbbWorkstation.ValueMember = "number";
            cbbWorkstation.DisplayMember = "note";
            cbbWorkstation.DataSource = lsW;
            if (lsW.Count > 0)
                cbbWorkstation.SelectedIndex = 0;
        }

        private void loadform(object sender, EventArgs e)
        {
            productInformation();
            chartInformation1();
            chartInformation2();
        }

        private void productInformation()
        {
            DateTime date = dtpDay.Value;
            string numberWorkstation = "";
            if (cbbWorkstation.SelectedValue != null)
                numberWorkstation = cbbWorkstation.SelectedValue.ToString();
            DMMaySX item = frmDashboardMachineEfficiencyServices.getAllIOTRunTime(numberWorkstation, date);
            if(item != null)
            {
                if (item.slkh == 0)
                    item.slkh = item.slsx;
                double tghd = Math.Round(item.runtime / (item.runtime + item.stoptime), 3),
                    hsvh = Math.Round(item.slsx / item.slkh, 3), 
                    tlsp = Math.Round(item.slsx / (item.slsx + item.slsx_error), 3);
                lbRunTime.Text = (tghd * 100).ToString() + "%";
                lbHieuSuat.Text = (hsvh * 100).ToString() + "%";
                lbTyleDat.Text = (tlsp * 100).ToString() + "%";
                lbOEE.Text = Math.Round((tghd * hsvh * tlsp)*100, 1).ToString() + "%";
                LBSumTime.Text = Math.Round((item.runtime + item.stoptime) / 60, 2).ToString() + "";
            }
            else
            {
                lbRunTime.Text = "0%";
                lbHieuSuat.Text = "0%";
                lbTyleDat.Text = "0%";
                lbOEE.Text = "0%";
                LBSumTime.Text = "0";
            }

        }

        private void chartInformation1()
        {
            Series kehoach = chartMachineLine.Series["Series"];
            kehoach.Points.Clear();

            chartMachineLine.ChartAreas[0].AxisY.Minimum = 0;
            chartMachineLine.ChartAreas[0].AxisY.Maximum = 100;
            chartMachineLine.ChartAreas[0].AxisY.Interval = 10;

            List<DMLineSX> lskehoach = dataSanPham();

            foreach (var item in lskehoach)
            {
                int indexoee = kehoach.Points.AddXY(item.line, item.slsx);

                kehoach.Points[indexoee].ToolTip = $"{item.line}\n Target:" + item.slsx.ToString();
            }
        }

        private void chartInformation2()
        {
            Series oee = chartOEE.Series["OEE"];
            Series kehoach = chartOEE.Series["Target"];
            kehoach.Points.Clear();
            oee.Points.Clear();

            chartOEE.ChartAreas[0].AxisX.Interval = 1;
            chartOEE.ChartAreas[0].AxisY.Minimum = 50;
            chartOEE.ChartAreas[0].AxisY.Maximum = 95;
            //chartOEE.ChartAreas[0].AxisY.Interval = 3;

            List<DMWeekProduction> lskehoach = datakehoach();

            foreach (var item in lskehoach)
            {
                // ngat line
                double yKH = item.slsxKH;
                double yTT = item.slsxTT;

                if (double.IsNaN(yKH) || double.IsInfinity(yKH)) yKH = 0;
                if (double.IsNaN(yTT) || double.IsInfinity(yTT)) yTT = 0;

                int indexKH = kehoach.Points.AddXY(item.day.ToString("dd"), yKH);
                int indexoee = oee.Points.AddXY(item.day.ToString("dd"), yTT);

                // lien line
                //int indexKH = kehoach.Points.AddXY(item.day, item.slsxKH);
                //int indexoee = oee.Points.AddXY(item.day, item.slsxTT);

                oee.Points[indexoee].ToolTip = $"{item.day.ToString("dd/MM/yyyy")}\n OEE:" + item.slsxTT.ToString();
            }
        }

        private List<DMLineSX> dataSanPham()
        {
            DateTime date = dtpDay.Value;
            List<DMLineSX> ls = new List<DMLineSX>();
            DMLineSX dm = null;

            dm = new DMLineSX()
            {
                line = "RBH21"
            };
            DMMaySX item = frmDashboardMachineEfficiencyServices.getAllIOTRunTime(dm.line, date);
            if (item != null)
            {
                if (item.slkh == 0)
                    item.slkh = item.slsx;
                double tghd = Math.Round(item.runtime / (item.runtime + item.stoptime), 3),
                    hsvh = Math.Round(item.slsx / item.slkh, 3),
                    tlsp = Math.Round(item.slsx / (item.slsx + item.slsx_error), 3);
                dm.slsx = Math.Round((tghd * hsvh * tlsp) * 100, 1);
            }
            else 
                dm.slsx = 0;
            ls.Add(dm);

            dm = new DMLineSX()
            {
                line = "RBH53"
            };
            DMMaySX item1 = frmDashboardMachineEfficiencyServices.getAllIOTRunTime(dm.line, date);
            if (item1 != null)
            {
                if (item1.slkh == 0)
                    item1.slkh = item1.slsx;
                double tghd = Math.Round(item1.runtime / (item1.runtime + item1.stoptime), 3),
                    hsvh = Math.Round(item1.slsx / item1.slkh, 3),
                    tlsp = Math.Round(item1.slsx / (item1.slsx + item1.slsx_error), 3);
                dm.slsx = Math.Round((tghd * hsvh * tlsp) * 100, 1);
            }
            else
                dm.slsx = 0;
            ls.Add(dm);

            return ls;
        }

        private List<DMWeekProduction> datakehoach()
        {
            DateTime date = dtpDay.Value;
            string numberWorkstation = "";
            if (cbbWorkstation.SelectedValue != null)
                numberWorkstation = cbbWorkstation.SelectedValue.ToString();
            List<DMMaySX> ls = frmDashboardMachineEfficiencyServices.getChartOneMon(numberWorkstation, date);
            List<DMWeekProduction> lschart = new List<DMWeekProduction>();
            DMWeekProduction it = null;

            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            for (int i = 1; i <= daysInMonth; i++)
            {
                int a = 0;
                DateTime cvdate = new DateTime(date.Year, date.Month, i);
                if (cvdate.ToString("yyyy-MM-dd") == "2026-03-18")
                    a = 1;
                DMMaySX dmchart = ls.Where(x => x.day.Date.ToString("yyyy-MM-dd") == cvdate.Date.ToString("yyyy-MM-dd")).OrderByDescending(x => x.id).FirstOrDefault();
                if (dmchart != null)
                {
                    if (dmchart.slkh == 0)
                        dmchart.slkh = dmchart.slsx;
                    double tghd = Math.Round(dmchart.runtime / (dmchart.runtime + dmchart.stoptime), 3),
                        hsvh = Math.Round(dmchart.slsx / dmchart.slkh, 3),
                        tlsp = Math.Round(dmchart.slsx / (dmchart.slsx + dmchart.slsx_error), 3);
                    it = new DMWeekProduction()
                    {
                        id = i,
                        day = cvdate,
                        slsxKH = 85,
                        slsxTT = (tghd * hsvh * tlsp)*100
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

        private void frmDashboardMachineEfficiency_FormClosing(object sender, FormClosingEventArgs e)
        {
            isFormClosing = true;

            if (tRefreshData != null)
            {
                tRefreshData.Stop();
                tRefreshData.Dispose();
                tRefreshData = null;
            }
        }

        private void guna2DateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            cbbWorkstation_SelectedIndexChanged(null, null);
        }

        private void cbbWorkstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<DMTTMay> lsW = frmProductionOverviewServices.getTTWorkstation(cbbWorkstation.SelectedValue.ToString());
            if (lsW.Count > 0)
            {
                txtLine.Text = lsW[0].productionline.ToString();
                txtDivision.Text = lsW[0].division.ToString();
            }
            else
            {
                txtLine.Text = "";
                txtDivision.Text = "";
            }
            loadform(null, null);
        }
    }
}
