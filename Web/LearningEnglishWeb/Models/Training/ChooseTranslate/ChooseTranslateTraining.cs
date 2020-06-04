using Data.Core;
using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.ViewModels.Training.ChooseTranslate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningEnglishWeb.Models.Training.ChooseTranslate
{
    public class ChooseTranslateTraining : TrainingBase<QuestionWithOptions>
    {
        public ChooseTranslateTraining(IEnumerable<QuestionWithOptions> questions, bool isReverse = false) 
            : base(questions, TrainingTypeEnum.ChooseTranslate, isReverse)
        {
          
        }
      
    }
}
