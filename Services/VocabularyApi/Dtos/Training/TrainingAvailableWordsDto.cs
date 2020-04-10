using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VocabularyApi.Models;

namespace VocabularyApi.Dtos.Training
{
    public class TrainingAvailableWordsDto
    {
        public TrainingTypeEnum TrainingType { get; set; }
        public bool IsReverseTraining { get; set; }

        public int AvailableWordsCount { get; set; }

    }
}
