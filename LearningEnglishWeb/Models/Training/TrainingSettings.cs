
using Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models.Training
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
