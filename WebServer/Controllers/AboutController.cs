using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebServer.Controllers
{
    public class AboutController : Controller
    {
        private readonly ILogger<AboutController> _logger;

        public AboutController(ILogger<AboutController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TellMeMore(string moreInfo = "")
        {
            return new JsonResult( new { name = "TellMeMore", content = moreInfo } );
            //return Content($"You asked me about {moreInfo}");
            //return View();
        }
    }
}
