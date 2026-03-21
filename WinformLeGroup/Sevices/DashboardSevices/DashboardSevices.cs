using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformLeGroup.Models.GSAppLibrary;
using WinformLeGroup.Models.Dashboard;
using WinformLeGroup.Models.CommonModel;

namespace WinformLeGroup.Sevices.DashboardSevices
{
    public class DashboardSevices
    {
        public static List<Data_BoPhan> getDivision(DateTime tungay, DateTime denngay)
        {
            List<Data_BoPhan> ret = new List<Data_BoPhan>();
            Data_BoPhan item = null;
            string sql = "";

            try
            {
                item = new Data_BoPhan()
                {
                    id = 0,
                    name = ""
                };
                ret.Insert(0, item);
                sql = " select DISTINCT t1.id, t1.name " +
                    " from orders_operationaltask t0 " +
                    " inner join basic_division t1 on t0.division_id = t1.id " +
                    " where t0.startdate between '" + tungay.ToString("yyyy-MM-dd 00:00:00") + "' and '" + denngay.ToString("yyyy-MM-dd 23:59:59") + "' ";
                sql += "order by t1.name asc";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item = new Data_BoPhan()
                            {
                                id = reader.GetInt32(0),
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

        public static List<DMWorktation> getWorktation(int idbp)
        {
            List<DMWorktation> ret = new List<DMWorktation>();
            DMWorktation item = null;
            string sql = "";

            try
            {
                item = new DMWorktation()
                {
                    id = 0,
                    name = ""
                };
                ret.Insert(0, item);
                sql = " select DISTINCT t1.id, t1.name " +
                    " from orders_operationaltask t0 " +
                    " inner join basic_workstation t1 on t0.workstation_id = t1.id " +
                    " where t0.division_id = " + idbp +
                    " order by t1.name asc ";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item = new DMWorktation()
                            {
                                id = reader.GetInt32(0),
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

        public static List<searchdb> getSearch(DateTime tungay, DateTime denngay, int bophan, int mm)
        {
            List<searchdb> ret = new List<searchdb>();
            searchdb item = null;
            string sql = "";

            try
            {
                sql = " select t0.product_id, MIN(t0.startdate) AS startdate, MAX(t7.name) AS ca, " +
                    " MAX(t3.name) AS daychuyen, MAX(t4.number) AS masp, MAX(t2.plannedquantity) AS sohk, " +
                    " MAX(t2.donequantity) AS sott " +
                    " from orders_operationaltask t0 " +
                    " left join technologies_technologyoperationcomponent t1 on t0.technologyoperationcomponent_id = t1.id " +
                    " left join orders_order t2 on t0.order_id = t2.id " +
                    " left join productionlines_productionline t3 on t2.productionline_id = t3.id " +
                    " left join basic_product t4 on t0.product_id = t4.id " +
                    " left join basic_workstation t5 on t0.workstation_id = t5.id " +
                    " left join productioncounting_productiontracking t6 on t6.technologyoperationcomponent_id = t1.id " +
                    " left join basic_shift t7 on t6.shift_id = t7.id " +
                    " where 1=1 " +
                    " and t0.startdate between '"+tungay.ToString("yyyy-MM-dd 00:00:00") +"' and '"+denngay.ToString("yyyy-MM-dd 23:59:59") + "' ";
                if (bophan > 0)
                    sql += " and t0.division_id = " + bophan;
                if (mm > 0)
                    sql += " and t0.workstation_id = " + mm;
                sql += " GROUP BY t0.product_id ";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item = new searchdb()
                            {
                                date = reader.GetDateTime(1),
                                calv = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                daychuyen = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                masp = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                soKeHoach = reader.IsDBNull(5) ? 0 : reader.GetDouble(5),
                                soThucTe = reader.IsDBNull(6) ? 0 : reader.GetDouble(6),
                                tinhtrang = "Đạt"
                            };
                            item.tyle = Math.Round((item.soThucTe / item.soKeHoach) * 100, 1);
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
