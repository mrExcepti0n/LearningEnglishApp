using LearningEnglishMobile.Core.Models.Vocabulary;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglishMobile.Core.Services.Vocabulary
{
    public interface IVocabularyService
    {
        IEnumerable<UserVocabulary> GetVocabularies();
        IEnumerable<UserWord> GetUserWords(int id);
    }
}
