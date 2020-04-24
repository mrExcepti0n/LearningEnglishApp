using LearningEnglishMobile.Core.Models.Training.Results;
using LearningEnglishMobile.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglishMobile.Core.ViewModels
{
    public class TrainingResultViewModel : ExtendedBindableObject
    {
        public int TotalQuestions;
        public int RightQuestions;

        public string Summary => RightQuestions / TotalQuestions > 0.6 ? "Хорошо" : "Плохо";

        public string Caption => $"Правильных ответов {RightQuestions} из {TotalQuestions}";


        public TrainingResultViewModel(TrainingSummarizing summarizing)
        {
            TotalQuestions = summarizing.TotalQuestions;
            RightQuestions = summarizing.RightQuestions;
        } 
    }
}
