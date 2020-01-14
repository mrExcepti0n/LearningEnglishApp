using LearningEnglishWeb.Models;

namespace LearningEnglishWeb.ViewModels.Training
{
    public class ChooseTranslateAnswerResult : ChooseTranslateAnswer
    {
        public bool UserSelect;

        public ChooseTranslateAnswerResult(ChooseTranslateAnswer ta, string userAnswer)
        {
            if (userAnswer == ta.Translation)
            {
                UserSelect = true;
            }

            Translation = ta.Translation;
            IsRight = ta.IsRight;
        }


    }
}
