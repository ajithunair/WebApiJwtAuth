using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiJwtAuth.Models;

namespace WebApiJwtAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login user)
        {
            if (user == null)
                return BadRequest("Invalid user request");
            if (user.UserName == "Ajith" && user.Password == "1234")
            {
                var secret = Configuration["Jwt:Secret"];
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
                var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };
                var securityToken = new JwtSecurityToken
                (
                    issuer : Configuration["Jwt:Issuer"],
                    audience: Configuration["Jwt:Audience"],
                    signingCredentials: signingCredentials,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5)
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
                return Ok(new JwtTokenResponse { Token = tokenString });
            }

            return Unauthorized();
        }
    }
}
