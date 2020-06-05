using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LearningEnglishWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglishWeb.Controllers
{
    public class ErrorController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
