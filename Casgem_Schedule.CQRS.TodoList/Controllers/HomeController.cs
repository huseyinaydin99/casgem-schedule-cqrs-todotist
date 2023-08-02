using Casgem_Schedule.CQRS.TodoList.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Casgem_Schedule.CQRS.TodoList.Controllers
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
            return RedirectToAction("Index","Calendar");
        }

        public IActionResult Privacy()
        {
            return RedirectToAction("Index", "Calendar");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return RedirectToAction("Index", "Calendar");
        }
    }
}