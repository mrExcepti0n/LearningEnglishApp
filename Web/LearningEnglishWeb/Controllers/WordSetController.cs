using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using LearningEnglishWeb.Services.Dtos;
using System.Threading.Tasks;
using LearningEnglishWeb.Services.Abstractions;
using System.Collections.Generic;
using LearningEnglishWeb.Models.WordSet;

namespace LearningEnglishWeb.Controllers
{
    public class WordSetController : Controller
    {
        private readonly IWordSetService _wordSetService;
        public WordSetController(IWordSetService wordSetService)
        {
            _wordSetService = wordSetService;
        }

        public async Task<IActionResult> Index()
        {
            var wordSets = await _wordSetService.GetWordSets();
            return View(wordSets.Select(ws => new WordSetShortModel(ws)));
        }

        public async Task<IActionResult> Details(int id)
        {
            var wordSetDto = await _wordSetService.GetWordSet(id);
            return View("WordSet", new WordSetModel(wordSetDto));
        }


        [HttpPost]
        public async Task<IActionResult> Details(int id, ICollection<int> wordSetItemIds)
        {
            var userVocabularyId = await _wordSetService.AddWords(id, wordSetItemIds);
            return RedirectToAction("Index", "Vocabulary", new { id = userVocabularyId});
        }
         

        [HttpGet]
        public IActionResult Add()
        { 
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Add(WordSetAddModel wordSetModel)
        {
            var wordSetDto = new WordSetSaveDto()
            {
                Title = wordSetModel.Title,
                WordSetItems = wordSetModel.Items.Select(wsi => new WordSetItemDto { Translation = wsi.Translation, Word = wsi.Word })
                                                 .ToList()
            };
            using (var binaryReader = new BinaryReader(wordSetModel.Image.OpenReadStream()))
            {
                wordSetDto.Image = binaryReader.ReadBytes((int)wordSetModel.Image.Length);
            }
            await _wordSetService.AddWordSet(wordSetDto);
            return View();
        }     

    }
}
