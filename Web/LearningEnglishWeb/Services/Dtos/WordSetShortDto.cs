using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Services.Dtos
{
    public class WordSetShortDto
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public int WordsCount { get; set; }
        

        public byte[] Image { get; set; }

    }
}
