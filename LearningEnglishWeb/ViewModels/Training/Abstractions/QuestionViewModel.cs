using LearningEnglishWeb.Models.Training.CollectWord;
using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.ViewModels.Training.ChooseTranslate;
using LearningEnglishWeb.ViewModels.Training.CollectWord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.ViewModels.Training.Abstractions
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




        private static CollectWordQuestionViewModel Create(CollectWordQuestion question, string src)
        {
            return new CollectWordQuestionViewModel(question, src);
        }

        private static QuestionViewModel Create(Question question, string src)
        {
            return new QuestionViewModel(question, src);
        }

        private static ChooseTranslateQuestionViewModel Create(QuestionWithOptions question, string src)
        {
            return new ChooseTranslateQuestionViewModel(question, src);
        }

        public static dynamic Create(dynamic question, string src)
        {
            return Create(question, src);
        }
    }
}
