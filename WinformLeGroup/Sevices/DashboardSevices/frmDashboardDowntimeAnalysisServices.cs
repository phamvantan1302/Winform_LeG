using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformLeGroup.Models.DashboardIOT;
using WinformLeGroup.Models.GSAppLibrary;

namespace WinformLeGroup.Sevices.DashboardSevices
{
    public class frmDashboardDowntimeAnalysisServices
    {
        public static string getLineIOT(string workstation, DateTime date)
        {
            string ret = "0";
            DMLine item = null;
            string sql = "";

            try
            {
                sql = $"select \"StopSecond\" \r\n\t" +
                    $"from fti_poc_iot t0\r\n\t" +
                    $"left join fti_poc_iot_workstation t1 on t0.\"Serial\" = t1.serial_iot\r\n\t" +
                    $"where t1.workstation_number = '{workstation}' " +
                    $"and t0.\"LastLatch\" ilike '{date.ToString("yyyy-MM-dd")}%' " +
                    $" order by t0.id desc " +
                    $" limit 1 ";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ret = reader.IsDBNull(0) ? "0" : reader.GetString(0);
                        }
                        reader.Close();
                        command.Dispose();
                    }
                }
                ret = (Math.Round(double.Parse(ret)/60, 2)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ret;
        }
    }
}
