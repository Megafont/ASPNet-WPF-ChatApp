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
using Newtonsoft.Json.Linq;
using WebServer.Authentication;
using System.Diagnostics;

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
        /// Tries to register for a new account on the server
        /// </summary>
        /// <param name="registerCredentials">The registration details</param>
        /// <returns>the result of the register request</returns>
        [Route("api/register")]
        public async Task<ApiResponseModel<RegisterResultApiModel>> RegisterAsync([FromBody]RegisterCredentialsApiModel registerCredentials)
        {
            // TODO: Localize all strings

            // The message when we fail to login
            string invalidErrorMessage = "Please provide all required details to register for an account.";

            // The error response for a failed login
            var errorResponse = new ApiResponseModel<RegisterResultApiModel>
            {
                // Set error message
                ErrorMessage = invalidErrorMessage
            };

            // If we have no credentials...
            if (registerCredentials == null)
                // Return the failed response
                return errorResponse;


            // Make sure we have a user name
            if (string.IsNullOrWhiteSpace(registerCredentials.UserName))
            {
                // Return error message to user
                return errorResponse;
            }

            // Create the desired user from the given details
            var user = new ApplicationUser
            {
                UserName = registerCredentials.UserName,
                FirstName = registerCredentials.FirstName,
                LastName = registerCredentials.LastName,
                Email = registerCredentials.Email,
            };
            Debug.WriteLine($"UserName: {user.UserName}    First: {user.FirstName}    Last: {user.LastName}    Email: {user.Email}", "Information");
            // Try and create a user
            var result = await _UserManager.CreateAsync(user, registerCredentials.Password);

            // If the registration was successful...
            if (result.Succeeded)
            {
                // Get the user details
                var userIdentity = await _UserManager.FindByNameAsync(registerCredentials.UserName);

                // Generate an email verification code
                var emailVerificationCode = await _UserManager.GenerateEmailConfirmationTokenAsync(user);

                // TODO: Email the user the verification code

                // Return valid response containing all the user's details
                return new ApiResponseModel<RegisterResultApiModel>
                {
                    Response = new RegisterResultApiModel
                    {
                        FirstName = userIdentity.FirstName,
                        LastName = userIdentity.LastName,
                        Email = userIdentity.Email,
                        UserName = userIdentity.UserName,
                        Token = userIdentity.GenerateJwtToken()
                    }
                };
            }
            // Otherwise it failed
            else
            {
                // Return the failed response
                return new ApiResponseModel<RegisterResultApiModel>
                {
                    // Aggregate all errors into a single error string
                    ErrorMessage = result.Errors?.ToList()
                        .Select(f => f.Description)
                        .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}")
                };
            }
        }

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


            // Make sure we have a user name
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
                    Token = user.GenerateJwtToken()
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
