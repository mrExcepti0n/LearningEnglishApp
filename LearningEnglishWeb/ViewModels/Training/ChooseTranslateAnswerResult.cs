using LearningEnglishWeb.Models;
using LearningEnglishWeb.Models.Training.Shared;

namespace LearningEnglishWeb.ViewModels.Training
{
    public class ChooseTranslateAnswerResult : Answer
    {
        public bool IsRight { get; set; }

        public ChooseTranslateAnswerResult(Answer answer, bool isRight)
        {
            Option = answer.Option;
            UserSelect = answer.UserSelect;
            IsRight = isRight;
        }


    }
}
