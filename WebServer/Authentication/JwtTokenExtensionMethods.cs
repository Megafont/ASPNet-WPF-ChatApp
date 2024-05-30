using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;

using Dna;

using ASPNet_WPF_ChatApp.WebServer.Data;
using ASPNet_WPF_ChatApp.WebServer.DependencyInjection;


namespace ASPNet_WPF_ChatApp.WebServer.Authentication
{
    /// <summary>
    /// Extension methods for working with JWT (JSON Web Token) bearer tokens
    /// </summary>
    public static class JwtTokenExtensionMethods
    {
        /// <summary>
        /// Generates a JWT (JSON Web Token) bearer token containing the user's user name
        /// </summary>
        /// <param name="user">The user's details</param>
        /// <returns></returns>
        public static string GenerateJwtToken(this ApplicationUser user)
        {
            // Set our token's claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),

                //new Claim("my key", "my value"), // This one is just an example to show that these are just key/value pairs
            };


            // Create the credentials used to generate the token
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(FrameworkDI.Configuration["Jwt:SecretKey"])),
                SecurityAlgorithms.HmacSha256
            );


            // Generate the JWT Token
            var token = new JwtSecurityToken(
                issuer: FrameworkDI.Configuration["Jwt:Issuer"],
                audience: FrameworkDI.Configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: credentials,

                // Expire if not used for 3 months
                expires: DateTime.Now.AddMonths(3)
            );


            // Return the generated token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
