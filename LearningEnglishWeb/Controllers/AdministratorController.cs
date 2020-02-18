using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LearningEnglishWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglishWeb.Controllers
{
    public class AdministratorController : Controller
    {
        private IVocabularyService _vocabularyService;
        public AdministratorController(IVocabularyService vocabularyService)
        {
            _vocabularyService = vocabularyService;
        }

        [HttpGet]
        public IActionResult Index()
        {


            return View();
        }


        [HttpPost]
        public IActionResult AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    uploadedFile.CopyTo(ms);
                    var fileBytes = ms.ToArray();

                    _vocabularyService.LoadDictionary(fileBytes);

                }
            }


            return Ok();
        }
    }
}