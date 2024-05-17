using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using WebServer.InversionOfControl;

using ASPNet_WPF_ChatApp.Core.ApiModels;
using Microsoft.AspNetCore.Identity;
using WebServer.Data;

namespace WebServer.Controllers
{
    /// <summary>
    /// Manages the web API calls
    /// </summary>
    public class ApiController : Controller

    {
        #region Protected Members

        /// <summary>
        /// The scoped <see cref="ApplicationDbContext"/>
        /// </summary>
        protected ApplicationDbContext _Context;

        /// <summary>
        /// The manager for handling user creation, deletion, searching, roles, etc.
        /// </summary>
        protected UserManager<ApplicationUser> _UserManager;

        /// <summary>
        /// The manager for handling signing in and out for our users
        /// </summary>
        protected SignInManager<ApplicationUser> _SignInManager;

        #endregion

        #region Private Members

        /// <summary>
        /// The built-in logger
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="logger">The built-in logger</param>
        /// <param name="context">The injected context</param>
        /// <param name="userManager">The Identity sign in manager</param>
        /// <param name="signInManager">The Identity user manager</param>
        public ApiController(
            ILogger<HomeController> logger,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _Context = context;
            _UserManager = userManager;
            _SignInManager = signInManager;
        } 

        #endregion

        /// <summary>
        /// Logs in a user using token-based authentication
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Route("api/login")]
        public async Task<ApiResponseModel<LoginResultApiModel>> LogInAsync([FromBody] LoginCredentialsApiModel loginCredentials)
        {
            // TODO: Localize all strings

            // The message when we fail to login
            string invalidErrorMessage = "Invalid username or password!";

            // The error response for a failed login
            var errorResponse = new ApiResponseModel<LoginResultApiModel>
            {
                // Set error message
                ErrorMessage = invalidErrorMessage
            };


            // Make sure we have a username
            if (loginCredentials?.UsernameOrEmail == null || string.IsNullOrWhiteSpace(loginCredentials.UsernameOrEmail))
            {
                // Return error message to user
                return errorResponse;
            }

            // Validate if the user credentials are correct

            // Is it an email?
            bool isEmail = loginCredentials.UsernameOrEmail.Contains("@");
            
            // Get the user details
            var user = isEmail 
                // Find by email
                ? await _UserManager.FindByEmailAsync(loginCredentials.UsernameOrEmail) 
                // Find by username
                : await _UserManager.FindByNameAsync(loginCredentials.UsernameOrEmail);

            // If we failed to find a user...
            if (user == null)
            {
                // Return error message to user
                return errorResponse;
            }

            // If we got here, we have found a user
            // Let's validate the password
            bool isValidPassword = await _UserManager.CheckPasswordAsync(user, loginCredentials.Password);

            // If the password was wrong
            if (!isValidPassword)
            {
                // Return error message to user
                return errorResponse;
            }
            
            // If we got here, the user has passed in correct login details
            
            // Get username
            var username = user.UserName;


            // Set our token's claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(ClaimsIdentity.DefaultNameClaimType, username),

                //new Claim("my key", "my value"), // This one is just an example to show that these are just key/value pairs
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
            return new ApiResponseModel<LoginResultApiModel>
            {
                // Pass back the user details and the token
                Response = new LoginResultApiModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                }
            };
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
