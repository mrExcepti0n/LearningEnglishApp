using System;
using System.Threading.Tasks;
using LearningEnglishWeb.Areas.Training.Infrastructure.Factories;
using LearningEnglishWeb.Areas.Training.Models;
using LearningEnglishWeb.Areas.Training.Models.Shared;
using LearningEnglishWeb.Areas.Training.Services;
using LearningEnglishWeb.Areas.Training.ViewModels;
using LearningEnglishWeb.Areas.Training.ViewModels.Abstractions;
using LearningEnglishWeb.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LearningEnglishWeb.Areas.Training.Infrastructure.Facades.Abstractions
{
    public abstract class TrainingFacade<T, TVM> where T:  ITraining<Question> where TVM : QuestionViewModel
    {
        protected TrainingFactory TrainingFactory { get; }
        protected IWordImageService WordImageService { get; }
        private readonly ITrainingService _trainingService;

        protected TrainingFacade(TrainingFactory trainingFactory, IWordImageService wordImageService, ITrainingService trainingService)
        {
            TrainingFactory = trainingFactory;
            WordImageService = wordImageService;
            _trainingService = trainingService;
        }


        public async Task<TrainingViewModel<TVM>> StartNewGame(HttpContext httpContext, TrainingSettings trainingSettings)
        {
            var training = await TrainingFactory.GetTraining<T>(trainingSettings);
            if (training.QuestionsCount == 0)
            {
                return default;
            }

            SaveTraining(httpContext, training);
            var question = training.GetCurrentQuestion();
            var image = await training.GetCurrentWordImageSrc(WordImageService);

            var questionModel = GetQuestionViewModel(question, image);
            return new TrainingViewModel<TVM>(training.Id, training.IsReverse, questionModel);
        }    

        public async Task<TVM> GetNextQuestionViewModel(HttpContext httpContext, Guid trainingId)
        {
            var training = GetTraining(httpContext, trainingId);
            var question = training.GetNextQuestion();

            if (question == null)
            {
                return null;
            }

            var image = await training.GetCurrentWordImageSrc(WordImageService);
            SaveTraining(httpContext, training);

            return GetQuestionViewModel(question, image);
        }



        protected void SaveTraining(HttpContext httpContext, T training)
        {
            httpContext.Session.SetString(training.Id.ToString(), JsonConvert.SerializeObject(training));
        }

        protected T GetTraining(HttpContext httpContext, Guid trainingId)
        {
            var training = httpContext.Session.GetString(trainingId.ToString());
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

        public async Task<TrainingSummarizingModel> GetTrainingSummarizingModelAsync(HttpContext httpContext, Guid trainingId)
        {
            var training = GetTraining(httpContext, trainingId);
            await SaveResultsAsync(training);
            httpContext.Session.Remove(trainingId.ToString());

            return new TrainingSummarizingModel
            {
                RightQuestions = training.RightAnsweredQuestions,
                TotalQuestions = training.QuestionsCount
            };
        }
    }
}
