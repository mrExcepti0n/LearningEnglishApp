using LearningEnglishWeb.Services;
using LearningEnglishWeb.ViewModels.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models
{
    public class CollectWordTraining : TrainingBase<CollectWordQuestion>
    {
        public CollectWordTraining(IWordImageService wordImageService, IEnumerable<CollectWordQuestion> questions, bool isReverse = false) : base(wordImageService, questions, isReverse)
        {
        }


        public CollectWordQuestionResult CheckAnswer(string answer)
        {
            var question = GetCurrentQuestion();
            var collectWordAnswerResults = new List<CollectWordAnswerResult> { };
            for (int i=0; i< answer.Length; i++)
            {
                var ch = answer[i];
                collectWordAnswerResults.Add(new CollectWordAnswerResult { Letter = ch, IsRight = ch == question.RightAnswer[i] });
            }

            var questionResult = new CollectWordQuestionResult
            {
                CollectWordAnswerResults = collectWordAnswerResults,
                RigtAnswer = question.RightAnswer
            };

            if (questionResult.IsCorrectAnswer)
            {
                RightAnsweredQuestions++;
            }

            return questionResult;
        }
    }
}
