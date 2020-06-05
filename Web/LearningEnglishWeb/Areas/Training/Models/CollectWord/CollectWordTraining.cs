using System.Collections.Generic;
using Data.Core;
using LearningEnglishWeb.Areas.Training.Models.Shared;

namespace LearningEnglishWeb.Areas.Training.Models.CollectWord
{
    public class CollectWordTraining : TrainingBase<CollectWordQuestion>
    {
        public CollectWordTraining(IEnumerable<CollectWordQuestion> questions, bool isReverse = false) : base(questions, TrainingTypeEnum.CollectWord, isReverse )
        {

        }
     
    }
}
