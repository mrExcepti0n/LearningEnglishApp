using System.Collections.Generic;
using LearningEnglishWeb.Infrastructure;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.ViewModels.Training;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglishWeb.Controllers
{
    public class CollectWordTrainingController : Controller
    {

        TrainingFactoryV2 _trainingFactory;
        public CollectWordTrainingController(TrainingFactoryV2 trainingFactory)
        {
            _trainingFactory = trainingFactory;
        }


        static CollectWordTraining  _training;

        public IActionResult Index()
        {
            _training = _trainingFactory.GetCollectWordTraining();

            var question = _training.GetNextQuestion();
            return View(model: question.Word);
        }

        public IActionResult CheckAnswer(string answer)
        {
            var result = _training.CheckAnswer(answer);

            return PartialView("CollectWordTrainingAnswerResult", result);
        }


        public IActionResult GetNextQuestion()
        {
            var question = _training.GetNextQuestion();
            if (question != null)
            {
                return PartialView("CollectWordTrainingQuestion", question.Word);
            }

            var summary = new TrainingSummarizingModel
            {
                RightQuestions = _training.RightAnsweredQuestions,
                TotalQuestions = _training.QuestionsCount
            };

            

            //_training.Reset();
            return PartialView("../Training/TrainingSummarizing", summary);
        }
    }
}