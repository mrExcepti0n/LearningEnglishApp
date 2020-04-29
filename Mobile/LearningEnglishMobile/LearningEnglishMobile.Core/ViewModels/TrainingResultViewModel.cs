using LearningEnglishMobile.Core.Models.Training.Results;
using LearningEnglishMobile.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglishMobile.Core.ViewModels
{
    public class TrainingResultViewModel : ViewModelBase
    {
        private string _summary;

        public string Summary { 
            get => _summary; 
            set { 
                _summary = value;
                RaisePropertyChanged(() => Summary); 
            } 
        }

        private string _caption;
        public string Caption
        {
            get => _caption;
            set
            {
                _caption = value;
                RaisePropertyChanged(() => Caption);
            }
        }


        public TrainingResultViewModel()
        {
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is TrainingSummarizing trainingSummarizing)
            {
                SetTrainingSummarizing(trainingSummarizing);
            }
            return base.InitializeAsync(navigationData);
        }

        private void SetTrainingSummarizing(TrainingSummarizing summarizing)
        {
            Summary = summarizing.RightQuestions / summarizing.TotalQuestions > 0.6 ? "Хорошо" : "Плохо";
            Caption = $"Правильных ответов { summarizing.RightQuestions} из {summarizing.TotalQuestions}";
        }
    }
}
