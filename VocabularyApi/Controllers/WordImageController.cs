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

namespace VocabularyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordImageController : ControllerBase
    {
        VocabularyContext _context;

        public WordImageController(VocabularyContext context)
        {
            _context = context;
        }


        [HttpGet("{word}")]
        public async Task<FileContentResult>  GetWordImage(string word)
        {
            var vocabularyWord = _context.VocabularyWords.Include(vw => vw.Image).FirstOrDefault(w => w.Word == word);

            if (vocabularyWord == null)
            {
                return null;
            }
            if (vocabularyWord.Image == null)
            {
                await FillWordImage(vocabularyWord);
            }            
            return new FileContentResult(vocabularyWord.Image.Image, "image/gif");
        }



        [HttpGet("{word}/thumbnail")]
        public async Task<FileContentResult> GetWordThumbnaile(string word)
        {
            var vocabularyWord = _context.VocabularyWords.Include(vw => vw.Thumbnail).FirstOrDefault(w => w.Word == word);

            if (vocabularyWord == null)
            {
                return null;
            }
            if (vocabularyWord.Thumbnail == null)
            {
                await FillWordImage(vocabularyWord);
            }

            return new FileContentResult(vocabularyWord.Thumbnail.Image, "image/gif");
        }    


        private async Task FillWordImage(VocabularyWord word)
        {
            var image = GetImage(word.Word);

            word.Image = new WordImage { Image = GetThubnail(image, 220, 220), IsThumbnail = false};
            word.Thumbnail = new WordImage { Image = GetThubnail(image, 60, 60), IsThumbnail = true };

            await _context.SaveChangesAsync();
        }



        private Image GetImage(string word)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = context.OpenAsync("https://yandex.ru/images/search?text=" + word).Result;

            var element = document.QuerySelector("a.serp-item__link img");
            var href = element.GetAttribute("src");
            using (WebClient client = new WebClient())
            {
                Stream stream = client.OpenRead("http:" + href);
                return Image.FromStream(stream);
            }
        }


        private byte[] GetThubnail(Image image, int width, int height)
        {

            if (image.Height > image.Width)
            {
                width = width * image.Width / image.Height;
            }
            else
            {
                height = height * image.Height / image.Width;
            }

            var thubnail = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);

            using (var memoryStream = new MemoryStream())
            {
                thubnail.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }
    }
}