using LearningEnglishMobile.Core.Models.Vocabulary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglishMobile.Core.Services.Vocabulary
{
    public interface IVocabularyService
    {
        Task<IEnumerable<UserVocabulary>> GetVocabularies();
        Task<IEnumerable<UserWord>> GetUserWords(int id);
    }
}
