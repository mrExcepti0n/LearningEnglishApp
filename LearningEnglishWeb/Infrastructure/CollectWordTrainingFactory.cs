using Data.Core;
using LearningEnglishWeb.Models;
using LearningEnglishWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Infrastructure
{
    public class CollectWordTrainingFactory : TrainingFactoryBase<CollectWordTraining>
    {
        private Random _randomGenerator;

        public CollectWordTrainingFactory(IVocabularyService vocabularyService, IWordImageService wordImageService, LanguageEnum fromLanguage, LanguageEnum toLanguage, bool reverseWay) 
            : base(vocabularyService, wordImageService, fromLanguage, toLanguage, reverseWay)
        {
            _randomGenerator = new Random();
        }

        public override async Task<CollectWordTraining> GetTraining()
        {
            Word[] words = (await _vocabularyService.GetRequiringStudyWords()).ToArray();

            return new CollectWordTraining(_wordImageService, GetQuestions(words));
        }  


        private IEnumerable<CollectWordQuestion> GetQuestions(Word[] words)
        {
            for (var i= 0; i< words.Length; i++)
            {
                var word = words[i].Name.ToLower();
                yield return new CollectWordQuestion { Number = i + 1, RightAnswer = word, AnswerLetters = ReshuffleLetters(word),   Word = words[i].Translation };
            }
        }


        private char[] ReshuffleLetters(string word)
        {
            var letters = word.ToArray();

            for (int i = letters.Length - 1; i >= 1; i--)
            {
                int j = _randomGenerator.Next(i + 1);
                var temp = letters[j];
                letters[j] = letters[i];
                letters[i] = temp;
            }

            return letters;
        }




    }
}
