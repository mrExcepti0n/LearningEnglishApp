using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.ViewModels.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models.Training.ChooseTranslate
{
    public class ChooseTranslateTraining : TrainingBase<QuestionWithOptions>
    {
        public ChooseTranslateTraining(IEnumerable<QuestionWithOptions> questions, bool isReverse = false) 
            : base(questions, isReverse)
        {
        }


        public ChooseTranslateQuestionResult CheckAnswer(string answer)
        {
            var question = GetCurrentQuestion();

            var isRight = question.CheckAnswer(answer);
            if (isRight)
            {
                RightAnsweredQuestions++;
            }

            var questionResult = new ChooseTranslateQuestionResult
            {
                Word = question.Word,
                QuestionNumber = question.Number.ToString(),
                Translations = question.Options.Select(ta => new ChooseTranslateAnswerResult(ta, isRight)).ToList()
            };

            return questionResult;
        }
      
    }
}
