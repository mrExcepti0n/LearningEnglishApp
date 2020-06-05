using System.Collections.Generic;
using Data.Core;

namespace LearningEnglishWeb.Areas.Training.Services.Dtos
{
    public class TrainingResultDto
    {
        public TrainingTypeEnum TrainingType { get; set; }
        public bool IsReverseTraining { get; set; }
        public List<TrainingWordResultDto> TrainingWordResults { get; set; }
    }
}
