using System.Collections.Generic;

namespace LearningEnglishWeb.Areas.Training.ViewModels.CollectWord
{
    public class CollectWordAnswerViewModel
    {
        public List<CollectWordAnswerResult> CollectWordAnswerResults { get; set; }

        public string RightAnswer { get; set; }

        public bool IsCorrectAnswer { get; set; }
    }
}
