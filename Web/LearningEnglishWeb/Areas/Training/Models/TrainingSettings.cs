using System.Collections.Generic;
using Data.Core;

namespace LearningEnglishWeb.Areas.Training.Models
{
    public class TrainingSettings
    {
        public TrainingSettings()
        {

        }

        public TrainingSettings(bool isReverseWay, IEnumerable<int> userWords = null)
        {
            IsReverseWay = isReverseWay;
            SelectedUserWords = userWords ?? new List<int>();
        }

        public bool IsReverseWay { get; } 

        public LanguageEnum FromLanguage { get;} = LanguageEnum.English;

        public LanguageEnum ToLanguage { get;  } = LanguageEnum.Russian;


        public IEnumerable<int> SelectedUserWords { get;  }
    }
}
