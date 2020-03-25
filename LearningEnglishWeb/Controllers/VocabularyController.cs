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
using LearningEnglishWeb.ViewModels;
using LearningEnglishWeb.ViewModels.Vocabulary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglishWeb.Controllers
{
   
    public class VocabularyController : Controller
    {
        public VocabularyController(IVocabularyService vocabularyService, ISpeachService speachService, IWordImageService wordImageService)
        {
            _vocabularyService = vocabularyService;

            _speachService = speachService;
            _wordImageService = wordImageService;
        }
        private IVocabularyService _vocabularyService { get; set; } 
        private ISpeachService _speachService { get; set; }
        private IWordImageService _wordImageService { get; set; }

        public async Task<IActionResult> Index()
        {
            var words = await _vocabularyService.GetWords();
            var userVocabularies = await _vocabularyService.GetVocabularies();

            userVocabularies = new List<UserVocabulary>
            {
                new UserVocabulary
                {
                    Title = "Набор слов 1",
                    WordsCount = 24
                },
                new UserVocabulary
                {
                    Title = "Набор слов 2",
                    WordsCount = 18
                },
                new UserVocabulary
                {
                    Title = "Набор слов 3",
                    WordsCount = 14
                },
                new UserVocabulary
                {
                    Title = "Набор слов 4",
                    WordsCount = 5
                },
                new UserVocabulary
                {
                    Title = "Набор слов 5",
                    WordsCount = 6
                },
                new UserVocabulary
                {
                    Title = "Набор слов 6",
                    WordsCount = 33
                },
                new UserVocabulary
                {
                    Title = "Набор слов 7",
                    WordsCount = 17
                },
                new UserVocabulary
                {
                    Title = "Набор слов 8",
                    WordsCount = 14
                },
                new UserVocabulary
                {
                    Title = "Набор слов 9",
                    WordsCount = 50
                }
            };
            await FillImages(words); 
            return View(new UserVocabularyViewModel {UserVocabularies = userVocabularies, UserWords = words });
        }

        private async Task FillImages(List<UserWord> words)
        {
            foreach (var word in words)
            {
                word.ImageSrc = await _wordImageService.GetThumbnailSrc(word.Name);
            }
        }

        public async Task<IActionResult> WordList(string mask)
        {
            var words = await _vocabularyService.GetWords(mask);
            await FillImages(words);
            return PartialView(words);
        }

        public async Task<IActionResult> TranslationList(string word)
        {
            var words = await _vocabularyService.GetTranslations(word);
            return PartialView(new TranslationListModel(words));
        }
     


        [HttpPost]
        public async Task<IActionResult> AddWord(string word, string translation)
        {
            await _vocabularyService.AddWord(word, translation);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddOwnTranslation(string word, string translation)
        {
            await _vocabularyService.AddWord(word, translation);
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> RemoveWord(string word, string translation)
        {
            await _vocabularyService.RemoveWord(word, translation);
            return Ok();
        }


        [HttpGet]
        public async Task<string> GetAudio(string word, LanguageEnum language = LanguageEnum.Russian)
        {
            return await _speachService.GetAudio(word, language);
        }

    }
}