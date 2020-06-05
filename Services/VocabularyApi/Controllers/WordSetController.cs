using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class WordSetController : ControllerBase
    {
        private readonly VocabularyContext _vocabularyContext;
        private readonly IIdentityService _identityService;

        public WordSetController(VocabularyContext vocabularyContext, IIdentityService identityService)
        {
            _vocabularyContext = vocabularyContext;
            _identityService = identityService;
        }

        private Guid UserId => _identityService.GetUserIdentity();

        [HttpGet]
        public ActionResult<List<WordSetShortDto>> Get()
        {
            var wordSets = _vocabularyContext.WordSets.Select(ws => new WordSetShortDto { Id = ws.Id, Image = ws.Image, Title = ws.Title, WordsCount = ws.WordSetItems.Count() }).ToList();
            return wordSets;
        }

        [HttpPost("UserWordSet")]
        public ActionResult<int> AddWords(UserWordSetSaveDto userWordSet)
        {
            var userWordSetItemIds = new List<int>();
            var wordSet = _vocabularyContext.Set<WordSet>().Include(ws => ws.WordSetItems).SingleOrDefault(ws => ws.Id == userWordSet.WordSetId);

            var selectedWords = wordSet.WordSetItems.Where(wsi => userWordSet.WordSetItemIds.Contains(wsi.Id)).ToList();

            var userVocabulary = _vocabularyContext.Set<UserVocabulary>().SingleOrDefault(uv => uv.WordSetId == userWordSet.WordSetId);
            if (userVocabulary == null)
            {              
                userVocabulary = new UserVocabulary(wordSet.Title, userWordSet.WordSetId, UserId);
                _vocabularyContext.Add(userVocabulary);
            }

            List<UserVocabularyWord> userWords = _vocabularyContext.UserVocabularyWords.Where(uvw => uvw.UserVocabulary.UserId == UserId).ToList();
            var wordSets = selectedWords.Where(sw => !userWords.Any(uw => uw.Word == uw.Translation && uw.Translation == sw.Translation)).ToList();

            foreach (var ws in wordSets) {
                userVocabulary.Words.Add(new UserVocabularyWord {Translation = ws.Translation, Word = ws.Word });
            }

            _vocabularyContext.SaveChanges();
            return userVocabulary.Id;
        }

        [HttpGet("{id}")]
        public ActionResult<WordSetDto> Get(int id)
        {
            var wordSet = _vocabularyContext.WordSets.Include(ws => ws.WordSetItems).SingleOrDefault(ws => ws.Id == id);

            return new WordSetDto
            {
                Id = wordSet.Id,
                Image = wordSet.Image,
                Title = wordSet.Title,
                WordsCount = wordSet.WordSetItems.Count(),
                Items = wordSet.WordSetItems.Select(wsi => new WordSetItemDto { Translation = wsi.Translation, Word = wsi.Word, Id = wsi.Id }).ToList()
            };
        }



        [HttpPost]
        public ActionResult Add(WordSetSaveDto wordSetDto)
        {
            var wordSet = new WordSet
            {
                Image = wordSetDto.Image,
                Title = wordSetDto.Title,
                WordSetItems = wordSetDto.WordSetItems.Select(w => new WordSetItem { Word = w.Word, Translation = w.Translation }).ToList()

            };

            _vocabularyContext.Add(wordSet);
            _vocabularyContext.SaveChanges();
            return Ok();
        }
    }
}