using Data.Core;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Models.Training;
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

        public async Task<T> GetTraining<T>(TrainingSettings trainingSettings) where T: ITraining<Question>
        {
            ITraining<Question> training;

            if (typeof(T) == typeof(CollectWordTraining))
            {
                training = await GetCollectWordTraining(trainingSettings);
            }
            else if (typeof(T) == typeof(ChooseTranslateTraining))
            {
                training = await GetChooseTranslateTraining(trainingSettings);
            }
            else if (typeof(T) == typeof(TranslateWordTraining))
            {
                training = await GetTranslateTraining(trainingSettings);
            }
            else
            {
                throw new NotImplementedException();
            }

            return (T)training;
        }


        private async Task<CollectWordTraining> GetCollectWordTraining(TrainingSettings trainingSettings)
        {
            var factory = new CollectWordTrainingFactory(_trainingService, trainingSettings);
            return await factory.GetTraining();
        }


        private async Task<ChooseTranslateTraining> GetChooseTranslateTraining(TrainingSettings trainingSettings)
        {
            var factory = new ChooseTranslateTrainingFactory(_trainingService, trainingSettings);
            return await factory.GetTraining();
        }

        private async Task<TranslateWordTraining> GetTranslateTraining(TrainingSettings trainingSettings)
        {
            return await new TranslateWordTrainingFactory(_trainingService, trainingSettings).GetTraining();
        }
    }
}
