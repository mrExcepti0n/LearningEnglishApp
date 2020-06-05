using System.Collections.Generic;
using Data.Core;
using LearningEnglishWeb.Areas.Training.Models.Shared;

namespace LearningEnglishWeb.Areas.Training.Models.TranslateWord
{
    public class TranslateWordTraining : TrainingBase<Question>
    {
        public TranslateWordTraining(IEnumerable<Question> questions, bool isReverse = false) : base(questions, TrainingTypeEnum.TranslateWord, isReverse)
        {
           
        }    


    }
}
