using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetApplication.Models;

namespace TimeSheetApplication.Models
{
    public class TimesheetContext : DbContext
    {
        public TimesheetContext(DbContextOptions<TimesheetContext> options) : base(options)
        {
        }

        public DbSet<User> UserItems { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<TimeLog> TimeLog { get; set; }
    }
}
