using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformLeGroup.Models.DashboardIOT;
using WinformLeGroup.Sevices.DashboardSevices;

namespace WinformLeGroup.Forms.Dashboard
{
    public partial class frmIoTMonitoring : Form
    {
        private System.Windows.Forms.Timer tRefreshData;
        private bool isFormClosing = false;
        public frmIoTMonitoring()
        {
            InitializeComponent();

            loadDataOpen();

            loadform(null, null);
            tRefreshData = new System.Windows.Forms.Timer();
            tRefreshData.Tick += new EventHandler(loadform);
            tRefreshData.Interval = 8000;
            tRefreshData.Enabled = true;
        }

        private void loadDataOpen()
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
        }

        private void productInformation()
        {
            DateTime date = dtpDay.Value;
            string numberWorkstation = "", nameStatus = "";
            if (cbbWorkstation.SelectedValue != null)
                numberWorkstation = cbbWorkstation.SelectedValue.ToString();
            double valueStatus = frmIoTMonitoringServices.getAllStatusW(numberWorkstation, date);
            lbNameMay1.Text = cbbWorkstation.SelectedValue.ToString();
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
            //txtProduct.Text = frmProductionOverviewServices.getProduction(numberWorkstation, date);
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

        private void guna2DateTimePicker1_DropDown(object sender, EventArgs e)
        {
            cbbWorkstation_SelectedIndexChanged(null, null);
        }

        private void frmIoTMonitoring_FormClosing(object sender, FormClosingEventArgs e)
        {
            isFormClosing = true;

            if (tRefreshData != null)
            {
                tRefreshData.Stop();
                tRefreshData.Dispose();
                tRefreshData = null;
            }
        }
    }
}
