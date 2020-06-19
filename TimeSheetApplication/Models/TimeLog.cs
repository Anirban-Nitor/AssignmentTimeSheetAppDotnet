using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models
{
    public class TimeLog
    {
        public int TimeLogId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Comment { get; set; }
        public int? UserId { get; set; }
        public int? ProjectId { get; set; }
        public User User { get; set; }
        public Project Project { get; set; }
    }
}
