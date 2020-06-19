using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.ViewModels
{
    public class CustomTimeLog
    {
        public int TimeLogId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Comment { get; set; }
        public int? UserId { get; set; }
        public int? ProjectId { get; set; }
        public string Email { get; set; }
        public string ProjectName { get; set; }
        public string UserName { get; set; }
    }
}
