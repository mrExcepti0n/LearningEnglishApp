using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearningEnglishWeb.Areas.Training.Infrastructure.Facades;
using LearningEnglishWeb.Areas.Training.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglishWeb.Areas.Training.Controllers
{
    public class ChooseTranslateTrainingController : Controller
    {
        private readonly ChooseTranslateTrainingFacade _trainingFacade;

        public ChooseTranslateTrainingController(ChooseTranslateTrainingFacade trainingFacade)
        {
            _trainingFacade = trainingFacade;          
        }

        public async Task<IActionResult> Index(bool isReverseWay = false, IEnumerable<int> userWords = null)
        {
            var trainingSettings = new TrainingSettings(isReverseWay, userWords);
            var trainingModel = await _trainingFacade.StartNewGame(HttpContext, trainingSettings);
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