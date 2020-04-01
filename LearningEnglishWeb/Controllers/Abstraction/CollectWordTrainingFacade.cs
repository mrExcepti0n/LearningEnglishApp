using Data.Core;
using LearningEnglishWeb.Infrastructure.Training;
using LearningEnglishWeb.Models.Training.ChooseTranslate;
using LearningEnglishWeb.Models.Training.CollectWord;
using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.Services.Abstractions;
using LearningEnglishWeb.ViewModels.Training;
using LearningEnglishWeb.ViewModels.Training.Abstractions;
using LearningEnglishWeb.ViewModels.Training.ChooseTranslate;
using LearningEnglishWeb.ViewModels.Training.CollectWord;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Controllers.Abstraction
{
    public class CollectWordTrainingFacade : TrainingFacade<CollectWordTraining, CollectWordQuestion>
    {
        public CollectWordTrainingFacade(TrainingFactory trainingFactory, IWordImageService wordImageService, ITrainingService trainingService) : base(trainingFactory, wordImageService, trainingService)
        {
        }
        public async Task<TrainingViewModel<CollectWordQuestionViewModel>> StartNewGame(HttpContext htppContext, bool isReverseWay = false, LanguageEnum fromLanguage = LanguageEnum.English, LanguageEnum toLanguage = LanguageEnum.Russian)
        {
            var training = await _trainingFactory.GetCollectWordTraining(isReverseWay, fromLanguage, toLanguage);
            SaveTraining(htppContext, training);
            var question = training.GetCurrentQuestion();
            var image = await training.GetCurrentWordImageSrc(_wordImageService);

            var questionModel = new CollectWordQuestionViewModel(question.Word, question.Number, image, question.AnswerLetters);
            return new TrainingViewModel<CollectWordQuestionViewModel>(training.Id, training.IsReverse, questionModel);
        }

        public async Task<CollectWordQuestionViewModel> GetNextQuestionViewModel(HttpContext htppContext, Guid trainingId)
        {
            var training = GetTraining(htppContext, trainingId);
            var question = training.GetNextQuestion();

            if (question == null)
            {
                return null;
            }

            var image = await training.GetCurrentWordImageSrc(_wordImageService);
            SaveTraining(htppContext, training);
            return new CollectWordQuestionViewModel(question.Word, question.Number, image, question.AnswerLetters);
        }


    


        public CollectWordAnswerViewModel GetCheckAnswerModel(HttpContext htppContext, Guid trainingId, string answer)
        {
            var training = GetTraining(htppContext, trainingId);
            var isRight = training.CheckAnswer(answer);
            SaveTraining(htppContext, training);

            var question = training.GetCurrentQuestion();
            var collectWordAnswerResults = new List<CollectWordAnswerResult> { };

            for (int i = 0; i < question.UserLetters.Length; i++)
            {
                var ch = question.UserLetters[i];
                collectWordAnswerResults.Add(new CollectWordAnswerResult { Letter = ch, IsRight = ch == question.Translation[i] });
            }

            var questionResult = new CollectWordAnswerViewModel
            {
                CollectWordAnswerResults = collectWordAnswerResults,
                RigtAnswer = question.Translation
            };


            return questionResult;
        }
    }
}
