using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core;
using LearningEnglishWeb.Areas.Training.Services;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Services.Abstractions;
using LearningEnglishWeb.ViewModels.Vocabulary;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglishWeb.Controllers
{
   
    public class VocabularyController : Controller
    {
        public VocabularyController(IVocabularyService vocabularyService, ISpeechService speechService, IWordImageService wordImageService, ITrainingService trainingService)
        {
            _trainingService = trainingService;
            _vocabularyService = vocabularyService;

            _speechService = speechService;
            _wordImageService = wordImageService;
        }

        private readonly IVocabularyService _vocabularyService;
        private readonly ISpeechService _speechService;
        private readonly IWordImageService _wordImageService;

        private readonly ITrainingService _trainingService;


        private async Task FillImages(List<UserWord> words)
        {
            foreach (var word in words)
            {
                word.ImageSrc = await _wordImageService.GetThumbnailSrc(word.Word);
            }
        }


        private async Task FillTrainingRatio(List<UserWord> words)
        {
            var trainingWordsRatio = await _trainingService.GetTrainingWordsRatio(words.Select(w => w.Id).ToList());

            foreach (var word in words)
            {
                word.SetKnowledgeRatio(trainingWordsRatio[word.Id]);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            var words = await _vocabularyService.GetWords(vocabularyId: id);
            await FillImages(words);
            await FillTrainingRatio(words);

            if (id == null)
            {
                var userVocabularies = await _vocabularyService.GetVocabularies();
                return View(new UserVocabulariesViewModel { UserVocabularies = userVocabularies, UserWords = words });
            }
            else
            {            
                var userVocabulary = await _vocabularyService.GetVocabulary(id.Value);
                return View("UserVocabulary", new UserVocabularyViewModel { UserVocabulary = userVocabulary, UserWords = words });
            }
        }

        [HttpGet]
        public async Task<IActionResult> WordList(string mask, int? vocabularyId)
        {
            var words = await _vocabularyService.GetWords(mask, vocabularyId);
            await FillImages(words);
            await FillTrainingRatio(words);
            return PartialView(words);
        }
        [HttpGet]
        public async Task<IActionResult> TranslationList(string word)
        {
            var words = await _vocabularyService.GetTranslations(word);
            return PartialView(new TranslationListModel(words));
        }
     


        [HttpPost]
        public async Task<IActionResult> AddWord(string word, string translation, int? vocabularyId)
        {
            await _vocabularyService.AddWord(word, translation, vocabularyId);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddOwnTranslation(string word, string translation, int? vocabularyId)
        {
            await _vocabularyService.AddWord(word, translation, vocabularyId);
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> RemoveWord(int wordId)
        {
            await _vocabularyService.RemoveWord(wordId);
            return Ok();
        }


        [HttpGet]
        public async Task<string> GetAudio(string word, LanguageEnum language = LanguageEnum.Russian)
        {
            return await _speechService.GetAudio(word, language);
        }

    }
}