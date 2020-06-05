using LearningEnglishWeb.Areas.Training.Models.CollectWord;
using LearningEnglishWeb.Areas.Training.Models.Shared;
using LearningEnglishWeb.Areas.Training.ViewModels.ChooseTranslate;
using LearningEnglishWeb.Areas.Training.ViewModels.CollectWord;

namespace LearningEnglishWeb.Areas.Training.ViewModels.Abstractions
{
    public class QuestionViewModel
    {
        public QuestionViewModel(string word, int number, string imgSrc)
        {
            Word = word;
            QuestionNumber = number.ToString();
            ImageSrc = imgSrc;
        }

        public QuestionViewModel(Question question, string imgSrc) : this(question.Word, question.Number, imgSrc)
        {
        }

        public string Word { get; set; }
        public string QuestionNumber { get; set; }

        public string ImageSrc { get; set; }

        
        private static CollectWordQuestionViewModel CreateImpl(CollectWordQuestion question, string src)
        {
            return new CollectWordQuestionViewModel(question, src);
        }

        private static QuestionViewModel CreateImpl(Question question, string src)
        {
            return new QuestionViewModel(question, src);
        }

        private static ChooseTranslateQuestionViewModel CreateImpl(QuestionWithOptions question, string src)
        {
            return new ChooseTranslateQuestionViewModel(question, src);
        }

        public static dynamic Create(Question question, string src)
        {
            dynamic dQuestion = question;
            return CreateImpl(dQuestion, src);
        }
    }
}
