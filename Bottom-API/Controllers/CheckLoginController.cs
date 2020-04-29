using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Bottom_API._Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Bottom_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckLoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthService _authService;
        public CheckLoginController(IConfiguration config,
        IAuthService authService)
        {
            _config = config;
            _authService = authService;

        }
        [HttpGet]
        public async Task<IActionResult> Get(string username)
        {
            var userFromService = await _authService.GetUser(username);
            if (userFromService == null)
            {
                return NotFound();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromService.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromService.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user = userFromService
            });
        }
    }
}