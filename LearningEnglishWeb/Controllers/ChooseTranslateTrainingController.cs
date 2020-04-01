using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core;
using LearningEnglishWeb.Controllers.Abstraction;
using LearningEnglishWeb.Infrastructure;
using LearningEnglishWeb.Infrastructure.Training;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Models.Training.ChooseTranslate;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.Services.Abstractions;
using LearningEnglishWeb.ViewModels.Training;
using LearningEnglishWeb.ViewModels.Training.Abstractions;
using LearningEnglishWeb.ViewModels.Training.ChooseTranslate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LearningEnglishWeb.Controllers
{
    public class ChooseTranslateTrainingController : Controller
    {
        private ChooseTranslateTrainingFacade _trainingFacade;

        public ChooseTranslateTrainingController(TrainingFactory trainingFactory, IWordImageService wordImageService, ITrainingService trainingService)
        {
            _trainingFacade = new ChooseTranslateTrainingFacade(trainingFactory, wordImageService, trainingService);          
        }

        public async Task<IActionResult> Index(bool isReverseWay = false, LanguageEnum fromLanguage = LanguageEnum.English, LanguageEnum toLanguage = LanguageEnum.Russian)
        {
            var trainingModel = await _trainingFacade.StartNewGame(HttpContext, isReverseWay, fromLanguage, toLanguage);
            return View(trainingModel);
        }

        public async Task<IActionResult> GetNextQuestion(Guid trainingId)
        {
            var nextQuestionModel = await _trainingFacade.GetNextQuestionViewModel(HttpContext, trainingId);

            if (nextQuestionModel != null)
            {
                return PartialView("ChooseTranslateTrainingQuestion", nextQuestionModel);
            }

            var trainingSummarizingModel = await  _trainingFacade.GetTrainingSummarizingModelAsync(HttpContext, trainingId);  
            return PartialView("../Training/TrainingSummarizing", trainingSummarizingModel);
        }


        public IActionResult CheckAnswer(Guid trainingId, string answer)
        {
            var model =  _trainingFacade.GetCheckAnswerModel(HttpContext, trainingId, answer);
            return PartialView("ChooseTranslateTrainingAnswerResult", model);
        }      
    }
}