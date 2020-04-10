using LearningEnglishWeb.Models.Training.ChooseTranslate;
using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.ViewModels.Training.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace LearningEnglishWeb.ViewModels.Training.ChooseTranslate
{
    public class ChooseTranslateQuestionViewModel : QuestionViewModel
    {
        public IEnumerable<string> Translations { get; set; }

        public ChooseTranslateQuestionViewModel(QuestionWithOptions question, string imageSrc) : base(question.Word, question.Number, imageSrc)
        {
            Translations = question.Options;
        }



    }
}
