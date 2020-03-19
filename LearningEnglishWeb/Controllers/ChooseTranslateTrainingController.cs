using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core;
using LearningEnglishWeb.Infrastructure;
using LearningEnglishWeb.Infrastructure.Training;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Models.Training.ChooseTranslate;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.ViewModels.Training;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LearningEnglishWeb.Controllers
{
    public class ChooseTranslateTrainingController : Controller
    {

        //static ChooseTranslateTraining _training;
        private TrainingFactory _trainingFactory;
        private IWordImageService _wordImageService;

        public ChooseTranslateTrainingController(TrainingFactory trainingFactory, IWordImageService wordImageService)
        {
            _trainingFactory = trainingFactory;
            _wordImageService = wordImageService;
        }

        public async Task<IActionResult> Index(bool isReverseWay = false, LanguageEnum fromLanguage = LanguageEnum.English, LanguageEnum toLanguage = LanguageEnum.Russian)
        {
            ChooseTranslateTraining training = await _trainingFactory.GetChooseTranslateTraining(isReverseWay, fromLanguage, toLanguage);

            SaveTraining(training);

            var question = training.GetCurrentQuestion();
            var image = await training.GetCurrentWordImageSrc(_wordImageService);

            var questionModel = new ChooseTranslateQuestionModel(training, question, image);
            return View(questionModel);
        }


        private ChooseTranslateTraining GetTraining(Guid trainingId)
        {
            var training = HttpContext.Session.GetString(trainingId.ToString());
            return JsonConvert.DeserializeObject<ChooseTranslateTraining>(training);
        }

        private void SaveTraining(ChooseTranslateTraining training)
        {
            HttpContext.Session.SetString(training.Id.ToString(), JsonConvert.SerializeObject(training));
        }

        public async Task<IActionResult> GetNextQuestion(Guid trainingId)
        {
            var training = GetTraining(trainingId);
            var question = training.GetNextQuestion();

            if (question != null)
            {
                var image = await training.GetCurrentWordImageSrc(_wordImageService);
                SaveTraining(training);
                var questionModel = new ChooseTranslateQuestionModel(training, question, image);
                return PartialView("ChooseTranslateTrainingQuestion", questionModel);
            }

            var summary = new TrainingSummarizingModel
            {
                RightQuestions = training.RightAnsweredQuestions,
                TotalQuestions = training.QuestionsCount
            };

            HttpContext.Session.Remove(trainingId.ToString());

            return PartialView("../Training/TrainingSummarizing", summary);
        }


        public IActionResult CheckAnswer(Guid trainingId, string answer)
        {
            var training = GetTraining(trainingId);
            var res = training.CheckAnswer(answer);
            SaveTraining(training);
            return PartialView("ChooseTranslateTrainingAnswerResult", res);
        }



        public IActionResult SkipQuestion(Guid trainingId)
        {
            var training = GetTraining(trainingId);
            var res = training.CheckAnswer(null);
            SaveTraining(training);
            return PartialView("ChooseTranslateTrainingAnswerResult", res);
        }

      
    }
}