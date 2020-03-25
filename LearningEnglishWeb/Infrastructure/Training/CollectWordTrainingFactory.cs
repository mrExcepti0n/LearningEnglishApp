using Data.Core;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Models.Training.CollectWord;
using LearningEnglishWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Infrastructure.Training
{
    public class CollectWordTrainingFactory : TrainingFactoryBase<CollectWordTraining>
    {

        public CollectWordTrainingFactory(IVocabularyService vocabularyService,  LanguageEnum fromLanguage, LanguageEnum toLanguage, bool reverseWay) 
            : base(vocabularyService, fromLanguage, toLanguage, reverseWay)
        {
            
        }

        public override async Task<CollectWordTraining> GetTraining()
        {
            UserWord[] words = await GetWords();

            return new CollectWordTraining(GetQuestions(words), _reverseWay);
        }  


        private IEnumerable<CollectWordQuestion> GetQuestions(UserWord[] words)
        {
            for (var i= 0; i< words.Length; i++)
            {
                var word = words[i].Name.ToLower();
                var translation = words[i].Translation.ToLower();
                yield return new CollectWordQuestion { Number = i + 1, Translation = translation, AnswerLetters = ShuffleWords(translation.ToCharArray()),   Word = word };
            }
        }
    }
}
