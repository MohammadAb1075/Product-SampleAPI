//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using SampleAPI.Models;
//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace SampleAPI.Controllers
//{
    //[Route("[controller]")]
    //[ApiController]
    //public class AuthenticationController : ControllerBase
    //{
//        public IConfiguration Configuration { get; }

//        public AuthenticationController(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }
//        public async Task<IActionResult> Authenticate(string username, string password)
//        {
//            // Read token time and key from appsetting.json
//            var secretKey = Configuration.GetValue<string>("TokenKey");
//            var tokenTimeOut = Configuration.GetValue<int>("TokenTimeOut");

//            // Authentication successful generate JWT token
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.UTF8.GetBytes(secretKey);
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(new Claim[]
//                {
//                    new Claim(ClaimTypes.Name, "Mohammad Abdollahzadeh"),
//                    new Claim("email", "mabdollah1375@gmail.com")
//                }),
//                Expires = DateTime.UtcNow.AddMinutes(tokenTimeOut),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
//            };

//            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
//            var token = tokenHandler.WriteToken(securityToken);

//            var model = new AuthenticateViewModel
//            {
//                RefreshToken = "",
//                Token = token
//            };

//            return Ok(model);

//        }

    //}
//}
