﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VocabularyApi.Dtos;
using VocabularyApi.Infrastructure.DataAccess;
using VocabularyApi.Models;

namespace VocabularyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocubalryController : ControllerBase
    {
        public VocabularyContext _vocabularyContext;


        public VocubalryController(VocabularyContext vocabularyContext)
        {
            _vocabularyContext = vocabularyContext;
        }

        private string userId => "464128bc-6ff0-4434-af91-c6c7c223c2c0";
        // User.FindFirstValue(ClaimTypes.NameIdentifier);
      

        [HttpGet]
        public ActionResult<List<TranslationDto>> Get([FromQuery] string mask = null)
        {
            var translations = _vocabularyContext.Set<UserVocabulary>().Where(uv => uv.UserId == userId);

            if (mask != null) {
                translations = translations.Where(uv => uv.Word.Contains(mask));
            }
            return translations.Select(tr => new TranslationDto { Name = tr.Word, Translation = tr.Translation })
                               .ToList();
        }

        [HttpGet]
        public ActionResult<List<string>> GetTranslations(string word)
        {
            var translations = _vocabularyContext.Set<WordTranslation>().Where(wt => wt.Word.Word == word).ToList();
            return translations.Select(tr => tr.Translation).ToList();
        }

        [HttpPost]
        public void Post([FromQuery] string word, [FromQuery] string translation)
        {
            _vocabularyContext.Add<UserVocabulary>(new UserVocabulary { Translation = translation, Word = word, UserId = userId });
            _vocabularyContext.SaveChanges();
        }

        [HttpDelete]
        public void Delete(string name, string translation)
        {
            var word = _vocabularyContext.Set<UserVocabulary>().FirstOrDefault(uv => uv.UserId == userId && uv.Word == name && uv.Translation == translation);
            _vocabularyContext.Remove(word);
            _vocabularyContext.SaveChanges();
        }
    }
}