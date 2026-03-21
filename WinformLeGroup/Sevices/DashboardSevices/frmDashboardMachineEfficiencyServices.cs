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
    public class frmDashboardMachineEfficiencyServices
    {
        public static DMMaySX getAllIOTRunTime(string workstation, DateTime date)
        {
            //List<DMMaySX> ret = new List<DMMaySX>();
            DMMaySX ret = new DMMaySX();
            string sql = "";

            try
            {
                sql = $"select max(t0.\"LastLatch\"::TIMESTAMP), max(t0.\"OKCounter\"::NUMERIC), max(t0.\"NGCounter\"::NUMERIC), " + 
                    $" max(t0.\"RunSecond\"::NUMERIC), max(t0.\"StopSecond\"::NUMERIC), max(t2.plannedquantity), max(t0.id) " +
                    $" from fti_poc_iot t0 " +
                    $" left join fti_poc_iot_workstation t1 on t0.\"Serial\" = t1.serial_iot " +
                    $" left join orders_order t2 on t0.\"PONumber\" = t2.number " +
                    $" where t1.workstation_number = '{workstation}' " +
                    $" and \"LastLatch\" ilike '{date.ToString("yyyy-MM-dd")}%' " +
                    $" group by  t0.\"Serial\", t0.\"LastLatch\"";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ret = new DMMaySX()
                            {
                                day = reader.GetDateTime(0),
                                slsx = reader.IsDBNull(1) ? 0 : reader.GetDouble(1),
                                slsx_error = reader.IsDBNull(2) ? 0 : reader.GetDouble(2),
                                runtime = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                                stoptime = reader.IsDBNull(4) ? 0 : reader.GetDouble(4),
                                slkh = reader.IsDBNull(5) ? 0 : reader.GetDouble(5),
                            };
                            
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

        public static List<DMMaySX> getChartOneMon(string workstation, DateTime date)
        {
            List<DMMaySX> ret = new List<DMMaySX>();
            DMMaySX item = null;
            string sql = "";
            DateTime startDate = new DateTime(date.Year, date.Month, 1);
            DateTime endDate = startDate.AddMonths(1);

            try
            {
                sql = $"select max(t0.\"LastLatch\"::TIMESTAMP), max(t0.\"OKCounter\"::NUMERIC), max(t0.\"NGCounter\"::NUMERIC), " +
                    $" max(t0.\"RunSecond\"::NUMERIC), max(t0.\"StopSecond\"::NUMERIC), max(t2.plannedquantity), max(t0.id) " +
                    $" from fti_poc_iot t0 " +
                    $" left join fti_poc_iot_workstation t1 on t0.\"Serial\" = t1.serial_iot " +
                    $" left join orders_order t2 on t0.\"PONumber\" = t2.number " +
                    $" where t1.workstation_number = '{workstation}' " +
                    $" and t0.\"LastLatch\"::TIMESTAMP >= '{startDate.ToString("yyyy-MM-dd 00:00:00")}' " +
                    $" and t0.\"LastLatch\"::TIMESTAMP <  '{endDate.ToString("yyyy-MM-dd 00:00:00")}' " +
                    $" group by t0.\"Serial\", t0.\"LastLatch\"";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item = new DMMaySX()
                            {
                                day = reader.GetDateTime(0),
                                slsx = reader.IsDBNull(1) ? 0 : reader.GetDouble(1),
                                slsx_error = reader.IsDBNull(2) ? 0 : reader.GetDouble(2),
                                runtime = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                                stoptime = reader.IsDBNull(4) ? 0 : reader.GetDouble(4),
                                slkh = reader.IsDBNull(5) ? 0 : reader.GetDouble(5),
                                id = reader.IsDBNull(6) ? 0 : reader.GetInt32(6)
                            };
                            ret.Add(item);
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
