using System;
using System.Threading.Tasks;
using LearningEnglishWeb.Areas.Training.Models;
using LearningEnglishWeb.Areas.Training.Models.ChooseTranslate;
using LearningEnglishWeb.Areas.Training.Models.CollectWord;
using LearningEnglishWeb.Areas.Training.Models.Shared;
using LearningEnglishWeb.Areas.Training.Models.TranslateWord;
using LearningEnglishWeb.Areas.Training.Services;

namespace LearningEnglishWeb.Areas.Training.Infrastructure.Factories
{
    public class TrainingFactory
    {
        private readonly ITrainingService _trainingService;
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
