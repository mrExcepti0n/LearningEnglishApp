using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyApi.Dtos
{
    public class WordSetSaveDto
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public byte[] Image { get; set; }

        public ICollection<WordSetItemDto> WordSetItems { get; set; }
    }
}
