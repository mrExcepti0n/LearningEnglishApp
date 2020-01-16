using LearningEnglishWeb.ViewModels.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models
{
    public class CollectWordTraining : TrainingBase<TranslateWordQuestion>
    {
        public CollectWordTraining(IEnumerable<TranslateWordQuestion> questions) : base(questions)
        {
        }


        public CollectWordQuestionResult CheckAnswer(string answer)
        {
            var question = GetCurrentQuestion();
            var collectWordAnswerResults = new List<CollectWordAnswerResult> { };
            for (int i=0; i< answer.Length; i++)
            {
                var ch = answer[i];
                collectWordAnswerResults.Add(new CollectWordAnswerResult { Letter = ch, IsRight = ch == question.RightTranslation[i] });
            }

            var questionResult = new CollectWordQuestionResult
            {
                CollectWordAnswerResults = collectWordAnswerResults,
                RigtAnswer = question.RightTranslation
            };

            if (questionResult.IsCorrectAnswer)
            {
                RightAnsweredQuestions++;
            }

            return questionResult;
        }
    }
}
