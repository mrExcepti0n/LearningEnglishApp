using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<IActionResult> Index()
        {
            _training =  await _trainingFactory.GetCollectWordTraining();

            CollectWordQuestion question = _training.GetNextQuestion();
            return View(model: question);
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
                return PartialView("CollectWordTrainingQuestion", question);
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