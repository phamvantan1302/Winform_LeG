using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformLeGroup.Models.CommonModel;
using WinformLeGroup.Models.GSAppLibrary;

namespace WinformLeGroup.Sevices.DashboardSevices
{
    public class frmIoTMonitoringServices
    {
        public static double getAllStatusW(string workstation, DateTime date)
        {
            double ret = 5;
            DMStatus item = null;
            string sql = "";

            try
            {
                sql = $" select distinct on (t0.\"Serial\") t1.\"workstation_number\", t0.\"StopValue\"::numeric " +
                    $" from fti_poc_iot t0 " +
                    $" left join fti_poc_iot_workstation t1 on t0.\"Serial\" = t1.serial_iot " +
                    $" where t1.workstation_number in ('{workstation}') " +
                    $" and t0.\"LastLatch\" ilike '{date.ToString("yyyy-MM-dd")}%' " +
                    $" order by t0.\"Serial\", t0.id desc " +
                    $" limit 2";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ret = reader.IsDBNull(1) ? 5 : reader.GetDouble(1);
                        }
                        reader.Close();
                        command.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ret;
        }
    }
}
