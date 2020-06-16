using Microsoft.AspNetCore.Mvc;

namespace LearningEnglishWeb.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
