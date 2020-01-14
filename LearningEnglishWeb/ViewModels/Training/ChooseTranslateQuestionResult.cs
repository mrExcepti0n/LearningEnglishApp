using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.ViewModels.Training
{
    public class ChooseTranslateQuestionResult
    {
        public string Word { get; set; }

        public List<ChooseTranslateAnswerResult> Translations { get; set; }

        public string QuestionNumber { get; set; }


        public bool CoorectAnswer => Translations.All(tr => tr.UserSelect == tr.IsRight);
       
    }
}
