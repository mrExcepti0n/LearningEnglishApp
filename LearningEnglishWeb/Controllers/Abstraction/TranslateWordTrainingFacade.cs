using Data.Core;
using LearningEnglishWeb.Infrastructure.Training;
using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.Models.Training.TranslateWord;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.Services.Abstractions;
using LearningEnglishWeb.ViewModels.Training.Abstractions;
using LearningEnglishWeb.ViewModels.Training.TranslateWord;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Controllers.Abstraction
{
    public class TranslateWordTrainingFacade : TrainingFacade<TranslateWordTraining, QuestionViewModel>
    {
        public TranslateWordTrainingFacade(TrainingFactory trainingFactory, IWordImageService wordImageService, ITrainingService trainingService) : base(trainingFactory, wordImageService, trainingService)
        {
        }

        public TranslateWordAnswerViewModel GetCheckAnswerModel(HttpContext htppContext, Guid trainingId, string answer)
        {
            var training = GetTraining(htppContext, trainingId);
            var isRight = training.CheckAnswer(answer);
            SaveTraining(htppContext, training);


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
