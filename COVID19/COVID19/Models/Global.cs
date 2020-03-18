using System;
using System.Collections.Generic;
using System.Text;

namespace COVID19.Models
{
    public class Global
    {
        public int? cases { get; set; }
        public int? deaths { get; set; }
        public int? recovered { get; set; }

        public bool CatchError { get; set; }

        public Global()
        {
            cases = 0;
            deaths = 0;
            recovered = 0;
        }
    }
}
