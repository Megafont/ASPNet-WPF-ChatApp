using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebServer.Data;

namespace WebServer.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// The scoped <see cref="ApplicationDbContext"/>
        /// </summary>
        protected ApplicationDbContext _Context { get; set; }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _Context = context;
        }

        public IActionResult Index()
        {


            // Make sure we have the database
            _Context.Database.EnsureDeleted();
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

    }
}
