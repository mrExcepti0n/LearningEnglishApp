using Data.Core;
using LearningEnglishWeb.Infrastructure.Training;
using LearningEnglishWeb.Models.Training.ChooseTranslate;
using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.Services.Abstractions;
using LearningEnglishWeb.ViewModels.Training;
using LearningEnglishWeb.ViewModels.Training.Abstractions;
using LearningEnglishWeb.ViewModels.Training.ChooseTranslate;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Controllers.Abstraction
{
    public class ChooseTranslateTrainingFacade : TrainingFacade<ChooseTranslateTraining, QuestionWithOptions>
    {
        public ChooseTranslateTrainingFacade(TrainingFactory trainingFactory, IWordImageService wordImageService, ITrainingService trainingService) : base(trainingFactory, wordImageService, trainingService)
        {
        }

        public async Task<TrainingViewModel<ChooseTranslateQuestionViewModel>> StartNewGame(HttpContext htppContext, bool isReverseWay = false, LanguageEnum fromLanguage = LanguageEnum.English, LanguageEnum toLanguage = LanguageEnum.Russian)
        {
            ChooseTranslateTraining training = await _trainingFactory.GetChooseTranslateTraining(isReverseWay, fromLanguage, toLanguage);

            if (training.QuestionsCount == 0)
            {
                return null;
            }

            SaveTraining(htppContext,training);
            var question = training.GetCurrentQuestion();
            var image = await training.GetCurrentWordImageSrc(_wordImageService);

            var questionModel = new ChooseTranslateQuestionViewModel(question, image);
            return new TrainingViewModel<ChooseTranslateQuestionViewModel>(training.Id, training.IsReverse, questionModel);
        }

        public async Task<ChooseTranslateQuestionViewModel> GetNextQuestionViewModel(HttpContext htppContext, Guid trainingId)
        {
            var training = GetTraining(htppContext, trainingId);
            var question = training.GetNextQuestion();

            if (question == null)
            {
                return null;
            }

            var image = await training.GetCurrentWordImageSrc(_wordImageService);
            SaveTraining(htppContext, training);
            return new ChooseTranslateQuestionViewModel(question, image);
        }
        

        public ChooseTranslateAnswerViewModel GetCheckAnswerModel(HttpContext htppContext, Guid trainingId, string answer)
        {
            var training = GetTraining(htppContext, trainingId);
            var isRight = training.CheckAnswer(answer);
            SaveTraining(htppContext, training);

            var question = training.GetCurrentQuestion();

            var questionResult = new ChooseTranslateAnswerViewModel
            {
                Translations = question.Options.Select(ta => new ChooseTranslateAnswerResult(ta, ta == question.UserAnswer, ta == question.Translation)).ToList()
            };

            return questionResult;
        }

    }
}
