using LearningEnglishWeb.Services.Dtos;
using System;

namespace LearningEnglishWeb.Models
{
    public class WordSetShortModel
    {
        public WordSetShortModel(WordSetShortDto ws)
        {
            Id = ws.Id;
            Title = ws.Title;
            WordsCount = ws.WordsCount;

            var base64 = Convert.ToBase64String(ws.Image);
            ImageSrc = string.Format("data:image/gif;base64,{0}", base64);
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public int WordsCount { get; set; }


        public string ImageSrc { get; set; }
    }
}
