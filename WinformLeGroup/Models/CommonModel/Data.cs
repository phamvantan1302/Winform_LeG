using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformLeGroup.Models.CommonModel
{
    public class Data_BoPhan
    {
        public int id { get; set; }
        public string number { get; set; }
        public string name { get; set; }
    }

    public class DMStatus
    {
        public int id { get; set; }
        public string workstation { get; set; }
        public double stopvalue { get; set; }
    }

    public class Data_LineSX
    {
        public int id { get; set; }
        public string number { get; set; }
        public string name { get; set; }
        public int division_id { get; set; }
    }

    public class Data_Tram
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Data_LenhSX
    {
        public int id { get; set; }
        public string name { get; set; }
        public int product_id { get; set; }
    }

    public class Data_CongDoan
    {
        public int id { get; set; }
        public string name { get; set; }
        public int technology_id { get; set; }
        public int technologyoperation_id { get; set; }
        public int id_daura { get; set; }
        public string name_daura { get; set; }
        public string dvt { get; set; }
        public string typeofmaterial { get; set; }
        public int khsx_chitiet_id { get; set; }
    }

    public class Data_VatTuCongDoan
    {
        public int id { get; set; }
        public string name { get; set; }
        public int so_luong { get; set; }
        public string dvt { get; set; }
        public string typeofmaterial { get; set; }
    }

    public class Data_CongNhan
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Data_Ca
    {
        public int id { get; set; }
        public string name { get; set; }
        public string mondayhours { get; set; }
        public string tuesdayhours { get; set; }
        public string wensdayhours { get; set; }
        public string thursdayhours { get; set; }
        public string fridayhours { get; set; }
        public string saturdayhours { get; set; }
        public string sundayhours { get; set; }
    }
}
