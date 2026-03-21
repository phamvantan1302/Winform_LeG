using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformLeGroup.Models.Dashboard;
using WinformLeGroup.Models.GSAppLibrary;

namespace WinformLeGroup.Sevices.DashboardSevices
{
    public class frmDashboardBTBDService
    {
        public static List<DMByTT> getTrangThai()
        {
            List<DMByTT> ret = new List<DMByTT>();
            DMByTT item = null;
            string sql = "";

            try
            {
                sql = " SELECT t0.state, COUNT(*) AS total " +
                    " FROM cmmsmachineparts_plannedevent t0 " +
                    " where t0.state in ('04inRealization','05realized','01new') " +
                    " GROUP BY t0.state";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item = new DMByTT()
                            {
                                tt = reader.IsDBNull(0) ? "" : reader.GetString(0),
                                sl = reader.GetInt32(1),
                            };
                            if (item.tt == "01new")
                                item.trangthai = "Khởi tạo";
                            else if (item.tt == "04inRealization")
                                item.trangthai = "Đang thực hiện";
                            else if (item.tt == "05realized")
                                item.trangthai = "Hoàn thành";
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

        public static List<DMByBoPhan> GetBoPhan()
        {
            List<DMByBoPhan> ret = new List<DMByBoPhan>();
            DMByBoPhan item = null;
            string sql = "";

            try
            {
                sql = " SELECT t0.division_id, min(t1.name), COUNT(*) AS total " +
                    " FROM cmmsmachineparts_plannedevent t0 " +
                    " left join basic_division t1 on t0.division_id = t1.id " +
                    " where t0.state in ('04inRealization','05realized','01new') " +
                    " GROUP BY t0.division_id";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item = new DMByBoPhan()
                            {
                                bophan = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                sl = reader.GetInt32(2),
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

        public static List<DMBTBD> GetBTBD()
        {
            List<DMBTBD> ret = new List<DMBTBD>();
            DMBTBD item = null;
            string sql = "";

            try
            {
                sql = " select t0.number, t2.number, t2.name, t1.name, " +
                    " t0.updatedate, t3.surname || ' ' || t3.name, t0.state, " +
                    " t5.number, t5.name " +
                    " from cmmsmachineparts_plannedevent t0 " +
                    " left join basic_division t1 on t0.division_id = t1.id " +
                    " left join basic_workstation t2 on t0.workstation_id = t2.id " +
                    " left join basic_staff t3 on t0.owner_id = t3.id " +
                    " left join basic_staff t4 on t0.owner1_id = t4.id " +
                    " left join cmmsmachineparts_tool t5 on t0.tool_id = t5.id " +
                    " where t0.state in ('04inRealization','05realized','01new') " +
                    " order by t1.name ";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item = new DMBTBD()
                            {
                                malenh = reader.IsDBNull(0) ? "" : reader.GetString(0),
                                mamay = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                tenmay = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                bophan = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                ngayTT = reader.IsDBNull(4) ? "" : reader.GetDateTime(4).ToString("dd/MM/yyyy"),
                                nguoiTT = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                tt = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                makhuon = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                tenkhuon = reader.IsDBNull(8) ? "" : reader.GetString(8),
                            };
                            if (item.tt == "01new")
                                item.trangthai = "Khởi tạo";
                            else if (item.tt == "04inRealization")
                                item.trangthai = "Đang thực hiện";
                            else if (item.tt == "05realized")
                                item.trangthai = "Hoàn thành";
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
