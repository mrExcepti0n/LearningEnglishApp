using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.ViewModels.Training
{
    public class TrainingSummarizingModel
    {
        public int TotalQuestions;
        public int RightQuestions;

        public string Summary => RightQuestions / TotalQuestions > 0.6 ? "Хорошо" : "Плохо";
    }
}
