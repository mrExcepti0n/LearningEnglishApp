using System;
using LearningEnglishWeb.Areas.Training.Infrastructure.Facades.Abstractions;
using LearningEnglishWeb.Areas.Training.Infrastructure.Factories;
using LearningEnglishWeb.Areas.Training.Models.TranslateWord;
using LearningEnglishWeb.Areas.Training.Services;
using LearningEnglishWeb.Areas.Training.ViewModels.Abstractions;
using LearningEnglishWeb.Areas.Training.ViewModels.TranslateWord;
using LearningEnglishWeb.Services.Abstractions;
using Microsoft.AspNetCore.Http;

namespace LearningEnglishWeb.Areas.Training.Infrastructure.Facades
{
    public class TranslateWordTrainingFacade : TrainingFacade<TranslateWordTraining, QuestionViewModel>
    {
        public TranslateWordTrainingFacade(TrainingFactory trainingFactory, IWordImageService wordImageService, ITrainingService trainingService) : base(trainingFactory, wordImageService, trainingService)
        {
        }

        public TranslateWordAnswerViewModel GetCheckAnswerModel(HttpContext httpContext, Guid trainingId, string answer)
        {
            var training = GetTraining(httpContext, trainingId);
            var isRight = training.CheckAnswer(answer);
            SaveTraining(httpContext, training);


            var question = training.GetCurrentQuestion();
            var questionResult = new TranslateWordAnswerViewModel
            {
                Word = question.Word,
                UserTranslation = answer,
                RightTranslation = question.Translation
            };

            return questionResult;
        }
    }
}
