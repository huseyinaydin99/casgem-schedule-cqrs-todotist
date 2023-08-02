using Microsoft.AspNetCore.Mvc;

namespace Casgem_Schedule.CQRS.TodoList.Controllers
{
    public class CalendarController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
