using System.Collections.Generic;
using System.Linq;

namespace LearningEnglishWeb.Areas.Training.ViewModels.ChooseTranslate
{
    public class ChooseTranslateAnswerViewModel 
    {
        public List<ChooseTranslateAnswerResult> Translations { get; set; }

        public bool CorrectAnswer => Translations.All(tr => tr.UserSelect == tr.IsRight);
       
    }
}
