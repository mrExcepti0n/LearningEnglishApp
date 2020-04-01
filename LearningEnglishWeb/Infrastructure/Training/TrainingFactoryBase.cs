using Data.Core;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Models.Training;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.Services.Abstractions;
using System;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Infrastructure.Training
{
    public abstract class TrainingFactoryBase<T>
    {

        protected ITrainingService _trainingService;
        protected readonly LanguageEnum _fromLanguage;
        protected readonly LanguageEnum _toLanguage;
        protected readonly bool _reverseWay;
        protected readonly TrainingTypeEnum _trainingType;

        private Random _random;

        protected TrainingFactoryBase(ITrainingService trainingService, TrainingTypeEnum trainingType, LanguageEnum fromLanguage, LanguageEnum toLanguage, bool reverseWay)
        {
            _trainingService = trainingService;
            _fromLanguage = fromLanguage;
            _toLanguage = toLanguage;
            _reverseWay = reverseWay;
            _trainingType = trainingType;

            _random = new Random();
        }

        public abstract Task<T> GetTraining();


        protected async Task<UserWord[]> GetWords()
        {
             var words = await _trainingService.GetRequiringStudyWords(_trainingType, _reverseWay);

            if (_reverseWay)
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
