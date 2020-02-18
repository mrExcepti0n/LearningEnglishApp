using LearningEnglishWeb.Models;
using LearningEnglishWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Infrastructure
{
    public class TrainingFactoryV2
    {
        private IVocabularyService _service;
        public TrainingFactoryV2(IVocabularyService service)
        {
            _service = service;
        }

        public CollectWordTraining GetCollectWordTraining()
        {
            return new CollectWordTrainingFactory(_service).GetTraining();
        }
    }
}
