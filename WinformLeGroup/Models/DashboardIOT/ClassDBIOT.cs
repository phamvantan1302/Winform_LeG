using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformLeGroup.Models.DashboardIOT
{
    public class DMWeekProduction
    {
        public int id {  get; set; }
        public DateTime day { get; set; }
        public double slsxKH {  get; set; }
        public double slsxTT {  get; set; }
    }

    public class DMWorkstation
    {
        public int id { get; set; }
        public string number { get; set; }
        public string name { get; set; }
        public string note
        {
            get { return number + " - " + name; }
        }
    }

    public class DMLine
    {
        public int id { get; set; }
        public string number { get; set; }
        public string name { get; set; }
        public string division { get; set; }
        //public string note
        //{
        //    get { return number + " - " + name; }
        //}
    }

    public class DMMaySX
    {
        public int id { get; set; }
        public DateTime day { get; set; }
        public double slsx { get; set; }
        public double slsx_error { get; set; }
        public double slkh { get; set; }
        public double runtime { get; set; }
        public double stoptime { get; set; }
    }

    public class DMLineSX
    {
        public string line { get; set; }
        public double slsx { get; set; }
    }

    public class DMTTMay
    {
        public string division { get; set; }
        public string productionline { get; set; }
        public string production { get; set; }
    }

    public class DMSerialWorkstation
    {
        public int id { get; set; }
        public string serial_iot { get; set; }
        public string workstation_number { get; set; }
    }
}
