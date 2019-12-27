using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEnglishWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglishWeb.Controllers
{
    public class VocabularyController : Controller
    {
        public IActionResult Index()
        {
            var words = new List<Word>
            {
                new Word {Name ="Dog", Translation ="Собака", Transcription = "[Dog]"},
                new Word {Name ="Fox", Translation ="Лиса", Transcription = "[Fox]"}
            };
            return View(words);
        }
    }
}