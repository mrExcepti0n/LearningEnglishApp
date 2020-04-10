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

        [HttpGet("{id}")]
        public ActionResult<UserVocabularyDto> Get(int id)
        {
            var userVocabulary = _vocabularyContext.Set<UserVocabulary>()
                .Select(uv => new { 
                    Id = uv.Id, 
                    WordsCount = uv.Words.Count,
                    Title = uv.Title,
                    UserId = uv.UserId
                }).SingleOrDefault(uv => uv.UserId == userId && uv.Id == id);


            return new UserVocabularyDto { WordsCount = userVocabulary.WordsCount, Title = userVocabulary.Title, Id = userVocabulary.Id };
        }

        [HttpGet("Words")]
        public ActionResult<List<UserWordDto>> GetWords(string mask = null, int? vocabularyId = null )
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

            return translations.Select(tr => new UserWordDto { Id = tr.Id, Word = tr.Word, Translation = tr.Translation })
                             .ToList();
        }








        [HttpGet("{word}/translations")]
        public ActionResult<List<string>> GetTranslations(string word)
        {
            var translations = _vocabularyContext.Set<WordTranslation>().Where(wt => wt.Word.Word == word).ToList();
            return translations.Select(tr => tr.Translation).ToList();
        }

        [HttpPost]
        public ActionResult Post(UserVocabularyWordDto vocabularyWordDto)
        {
            var vocabularyWord = vocabularyWordDto.ToVocabularyWord();


            if (vocabularyWord.UserVocabularyId == default)
            {
                vocabularyWord.UserVocabularyId = GetOrCreateDefaultUserVocabulary();
            }

            _vocabularyContext.Add(vocabularyWord);
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



        [HttpDelete("Words/{wordId}")]
        public ActionResult Delete(int wordId)
        {
            var predicateBuilder = PredicateBuilder.New<UserVocabularyWord>(uvw => uvw.UserVocabulary.UserId == userId && uvw.Id == wordId);

            var word = _vocabularyContext.Set<UserVocabularyWord>().SingleOrDefault(predicateBuilder);

            if (word != null)
            {
                _vocabularyContext.Remove(word);
                _vocabularyContext.SaveChanges();
            }
            return Ok();
        }
    }
}
