using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core;
using LearningEnglishWeb.Areas.Training.Models;
using LearningEnglishWeb.Areas.Training.Services;

namespace LearningEnglishWeb.Areas.Training.Infrastructure.Factories.Abstractions
{
    public abstract class TrainingFactoryBase<T>
    {

        protected ITrainingService TrainingService { get; }
        protected readonly TrainingSettings TrainingSettings;
        protected readonly TrainingTypeEnum TrainingType;

        protected TrainingFactoryBase(ITrainingService trainingService, TrainingTypeEnum trainingType, TrainingSettings trainingSettings)
        {
            TrainingService = trainingService;
            TrainingSettings = trainingSettings;
            TrainingType = trainingType;
        }

        public abstract Task<T> GetTraining();


        protected async Task<IEnumerable<TW>> GetQuestions<TW>()
        {
            return await TrainingService.GetTrainingQuestions<TW>(TrainingType, TrainingSettings.IsReverseWay, TrainingSettings.SelectedUserWords);
        }

        //protected async Task<UserWord[]> GetWords()
        //{

        //    List<UserWord> words;
        //    if (TrainingSettings.SelectedUserWords?.Any() ?? false)
        //    {
        //        words = await TrainingService.GetTrainingWords(TrainingSettings.SelectedUserWords);
        //    }
        //    else
        //    {
        //        words = await TrainingService.GetRequiringStudyWords(_trainingType, TrainingSettings.IsReverseWay);
        //    }

        //    if (TrainingSettings.IsReverseWay)
        //    {
        //        words.ForEach(word => {
        //            var tmp = word.Word;
        //            word.Word = word.Translation;
        //            word.Translation = tmp;
        //        });
        //    }

        //    return words.ToArray();
        //}


    }
}
