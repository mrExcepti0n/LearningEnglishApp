
using Data.Core;
using System.Collections.Generic;

namespace LearningEnglishMobile.Core.Models.Training
{
    public class TrainingSettings
    {
        public TrainingSettings()
        {

        }

        public TrainingSettings(bool isReverseWay, IEnumerable<int> userWords = null)
        {
            IsReverseWay = isReverseWay;
            SelectedUserWords = userWords;
        }

        public bool IsReverseWay { get; set; } 

        public LanguageEnum FromLanguage { get; set; } = LanguageEnum.English;

        public LanguageEnum ToLanguage { get; set; } = LanguageEnum.Russian;


        public IEnumerable<int> SelectedUserWords { get; set; }
    }
}
