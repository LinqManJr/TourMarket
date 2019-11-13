﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TourMarket.Dto;
using TourMarket.Helpers;
using TourMarket.Services;

namespace TourMarket.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly AppSettings _appSettings;

        public AccountController(IUserService userService,AppSettings appSettings)
        {
            this.userService = userService;
            this._appSettings = appSettings;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]ManagerDto managerDto)
        {
            var manager = userService.Authenticate(managerDto.Login, managerDto.Password);

            if (manager == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, manager.Login.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
                        
            return Ok(new
            {
                Id = manager.Id,
                Login = manager.Login,
                Name = manager.Name,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]ManagerDto managerDto)
        {            
            var user = managerDto.ToManager();

            try
            {                
                userService.Create(user, managerDto.Password);
                return Ok();
            }
            catch (Exception ex)
            {                
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}