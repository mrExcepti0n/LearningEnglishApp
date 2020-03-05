using Data.Core;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Infrastructure
{
    internal class TranslateWordTrainingFactory : TrainingFactoryBase<TranslateWordTraining>
    {
        private IVocabularyService _service;

        public TranslateWordTrainingFactory(IVocabularyService vocabularyService, IWordImageService wordImageService, LanguageEnum fromLanguage, LanguageEnum toLanguage, bool reverseWay) 
            : base(vocabularyService, wordImageService, fromLanguage, toLanguage, reverseWay)
        {

        }

        public override async Task<TranslateWordTraining> GetTraining()
        {
            Word[] words = (await _service.GetRequiringStudyWords()).ToArray();

            return new TranslateWordTraining(_wordImageService,GetQuestions(words).ToList());
        }


        private IEnumerable<TranslateWordQuestion> GetQuestions(Word[] words)
        {
            for (var i = 0; i < words.Length; i++)
            {
                yield return new TranslateWordQuestion { Number = i + 1, Word = words[i].Name, RightTranslation = words[i].Translation };
            }
        }
    }
}