using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformLeGroup.Models.Dashboard
{
    public class NGData
    {
        public string soLSX { get; set; } = "";
        public int CongDoanID { get; set; } = -1;
        public string maCongDoan { get; set; } = "";
        public string tenCongDoan { get; set; } = "";
        public string maHangHoa { get; set; } = "";
        public string tenHangHoa { get; set; } = "";
        public double soLoi { get; set; } = 0;
    }

    public class TTThanhPham
    {
        public string SoPO { get; set; }
        public string MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public double SoLuongKH { get; set; } = 0;
        public double SoLuongTT { get; set; } = 0;
        public double SoLuongNG { get; set; } = 0;
        public string TinhTrang
        {
            get
            {
                if (SoLuongTT >= SoLuongKH)
                    return "Hoàn thành";
                else
                    return "Đang thực hiện";
            }
        }
    }

    public class DashboardCongDoan
    {
        public string maCongDoan { get; set; } = "";
        public string tenCongDoan { get; set; } = "";
        public int soLoiNG { get; set; } = 0;
        public double sanLuongKH { get; set; } = 0;
        public double sanLuongSX { get; set; } = 0;
        public double tyLe
        {
            get
            {
                if (sanLuongKH == 0)
                    return 0;
                else
                    return sanLuongSX * 100 / sanLuongKH;
            }
        }
    }

    public class KHNgayModel
    {
        public int STT { get; set; }
        public DateTime Ngay { get; set; }
        public string SoDonHang { get; set; }
        public string SoLSX { get; set; }
        public double SoLuongKH { get; set; }
        public double SoThucTe { get; set; }
        public int WorkstationID { get; set; }
        public int operation_id { get; set; }
        public int CongDoanCNID { get; set; }
        public string maCongDoan { get; set; }
        public string tenCongDoan { get; set; }
        public int MachineWorkTime { get; set; }
        public int dinhmucthoigian_tj { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public DateTime? TimeRangeFrom { get; set; }
        public DateTime? TimeRangeTo { get; set; }
        public string MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public string MaThanhPham { get; set; }
        public string TenThanhPham { get; set; }
        public string Unit { get; set; }
        public string TrangThai { get; set; }
        public int order_id { get; set; }
        public DateTime? hanSX { get; set; }

        public double timeProceduredAProduct
        {
            get
            {
                double ret = 0;

                if (TimeRangeFrom != null && TimeRangeTo != null)
                    ret = ((DateTime)TimeRangeTo - (DateTime)TimeRangeFrom).TotalSeconds / SoThucTe;

                return ret;
            }
        }

        public int SoLuongKHNgay
        {
            get
            {
                double numOfDay, sumPlan;
                numOfDay = (double)MachineWorkTime / (8 * 3600);
                if (numOfDay > 1)
                    sumPlan = SoLuongKH / numOfDay;
                else
                    sumPlan = SoLuongKH;

                return Convert.ToInt32(sumPlan);
            }
        }
    }
}
