namespace LearningEnglishWeb.Areas.Training.ViewModels.TranslateWord
{
    public class TranslateWordAnswerViewModel
    {
        public string Word { get; set; }


        public string UserTranslation { get; set; }

        public string RightTranslation { get; set; }


        public bool IsCorrectAnswer => UserTranslation.ToLower().Trim() == RightTranslation.ToLower().Trim();
    }
}
