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
    public class AccountController : ControllerBase
    {
        private readonly TimesheetContext _context;

        public AccountController(TimesheetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Login()
        {
            return new string[] { "Login", "Success" };
        }

        [HttpPost]
        public ActionResult<CustomUser> Login(CustomUser user)
        {
            var isValid = _context.UserItems.Any(u => u.Email.Equals(user.Email) &&
                                                 u.Password.Equals(user.Password));
            var User = _context.UserItems.Where(u => u.Email == user.Email).FirstOrDefault();

            //var projectName = _context.Project.Where(p => p.ProjectId == User.ProjectId).FirstOrDefault().ProjectName;

            if (isValid)
            {
                CustomUser customUser = new CustomUser
                {
                    UserId = User.UserId,
                    UserName = User.UserName,
                    Email = user.Email,
                    //ProjectName = projectName,
                    ProjectId = User.ProjectId,
                };
                return customUser;
            }

            return null;
        }

        [HttpPost]
        [Route("Registration")]
        public ActionResult<string> Registration(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isRegistered = _context.UserItems.Where(u => u.Email == user.Email).Any();
            if (isRegistered)
            {
                return "Already Registered";
            }
            else
            {
                _context.UserItems.Add(user);
                _context.SaveChanges();
            }

            return "Registered";
        }


    }
}