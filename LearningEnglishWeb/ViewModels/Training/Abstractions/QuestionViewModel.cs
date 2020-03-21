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

        public string Word { get; set; }
        public string QuestionNumber { get; set; }

        public string ImageSrc { get; set; }
    }
}
