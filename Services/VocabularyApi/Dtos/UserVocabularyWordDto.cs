using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VocabularyApi.Models;

namespace VocabularyApi.Dtos
{
    public class UserVocabularyWordDto
    {
        [JsonConstructor]
        protected UserVocabularyWordDto()
        {

        }

        public UserVocabularyWordDto(UserVocabularyWord userVocabularyWord, bool isReverse = false)
        {
            Id = userVocabularyWord.Id;

            if (!isReverse)
            {
                Word = userVocabularyWord.Word;
                Translation = userVocabularyWord.Translation;
            }
            else
            {
                Word = userVocabularyWord.Translation;
                Translation = userVocabularyWord.Word;
            }

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
