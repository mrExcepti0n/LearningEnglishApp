using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VocabularyApi.Models;

namespace VocabularyApi.Dtos.Training
{
    public class TrainingResultDto
    {
        public TrainingTypeEnum TrainingType { get; set; }
        public bool IsReverseTraining { get; set; }
        public List<TrainingWordResultDto> TrainingWordResults { get; set; }
    }
}
