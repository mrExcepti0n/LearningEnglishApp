using System;
using Data.Core;
using VocabularyApi.Dtos.Training;
using VocabularyApi.Infrastructure.DataAccess;
using VocabularyApi.Services.Training;
using VocabularyApi.Services.Training.Abstractions;

namespace VocabularyApi.Infrastructure
{
    public class TrainingFactory
    {
        public static ITrainingService<QuestionDto> GetTrainingService(VocabularyContext context, TrainingTypeEnum trainingType)
        {
            if (trainingType == TrainingTypeEnum.ChooseTranslate)
            {
                return new ChooseTranslateTrainingService(context);
            } else if (trainingType == TrainingTypeEnum.CollectWord)
            {
                return new CollectWordTrainingService(context);
            } else if (trainingType == TrainingTypeEnum.TranslateWord)
            {
                return new TranslateWordTrainingService(context);
            }

            throw new NotImplementedException();
        }
    }
}
