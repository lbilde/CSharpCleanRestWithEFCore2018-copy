using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CustomerApp.Core.ApplicationService;
using CustomerApp.Core.Entity;
using CustomerApp.Infrastructure.Data;
using CustomerApp.Infrastructure.Data.Managers;
using EASV.CustomerRestApi.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EASV.CustomerRestApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly TokenManager _tokenManager;
        
        public AccountController(
            IUserService userService,
            IConfiguration configuration)
        {
            _userService = userService;
            _tokenManager = new TokenManager(
                configuration["JwtKey"],
                double.Parse(configuration["JwtExpireDays"]),
                configuration["JwtIssuer"]);
        }
        
        [HttpPost]
        public ActionResult<string> Login([FromBody] LoginDto model)
        {
            try
            {
                var user = new User
                {
                    Email = model.Email
                };
                var userFound = _userService.SignIn(user, model.Password);

                return _tokenManager
                    .GenerateJwtToken(model.Email, userFound);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
       
        [HttpPost]
        public ActionResult<string> Register([FromBody] RegisterDto model)
        {   
            try
            {
                var user = new User
                {
                    UserName = model.Email, 
                    Email = model.Email
                };
                var userFound = _userService.CreateUser(user, model.Password);
                
                return Ok(_tokenManager
                    .GenerateJwtToken(model.Email, userFound));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}