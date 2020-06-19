using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TimeSheetApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [EnableCors("AllowOrigin")]
        public ActionResult<IEnumerable<string>> Index()
        {
            return new string[] {"this","is","hard","coded"};
        }
    }
}