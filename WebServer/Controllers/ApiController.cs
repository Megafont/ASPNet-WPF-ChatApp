using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json.Linq;

using ASPNet_WPF_ChatApp.WebServer.DependencyInjection;
using ASPNet_WPF_ChatApp.Core.ApiModels;
using ASPNet_WPF_ChatApp.WebServer.Authentication;
using ASPNet_WPF_ChatApp.WebServer.Data;
using ASPNet_WPF_ChatApp.WebServer.Email;
using ASPNet_WPF_ChatApp.WebServer.Identity;
using ASPNet_WPF_ChatApp.Core.Routes;

namespace ASPNet_WPF_ChatApp.WebServer.Controllers
{
    /// <summary>
    /// Manages the web API calls
    /// </summary>
    [Authorize]
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



        #region Login / Registration / Verification

        /// <summary>
        /// Tries to register for a new account on the server
        /// </summary>
        /// <param name="registerCredentials">The registration details</param>
        /// <returns>the result of the register request</returns>
        [AllowAnonymous]
        [Route(ApiRoutes.Register)]
        public async Task<ApiResponseModel<RegisterResultApiModel>> RegisterAsync([FromBody] RegisterCredentialsApiModel registerCredentials)
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
            {
                // Return the failed response
                return errorResponse;
            }

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

            // Try and create a user
            var result = await _UserManager.CreateAsync(user, registerCredentials.Password);

