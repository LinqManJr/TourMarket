using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TourMarket.Dto;
using TourMarket.Helpers;
using TourMarket.Services;

namespace TourMarket.Controllers
{
    [Route("api/[controller]")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class AccountController : Controller
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Constructor of AccountController
        /// </summary>
        /// <param name="userService">IUserService for manage users</param>
        /// <param name="appSettings">IOptions AppSettings</param>
        public AccountController(IUserService userService, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Login by manager 
        /// </summary>
        /// <param name="managerDto">ManagerDto</param>
        /// <returns>ActionResult with data as : id, login, name, token</returns>
        /// <response code="200">Returns Ok</response>
        /// <response code="400">If the item is null</response>
        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login([FromBody]ManagerDto managerDto)
        {
            var manager = _userService.Authenticate(managerDto.Login, managerDto.Password);

            if (manager == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {                    
                    new Claim(ClaimTypes.NameIdentifier, manager.Id.ToString())                    
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
                        
            return Ok(new
            {
                manager.Id,
                manager.Login,
                manager.Name,
                Token = tokenString
            });
        }

        /// <summary>
        /// Register manager
        /// </summary>
        /// <param name="managerDto">ManagerDto</param>
        /// <returns>StatusCode: Ok or BadRequest</returns>
        /// <response code="200">Returns Ok</response>
        /// <response code="400">If throw exception on CreateManager</response>
        [AllowAnonymous]
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody]ManagerDto managerDto)
        {            
            var user = managerDto.ToManager();

            try
            {                
                _userService.Create(user, managerDto.Password);
                return Ok();
            }
            catch (Exception ex)
            {                
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}