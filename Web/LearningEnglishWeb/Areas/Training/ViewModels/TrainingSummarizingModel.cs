namespace LearningEnglishWeb.Areas.Training.ViewModels
{
    public class TrainingSummarizingModel
    {
        public int TotalQuestions;
        public int RightQuestions;

        public string Summary
        {
            get
            {
                if (TotalQuestions == 0)
                    return "Отсутствуют вопросы";

                return  (double)RightQuestions / TotalQuestions > 0.6 ? "Хорошо" : "Плохо";

            }
        }
    }
}
