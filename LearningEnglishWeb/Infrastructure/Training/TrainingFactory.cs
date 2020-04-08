using Data.Core;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Models.Training.ChooseTranslate;
using LearningEnglishWeb.Models.Training.CollectWord;
using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.Models.Training.TranslateWord;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.Services.Abstractions;
using System;
using System.Collections.Generic;
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

        public async Task<T> GetTraining<T>(bool isReverseWay = false, LanguageEnum fromLanguage = LanguageEnum.English, LanguageEnum toLanguage = LanguageEnum.Russian) where T: ITraining<Question>
        {

            if (typeof(T) == typeof(CollectWordTraining))
            {
                ITraining<Question> z = await GetCollectWordTraining(isReverseWay, fromLanguage, toLanguage);
                return (T) z;
            }

            if (typeof(T) == typeof(ChooseTranslateTraining))
            {
                ITraining<Question> z = await GetChooseTranslateTraining(isReverseWay, fromLanguage, toLanguage);
                return (T)z;
            }

            if (typeof(T) == typeof(TranslateWordTraining))
            {
                ITraining<Question> z = await GetTranslateTraining(isReverseWay, fromLanguage, toLanguage);
                return (T)z;
            }

            throw new NotImplementedException();
        }


        private async Task<CollectWordTraining> GetCollectWordTraining(bool isReverseWay = false, LanguageEnum fromLanguage = LanguageEnum.English, LanguageEnum toLanguage = LanguageEnum.Russian)
        {
            var factory = new CollectWordTrainingFactory(_trainingService, fromLanguage, toLanguage, isReverseWay);
            return await factory.GetTraining();
        }


        private async Task<ChooseTranslateTraining> GetChooseTranslateTraining(bool isReverseWay = false, LanguageEnum fromLanguage = LanguageEnum.English, LanguageEnum toLanguage = LanguageEnum.Russian)
        {
            var factory = new ChooseTranslateTrainingFactory(_trainingService,  fromLanguage, toLanguage, isReverseWay);
            return await factory.GetTraining();
        }

        private async Task<TranslateWordTraining> GetTranslateTraining(bool isReverseWay = false, LanguageEnum fromLanguage = LanguageEnum.English, LanguageEnum toLanguage = LanguageEnum.Russian)
        {
            return await new TranslateWordTrainingFactory(_trainingService,  fromLanguage, toLanguage, isReverseWay).GetTraining();
        }
    }
}
