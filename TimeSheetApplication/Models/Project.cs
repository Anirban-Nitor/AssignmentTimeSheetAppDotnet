using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectType { get; set; }
        public string ProjectTechnology { get; set; }
        public string ProjectDetails { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<TimeLog> TimeLogs { get; set; }
    }
}
