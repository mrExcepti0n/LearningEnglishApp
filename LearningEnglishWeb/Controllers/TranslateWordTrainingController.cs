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
        TranslateWordTraining _training = TrainingFactory.GetTranslateTraining();




        public IActionResult Index()
        {
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

            _training.Reset();
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