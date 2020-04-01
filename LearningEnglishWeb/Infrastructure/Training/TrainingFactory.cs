﻿using Data.Core;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Models.Training.ChooseTranslate;
using LearningEnglishWeb.Models.Training.CollectWord;
using LearningEnglishWeb.Models.Training.TranslateWord;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.Services.Abstractions;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Infrastructure.Training
{
    public class TrainingFactory
    {
        private ITrainingService _trainingService;
        public TrainingFactory(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        public async Task<CollectWordTraining> GetCollectWordTraining(bool isReverseWay = false, LanguageEnum fromLanguage = LanguageEnum.English, LanguageEnum toLanguage = LanguageEnum.Russian)
        {
            var factory = new CollectWordTrainingFactory(_trainingService, fromLanguage, toLanguage, isReverseWay);
            return await factory.GetTraining();
        }


        public async Task<ChooseTranslateTraining> GetChooseTranslateTraining(bool isReverseWay = false, LanguageEnum fromLanguage = LanguageEnum.English, LanguageEnum toLanguage = LanguageEnum.Russian)
        {
            var factory = new ChooseTranslateTrainingFactory(_trainingService,  fromLanguage, toLanguage, isReverseWay);
            return await factory.GetTraining();
        }

        internal async Task<TranslateWordTraining> GetTranslateTraining(bool isReverseWay = false, LanguageEnum fromLanguage = LanguageEnum.English, LanguageEnum toLanguage = LanguageEnum.Russian)
        {
            return await new TranslateWordTrainingFactory(_trainingService,  fromLanguage, toLanguage, isReverseWay).GetTraining();
        }
    }
}
