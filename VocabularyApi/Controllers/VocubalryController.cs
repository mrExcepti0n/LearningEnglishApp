using System;
using System.Collections.Generic;
using System.Linq;
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


        [HttpGet]
        public ActionResult<IEnumerable<TranslationDto>> Get(int userId)
        {
            var translations = _vocabularyContext.Set<UserVocabulary>().Where(uv => uv.UserId == userId).ToList();
            return translations.Select(tr => new TranslationDto { Name = tr.Word, Translation = tr.Translation }).ToList();
        }

        [HttpGet("{userId}")]
        public ActionResult<List<TranslationDto>> Get(int userId, [FromQuery] string mask = null)
        {
            var translations = _vocabularyContext.Set<UserVocabulary>().Where(uv => uv.UserId == userId && uv.Word.Contains(mask))
                                                                       .ToList();
            return translations.Select(tr => new TranslationDto { Name = tr.Word, Translation = tr.Translation })
                               .ToList();
        }

        public ActionResult<List<string>> GetTranslations(string word)
        {
            var translations = _vocabularyContext.Set<WordTranslation>().Where(wt => wt.Word.Word == word).ToList();
            return translations.Select(tr => tr.Translation).ToList();
        }
        [HttpPost]
        public void Post([FromQuery] int userId, /*[FromBody]*/ [FromQuery] string word, [FromQuery] string translation)
        {
            _vocabularyContext.Add<UserVocabulary>(new UserVocabulary { Translation = translation, Word = word, UserId = userId });
            _vocabularyContext.SaveChanges();
        }
    }
}
