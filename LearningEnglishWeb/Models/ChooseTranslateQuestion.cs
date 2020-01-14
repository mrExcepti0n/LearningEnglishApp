using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models
{
    public class ChooseTranslateQuestion : QuestionBase
    {
        public List<ChooseTranslateAnswer> TranslationAnswers { get; set; }
       
    }
}
