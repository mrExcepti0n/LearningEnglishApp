﻿using Data.Core;
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
    public class CollectWordTrainingFacade : TrainingFacade<CollectWordTraining, CollectWordQuestionViewModel>
    {
        public CollectWordTrainingFacade(TrainingFactory trainingFactory, IWordImageService wordImageService, ITrainingService trainingService) : base(trainingFactory, wordImageService, trainingService)
        {
        }

        public CollectWordAnswerViewModel GetCheckAnswerModel(HttpContext htppContext, Guid trainingId, string answer)
        {
            var training = GetTraining(htppContext, trainingId);
            var isRight = training.CheckAnswer(answer);
            SaveTraining(htppContext, training);

            var question = training.GetCurrentQuestion();
            var collectWordAnswerResults = GetCheckedUserAnswerLetters(question).ToList();

            var questionResult = new CollectWordAnswerViewModel
            {
                CollectWordAnswerResults = collectWordAnswerResults,
                IsCorrectAnswer = isRight,
                RigtAnswer = question.Translation
            };


            return questionResult;
        }

        private IEnumerable<CollectWordAnswerResult> GetCheckedUserAnswerLetters(CollectWordQuestion question)
        {
            if (question.UserLetters != null)
            {
                for (int i = 0; i < question.UserLetters.Length; i++)
                {
                    var ch = question.UserLetters[i];
                    yield return new CollectWordAnswerResult { Letter = ch, IsRight = ch == question.Translation[i] };
                }
            }
        }
    }
}