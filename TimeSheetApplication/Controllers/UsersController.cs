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
    public class UsersController : ControllerBase
    {
        private readonly TimesheetContext _context;

        public UsersController(TimesheetContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUserItems()
        {
            return await _context.UserItems.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.UserItems.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CustomUser>> PostUser(CustomUser user)
        {
            User user1 = new User
            {
                Email = user.Email,
                Password = user.Password,
                UserName = user.UserName,
                ProjectId = user.ProjectId
            };
            _context.UserItems.Add(user1);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.UserItems.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.UserItems.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        //[Route("api/[controller]/[action]")]
        [Route("AssignProject")]
        [HttpPost]
        public ActionResult<AssignProjectModel> AssignProject(AssignProjectModel user)
        {
            var projectId = _context.Project.Where(p => p.ProjectName.Equals(user.ProjectName)).FirstOrDefault().ProjectId;

            _context.UserItems.Where(u => u.UserId.Equals(user.userId)).FirstOrDefault().ProjectId = projectId;
            _context.SaveChanges();

            return user;
        }


        private bool UserExists(int id)
        {
            return _context.UserItems.Any(e => e.UserId == id);
        }
    }
}
