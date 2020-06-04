using Data.Core;
using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.ViewModels.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models.Training.CollectWord
{
    public class CollectWordTraining : TrainingBase<CollectWordQuestion>
    {
        public CollectWordTraining(IEnumerable<CollectWordQuestion> questions, bool isReverse = false) : base(questions, TrainingTypeEnum.CollectWord, isReverse )
        {

        }
     
    }
}
