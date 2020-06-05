using System.Collections.Generic;

namespace LearningEnglishWeb.Areas.Training.Services.Dtos
{
    public class QuestionWithOptionsDto : QuestionDto
    {
        public List<string> Options { get; protected set; }
    }
}
