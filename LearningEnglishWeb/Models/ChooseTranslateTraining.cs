using LearningEnglishWeb.ViewModels.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models
{
    public class ChooseTranslateTraining : TrainingBase<ChooseTranslateQuestion>
    {
        public ChooseTranslateTraining(IEnumerable<ChooseTranslateQuestion> questions) : base(questions)
        {
        }


        public ChooseTranslateQuestionResult CheckAnswer(string answer)
        {
            var question = GetCurrentQuestion();
            var questionResult = new ChooseTranslateQuestionResult
            {
                Word = question.Word,
                QuestionNumber = question.Number.ToString(),
                Translations = question.TranslationAnswers.Select(ta => new ChooseTranslateAnswerResult(ta, answer)).ToList()
            };

            if (questionResult.CoorectAnswer)
            {
                RightAnsweredQuestions++;
            }

            return questionResult;
        }
    }
}
