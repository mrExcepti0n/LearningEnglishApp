using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.ViewModels.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models.Training.CollectWord
{
    public class CollectWordTraining : TrainingBase<CollectWordQuestion>
    {
        public CollectWordTraining(IEnumerable<CollectWordQuestion> questions, bool isReverse = false) : base(questions, isReverse)
        {
        }


        public CollectWordQuestionResult CheckAnswer(string answer)
        {
            var question = GetCurrentQuestion();
            var isRight = question.CheckAnswer(answer);
            if (isRight)
            {
                RightAnsweredQuestions++;
            }

            var collectWordAnswerResults = new List<CollectWordAnswerResult> { };
            for (int i=0; i< question.UserLetters.Length; i++)
            {
                var ch = question.UserLetters[i];
                collectWordAnswerResults.Add(new CollectWordAnswerResult { Letter = ch, IsRight = ch == question.Translation[i] });
            }

            var questionResult = new CollectWordQuestionResult
            {
                CollectWordAnswerResults = collectWordAnswerResults,
                RigtAnswer = question.Translation
            };
            return questionResult;
        }
    }
}
