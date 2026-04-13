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
    public partial class frmProductionOverview : Form
    {
        private System.Windows.Forms.Timer tRefreshData;
        private bool isFormClosing = false;
        public frmProductionOverview()
        {
            InitializeComponent();
            dtpDay.Value = DateTime.Now;
            dataOpenForm();

            loadform(null, null);
            tRefreshData = new System.Windows.Forms.Timer();
            tRefreshData.Tick += new EventHandler(loadform);
            tRefreshData.Interval = 15000;
            tRefreshData.Enabled = true;
        }

        private void dataOpenForm()
        {
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

        private void frmProductionOverview_FormClosing(object sender, FormClosingEventArgs e)
        {
            isFormClosing = true;

            if (tRefreshData != null)
            {
                tRefreshData.Stop();
                tRefreshData.Dispose();
                tRefreshData = null;
            }
        }

        private void productInformation()
        {
            DateTime date = dtpDay.Value;
            string numberWorkstation = "";
            if(cbbWorkstation.SelectedValue != null)
                numberWorkstation = cbbWorkstation.SelectedValue.ToString();
            double sltt = Math.Floor(frmProductionOverviewServices.getSLSXIOTBySerial(numberWorkstation, date)/2),
                slkh = frmProductionOverviewServices.getSLKH(numberWorkstation, date);
            lbDailyProduction.Text = sltt.ToString();
            lbTargetAchievement.Text = slkh.ToString();
            lbOverallEfficiency.Text = Math.Round((sltt/slkh)*100, 2).ToString() + "%";
            lbDefectRate.Text = frmProductionOverviewServices.getSLNG(numberWorkstation, date).ToString();
            //txtProduct.Text = frmProductionOverviewServices.getProduction(numberWorkstation, date);
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

                int indexKH = kehoach.Points.AddXY(item.day.ToString("dd"), yKH);
                int indexTT = thucte.Points.AddXY(item.day.ToString("dd"), yTT);

                // lien line
                //kehoach.Points.AddXY(item.day.ToString("dd"), item.slsxKH); 
                //thucte.Points.AddXY(item.day.ToString("dd"), item.slsxTT);

                // show TT tại điểm thay đổi
                kehoach.Points[indexKH].ToolTip = $"{item.day.ToString("dd/MM/yyyy")}\n Target: {yKH}";
                thucte.Points[indexTT].ToolTip = $"{item.day.ToString("dd/MM/yyyy")}\n Actual Producttion: {yTT}";
            }

        }

        private void chartInformation2()
        {
            
        }

        private List<DMWeekProduction> datakehoach()
        {
            DateTime date = dtpDay.Value;
            string numberWorkstation = "";
            if (cbbWorkstation.SelectedValue != null)
                numberWorkstation = cbbWorkstation.SelectedValue.ToString();
            List<DMWeekProduction> ls = frmProductionOverviewServices.getChartOneMon(numberWorkstation, dtpDay.Value);
            List<DMWeekProduction> lschart = new List<DMWeekProduction>();
            DMWeekProduction it = null;
            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            for (int i = 1; i <= daysInMonth; i++)
            {
                DateTime cvdate = new DateTime(date.Year, date.Month, i);
                DMWeekProduction dmchart = ls.Where(x => x.day.Date.ToString("yyyy-MM-dd") == cvdate.Date.ToString("yyyy-MM-dd")).OrderByDescending(x => x.id).FirstOrDefault();
                if(dmchart != null)
                {
                    it = new DMWeekProduction()
                    {
                        id = i,
                        day = cvdate,
                        slsxKH = dmchart.slsxKH,
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

        private void frmProductionOverview_Load(object sender, EventArgs e)
        {

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

        private void dtpDay_CloseUp(object sender, EventArgs e)
        {
            cbbWorkstation_SelectedIndexChanged(null, null);
        }
    }
}
