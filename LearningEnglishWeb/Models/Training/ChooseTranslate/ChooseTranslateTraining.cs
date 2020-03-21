using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.ViewModels.Training.ChooseTranslate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningEnglishWeb.Models.Training.ChooseTranslate
{
    public class ChooseTranslateTraining : TrainingBase<QuestionWithOptions>
    {
        public ChooseTranslateTraining(IEnumerable<QuestionWithOptions> questions, bool isReverse = false) 
            : base(questions, isReverse)
        {
        }


        //public ChooseTranslateAnswerViewModel CheckAnswer(string answer)
        //{
        //    var question = GetCurrentQuestion();

        //    var isRight = question.CheckAnswer(answer);
        //    if (isRight)
        //    {
        //        RightAnsweredQuestions++;
        //    }

        //    var questionResult = new ChooseTranslateAnswerViewModel
        //    {
        //        Translations = question.Options.Select(ta => new ChooseTranslateAnswerResult(ta, isRight)).ToList()
        //    };

        //    return questionResult;
        //}
      
    }
}
