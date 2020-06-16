using Data.Core;
using System.Collections.Generic;

namespace VocabularyApi.Dtos.Training
{
    public class TrainingResultDto
    {
        public TrainingTypeEnum TrainingType { get; set; }
        public bool IsReverseTraining { get; set; }
        public List<TrainingWordResultDto> TrainingWordResults { get; set; }
    }
}
