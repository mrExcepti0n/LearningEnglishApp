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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglishWeb.Controllers
{
   
    public class VocabularyController : Controller
    {
        public VocabularyController(IVocabularyService vocabularyService, ISpeachService speachService)
        {
            _vocabularyService = vocabularyService;

            _speachService = speachService;
        }
        private IVocabularyService _vocabularyService { get; set; } 
        private ISpeachService _speachService { get; set; }

        public async Task<IActionResult> Index()
        {
            var words = await _vocabularyService.GetWords();          
            return View(words);
        }

        public async Task<IActionResult> WordList(string mask)
        {
            var words = await _vocabularyService.GetWords(mask);
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