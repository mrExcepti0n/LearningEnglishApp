using Data.Core;

namespace LearningEnglishWeb.Areas.Training.Services.Dtos
{
    public class TrainingAvailableWordsDto
    {
        public TrainingTypeEnum TrainingType { get; set; }
        public bool IsReverseTraining { get; set; }

        public int AvailableWordsCount { get; set; }

    }
}
