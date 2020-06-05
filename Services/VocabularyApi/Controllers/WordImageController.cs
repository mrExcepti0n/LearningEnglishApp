using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngleSharp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VocabularyApi.Infrastructure.DataAccess;
using VocabularyApi.Models;
using VocabularyApi.Services;

namespace VocabularyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordImageController : ControllerBase
    {
        readonly VocabularyContext _context;
        private ImageWebLoaderService _imageWebLoaderService;

        public WordImageController(VocabularyContext context, ImageWebLoaderService imageLoaderService)
        {
            _context = context;
            _imageWebLoaderService = imageLoaderService;
        }


        [HttpGet("{word}")]
        public async Task<ActionResult>  GetWordImage(string word)
        {
            var vocabularyWord = _context.VocabularyWords.Include(vw => vw.Image).FirstOrDefault(w => w.Word == word);

            if (vocabularyWord == null)
            {
                return NoContent();
            }
            if (vocabularyWord.Image == null)
            {
                await FillWordImage(vocabularyWord);
            }            
            return new FileContentResult(vocabularyWord.Image.Image, "image/png");
        }



        [HttpGet("{word}/thumbnail")]
        public async Task<ActionResult> GetWordThumbnail(string word)
        {
            var vocabularyWord = _context.VocabularyWords.Include(vw => vw.Thumbnail).FirstOrDefault(w => w.Word == word);

            if (vocabularyWord == null)
            {
                return NoContent();
            }
            if (vocabularyWord.Thumbnail == null)
            {
                await FillWordImage(vocabularyWord);
            }

            return new FileContentResult(vocabularyWord.Thumbnail.Image, "image/png");
        }    


        private async Task FillWordImage(VocabularyWord word)
        {
            var image = _imageWebLoaderService.GetImage(word.Word);
            word.SaveImages(_imageWebLoaderService.GetImage(image), _imageWebLoaderService.GetThumbnail(image));

            await _context.SaveChangesAsync();
        }

    }
}