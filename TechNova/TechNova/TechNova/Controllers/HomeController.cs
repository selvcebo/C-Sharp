using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TechNova.Models;
using Microsoft.AspNetCore.Authorization;

namespace TechNova.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous] // Home público
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous] // Privacy público (si quieres)
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
