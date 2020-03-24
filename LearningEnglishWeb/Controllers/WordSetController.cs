using LearningEnglishWeb.Models;
using LearningEnglishWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using LearningEnglishWeb.Services.Dtos;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Controllers
{
    public class WordSetController : Controller
    {
        private IWordSetService _wordSetService;
        public WordSetController(IWordSetService wordSetService)
        {
            _wordSetService = wordSetService;
        }

        public async Task<IActionResult> Index()
        {
            var wordSets = await _wordSetService.GetWordSets();
            return View(wordSets.Select(ws => new WordSetShortModel(ws)));
        }

        [HttpGet("[controller]/[action]/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var wordSetDto = await _wordSetService.GetWordSet(id);
            return View("WordSet", new WordSetModel(wordSetDto));
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


        [HttpPost]
        public async Task AddWordsToVocabulary(int[] wodsSetItems)
        {
           await _wordSetService.AddWords(wodsSetItems);
        }

    }
}
