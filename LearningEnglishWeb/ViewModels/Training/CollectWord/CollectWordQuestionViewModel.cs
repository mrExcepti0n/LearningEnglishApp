using LearningEnglishWeb.ViewModels.Training.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.ViewModels.Training.CollectWord
{
    public class CollectWordQuestionViewModel : QuestionViewModel
    {
        public CollectWordQuestionViewModel(string word, int number, string imgSrc, char[] letters) : base(word, number, imgSrc)
        {
            Letters = letters;
        }

        public char[] Letters { get; set; }
    }
}
