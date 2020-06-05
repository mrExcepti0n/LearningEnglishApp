using System;

namespace LearningEnglishWeb.Areas.Training.ViewModels.Abstractions
{
    public class TrainingViewModel<Qst> where Qst : QuestionViewModel
    {
        public TrainingViewModel(Guid trainingId, bool isReverse, Qst currentQuestion) {
            TrainingId = trainingId;
            IsReverse = isReverse;
            CurrentQuestion = currentQuestion;
        }

        public Guid TrainingId { get; set; }

        public bool IsReverse { get; set; }

        public Qst CurrentQuestion { get; set; }
    }
}
