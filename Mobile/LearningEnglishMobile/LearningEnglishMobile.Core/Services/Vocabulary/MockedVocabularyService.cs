using LearningEnglishMobile.Core.Models.Vocabulary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglishMobile.Core.Services.Vocabulary
{
    public class MockedVocabularyService : IVocabularyService
    {
        public MockedVocabularyService()
        {
        }

        public async Task<IEnumerable<UserWord>> GetUserWords(int id)
        {
            return await Task.FromResult(GenerateUserWords(10).ToList());
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

        public async Task<IEnumerable<UserVocabulary>> GetVocabularies()
        {
            return await Task.FromResult(GenerateUserVocabularies(10).ToList());
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
