using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyApi.Models
{
    public class WordSet
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public byte[] Image { get; set; }


        public ICollection<WordSetItem> WordSetItems { get; set; }
    }
}
