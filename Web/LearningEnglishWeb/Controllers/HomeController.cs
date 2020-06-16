using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LearningEnglishWeb.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.Services.Abstractions;

namespace LearningEnglishWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISpeechService _speechService;
        public HomeController(ISpeechService speechService)
        {
            _speechService = speechService;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
