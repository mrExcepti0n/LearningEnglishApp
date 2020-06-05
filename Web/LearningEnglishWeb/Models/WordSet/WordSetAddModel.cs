using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace LearningEnglishWeb.Models.WordSet
{
    public class WordSetAddModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int WordsCount { get; set; }


        public IFormFile Image { get; set; }

        public List<WordSetItemModel> Items { get; set; }
    }
}
