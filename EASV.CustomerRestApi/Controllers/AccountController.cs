using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly UserManager<AuthUser> _userManager;
        private readonly TokenManager _tokenManager;
        
        public AccountController(
            UserManager<AuthUser> userManager,
            SignInManager<AuthUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenManager = new TokenManager(
                configuration["JwtKey"],
                double.Parse(configuration["JwtExpireDays"]),
                configuration["JwtIssuer"]);
        }
        
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (!result.Succeeded) return StatusCode(500, "Login Failed");
                
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                return await _tokenManager
                    .GenerateJwtToken(model.Email, appUser);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
       
        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDto model)
        {   
            try
            {
                var user = new AuthUser
                {
                    UserName = model.Email, 
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded) return StatusCode(500, "Failed to create User");
                
                await _signInManager.SignInAsync(user, false);
                return await _tokenManager
                    .GenerateJwtToken(model.Email, user);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}