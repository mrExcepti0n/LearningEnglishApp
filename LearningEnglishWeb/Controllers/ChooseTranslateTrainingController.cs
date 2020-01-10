using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglishWeb.Controllers
{
    public class ChooseTranslateTrainingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}