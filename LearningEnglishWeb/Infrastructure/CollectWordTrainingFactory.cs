using LearningEnglishWeb.Models;
using LearningEnglishWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VocabularyApi.Infrastructure.DataAccess;

namespace LearningEnglishWeb.Infrastructure
{
    public class CollectWordTrainingFactory : TrainingFactoryBase<CollectWordTraining>
    {
        private IVocabularyService _service;
        private Random _randomGenerator;

        public CollectWordTrainingFactory(IVocabularyService service)
        {
            _service = service;
            _randomGenerator = new Random();
        }

        public override CollectWordTraining GetTraining()
        {
            var words = _service.GetRequiringStudyWords().ToArray();

            return new CollectWordTraining(GetQuestions(words));
        }  


        private IEnumerable<TranslateWordQuestion> GetQuestions(Word[] words)
        {
            for (var i= 0; i< words.Length; i++)
            {
                var word = words[i].Name.ToLower();
                yield return new TranslateWordQuestion { Number = i + 1, RightTranslation = word, Word = ReshuffleLetters(word) };
            }
        }


        private string ReshuffleLetters(string word)
        {
            var letters = word.ToArray();

            for (int i = letters.Length - 1; i >= 1; i--)
            {
                int j = _randomGenerator.Next(i + 1);
                var temp = letters[j];
                letters[j] = letters[i];
                letters[i] = temp;
            }

            return new string(letters);
        }




    }
}
