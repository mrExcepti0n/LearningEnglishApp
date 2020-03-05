using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models
{
    public class CollectWordQuestion : QuestionBase
    {


        public char[] AnswerLetters { get; set; }

        public string RightAnswer { get; set; }
        public override string Translation => RightAnswer;
    }
}
