using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using task_chat.Models;
using task_chat.Services;

namespace task_chat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginUtenteController : Controller
    {
        private readonly UtenteService _service;

        public LoginUtenteController(UtenteService service)
        {
            _service = service;
        }
        [HttpPost("login")]
        public IActionResult Login(UserLogin model)
        {
            
            if (_service.LoginUtente(model))
            {
                var claims = new List<Claim>
                {

                    new Claim(JwtRegisteredClaimNames.Sub, model.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserType", "USER"),
                    new Claim("Username", model.Username)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_key_your_super_secret_key"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "YourIssuer",
                    audience: "YourAudience",
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return Unauthorized();

        }
    }
}