using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglishWeb.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error() => View();
    }
}
