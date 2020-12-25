using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HakkuPaysaAPI.DTOs;
using HakkuPaysaAPI.Entities;
using HakkuPaysaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HakkuPaysaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly HakkuPayasaDbContext _dbContext;

        public AccountsController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            HakkuPayasaDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo userInfo)
        {
            var user = new IdentityUser
            {
                UserName = userInfo.Email,
                Email = userInfo.Email
            };

            var result = await _userManager.CreateAsync(user, userInfo.Password);
            if (result.Succeeded)
            {
                var userProfile = new UserProfile();
                userProfile.Username = userInfo.Email;
                userProfile.ProfilePic = "https://u.cubeupload.com/adsuri/7NQDHz.jpeg";

                try
                {
                    _dbContext.Users.Add(userProfile);
                    await _dbContext.SaveChangesAsync();
                }
                catch
                {
                    return BadRequest("Failed to create user profile");
                }
                return BuildToken(userInfo);
            }
            else 
            {
                return BadRequest("Username or password is invalid");
            }

        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
        {
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return BuildToken(userInfo);
            }
            else 
            {
                return BadRequest("Invalid login attempt");
            }
        }

        private UserToken BuildToken(UserInfo userInfo)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userInfo.Email),
                new Claim(ClaimTypes.Email, userInfo.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            JwtSecurityToken token = new JwtSecurityToken
                (
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: expiration,
                    signingCredentials: credentials
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}