using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyApi.Dtos
{
    public class WordSetDto : WordSetShortDto
    {

        public ICollection<WordSetItemDto> Items { get; set; }
    }
}
