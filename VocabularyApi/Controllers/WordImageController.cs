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
            var vocabularyWord = _context.VocabularyWords.Include(vw => vw.Thumbnail).FirstOrDefault(w => w.Word == word);

            if (vocabularyWord == null)
            {
                return null;
            }
            if (vocabularyWord.Thumbnail == null)
            {
                var thumbnail = GetImage(word);

                vocabularyWord.Thumbnail = new Models.WordImage { Image = thumbnail };
            }
            await _context.SaveChangesAsync();


           // using (var memoryStream = new MemoryStream(vocabularyWord.Thumbnail.Image))
           // {
             //   memoryStream.Position = 0;
                return new FileContentResult(vocabularyWord.Thumbnail.Image, "text/plain");
           // }       
        }


        //[HttpGet("[action]")]

        //public void LoadMissedImages()
        //{
        //    var words = _context.VocabularyWords.Where(vw => vw.Image == null).Take(20).ToList();

        //    foreach (var word in words)
        //    {
        //        word.Thumbnail = new Models.WordImage
        //        {
        //            Image = GetImage(word.Word),
        //            IsThumbnail = true
        //        };
        //    }            
        //    _context.SaveChanges();
        //}


        private byte[] GetImage(string word)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = context.OpenAsync("https://yandex.ru/images/search?text=" + word).Result;

            var element = document.QuerySelector("a.serp-item__link img");
            var href = element.GetAttribute("src");
            using (WebClient client = new WebClient())
            {
                Stream stream = client.OpenRead("http:" + href);
                var image = Image.FromStream(stream);


                int thubnailWidth = 220;
                int thubnailHeight = 220;

                if (image.Height > image.Width)
                {
                    thubnailWidth = thubnailWidth * image.Width / image.Height;
                }
                else
                {
                    thubnailHeight = thubnailHeight * image.Height / image.Width;
                }

                var thubnail = image.GetThumbnailImage(thubnailWidth, thubnailHeight, () => false, IntPtr.Zero);             
                
                using (var memoryStream = new MemoryStream())
                {
                    thubnail.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}