using LearningEnglishWeb.Services;
using LearningEnglishWeb.ViewModels.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models
{
    public class TranslateWordTraining : TrainingBase<TranslateWordQuestion>
    {
        public TranslateWordTraining(IWordImageService wordImageService, List<TranslateWordQuestion> questions, bool isReverse = false) : base(wordImageService, questions, isReverse)
        {
        }      


        public TranslateWordAnswerResult CheckAnswer(string answer)
        {
            var question = GetCurrentQuestion();

            var questionResult = new TranslateWordAnswerResult
            {
                Word = question.Word,
                UserTranslation = answer,
                RightTranslation = question.RightTranslation
            };     
            if (questionResult.IsCorrectAnswer)
            {
                RightAnsweredQuestions++;
            }
            
            return questionResult;
        }
    }
}
