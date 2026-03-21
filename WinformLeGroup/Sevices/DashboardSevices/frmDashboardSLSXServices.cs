using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformLeGroup.Models.CommonModel;
using WinformLeGroup.Models.Dashboard;
using WinformLeGroup.Models.GSAppLibrary;

namespace HKDashboard.Services
{
    public class frmDashboardSLSXServices
    {
        public static List<NGData> getAllNG(DateTime ngay, string division_id)
        {
            List<NGData> ret = new List<NGData>();
            Hashtable data = new Hashtable();
            string sql = "";
            int congdoanid;
            double soLuong = 0;
            NGData item;

            try
            {
                string strWhere = "";
                if (division_id != "0")
                    strWhere += "AND T0.productionline_id  = '" + division_id + "' ";
                sql = "select t1.mahang, t1.slkiem, t3.id operation_id, t3.number operationcode, " +
                    " t3.name operationname, t0.number solsx, t2.number mahanghoa, t3.name tenhanghoa " +
                    " from orders_order t0 " +
                    " inner join qualitycontrol_cus_qachecksheet t1 on t0.number = t1. lenhsx " +
                    " inner join basic_product t2 on t0.product_id = t2.id " +
                    " inner join technologies_operation t3 on t1.congdoanid = t3.id " +
                    //" inner join technologies_technologyoperationcomponent t4 on t1.technology_id = t4.technology_id " +
                    " where solo = '" + ngay.Year.ToString().Substring(2, 2) + ngay.Month.ToString("D2") + ngay.Day.ToString("D2") + "' " +
                    " and t2.globaltypeofmaterial = '03finalProduct' "+ strWhere + " " +
                    
                    " order by t3.name ";
                    //" order by t4.nodenumber ";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            congdoanid = reader.GetInt32(2);
                            if (!data.Contains(congdoanid.ToString()))
                            {
                                item = new NGData()
                                {
                                    CongDoanID = congdoanid,
                                    maCongDoan = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                    tenCongDoan = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                    soLSX = reader.GetString(5),
                                    maHangHoa = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                    tenHangHoa = reader.IsDBNull(7) ? "" : reader.GetString(7)
                                };
                                data.Add(congdoanid.ToString(), item);
                                ret.Add(item);
                            }
                            else
                                item = (NGData)data[congdoanid.ToString()];

                            soLuong = reader.IsDBNull(1) ? 0 : reader.GetDouble(1);
                            item.soLoi += soLuong;
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

        public static List<Data_BoPhan> getAllDivision()
        {
            List<Data_BoPhan> ret = new List<Data_BoPhan>();
            Data_BoPhan item = null;
            string sql = "";

            try
            {
                sql = "select id, number, name from basic_division GROUP BY id, number, name order by name";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item = new Data_BoPhan()
                            {
                                id = reader.GetInt32(0),
                                number = reader.GetString(1),
                                name = reader.GetString(2)
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

        public static List<Data_LineSX> getAllProductionLines(string division_id = "")
        {
            List<Data_LineSX> ret = new List<Data_LineSX>();
            Data_LineSX item = null;
            string sql = "";

            try
            {
                sql = "select id, number, name, t1.division_id from productionlines_productionline  t0 " +
                    " inner join jointable_division_productionline t1 on t0.id = t1.productionline_id ";
                if (!string.IsNullOrEmpty(division_id))
                    sql += " where t1.division_id = " + division_id + " ";
                sql += "order by name";
                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item = new Data_LineSX()
                            {
                                id = reader.GetInt32(0),
                                number = reader.GetString(1),
                                name = reader.GetString(2),
                                division_id = reader.IsDBNull(3) ? -1 : reader.GetInt32(3)
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
        public static List<KHNgayModel> getKHNgayData(DateTime ngayKH, int chuyen_id)
        {
            List<KHNgayModel> ret = new List<KHNgayModel>();
            KHNgayModel item = null;
            string sql = "", soLSX = "", key = "", nodenumber = "", comment = "", opername = "";
            int stt = 0, congDoanCNID = 0;
            double soKH, soTT;
            Hashtable data = new Hashtable();
            DateTime? timerangefrom = null, timerangeto = null, starttime = null, endtime = null;

            try
            {
                string strWhere = "";
                if (chuyen_id > 0)
                    strWhere += " AND a.productionline_id  = '" + chuyen_id.ToString() + "' ";
                sql = "select i.number SONumber, a.number PONumber, d.quantity SLKeHoach, e.givenquantity SLThucTe, " +
                    " g.operation_id, h.name congdoan, a.id order_id, d.schedule_id, " +
                    " d.machineworktime, d.product_id, d.workstation_id, g.id technologyoperationcomponent_id, " +
                    " e.timerangefrom, e.timerangeto, g.nodenumber, g.comment, d.starttime, d.endtime, h.name, " +
                    " h.number maCongDoan, l.number mahanghoa, l.name tenhanghoa, a.mathanhpham, a.tenthanhpham, " +
                    " a.globaltypeofmaterial, a.dateto " +
                    " from (" +
                        " select t0.id, t0.number, t0.masterorder_id, t0.state, t1.number mathanhpham, " +
                        " t1.name tenthanhpham, t1.globaltypeofmaterial, t0.dateto, " +
                        " T0.productionline_id" +
                        " from orders_order t0 " +
                        " inner join basic_product t1 on t0.product_id = t1.id " +
                        //" where t0.state = '03inProgress' " +
                        " order by t0.id " +
                        " ) a " +
                    " inner join jointable_order_schedule b on a.id = b.order_id " +
                    " inner join orders_schedule c on b.schedule_id = c.id " +
                    " inner join orders_scheduleposition d on b.schedule_id = d.schedule_id " +
                    " left join (" +
                        " select t0.technologyoperationcomponent_id, t0.order_id, t1.product_id, t1.givenquantity, " +
                        " t0.timerangefrom, t0.timerangeto " +
                        " from productioncounting_productiontracking t0 " +
                        " inner join productioncounting_trackingoperationproductoutcomponent t1 on t0.id = t1.productiontracking_id " +
                        " where t0.state = '02accepted' " +
                        " and t0.timerangefrom >= '" + ngayKH.ToString("yyyy-MM-dd") + " 00:00:00' " +
                        " and t0.timerangeto <= '" + ngayKH.ToString("yyyy-MM-dd") + " 23:59:59'" +
                        " order by t0.order_id, t0.technologyoperationcomponent_id, t1.product_id " +
                    ") e on a.id = e.order_id " +
                    " and d.technologyoperationcomponent_id = e.technologyoperationcomponent_id " +
                    " and d.product_id = e.product_id " +
                    " inner join technologies_technologyoperationcomponent g on d.technologyoperationcomponent_id = g.id" +
                    " inner join technologies_operation h on g.operation_id = h.id " +
                    " left join masterorders_masterorder i on a.masterorder_id = i.id " +
                    " inner join orders_operationaltask k on d.id = k.scheduleposition_id " +
                    " inner join basic_product l on d.product_id = l.id " +
                    " where " +
                    //" a.state = '03inProgress' and " +
                    " a.globaltypeofmaterial ='03finalProduct' and " +
                    " k.startdate <= '" + ngayKH.ToString("yyyy-MM-dd") + " 23:59:59' " +
                    " and k.finishdate >= '" + ngayKH.ToString("yyyy-MM-dd") + " 00:00:00' "+ strWhere + "" +
                    " order by i.number, a.number, g.nodenumber";

                using (var command = new NpgsqlCommand(sql, Globals.NpgsqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            soKH = reader.IsDBNull(2) ? 0 : reader.GetDouble(2);
                            soTT = reader.IsDBNull(3) ? 0 : reader.GetDouble(3);
                            soLSX = reader.GetString(1);
                            congDoanCNID = reader.GetInt32(11);
                            timerangefrom = reader.IsDBNull(12) ? null : (DateTime?)reader.GetDateTime(12);
                            timerangeto = reader.IsDBNull(13) ? null : (DateTime?)reader.GetDateTime(13);
                            nodenumber = reader.IsDBNull(14) ? "" : reader.GetString(14);
                            comment = reader.IsDBNull(15) ? "" : reader.GetString(15);
                            opername = reader.IsDBNull(18) ? "" : reader.GetString(18);
                            starttime = reader.IsDBNull(16) ? null : (DateTime?)reader.GetDateTime(16);
                            endtime = reader.IsDBNull(17) ? null : (DateTime?)reader.GetDateTime(17);
                            key = soLSX + "_" + congDoanCNID.ToString();
                            if (data.Contains(key))
                            {
                                item = (KHNgayModel)data[key];
                                item.SoThucTe += soTT;
                                //gan lai gio bat dau
                                if (item.TimeRangeFrom == null)
                                    item.TimeRangeFrom = timerangefrom;
                                else if (item.TimeRangeFrom != null && timerangefrom != null &&
                                    DateTime.Compare((DateTime)item.TimeRangeFrom, (DateTime)timerangefrom) > 0)
                                    item.TimeRangeFrom = timerangefrom;
                                //gan lai gio ket thuc
                                if (item.TimeRangeTo == null)
                                    item.TimeRangeTo = timerangeto;
                                else if (item.TimeRangeTo != null && timerangeto != null &&
                                    DateTime.Compare((DateTime)item.TimeRangeTo, (DateTime)timerangeto) < 0)
                                    item.TimeRangeTo = timerangeto;
                                item.TrangThai = item.SoLuongKH <= item.SoThucTe ? "Hoàn thành" : "Đang thực hiện";
                            }
                            else
                            {
                                stt += 1;
                                item = new KHNgayModel()
                                {
                                    STT = stt,
                                    Ngay = DateTime.Now,
                                    SoDonHang = reader.IsDBNull(0) ? "" : reader.GetString(0),
                                    SoLSX = soLSX,
                                    SoLuongKH = soKH,
                                    SoThucTe = soTT,
                                    MachineWorkTime = reader.IsDBNull(8) ? 0 : reader.GetInt32(8),
                                    CongDoanCNID = congDoanCNID,
                                    operation_id = reader.GetInt32(4),
                                    tenCongDoan = string.IsNullOrEmpty(comment) ? nodenumber + " - " + opername : nodenumber + " - " + comment,//reader.GetString(5),
                                    maCongDoan = reader.GetString(19),
                                    MaHangHoa = reader.GetString(20),
                                    TenHangHoa = reader.GetString(21),
                                    MaThanhPham = reader.GetString(22),
                                    TenThanhPham = reader.GetString(23),
                                    TimeRangeFrom = timerangefrom,
                                    TimeRangeTo = timerangeto,
                                    TimeStart = starttime,
                                    TimeEnd = endtime,
                                    hanSX = reader.IsDBNull(25) ? null : (DateTime?)reader.GetDateTime(25),
                                    TrangThai = soKH == soTT ? "Hoàn thành" : "Đang thực hiện"
                                };
                                data.Add(key, item);
                            }
                        }
                        ret = data.Values.OfType<KHNgayModel>().ToList();
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
