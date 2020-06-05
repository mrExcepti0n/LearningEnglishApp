using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using LearningEnglishWeb.Areas.Training.Infrastructure.Facades.Abstractions;
using LearningEnglishWeb.Areas.Training.Infrastructure.Factories;
using LearningEnglishWeb.Areas.Training.Models.CollectWord;
using LearningEnglishWeb.Areas.Training.Services;
using LearningEnglishWeb.Areas.Training.ViewModels.CollectWord;
using LearningEnglishWeb.Services.Abstractions;

namespace LearningEnglishWeb.Areas.Training.Infrastructure.Facades
{
    public class CollectWordTrainingFacade : TrainingFacade<CollectWordTraining, CollectWordQuestionViewModel>
    {
        public CollectWordTrainingFacade(TrainingFactory trainingFactory, IWordImageService wordImageService, ITrainingService trainingService) : base(trainingFactory, wordImageService, trainingService)
        {
        }

        public CollectWordAnswerViewModel GetCheckAnswerModel(HttpContext httpContext, Guid trainingId, string answer)
        {
            var training = GetTraining(httpContext, trainingId);
            var isRight = training.CheckAnswer(answer);
            SaveTraining(httpContext, training);

            var question = training.GetCurrentQuestion();
            var collectWordAnswerResults = GetCheckedUserAnswerLetters(question).ToList();

            var questionResult = new CollectWordAnswerViewModel
            {
                CollectWordAnswerResults = collectWordAnswerResults,
                IsCorrectAnswer = isRight,
                RightAnswer = question.Translation
            };


            return questionResult;
        }

        private IEnumerable<CollectWordAnswerResult> GetCheckedUserAnswerLetters(CollectWordQuestion question)
        {
            if (question.UserLetters == null) yield break;

            for (int i = 0; i < question.UserLetters.Length; i++)
            {
                var ch = question.UserLetters[i];
                yield return new CollectWordAnswerResult { Letter = ch, IsRight = ch == question.Translation[i] };
            }
        }
    }
}
