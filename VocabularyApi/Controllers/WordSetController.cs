using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VocabularyApi.Dtos;
using VocabularyApi.Infrastructure.DataAccess;
using VocabularyApi.Models;

namespace VocabularyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordSetController : ControllerBase
    {
        private VocabularyContext _vocabularyContext;


        public WordSetController(VocabularyContext vocabularyContext)
        {
            _vocabularyContext = vocabularyContext;
        }

        private string userId => "464128bc-6ff0-4434-af91-c6c7c223c2c0";

        [HttpGet]
        public ActionResult<List<WordSetShortDto>> Get()
        {
            var wordSets = _vocabularyContext.WordSets.Select(ws => new WordSetShortDto { Id = ws.Id, Image = ws.Image, Title = ws.Title, WordsCount = ws.WordSetItems.Count() }).ToList();
            return wordSets;
        }

        [HttpPost]
        public void AddWords(int[] wordSetItems)
        {
            var wordSets = _vocabularyContext.WordSetItem.Where(wsi => wordSetItems.Contains(wsi.Id) 
                                                            && !_vocabularyContext.UserVocabularies.Any(vw=> vw.UserId == userId && vw.Word == wsi.Word && vw.Translation == wsi.Translation))
                                                          .ToList();

            _vocabularyContext.AddRange(wordSets.Select(ws => new UserVocabulary { UserId = userId, Translation = ws.Translation, Word = ws.Word }));
            _vocabularyContext.SaveChanges();
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