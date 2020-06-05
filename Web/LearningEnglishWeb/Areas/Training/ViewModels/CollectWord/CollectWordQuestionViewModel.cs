using LearningEnglishWeb.Areas.Training.Models.CollectWord;
using LearningEnglishWeb.Areas.Training.ViewModels.Abstractions;

namespace LearningEnglishWeb.Areas.Training.ViewModels.CollectWord
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
