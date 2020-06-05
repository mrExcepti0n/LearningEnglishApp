using System.Collections.Generic;
using LearningEnglishWeb.Areas.Training.Models.Shared;
using LearningEnglishWeb.Areas.Training.ViewModels.Abstractions;

namespace LearningEnglishWeb.Areas.Training.ViewModels.ChooseTranslate
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
