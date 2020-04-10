using Data.Core;
using LearningEnglishWeb.Infrastructure.Training;
using LearningEnglishWeb.Models.Training;
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
    public abstract class TrainingFacade<T, TVM> where T:  ITraining<Question> where TVM : QuestionViewModel
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


        public async Task<TrainingViewModel<TVM>> StartNewGame(HttpContext htppContext, TrainingSettings trainingSettings)
        {
            var training = await _trainingFactory.GetTraining<T>(trainingSettings);
            if (training.QuestionsCount == 0)
            {
                return default;
            }


            SaveTraining(htppContext, training);
            var question = training.GetCurrentQuestion();
            var image = await training.GetCurrentWordImageSrc(_wordImageService);

            var questionModel = GetQuestionViewModel(question, image);
            return new TrainingViewModel<TVM>(training.Id, training.IsReverse, questionModel);
        }    

        public async Task<TVM> GetNextQuestionViewModel(HttpContext htppContext, Guid trainingId)
        {
            var training = GetTraining(htppContext, trainingId);
            var question = training.GetNextQuestion();

            if (question == null)
            {
                return null;
            }

            var image = await training.GetCurrentWordImageSrc(_wordImageService);
            SaveTraining(htppContext, training);

            return GetQuestionViewModel(question, image);
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



        protected virtual TVM GetQuestionViewModel(Question currentQuestion, string imgSrc)
        {
            return QuestionViewModel.Create(currentQuestion, imgSrc);
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
