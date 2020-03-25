using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VocabularyApi.Dtos;
using VocabularyApi.Infrastructure.DataAccess;
using VocabularyApi.Models;
using VocabularyApi.Services;

namespace VocabularyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocabularyController : ControllerBase
    {
        private VocabularyContext _vocabularyContext;
        private IIdentityService _identityService;



        public VocabularyController(VocabularyContext vocabularyContext, IIdentityService identityService)
        {
            _vocabularyContext = vocabularyContext;
            _identityService = identityService;
        }


        private Guid userId => _identityService.GetUserIdentity();


        [HttpGet]
        public ActionResult<List<UserVocabularyDto>> Get()
        {
            var userVocabularies = _vocabularyContext.Set<UserVocabulary>().Where(uv => uv.UserId == userId).Select(uv => new UserVocabularyDto { WordsCount = uv.Words.Count,  Title = uv.Title, Id = uv.Id });
            return userVocabularies.ToList();
        }


        [HttpGet("Words")]
        public ActionResult<List<TranslationDto>> GetWords(string mask = null, int? vocabularyId = null )
        {
            var predicateBuilder = PredicateBuilder.New<UserVocabularyWord>(uvw => uvw.UserVocabulary.UserId == userId);

            if (vocabularyId.HasValue)
            {
                predicateBuilder.And(uvw => uvw.UserVocabularyId == vocabularyId);
            } 
            if (!string.IsNullOrWhiteSpace(mask))
            {
                predicateBuilder.And(uvw => uvw.Word.Contains(mask));
            }
            
            var translations = _vocabularyContext.Set<UserVocabularyWord>().Where(predicateBuilder);

            return translations.Select(tr => new TranslationDto { Name = tr.Word, Translation = tr.Translation })
                               .ToList();
        }



        [HttpGet("RequiringStudyWords")]
        public ActionResult<List<TranslationDto>> GetRequiringStudyWords(TrainingTypeEnum trainingType, int count = 10)
        {
            var userWords = _vocabularyContext.Set<UserVocabularyWord>().Where(uv => uv.UserVocabulary.UserId == userId).ToList();

            var requiringStudyWords = userWords.Where(uv => uv.NeedToRepeat(trainingType))
                                                .OrderByDescending(uv => uv.GetKnowledgeRatio(trainingType))
                                                .Take(count)
                                                .ToList();

            return requiringStudyWords.Select(rsw => new TranslationDto { Name = rsw.Word, Translation = rsw.Translation }).ToList();
        }





        [HttpGet("{word}/translations")]
        public ActionResult<List<string>> GetTranslations(string word)
        {
            var translations = _vocabularyContext.Set<WordTranslation>().Where(wt => wt.Word.Word == word).ToList();
            return translations.Select(tr => tr.Translation).ToList();
        }

        [HttpPost]
        public ActionResult Post(UserVocabularyWordDto vocabularyWord)
        {

            if (vocabularyWord.UserVocabularyId == null)
            {
                vocabularyWord.UserVocabularyId = GetOrCreateDefaultUserVocabulary();
            }

            _vocabularyContext.Add<UserVocabularyWord>(new UserVocabularyWord { Translation = vocabularyWord.Translation, Word = vocabularyWord.Name, UserVocabularyId = vocabularyWord.UserVocabularyId.Value });
            _vocabularyContext.SaveChanges();

            return Ok();
        }

        private int GetOrCreateDefaultUserVocabulary()
        {
            var userVocabularyId = _vocabularyContext.UserVocabularies.FirstOrDefault(uv => uv.UserId == userId && uv.IsDefault)?.Id;

            if (userVocabularyId != null)
            {
                return userVocabularyId.Value;
              

            }
            var userVocabulary = new UserVocabulary { IsDefault = true, Title = "Пользовательский набор слов", UserId = userId };

            _vocabularyContext.Add(userVocabulary);
            _vocabularyContext.SaveChanges();

            return userVocabulary.Id;
        }


        [HttpPost("Load")]
        public async Task<ActionResult> LoadDictionary(IFormFile fromFile)
        {
            var loader = new VocabularyLoader(_vocabularyContext);
            await loader.LoadAsync(fromFile.OpenReadStream());
            return Ok();
        }



        [HttpDelete("{name}/{translation}")]
        public ActionResult Delete(string name, string translation, int? userVocabularyId = null)
        {
            var predicateBuilder = PredicateBuilder.New<UserVocabularyWord>(uvw => uvw.UserVocabulary.UserId == userId && uvw.Word == name && uvw.Translation == translation);

            if (userVocabularyId.HasValue)
            {
                predicateBuilder.And(uvw => uvw.UserVocabularyId == userVocabularyId.Value);
            }

            var word = _vocabularyContext.Set<UserVocabularyWord>().FirstOrDefault(predicateBuilder);
            _vocabularyContext.Remove(word);
            _vocabularyContext.SaveChanges();
            return Ok();
        }
    }
}
