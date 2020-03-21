using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core;
using LearningEnglishWeb.Controllers.Abstraction;
using LearningEnglishWeb.Infrastructure;
using LearningEnglishWeb.Infrastructure.Training;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Models.Training.CollectWord;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.ViewModels.Training;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglishWeb.Controllers
{
    public class CollectWordTrainingController : Controller
    {

        private CollectWordTrainingFacade _trainingFacade;
        public CollectWordTrainingController(TrainingFactory trainingFactory, IWordImageService wordImageService)
        {
            _trainingFacade = new CollectWordTrainingFacade(trainingFactory, wordImageService);
        }

        public async Task<IActionResult> Index(bool isReverseWay = false, LanguageEnum fromLanguage = LanguageEnum.English, LanguageEnum toLanguage = LanguageEnum.Russian)
        {
            var trainingModel = await _trainingFacade.StartNewGame(HttpContext, isReverseWay, fromLanguage, toLanguage);
            return View(trainingModel);
        }

        public IActionResult CheckAnswer(Guid trainingId, string answer)
        {
            var model = _trainingFacade.GetCheckAnswerModel(HttpContext, trainingId, answer);
            return PartialView("CollectWordTrainingAnswerResult", model);
        }


        public async Task<IActionResult> GetNextQuestion(Guid trainingId)
        {
            var nextQuestionModel = await _trainingFacade.GetNextQuestionViewModel(HttpContext, trainingId);
            if (nextQuestionModel != null)
            {
                return PartialView("CollectWordTrainingQuestion", nextQuestionModel);
            }
            var trainingSummarizingModel = _trainingFacade.GetTrainingSummarizingModel(HttpContext, trainingId);
            return PartialView("../Training/TrainingSummarizing", trainingSummarizingModel);
        }
    }
}