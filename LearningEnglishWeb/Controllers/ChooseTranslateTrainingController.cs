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
    public class ChooseTranslateTrainingController : Controller
    {

        ChooseTranslateTraining _training = TrainingFactory.GetChooseTranlsateTraining();


        public IActionResult Index()
        {
            _training.Reset();
            var question = _training.GetNextQuestion();
            var questionModel = new ChooseTranslateQuestionModel(_training.Id, question);
            return View(questionModel);
        }

        public IActionResult GetNextQuestion()
        {
            var question = _training.GetNextQuestion();
            if (question != null)
            {
                var questionModel = new ChooseTranslateQuestionModel(_training.Id, question);
                return PartialView("ChooseTranslateTrainingQuestion", questionModel);
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
            return PartialView("ChooseTranslateTrainingAnswerResult", res);
        }
    }
}