using Data.Core;

namespace VocabularyApi.Dtos.Training
{
    public class TrainingAvailableWordsDto
    {
        public TrainingTypeEnum TrainingType { get; set; }
        public bool IsReverseTraining { get; set; }

        public int AvailableWordsCount { get; set; }

    }
}
