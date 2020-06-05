using System;
using System.Linq;
using LearningEnglishWeb.Areas.Training.Infrastructure.Facades.Abstractions;
using LearningEnglishWeb.Areas.Training.Infrastructure.Factories;
using LearningEnglishWeb.Areas.Training.Models.ChooseTranslate;
using LearningEnglishWeb.Areas.Training.Services;
using LearningEnglishWeb.Areas.Training.ViewModels.ChooseTranslate;
using LearningEnglishWeb.Services.Abstractions;
using Microsoft.AspNetCore.Http;

namespace LearningEnglishWeb.Areas.Training.Infrastructure.Facades
{
    public class ChooseTranslateTrainingFacade : TrainingFacade<ChooseTranslateTraining, ChooseTranslateQuestionViewModel>
    {
        public ChooseTranslateTrainingFacade(TrainingFactory trainingFactory, IWordImageService wordImageService, ITrainingService trainingService) : base(trainingFactory, wordImageService, trainingService)
        {
        }
        

        public ChooseTranslateAnswerViewModel GetCheckAnswerModel(HttpContext httpContext, Guid trainingId, string answer)
        {
            var training = GetTraining(httpContext, trainingId);
            var isRight = training.CheckAnswer(answer);
            SaveTraining(httpContext, training);

            var question = training.GetCurrentQuestion();

            var questionResult = new ChooseTranslateAnswerViewModel
            {
                Translations = question.Options.Select(ta => new ChooseTranslateAnswerResult(ta, ta == question.UserAnswer, ta == question.Translation)).ToList()
            };

            return questionResult;
        }
    }
}
