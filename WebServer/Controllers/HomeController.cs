using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ASPNet_WPF_ChatApp.WebServer.Data;
using ASPNet_WPF_ChatApp.Core.Routes;

namespace ASPNet_WPF_ChatApp.WebServer.Controllers
{
    /// <summary>
    /// Manages the standard web server pages
    /// </summary>
    public class HomeController : Controller
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

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="logger">The built-in logger</param>
        /// <param name="context">The injected context</param>
        /// <param name="userManager">The Identity sign in manager</param>
        /// <param name="signInManager">The Identity user manager</param>
        public HomeController(
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

        public IActionResult Index()
        {


            // Make sure we have the database
            //_Context.Database.EnsureDeleted();
            _Context.Database.EnsureCreated();


            Debug.WriteLine(_Context.Settings.Any());

            if (!_Context.Settings.Any())
            {
                _Context.Settings.Add(new SettingsDataModel
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "BackgroundColor",
                    Value = "Red",
                });

                Debug.WriteLine($"LOCAL COUNT: {_Context.Settings.Local.Count()}", "informative");
                Debug.WriteLine($"REMOTE COUNT: {_Context.Settings.Count()}", "informative");

                Debug.WriteLine($"LOCAL FIRST_OR_DEFAULT: {_Context.Settings.Local.FirstOrDefault()}", "informative");
                Debug.WriteLine($"REMOTE FIRST_OR_DEFAULT: {_Context.Settings.FirstOrDefault()}", "informative"); 

                _Context.SaveChanges();

                Debug.WriteLine($"LOCAL COUNT: {_Context.Settings.Local.Count()}", "informative");
                Debug.WriteLine($"REMOTE COUNT: {_Context.Settings.Count()}", "informative");

                Debug.WriteLine($"LOCAL FIRST_OR_DEFAULT: {_Context.Settings.Local.FirstOrDefault()}", "informative");
                Debug.WriteLine($"REMOTE FIRST_OR_DEFAULT: {_Context.Settings.FirstOrDefault()}", "informative");

            }


            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        
        /// <summary>
        /// Creates our single user for now
        /// </summary>
        /// <returns></returns>
        [Route(WebRoutes.CreateUser)]
        public async Task<IActionResult> CreateUserAsync()
        {
            var result = await _UserManager.CreateAsync(new ApplicationUser
            {
                UserName = "Megafont",
                Email = "megafont@gmail.com",
                FirstName = "Michael",
                LastName = "Fontanini",
            }, "password");

            if (result.Succeeded)
                return Content("User was created.", "text/html");
            else
                return Content("User creation failed!", "text/html");

        }

        /// <summary>
        /// Private area, must be logged in
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route(WebRoutes.Private)]
        public IActionResult Private()
        {
            return Content($"This is a private area. Welcome, {HttpContext.User.Identity.Name}.", "text/html");
        }

        /// <summary>
        /// A a logout page for testing
        /// </summary>
        /// <returns></returns>
        [Route(WebRoutes.LogOut)]
        public async Task<IActionResult> LogOutAsync()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return Content("Logged out.", "text/html");
        }

        /// <summary>
        /// An auto-login page for testing
        /// </summary>
        /// <param name="returnUrl">The Url to return to if successfully logged in</param>
        /// <returns></returns>
        [Route(WebRoutes.Login)]
        public async Task<IActionResult> LoginAsync(string returnUrl)
        {
            Debug.WriteLine("LOGIN CALLED", "Warning");

            // Sign out any previous sessions
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            // Sign in with the valid credentials
            var result = await _SignInManager.PasswordSignInAsync("Megafont", "password", true, false);

            if (result.Succeeded)
            {
                // If we have no return URL...
                if (string.IsNullOrWhiteSpace(returnUrl))
                    // Go to home
                    return RedirectToAction(nameof(Index));
                
                // Otherwise, go to the return URL
                return Redirect(returnUrl);
            }
            

            return Content("Login failed!", "text/html");
        }
    }
}
