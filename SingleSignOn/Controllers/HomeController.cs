using Microsoft.AspNetCore.Mvc;
using SingleSignOn.Models;
using System.Diagnostics;

namespace SingleSignOn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var user = User.Identity;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/Error/Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}