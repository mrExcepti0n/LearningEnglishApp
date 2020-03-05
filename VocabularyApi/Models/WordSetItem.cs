using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyApi.Models
{
    public class WordSetItem
    {
        public int Id { get; set; }

        public string Word { get; set; }

        public string Translation { get; set; }

        public int WordSetId { get; set; }

        public WordSet WordSet { get; set; }
    }
}
