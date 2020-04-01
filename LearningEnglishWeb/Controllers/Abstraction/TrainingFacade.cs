using Data.Core;
using LearningEnglishWeb.Infrastructure.Training;
using LearningEnglishWeb.Models.Training.ChooseTranslate;
using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.Services.Abstractions;
using LearningEnglishWeb.Services.Dtos;
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
        private ITrainingService _trainingService;

        public TrainingFacade(TrainingFactory trainingFactory, IWordImageService wordImageService, ITrainingService trainingService)
        {
            _trainingFactory = trainingFactory;
            _wordImageService = wordImageService;
            _trainingService = trainingService;
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


        private async Task SaveResultsAsync(T training)
        {
            var results = training.GetResults();
            await _trainingService.SaveTrainingResult(results);
        }

        public async Task<TrainingSummarizingModel> GetTrainingSummarizingModelAsync(HttpContext htppContext, Guid trainingId)
        {
            var training = GetTraining(htppContext, trainingId);
            await SaveResultsAsync(training);
            htppContext.Session.Remove(trainingId.ToString());

            

            return new TrainingSummarizingModel
            {
                RightQuestions = training.RightAnsweredQuestions,
                TotalQuestions = training.QuestionsCount
            };
        }
    }
}
