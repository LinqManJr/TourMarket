using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourMarket.Models;

namespace TourMarket.Controllers
{
    [Route("api/[controller]")]
    
    public class AccountController : Controller
    {
        [HttpPost("[action]")]
        public IActionResult Login([FromBody]Manager manager)
        {
            //TODO: managerDTO
            throw new NotImplementedException();
        }

        [HttpPost("[action]")]
        public IActionResult Register([FromBody]Manager manager)
        {
            //TODO:managerDTO
            throw new NotImplementedException();
        }
    }
}