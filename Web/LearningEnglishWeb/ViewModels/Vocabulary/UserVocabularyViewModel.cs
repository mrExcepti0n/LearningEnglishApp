using LearningEnglishWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.ViewModels.Vocabulary
{
    public class UserVocabularyViewModel
    {
        public IEnumerable<UserWord> UserWords { get; set; }

        public UserVocabulary UserVocabulary { get; set; }
    }
}
