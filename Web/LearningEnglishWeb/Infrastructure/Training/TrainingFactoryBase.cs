using Data.Core;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Models.Training;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Infrastructure.Training
{
    public abstract class TrainingFactoryBase<T>
    {

        protected ITrainingService _trainingService;
        protected readonly TrainingSettings _trainingSettings;
        protected readonly TrainingTypeEnum _trainingType;
        private Random _random;

        protected TrainingFactoryBase(ITrainingService trainingService, TrainingTypeEnum trainingType, TrainingSettings trainingSettings)
        {
            _trainingService = trainingService;
            _trainingSettings = trainingSettings;
            _trainingType = trainingType;
            _random = new Random();
        }

        public abstract Task<T> GetTraining();


        protected async Task<UserWord[]> GetWords()
        {

            List<UserWord> words;
            if (_trainingSettings.SelectedUserWords?.Any() ?? false)
            {
                words = await _trainingService.GetTrainingWords(_trainingSettings.SelectedUserWords);
            }
            else
            {
                words = await _trainingService.GetRequiringStudyWords(_trainingType, _trainingSettings.IsReverseWay);
            }

            if (_trainingSettings.IsReverseWay)
            {
                words.ForEach(word => {
                    var tmp = word.Word;
                    word.Word = word.Translation;
                    word.Translation = tmp;
                });
            }

            return words.ToArray();
        }


        protected W[] ShuffleWords<W>(W[] words)
        {
            for (int i = words.Length - 1; i >= 1; i--)
            {
                int j = _random.Next(i + 1);
                var temp = words[j];
                words[j] = words[i];
                words[i] = temp;
            }

            return words;
        }


    }
}
