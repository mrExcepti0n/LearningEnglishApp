﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core;
using LearningEnglishWeb.Controllers.Abstraction;
using LearningEnglishWeb.Infrastructure;
using LearningEnglishWeb.Infrastructure.Training;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Models.Training;
using LearningEnglishWeb.Models.Training.CollectWord;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.Services.Abstractions;
using LearningEnglishWeb.ViewModels.Training;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglishWeb.Controllers
{
    public class CollectWordTrainingController : Controller
    {

        private CollectWordTrainingFacade _trainingFacade;
        public CollectWordTrainingController(TrainingFactory trainingFactory, IWordImageService wordImageService, ITrainingService trainingService)
        {
            _trainingFacade = new CollectWordTrainingFacade(trainingFactory, wordImageService, trainingService);
        }

        public async Task<IActionResult> Index(bool isReverseWay = false, IEnumerable<int> userWords = null)
        {
            var trainingSettings = new TrainingSettings(isReverseWay, userWords);
            var trainingModel = await _trainingFacade.StartNewGame(HttpContext, trainingSettings);
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
            var trainingSummarizingModel = await _trainingFacade.GetTrainingSummarizingModelAsync(HttpContext, trainingId);
            return PartialView("../Training/TrainingSummarizing", trainingSummarizingModel);
        }
    }
}