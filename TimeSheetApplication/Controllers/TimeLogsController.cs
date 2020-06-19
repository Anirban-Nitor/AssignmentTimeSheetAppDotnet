using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheetApplication.Models;
using TimeSheetApplication.ViewModels;

namespace TimeSheetApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeLogsController : ControllerBase
    {
        private readonly TimesheetContext _context;

        public TimeLogsController(TimesheetContext context)
        {
            _context = context;
        }

        // GET: api/TimeLogs
        [HttpGet]
        public ActionResult<IEnumerable<CustomTimeLog>> GetTimeLog()
        {
            var timeLog = _context.TimeLog.ToList();

            var projectName = string.Empty;
            var Useremail = string.Empty;

            List<CustomTimeLog> customTimeLogs = new List<CustomTimeLog>();

            foreach (var log in timeLog)
            {
                projectName = _context.Project.Where(p => p.ProjectId == log.ProjectId).FirstOrDefault().ProjectName;
                Useremail = _context.UserItems.Where(p => p.UserId == log.UserId).FirstOrDefault().Email;

                CustomTimeLog customTimeLog = new CustomTimeLog
                {
                   TimeLogId = log.TimeLogId,
                   ProjectName = projectName,
                   Email = Useremail,
                   Date = log.Date,
                   Time = log.Time,
                   Comment = log.Comment
                };

                customTimeLogs.Add(customTimeLog);
            }

            return customTimeLogs;
        }

        // GET: api/TimeLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TimeLog>> GetTimeLog(int id)
        {
            var timeLog = await _context.TimeLog.FindAsync(id);

            if (timeLog == null)
            {
                return NotFound();
            }

            return timeLog;
        }

        // PUT: api/TimeLogs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimeLog(int id, TimeLog timeLog)
        {
            if (id != timeLog.TimeLogId)
            {
                return BadRequest();
            }

            _context.Entry(timeLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeLogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TimeLogs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<CustomTimeLog> PostTimeLog(CustomTimeLog postTimeLog)
        {
            var projectId = _context.Project.Where(p => p.ProjectName.Equals(postTimeLog.ProjectName)).FirstOrDefault().ProjectId;
            var UserId = _context.UserItems.Where(p => p.Email.Equals(postTimeLog.Email)).FirstOrDefault().UserId;

            TimeLog timeLog = new TimeLog
            {
                UserId = UserId,
                ProjectId = projectId,
                Date = postTimeLog.Date,
                Time = postTimeLog.Time,
                Comment = postTimeLog.Comment
            };

            _context.TimeLog.Add(timeLog);
            _context.SaveChangesAsync();

            return postTimeLog;
            //return CreatedAtAction("GetTimeLog", new { id = timeLog.TimeLogId }, timeLog);
        }

        // DELETE: api/TimeLogs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TimeLog>> DeleteTimeLog(int id)
        {
            var timeLog = await _context.TimeLog.FindAsync(id);
            if (timeLog == null)
            {
                return NotFound();
            }

            _context.TimeLog.Remove(timeLog);
            await _context.SaveChangesAsync();

            return timeLog;
        }

        private bool TimeLogExists(int id)
        {
            return _context.TimeLog.Any(e => e.TimeLogId == id);
        }
    }
}
