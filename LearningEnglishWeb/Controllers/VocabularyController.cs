﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglishWeb.Controllers
{
    public class VocabularyController : Controller
    {
        public VocabularyController(IVocabularyService vocabularyService)
        {
            _vocabularyService = vocabularyService;
        }
        private IVocabularyService _vocabularyService { get; set; } 
        public IActionResult Index()
        {
            var words = _vocabularyService.GetWords();          
            return View(words);
        }

        public IActionResult WordList(string mask)
        {
            var words = _vocabularyService.GetWords(mask);
            return PartialView(words);
        }

        public IActionResult TranslationList(string word)
        {
            var words = _vocabularyService.GetTranslations(word);
            return PartialView(words);
        }


        [HttpPost]
        public IActionResult AddWord( string word, string translation)
        {
            _vocabularyService.AddWord(word, translation);            

            var words = _vocabularyService.GetWords();
            return View("Index", words);
        }

    }
}