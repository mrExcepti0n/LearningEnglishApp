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
        public VocabularyController(IVocabularyService vocabularyService, ISpeachService speachService, IWordImageService wordImageService, ITrainingService trainingService)
        {
            _trainingService = trainingService;
            _vocabularyService = vocabularyService;

            _speachService = speachService;
            _wordImageService = wordImageService;
        }
        private IVocabularyService _vocabularyService { get; set; } 
        private ISpeachService _speachService { get; set; }
        private IWordImageService _wordImageService { get; set; }

        private ITrainingService _trainingService { get; set; }

        public async Task<IActionResult> Index()
        {
            var words = await _vocabularyService.GetWords();
            var userVocabularies = await _vocabularyService.GetVocabularies();          
            await FillImages(words);
            await FillTrainingRatio(words);
            return View(new UserVocabulariesViewModel {UserVocabularies = userVocabularies, UserWords = words });
        }

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


        public async Task<IActionResult> UserVocabulary(int vocabularyId)
        {
            var words = await _vocabularyService.GetWords(vocabularyId : vocabularyId);
            var userVocabulary = await _vocabularyService.GetVocabulary(vocabularyId);
            await FillImages(words);
            await FillTrainingRatio(words);
            return View(new UserVocabularyViewModel { UserVocabulary = userVocabulary, UserWords = words });
        }


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
            return await _speachService.GetAudio(word, language);
        }

    }
}