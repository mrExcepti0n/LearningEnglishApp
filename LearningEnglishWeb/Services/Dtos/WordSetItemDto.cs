using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Services.Dtos
{
    public class WordSetItemDto
    {
        public int Id { get; set; }

        public string Word { get; set; }

        public string Translation { get; set; }
    }
}
