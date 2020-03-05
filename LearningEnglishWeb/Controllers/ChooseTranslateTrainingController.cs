using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core;
using LearningEnglishWeb.Infrastructure;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.ViewModels.Training;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglishWeb.Controllers
{
    public class ChooseTranslateTrainingController : Controller
    {

        static ChooseTranslateTraining _training;
        private TrainingFactoryV2 _trainingFactory;

        public ChooseTranslateTrainingController(TrainingFactoryV2 trainingFactory)
        {
            _trainingFactory = trainingFactory;
        }

        public async Task<IActionResult> Index(bool isReverseWay = false, LanguageEnum fromLanguage = LanguageEnum.English, LanguageEnum toLanguage = LanguageEnum.Russian)
        {
            _training = await _trainingFactory.GetChooseTranslateTraining(isReverseWay, fromLanguage, toLanguage);
            var question = _training.GetNextQuestion();
            var image = await _training.GetCurrentWordImageSrc();

            var questionModel = new ChooseTranslateQuestionModel(_training, question, image);
            return View(questionModel);
        }

        public async Task<IActionResult> GetNextQuestion()
        {
            var question = _training.GetNextQuestion();
            var image = await _training.GetCurrentWordImageSrc();

            if (question != null)
            {
                var questionModel = new ChooseTranslateQuestionModel(_training, question, image);
                return PartialView("ChooseTranslateTrainingQuestion", questionModel);
            }

            var summary = new TrainingSummarizingModel
            {
                RightQuestions = _training.RightAnsweredQuestions,
                TotalQuestions = _training.QuestionsCount
            };

            //_training.Reset();
            return PartialView("../Training/TrainingSummarizing", summary);
        }


        public IActionResult CheckAnswer(string answer)
        {
            var res = _training.CheckAnswer(answer);
            return PartialView("ChooseTranslateTrainingAnswerResult", res);
        }



        public IActionResult SkipQuestion()
        {
            var res = _training.CheckAnswer(null);
            return PartialView("ChooseTranslateTrainingAnswerResult", res);
        }

      
    }
}