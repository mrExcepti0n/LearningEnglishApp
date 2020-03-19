using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.ViewModels.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models.Training.TranslateWord
{
    public class TranslateWordTraining : TrainingBase<Question>
    {
        public TranslateWordTraining(IEnumerable<Question> questions, bool isReverse = false) : base(questions, isReverse)
        {
        }      


        public TranslateWordAnswerResult CheckAnswer(string answer)
        {
            var question = GetCurrentQuestion();
            var isCorrect = question.CheckAnswer(answer);
            if (isCorrect)
            {
                RightAnsweredQuestions++;
            }

            var questionResult = new TranslateWordAnswerResult
            {
                Word = question.Word,
                UserTranslation = answer,
                RightTranslation = question.Translation
            };     
         
            
            return questionResult;
        }
    }
}
