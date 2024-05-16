using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebServer.InversionOfControl;

namespace WebServer.Controllers
{
    /// <summary>
    /// Manages the web API calls
    /// </summary>
    public class ApiController : Controller
    {
        [Route("api/login")]
        public IActionResult LogIn()
        {
            // TODO: Get user's login information and check it is correct

            var username = "Megafont";
            var email = "megafont@gmail.com";

            // Set our token's claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimsIdentity.DefaultNameClaimType, username),

                new Claim("my key", "my value"), // This one is just an example to show that these are just key/value pairs
            };


            // Create the credentials used to generate the token
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IoC.Configuration["Jwt:SecretKey"])),
                SecurityAlgorithms.HmacSha256
            );


            // Generate the JWT Token
            var token = new JwtSecurityToken(
                issuer: IoC.Configuration["Jwt:Issuer"],
                audience: IoC.Configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMonths(3),
                signingCredentials: credentials
            );

            // Return token to user
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("api/private")]
        public IActionResult Private()
        {
            var user = HttpContext.User;

            if (user != null)
            {
                return Ok(new
                {
                    privateData = $"Some secret for {user.Identity.Name}"
                });
            }

            return Content("Login failed!", "text/html");
        }
    }
}
