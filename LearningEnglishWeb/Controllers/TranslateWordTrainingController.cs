using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEnglishWeb.Infrastructure;
using LearningEnglishWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglishWeb.Controllers
{
    public class TranslateWordTrainingController : Controller
    {
        Training _training = TrainingFactory.GetTraining();


        public IActionResult Index()
        {
            var questionModel = new QuestionModel { Word = _training.Questions.First().Word };
            return View(questionModel);
        }

        public IActionResult GetNextQuestion()
        {
            var question = _training.GetNextQuestion();

            if (question != null)
            {
                var questionModel = new QuestionModel { Word = question.Word };
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
    }
}