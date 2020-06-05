using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyApi.Dtos
{
    public class UserVocabularyDto
    {
        public UserVocabularyDto()
        {
           
        }

        public UserVocabularyDto(int id, string title, int wordsCount)
        {
            Id = id;
            Title = title;
            WordsCount = wordsCount;
        }


        public int Id { get; set; }

        public string Title { get; set; }

        public int WordsCount { get; set; }
    }
}
