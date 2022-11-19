using DigiStore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SampleAPI.Data;
using SampleAPI.Entities;
using SampleAPI.Models;
using SampleAPI.Utils;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.Controllers
{

    [Route("[Controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private AppDbContext db;

        public IConfiguration _configuration;

        public IConfiguration Configuration { get; }

        //public AccountController()
        //{
        //    db = new AppDbContext();
        //}
        public AccountController(IConfiguration configuration, AppDbContext adc)
        {
            db = adc;
            _configuration = configuration;
        }


        [HttpPost("SignUp/")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var checkUser = db.Users.SingleOrDefault(x => x.Username == model.Username);
            if (!ModelState.IsValid)
            {
                return BadRequest("Model Is Not Valid");
            }
            if (checkUser != null)
            {
                return BadRequest("There Is an Account With This Username");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("Confirm password doesn't match!");
            }
            string salt = Guid.NewGuid().ToString();

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = model.Username,
                Password = Crypto.ToSHA512(model.Password + salt),
                Salt = salt
            };

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            return Created("", model);

        }

        [HttpPost("Login/")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Model Is Not Valid");
            }
            var user = db.Users.FirstOrDefault(q => q.Username == model.Username);


            if (user != null)
            {
                var hash = Crypto.ToSHA512(model.Password + user.Salt);
                if (hash != user.Password)
                {
                    return Unauthorized("Username or password is not correct");
                }
            }
            else
            {
                return Unauthorized("Username or password is not correct");
            }



            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //                    .AddJsonFile("appsettings.json")
            //                    .Build();

            //var secretKey = configuration.GetValue<string>("TokenKey");
            //var tokenTimeOut = configuration.GetValue<int>("TokenTimeOut");

            //// Authentication successful generate JWT token
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.UTF8.GetBytes(secretKey);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.Name, user.Username),
            //        new Claim("Id", user.Id.ToString())
            //    }),
            //    Expires = DateTime.UtcNow.AddMinutes(tokenTimeOut),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};

            //var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            //var token = tokenHandler.WriteToken(securityToken);

            //var Amodel = new AuthenticateViewModel
            //{
            //    RefreshToken = "",
            //    Token = token
            //};

            //return Ok(Amodel);



            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("Username", user.Username),
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }


        [HttpGet("LogOut/")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("ActionMethodName");
            return Ok();
        }

    }
}
