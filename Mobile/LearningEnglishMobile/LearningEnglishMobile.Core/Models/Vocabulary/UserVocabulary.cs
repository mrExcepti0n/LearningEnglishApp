using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglishMobile.Core.Models.Vocabulary
{
    public class UserVocabulary 
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public int WordsCount { get; set; }

        public string ImageSrc { get; set; }
    }
}
