using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly VocabularyContext _vocabularyContext;
        private readonly IIdentityService _identityService;



        public VocabularyController(VocabularyContext vocabularyContext, IIdentityService identityService)
        {
            _vocabularyContext = vocabularyContext;
            _identityService = identityService;
        }


        private Guid userId => _identityService.GetUserIdentity();



        [HttpGet]
        public async Task<ActionResult<List<UserVocabularyDto>>> Get()
        {
            var userVocabularies = _vocabularyContext.UserVocabularies.Where(uv => uv.UserId == userId)
                                                    .Select(uv => new UserVocabularyDto { WordsCount = uv.Words.Count,  Title = uv.Title, Id = uv.Id });
            return await userVocabularies.ToListAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<UserVocabularyDto> Get(int id)
        {
            var userVocabulary = _vocabularyContext.UserVocabularies
                .Select(uv => new { 
                    Id = uv.Id, 
                    WordsCount = uv.Words.Count,
                    Title = uv.Title,
                    UserId = uv.UserId
                }).SingleOrDefault(uv => uv.UserId == userId && uv.Id == id);

            if (userVocabulary == null)
            {
                return NotFound();
            }

            return new UserVocabularyDto(userVocabulary.Id, userVocabulary.Title, userVocabulary.WordsCount);
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

        [HttpPost("Words")]
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

            var userVocabulary = UserVocabulary.GetDefaultUserVocabulary(userId);
            _vocabularyContext.Add(userVocabulary);
            _vocabularyContext.SaveChanges();

            return userVocabulary.Id;
        }

        [HttpDelete("Words/{wordId}")]
        public ActionResult Delete(int wordId)
        {
            var word = _vocabularyContext.UserVocabularyWords.SingleOrDefault(uvw => uvw.UserVocabulary.UserId == userId && uvw.Id == wordId);

            if (word != null)
            {
                _vocabularyContext.Remove(word);
                _vocabularyContext.SaveChanges();
            }

            return Ok();
        }
    }
}
