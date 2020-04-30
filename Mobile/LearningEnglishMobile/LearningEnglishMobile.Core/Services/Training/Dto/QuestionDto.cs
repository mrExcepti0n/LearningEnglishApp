using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglishMobile.Core.Services.Training.Dto
{
    public class QuestionDto
    {
        public int UserWordId { get; set; }

        public int Number { get; set; }

        public string Word { get; set; }

        public string Translation { get; set; }
    }
}
