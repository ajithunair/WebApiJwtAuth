using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiJwtAuth.Models;

namespace WebApiJwtAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login user)
        {
            if (user == null)
                return BadRequest("Invalid user request");
            if (user.UserName == "Ajith" && user.Password == "1234")
            {
                var secret = ConfigurationManager.Configuration["Jwt:Secret"];
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
                var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var securityToken = new JwtSecurityToken
                (
                    issuer : ConfigurationManager.Configuration["Jwt:Issuer"],
                    audience: ConfigurationManager.Configuration["Jwt:Audience"],
                    signingCredentials: signingCredentials,
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5)
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
                return Ok(new JwtTokenResponse { Token = tokenString });
            }

            return Unauthorized();
        }
    }
}
