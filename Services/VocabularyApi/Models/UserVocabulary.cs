using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyApi.Models
{
    public class UserVocabulary
    {

        public UserVocabulary()
        {

        }

        public UserVocabulary(string wordSetTitle, int? wordSetId, Guid userId, bool isDefault = false, params UserVocabularyWord[] vocabularyWords)
        {
            Title = wordSetTitle;
            WordSetId = wordSetId;
            UserId = userId;
            IsDefault = false;
            Words = vocabularyWords;
        }


        public int Id { get; set; }

        public string Title { get; set; }
        
        public Guid UserId { get; set; }

        public int? WordSetId { get; set; }


        public bool IsDefault { get; set; }

        public ICollection<UserVocabularyWord> Words { get; set; } = new HashSet<UserVocabularyWord>();
        public WordSet WordSet { get; set; }



        public static UserVocabulary GetDefaultUserVocabulary(Guid userId)
        {
            return new UserVocabulary { IsDefault = true, Title = "Пользовательский набор слов", UserId = userId };
        }
    }
}
