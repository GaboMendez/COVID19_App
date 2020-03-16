using System;
using System.Collections.Generic;
using System.Text;

namespace COVID19.Models
{
    public class Country
    {
        public string country { get; set; }
        public int cases { get; set; }
        public int todayCases { get; set; }
        public int deaths { get; set; }
        public int todayDeaths { get; set; }
        public int recovered { get; set; }
        public int critical { get; set; }
    }
}