            // If the registration was successful...
            if (result.Succeeded)
            {
                // Get the user details
                var userIdentity = await _UserManager.FindByNameAsync(user.UserName);

                // Send a verification email to the user
                await SendUserEmailVerificationAsync(user);

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
                    ErrorMessage = result.Errors.AggregateErrors()
                };
            }
        }

        /// <summary>
        /// Logs in a user using token-based authentication
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route(ApiRoutes.Login)]
        public async Task<ApiResponseModel<UserProfileDetailsApiModel>> LogInAsync([FromBody] LoginCredentialsApiModel loginCredentials)
        {
            // TODO: Localize all strings

            // The message when we fail to login
            string invalidErrorMessage = "Invalid username or password!";

            // The error response for a failed login
            var errorResponse = new ApiResponseModel<UserProfileDetailsApiModel>
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
            return new ApiResponseModel<UserProfileDetailsApiModel>
            {
                // Pass back the user details and the token
                Response = new UserProfileDetailsApiModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    Token = user.GenerateJwtToken()
                }
            };
        }

        [AllowAnonymous]
        [Route(ApiRoutes.VerifyEmail)]")]
        [HttpGet]
        public async Task<ActionResult> VerifyEmailAsync(string userId, string emailToken)
        {
            // Get the user
            var user = await _UserManager.FindByIdAsync(userId);

            // NOTE: Issue at the minute with Url Decoding that contains /s does not replace them.
            //       MY NOTES: This issue is still present in 2024 for some reason. :(
            //       https://github.com/aspnet/Home/issues/2669
            //
            //       For now, manually fix that
            emailToken = emailToken.Replace("%2f", "/").Replace("%2F", "/");


            // If the user is null
            if (user == null)
            {
                // TODO: Nice UI
                return Content("User not found!");
            }

            // If we have the user...

            // Verify the email token
            var result = await _UserManager.ConfirmEmailAsync(user, emailToken);

            // If succeeded...
            if (result.Succeeded)
            {
                return Content("Email verified!");
            }

            return Content("Invalid Email Verification token! :(");
        }

        #endregion

        /// <summary>
        /// Returns the user's profile details based on the authenticated user
        /// </summary>
        /// <returns></returns>
        [Route(ApiRoutes.GetUserProfile)]
        public async Task<ApiResponseModel<UserProfileDetailsApiModel>> GetUserProfileAsync()
        {
            // Get user claims
            var user = await _UserManager.GetUserAsync(HttpContext.User);

            // If we have no user...
            if (user == null)
            {
                // Return error to user
                return new ApiResponseModel<UserProfileDetailsApiModel>
                {
                    // TODO: Localization
                    // Pass back the error
                    ErrorMessage = "User not found"
                };
            }

            // Return token to user
            return new ApiResponseModel<UserProfileDetailsApiModel>
            {
                // Pass back the user details and the token
                Response = new UserProfileDetailsApiModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                }
            };
        }

        /// <summary>
        /// Attempts to update the user's profile details
        /// </summary>
        /// <param name="model">The user profile details to update</param>
        /// <returns>
        ///     A successful response if the update was successful,
        ///     otherwise, it returns the errors that occurred
        /// </returns>
        [Route(ApiRoutes.UpdateUserProfile)]
        public async Task<ApiResponseModel> UpdateUserProfileAsync([FromBody]UpdateUserProfileDetailsApiModel model)
        {
            #region Declare Variables

            // Make an empty errors list
            var errors = new List<string>();

            // Tracks whether the email address was changed or not in this update
            bool emailChanged = false;

            #endregion

            #region Get User

            // Get the current user
            var user = await _UserManager.GetUserAsync(HttpContext.User);

            // If we have no user...
            if (user == null)
            {
                // TODO: Localization
                // Return error to user
                return new ApiResponseModel
                {
                    // Pass back the error
                    ErrorMessage = "User not found"
                };
            }

            #endregion

            #region Update User Profile

            // If we have a first name...
            if (model.FirstName != null)
            {
                // Update the first name
                user.FirstName = model.FirstName;
            }

            // If we have a last name...
            if (model.LastName != null)
            {
                // Update the last name
                user.LastName = model.LastName;
            }

            // If we have an email...
            if (model.Email != null &&
                // And it is not the same as the current one...
                !string.Equals(model.Email.Replace(" ", ""), user.NormalizedEmail))
            {
                // Update the email
                user.Email = model.Email;

                // Set the email as unverified
                user.EmailConfirmed = false;

                // Flag that we have changed the email
                emailChanged = true;
            }

            // If we have a user name...
            if (model.UserName != null)
            {
                // Update the user name
                user.UserName = model.UserName;
            }

            #endregion

            #region Save User Profile

            // Attempt to commit changes to the data store
            var result = await _UserManager.UpdateAsync(user);

            // If successful, send out a new verification email
            if (result.Succeeded && emailChanged)
            {
                // Send a new verification email to the user
                await SendUserEmailVerificationAsync(user);
            }

        #endregion

        #region Respond

        // If we were successful
        if (result.Succeeded)
        {
            // Return a successful response
            return new ApiResponseModel();
        }
        // Otherwise if it failed...
        else
        {
            // Return the failed response
            return new ApiResponseModel
            {
                // Aggregate all errors into a single error string
                ErrorMessage = result.Errors.AggregateErrors()
            };
        }

        #endregion
    }

        /// <summary>
        /// Attempts to update the user's password
        /// </summary>
        /// <param name="model">The password details to update</param>
        /// <returns>
        ///     A successful response if the update was successful,
        ///     otherwise, it returns the errors that occurred
        /// </returns>
        [Route(ApiRoutes.UpdatePassword)]
        public async Task<ApiResponseModel> UpdateUserPasswordAsync([FromBody] UpdateUserPasswordDetailsApiModel model)
        {
            #region Declare Variables

            // Make an empty errors list
            var errors = new List<string>();

            #endregion

            #region Get User

            // Get the current user
            var user = await _UserManager.GetUserAsync(HttpContext.User);

            // If we have no user...
            if (user == null)
            {
                // TODO: Localization
                // Return error to user
                return new ApiResponseModel
                {
                    // Pass back the error
                    ErrorMessage = "User not found"
                };
            }

            #endregion

            #region Update Password

            // Attempt to commit changes to the data store
            var result = await _UserManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            #endregion

            #region Respond

            // If we were successful
            if (result.Succeeded)
            {
                // Return a successful response
                return new ApiResponseModel();
            }
            // Otherwise if it failed...
            else
            {
                // Return the failed response
                return new ApiResponseModel
                {
                    // Aggregate all errors into a single error string
                    ErrorMessage = result.Errors.AggregateErrors()
                };
            }

            #endregion
        }

        #region Website Private Areas

        /// <summary>
        /// This is a sample of how to make a private area that only logged in users can access
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route(ApiRoutes.Private)]
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

        #endregion

        #region Private Helpers

        /// <summary>
        /// Sends the given user a new verification email
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task SendUserEmailVerificationAsync(ApplicationUser user)
        {
            // Get the user details
            var userIdentity = await _UserManager.FindByNameAsync(user.UserName);

            // Generate an email verification code
            var emailVerificationCode = await _UserManager.GenerateEmailConfirmationTokenAsync(user);

            // TODO: Replace with APIRoutes that will contain the static routes to use
            var confirmationUrl = $"http://{Request.Host.Value}/api/verify/email/{HttpUtility.UrlEncode(userIdentity.Id)}/{HttpUtility.UrlEncode(emailVerificationCode)}";

            // Send a verification code to the user's email
            await WebServerEmailSender.SendUserVerificationEmailAsync(user.UserName, userIdentity.Email, confirmationUrl);
        }

        #endregion
    }
}
