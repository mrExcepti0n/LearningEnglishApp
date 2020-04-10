using LearningEnglishWeb.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LearningEnglishWeb.Models
{
    public class WordSetModel : WordSetShortModel
    {
        public WordSetModel(WordSetDto ws) : base(ws)
        {
            Items = ws.Items.Select(i => new WordSetItemModel { Translation = i.Translation, Word = i.Word, Id =i.Id }).ToList();
        }

        public ICollection<WordSetItemModel> Items { get; set; }
    }
}
