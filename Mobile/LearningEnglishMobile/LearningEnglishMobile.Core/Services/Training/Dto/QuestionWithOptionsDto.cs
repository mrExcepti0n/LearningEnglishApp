using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglishMobile.Core.Services.Training.Dto
{
    public class QuestionWithOptionsDto : QuestionDto
    {
        public List<string> Options { get; set; }
    }
}
