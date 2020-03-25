using Data.Core;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Services;
using System;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Infrastructure.Training
{
    public abstract class TrainingFactoryBase<T>
    {

        protected IVocabularyService _vocabularyService;
        protected readonly LanguageEnum _fromLanguage;
        protected readonly LanguageEnum _toLanguage;
        protected readonly bool _reverseWay;

        private Random _random;

        protected TrainingFactoryBase(IVocabularyService vocabularyService, LanguageEnum fromLanguage, LanguageEnum toLanguage, bool reverseWay)
        {
            _vocabularyService = vocabularyService;
            _fromLanguage = fromLanguage;
            _toLanguage = toLanguage;
            _reverseWay = reverseWay;

            _random = new Random();
        }

        public abstract Task<T> GetTraining();


        protected async Task<UserWord[]> GetWords()
        {
             var words = await _vocabularyService.GetRequiringStudyWords();

            if (_reverseWay)
            {
                words.ForEach(word => {
                    var tmp = word.Name;
                    word.Name = word.Translation;
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
