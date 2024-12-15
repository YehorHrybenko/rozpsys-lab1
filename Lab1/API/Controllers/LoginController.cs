using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpGet(Name = "Login")]
        public string Get(IConfiguration config, string login, string password)
        {
            // TODO: Implement users
            if (login != config["LoginStub:Login"] || password != config["LoginStub:Password"]) return "Wrong login or password";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDeskriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(config.GetValue<int>("JwtSettings:Expires")),
                SigningCredentials = credentials,
                Issuer = config["JwtSettings:Issuer"],
                Audience = config["JwtSettings:Audience"]
            };

            var handler = new JsonWebTokenHandler();
            string token = handler.CreateToken(tokenDeskriptor);

            return token;
        }
    }
}
