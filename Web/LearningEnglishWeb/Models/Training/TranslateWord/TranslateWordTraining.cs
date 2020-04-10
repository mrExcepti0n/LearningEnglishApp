using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.ViewModels.Training.TranslateWord;
using System.Collections.Generic;

namespace LearningEnglishWeb.Models.Training.TranslateWord
{
    public class TranslateWordTraining : TrainingBase<Question>
    {
        public TranslateWordTraining(IEnumerable<Question> questions, bool isReverse = false) : base(questions, TrainingTypeEnum.TranslateWord, isReverse)
        {
           
        }    


    }
}
