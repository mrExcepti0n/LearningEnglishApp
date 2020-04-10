using LearningEnglishWeb.Models.Training.CollectWord;
using LearningEnglishWeb.ViewModels.Training.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.ViewModels.Training.CollectWord
{
    public class CollectWordQuestionViewModel : QuestionViewModel
    {
       
        public CollectWordQuestionViewModel(CollectWordQuestion collectWordQuestion, string imgSrc) : base(collectWordQuestion.Word, collectWordQuestion.Number, imgSrc)
        {
            Letters = collectWordQuestion.AnswerLetters;
        }


        public char[] Letters { get; set; }
    }
}
