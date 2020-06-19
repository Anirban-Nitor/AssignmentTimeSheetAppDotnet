using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<TimeLog> TimeLogs { get; set; }
    }
}
