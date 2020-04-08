using Data.Core;
using LearningEnglishWeb.Infrastructure.Training;
using LearningEnglishWeb.Models.Training.ChooseTranslate;
using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.Services.Abstractions;
using LearningEnglishWeb.ViewModels.Training;
using LearningEnglishWeb.ViewModels.Training.Abstractions;
using LearningEnglishWeb.ViewModels.Training.ChooseTranslate;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Controllers.Abstraction
{
    public class ChooseTranslateTrainingFacade : TrainingFacade<ChooseTranslateTraining, ChooseTranslateQuestionViewModel>
    {
        public ChooseTranslateTrainingFacade(TrainingFactory trainingFactory, IWordImageService wordImageService, ITrainingService trainingService) : base(trainingFactory, wordImageService, trainingService)
        {
        }
        

        public ChooseTranslateAnswerViewModel GetCheckAnswerModel(HttpContext htppContext, Guid trainingId, string answer)
        {
            var training = GetTraining(htppContext, trainingId);
            var isRight = training.CheckAnswer(answer);
            SaveTraining(htppContext, training);

            var question = training.GetCurrentQuestion();

            var questionResult = new ChooseTranslateAnswerViewModel
            {
                Translations = question.Options.Select(ta => new ChooseTranslateAnswerResult(ta, ta == question.UserAnswer, ta == question.Translation)).ToList()
            };

            return questionResult;
        }
    }
}
