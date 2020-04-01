using LearningEnglishWeb.Models.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Services.Dtos
{
    public class TrainingResultDto
    {
        public TrainingTypeEnum TrainingType { get; set; }
        public bool IsReverseTraining { get; set; }
        public List<TrainingWordResultDto> TrainingWordResults { get; set; }
    }
}
