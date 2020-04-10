
using LearningEnglishWeb.Models.Training;

namespace LearningEnglishWeb.Services.Dtos
{
    public class TrainingAvailableWordsDto
    {
        public TrainingTypeEnum TrainingType { get; set; }
        public bool IsReverseTraining { get; set; }

        public int AvailableWordsCount { get; set; }

    }
}
