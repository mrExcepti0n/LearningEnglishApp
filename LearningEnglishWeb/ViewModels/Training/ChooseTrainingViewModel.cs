using LearningEnglishWeb.Models.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.ViewModels.Training
{
    public class ChooseTrainingViewModel
    {
        public ChooseTrainingViewModel(Dictionary<(TrainingTypeEnum, bool), int> availibleTrainingWordsCount)
        {
            _availibleTrainingWordsCount = availibleTrainingWordsCount;
        }

        public ChooseTrainingViewModel()
        {

        }

        private Dictionary<(TrainingTypeEnum, bool), int> _availibleTrainingWordsCount { get; set; } = new Dictionary<(TrainingTypeEnum, bool), int>();

        public bool IsTrainingActive(TrainingTypeEnum trainingType, bool isReverseTraining)
        {
            if (!_availibleTrainingWordsCount.ContainsKey((trainingType, isReverseTraining))){
                return true;
            }

            return _availibleTrainingWordsCount[(trainingType, isReverseTraining)] > 0;
        }


        public int? GetTrainingWordsCount(TrainingTypeEnum trainingType, bool isReverseTraining)
        {
            if (!_availibleTrainingWordsCount.ContainsKey((trainingType, isReverseTraining)))
            {
                return null;
            }

            return _availibleTrainingWordsCount[(trainingType, isReverseTraining)];
        }
    }
}
