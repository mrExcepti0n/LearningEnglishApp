using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.ViewModels.Training.Abstractions
{
    public class QuestionViewModelBase
    {
        public Guid TrainingId { get; set; }
        public string Word { get; set; }
        public string QuestionNumber { get; set; }

        public string ImageSrc { get; set; }

        public bool IsReverse { get; set; }
    }
}
