using Data.Core;
using LearningEnglishWeb.Infrastructure.Training;
using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.Models.Training.TranslateWord;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.ViewModels.Training.Abstractions;
using LearningEnglishWeb.ViewModels.Training.TranslateWord;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Controllers.Abstraction
{
    public class TranslateWordTrainingFacade : TrainingFacade<TranslateWordTraining, Question>
    {
        public TranslateWordTrainingFacade(TrainingFactory trainingFactory, IWordImageService wordImageService) : base(trainingFactory, wordImageService)
        {
        }

        public async Task<TrainingViewModel<QuestionViewModel>> StartNewGame(HttpContext htppContext, bool isReverseWay = false, LanguageEnum fromLanguage = LanguageEnum.English, LanguageEnum toLanguage = LanguageEnum.Russian)
        {
            TranslateWordTraining training = await _trainingFactory.GetTranslateTraining(isReverseWay, fromLanguage, toLanguage);

            SaveTraining(htppContext, training);
            var question = training.GetCurrentQuestion();
            var image = await training.GetCurrentWordImageSrc(_wordImageService);

            var questionModel = new QuestionViewModel(question.Word, question.Number, image);
            return new TrainingViewModel<QuestionViewModel>(training.Id, training.IsReverse, questionModel);
        }

        public async Task<QuestionViewModel> GetNextQuestionViewModel(HttpContext htppContext, Guid trainingId)
        {
            var training = GetTraining(htppContext, trainingId);
            var question = training.GetNextQuestion();

            if (question == null)
            {
                return null;
            }

            var image = await training.GetCurrentWordImageSrc(_wordImageService);
            SaveTraining(htppContext, training);
            return new QuestionViewModel(question.Word, question.Number, image);
        }


        public TranslateWordAnswerViewModel GetCheckAnswerModel(HttpContext htppContext, Guid trainingId, string answer)
        {
            var training = GetTraining(htppContext, trainingId);
            var isRight = training.CheckAnswer(answer);
            SaveTraining(htppContext, training);


            var question = training.GetCurrentQuestion();
            var questionResult = new TranslateWordAnswerViewModel
            {
                Word = question.Word,
                UserTranslation = answer,
                RightTranslation = question.Translation
            };

            return questionResult;
        }
    }
}
