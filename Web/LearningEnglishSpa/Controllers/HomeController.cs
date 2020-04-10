using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LearningEnglishSpa.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptionsSnapshot<AppSettings> _settings;

        public HomeController(IOptionsSnapshot<AppSettings> settings)
        {
            _settings = settings;
        }

        public IActionResult Configuration()
        {
            return Json(_settings.Value);
        }
    }
}