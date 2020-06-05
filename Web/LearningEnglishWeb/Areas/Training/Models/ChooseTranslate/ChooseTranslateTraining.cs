using System.Collections.Generic;
using Data.Core;
using LearningEnglishWeb.Areas.Training.Models.Shared;

namespace LearningEnglishWeb.Areas.Training.Models.ChooseTranslate
{
    public class ChooseTranslateTraining : TrainingBase<QuestionWithOptions>
    {
        public ChooseTranslateTraining(IEnumerable<QuestionWithOptions> questions, bool isReverse = false) 
            : base(questions, TrainingTypeEnum.ChooseTranslate, isReverse)
        {
          
        }
      
    }
}
