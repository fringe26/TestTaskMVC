using Microsoft.AspNetCore.Mvc;

namespace TestTaskMVC.PL.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Result()
        {
            return View();
        }
    }
}
