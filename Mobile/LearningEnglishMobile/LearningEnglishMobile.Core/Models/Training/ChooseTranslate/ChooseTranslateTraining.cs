
using Data.Core;
using LearningEnglishMobile.Core.Models.Training.Results;
using LearningEnglishMobile.Core.Models.Training.Shared;
using System;
using System.Collections.Generic;

namespace LearningEnglishMobile.Core.Models.Training.ChooseTranslate
{
    public class ChooseTranslateTraining : TrainingBase<QuestionWithOptions>
    {
        public ChooseTranslateTraining(IEnumerable<QuestionWithOptions> questions, bool isReverse = false) 
            : base(questions, TrainingTypeEnum.ChooseTranslate, isReverse)
        {
          
        }

    }
}
