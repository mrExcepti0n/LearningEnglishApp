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
        private ISpeachService _speachService;
        public HomeController(ISpeachService speachService)
        {
            _speachService = speachService;
        }

        public IActionResult Index()
        {
            return View();
        }      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public async Task<string> GetText(IFormFile formFile)
        {
            using (var fileStream = new FileStream(@"D:\\sounds\soundnew.wav", FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            return "Ok";


            var res = await _speachService.GetText(formFile.OpenReadStream());

            return res;
        }
    }
}
