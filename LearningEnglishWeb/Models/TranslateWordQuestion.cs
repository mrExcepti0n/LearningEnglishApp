using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models
{
    public class TranslateWordQuestion : QuestionBase
    {
        public string RightTranslation { get; set; }
        public override string Translation => RightTranslation;
    }
}
