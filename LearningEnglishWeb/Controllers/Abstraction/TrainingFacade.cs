using Data.Core;
using LearningEnglishWeb.Infrastructure.Training;
using LearningEnglishWeb.Models.Training.ChooseTranslate;
using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.ViewModels.Training;
using LearningEnglishWeb.ViewModels.Training.Abstractions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Controllers.Abstraction
{
    public abstract class TrainingFacade<T, TQ> where T: TrainingBase<TQ> where TQ :Question
    {
        protected TrainingFactory _trainingFactory;
        protected IWordImageService _wordImageService;

        public TrainingFacade(TrainingFactory trainingFactory, IWordImageService wordImageService)
        {
            _trainingFactory = trainingFactory;
            _wordImageService = wordImageService;
        }

        protected void SaveTraining(HttpContext htppContext, T training)
        {
            htppContext.Session.SetString(training.Id.ToString(), JsonConvert.SerializeObject(training));
        }

        protected T GetTraining(HttpContext htppContext, Guid trainingId)
        {
            var training = htppContext.Session.GetString(trainingId.ToString());
            return JsonConvert.DeserializeObject<T>(training);
        }


        public TrainingSummarizingModel GetTrainingSummarizingModel(HttpContext htppContext, Guid trainingId)
        {
            var training = GetTraining(htppContext, trainingId);
            htppContext.Session.Remove(trainingId.ToString());

            return new TrainingSummarizingModel
            {
                RightQuestions = training.RightAnsweredQuestions,
                TotalQuestions = training.QuestionsCount
            };
        }
    }
}
