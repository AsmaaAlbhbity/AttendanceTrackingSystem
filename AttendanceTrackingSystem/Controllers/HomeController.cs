using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;

namespace AttendanceTrackingSystem.Controllers
{
    public class HomeController : Controller
    {
        IRepoStudent std;

       
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IRepoStudent _std)
        {
            _logger = logger;

            std= _std;
        }
        public IActionResult Index()
        {
             return View();
        }
       

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
