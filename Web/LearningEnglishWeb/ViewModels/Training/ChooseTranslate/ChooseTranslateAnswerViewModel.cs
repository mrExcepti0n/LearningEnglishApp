using LearningEnglishWeb.ViewModels.Training.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.ViewModels.Training.ChooseTranslate
{
    public class ChooseTranslateAnswerViewModel 
    {
        public List<ChooseTranslateAnswerResult> Translations { get; set; }

        public bool CoorectAnswer => Translations.All(tr => tr.UserSelect == tr.IsRight);
       
    }
}
