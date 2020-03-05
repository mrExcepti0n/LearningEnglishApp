using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEnglishWeb.Infrastructure;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.ViewModels.Training;
using Microsoft.AspNetCore.Mvc;


namespace LearningEnglishWeb.Controllers
{
    public class TranslateWordTrainingController : Controller
    {
        static TranslateWordTraining _training;



        TrainingFactoryV2 _trainingFactory;
        public TranslateWordTrainingController(TrainingFactoryV2 trainingFactory)
        {
            _trainingFactory = trainingFactory;
        }



        public async Task<IActionResult> Index()
        {
            _training = await _trainingFactory.GetTranslateTraining();
            var question = _training.GetNextQuestion();
            var questionModel = new TranslateWordQuestionModel(_training.Id, question);
            return View(questionModel);
        }

        public IActionResult GetNextQuestion()
        {
            var question = _training.GetNextQuestion();
            if (question != null)
            {
                var questionModel = new TranslateWordQuestionModel(_training.Id, question);
                return PartialView("TranslateWordTrainingQuestion", questionModel);
            }

            var summary = new TrainingSummarizingModel
            {
                RightQuestions = _training.RightAnsweredQuestions,
                TotalQuestions = _training.QuestionsCount
            };

            return PartialView("../Training/TrainingSummarizing", summary);
        }


        public IActionResult CheckAnswer(string answer)
        {
            var res = _training.CheckAnswer(answer);
            return PartialView("TranslateWordTrainingAnswerResult", res);
        }


        public IActionResult SkipQuestion()
        {
            var res = _training.CheckAnswer(null);
            return PartialView("TranslateWordTrainingAnswerResult", res);
        }



    }
}