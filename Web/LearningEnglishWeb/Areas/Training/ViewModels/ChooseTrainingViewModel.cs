using System.Collections.Generic;
using Data.Core;

namespace LearningEnglishWeb.Areas.Training.ViewModels
{
    public class ChooseTrainingViewModel
    {
        public ChooseTrainingViewModel(Dictionary<(TrainingTypeEnum, bool), int> availableTrainingWordsCount)
        {
            AvailableTrainingWordsCount = availableTrainingWordsCount;
        }

        public ChooseTrainingViewModel(IEnumerable<int> selectedUserWords)
        {
            SelectedUserWords = selectedUserWords;
        }

        private Dictionary<(TrainingTypeEnum, bool), int> AvailableTrainingWordsCount { get; } = new Dictionary<(TrainingTypeEnum, bool), int>();
        public IEnumerable<int> SelectedUserWords { get; private set; } =  new List<int> { };

        public bool IsTrainingActive(TrainingTypeEnum trainingType, bool isReverseTraining)
        {
            if (!AvailableTrainingWordsCount.ContainsKey((trainingType, isReverseTraining))){
                return true;
            }

            return AvailableTrainingWordsCount[(trainingType, isReverseTraining)] > 0;
        }


        public int? GetTrainingWordsCount(TrainingTypeEnum trainingType, bool isReverseTraining)
        {
            if (!AvailableTrainingWordsCount.ContainsKey((trainingType, isReverseTraining)))
            {
                return null;
            }

            return AvailableTrainingWordsCount[(trainingType, isReverseTraining)];
        }
    }
}
