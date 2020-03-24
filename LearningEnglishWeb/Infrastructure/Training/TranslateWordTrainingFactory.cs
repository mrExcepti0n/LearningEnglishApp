using Data.Core;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Models.Training.Shared;
using LearningEnglishWeb.Models.Training.TranslateWord;
using LearningEnglishWeb.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Infrastructure.Training
{
    internal class TranslateWordTrainingFactory : TrainingFactoryBase<TranslateWordTraining>
    {
        public TranslateWordTrainingFactory(IVocabularyService vocabularyService, LanguageEnum fromLanguage, LanguageEnum toLanguage, bool reverseWay) 
            : base(vocabularyService, fromLanguage, toLanguage, reverseWay)
        {

        }

        public override async Task<TranslateWordTraining> GetTraining()
        {
            Word[] words = await GetWords();

            return new TranslateWordTraining(GetQuestions(words).ToList(), _reverseWay);
        }


        private IEnumerable<Question> GetQuestions(Word[] words)
        {
            for (var i = 0; i < words.Length; i++)
            {
                yield return new Question { Number = i + 1, Word = words[i].Name, Translation = words[i].Translation };
            }
        }
    }
}