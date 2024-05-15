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
                Debug.WriteLine("HAS SETTINGS!");
                _Context.Settings.Add(new SettingsDataModel
                {
                    Name = "BackgroundColor",
                    Value = "Red",
                });

                var localSettings = _Context.Settings.Local.Count();
                var remoteSettings = _Context.Settings.Count();

                var firstLocal = _Context.Settings.Local.FirstOrDefault();
                var firstRemote = _Context.Settings.FirstOrDefault();

                _Context.SaveChanges();
            }
            

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
