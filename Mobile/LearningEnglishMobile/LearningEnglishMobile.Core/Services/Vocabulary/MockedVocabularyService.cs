using LearningEnglishMobile.Core.Models.Vocabulary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningEnglishMobile.Core.Services.Vocabulary
{
    public class MockedVocabularyService : IVocabularyService
    {
        private int _currentVocabularyWordsCount;
        public MockedVocabularyService(int currentVocabularyWordsCount = 40)
        {
            _currentVocabularyWordsCount = currentVocabularyWordsCount;
        }

        public IEnumerable<UserWord> GetUserWords(int id)
        {
            return GenerateUserWords(_currentVocabularyWordsCount).ToList();
        }

        private IEnumerable<UserWord> GenerateUserWords(int count)
        {
            for (int i = 1; i <= count; i++)
            {
                yield return new UserWord { 
                    Id = i, 
                    Word ="Word"+i, 
                    Translation = "Слово"+i, 
                    //IsSelected = i%2 ==0
                };
            }
        }

        public IEnumerable<UserVocabulary> GetVocabularies()
        {
            return GenerateUserVocabularies(10).ToList();
        }

      

        private IEnumerable<UserVocabulary> GenerateUserVocabularies(int count)
        {
            for (int i=1; i <= count; i++)
            {
                yield return new UserVocabulary { Id = i, WordsCount = i, Title = "Vocabulary" + i };
            }
        }
    }
}
