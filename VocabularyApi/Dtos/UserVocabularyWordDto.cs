using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VocabularyApi.Models;

namespace VocabularyApi.Dtos
{
    public class UserVocabularyWordDto
    {
        //public UserVocabularyWordDto()
        //{

        //}

        public UserVocabularyWordDto(UserVocabularyWord userVocabularyWord)
        {
            Id = userVocabularyWord.Id;
            Word = userVocabularyWord.Word;
            Translation = userVocabularyWord.Translation;
            UserVocabularyId = userVocabularyWord.UserVocabularyId;
        }

        public int Id { get; set; }

        public string Word { get; set; }

        public string Translation { get; set; }

        public int? UserVocabularyId { get; set; }


        public UserVocabularyWord ToVocabularyWord()
        {
            return new UserVocabularyWord { Id = Id, UserVocabularyId = UserVocabularyId ?? default, Word = Word, Translation = Translation };
        }


    }
}
