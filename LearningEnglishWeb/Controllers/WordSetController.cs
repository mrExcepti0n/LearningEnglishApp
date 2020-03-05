using LearningEnglishWeb.Models;
using LearningEnglishWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using LearningEnglishWeb.Services.Dtos;

namespace LearningEnglishWeb.Controllers
{
    public class WordSetController : Controller
    {
        private IWordSetService _wordSetService;
        public WordSetController(IWordSetService wordSetService)
        {
            _wordSetService = wordSetService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var wordSets = _wordSetService.GetWordSets();
            return View(wordSets.Select(ws => new WordSetShortModel(ws)));
        }


        [HttpGet("{id}")]
        public IActionResult Index(int id)
        {
            var wordSetDto = _wordSetService.GetWordSet(id);
            return View("WordSet",new WordSetModel(wordSetDto));
        }


        [HttpGet]
        public IActionResult Add()
        { 
            return View();
        }



        [HttpPost]
        public IActionResult Add(WordSetAddModel wordSetModel)
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
            _wordSetService.AddWordSet(wordSetDto);
            return View();
        }


        [HttpPost]
        public void AddWordsToVocabulary(int[] wodsSetItems)
        {
            _wordSetService.AddWords(wodsSetItems);
        }

    }
}
