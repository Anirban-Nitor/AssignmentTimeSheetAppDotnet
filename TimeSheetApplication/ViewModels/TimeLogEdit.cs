using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.ViewModels
{
    public class TimeLogEdit
    {
        public int logId { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string comment { get; set; }
    }
}
