using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformLeGroup.Models.Dashboard
{
    public class DMByTT
    {
        public string tt {  get; set; }
        public string trangthai {  get; set; }
        public int sl {  get; set; } 
    }

    public class DMByBoPhan
    {
        public string bophan { get; set; }
        public int sl { get; set; }
    }

    public class DMBTBD
    {
        public string malenh { get; set; }
        public string mamay { get; set; }
        public string tenmay { get; set; }
        public string makhuon { get; set; }
        public string tenkhuon { get; set; }
        public string bophan { get; set; }
        public string ngayTT { get; set; }
        public string nguoiTT { get; set; }
        public string tt { get; set; }
        public string trangthai { get; set; }
    }
}
