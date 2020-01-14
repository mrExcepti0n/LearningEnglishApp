using LearningEnglishWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.ViewModels.Training
{
    public class ChooseTranslateQuestionModel
    {
        public Guid TrainingId { get; set; }
        public string Word { get; set; }

        public List<string> Translations { get; set; }

        public string QuestionNumber { get; set; }


        public ChooseTranslateQuestionModel()
        {

        }

        public ChooseTranslateQuestionModel(Guid trainingId, ChooseTranslateQuestion question)
        {
            TrainingId = trainingId;
            Word = question.Word;
            Translations = question.TranslationAnswers.Select(ta => ta.Translation).ToList();
            QuestionNumber = question.Number.ToString();
        }

    }
}
