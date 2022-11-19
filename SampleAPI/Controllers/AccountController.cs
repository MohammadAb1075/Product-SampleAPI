using DigiStore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Data;
using SampleAPI.Entities;
using SampleAPI.Models;
using SampleAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SampleAPI.Controllers
{

    [Route("[Controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private AppDbContext db;

        public AccountController()
        {
            db = new AppDbContext();
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

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username)
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);




            var authProperties = new AuthenticationProperties
            {
                //AlowRefresh = <bool> => Refreshing the authentication session should be allowed.
                //RedirectUri = <string>
                //IssuedUTC = <DateTimeOffset >
                //ExpiresUtc = <DateTimeOffset.UTCNow.AddMinutes(10)>
            };


            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return Ok();
        }


        [HttpGet("LogOut/")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }



    }
}
