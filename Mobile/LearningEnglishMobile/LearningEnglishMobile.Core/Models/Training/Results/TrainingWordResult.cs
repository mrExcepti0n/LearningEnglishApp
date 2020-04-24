using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglishMobile.Core.Models.Training.Results
{
    public class TrainingWordResult
    {
        public int UserWordId { get; set; }

        public bool IsRightAnswer { get; set; }

        public string UserAnswer { get; set; }
    }
}
