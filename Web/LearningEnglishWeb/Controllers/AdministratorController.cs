using System.IO;
using LearningEnglishWeb.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglishWeb.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IImportVocabularyService _importVocabularyService;
        public AdministratorController(IImportVocabularyService importVocabularyService)
        {
            _importVocabularyService = importVocabularyService;
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
                    _importVocabularyService.LoadDictionary(fileBytes);
                }
            }
            return Ok();
        }
    }
}