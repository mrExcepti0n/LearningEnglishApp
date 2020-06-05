namespace LearningEnglishWeb.Areas.Training.ViewModels.ChooseTranslate
{
    public class ChooseTranslateAnswerResult
    {
        public bool IsRight { get; set; }

        public bool UserSelect { get; set; }

        public string Option { get; set; }

        public ChooseTranslateAnswerResult(string option, bool isUserSelect, bool isRight)
        {
            Option = option;
            UserSelect = isUserSelect;
            IsRight = isRight;
        }


    }
}
