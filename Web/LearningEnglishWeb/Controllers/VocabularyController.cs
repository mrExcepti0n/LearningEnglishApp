using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngleSharp;
using Data.Core;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.Services.Abstractions;
using LearningEnglishWeb.ViewModels;
using LearningEnglishWeb.ViewModels.Vocabulary;
using Microsoft.AspNetCore.Authorization;
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
        private IVocabularyService _vocabularyService { get; set; } 
        private ISpeechService _speechService { get; set; }
        private IWordImageService _wordImageService { get; set; }

        private ITrainingService _trainingService { get; set; }


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
                word.SetKnotledgeRatio(trainingWordsRatio[word.Id]);
            }
        }

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

        

        //public async Task<IActionResult> Details(int id)
        //{
        //    var words = await _vocabularyService.GetWords(vocabularyId : id);
        //    var userVocabulary = await _vocabularyService.GetVocabulary(id);
        //    await FillImages(words);
        //    await FillTrainingRatio(words);
        //    return View(new UserVocabularyViewModel { UserVocabulary = userVocabulary, UserWords = words });
        //}


        public async Task<IActionResult> WordList(string mask, int? vocabularyId)
        {
            var words = await _vocabularyService.GetWords(mask, vocabularyId);
            await FillImages(words);
            await FillTrainingRatio(words);
            return PartialView(words);
        }

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