using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformLeGroup.Models.CommonModel;
using WinformLeGroup.Models.DashboardIOT;
using WinformLeGroup.Models.GSAppLibrary;

namespace WinformLeGroup.Sevices.DashboardSevices
{
    public class frmProductionOverviewServices
    {
        public static double getSLSXIOTBySerial(string workstation, DateTime date)
        {
            double ret = 0;
            string sql = "";

            try
            {
                sql = $"select t0.\"OKCounter\"::NUMERIC " +
                    $" from fti_poc_iot t0 " +
                    $" left join fti_poc_iot_workstation t1 on t0.\"Serial\" = t1.serial_iot " +
                    //$" left join orders_order t2 on t0.\"PONumber\" = t2.number " +
                    $" where t1.workstation_number = '{workstation}' " +
                    $" and t0.\"LastLatch\" ilike '{date.ToString("yyyy-MM-dd")}%' " +
                    $" order by t0.id desc " +
                    $" limit 1";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ret = reader.IsDBNull(0) ? 0 : reader.GetDouble(0);
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

        public static List<DMWorkstation> getWorkstationIOT()
        {
            List<DMWorkstation> ret = new List<DMWorkstation>();
            DMWorkstation item = null;
            string sql = "";

            try
            {
                sql = $"select t0.workstation_number, t1.name " +
                    $" from fti_poc_iot_workstation t0 " +
                    $" left join basic_workstation t1 on t0.workstation_number = t1.number ";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item = new DMWorkstation()
                            {
                                number = reader.IsDBNull(0) ? "" : reader.GetString(0),
                                name = reader.IsDBNull(1) ? "" : reader.GetString(1),
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

        public static List<DMLine> getLineIOT()
        {
            List<DMLine> ret = new List<DMLine>();
            DMLine item = null;
            string sql = "";

            try
            {
                sql = $"select t0.line_number  " +
                    $" from fti_poc_iot_workstation t0 " +
                    $" group by t0.line_number ";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item = new DMLine()
                            {
                                number = reader.IsDBNull(0) ? "" : reader.GetString(0),
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

        public static double getSLKH(string workstation, DateTime date)
        {
            double ret = 0;
            DMMaySX item = null;
            string sql = "";

            try
            {
                sql = $"select t2.plannedquantity " +
                    $" from fti_poc_iot t0 " +
                    $" left join fti_poc_iot_workstation t1 on t0.\"Serial\" = t1.serial_iot " +
                    $" left join orders_order t2 on t0.\"PONumber\" = t2.number " +
                    $" where t1.workstation_number = '{workstation}' " +
                    $" and t0.\"LastLatch\" ilike '{date.ToString("yyyy-MM-dd")}%' " +
                    $" order by t0.id desc " +
                    $" limit 1";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ret = reader.IsDBNull(0) ? 0 : reader.GetDouble(0);
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

        public static double getSLNG(string workstation, DateTime date)
        {
            double ret = 0;
            DMMaySX item = null;
            string sql = "";

            try
            {
                sql = $"select max(t0.\"NGCounter\"::NUMERIC) " +
                    $" from fti_poc_iot t0 " +
                    $" left join fti_poc_iot_workstation t1 on t0.\"Serial\" = t1.serial_iot " +
                    $" left join orders_order t2 on t0.\"PONumber\" = t2.number " +
                    $" where t1.workstation_number = '{workstation}' " +
                    $" and t0.\"LastLatch\" ilike '{date.ToString("yyyy-MM-dd")}%' " +
                    $" group by t0.\"Serial\"";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ret = reader.IsDBNull(0) ? 0 : reader.GetDouble(0);
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

        public static string getProduction(string workstation, DateTime date)
        {
            string ret = "";
            DMMaySX item = null;
            string sql = "";

            try
            {
                sql = $"select max(t0.\"ProductCode\"), max(t2.name) " +
                    $" from fti_poc_iot t0 " +
                    $" left join fti_poc_iot_workstation t1 on t0.\"Serial\" = t1.serial_iot " +
                    $" left join basic_product t2 on t0.\"ProductCode\" = t2.number " +
                    $" where t1.workstation_number = '{workstation}' " +
                    $" and t0.\"LastLatch\" ilike '{date.ToString("yyyy-MM-dd")}%' " +
                    $" group by t0.\"Serial\"";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ret = (reader.IsDBNull(0) && reader.IsDBNull(1)) ? "" : (reader.GetString(0) + " - " + reader.GetString(1));
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

        public static List<DMWeekProduction> getChartOneMon(string workstation, DateTime date)
        {
            List<DMWeekProduction> ret = new List<DMWeekProduction>();
            DMWeekProduction item = null;
            string sql = "";
            DateTime startDate = new DateTime(date.Year, date.Month, 1);
            DateTime endDate = startDate.AddMonths(1);

            try
            {
                sql = $"select t0.\"Serial\", max(t0.\"OKCounter\"::NUMERIC), max(t0.\"LastLatch\"::TIMESTAMP), max(t2.plannedquantity), max(t0.id) " +
                    $" from fti_poc_iot t0 " +
                    $" left join fti_poc_iot_workstation t1 on t0.\"Serial\" = t1.serial_iot " +
                    " left join orders_order t2 on t0.\"PONumber\" = t2.number " +
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
                            item = new DMWeekProduction()
                            {
                                day = reader.GetDateTime(2),
                                slsxTT = reader.IsDBNull(1) ? 0 : reader.GetDouble(1),
                                slsxKH = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                                id = reader.IsDBNull(4) ? 0 : reader.GetInt32(4)
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

        public static List<DMWeekProduction> getChartOneMonLine(string workstation, DateTime date)
        {
            List<DMWeekProduction> ret = new List<DMWeekProduction>();
            DMWeekProduction item = null;
            string sql = "";
            DateTime startDate = new DateTime(date.Year, date.Month, 1);
            DateTime endDate = startDate.AddMonths(1);

            try
            {
                sql = $"select t0.\"Serial\", max(t0.\"OKCounter\"::NUMERIC), max(t0.\"LastLatch\"::TIMESTAMP), max(t0.\"PlanTarget\"::NUMERIC) " +
                    $" from fti_poc_iot t0 " +
                    $" left join fti_poc_iot_workstation t1 on t0.\"Serial\" = t1.serial_iot " +
                    //$" left join orders_order t2 on t0.\"PONumber\" = t2.number " +
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
                            item = new DMWeekProduction()
                            {
                                day = reader.GetDateTime(2),
                                slsxTT = reader.IsDBNull(1) ? 0 : reader.GetDouble(1),
                                slsxKH = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
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

        public static List<DMSerialWorkstation> getSerialWorkstation()
        {
            List<DMSerialWorkstation> ret = new List<DMSerialWorkstation>();
            DMSerialWorkstation item = null;
            string sql = "";

            try
            {
                sql = "select id, serial_iot, workstation_number " +
                    " from fti_poc_iot_workstation " +
                    " order by id";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item = new DMSerialWorkstation()
                            {
                                id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                                serial_iot = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                workstation_number = reader.IsDBNull(2) ? "" : reader.GetString(2)
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

        public static List<DMTTMay> getTTWorkstation(string number)
        {
            List<DMTTMay> ret = new List<DMTTMay>();
            DMTTMay item = null;
            string sql = "";

            try
            {
                sql = $"select t1.name, t2.name " +
                    $"from basic_workstation t0 " +
                    $"left join basic_division t1 on t0.division_id = t1.id " +
                    $"left join productionlines_productionline t2 on t0.productionline_id = t2.id " +
                    //$"left join basic_product t3 on t0.productionline_id = t2.id " +
                    $"where t0.number = '{number}'";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item = new DMTTMay()
                            {
                                division = reader.IsDBNull(0) ? "" : reader.GetString(0),
                                productionline = reader.IsDBNull(1) ? "" : reader.GetString(1),
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
