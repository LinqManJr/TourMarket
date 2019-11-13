using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourMarket.Models;
using TourMarket.Services;

namespace TourMarket.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("[action]")]
        public IActionResult Login([FromBody]Manager manager)
        {
            //TODO: managerDTO
            throw new NotImplementedException();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {            
            var user = _mapper.Map<User>(userDto);

            try
            {
                
                userService.Create(user, userDto.Password);
                return Ok();
            }
            catch (Exception ex)
            {                
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}