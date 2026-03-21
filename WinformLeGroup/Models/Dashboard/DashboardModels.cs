using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformLeGroup.Models.Dashboard
{
    public class DMWorktation
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class searchdb
    {
        public DateTime date { get; set; }
        public string calv { get; set; }
        public string daychuyen { get; set; }
        public string masp { get; set; }
        public double soKeHoach { get; set; }
        public double soThucTe {  get; set; }
        public double tyle {  get; set; }
        public string tinhtrang { get; set; }
    }
}
