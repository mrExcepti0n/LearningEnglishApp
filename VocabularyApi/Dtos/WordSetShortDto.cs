using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyApi.Dtos
{
    public class WordSetShortDto
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public int WordsCount { get; set; }
        

        public byte[] Image { get; set; }

    }
}
